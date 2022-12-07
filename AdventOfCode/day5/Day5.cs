using AdventOfCode.Helpers;
using AdventOfCode.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.day5;

public class Day5 : ISolver
{
    private readonly string[] lines;
    private List<MoveCrateInstruction> _moveCrateInstructions;

    public string DayName => nameof(Day5).ToLower();

    public Day5()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
        _moveCrateInstructions = new List<MoveCrateInstruction>();
    }

    public void Solve()
    {
        //var initialConfiguration = CreateInitialConfiguration();
        var initialConfigurationExercise2 = CreateInitialConfiguration();


        //var initialConfigurationForExercise2 = new Dictionary<int, Stack<char>>();
        //foreach(var item in initialConfiguration)
        //{
        //    initialConfigurationForExercise2.Add(item.Key, item.Value);
        //}

        //foreach (var moveCreateInstruction in _moveCrateInstructions)
        //{
        //    MoveCrate(initialConfiguration, moveCreateInstruction);
        //}

        //var resultExercise1 = new StringBuilder();

        //foreach (var item in initialConfiguration)
        //{
        //    if(item.Value.TryPeek(out char topCrate))
        //    {
        //        resultExercise1.Append(topCrate);
        //    }
        //}

        //ReadWriteHelpers.WriteResult(DayName, "1", resultExercise1);

        var resultExercise2 = new StringBuilder();

        foreach(var moveCreateInstruction in _moveCrateInstructions)
        {
            MoveCrateExercise2(initialConfigurationExercise2, moveCreateInstruction);
        }

        foreach (var item in initialConfigurationExercise2)
        {
            if (item.Value.TryPeek(out char topCrate))
            {
                resultExercise2.Append(topCrate);
            }
        }

        ReadWriteHelpers.WriteResult(DayName, "2", resultExercise2.ToString());
    }

    private Dictionary<int, Stack<char>> CreateInitialConfiguration()
    {
        var lineWithNumbers = lines.Single(x => Regex.IsMatch(x, @"^ \d+"));
        var numberOfInitialColumns = lineWithNumbers
            .Split(' ')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => int.Parse(x))
            .Max();

        var initialConfiguration = new Dictionary<int, Stack<char>>();

        for (var i = 1; i <= numberOfInitialColumns; i++)
        {
            initialConfiguration.Add(i, new Stack<char>());
        }

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)
                || line.Equals(lineWithNumbers)) continue;

            if (line.StartsWith("move"))
            {
                // instruction line
                _moveCrateInstructions.Add(line.GetMoveCrateInstruction());
            }
            else
            {
                // initial stack description line
                for (int i = 0; i < numberOfInitialColumns; i++)
                {
                    // 1..5..9..13..17..21..25..29..33..
                    // i*4+1
                    var currentCrate = line[(i * 4) + 1];

                    if (!char.IsWhiteSpace(currentCrate))
                    {
                        initialConfiguration[i + 1].Push(currentCrate);
                    }
                }
            }
        }

        // reverse all stacks
        foreach (var item in initialConfiguration)
        {
            initialConfiguration[item.Key] = Reverse(item.Value);
        }

        return initialConfiguration;
    }

    private Dictionary<int, Stack<char>> CreateInitialConfiguration(string[] lines)
    {
        var lineWithNumbers = lines.Single(x => Regex.IsMatch(x, @"^ \d+"));
        var numberOfInitialColumns = lineWithNumbers
            .Split(' ')
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => int.Parse(x))
            .Max();

        var createsConfiguration = new Dictionary<int, Stack<char>>();

        for (var i = 1; i <= numberOfInitialColumns; i++)
        {
            createsConfiguration.Add(i, new Stack<char>());
        }

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)
                || line.Equals(lineWithNumbers)) continue;

            if (line.StartsWith("move"))
            {
                // instruction line
                _moveCrateInstructions.Add(line.GetMoveCrateInstruction());
            }
            else
            {
                // initial stack description line
                for (int i = 0; i < numberOfInitialColumns; i++)
                {
                    // 1..5..9..13..17..21..25..29..33..
                    // i*4+1
                    var currentCrate = line[(i * 4) + 1];

                    if (!char.IsWhiteSpace(currentCrate))
                    {
                        createsConfiguration[i + 1].Push(currentCrate);
                    }
                }
            }
        }

        // reverse all stacks
        foreach (var item in createsConfiguration)
        {
            createsConfiguration[item.Key] = Reverse(item.Value);
        }

        return createsConfiguration;
    }

    private void MoveCrateExercise2(Dictionary<int, Stack<char>> inputCrates, MoveCrateInstruction moveCrateInstruction)
    {
        var tempList = new List<char>();

        for (int i = 0; i < moveCrateInstruction.HowManyToMove; i++)
        {
            var createToMove = inputCrates[moveCrateInstruction.MoveFrom].Peek();
            tempList.Add(createToMove);
            inputCrates[moveCrateInstruction.MoveFrom].Pop();
        }

        tempList.Reverse();
        foreach (var item in tempList)
        {
            inputCrates[moveCrateInstruction.MoveTo].Push(item);
        }
    }

    static Stack<char> Reverse(Stack<char> stack)
    {
        Stack<char> temp = new Stack<char>();
        while (stack.Count > 0)
        {
            temp.Push(stack.Pop());
        }
        return temp;
    }
}
