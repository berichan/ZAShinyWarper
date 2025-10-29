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
            public int? Rolls { get; }
            public ulong? Seed { get; }

            public StashedShiny(T pk, ulong locHash)
            {
                PKM = pk;
                LocationHash = locHash;
                if (PKM is PA9 zapk)
                (Rolls, Seed) = ShinyRollChecker<T>.CheckValidDirtyZARNG(PKM);
            }

            public string GetRollsInfo()
            {
                if (Rolls.HasValue)
                    return (Rolls.Value < 4 ? $"Rolls: {Rolls.Value} (Legit or unknown)" : $"Rolls: {Rolls.Value} (Mad haxx)") + $" Seed: {Seed:X16}";
                else
                    return "Rolls: N/A";
            }

            public override string ToString() => $"Location hash: {LocationHash:X16}\r\n{GetRollsInfo()}\r\n" + ShowdownParsing.GetShowdownText(PKM) + "\r\n";
            
        }

        private const int STASHED_SHINIES_MAX = 10;
        private const int PA9_SIZE = 0x158;
        private const int PA9_BUFFER = 0x1F0;
        private const string STASH_FOLDER = "StashedShinies";
        
        private readonly long[] jumpsPos = new long[] { 0x5F0B250, 0x120, 0x168 }; // [[[main+5F0B250]+120]+168]

        public IList<StashedShiny<T>> PreviousStashedShinies { get; private set; } = [];
        public IList<StashedShiny<T>> StashedShinies { get; private set; } = [];
        public IList<StashedShiny<T>> DifferentShinies { get; private set; } = [];

        public ShinyFilter<T> Filter { get; private set; } = new ShinyFilter<T>();

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
            if (stash.Count == 0)
                return "No stashed shinies found.";

            var info = new StringBuilder("The following shinies are stashed on your save currently:\r\n\r\n");
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
