using AdventOfCode2023.Extensions;
using AdventOfCode2023.Utility;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2023.Solutions.Day04
{
    public class Day4 : DayBase
    {
        public override string Day { get; } = "04";

        protected override Task<string> PartOneAsync(ReadOnlyMemory<char> input)
        {
            var points = 0;

            foreach (var line in new MemoryLineEnumerator(input))
            {
                var count = CountWinningNumbers(line);

                points += count > 0 ? 1 << (count - 1) : 0;
            }

            return Task.FromResult(points.ToString());
        }
        protected override Task<string> PartTwoAsync(ReadOnlyMemory<char> input)
        {
            return Task.FromResult(CalculateScratchCardCount(input).ToString());
        }

        private static int CalculateScratchCardCount(ReadOnlyMemory<char> input)
        {
            Span<int> scratchcardCount = stackalloc int[214];
            var scratchcardIndex = 0;

            foreach (var line in new MemoryLineEnumerator(input))
            {
                var winningNumberCount = CountWinningNumbers(line);

                scratchcardCount[scratchcardIndex]++;

                for (int j = 1; j <= winningNumberCount; j++)
                {
                    var index = scratchcardIndex + j;

                    if (index > scratchcardCount.Length)
                    {
                        break;
                    }

                    scratchcardCount[index] += 1 * scratchcardCount[scratchcardIndex];
                }

                scratchcardIndex++;
            }

            var count = 0;

            for (int i = 0; i < scratchcardCount.Length; i++)
            {
                count += scratchcardCount[i];
            }

            return count;
        }
        private static int CountWinningNumbers(ReadOnlyMemory<char> line)
        {
            var gameDataSpan = line[9..];
            var pipeIndex = gameDataSpan.Span.IndexOf('|');
            Span<int> winningNumbers = stackalloc int[10];
            Span<int> drawnNumbers = stackalloc int[25];

            ExtractNumbers(gameDataSpan.Span[..pipeIndex], ref winningNumbers);
            ExtractNumbers(gameDataSpan.Span[(pipeIndex + 1)..], ref drawnNumbers);

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
                    results[resultIndex] = res;
                    resultIndex++;
                }
            }

            if (Flush(ref buffer, ref bufferIndex, out var result))
            {
                results[resultIndex] = result;
                resultIndex++;
            }
        }
        private static bool Flush(ref Span<int?> buffer, ref int index, [NotNullWhen(true)] out int result)
        {
            result = 0;

            if (index > -1)
            {
                result = buffer[1] != null ? (int)((uint)buffer[0]!).Concat((uint)buffer[1]!) : buffer[0]!.Value;

                buffer[0] = buffer[1] = null;
                index = -1;
            }

            return result > 0;
        }
    }
}
