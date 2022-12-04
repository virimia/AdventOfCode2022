using AdventOfCode.Helpers;
using AdventOfCode.Models;

namespace AdventOfCode.day4;

internal class Day4 : ISolver
{
    private readonly string[] lines;
    public string DayName => nameof(Day4).ToLower();

    public Day4()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
    }

    public void Solve()
    {
        var resultExercise1 = 0;
        var resultExercise2 = 0;

        foreach (var line in lines)
        {
            var arrays = line.Split(',');

            var interval1 = CreateInterval(arrays[0]);
            var interval2 = CreateInterval(arrays[1]);

            if (IntHelpers.CheckIfOneIntervalContainsTheOther(interval1, interval2))
            {
                resultExercise1++;
            }

            if (IntHelpers.CheckIfIntervalsIntersect(interval1, interval2))
            {
                resultExercise2++;
            }
        }

        ReadWriteHelpers.WriteResult(DayName, "1", resultExercise1);
        ReadWriteHelpers.WriteResult(DayName, "2", resultExercise2);
    }

    private Interval CreateInterval(string input)
    {
        var splitedString = input.Split('-');

        return new Interval(
            Convert.ToInt32(splitedString[0]),
            Convert.ToInt32(splitedString[1]));
    }
}
