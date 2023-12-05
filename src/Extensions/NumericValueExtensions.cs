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

        public static long Concat(this int a, int b)
        {
            if (a < 0 || b < 0)
            {
                throw new ArgumentException();
            }

            if (b < 10U) return 10L * a + b;
            if (b < 100U) return 100L * a + b;
            if (b < 1000U) return 1000L * a + b;
            if (b < 10000U) return 10000L * a + b;
            if (b < 100000U) return 100000L * a + b;
            if (b < 1000000U) return 1000000L * a + b;
            if (b < 10000000U) return 10000000L * a + b;
            if (b < 100000000U) return 100000000L * a + b;
            return 1000000000L * a + b;
        }
    }
}
