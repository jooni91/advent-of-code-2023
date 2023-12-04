namespace AdventOfCode2023.Tests.Solutions.Day04
{
    public class Day4Tests
    {
        [Fact]
        public async Task PartOne_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new AdventOfCode2023.Solutions.Day04.Day4();

            // Act
            var result = await daySolution.GetResultAsync(Part.One);

            // Assert
            Assert.Equal("23750", result);
        }

        [Fact]
        public async Task PartTwo_ShouldReturn_ExpectedValue()
        {
            // Arrange
            var daySolution = new AdventOfCode2023.Solutions.Day04.Day4();

            // Act
            var result = await daySolution.GetResultAsync(Part.Two);

            // Assert
            Assert.Equal("13261850", result);
        }
    }
}
