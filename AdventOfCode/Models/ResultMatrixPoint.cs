namespace AdventOfCode.Models;

public class ResultMatrixPoint
{
    public int X;
    public int Y;
    public int CellValue;
    public int ScenicScore { get; private set; }
    public bool IsTallest { get; private set; }

    public ResultMatrixPoint(int x, int y, int cellValue)
    {
        X = x;
        Y = y;
        CellValue = cellValue;
    }

    public ResultMatrixPoint(int x, int y, int cellValue, bool isTallest) : this(x, y, cellValue)
    {
        IsTallest = isTallest;
    }

    public void SetIsTallest()
    {
        IsTallest = true;
    }
    public void SetScenicScore(int scenicScore)
    {
        ScenicScore = scenicScore;
    }
}
