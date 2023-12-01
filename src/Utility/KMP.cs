namespace AdventOfCode2023.Utility
{
    public static class KMP
    {
        public static List<int> SearchAllIndexes(string source, string match)
        {
            List<int> indices = new List<int>();
            int sourceIndex = 0;
            int matchIndex = 0;
            int[] failure = ComputeFailure(match);

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

        private static int[] ComputeFailure(string pattern)
        {
            int[] failure = new int[pattern.Length];
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

            return failure;
        }
    }
}
