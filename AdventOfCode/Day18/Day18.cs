using AdventOfCode.Helpers;

namespace AdventOfCode.Day18;

internal class Day18 : ISolver
{
    public string DayName => nameof(Day18).ToLower();
    private readonly string[] _lines;
    private List<MyCube> _cubes;

    public Day18()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        _cubes = new List<MyCube>(_lines.Length);

        foreach (var line in _lines)
        {
            var tokens = line.Split(',');
            var currentCube = new MyCube(int.Parse(tokens[0]), int.Parse(tokens[1]), int.Parse(tokens[2]));

            _cubes.Add(currentCube);
        }
    }

    public void Solve()
    {
        SolveExercise1();
        SolveExercise2();
    }

    private void SolveExercise1()
    {
        var resultExercise1 = _cubes.SelectMany(GetNeighbours).Count(neighbouringCube => !_cubes.Contains(neighbouringCube));

        ReadWriteHelpers.WriteResult(DayName, "1", resultExercise1);
    }

    private void SolveExercise2()
    {

    }

    List<MyCube> GetNeighbours(MyCube cube) => new()
    {
        cube with { X = cube.X - 1 },
        cube with { X = cube.X + 1 },
        cube with { Y = cube.Y - 1 },
        cube with { Y = cube.Y + 1 },
        cube with { Z = cube.Z - 1 },
        cube with { Z = cube.Z + 1 },
    };

    private record MyCube(int X, int Y, int Z);
}
