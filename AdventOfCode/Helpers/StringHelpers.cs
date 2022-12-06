namespace AdventOfCode.Helpers;

public static class StringHelpers
{
    public static IEnumerable<string> SplitInChunks(this string inputString, int chunkSize)
    {
        return Enumerable
            .Range(0, inputString.Length / chunkSize)
            .Select(x => inputString.Substring(x * chunkSize, chunkSize));
    }

    public static bool CheckIfCharactersAreDifferent(this string input)
    {
        return input.Distinct().Count() == input.Length;
    }
}
