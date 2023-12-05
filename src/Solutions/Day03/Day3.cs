using AdventOfCode2023.Extensions;

namespace AdventOfCode2023.Solutions.Day03
{
    public class Day3 : DayBase
    {
        public override string Day { get; } = "03";

        protected override async Task<string> PartOneAsync(FileStream inputStream)
        {
            var data = await inputStream.ReadLineAsync().ToListAsync();
            var sum = FindSchemaPartNumbers(data).Sum();

            return sum.ToString();
        }
        protected override async Task<string> PartTwoAsync(FileStream inputStream)
        {
            var data = await inputStream.ReadLineAsync().ToListAsync();
            var sum = FindSchemaGearRatios(data).Sum();

            return sum.ToString();
        }

        private IEnumerable<int> FindSchemaPartNumbers(List<string> data)
        {
            for (int row = 0; row < data.Count; row++)
            {
                for (int column = 0; column < data[row].Length; column++)
                {
                    if (data[row][column] == '.' || char.IsDigit(data[row][column]))
                    {
                        continue;
                    }

                    foreach (var num in SearchSurroundingNumbers(data, (column, row)))
                    {
                        yield return num;
                    }
                }
            }
        }
        private IEnumerable<int> FindSchemaGearRatios(List<string> data)
        {
            for (int row = 0; row < data.Count; row++)
            {
                for (int column = 0; column < data[row].Length; column++)
                {
                    if (data[row][column] != '*')
                    {
                        continue;
                    }

                    var partNumbers = SearchSurroundingNumbers(data, (column, row)).ToArray();

                    if (partNumbers.Length != 2)
                    {
                        continue;
                    }

                    yield return partNumbers[0] * partNumbers[1];
                }
            }
        }
        private IEnumerable<int> SearchSurroundingNumbers(List<string> data, (int X, int Y) centerIndex)
        {
            for (int i = -1; i < 2; i++)
            {
                if (centerIndex.Y - i < 0 || centerIndex.Y + 1 >= data.Count)
                {
                    continue;
                }

                var startIndex = (centerIndex.X - 3) < 0 ? 0 : centerIndex.X - 3;
                var endIndex = (centerIndex.X + 4) > data.Count ? data.Count : centerIndex.X + 4;

                foreach (var num in ParseNumbers(data[centerIndex.Y + i].AsSpan()[startIndex..endIndex]))
                {
                    yield return num;
                }
            }
        }
        private List<int> ParseNumbers(ReadOnlySpan<char> span)
        {
            var foundNumbers = new List<int>(2);
            var numbers = new int?[3];
            var numberIndex = -1;

            for (int i = 0; i < span.Length; i++)
            {
                if (char.IsDigit(span[i]) && IsConnected(span, i))
                {
                    numberIndex++;
                    numbers[numberIndex] = (int)char.GetNumericValue(span[i]);
                }
                else if (numberIndex > -1)
                {
                    foundNumbers.Add(ConvertToConcatenatedNumber(ref numbers, ref numberIndex));
                }
            }

            if (numberIndex > -1)
            {
                foundNumbers.Add(ConvertToConcatenatedNumber(ref numbers, ref numberIndex));
            }

            return foundNumbers;
        }
        private bool IsConnected(ReadOnlySpan<char> span, int index)
        {
            if (index > 1 && index < 5)
            {
                return char.IsDigit(span[index]);
            }
            else if (index < 2)
            {
                while (index < 2)
                {
                    if (!char.IsDigit(span[index + 1]))
                    {
                        return false;
                    }

                    index++;
                }

                return true;
            }
            else
            {
                while (index > 4)
                {
                    if (!char.IsDigit(span[index - 1]))
                    {
                        return false;
                    }

                    index--;
                }

                return true;
            }
        }
        private int ConvertToConcatenatedNumber(ref int?[] numbers, ref int numberIndex)
        {
            if (numbers[0] == null)
            {
                throw new ArgumentException();
            }

            var number = numbers[0]!.Value;

            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] == null)
                {
                    break;
                }

                number = (int)((uint)number).Concat((uint)numbers[i]!);
            }

            numberIndex = -1;
            numbers[0] = numbers[1] = numbers[2] = null;

            return number;
        }
    }
}
