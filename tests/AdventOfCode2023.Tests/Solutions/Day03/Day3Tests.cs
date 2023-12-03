namespace AdventOfCode2023.Tests.Solutions.Day03
{
    public class Day3Tests
    {
        [Fact]
        public async Task PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new AdventOfCode2023.Solutions.Day03.Day3();

            // Act
            var result = await daySolution.GetResultAsync(Part.One);

            // Assert
            Assert.Equal("528819", result);
        }

        [Fact]
        public async Task PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new AdventOfCode2023.Solutions.Day03.Day3();

            // Act
            var result = await daySolution.GetResultAsync(Part.Two);

            // Assert
            Assert.Equal("80403602", result);
        }
    }
}
