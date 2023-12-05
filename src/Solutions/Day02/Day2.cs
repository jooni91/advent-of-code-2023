using AdventOfCode2023.Extensions;
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

        protected override string Day { get; } = "02";

        protected override async Task<string> PartOneAsync(FileStream inputStream)
        {
            var sum = await GetValidGameNumbersAsync(inputStream.ReadLineAsync()).SumAsync();

            return sum.ToString();
        }
        protected override async Task<string> PartTwoAsync(FileStream inputStream)
        {
            var sum = await PowerOfGamesAsync(inputStream.ReadLineAsync()).SumAsync();

            return sum.ToString();
        }

        public async IAsyncEnumerable<int> GetValidGameNumbersAsync(IAsyncEnumerable<string> lines)
        {
            await foreach (var line in lines)
            {
                var validity = CheckValidity(line);

                if (validity.Valid)
                {
                    yield return validity.GameNumber;
                }
            }
        }
        public async IAsyncEnumerable<int> PowerOfGamesAsync(IAsyncEnumerable<string> lines)
        {
            await foreach (var line in lines)
            {
                yield return GetPowerOfGame(line);
            }
        }

        public (bool Valid, int GameNumber) CheckValidity(string line)
        {
            var lineSpan = line.AsSpan();
            var colonIndex = lineSpan.IndexOf(':');
            var gameDataSpan = lineSpan[(colonIndex + 1)..];
            var semicolonIndieces = KMP.SearchAllIndieces(gameDataSpan, ";");
            semicolonIndieces.Add(gameDataSpan.Length);

            var drawStartIndex = 1;

            foreach (var drawEndIndex in semicolonIndieces)
            {
                var drawSpan = gameDataSpan[drawStartIndex..drawEndIndex];
                var commaIndieces = KMP.SearchAllIndieces(drawSpan, ',');
                commaIndieces.Add(drawSpan.Length);

                for (int i = 0; i < commaIndieces.Count; i++)
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

                drawStartIndex = drawEndIndex + 2;
            }

            return (true, int.Parse(lineSpan[4..colonIndex]));
        }
        public int GetPowerOfGame(string line)
        {
            var lineSpan = line.AsSpan();
            var colonIndex = lineSpan.IndexOf(':');
            var gameDataSpan = lineSpan[(colonIndex + 1)..];
            var semicolonIndieces = KMP.SearchAllIndieces(gameDataSpan, ";");
            semicolonIndieces.Add(gameDataSpan.Length);

            int largestGreenCount = 0;
            int largestRedCount = 0;
            int largestBlueCount = 0;

            var drawStartIndex = 1;

            foreach (var drawEndIndex in semicolonIndieces)
            {
                var drawSpan = gameDataSpan[drawStartIndex..drawEndIndex];
                var commaIndieces = KMP.SearchAllIndieces(drawSpan, ',');
                commaIndieces.Add(drawSpan.Length);

                for (int i = 0; i < commaIndieces.Count; i++)
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

                drawStartIndex = drawEndIndex + 2;
            }

            return largestGreenCount * largestRedCount * largestBlueCount;
        }
    }
}
