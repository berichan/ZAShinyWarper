using NHSE.Injection;
using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PLAWarper
{
    public enum ShinyFoundAction
    {
        StopOnFound,
        CacheAndContinue
    }

    public enum IVType
    {
        Any,
        Perfect, // 31
        Zero // 0
    }
    public enum GenderDependent : ushort
    {
        Venusaur = 3,
        Butterfree = 12,
        Rattata = 19,
        Raticate = 20,
        Pikachu = 25,
        Raichu = 26,
        Zubat = 41,
        Golbat = 42,
        Gloom = 44,
        Vileplume = 45,
        Kadabra = 64,
        Alakazam = 65,
        Doduo = 84,
        Dodrio = 85,
        Hypno = 97,
        Rhyhorn = 111,
        Rhydon = 112,
        Goldeen = 118,
        Seaking = 119,
        Scyther = 123,
        Magikarp = 129,
        Gyarados = 130,
        Eevee = 133,
        Meganium = 154,
        Ledyba = 165,
        Ledian = 166,
        Xatu = 178,
        Sudowoodo = 185,
        Politoed = 186,
        Aipom = 190,
        Wooper = 194,
        Quagsire = 195,
        Murkrow = 198,
        Wobbuffet = 202,
        Girafarig = 203,
        Gligar = 207,
        Steelix = 208,
        Scizor = 212,
        Heracross = 214,
        Sneasel = 215,
        Ursaring = 217,
        Piloswine = 221,
        Octillery = 224,
        Houndoom = 229,
        Donphan = 232,
        Torchic = 255,
        Combusken = 256,
        Blaziken = 257,
        Beautifly = 267,
        Dustox = 269,
        Ludicolo = 272,
        Nuzleaf = 274,
        Shiftry = 275,
        Meditite = 307,
        Medicham = 308,
        Roselia = 315,
        Gulpin = 316,
        Swalot = 317,
        Numel = 322,
        Camerupt = 323,
        Cacturne = 332,
        Milotic = 350,
        Relicanth = 369,
        Starly = 396,
        Staravia = 397,
        Staraptor = 398,
        Bidoof = 399,
        Bibarel = 400,
        Kricketot = 401,
        Kricketune = 402,
        Shinx = 403,
        Luxio = 404,
        Luxray = 405,
        Roserade = 407,
        Combee = 415,
        Pachirisu = 417,
        Floatzel = 418,
        Buizel = 419,
        Ambipom = 424,
        Gible = 443,
        Gabite = 444,
        Garchomp = 445,
        Hippopotas = 449,
        Hippowdon = 450,
        Croagunk = 453,
        Toxicroak = 454,
        Finneon = 456,
        Lumineon = 457,
        Snover = 459,
        Abomasnow = 460,
        Weavile = 461,
        Rhyperior = 464,
        Tangrowth = 465,
        Mamoswine = 473,
        Unfezant = 521,
        Frillish = 592,
        Jellicent = 593,
        Pyroar = 668,
    }

    public class ShinyHunter<T> where T : PKM, new()
    {
        public class ShinyFilter<T> where T : PKM, new()
        {
            public IVType[] IVs { get; set; } = new IVType[6]
            {
                IVType.Any,
                IVType.Any,
                IVType.Any,
                IVType.Any,
                IVType.Any,
                IVType.Any
            };
            public List<ushort>? SpeciesList { get; set; } = null;
            public byte SizeMinimum { get; set; } = 0;
            public byte SizeMaximum { get; set; } = 0;

            public ShinyFilter()
            {

            }

            public ShinyFilter(IVType[] iVs, ushort species, byte sizeMin) { }

            public bool MatchesFilter(T pk)
            {
                if (!MatchesFilterSpeciesOrAlpha(pk))
                    return false;
                // IVs
                for (int i = 0; i < 6; i++)
                {
                    var iv = pk.GetIV(i);
                    switch (IVs[i])
                    {
                        case IVType.Perfect:
                            if (iv != 31)
                                return false;
                            break;
                        case IVType.Zero:
                            if (iv != 0)
                                return false;
                            break;
                    }
                }
                return true;
            }

            // This is required otherwise the spawn may hog that encounter until it is defeated/captured.
            public bool MatchesFilterSpeciesOrAlpha(T pk)
            {
                // Species - check if species matches any in the list
                if (SpeciesList != null && SpeciesList.Count > 0)
                {
                    if (!SpeciesList.Contains(pk.Species))
                        return false;
                }
                // Size
                if (pk is IScaledSize3 pks)
                {
                    if (pks.Scale < SizeMinimum)
                        return false;
                    if (pks.Scale > SizeMaximum)
                        return false;
                }
                return true;
            }
        }

        public class StashedShiny<T> where T : PKM, new ()
        {
            public T PKM { get; private set; }
            public ulong LocationHash { get; private set; } = 0;
            public uint EncryptionConstant => PKM.EncryptionConstant;

            public StashedShiny(T pk, ulong locHash)
            {
                PKM = pk;
                LocationHash = locHash;
            }

            public override string ToString() => $"Location hash: {LocationHash:X16}\r\n" + ShowdownParsing.GetShowdownText(PKM) + "\r\n";
            
        }

        private const int STASHED_SHINIES_MAX = 10;
        private const int PA9_SIZE = 0x158;
        private const int PA9_BUFFER = 0x1F0;
        private const string STASH_FOLDER = "StashedShinies";
        
        private readonly long[] jumpsPos = new long[] { 0x5F0B250, 0x120, 0x168 }; // [[[main+5F0B250]+120]+168]

        public IList<StashedShiny<T>> PreviousStashedShinies { get; private set; } = [];
        public IList<StashedShiny<T>> StashedShinies { get; private set; } = [];
        public IList<StashedShiny<T>> DifferentShinies { get; private set; } = [];
        public IList<string> StashedShinesPing { get; private set; } = [];
        public ShinyFilter<T> Filter { get; private set; } = new ShinyFilter<T>();

        public static string PokeImg(PKM pkm, bool canGmax)
        {
            bool md = false;
            bool fd = false;
            string[] baseLink;
            baseLink = "https://raw.githubusercontent.com/zyro670/HomeImages/master/128x128/poke_capture_0001_000_mf_n_00000000_f_n.png".Split('_');

            if (Enum.IsDefined(typeof(GenderDependent), pkm.Species) && !canGmax && pkm.Form is 0)
            {
                if (pkm.Gender is 0 && pkm.Species is not (ushort)Species.Torchic)
                    md = true;
                else fd = true;
            }

            int form = pkm.Species switch
            {
                (ushort)Species.Sinistea or (ushort)Species.Polteageist or (ushort)Species.Rockruff or (ushort)Species.Mothim => 0,
                (ushort)Species.Alcremie when pkm.IsShiny || canGmax => 0,
                _ => pkm.Form,

            };

            if (pkm.Species is (ushort)Species.Sneasel)
            {
                if (pkm.Gender is 0)
                    md = true;
                else fd = true;
            }

            if (pkm.Species is (ushort)Species.Basculegion)
            {
                if (pkm.Gender is 0)
                {
                    md = true;
                    pkm.Form = 0;
                }
                else
                    pkm.Form = 1;

                string s = pkm.IsShiny ? "r" : "n";
                string g = md && pkm.Gender is not 1 ? "md" : "fd";
                return $"https://raw.githubusercontent.com/zyro670/HomeImages/master/128x128/poke_capture_0" + $"{pkm.Species}" + "_00" + $"{pkm.Form}" + "_" + $"{g}" + "_n_00000000_f_" + $"{s}" + ".png";
            }

            baseLink[2] = pkm.Species < 10 ? $"000{pkm.Species}" : pkm.Species < 100 && pkm.Species > 9 ? $"00{pkm.Species}" : pkm.Species >= 1000 ? $"{pkm.Species}" : $"0{pkm.Species}";
            baseLink[3] = pkm.Form < 10 ? $"00{form}" : $"0{form}";
            baseLink[4] = pkm.PersonalInfo.OnlyFemale ? "fo" : pkm.PersonalInfo.OnlyMale ? "mo" : pkm.PersonalInfo.Genderless ? "uk" : fd ? "fd" : md ? "md" : "mf";
            baseLink[5] = canGmax ? "g" : "n";
            baseLink[6] = "0000000" + (pkm.Species is (ushort)Species.Alcremie && !canGmax ? pkm.Data[0xD0] : 0);
            baseLink[8] = pkm.IsShiny ? "r.png" : "n.png";
            return string.Join("_", baseLink);
        }

        private ulong getShinyStashOffset(IRAMReadWriter bot)
        {
            return bot.FollowMainPointer(jumpsPos);
        }

        /// <summary>
        /// Loads the stashed shinies from RAM, compares them to previous and saves them to disk.
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="path"></param>
        /// <returns>whether or not a new one has entered since previous</returns>
        public bool LoadStashedShinies(IRAMReadWriter bot, string path)
        {
            var offs = getShinyStashOffset(bot);
            PreviousStashedShinies = StashedShinies;
            StashedShinies = new List<StashedShiny<T>>();
            StashedShinesPing = new List<string>();

            if (!Directory.Exists(STASH_FOLDER))
                Directory.CreateDirectory(STASH_FOLDER);

            for (int i = 0; i < STASHED_SHINIES_MAX; i++)
            {
                var data = bot.ReadBytes(offs + (ulong)(i * PA9_BUFFER), PA9_SIZE + 8, RWMethod.Absolute);
                var construct = typeof(T).GetConstructor(new Type[1] { typeof(Memory<byte>) });
                Debug.Assert(construct != null, "PKM type must have a Memory<byte> constructor");

                var pk = (T)construct.Invoke(new object[] { new Memory<byte>(data[8..]) });
                var location = BitConverter.ToUInt64(data, 0);
                if (pk.Species != 0)
                {
                    var stashed = new StashedShiny<T>(pk, location);
                    StashedShinies.Add(stashed);
                    StashedShinesPing.Add(PokeImg(pk, false));

                    var fileName = Path.Combine(STASH_FOLDER, pk.FileName);
                    File.WriteAllBytes(fileName, pk.DecryptedPartyData);
                }
            }

            if (!string.IsNullOrEmpty(path))
                File.WriteAllText(path, GetShinyStashInfo(StashedShinies));

            DifferentShinies = StashedShinies.Where(pk => !PreviousStashedShinies.Any(x => x.EncryptionConstant == pk.EncryptionConstant)).ToList();
            return DifferentShinies.Any();
        }

        public string GetShinyStashInfo(IList<StashedShiny<T>> stash)
        {
            var info = new StringBuilder();
            foreach (var pk in stash)
            {
                info.AppendLine(pk.ToString());
            }
            return info.ToString();
        }

        public string GetShowdownSets(IList<T> pkms)
        {
            var sets = new StringBuilder();
            foreach (var pk in pkms)
            {
                sets.AppendLine(ShowdownParsing.GetShowdownText(pk) + "\r\n");
            }
            return sets.ToString();
        }

        public void InitialiseFilter(ShinyFilter<T> filter)
        {
            Filter = filter;
        }
    }
}
