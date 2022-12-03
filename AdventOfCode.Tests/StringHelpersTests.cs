using AdventOfCode.Helpers;

namespace AdventOfCode.Tests;

public class StringHelpersTests
{
    [Theory]
    [InlineData("vJrwpWtwJgWrhcsFMMfFFhFp", "vJrwpWtwJgWr", "hcsFMMfFFhFp")]
    [InlineData("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL", "jqHRNqRjqzjGDLGL", "rsFMfFZSrLrFZsSL")]
    [InlineData("PmmdzqPrVvPwwTWBwg", "PmmdzqPrV", "vPwwTWBwg")]
    [InlineData("wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn", "wMqvLMZHhHMvwLH", "jbvcjnnSBnvTQFn")]
    [InlineData("ttgJtRGJQctTZtZT", "ttgJtRGJ", "QctTZtZT")]
    [InlineData("CrZsJsPPZsGzwwsLwLmpwMDw", "CrZsJsPPZsGz", "wwsLwLmpwMDw")]
    public void When_SplitStringIn2_ReturnCorrect2Strings(string inputString, string expectedFirstString, string expectedSecondString)
    {
        // Act
        var result = inputString.SplitInChunks(inputString.Length / 2);

        // Assert
        Assert.Equal(expectedFirstString, result.First());
        Assert.Equal(expectedSecondString, result.Last());
    }
}