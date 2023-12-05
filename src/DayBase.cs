using AdventOfCode2023.Utility;
using BenchmarkDotNet.Attributes;
using System.Text;

namespace AdventOfCode2023
{
    [MemoryDiagnoser]
    public abstract class DayBase
    {
        public abstract string Day { get; }

        public bool UnitTestMode { get; set; } = false;

        public Task<string> GetResultAsync(Part part)
        {
            return part == Part.One
                ? PartOneAsync(GetInputStream())
                : PartTwoAsync(GetInputStream());
        }

        public async Task RunPartOneBenchmark(ReadOnlyMemory<char> input)
        {
            await PartOneAsync(input);
        }
        public async Task RunPartTwoBenchmark(ReadOnlyMemory<char> input)
        {
            await PartTwoAsync(input);
        }

        protected virtual async Task<string> PartOneAsync(FileStream input)
        {
            int bufferSize = CalculateBufferSize(new FileInfo($"InputFiles/Day{Day}.txt").Length);
            char[] buffer = new char[bufferSize];

            using StreamReader reader = new(input, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: bufferSize, leaveOpen: true);

            int bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length);

            return await PartOneAsync(buffer.AsMemory(0, bytesRead));
        }
        protected virtual Task<string> PartOneAsync(ReadOnlyMemory<char> input)
        {
            return PartOneAsync(input.ToString());
        }
        protected virtual Task<string> PartOneAsync(string input)
        {
            throw new NotImplementedException();
        }
        protected virtual async Task<string> PartTwoAsync(FileStream input)
        {
            int bufferSize = CalculateBufferSize(new FileInfo($"InputFiles/Day{Day}.txt").Length);
            char[] buffer = new char[bufferSize];

            using StreamReader reader = new(input, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: bufferSize, leaveOpen: true);

            int bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length);

            return await PartTwoAsync(buffer.AsMemory(0, bytesRead));
        }
        protected virtual Task<string> PartTwoAsync(ReadOnlyMemory<char> input)
        {
            return PartTwoAsync(input.ToString());
        }
        protected virtual Task<string> PartTwoAsync(string input)
        {
            throw new NotImplementedException();
        }

        protected FileStream GetInputStream()
        {
            return InputLoader.LoadInputsAsFileStream($"InputFiles/Day{Day}.txt", false);
        }

        static int CalculateBufferSize(long fileSize)
        {
            const int minBufferSize = 1024;

            int bufferSize = (int)Math.Min(fileSize, int.MaxValue);

            return Math.Max(bufferSize, minBufferSize);
        }
    }

    public enum Part
    {
        One = 1,
        Two = 2
    }
}
