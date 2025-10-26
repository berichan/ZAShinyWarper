using PKHeX.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAWarper
{
    public static class ShinyRollChecker<T> where T : PKM, new()
    {
        public static int CheckValidDirtyZARNG(T pkm) // Dirty placeholder until we have a proper encounter/param input, partially leveraged from PKHeX
        {
            var p = new GenerateParam9();
            var fiv = pkm.FlawlessIVCount;

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
                const int UNSET = -1;
                const int MAX = 31;
                Span<int> ivs = [UNSET, UNSET, UNSET, UNSET, UNSET, UNSET];
                if (p.IVs.IsSpecified)
                {
                    p.IVs.CopyToSpeedLast(ivs);
                }
                else
                {
                    for (int j = 0; j < fiv; j++)
                    {
                        int index;
                        do { index = (int)xoro.NextInt(6); }
                        while (ivs[index] != UNSET);
                        ivs[index] = MAX;
                    }
                }
                for (int j = 0; j < 6; j++)
                {
                    if (ivs[j] == UNSET)
                        ivs[j] = (int)xoro.NextInt(32);
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
                return tries;
            }
            return -1;
        }
    }
}
