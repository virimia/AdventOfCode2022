using AdventOfCode.Models;

namespace AdventOfCode.Helpers;

internal static class MixedHelpers
{
    internal static bool IsMatrixPointOnTheEdge(this ResultMatrixPoint matrixPoint, int lineLength)
    {
        return matrixPoint.X == 0
            || matrixPoint.X == lineLength - 1
            || matrixPoint.Y == 0
            || matrixPoint.Y == lineLength - 1;
    }
}
