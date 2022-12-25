using AdventOfCode.Helpers;

namespace AdventOfCode.Day20;

internal class Day20 : ISolver
{
    public string DayName => nameof(Day20).ToLower();
    private readonly List<string> _lines;

    internal Day20()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }

    public void Solve()
    {
        SolveExercise1();
        SolveExercise2();
    }

    private void SolveExercise1()
    {
        var initialConfiguration = GetInitialConfiguration(1);
        var result = GetResult(initialConfiguration, 1);

        ReadWriteHelpers.WriteResult(DayName, "1", result);
    }


    private void SolveExercise2()
    {
        var initialConfiguration = GetInitialConfiguration(811589153);
        var result = GetResult(initialConfiguration, 10);

        ReadWriteHelpers.WriteResult(DayName, "2", result);
    }

    private long GetResult(List<Coordinate> initialConfiguration, int howManyTimesToMix)
    {
        var tempConfiguration = new List<Coordinate>(initialConfiguration);
        var length = initialConfiguration.Count - 1;

        for (var i = 0; i < howManyTimesToMix; ++i)
        {
            foreach (var item in initialConfiguration)
            {
                var initialItemPosition = tempConfiguration.IndexOf(item);
                var newItemPosition = (int)((initialItemPosition + item.Value) % length);

                if (newItemPosition < 0)
                {
                    newItemPosition += length;
                }

                tempConfiguration.RemoveAt(initialItemPosition);
                tempConfiguration.Insert(newItemPosition, item);
            }
        }

        var positionOfZero = tempConfiguration.FindIndex(x => x.Value == 0);
        var result = tempConfiguration[(positionOfZero + 1000) % tempConfiguration.Count].Value +
            tempConfiguration[(positionOfZero + 2000) % tempConfiguration.Count].Value +
            tempConfiguration[(positionOfZero + 3000) % tempConfiguration.Count].Value;

        return result;
    }

    List<Coordinate> GetInitialConfiguration(long key)
    {
        return _lines.Select((x, index) => new Coordinate(index, long.Parse(x) * key)).ToList();
    }

    class Coordinate
    {
        public int Index { get; set; }
        public long Value { get; set; }

        public Coordinate(int index, long value)
        {
            Index = index;
            Value = value;
        }
    }
}
