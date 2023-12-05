using AdventOfCode2023.Extensions;
using AdventOfCode2023.Utility;
using System.Text;

namespace AdventOfCode2023.Solutions.Day01
{
    public class Day1 : DayBase
    {
        private readonly Dictionary<string, string> replacementDictionary = new()
        {
            { "one", "1" },
            { "two", "2" },
            { "three", "3" },
            { "four", "4" },
            { "five", "5" },
            { "six", "6" },
            { "seven", "7" },
            { "eight", "8" },
            { "nine", "9" }
        };

        protected override string Day => "01";

        protected override async Task<string> PartOneAsync(FileStream inputStream)
        {
            var result = await ExtractTwoDigitNumberAsync(inputStream.ReadLineAsync()).SumAsync();

            return result.ToString();
        }
        protected override async Task<string> PartTwoAsync(FileStream inputStream)
        {
            var result = await ExtractTwoDigitNumberAsync(ReplaceWrittenNumbers(inputStream.ReadLineAsync())).SumAsync();

            return result.ToString();
        }

        private async IAsyncEnumerable<int> ExtractTwoDigitNumberAsync(IAsyncEnumerable<string> lines)
        {
            var digits = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            await foreach (var line in lines)
            {
                var firstIndex = line.AsSpan().IndexOfAny(digits);
                var lastIndex = line.AsSpan().LastIndexOfAny(digits);

                yield return int.Parse(string.Create(2, line, (span, input) => 
                {
                    span[0] = line[firstIndex];
                    span[1] = line[lastIndex];
                }));
            }
        }
        private async IAsyncEnumerable<string> ReplaceWrittenNumbers(IAsyncEnumerable<string> lines)
        {
            var writtenDigits = new string[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var replacements = new List<(int Index, string Replacement)>();

            await foreach (var line in lines)
            {
                var result = new StringBuilder(line);
                replacements.Clear();

                foreach (var substring in writtenDigits)
                {
                    var indexes = KMP.SearchAllIndexes(line, substring);

                    foreach (var index in indexes)
                    {
                        replacements.Add((index, replacementDictionary[substring]));
                    }
                }

                // Apply replacements in reverse order to avoid index shifting
                foreach (var (index, replacement) in replacements.OrderByDescending(r => r.Index))
                {
                    result.Remove(index, replacement.Length);
                    result.Insert(index, replacement);
                }

                yield return result.ToString();
            }
        }
    }
}
