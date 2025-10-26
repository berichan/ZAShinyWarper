using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PLAWarper
{
    public static class ShinyRollChecker<T> where T : PKM, new()
    {
        public static (int?, ulong?) CheckValidDirtyZARNG(T pkm, int fivCheck = -1) // Dirty placeholder until we have a proper encounter/param input, partially leveraged from PKHeX
        {
            var p = new GenerateParam9();
            var fiv = fivCheck == -1 ? pkm.FlawlessIVCount : fivCheck;

            var ec = pkm.EncryptionConstant;
            var seeds = new XoroMachineConsecutive(ec, pkm.ID32);
            foreach (var s in seeds)
            {
                int tries = 0;
                // skip two then gen IVs
                var xoro = new Xoroshiro128Plus(s);
                var ul = xoro.NextInt();
                Debug.Assert(ul == ec);
                ul = xoro.NextInt();
                Debug.Assert(ul == pkm.ID32);
                ul = xoro.NextInt();
                while (ul != pkm.PID && tries++ < 90001) // check 90001 rolls for hax
                    ul = xoro.NextInt();
                if (ul != pkm.PID)
                    continue;
                const int UNSET = -1;
                const int MAX = 31;
                Span<int> ivs = [UNSET, UNSET, UNSET, UNSET, UNSET, UNSET];
                Span<int> ivspk = [pkm.IV_HP, pkm.IV_ATK, pkm.IV_DEF, pkm.IV_SPA, pkm.IV_SPD, pkm.IV_SPE];

                // We don't have encounter data for now, so we just skip possible flawless IVs
                if (fiv > 0)
                { 
                    for (int j = 0; j < 6; j++)
                    {
                        bool isFlawless = ivspk[j] == MAX;
                        if (ivs[j] == UNSET && !isFlawless)
                            ivs[j] = (int)xoro.NextInt(MAX + 1);
                        else
                            ivs[j] = MAX;
                    }
                }
                else
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (ivs[j] == UNSET)
                            ivs[j] = (int)xoro.NextInt(MAX + 1);
                    }
                }
                if (pkm.IV_HP != ivs[0])
                    continue;
                if (pkm.IV_ATK != ivs[1])
                    continue;
                if (pkm.IV_DEF != ivs[2])
                    continue;
                if (pkm.IV_SPA != ivs[3])
                    continue;
                if (pkm.IV_SPD != ivs[4])
                    continue;
                if (pkm.IV_SPE != ivs[5])
                    continue;
                Console.WriteLine($"Valid seed: {s} / EC: {ec} / {pkm.FileName} Rolls: {tries}");
                return (tries, s);
            }
            if (fiv > 0) // no encounter data, we don't know if we naturally rolled flawless IVs
                return CheckValidDirtyZARNG(pkm, fiv - 1);
            return (null, null);
        }
    }
}
