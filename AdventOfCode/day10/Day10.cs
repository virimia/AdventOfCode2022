using AdventOfCode.Helpers;

namespace AdventOfCode.day10;

internal class Day10 : ISolver
{
    public string DayName => nameof(Day10).ToLower();
    private readonly string[] _lines;
    private List<Cycle> _cycles;
    private const string _noop = "noop";

    public Day10()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName);
    }

    public void Solve()
    {
        var resultExercise1 = 0;
        _cycles = new List<Cycle>();
        Cycle? prevCycle = null;

        foreach (var line in _lines)
        {
            var x = prevCycle is not null ? prevCycle.Index : 1;
            if (line == _noop)
            {
                var cycle = new Cycle(
                    prevCycle is not null ? prevCycle.Index + 1 : 1,
                    prevCycle is not null ? prevCycle.X : 1,
                    line,
                    prevCycle);
                prevCycle = cycle;

                _cycles.Add(cycle);

                continue;
            }

            var tokenValue = Convert.ToInt32(line.Split(' ')[1]);

            // add 2 cycles
            var firstCycle = new Cycle(
                prevCycle is not null ? prevCycle.Index + 1 : 1,
                prevCycle is not null ? prevCycle.X : 1,
                line,
                prevCycle);

            prevCycle = firstCycle;

            _cycles.Add(firstCycle);

            var secondCycle = new Cycle(
                prevCycle.Index + 1,
                prevCycle.X + tokenValue,
                line,
                prevCycle);
            prevCycle = secondCycle;

            _cycles.Add(secondCycle);
        }

        resultExercise1 =
            CalculateStrength(_cycles, 20) +
            CalculateStrength(_cycles, 60) +
            CalculateStrength(_cycles, 100) +
            CalculateStrength(_cycles, 140) +
            CalculateStrength(_cycles, 180) +
            CalculateStrength(_cycles, 220);

        ReadWriteHelpers.WriteResult(DayName, "1", resultExercise1);

        Console.WriteLine();

        Exercise2();
    }

    private void Exercise2()
    {
        var crtIndex = -1;

        foreach (var cycle in _cycles)
        {
            crtIndex++;

            CheckPositionAndDrawPixel(cycle, crtIndex);

            if (crtIndex == 39)
            {
                // should be exit condition
                crtIndex = -1;
                Console.WriteLine();
            }
        }
    }

    private void CheckPositionAndDrawPixel(Cycle cycle, int position)
    {
        if (position == 0 || position == 1)
        {
            Console.Write("#");

            return;
        }

        if (position == cycle.PreviousCycle?.X - 1 || position == cycle.PreviousCycle?.X || position == cycle.PreviousCycle?.X + 1)
        {
            Console.Write("#");
        }
        else
        {
            Console.Write(".");
        }
    }

    private int CalculateStrength(List<Cycle> cycles, int index)
    {
        return index * cycles.Single(c => c.Index == index - 1).X;
    }

    private record Cycle(int Index, int X, string Instruction, Cycle? PreviousCycle)
    {
        public int SignalStrength
        {
            get
            {
                return this.Index * this.X;
            }
        }

        public override string ToString()
        {
            return $"Index: {Index} | X: {X} | Instruction: {Instruction} | SignalStrength: {SignalStrength}";
        }
    }
}
