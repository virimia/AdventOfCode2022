using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day14;

internal class Day14 : ISolver
{
    public string DayName => nameof(Day14).ToLower();
    private readonly string[] _lines;

    public Day14()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName);
    }

    public void Solve()
    {
        var parsedInput = ParseInput();

        var caveMap = new CaveMap(parsedInput.MaxWidth, parsedInput.MaxHight, parsedInput.Paths);
        
        var counter = 0;
        var result = SandResult.Stopped;

        while (result is SandResult.Stopped)
        {
            result = caveMap.SimulateGrainOfSand();
            if (result is SandResult.Stopped)
            {
                ++counter;
            }
        }

        ReadWriteHelpers.WriteResult(DayName, "1", counter);
    }

    private (List<List<MyPoint>> Paths, int MaxWidth, int MaxHight) ParseInput()
    {
        var paths = new List<List<MyPoint>>();
        var maxWidth = 0;
        var maxHeight = 0;

        foreach (var line in _lines)
        {
            var tokensArrow = line.Split(" -> ");
            var pathsInCurrentLine = new List<MyPoint>();

            foreach (var tokenArrow in tokensArrow)
            {
                var tokens = tokenArrow.Split(",");

                var newPoint = new MyPoint(int.Parse(tokens[0]), int.Parse(tokens[1]));
                pathsInCurrentLine.Add(newPoint);

                maxWidth = Math.Max(maxWidth, newPoint.X);
                maxHeight = Math.Max(maxHeight, newPoint.Y);
            }

            paths.Add(pathsInCurrentLine);
        }

        return (paths, maxWidth + 1, maxHeight + 1);
    }
}
