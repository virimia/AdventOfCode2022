using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day12;

internal class Day12 : ISolver
{
    public string DayName => nameof(Day12).ToLower();
    private string[] _lines;

    public Day12()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName);
    }

    public void Solve()
    {
        Exercise1();
        Exercise2();
    }

    public void Exercise1()
    {
        var width = _lines[0].Length;
        var height = _lines.Length;
        var map = new char[width, height];
        var steps = new int[width, height];
        VisitedPoint startPosition = default;
        VisitedPoint endPosition = default;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, y] = _lines[y][x];
                if (map[x, y] == 'S')
                {
                    startPosition = new VisitedPoint(x, y);
                }
                else if (map[x, y] == 'E')
                {
                    endPosition = new VisitedPoint(x, y);
                }
            }
        }

        map[startPosition.X, startPosition.Y] = 'z';
        map[endPosition.X, endPosition.Y] = 'z';

        var queue = new Queue<VisitedPoint>();
        queue.Enqueue(startPosition);

        while (queue.Count > 0)
        {
            var currentPoint = queue.Dequeue();
            var nextSteps = steps[currentPoint.X, currentPoint.Y] + 1;

            foreach (var direction in WithoutDiagonals)
            {
                var newPosition = currentPoint + direction;

                if (newPosition.X >= 0 &&
                    newPosition.Y >= 0 &&
                    newPosition.X < width &&
                    newPosition.Y < height &&
                    steps[newPosition.X, newPosition.Y] == 0 &&
                    newPosition != startPosition)
                {
                    if (map[newPosition.X, newPosition.Y] <= map[currentPoint.X, currentPoint.Y] + 1)
                    {
                        if (newPosition == endPosition)
                        {
                            ReadWriteHelpers.WriteResult(DayName, "1", nextSteps);
                            return;
                        }

                        steps[newPosition.X, newPosition.Y] = nextSteps;
                        queue.Enqueue(newPosition);
                    }
                }
            }
        }
    }

    public void Exercise2()
    {
        var width = _lines[0].Length;
        var height = _lines.Length;
        var map = new char[width, height];

        VisitedPoint endPosition = default;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map[x, y] = _lines[y][x];
                if (map[x, y] == 'S')
                {
                    map[x, y] = 'a';
                }
                else if (map[x, y] == 'E')
                {
                    endPosition = new VisitedPoint(x, y);
                }
            }
        }

        map[endPosition.X, endPosition.Y] = 'z';

        var best = int.MaxValue;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 'a')
                {
                    best = Math.Min(ComputeDistance(x, y), best);
                }
            }
        }

        ReadWriteHelpers.WriteResult(DayName, "2", best);

        int ComputeDistance(int x, int y)
        {
            VisitedPoint startPosition = (x, y);

            var steps = new int[width, height];

            var queue = new Queue<VisitedPoint>();
            queue.Enqueue(startPosition);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                var nextSteps = steps[current.X, current.Y] + 1;
                foreach (var direction in WithoutDiagonals)
                {
                    var newPosition = current + direction;
                    if (newPosition.X >= 0 &&
                        newPosition.Y >= 0 &&
                        newPosition.X < width &&
                        newPosition.Y < height &&
                        steps[newPosition.X, newPosition.Y] == 0 &&
                        newPosition != startPosition)
                    {
                        if (map[newPosition.X, newPosition.Y] <= map[current.X, current.Y] + 1)
                        {
                            if (newPosition == endPosition)
                            {
                                return nextSteps;
                            }

                            steps[newPosition.X, newPosition.Y] = nextSteps;
                            queue.Enqueue(newPosition);
                        }
                    }
                }
            }

            return int.MaxValue;
        }
    }

    private static VisitedPoint[] WithoutDiagonals { get; } = new VisitedPoint[]
    {
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0),
    };

    public record struct VisitedPoint(int X, int Y)
    {
        public static VisitedPoint operator +(VisitedPoint a, VisitedPoint b) => new VisitedPoint(a.X + b.X, a.Y + b.Y);

        public static implicit operator VisitedPoint((int X, int Y) tuple) => new VisitedPoint(tuple.X, tuple.Y);
    }
}
