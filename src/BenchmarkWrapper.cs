using AdventOfCode2023.Utility;
using BenchmarkDotNet.Attributes;
using System.Text;

namespace AdventOfCode2023
{
    [MemoryDiagnoser]
    public class BenchmarkWrapper<T> where T : DayBase, new()
    {
        private readonly T _solution;
        private readonly ReadOnlyMemory<char> _input;

        public BenchmarkWrapper()
        {
            _solution = new T();
            _input = LoadInput(_solution.Day);
        }

        [Benchmark]
        public async Task RunPartOneBenchmark()
        {
            await _solution.RunPartOneBenchmark(_input);
        }

        [Benchmark]
        public async Task RunPartTwoBenchmark()
        {
            await _solution.RunPartTwoBenchmark(_input);
        }

        static ReadOnlyMemory<char> LoadInput(string day)
        {
            var input = InputLoader.LoadInputsAsFileStream($"InputFiles/Day{day}.txt", false);

            int bufferSize = CalculateBufferSize(new FileInfo($"InputFiles/Day{day}.txt").Length);
            char[] buffer = new char[bufferSize];

            using StreamReader reader = new(input, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, bufferSize: bufferSize, leaveOpen: true);

            int bytesRead = reader.Read(buffer, 0, buffer.Length);

            return buffer.AsMemory(0, bytesRead);
        }
        static int CalculateBufferSize(long fileSize)
        {
            const int minBufferSize = 1024;

            int bufferSize = (int)Math.Min(fileSize, int.MaxValue);

            return Math.Max(bufferSize, minBufferSize);
        }
    }
}
