namespace AdventOfCode2023.Extensions
{
    public static class NumericValueExtensions
    {
        public static ulong Concat(this uint a, uint b)
        {
            if (b < 10U) return 10UL * a + b;
            if (b < 100U) return 100UL * a + b;
            if (b < 1000U) return 1000UL * a + b;
            if (b < 10000U) return 10000UL * a + b;
            if (b < 100000U) return 100000UL * a + b;
            if (b < 1000000U) return 1000000UL * a + b;
            if (b < 10000000U) return 10000000UL * a + b;
            if (b < 100000000U) return 100000000UL * a + b;
            return 1000000000UL * a + b;
        }
    }
}
