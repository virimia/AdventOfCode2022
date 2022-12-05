using AdventOfCode.Models;
using System.Text.RegularExpressions;

namespace AdventOfCode.Helpers;

public static class IntHelpers
{
    public static bool CheckIfOneIntervalContainsTheOther(Interval interval1, Interval interval2)
    {
        return (interval1.Start <= interval2.Start && interval1.End >= interval2.End)
            || (interval1.Start >= interval2.Start && interval1.End <= interval2.End);
    }

    public static bool CheckIfIntervalsIntersect(Interval interval1, Interval interval2)
    {
        return interval1.Start.IsInInterval(interval2.Start, interval2.End)
            || interval1.End.IsInInterval(interval2.Start, interval2.End)
            || interval2.Start.IsInInterval(interval1.Start, interval1.End)
            || interval2.End.IsInInterval(interval1.Start, interval1.End);
    }

    public static MoveCrateInstruction GetMoveCrateInstruction(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        string[] numbers = (Regex.Split(input, @"\D+"))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x)
            .ToArray();
        var howManyToMove = int.Parse(numbers[0]);
        var moveFrom = int.Parse(numbers[1]);
        var moveTo = int.Parse(numbers[2]);

        return new(
            howManyToMove, 
            moveFrom, 
            moveTo);
    }

    private static bool IsInInterval(this int input, int start, int end)
    {
        return input >= start && input <= end;
    }
}
