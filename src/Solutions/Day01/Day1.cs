using AdventOfCode2023.Extensions;
using AdventOfCode2023.Utility;

namespace AdventOfCode2023.Solutions.Day01
{
    public class Day1 : DayBase
    {
        private readonly Dictionary<string, int> stringNumberMap = new()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };
        private readonly Dictionary<char, int> charNumberMap = new()
        {
            { '1', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 }
        };

        public override string Day => "01";

        protected override Task<string> PartOneAsync(ReadOnlyMemory<char> input)
        {
            return Task.FromResult(ExtractTwoDigitNumber(input).ToString());
        }
        protected override Task<string> PartTwoAsync(ReadOnlyMemory<char> input)
        {
            return Task.FromResult(ExtractTwoDigitNumberWithWrittenDigits(input).ToString());
        }

        private int ExtractTwoDigitNumber(ReadOnlyMemory<char> input)
        {
            int sum = 0;

            Span<char> digits = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];

            foreach (var line in new MemoryLineEnumerator(input))
            {
                var firstIndex = line.Span.IndexOfAny(digits);
                var lastIndex = line.Span.LastIndexOfAny(digits);

                sum += (int)charNumberMap[line.Span[firstIndex]].Concat(charNumberMap[line.Span[lastIndex]]);
            }

            return sum;
        }
        private int ExtractTwoDigitNumberWithWrittenDigits(ReadOnlyMemory<char> input)
        {
            int sum = 0;

            Span<char> digits = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];
            Span<string> writtenDigits = [ "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" ];

            Span<int> letterIndieces = stackalloc int[4];
            int letterCount = 0;

            foreach (var line in new MemoryLineEnumerator(input))
            {
                int min = input.Length;
                int minVal = 0;
                int max = -1;
                int maxVal = 0;

                var firstIndex = line.Span.IndexOfAny(digits);
                var lastIndex = line.Span.LastIndexOfAny(digits);

                if (firstIndex > -1 && min > firstIndex)
                {
                    min = firstIndex;
                    minVal = charNumberMap[line.Span[firstIndex]];
                }

                if (lastIndex > -1 && max < lastIndex)
                {
                    max = lastIndex;
                    maxVal = charNumberMap[line.Span[lastIndex]];
                }

                for (int i = 0; i < writtenDigits.Length; i++)
                {
                    letterCount = KMP.SearchAllIndieces(line.Span, writtenDigits[i].AsSpan(), ref letterIndieces);

                    for (int j = 0; j < letterCount; j++)
                    {
                        if (min > letterIndieces[j])
                        {
                            min = letterIndieces[j];
                            minVal = stringNumberMap[writtenDigits[i]];
                        }
                        if (max < letterIndieces[j])
                        {
                            max = letterIndieces[j];
                            maxVal = stringNumberMap[writtenDigits[i]];
                        }
                    }
                }

                sum += (int)minVal.Concat(maxVal);
            }

            return sum;
        }
    }
}
