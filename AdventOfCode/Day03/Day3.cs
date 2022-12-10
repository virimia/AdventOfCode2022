using AdventOfCode.Helpers;

namespace AdventOfCode.Day03;

internal class Day3 : ISolver
{
    private readonly string[] lines;

    public string DayName { get => nameof(Day3).ToLower(); }

    public Day3()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
    }

    public void Solve()
    {
        var resultExercise1 = 0;
        var resultExercise2 = 0;

        var exercise2Index = 0;
        List<string> exercise2TempList = new List<string>(3);

        foreach (var line in lines)
        {
            var lineSplitInTwo = line.SplitInChunks(line.Length / 2).ToList();
            var commonPart = lineSplitInTwo.First().Intersect(lineSplitInTwo.Last());

            var convertedChar = ConvertCharacter(commonPart.Single());
            resultExercise1 += convertedChar;

            exercise2TempList.Add(line);
            exercise2Index++;

            if (exercise2Index == 3)
            {
                exercise2Index = 0;

                var exercise2CommonPart = exercise2TempList[0].Intersect(exercise2TempList[1]).Intersect(exercise2TempList[2]);
                var exercise2ConvertedChar = ConvertCharacter(exercise2CommonPart.Single());
                resultExercise2 += exercise2ConvertedChar;

                exercise2TempList.Clear();
            }
        }

        ReadWriteHelpers.WriteResult(DayName, "1", resultExercise1);
        ReadWriteHelpers.WriteResult(DayName, "2", resultExercise2);
    }

    private int ConvertCharacter(char character)
    {
        var charToInt = (int)character;

        return char.IsLower(character) ? charToInt - 96 : charToInt - 38;
    }
}
