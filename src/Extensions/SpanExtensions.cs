using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2023.Extensions
{
    public static class SpanExtensions
    {
        public static int ExtractNumbers(this ReadOnlySpan<char> source, ref Span<int> buffer, int start, int length)
        {
            var resultIndex = 0;

            Span<int> currentNumberBuffer = stackalloc int[10];
            var currentBufferIndex = -1;

            for (int i = start; i < length; i++)
            {
                if (char.IsDigit(source[i]))
                {
                    currentBufferIndex++;
                    currentNumberBuffer[currentBufferIndex] = (int)char.GetNumericValue(source[i]);
                    continue;
                }

                if (Flush(ref currentNumberBuffer, ref currentBufferIndex, out var res))
                {
                    buffer[resultIndex] = res;
                    resultIndex++;
                }
            }

            if (Flush(ref currentNumberBuffer, ref currentBufferIndex, out var result))
            {
                buffer[resultIndex] = result;
                resultIndex++;
            }

            return resultIndex;

            static bool Flush(ref Span<int> buffer, ref int index, [NotNullWhen(true)] out int result)
            {
                result = 0;

                if (index > -1)
                {
                    for (int i = 0; i <= index; i++)
                    {
                        result = (int)result.Concat(buffer[i]);
                    }
                }

                buffer.Clear();
                index = -1;

                return result > 0;
            }
        }
    }
}
