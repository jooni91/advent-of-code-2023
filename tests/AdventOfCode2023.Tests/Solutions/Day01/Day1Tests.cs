namespace AdventOfCode2023.Tests.Solutions.Day01
{
    public class Day1Tests
    {
        [Fact]
        public async Task PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new AdventOfCode2023.Solutions.Day01.Day1();

            // Act
            var result = await daySolution.GetResultAsync(Part.One);

            // Assert
            Assert.Equal("53386", result);
        }

        [Fact]
        public async Task PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new AdventOfCode2023.Solutions.Day01.Day1();

            // Act
            var result = await daySolution.GetResultAsync(Part.Two);

            // Assert
            Assert.Equal("53312", result);
        }
    }
}
