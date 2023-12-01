namespace AdventOfCode2023.Extensions
{
    public static class FileStreamExtensions
    {
        public static async IAsyncEnumerable<string> ReadLineAsync(this FileStream fileStream)
        {
            using var sr = new StreamReader(fileStream);

            string? line = null;

            while ((line = await sr.ReadLineAsync()) != null)
            {
                yield return line;
            }
        }
    }
}
