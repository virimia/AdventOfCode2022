namespace AdventOfCode.Helpers;

internal static class ReadWriteHelpers
{
    public static string[] ReadTextFile(string day, string fileName = "input.txt")
    {
        var path = $"{Directory.GetCurrentDirectory()}/{day}/{fileName}";

        return File.ReadAllLines(path);
    }

    public static void WriteResult(string day, string exercise, object result)
    {
        Console.WriteLine($"Result for {day} -> exercise {exercise} is: {result}");
    }
}
