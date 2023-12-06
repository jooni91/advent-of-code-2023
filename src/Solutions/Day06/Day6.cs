using AdventOfCode2023.Extensions;
using AdventOfCode2023.Utility;

namespace AdventOfCode2023.Solutions.Day06
{
    public class Day6 : DayBase
    {
        public override string Day { get; } = "06";

        protected override Task<string> PartOneAsync(ReadOnlyMemory<char> input)
        {
            Span<int> times = stackalloc int[4];
            Span<int> distances = stackalloc int[4];

            ExtractData(input, ref times, ref distances);

            return Task.FromResult(CalculateNumberOfWaysToWin(ref times, ref distances).ToString());
        }
        protected override Task<string> PartTwoAsync(ReadOnlyMemory<char> input)
        {
            Span<int> times = stackalloc int[4];
            Span<int> distances = stackalloc int[4];

            ExtractData(input, ref times, ref distances);

            var time = Concat(ref times);
            var distance = Concat(ref distances);

            return Task.FromResult(CalculateNumberOfWaysToWin(time, distance).ToString());
        }

        private long CalculateNumberOfWaysToWin(ref Span<int> times, ref Span<int> distances)
        {
            long sum = 0;

            for (int i = 0; i < times.Length; i++)
            {
                int wins = 0;

                for (var j = 0; j < times[i]; j++)
                {
                    if (j * (times[i] - j) > distances[i])
                    {
                        wins++;
                    }
                }

                sum = sum == 0 ? wins : sum * wins;
            }

            return sum;
        }
        private long CalculateNumberOfWaysToWin(long time, long distance)
        {
            long wins = 0;

            for (var j = 0; j < time; j++)
            {
                if (j * (time - j) > distance)
                {
                    wins++;
                }
            }

            return wins;
        }
        private void ExtractData(ReadOnlyMemory<char> source, ref Span<int> times, ref Span<int> distances)
        {
            bool flip = false;

            foreach (var line in new MemoryLineEnumerator(source))
            {
                var colonIndex = line.Span.IndexOf(':');

                if (!flip)
                {
                    line.Span.ExtractNumbers(ref times, colonIndex + 1, line.Length);
                    flip = true;
                }
                else
                {
                    line.Span.ExtractNumbers(ref distances, colonIndex + 1, line.Length);
                }
            }
        }
        private long Concat(ref Span<int> source)
        {
            long number = 0;

            for (int i = 0; i < source.Length; i++)
            {
                number = number.Concat(source[i]);
            }

            return number;
        }
    }
}
