using AdventOfCode.Helpers;

namespace AdventOfCode.Day06;

public class Day6 : ISolver
{
    private string[] lines;
    public string DayName => nameof(Day6).ToLower();

    public void Solve()
    {
        ReadFile();
        var singleLine = lines.Single();

        var resultExercise1 = Exercise1(singleLine);
        var resultExercise2 = Exercise2(singleLine);

        ReadWriteHelpers.WriteResult(DayName, "1", resultExercise1);
        ReadWriteHelpers.WriteResult(DayName, "2", resultExercise2);
    }

    public int Exercise1(string singleLine)
    {
        for (int i = 3; i < singleLine.Length; i++)
        {
            var substring = singleLine.Substring(i - 3, 4);
            var allCharactersAreDifferent = substring.CheckIfCharactersAreDifferent();

            if (allCharactersAreDifferent)
            {
                return i + 1;
            }
        }

        return -1;
    }

    public int Exercise2(string singleLine)
    {
        for (int i = 14; i < singleLine.Length; i++)
        {
            var substring = singleLine.Substring(i - 14, 14);
            var allCharactersAreDifferent = substring.CheckIfCharactersAreDifferent();

            if (allCharactersAreDifferent)
            {
                return i;
            }
        }

        return -1;
    }

    private void ReadFile()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
    }
}
