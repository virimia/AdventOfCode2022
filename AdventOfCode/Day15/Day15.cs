using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15;

internal class Day15 : ISolver
{
    public string DayName => nameof(Day15).ToLower();
    private readonly string[] _lines;

    public Day15()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName);

        var sensors = new List<Sensor>();

        foreach(var line in _lines)
        {

        }
    }

    public void Solve()
    {

    }
}
