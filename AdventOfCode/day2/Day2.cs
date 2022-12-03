using AdventOfCode.Helpers;

namespace AdventOfCode.day2;

internal class Day2 : ISolver
{
    private readonly string[] lines;
    private readonly Dictionary<string, int> _allCombosExercise1;
    private readonly Dictionary<string, int> _allCombosExercise2;

    public string DayName => nameof(Day2).ToLower();

    public Day2()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);

        _allCombosExercise1 = new Dictionary<string, int>
        {
            { "A X", 4 },
            { "A Y", 8 },
            { "A Z", 3 },

            { "B X", 1 },
            { "B Y", 5 },
            { "B Z", 9 },

            { "C X", 7 },
            { "C Y", 2 },
            { "C Z", 6 },
        };

        _allCombosExercise2 = new Dictionary<string, int>
        {
            { "A X", 3 },
            { "A Y", 4 },
            { "A Z", 8 },

            { "B X", 1 },
            { "B Y", 5 },
            { "B Z", 9 },

            { "C X", 2 },
            { "C Y", 6 },
            { "C Z", 7 },
        };
    }

    /*
     A, X - rock (1)
     B, Y - paper (2)
     C, Z - scisors (3)

     X - lose
     Y - draw
     Z - win
     */

    public void Solve()
    {
        var resultExercise1 = 0;
        var resultExercise2 = 0;

        foreach (var line in lines)
        {
            resultExercise1 += _allCombosExercise1[line];
            resultExercise2 += _allCombosExercise2[line];
        }

        ReadWriteHelpers.WriteResult(DayName, "1", resultExercise1);
        ReadWriteHelpers.WriteResult(DayName, "2", resultExercise2);
    }
}

internal enum Score
{
    Loss = 0,
    Draw = 3,
    Win = 6
}
