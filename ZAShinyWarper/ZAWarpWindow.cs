using NHSE.Injection;
using PKHeX.Core;
using PLAWarper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PLADumper
{
    public partial class ZAWarpWindow : Form
    {
        private readonly long[] jumpsPos = new long[] { 0x41EC340, 0x248, 0x00, 0x138 }; // [[[[main+41EC340]+248]+00]+138]+90
        private static IRAMReadWriter bot = default!;

        private List<Vector3> positions = new List<Vector3>();
        private const string configName = "positions.txt";
        private const string filterConfigName = "filter_config.txt";

        private ShinyHunter<PK9> shinyHunter = new ShinyHunter<PK9>();
        private readonly List<PictureBox> PingList;

        private LabelForm lf = new LabelForm();

        public ComboBox[] CBIVs = default!;

        // Bot
        private bool warping = false;
        private int warpsPerSave = 3;
        private int currentWarps = 0;

        public ZAWarpWindow()
        {
            InitializeComponent();
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            Application.ApplicationExit += (s, e) => cleanUpBot();
            PingList = new List<PictureBox>() { ShinymonPic1, ShinymonPic2, ShinymonPic3, ShinymonPic4, ShinymonPic5, ShinymonPic6, ShinymonPic7, ShinymonPic8, ShinymonPic9, ShinymonPic10 };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load enums into comboboxes
            // WhenShinyFound
            foreach (var item in Enum.GetValues<ShinyFoundAction>())
                cBWhenShinyFound.Items.Add(item);
            cBWhenShinyFound.SelectedIndex = 0;

            // Species
            cBSpecies.Items.Add("Any");
            foreach (var item in ZAZukan.PokedexNumbersZA)
                cBSpecies.Items.Add($"{item.Value:D3} - {item.Key}");
            cBSpecies.SetItemChecked(0, true);

            // IVs
            CBIVs = new ComboBox[6]
            {
                cBIVHP,
                cBIVAtk,
                cBIVDef,
                cBIVSpA,
                cBIVSpD,
                cBIVSpe
            };

            foreach (var cb in CBIVs)
            {
                foreach (var item in Enum.GetValues<IVType>())
                    cb.Items.Add(item);
                cb.SelectedIndex = 0;
            }

            if (File.Exists("config.txt"))
            {
                var ip = File.ReadAllText("config.txt");
                textBox1.Text = ip;
            }

            LoadFilterConfig();
            LoadAllAndUpdateUI();
        }

        private ShinyHunter<PK9>.ShinyFilter<PK9> getFilter()
        {
            var filter = new ShinyHunter<PK9>.ShinyFilter<PK9>();
            // Species - collect all checked species
            var checkedSpecies = new List<ushort>();
            for (int i = 0; i < cBSpecies.Items.Count; i++)
            {
                if (cBSpecies.GetItemChecked(i) && i > 0) // Skip "Any" at index 0
                {
                    var sel = cBSpecies.Items[i].ToString()!;
                    var spl = sel.Split(" - ");
                    checkedSpecies.Add(ushort.Parse(spl[0]));
                }
            }

            // If "Any" is checked or no species selected, leave filter.Species as null
            // Otherwise, set the list of species
            if (checkedSpecies.Count > 0)
                filter.SpeciesList = checkedSpecies;

            // IVs
            for (int i = 0; i < 6; i++)
            {
                filter.IVs[i] = (IVType)CBIVs[i].SelectedItem;
            }

            // Size
            filter.SizeMinimum = (byte)numericUpDownScale.Value;
            filter.SizeMaximum = (byte)numericUpDownScale2.Value;
            return filter;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var botsys = new SysBot();
                botsys.Connect(textBox1.Text, 6000);
                bot = botsys;
                groupBox1.Enabled = true;
                gBShinyHunt.Enabled = true;
                shinyHunter.LoadStashedShinies(bot, "sets.txt");
                var client = new HttpClient();
                await DisplayStashedShinies(client).ConfigureAwait(false);
                MessageBox.Show($"Connected to SysBot (network). The following shinies are stashed on your save currently: \r\n{shinyHunter.GetShinyStashInfo([.. shinyHunter.StashedShinies.Reverse()])}");
                bot.SendBytes(Encoding.ASCII.GetBytes("detachController\r\n"));
                cleanUpBot();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var botusb = new USBBot();
                botusb.Connect();
                bot = botusb;
                groupBox1.Enabled = true;
                gBShinyHunt.Enabled = true;
                shinyHunter.LoadStashedShinies(bot, "sets.txt");
                var client = new HttpClient();
                await DisplayStashedShinies(client).ConfigureAwait(false);
                MessageBox.Show($"Connected to UsbBot (USB). The following shinies are stashed on your save currently: \r\n{shinyHunter.GetShinyStashInfo([.. shinyHunter.StashedShinies.Reverse()])}");
                bot.SendBytes(Encoding.ASCII.GetBytes("detachController\r\n"));
                cleanUpBot();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            File.WriteAllText("config.txt", textBox1.Text);
        }

        private void MovePlayer(float x, float y)
        {
            int stepOffset = (int)numericUpDown1.Value;
            ulong ramOffset = getPcoordOfs();

            var bytes = bot.ReadBytes(ramOffset, 12, RWMethod.Absolute);
            float xn = BitConverter.ToSingle(bytes, 0);
            float yn = BitConverter.ToSingle(bytes, 8);
            xn += (x * stepOffset); yn += (y * stepOffset);

            bot.WriteBytes(BitConverter.GetBytes(xn), ramOffset, RWMethod.Absolute);
            bot.WriteBytes(BitConverter.GetBytes(yn), ramOffset + 8, RWMethod.Absolute);
        }

        private Vector3 GetPos()
        {
            ulong ramOffset = getPcoordOfs();
            var bytes = bot.ReadBytes(ramOffset, 12, RWMethod.Absolute);

            float xn = BitConverter.ToSingle(bytes, 0);
            float yn = BitConverter.ToSingle(bytes, 4);
            float zn = BitConverter.ToSingle(bytes, 8);

            return new Vector3() { x = xn, y = yn, z = zn };
        }

        private void SetPos(float x, float y, float z)
        {
            ulong ramOffset = getPcoordOfs();

            byte[] xb = BitConverter.GetBytes(x);
            byte[] yb = BitConverter.GetBytes(y);
            byte[] zb = BitConverter.GetBytes(z);

            var bytes = xb.Concat(yb).Concat(zb);

            bot.WriteBytes(bytes.ToArray(), ramOffset, RWMethod.Absolute);
        }

        private ulong getPcoordOfs()
        {
            return bot.FollowMainPointer(jumpsPos) + 0x90;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MovePlayer(0, 1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MovePlayer(0, -1);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MovePlayer(1, 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MovePlayer(-1, 0);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SaveNewValue();
        }

        private void SaveNewValue()
        {
            var pos = GetPos();
            positions.Add(pos);
            SaveAllAndUpdateUI();

            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void LoadAllAndUpdateUI()
        {
            if (File.Exists(configName))
            {
                var lines = File.ReadAllLines(configName);
                foreach (var line in lines)
                    positions.Add(Vector3.FromString(line));
            }

            UpdateUI();
        }

        private void SaveAllAndUpdateUI()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pos in positions)
                sb.AppendLine(pos.ToString());

            File.WriteAllText(configName, sb.ToString());

            UpdateUI();
        }

        private void UpdateUI()
        {
            listBox1.SelectedIndex = -1;
            listBox1.Items.Clear();
            foreach (var pos in positions)
                listBox1.Items.Add(pos);
        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                var toSend = (Vector3)listBox1.SelectedItem;
                SetPos(toSend.x, toSend.y, toSend.z);
                Thread.Sleep(4000); // fall out

                int i;
                lf.PerformSafely(() => lf.SetText("Warping..."));
                lf.PerformSafely(() => lf.Show());

                for (i = 0; i < 15; ++i)
                {
                    lf.PerformSafely(() => lf.SetText($"Warping... ({i + 1}/15) Please wait."));
                    if (GetPos().y == toSend.y)
                        break;
                    SetPos(toSend.x, toSend.y, toSend.z);
                    await Task.Delay(1100).ConfigureAwait(false);
                }

                lf.PerformSafely(() => lf.Hide());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                positions.RemoveAt(listBox1.SelectedIndex);
                SaveAllAndUpdateUI();
            }
        }

        private void btnResetSpecies_Click(object sender, EventArgs e)
        {
            // Uncheck all items
            for (int i = 0; i < cBSpecies.Items.Count; i++)
            {
                cBSpecies.SetItemChecked(i, false);
            }
            // Check "Any" (index 0)
            cBSpecies.SetItemChecked(0, true);
            SaveFilterConfig();
        }

        private void cBSpecies_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Save filter config when species selection changes
            // Use BeginInvoke to ensure the change is applied before saving
            BeginInvoke(new Action(() => SaveFilterConfig()));
        }

        private void SaveFilterConfig()
        {
            try
            {
                var sb = new StringBuilder();
                // Save checked species indices
                var checkedIndices = new List<int>();
                for (int i = 0; i < cBSpecies.Items.Count; i++)
                {
                    if (cBSpecies.GetItemChecked(i))
                        checkedIndices.Add(i);
                }
                sb.AppendLine("SpeciesIndices=" + string.Join(",", checkedIndices));

                File.WriteAllText(filterConfigName, sb.ToString());
            }
            catch
            {
                // Silently fail if we can't save config
            }
        }

        private void LoadFilterConfig()
        {
            try
            {
                if (!File.Exists(filterConfigName))
                    return;

                var lines = File.ReadAllLines(filterConfigName);
                foreach (var line in lines)
                {
                    if (line.StartsWith("SpeciesIndices="))
                    {
                        var indicesStr = line.Substring("SpeciesIndices=".Length);
                        if (string.IsNullOrEmpty(indicesStr))
                            continue;

                        var indices = indicesStr.Split(',').Select(int.Parse).ToList();

                        // First uncheck all
                        for (int i = 0; i < cBSpecies.Items.Count; i++)
                        {
                            cBSpecies.SetItemChecked(i, false);
                        }

                        // Then check the saved ones
                        foreach (var idx in indices)
                        {
                            if (idx < cBSpecies.Items.Count)
                                cBSpecies.SetItemChecked(idx, true);
                        }
                    }
                }
            }
            catch
            {
                // If loading fails, just use defaults ("Any" checked)
            }
        }

        private void setFiltersEnableState(bool enabled)
        {
            cBSpecies.PerformSafely(() => cBSpecies.Enabled = enabled);
            foreach (var cb in CBIVs)
                cb.PerformSafely(() => cb.Enabled = enabled);
            numericUpDownScale.PerformSafely(() => numericUpDownScale.Enabled = enabled);
            numericUpDownSpawnCheckTime.PerformSafely(() => numericUpDownSpawnCheckTime.Enabled = enabled);
            cBWhenShinyFound.PerformSafely(() => cBWhenShinyFound.Enabled = enabled);
            numericUpDownCamMove.PerformSafely(() => numericUpDownCamMove.Enabled = enabled);
            numericUpDownSaveFreq.PerformSafely(() => numericUpDownSaveFreq.Enabled = enabled);
            groupBox1.PerformSafely(() => groupBox1.Enabled = enabled);
            btnResetSpecies.PerformSafely(() => btnResetSpecies.Enabled = enabled);
            numericUpDownScale2.PerformSafely(() => numericUpDownScale2.Enabled = enabled);
        }

        // Bot

        private void cleanUpBot()
        {
            if (bot != null && bot.Connected)
            {
                bot.SendBytes(Encoding.ASCII.GetBytes("setStick RIGHT 0 0\r\n"));
                bot.SendBytes(Encoding.ASCII.GetBytes("detachController\r\n"));
            }
        }

        private async Task saveGame()
        {
            bot.SendBytes(Encoding.ASCII.GetBytes("click X\r\n"));
            await Task.Delay(1_000).ConfigureAwait(false);
            bot.SendBytes(Encoding.ASCII.GetBytes("click R\r\n"));
            await Task.Delay(1_000).ConfigureAwait(false);
            bot.SendBytes(Encoding.ASCII.GetBytes("click A\r\n"));
            await Task.Delay(5_000).ConfigureAwait(false); // wait for save
            bot.SendBytes(Encoding.ASCII.GetBytes("click B\r\n"));
            await Task.Delay(0_800).ConfigureAwait(false);
            bot.SendBytes(Encoding.ASCII.GetBytes("click B\r\n"));
            await Task.Delay(0_800).ConfigureAwait(false);
        }

        private async void btnWarp_Click(object sender, EventArgs e)
        {
            if (warping)
            {
                warping = false;
                setFiltersEnableState(true);
                btnWarp.PerformSafely(() => btnWarp.Text = "Start Warping");
                cleanUpBot();
                return;
            }

            if (positions.Count < 2)
            {
                MessageBox.Show("Not enough postions have been set to warp to!");
                return;
            }

            var client = new HttpClient();
            var filter = getFilter();
            var warpInterval = (int)numericUpDownSpawnCheckTime.Value;
            var camSpeed = (int)numericUpDownCamMove.Value;
            var action = (ShinyFoundAction)cBWhenShinyFound.SelectedItem!;
            var saveFrequency = (int)numericUpDownSaveFreq.Value;

            int shiniesFound = 0;

            currentWarps = 0;

            warping = true;
            setFiltersEnableState(false);
            btnWarp.PerformSafely(() => btnWarp.Text = "Warping. Click to end.");

            // Rotate camera for spawns
            if (camSpeed != 0)
                bot.SendBytes(Encoding.ASCII.GetBytes($"setStick RIGHT {camSpeed} 0\r\n"));

            while (warping)
            {
                currentWarps++;
                if (currentWarps % saveFrequency == 0)
                    await saveGame().ConfigureAwait(false);

                // Check shinies first as a new one may have spawned before we move
                var newFound = shinyHunter.LoadStashedShinies(bot, "sets.txt");
                if (newFound)
                {
                    await DisplayStashedShinies(client).ConfigureAwait(false);
                    var newShinies = shinyHunter.DifferentShinies;
                    foreach (var pk in newShinies)
                    {
                        shiniesFound++;
                        if (filter.MatchesFilter(pk.PKM))
                        {
                            // Found one
                            switch (action)
                            {
                                case ShinyFoundAction.StopOnFound:
                                    warping = false;
                                    cleanUpBot();
                                    bot.SendBytes(Encoding.ASCII.GetBytes("click X\r\n"));
                                    btnWarp.PerformSafely(() => btnWarp.Text = "Start Warping");
                                    setFiltersEnableState(true);
                                    MessageBox.Show($"A shiny matching the filter has been found after {currentWarps} attempts! Stopping warping.\r\n\r\n{pk}\r\n" +
                                                         (pk.PKM.Scale == 255 ? "This Pokemon is ALPHA!" : "This Pokemon is not an alpha"), "Found!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                            }
                        }
                        else // Notify the user anyway, but don't stop spawning/warping. The user should know that a shiny is found because it will occupy one of the shiny stash slots until it is removed
                        {
                            CrossThreadExtensions.DoThreaded(() =>
                            {
                                MessageBox.Show($"The following shiny has been found, but does not match your filter. You may wish to remove it such that it doesn't occupy one of your shiny stash slots.\r\n\r\n{pk}\r\n" +
                                                             (pk.PKM.Scale == 255 ? "This Pokemon is ALPHA!" : "This Pokemon is not an alpha"), "Found something we don't want!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            });
                        }
                    }
                }

                foreach (var pos in positions)
                {
                    if (!warping)
                        break;
                    SetPos(pos.x, pos.y, pos.z);
                    await Task.Delay(1_000).ConfigureAwait(false); // fall out and load species
                    // handle falling out
                    int tries = 25;
                    for (; tries > 0; --tries)
                    {
                        // check for less than 0.02 difference to avoid float precision issues. We only care about Y here as X/Z may vary due to terrain
                        if (GetPos().y >= pos.y - 0.02f && GetPos().y <= pos.y + 0.02f)
                            break;
                        SetPos(pos.x, pos.y + (tries > 20 ? 1 : 0), pos.z);
                        await Task.Delay(1_200).ConfigureAwait(false);
                    }

                    if (tries == 0) // failed to load
                    {
                        warping = false;
                        cleanUpBot();
                        btnWarp.PerformSafely(() => btnWarp.Text = "Start Warping");
                        MessageBox.Show($"Warping has failed, please check the console!");
                        break;
                    }

                    if (pos.flags.Contains("instant"))
                        continue;

                    if (pos.flags.Contains("halfwait"))
                        await Task.Delay(warpInterval / 2).ConfigureAwait(false);
                    else
                        await Task.Delay(warpInterval).ConfigureAwait(false);
                }
            }
        }
        private void ResetImage()
        {
            foreach (var pic in PingList)
            {
                pic.PerformSafely(() => pic.Image = null);
            }
        }
        private async Task DisplayStashedShinies(HttpClient client)
        {
            ResetImage();
            for (int i = 0; i < shinyHunter.StashedShinesPing.Count; i++)
            {
                var Picurl = await client.GetStreamAsync(shinyHunter.StashedShinesPing[i]).ConfigureAwait(false);
                var img = Image.FromStream(Picurl);
                PingList[i].PerformSafely(() => PingList[i].Image = img);
            }
        }
        private void cBSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Select "Any" if none are selected
            bool anyChecked = false;
            for (int i = 0; i < cBSpecies.Items.Count; i++)
            {
                if (cBSpecies.GetItemChecked(i))
                {
                    anyChecked = true;
                    break;
                }
            }
            if (!anyChecked)
            {
                cBSpecies.SetItemChecked(0, true);
                return;
            }
            // Clear everything else if "Any" is selected
            if (cBSpecies.SelectedIndex == 0)
            {
                for (int i = 1; i < cBSpecies.Items.Count; i++)
                {
                    cBSpecies.SetItemChecked(i, false);
                }
            }
            else // Clear "Any" if anything else is selected
            {
                cBSpecies.SetItemChecked(0, false);
            }
        }
    }

    public struct Vector3
    {
        public float x, y, z;
        public string[] flags = [];

        public Vector3()
        {
            x = 0; y = 0; z = 0;
            flags = [];
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            flags = [];
        }

        public override string ToString()
        {
            return $"{x},{y},{z};{string.Join(',', flags)}";
        }

        public static Vector3 FromString(string s)
        {
            var prePostFlags = s.Split(';');
            var spl = prePostFlags[0].Split(',');
            Vector3 v = new Vector3();
            v.x = float.Parse(spl[0]);
            v.y = float.Parse(spl[1]);
            v.z = float.Parse(spl[2]);

            if (prePostFlags.Length > 1)
            {
                var nFlags = new List<string>();
                spl = prePostFlags[1].Split(',');
                foreach (var str in spl)
                    nFlags.Add(str);
                v.flags = nFlags.ToArray();
            }

            return v;
        }
    }
}
