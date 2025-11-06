namespace ZAShinyWarper
{
    partial class ZAWarpWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            btnZ = new System.Windows.Forms.Button();
            button9 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            listBox1 = new System.Windows.Forms.ListBox();
            label2 = new System.Windows.Forms.Label();
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            button8 = new System.Windows.Forms.Button();
            button7 = new System.Windows.Forms.Button();
            button6 = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            label4 = new System.Windows.Forms.Label();
            button4 = new System.Windows.Forms.Button();
            gBShinyHunt = new System.Windows.Forms.GroupBox();
            numericUpDownSaveFreq = new System.Windows.Forms.NumericUpDown();
            lblSaveFreq = new System.Windows.Forms.Label();
            numericUpDownCamMove = new System.Windows.Forms.NumericUpDown();
            lblCam = new System.Windows.Forms.Label();
            numericUpDownSpawnCheckTime = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            numericUpDownScale = new System.Windows.Forms.NumericUpDown();
            numericUpDownScale2 = new System.Windows.Forms.NumericUpDown();
            lblScale = new System.Windows.Forms.Label();
            lblScale2 = new System.Windows.Forms.Label();
            lblIVSpe = new System.Windows.Forms.Label();
            lblIVSpD = new System.Windows.Forms.Label();
            lblIVSpA = new System.Windows.Forms.Label();
            cBIVSpe = new System.Windows.Forms.ComboBox();
            cBIVSpD = new System.Windows.Forms.ComboBox();
            cBIVSpA = new System.Windows.Forms.ComboBox();
            lblIVDef = new System.Windows.Forms.Label();
            lblIVAtk = new System.Windows.Forms.Label();
            lblIVHP = new System.Windows.Forms.Label();
            cBIVDef = new System.Windows.Forms.ComboBox();
            cBIVAtk = new System.Windows.Forms.ComboBox();
            cBIVHP = new System.Windows.Forms.ComboBox();
            lblIV = new System.Windows.Forms.Label();
            cBSpecies = new System.Windows.Forms.CheckedListBox();
            btnResetSpecies = new System.Windows.Forms.Button();
            lblSpecies = new System.Windows.Forms.Label();
            lblFilter = new System.Windows.Forms.Label();
            cBWhenShinyFound = new System.Windows.Forms.ComboBox();
            lblShinyFound = new System.Windows.Forms.Label();
            btnWarp = new System.Windows.Forms.Button();
            lblCreateTwo = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            gBShinyHunt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSaveFreq).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCamMove).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSpawnCheckTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScale2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(15, 15);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(20, 15);
            label1.TabIndex = 0;
            label1.Text = "IP:";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(46, 12);
            textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(272, 23);
            textBox1.TabIndex = 1;
            textBox1.Text = "192.168.0.1";
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(14, 42);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(304, 32);
            button1.TabIndex = 2;
            button1.Text = "Connect";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnZ);
            groupBox1.Controls.Add(button9);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(listBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(button8);
            groupBox1.Controls.Add(button7);
            groupBox1.Controls.Add(button6);
            groupBox1.Controls.Add(button5);
            groupBox1.Enabled = false;
            groupBox1.Location = new System.Drawing.Point(15, 125);
            groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox1.Size = new System.Drawing.Size(303, 425);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Editing";
            // 
            // btnZ
            // 
            btnZ.Location = new System.Drawing.Point(108, 333);
            btnZ.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnZ.Name = "btnZ";
            btnZ.Size = new System.Drawing.Size(88, 27);
            btnZ.TabIndex = 18;
            btnZ.Text = "Y";
            btnZ.UseVisualStyleBackColor = true;
            btnZ.Click += btnZ_Click;
            // 
            // button9
            // 
            button9.Location = new System.Drawing.Point(204, 166);
            button9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button9.Name = "button9";
            button9.Size = new System.Drawing.Size(88, 27);
            button9.TabIndex = 17;
            button9.Text = "Delete Pos";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(108, 166);
            button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(88, 27);
            button3.TabIndex = 16;
            button3.Text = "Restore Pos";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(12, 166);
            button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(88, 27);
            button2.TabIndex = 15;
            button2.Text = "Save Pos";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new System.Drawing.Point(10, 22);
            listBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            listBox1.Name = "listBox1";
            listBox1.Size = new System.Drawing.Size(285, 139);
            listBox1.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 296);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(61, 15);
            label2.TabIndex = 13;
            label2.Text = "Free warp:";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new System.Drawing.Point(216, 388);
            numericUpDown1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(76, 23);
            numericUpDown1.TabIndex = 12;
            numericUpDown1.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // button8
            // 
            button8.Location = new System.Drawing.Point(12, 333);
            button8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(88, 27);
            button8.TabIndex = 11;
            button8.Text = "←";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button7
            // 
            button7.Location = new System.Drawing.Point(204, 333);
            button7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button7.Name = "button7";
            button7.Size = new System.Drawing.Size(88, 27);
            button7.TabIndex = 10;
            button7.Text = "→";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.Location = new System.Drawing.Point(108, 372);
            button6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(88, 27);
            button6.TabIndex = 9;
            button6.Text = "↓";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(108, 296);
            button5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(88, 27);
            button5.TabIndex = 8;
            button5.Text = "↑";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(55, 555);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(216, 30);
            label4.TabIndex = 4;
            label4.Text = "Many thanks to Kurt for PKHeX and\r\nAnubis for the Z-A shiny stash research.";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(14, 82);
            button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(304, 36);
            button4.TabIndex = 5;
            button4.Text = "ConnectUSB";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // gBShinyHunt
            // 
            gBShinyHunt.Controls.Add(numericUpDownSaveFreq);
            gBShinyHunt.Controls.Add(lblSaveFreq);
            gBShinyHunt.Controls.Add(numericUpDownCamMove);
            gBShinyHunt.Controls.Add(lblCam);
            gBShinyHunt.Controls.Add(numericUpDownSpawnCheckTime);
            gBShinyHunt.Controls.Add(label3);
            gBShinyHunt.Controls.Add(numericUpDownScale);
            gBShinyHunt.Controls.Add(numericUpDownScale2);
            gBShinyHunt.Controls.Add(lblScale);
            gBShinyHunt.Controls.Add(lblScale2);
            gBShinyHunt.Controls.Add(lblIVSpe);
            gBShinyHunt.Controls.Add(lblIVSpD);
            gBShinyHunt.Controls.Add(lblIVSpA);
            gBShinyHunt.Controls.Add(cBIVSpe);
            gBShinyHunt.Controls.Add(cBIVSpD);
            gBShinyHunt.Controls.Add(cBIVSpA);
            gBShinyHunt.Controls.Add(lblIVDef);
            gBShinyHunt.Controls.Add(lblIVAtk);
            gBShinyHunt.Controls.Add(lblIVHP);
            gBShinyHunt.Controls.Add(cBIVDef);
            gBShinyHunt.Controls.Add(cBIVAtk);
            gBShinyHunt.Controls.Add(cBIVHP);
            gBShinyHunt.Controls.Add(lblIV);
            gBShinyHunt.Controls.Add(cBSpecies);
            gBShinyHunt.Controls.Add(btnResetSpecies);
            gBShinyHunt.Controls.Add(lblSpecies);
            gBShinyHunt.Controls.Add(lblFilter);
            gBShinyHunt.Controls.Add(cBWhenShinyFound);
            gBShinyHunt.Controls.Add(lblShinyFound);
            gBShinyHunt.Controls.Add(btnWarp);
            gBShinyHunt.Controls.Add(lblCreateTwo);
            gBShinyHunt.Enabled = false;
            gBShinyHunt.Location = new System.Drawing.Point(327, 15);
            gBShinyHunt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gBShinyHunt.Name = "gBShinyHunt";
            gBShinyHunt.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gBShinyHunt.Size = new System.Drawing.Size(212, 567);
            gBShinyHunt.TabIndex = 6;
            gBShinyHunt.TabStop = false;
            gBShinyHunt.Text = "Shiny Hunting";
            // 
            // numericUpDownSaveFreq
            // 
            numericUpDownSaveFreq.Location = new System.Drawing.Point(144, 207);
            numericUpDownSaveFreq.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownSaveFreq.Name = "numericUpDownSaveFreq";
            numericUpDownSaveFreq.Size = new System.Drawing.Size(63, 23);
            numericUpDownSaveFreq.TabIndex = 28;
            numericUpDownSaveFreq.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // lblSaveFreq
            // 
            lblSaveFreq.AutoSize = true;
            lblSaveFreq.Location = new System.Drawing.Point(14, 209);
            lblSaveFreq.Name = "lblSaveFreq";
            lblSaveFreq.Size = new System.Drawing.Size(90, 15);
            lblSaveFreq.TabIndex = 27;
            lblSaveFreq.Text = "Save frequency:";
            // 
            // numericUpDownCamMove
            // 
            numericUpDownCamMove.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownCamMove.Location = new System.Drawing.Point(14, 183);
            numericUpDownCamMove.Maximum = new decimal(new int[] { 32000, 0, 0, 0 });
            numericUpDownCamMove.Minimum = new decimal(new int[] { 32000, 0, 0, int.MinValue });
            numericUpDownCamMove.Name = "numericUpDownCamMove";
            numericUpDownCamMove.Size = new System.Drawing.Size(193, 23);
            numericUpDownCamMove.TabIndex = 26;
            numericUpDownCamMove.Value = new decimal(new int[] { 16000, 0, 0, 0 });
            // 
            // lblCam
            // 
            lblCam.AutoSize = true;
            lblCam.Location = new System.Drawing.Point(12, 166);
            lblCam.Name = "lblCam";
            lblCam.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lblCam.Size = new System.Drawing.Size(192, 15);
            lblCam.TabIndex = 25;
            lblCam.Text = "Cam move speed (-32000 to 32000)";
            // 
            // numericUpDownSpawnCheckTime
            // 
            numericUpDownSpawnCheckTime.Increment = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownSpawnCheckTime.Location = new System.Drawing.Point(12, 140);
            numericUpDownSpawnCheckTime.Maximum = new decimal(new int[] { 20000, 0, 0, 0 });
            numericUpDownSpawnCheckTime.Name = "numericUpDownSpawnCheckTime";
            numericUpDownSpawnCheckTime.Size = new System.Drawing.Size(193, 23);
            numericUpDownSpawnCheckTime.TabIndex = 24;
            numericUpDownSpawnCheckTime.Value = new decimal(new int[] { 2000, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 123);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(133, 15);
            label3.TabIndex = 23;
            label3.Text = "Spawn check time (ms):";
            // 
            // numericUpDownScale
            // 
            numericUpDownScale.Location = new System.Drawing.Point(80, 508);
            numericUpDownScale.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownScale.Name = "numericUpDownScale";
            numericUpDownScale.Size = new System.Drawing.Size(117, 23);
            numericUpDownScale.TabIndex = 22;
            // 
            // numericUpDownScale2
            // 
            numericUpDownScale2.Location = new System.Drawing.Point(80, 535);
            numericUpDownScale2.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numericUpDownScale2.Name = "numericUpDownScale2";
            numericUpDownScale2.Size = new System.Drawing.Size(117, 23);
            numericUpDownScale2.TabIndex = 22;
            numericUpDownScale2.Value = new decimal(new int[] { 255, 0, 0, 0 });
            // 
            // lblScale
            // 
            lblScale.AutoSize = true;
            lblScale.Location = new System.Drawing.Point(14, 510);
            lblScale.Name = "lblScale";
            lblScale.Size = new System.Drawing.Size(60, 15);
            lblScale.TabIndex = 21;
            lblScale.Text = "Min scale:";
            // 
            // lblScale2
            // 
            lblScale2.AutoSize = true;
            lblScale2.Location = new System.Drawing.Point(14, 538);
            lblScale2.Name = "lblScale2";
            lblScale2.Size = new System.Drawing.Size(61, 15);
            lblScale2.TabIndex = 21;
            lblScale2.Text = "Max scale:";
            // 
            // lblIVSpe
            // 
            lblIVSpe.AutoSize = true;
            lblIVSpe.Location = new System.Drawing.Point(140, 464);
            lblIVSpe.Name = "lblIVSpe";
            lblIVSpe.Size = new System.Drawing.Size(26, 15);
            lblIVSpe.TabIndex = 19;
            lblIVSpe.Text = "Spe";
            // 
            // lblIVSpD
            // 
            lblIVSpD.AutoSize = true;
            lblIVSpD.Location = new System.Drawing.Point(77, 464);
            lblIVSpD.Name = "lblIVSpD";
            lblIVSpD.Size = new System.Drawing.Size(28, 15);
            lblIVSpD.TabIndex = 18;
            lblIVSpD.Text = "SpD";
            // 
            // lblIVSpA
            // 
            lblIVSpA.AutoSize = true;
            lblIVSpA.Location = new System.Drawing.Point(14, 464);
            lblIVSpA.Name = "lblIVSpA";
            lblIVSpA.Size = new System.Drawing.Size(28, 15);
            lblIVSpA.TabIndex = 17;
            lblIVSpA.Text = "SpA";
            // 
            // cBIVSpe
            // 
            cBIVSpe.FormattingEnabled = true;
            cBIVSpe.Location = new System.Drawing.Point(140, 482);
            cBIVSpe.Name = "cBIVSpe";
            cBIVSpe.Size = new System.Drawing.Size(57, 23);
            cBIVSpe.TabIndex = 16;
            // 
            // cBIVSpD
            // 
            cBIVSpD.FormattingEnabled = true;
            cBIVSpD.Location = new System.Drawing.Point(77, 482);
            cBIVSpD.Name = "cBIVSpD";
            cBIVSpD.Size = new System.Drawing.Size(57, 23);
            cBIVSpD.TabIndex = 15;
            // 
            // cBIVSpA
            // 
            cBIVSpA.FormattingEnabled = true;
            cBIVSpA.Location = new System.Drawing.Point(14, 482);
            cBIVSpA.Name = "cBIVSpA";
            cBIVSpA.Size = new System.Drawing.Size(57, 23);
            cBIVSpA.TabIndex = 14;
            // 
            // lblIVDef
            // 
            lblIVDef.AutoSize = true;
            lblIVDef.Location = new System.Drawing.Point(140, 412);
            lblIVDef.Name = "lblIVDef";
            lblIVDef.Size = new System.Drawing.Size(25, 15);
            lblIVDef.TabIndex = 13;
            lblIVDef.Text = "Def";
            // 
            // lblIVAtk
            // 
            lblIVAtk.AutoSize = true;
            lblIVAtk.Location = new System.Drawing.Point(77, 412);
            lblIVAtk.Name = "lblIVAtk";
            lblIVAtk.Size = new System.Drawing.Size(25, 15);
            lblIVAtk.TabIndex = 12;
            lblIVAtk.Text = "Atk";
            // 
            // lblIVHP
            // 
            lblIVHP.AutoSize = true;
            lblIVHP.Location = new System.Drawing.Point(14, 412);
            lblIVHP.Name = "lblIVHP";
            lblIVHP.Size = new System.Drawing.Size(23, 15);
            lblIVHP.TabIndex = 11;
            lblIVHP.Text = "HP";
            // 
            // cBIVDef
            // 
            cBIVDef.FormattingEnabled = true;
            cBIVDef.Location = new System.Drawing.Point(140, 430);
            cBIVDef.Name = "cBIVDef";
            cBIVDef.Size = new System.Drawing.Size(57, 23);
            cBIVDef.TabIndex = 10;
            // 
            // cBIVAtk
            // 
            cBIVAtk.FormattingEnabled = true;
            cBIVAtk.Location = new System.Drawing.Point(77, 430);
            cBIVAtk.Name = "cBIVAtk";
            cBIVAtk.Size = new System.Drawing.Size(57, 23);
            cBIVAtk.TabIndex = 9;
            // 
            // cBIVHP
            // 
            cBIVHP.FormattingEnabled = true;
            cBIVHP.Location = new System.Drawing.Point(14, 430);
            cBIVHP.Name = "cBIVHP";
            cBIVHP.Size = new System.Drawing.Size(57, 23);
            cBIVHP.TabIndex = 8;
            // 
            // lblIV
            // 
            lblIV.AutoSize = true;
            lblIV.Location = new System.Drawing.Point(14, 395);
            lblIV.Name = "lblIV";
            lblIV.Size = new System.Drawing.Size(25, 15);
            lblIV.TabIndex = 7;
            lblIV.Text = "IVs:";
            // 
            // cBSpecies
            // 
            cBSpecies.CheckOnClick = true;
            cBSpecies.FormattingEnabled = true;
            cBSpecies.IntegralHeight = false;
            cBSpecies.Location = new System.Drawing.Point(14, 294);
            cBSpecies.Name = "cBSpecies";
            cBSpecies.Size = new System.Drawing.Size(193, 94);
            cBSpecies.TabIndex = 6;
            cBSpecies.ItemCheck += cBSpecies_ItemCheck;
            cBSpecies.SelectedIndexChanged += cBSpecies_SelectedIndexChanged;
            // 
            // btnResetSpecies
            // 
            btnResetSpecies.Location = new System.Drawing.Point(145, 273);
            btnResetSpecies.Name = "btnResetSpecies";
            btnResetSpecies.Size = new System.Drawing.Size(62, 23);
            btnResetSpecies.TabIndex = 29;
            btnResetSpecies.Text = "Reset";
            btnResetSpecies.UseVisualStyleBackColor = true;
            btnResetSpecies.Click += btnResetSpecies_Click;
            // 
            // lblSpecies
            // 
            lblSpecies.AutoSize = true;
            lblSpecies.Location = new System.Drawing.Point(12, 276);
            lblSpecies.Name = "lblSpecies";
            lblSpecies.Size = new System.Drawing.Size(46, 15);
            lblSpecies.TabIndex = 5;
            lblSpecies.Text = "Species";
            // 
            // lblFilter
            // 
            lblFilter.AutoSize = true;
            lblFilter.Location = new System.Drawing.Point(12, 261);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new System.Drawing.Size(33, 15);
            lblFilter.TabIndex = 4;
            lblFilter.Text = "Filter";
            // 
            // cBWhenShinyFound
            // 
            cBWhenShinyFound.FormattingEnabled = true;
            cBWhenShinyFound.Location = new System.Drawing.Point(12, 97);
            cBWhenShinyFound.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            cBWhenShinyFound.Name = "cBWhenShinyFound";
            cBWhenShinyFound.Size = new System.Drawing.Size(193, 23);
            cBWhenShinyFound.TabIndex = 3;
            // 
            // lblShinyFound
            // 
            lblShinyFound.AutoSize = true;
            lblShinyFound.Location = new System.Drawing.Point(12, 77);
            lblShinyFound.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblShinyFound.Name = "lblShinyFound";
            lblShinyFound.Size = new System.Drawing.Size(107, 15);
            lblShinyFound.TabIndex = 2;
            lblShinyFound.Text = "When shiny found:";
            // 
            // btnWarp
            // 
            btnWarp.Location = new System.Drawing.Point(12, 43);
            btnWarp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnWarp.Name = "btnWarp";
            btnWarp.Size = new System.Drawing.Size(194, 27);
            btnWarp.TabIndex = 1;
            btnWarp.Text = "Begin Warping";
            btnWarp.UseVisualStyleBackColor = true;
            btnWarp.Click += btnWarp_Click;
            // 
            // lblCreateTwo
            // 
            lblCreateTwo.AutoSize = true;
            lblCreateTwo.Location = new System.Drawing.Point(8, 23);
            lblCreateTwo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lblCreateTwo.Name = "lblCreateTwo";
            lblCreateTwo.Size = new System.Drawing.Size(172, 15);
            lblCreateTwo.TabIndex = 0;
            lblCreateTwo.Text = "Create at least two warp points.";
            // 
            // ZAWarpWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(553, 594);
            Controls.Add(gBShinyHunt);
            Controls.Add(button4);
            Controls.Add(label4);
            Controls.Add(groupBox1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ZAWarpWindow";
            Text = "Z-A Shiny Warper by Berichan";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            gBShinyHunt.ResumeLayout(false);
            gBShinyHunt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSaveFreq).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCamMove).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSpawnCheckTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScale).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownScale2).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox gBShinyHunt;
        private System.Windows.Forms.Label lblCreateTwo;
        private System.Windows.Forms.ComboBox cBWhenShinyFound;
        private System.Windows.Forms.Label lblShinyFound;
        private System.Windows.Forms.Button btnWarp;
        private System.Windows.Forms.CheckedListBox cBSpecies;
        private System.Windows.Forms.Button btnResetSpecies;
        private System.Windows.Forms.Label lblSpecies;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Label lblIVSpe;
        private System.Windows.Forms.Label lblIVSpD;
        private System.Windows.Forms.Label lblIVSpA;
        private System.Windows.Forms.ComboBox cBIVSpe;
        private System.Windows.Forms.ComboBox cBIVSpD;
        private System.Windows.Forms.ComboBox cBIVSpA;
        private System.Windows.Forms.Label lblIVDef;
        private System.Windows.Forms.Label lblIVAtk;
        private System.Windows.Forms.Label lblIVHP;
        private System.Windows.Forms.ComboBox cBIVDef;
        private System.Windows.Forms.ComboBox cBIVAtk;
        private System.Windows.Forms.ComboBox cBIVHP;
        private System.Windows.Forms.Label lblIV;
        private System.Windows.Forms.NumericUpDown numericUpDownScale;
        private System.Windows.Forms.NumericUpDown numericUpDownScale2;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.Label lblScale2;
        private System.Windows.Forms.NumericUpDown numericUpDownSpawnCheckTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownCamMove;
        private System.Windows.Forms.Label lblCam;
        private System.Windows.Forms.NumericUpDown numericUpDownSaveFreq;
        private System.Windows.Forms.Label lblSaveFreq;
        private System.Windows.Forms.Button btnZ;
    }
}

