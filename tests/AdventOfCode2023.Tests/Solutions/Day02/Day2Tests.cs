namespace AdventOfCode2023.Tests.Solutions.Day02
{
    public class Day2Tests
    {
        [Fact]
        public async Task PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new AdventOfCode2023.Solutions.Day02.Day2();

            // Act
            var result = await daySolution.GetResultAsync(Part.One);

            // Assert
            Assert.Equal("2486", result);
        }

        [Fact]
        public async Task PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new AdventOfCode2023.Solutions.Day02.Day2();

            // Act
            var result = await daySolution.GetResultAsync(Part.Two);

            // Assert
            Assert.Equal("87984", result);
        }
    }
}
