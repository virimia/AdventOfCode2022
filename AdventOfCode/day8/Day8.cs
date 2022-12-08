using AdventOfCode.day7;
using AdventOfCode.Helpers;
using AdventOfCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.day8;

internal class Day8 : ISolver
{
    private readonly string[] lines;
    public string DayName => nameof(Day8).ToLower();
    private readonly int _length;
    private readonly List<ResultMatrixPoint> _resultMatrix;

    public Day8()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);

        _length = lines[0].Length;
        _resultMatrix = new List<ResultMatrixPoint>();
    }

    public void Solve()
    {
        var resultExercise1 = 0;
        var resultExercise2 = 0;

        // build matrix
        for (int i = 0; i < _length; i++)
        {
            for (int j = 0; j < _length; j++)
            {
                _resultMatrix.Add(new ResultMatrixPoint(
                    i,
                    j,
                    (int)char.GetNumericValue(lines[i][j])));
            }
        }

        foreach (var point in _resultMatrix)
        {
            if (point.IsMatrixPointOnTheEdge(_length))
            {
                point.SetIsTallest();
                point.SetScenicScore(0);
            }
            else
            {
                if (IsTallest(point))
                {
                    point.SetIsTallest();
                }

                point.SetScenicScore(ComputeScenicScore(point)); 
            }
        }

        resultExercise1 = _resultMatrix.Count(x => x.IsTallest);
        resultExercise2 = _resultMatrix.Max(x => x.ScenicScore);

        ReadWriteHelpers.WriteResult(DayName, "1", resultExercise1);
        ReadWriteHelpers.WriteResult(DayName, "2", resultExercise2);
    }

    private bool IsTallest(ResultMatrixPoint currentPoint) 
    {
        var topCondition = _resultMatrix.Where(p => p.X < currentPoint.X && p.Y == currentPoint.Y).All(p => p.CellValue < currentPoint.CellValue);
        var bottomCondition = _resultMatrix.Where(p => p.X > currentPoint.X && p.Y == currentPoint.Y).All(p => p.CellValue < currentPoint.CellValue);

        var leftCondition = _resultMatrix.Where(p => p.Y < currentPoint.Y && p.X == currentPoint.X).All(p => p.CellValue < currentPoint.CellValue);
        var rightCondition = _resultMatrix.Where(p => p.Y > currentPoint.Y && p.X == currentPoint.X).All(p => p.CellValue < currentPoint.CellValue);

        return topCondition || bottomCondition || leftCondition || rightCondition;
    }

    private int ComputeScenicScore(ResultMatrixPoint currentPoint)
    {
        var topPoint = _resultMatrix.Where(p => p.X < currentPoint.X && p.Y == currentPoint.Y).LastOrDefault(p => p.CellValue >= currentPoint.CellValue);
        var bottomPoint = _resultMatrix.Where(p => p.X > currentPoint.X && p.Y == currentPoint.Y).FirstOrDefault(p => p.CellValue >= currentPoint.CellValue);
        var leftPoint = _resultMatrix.Where(p => p.Y < currentPoint.Y && p.X == currentPoint.X).LastOrDefault(p => p.CellValue >= currentPoint.CellValue);
        var rightPoint = _resultMatrix.Where(p => p.Y > currentPoint.Y && p.X == currentPoint.X).FirstOrDefault(p => p.CellValue >= currentPoint.CellValue);

        var result = 1;

        result *= topPoint is null ? currentPoint.X : currentPoint.X - topPoint.X;

        result *= bottomPoint is null ? _length - (currentPoint.X + 1) : bottomPoint.X - currentPoint.X;

        result *= leftPoint is null ? currentPoint.Y : currentPoint.Y - leftPoint.Y;

        result *= rightPoint is null ? _length - (currentPoint.Y + 1) : rightPoint.Y - currentPoint.Y;

        return result;
    }
}
