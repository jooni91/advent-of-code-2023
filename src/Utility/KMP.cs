namespace AdventOfCode2023.Utility
{
    public static class KMP
    {
        public static List<int> SearchAllIndexes(string source, string match)
        {
            List<int> indices = [];
            int sourceIndex = 0;
            int matchIndex = 0;
            Span<int> failure = stackalloc int[match.Length];

            ComputeFailure(match, ref failure);

            while (sourceIndex < source.Length)
            {
                if (match[matchIndex] == source[sourceIndex])
                {
                    matchIndex++;
                    if (matchIndex == match.Length)
                    {
                        indices.Add(sourceIndex - match.Length + 1);
                        matchIndex = failure[matchIndex - 1];
                    }
                    sourceIndex++;
                }
                else if (matchIndex > 0)
                {
                    matchIndex = failure[matchIndex - 1];
                }
                else
                {
                    sourceIndex++;
                }
            }

            return indices;
        }
        public static List<int> SearchAllIndieces(ReadOnlySpan<char> source, ReadOnlySpan<char> match)
        {
            List<int> indices = [];
            int sourceIndex = 0;
            int matchIndex = 0;
            Span<int> failure = stackalloc int[match.Length];

            ComputeFailure(match, ref failure);

            while (sourceIndex < source.Length)
            {
                if (matchIndex < match.Length && match[matchIndex] == source[sourceIndex])
                {
                    matchIndex++;
                    if (matchIndex == match.Length)
                    {
                        indices.Add(sourceIndex - match.Length + 1);
                        matchIndex = failure[matchIndex - 1];
                    }
                    sourceIndex++;
                }
                else if (matchIndex > 0)
                {
                    matchIndex = failure[matchIndex - 1];
                }
                else
                {
                    sourceIndex++;
                }
            }

            return indices;
        }
        public static List<int> SearchAllIndieces(ReadOnlySpan<char> source, char delimiter)
        {
            return SearchAllIndieces(source, delimiter.ToString().AsSpan());
        }

        public static int SearchAllIndieces(ReadOnlySpan<char> source, ReadOnlySpan<char> match, ref Span<int> buffer)
        {
            int sourceIndex = 0;
            int matchIndex = 0;
            int found = 0;
            Span<int> failure = stackalloc int[match.Length];

            ComputeFailure(match, ref failure);

            while (sourceIndex < source.Length)
            {
                if (matchIndex < match.Length && match[matchIndex] == source[sourceIndex])
                {
                    matchIndex++;
                    if (matchIndex == match.Length)
                    {
                        buffer[found] = sourceIndex - match.Length + 1;
                        matchIndex = failure[matchIndex - 1];
                        found++;
                    }
                    sourceIndex++;
                }
                else if (matchIndex > 0)
                {
                    matchIndex = failure[matchIndex - 1];
                }
                else
                {
                    sourceIndex++;
                }
            }

            return found;
        }
        public static int SearchAllIndieces(ReadOnlySpan<char> source, char delimiter, ref Span<int> buffer)
        {
            int index = 0;
            int found = 0;

            while (index < source.Length)
            {
                if (delimiter == source[index])
                {
                        buffer[found] = index;
                        found++;
                }

                index++;
            }

            return found;
        }

        private static void ComputeFailure(ReadOnlySpan<char> pattern, ref Span<int> failure)
        {
            int j = 0;

            for (int i = 1; i < pattern.Length; i++)
            {
                while (j > 0 && pattern[i] != pattern[j])
                {
                    j = failure[j - 1];
                }

                if (pattern[i] == pattern[j])
                {
                    j++;
                }

                failure[i] = j;
            }
        }
        private static void ComputeFailure(string pattern, ref Span<int> failure)
        {
            int j = 0;

            for (int i = 1; i < pattern.Length; i++)
            {
                while (j > 0 && pattern[i] != pattern[j])
                {
                    j = failure[j - 1];
                }

                if (pattern[i] == pattern[j])
                {
                    j++;
                }

                failure[i] = j;
            }
        }
    }
}
