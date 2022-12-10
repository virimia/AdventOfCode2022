using AdventOfCode.Helpers;

namespace AdventOfCode.Day01;

internal class Day1 : ISolver
{
    private readonly string[] lines;
    public string DayName => nameof(Day1).ToLower();

    public Day1()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
    }

    public void Solve()
    {
        var elfs = new List<Elf>();
        var index = -1;
        var tempSum = 0;

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                index++;

                elfs.Add(new(index, tempSum));

                tempSum = 0;
            }
            else
            {
                tempSum += Convert.ToInt32(line);
            }
        }

        var sortedElfs = elfs.OrderByDescending(el => el.Calories).ToList();

        SolveExercise1(sortedElfs);
        SolveExercise2(sortedElfs);
    }

    private void SolveExercise1(List<Elf> elfs)
    {
        ReadWriteHelpers.WriteResult(DayName, "1", elfs.First().Calories);
    }

    private void SolveExercise2(List<Elf> elfs)
    {
        ReadWriteHelpers.WriteResult(DayName, "2", elfs[0].Calories + elfs[1].Calories + elfs[2].Calories);
    }

    private record Elf(int Index, int Calories);
}
