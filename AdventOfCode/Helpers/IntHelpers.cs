using AdventOfCode.Models;

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

    private static bool IsInInterval(this int input, int start, int end)
    {
        return input >= start && input <= end;
    }
}
