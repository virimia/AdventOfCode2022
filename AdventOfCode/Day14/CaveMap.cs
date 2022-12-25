using System.Drawing;

namespace AdventOfCode.Day14;

internal class CaveMap
{
    public const int SandOriginX = 500;
    public const int SandOriginY = 0;

    public readonly int Width;
    public readonly int Height;
    public readonly CellComposition[,] Grid;
    public readonly int Padding;

    public CaveMap(int width, int height, List<List<MyPoint>> paths, int padding = 0)
    {
        Width = width + padding * 2;
        Height = height;
        Grid = new CellComposition[Height, Width];
        Padding = padding;

        for (var y = 0; y < Height; ++y)
        {
            for (var x = 0; x < Width; ++x)
            {
                Grid[y, x] = CellComposition.Air;
            }
        }

        foreach (var path in paths)
        {
            for (var i = 0; i < path.Count - 1; ++i)
            {
                var from = path[i];
                var to = path[i + 1];

                DrawLine(Grid, from, to, CellComposition.Rock, padding);
            }
        }

        Grid[SandOriginY, SandOriginX + Padding] = CellComposition.SandOrigin;
    }

    void DrawLine(CellComposition[,] grid, MyPoint from, MyPoint to, CellComposition cellComposition, int padding)
    {
        if (from == to)
        {
            grid[from.Y, from.X + padding] = cellComposition;
        }
        else if (from.X != to.X)
        {
            DrawHorizontalLine(grid, from, to, cellComposition, padding);
        }
        else
        {
            DrawVerticalLine(grid, from, to, cellComposition, padding);
        }
    }

    private void DrawVerticalLine(CellComposition[,] grid, MyPoint from, MyPoint to, CellComposition cellComposition, int padding)
    {
        var start = Math.Min(from.Y, to.Y) + padding;
        var end = Math.Max(from.Y, to.Y) + padding;

        for (var i = start; i <= end; ++i)
        {
            grid[i, from.X + padding] = cellComposition;
        }
    }

    private void DrawHorizontalLine(CellComposition[,] grid, MyPoint from, MyPoint to, CellComposition cellComposition, int padding)
    {
        var start = Math.Min(from.X, to.X) + padding;
        var end = Math.Max(from.X, to.X) + padding;

        for (var i = start; i <= end; ++i)
        {
            grid[from.Y, i] = cellComposition;
        }
    }

    public SandResult SimulateGrainOfSand()
    {
        var origin = new MyPoint(SandOriginX + Padding, SandOriginY);
        var position = origin;

        if (position.Y >= Height)
        {
            return SandResult.FellInAbyss;
        }

        while (position.Y < Height)
        {
            var (left, middle, right) = GetBelow(position);

            if (CanMove(middle))
            {
                position = middle;
                if (position.Y >= Height)
                {
                    return SandResult.FellInAbyss;
                }
            }
            else if (CanMove(left))
            {
                position = left;
                if (position.Y >= Height)
                {
                    return SandResult.FellInAbyss;
                }
            }
            else if (CanMove(right))
            {
                position = right;
                if (position.Y >= Height)
                {
                    return SandResult.FellInAbyss;
                }
            }
            else
            {
                if (left.Y >= Height && middle.Y >= Height && right.Y >= Height)
                {
                    return SandResult.FellInAbyss;
                }

                Grid[position.Y, position.X] = CellComposition.Sand;
                return position == origin
                    ? SandResult.SourceBlocked
                    : SandResult.Stopped;
            }
        }

        return SandResult.FellInAbyss;
    }

    private bool CanMove(MyPoint destination)
    {
        if (destination.X < 0 || destination.X >= Width || destination.Y >= Height || destination.Y < 0)
        {
            return false;
        }

        return Grid[destination.Y, destination.X] is CellComposition.Air;
    }

    private (MyPoint Left, MyPoint Middle, MyPoint Right) GetBelow(MyPoint coordinate) =>
    (
        Left: coordinate with { X = coordinate.X - 1, Y = coordinate.Y + 1 },
        Middle: coordinate with { Y = coordinate.Y + 1 },
        Right: coordinate with { X = coordinate.X + 1, Y = coordinate.Y + 1 }
    );
}
