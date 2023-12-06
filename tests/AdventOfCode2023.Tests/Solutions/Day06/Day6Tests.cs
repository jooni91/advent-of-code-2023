namespace AdventOfCode2023.Tests.Solutions.Day06
{
    public class Day6Tests
    {
        [Fact]
        public async Task PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new AdventOfCode2023.Solutions.Day06.Day6();

            // Act
            var result = await daySolution.GetResultAsync(Part.One);

            // Assert
            Assert.Equal("74698", result);
        }

        [Fact]
        public async Task PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new AdventOfCode2023.Solutions.Day06.Day6();

            // Act
            var result = await daySolution.GetResultAsync(Part.Two);

            // Assert
            Assert.Equal("27563421", result);
        }
    }
}
