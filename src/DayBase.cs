using AdventOfCode2023.Utility;

namespace AdventOfCode2023
{
    public abstract class DayBase
    {
        protected abstract string Day { get; }
        protected virtual bool PartOneInputAsStream { get; } = false;
        protected virtual bool PartTwoInputAsStream { get; } = false;

        public bool UnitTestMode { get; set; } = false;

        public Task<string> GetResultAsync(Part part)
        {
            return part == Part.One
                ? PartOneInputAsStream ? PartOneAsync(GetInputStream()) : PartOneAsync(GetInput())
                : PartTwoInputAsStream ? PartTwoAsync(GetInputStream()) : PartTwoAsync(GetInput());
        }

#pragma warning disable IDE0022 // Use block body for methods
        protected virtual Task<string> PartOneAsync(string input) => throw new NotImplementedException();
        protected virtual Task<string> PartOneAsync(FileStream inputStream) => throw new NotImplementedException();
        protected virtual Task<string> PartTwoAsync(string input) => throw new NotImplementedException();
        protected virtual Task<string> PartTwoAsync(FileStream inputStream) => throw new NotImplementedException();
#pragma warning restore IDE0022 // Use block body for methods

        protected string GetInput()
        {
            return InputLoader.LoadInputsFromFileAsString($"InputFiles/Day{Day}.txt");
        }
        protected FileStream GetInputStream()
        {
            return InputLoader.LoadInputsAsFileStream($"InputFiles/Day{Day}.txt");
        }
    }

    public enum Part
    {
        One = 1,
        Two = 2
    }
}
