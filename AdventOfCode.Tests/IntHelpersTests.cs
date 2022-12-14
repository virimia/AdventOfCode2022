using AdventOfCode.Helpers;
using AdventOfCode.Models;

namespace AdventOfCode.Tests;

public class IntHelpersTests
{
    public static IEnumerable<object[]> IntervalsOverlapTestData()
    {
        yield return new object[] { new Interval(2, 4), new Interval(6, 8), false };
        yield return new object[] { new Interval(2, 3), new Interval(4, 5), false };
        yield return new object[] { new Interval(5, 7), new Interval(7, 9), false };
        yield return new object[] { new Interval(2, 8), new Interval(3, 7), true };
        yield return new object[] { new Interval(6, 6), new Interval(4, 6), true };
        yield return new object[] { new Interval(2, 6), new Interval(4, 8), false };
        yield return new object[] { new Interval(38, 41), new Interval(38, 38), true };
    }

    [Theory]
    [MemberData(nameof(IntervalsOverlapTestData))]
    public void When_CheckInterval_CatchOverlap(Interval interval1, Interval interval2, bool expected)
    {
        // Act
        var result = IntHelpers.CheckIfOneIntervalContainsTheOther(interval1, interval2);

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> IntervalsIntersectTestData()
    {
        yield return new object[] { new Interval(2, 4), new Interval(6, 8), false };
        yield return new object[] { new Interval(2, 3), new Interval(4, 5), false };
        yield return new object[] { new Interval(5, 7), new Interval(7, 9), true };
        yield return new object[] { new Interval(2, 8), new Interval(3, 7), true };
        yield return new object[] { new Interval(6, 6), new Interval(4, 6), true };
        yield return new object[] { new Interval(2, 6), new Interval(4, 8), true };
        yield return new object[] { new Interval(38, 41), new Interval(38, 38), true };
    }

    [Theory]
    [MemberData(nameof(IntervalsIntersectTestData))]
    public void When_CheckInterval_CatchIntersectionp(Interval interval1, Interval interval2, bool expected)
    {
        // Act
        var result = IntHelpers.CheckIfIntervalsIntersect(interval1, interval2);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("move 1 from 2 to 1", 1, 2, 1)]
    [InlineData("move 3 from 1 to 3", 3, 1, 3)]
    [InlineData("move 2 from 2 to 1", 2, 2, 1)]
    [InlineData("move 1 from 1 to 2", 1, 1, 2)]
    public void When_GetMoveCrateInstruction_GetCorrectResult(string input, int expectedHowManyToMove, int expectedMoveFrom, int expectedMoveTo)
    {
        // Act
        var result = IntHelpers.GetMoveCrateInstruction(input);

        // Assert
        Assert.Equal(expectedHowManyToMove, result.HowManyToMove);
        Assert.Equal(expectedMoveFrom, result.MoveFrom);
        Assert.Equal(expectedMoveTo, result.MoveTo);
    }
}
