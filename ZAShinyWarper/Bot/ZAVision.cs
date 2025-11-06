using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZAShinyWarper
{
    internal static class ZAVision
    {
        // [[[main+5F0C250]+120]+168]
        public static IReadOnlyList<long> ShinyStashPointer { get; } = [0x5F0C250, 0x120, 0x168]; // +00
        // [[[main+41ED340]+248]+00]+138]
        public static IReadOnlyList<long> PlayerPositionPointer { get; } = [0x41ED340, 0x248, 0x00, 0x138]; // +90


        //[[main+4200C40]+D8]
        public static IReadOnlyList<long> IngameTimePointer { get; } = [0x4200C40, 0xD8]; // +30
        // [[main+4200C20]+1B0]
        public static IReadOnlyList<long> WeatherPointer { get; } = [0x4200C20, 0x1B0]; // +00

        // For clearing stashed shiny at index
        public static IReadOnlyList<long> GetStashedShinyPointer(int index) // +00
        {
            if (index < 0 || index >= 10)
                throw new ArgumentOutOfRangeException(nameof(index), "Index must be between 0 and 9.");
            return [0x4201D20, 0x350, (0x8 + 0x28 * index), 0x50, 0x30, 0x00];
        }
    }
}
