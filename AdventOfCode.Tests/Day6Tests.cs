using AdventOfCode.day6;

namespace AdventOfCode.Tests;

public class Day6Tests
{
    private readonly Day6 _sut;

    public Day6Tests()
    {
        _sut = new Day6();
    }

    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void When_Exercise1_GetCorrectResult(string input, int expected)
    {
        // Act
        var result = _sut.Exercise1(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    public void When_Exercise2_GetCorrectResult(string input, int expected)
    {
        // Act
        var result = _sut.Exercise2(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
