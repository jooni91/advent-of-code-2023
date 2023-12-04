using AdventOfCode2023.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2023.Solutions.Day04
{
    public class Day4 : DayBase
    {
        protected override string Day { get; } = "04";
        protected override bool PartOneInputAsStream { get; } = true;
        protected override bool PartTwoInputAsStream { get; } = true;

        protected override async Task<string> PartOneAsync(FileStream inputStream)
        {
            var points = 0;

            await foreach (var line in inputStream.ReadLineAsync())
            {
                var count = CountWinningNumbers(line);

                points += count > 0 ? 1 << (count - 1) : 0;
            }

            return points.ToString();
        }
        protected override async Task<string> PartTwoAsync(FileStream inputStream)
        {
            var input = await inputStream.ReadLineAsync().ToArrayAsync();

            return CalculateScratchCardCount(ref input).ToString();
        }

        private static int CalculateScratchCardCount(ref string[] input)
        {
            Span<int> scratchcardCount = stackalloc int[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                var winningNumberCount = CountWinningNumbers(input[i]);

                scratchcardCount[i]++;

                for (int j = 1; j <= winningNumberCount; j++)
                {
                    var index = i + j;

                    if (index > scratchcardCount.Length)
                    {
                        break;
                    }

                    scratchcardCount[index] += 1 * scratchcardCount[i];
                }
            }

            var count = 0;

            for (int i = 0; i < scratchcardCount.Length; i++)
            {
                count += scratchcardCount[i];
            }

            return count;
        }
        private static int CountWinningNumbers(string line)
        {
            var gameDataSpan = line.AsSpan()[9..];
            var pipeIndex = gameDataSpan.IndexOf('|');
            Span<int> winningNumbers = stackalloc int[10];
            Span<int> drawnNumbers = stackalloc int[25];

            ExtractNumbers(gameDataSpan[..pipeIndex], ref winningNumbers);
            ExtractNumbers(gameDataSpan[(pipeIndex + 1)..], ref drawnNumbers);

            var count = 0;

            for (int i = 0; i < drawnNumbers.Length; i++)
            {
                if (winningNumbers.Contains(drawnNumbers[i]))
                {
                    count++;
                }
            }

            return count;
        }
        private static void ExtractNumbers(ReadOnlySpan<char> span, ref Span<int> results)
        {
            var resultIndex = 0;

            Span<int?> buffer = stackalloc int?[2];
            var bufferIndex = -1;

            for (int i = 0; i < span.Length; i++)
            {
                if (char.IsDigit(span[i]))
                {
                    bufferIndex++;
                    buffer[bufferIndex] = (int)char.GetNumericValue(span[i]);
                    continue;
                }

                if (Flush(ref buffer, ref bufferIndex, out var res))
                {
                    results[resultIndex] = (int)res;
                    resultIndex++;
                }
            }

            if (Flush(ref buffer, ref bufferIndex, out var result))
            {
                results[resultIndex] = (int)result;
                resultIndex++;
            }
        }
        private static bool Flush(ref Span<int?> buffer, ref int index, [NotNullWhen(true)] out int? result)
        {
            result = null;

            if (index > -1)
            {
                result = buffer[1] != null ? (int)((uint)buffer[0]!).Concat((uint)buffer[1]!) : buffer[0]!.Value;

                buffer[0] = buffer[1] = null;
                index = -1;
            }

            return result != null;
        }
    }
}
