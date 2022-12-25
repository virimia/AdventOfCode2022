using AdventOfCode.Helpers;

namespace AdventOfCode.Day25;

internal class Day25 : ISolver
{
    public string DayName => nameof(Day25).ToLower();
    private readonly List<string> _lines;
    const string _snafu = "=-012";

    public Day25()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }

    public void Solve()
    {
        long totalBase10 = 0;

        foreach (var line in _lines)
        {
            long coef = 1;
            long tempResult = 0;

            for (var i = line.Length - 1; i >= 0; i--)
            {
                var index = _snafu.IndexOf(line[i]);
                var fixedIndex = index - 2;
                tempResult += fixedIndex * coef;

                coef *= 5;
            }

            totalBase10 += tempResult;

            Console.WriteLine($"SNAFU: {line}   Decimal: {tempResult}");
        }

        var result = Base10ToSnafu(totalBase10);

        ReadWriteHelpers.WriteResult(DayName, "1", result);
    }

    string Base10ToSnafu(long base10Number)
    {
        var result = string.Empty;
        long fixedBase10Number = base10Number < 0 ? -1 * base10Number : base10Number;

        while (fixedBase10Number > 0)
        {
            var rem = fixedBase10Number % 5;
            fixedBase10Number /= 5;

            if (rem <= 2)
            {
                result = rem + result;
            }
            else
            {
                result = "   =-"[(int)rem] + result;

                fixedBase10Number += 1;
            }
        }

        return result;
    }
}
