using AdventOfCode2023.Utility;

namespace AdventOfCode2023.Solutions.Day02
{
    public class Day2 : DayBase
    {
        private readonly Dictionary<string, int> limits = new()
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        public override string Day { get; } = "02";

        protected override Task<string> PartOneAsync(ReadOnlyMemory<char> input)
        {
            int sum = 0;

            foreach (var line in new MemoryLineEnumerator(input))
            {
                var validity = CheckValidity(line.Span);

                if (validity.Valid)
                {
                    sum += validity.GameNumber;
                }
            }

            return Task.FromResult(sum.ToString());
        }
        protected override Task<string> PartTwoAsync(ReadOnlyMemory<char> input)
        {
            int sum = 0;

            foreach (var line in new MemoryLineEnumerator(input))
            {
                sum += GetPowerOfGame(line.Span);
            }

            return Task.FromResult(sum.ToString());
        }

        private (bool Valid, int GameNumber) CheckValidity(ReadOnlySpan<char> lineSpan)
        {
            var colonIndex = lineSpan.IndexOf(':');
            var gameDataSpan = lineSpan[(colonIndex + 1)..];

            Span<int> semicolonIndieces = stackalloc int[64];
            var semicolonIndiecesCount = KMP.SearchAllIndieces(gameDataSpan, ';', ref semicolonIndieces);
            semicolonIndieces[semicolonIndiecesCount] = gameDataSpan.Length;
            semicolonIndiecesCount++;

            Span<int> commaIndieces = stackalloc int[3];
            int foundCommas = 0;

            var drawStartIndex = 1;

            for (int index = 0; index < semicolonIndiecesCount; index++)
            {
                var drawSpan = gameDataSpan[drawStartIndex..semicolonIndieces[index]];
                foundCommas = KMP.SearchAllIndieces(drawSpan, ',', ref commaIndieces);
                commaIndieces[foundCommas] = drawSpan.Length;

                for (int i = 0; i <= foundCommas; i++)
                {
                    var cubeSpan = drawSpan[(i > 0 ? commaIndieces[i - 1] + 2 : 0)..commaIndieces[i]];
                    var wsIndex = cubeSpan.IndexOf(' ');
                    var drawnCount = int.Parse(cubeSpan[0..wsIndex]);

                    foreach (var limit in limits)
                    {
                        if (MemoryExtensions.Equals(cubeSpan[(wsIndex + 1)..cubeSpan.Length], limit.Key, StringComparison.Ordinal))
                        {
                            if (drawnCount > limit.Value)
                            {
                                return (false, 0);
                            }

                            break;
                        }
                    }
                }

                drawStartIndex = semicolonIndieces[index] + 2;
            }

            return (true, int.Parse(lineSpan[4..colonIndex]));
        }
        private static int GetPowerOfGame(ReadOnlySpan<char> lineSpan)
        {
            var colonIndex = lineSpan.IndexOf(':');
            var gameDataSpan = lineSpan[(colonIndex + 1)..];

            Span<int> semicolonIndieces = stackalloc int[64];
            var semicolonIndiecesCount = KMP.SearchAllIndieces(gameDataSpan, ';', ref semicolonIndieces);
            semicolonIndieces[semicolonIndiecesCount] = gameDataSpan.Length;
            semicolonIndiecesCount++;

            Span<int> commaIndieces = stackalloc int[3];
            int foundCommas = 0;

            int largestGreenCount = 0;
            int largestRedCount = 0;
            int largestBlueCount = 0;

            var drawStartIndex = 1;

            for (int index = 0; index < semicolonIndiecesCount; index++)
            {
                var drawSpan = gameDataSpan[drawStartIndex..semicolonIndieces[index]];
                foundCommas = KMP.SearchAllIndieces(drawSpan, ',', ref commaIndieces);
                commaIndieces[foundCommas] = drawSpan.Length;

                for (int i = 0; i <= foundCommas; i++)
                {
                    var cubeSpan = drawSpan[(i > 0 ? commaIndieces[i - 1] + 2 : 0)..commaIndieces[i]];
                    var wsIndex = cubeSpan.IndexOf(' ');
                    var drawnCount = int.Parse(cubeSpan[0..wsIndex]);
                    var colorSpan = cubeSpan[(wsIndex + 1)..cubeSpan.Length];

                    if (MemoryExtensions.Equals(colorSpan, "red", StringComparison.Ordinal))
                    {
                        if (largestRedCount < drawnCount)
                        {
                            largestRedCount = drawnCount;
                        }
                    }
                    else if (MemoryExtensions.Equals(colorSpan, "green", StringComparison.Ordinal))
                    {
                        if (largestGreenCount < drawnCount)
                        {
                            largestGreenCount = drawnCount;
                        }
                    }
                    else
                    {
                        if (largestBlueCount < drawnCount)
                        {
                            largestBlueCount = drawnCount;
                        }
                    }
                }

                drawStartIndex = semicolonIndieces[index] + 2;
            }

            return largestGreenCount * largestRedCount * largestBlueCount;
        }
    }
}
