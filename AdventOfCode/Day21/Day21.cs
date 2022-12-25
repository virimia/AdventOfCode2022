using AdventOfCode.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day21;

internal class Day21 : ISolver
{
    public string DayName => nameof(Day21).ToLower();
    private readonly List<string> _lines;

    internal Day21()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
    }

    public void Solve()
    {
        //SolveExercise1();
        SolveExercise2();
    }

    private void SolveExercise1()
    {
        var allMonkeys = new List<Monkey>();

        foreach (var line in _lines)
        {
            var monkeyTokens = line.Split(": ");
            var monkeyOperationTokensWithoutWhiteSpace = monkeyTokens[1].Replace(" ", "");

            Monkey currentMonkey = null;

            if (allMonkeys.Any(x => x.Name == monkeyTokens[0]))
            {
                currentMonkey = allMonkeys.Single(x => x.Name == monkeyTokens[0]);
            }
            else
            {
                currentMonkey = new Monkey(monkeyTokens[0]);

                allMonkeys.Add(currentMonkey);
            }

            if (long.TryParse(monkeyOperationTokensWithoutWhiteSpace, out var monkeyNumber))
            {
                // number
                currentMonkey.Number = monkeyNumber;
            }
            else
            {
                // operation
                var monkeyOperationElements = ParseOperation(monkeyOperationTokensWithoutWhiteSpace);

                currentMonkey.Operation = monkeyOperationElements.Operation;

                Monkey newLeftMonkey = null;
                Monkey newRightMonkey = null;

                if (!allMonkeys.Any(x => x.Name == monkeyOperationElements.LeftName))
                {
                    newLeftMonkey = new Monkey(monkeyOperationElements.LeftName);

                    allMonkeys.Add(newLeftMonkey);
                }
                else
                {
                    newLeftMonkey = allMonkeys.Single(x => x.Name == monkeyOperationElements.LeftName);
                }

                if (!allMonkeys.Any(x => x.Name == monkeyOperationElements.RightName))
                {
                    newRightMonkey = new Monkey(monkeyOperationElements.RightName);

                    allMonkeys.Add(newRightMonkey);
                }
                else
                {
                    newRightMonkey = allMonkeys.Single(x => x.Name == monkeyOperationElements.RightName);
                }

                currentMonkey.LeftMonkey = newLeftMonkey;
                currentMonkey.RightMonkey = newRightMonkey;
            }
        }

        var rootMonkey = allMonkeys.Single(x => x.Name == "root");
        CalculateNumber(rootMonkey);

        Console.WriteLine($"tlpd: {allMonkeys.Single(x => x.Name == "tlpd").Number}");
        Console.WriteLine($"jjmw: {allMonkeys.Single(x => x.Name == "jjmw").Number}");
        Console.WriteLine($"humn: {allMonkeys.Single(x => x.Name == "humn").Number}");

        ReadWriteHelpers.WriteResult(DayName, "1", rootMonkey.Number);
    }

    private void SolveExercise2()
    {
        var allMonkeys = new List<Monkey>();

        foreach (var line in _lines)
        {
            var monkeyTokens = line.Split(": ");
            var monkeyOperationTokensWithoutWhiteSpace = monkeyTokens[1].Replace(" ", "");

            Monkey currentMonkey = null;

            if (allMonkeys.Any(x => x.Name == monkeyTokens[0]))
            {
                currentMonkey = allMonkeys.Single(x => x.Name == monkeyTokens[0]);
            }
            else
            {
                currentMonkey = new Monkey(monkeyTokens[0]);

                allMonkeys.Add(currentMonkey);
            }

            if (long.TryParse(monkeyOperationTokensWithoutWhiteSpace, out var monkeyNumber))
            {
                // number
                currentMonkey.Number = monkeyNumber;
            }
            else
            {
                // operation
                var monkeyOperationElements = ParseOperation(monkeyOperationTokensWithoutWhiteSpace);

                currentMonkey.Operation = monkeyOperationElements.Operation;

                Monkey newLeftMonkey = null;
                Monkey newRightMonkey = null;

                if (!allMonkeys.Any(x => x.Name == monkeyOperationElements.LeftName))
                {
                    newLeftMonkey = new Monkey(monkeyOperationElements.LeftName);

                    allMonkeys.Add(newLeftMonkey);
                }
                else
                {
                    newLeftMonkey = allMonkeys.Single(x => x.Name == monkeyOperationElements.LeftName);
                }

                if (!allMonkeys.Any(x => x.Name == monkeyOperationElements.RightName))
                {
                    newRightMonkey = new Monkey(monkeyOperationElements.RightName);

                    allMonkeys.Add(newRightMonkey);
                }
                else
                {
                    newRightMonkey = allMonkeys.Single(x => x.Name == monkeyOperationElements.RightName);
                }

                currentMonkey.LeftMonkey = newLeftMonkey;
                currentMonkey.RightMonkey = newRightMonkey;
            }
        }

        var rootMonkey = allMonkeys.Single(x => x.Name == "root");

        CalculateNumber(rootMonkey);

        var leftMonkey = allMonkeys.Single(x => x.Name == "tlpd");
        var rightMonkey = allMonkeys.Single(x => x.Name == "jjmw");
        //var leftMonkey = allMonkeys.Single(x => x.Name == "pppw");
        //var rightMonkey = allMonkeys.Single(x => x.Name == "sjmn");
        var humnMonkey = allMonkeys.Single(x => x.Name == "humn");

        var leftContainsHumn = Find(leftMonkey);
        var rightContainsHumn = Find(rightMonkey);

        if (leftContainsHumn)
        {
            var difference = rightMonkey.Number - leftMonkey.Number;
        }
        else
        {
            var difference = leftMonkey.Number - rightMonkey.Number;
        }

        var x = GetQueryString(string.Empty, leftMonkey, humnMonkey, rightMonkey.Number.Value);
        var matches = Regex.Matches(x, @"(\d+[+\-*\/^%])*(\d+)");

        foreach (var match in matches)
        {
            Match currentMatch = (Match)match;

        }

        //CalculateNumber(rootMonkey);
        //long i = 391100078873;

        //while (tlpdMonkey.Number != jjmwMonkey.Number)
        //{
        //    i++;
        //    allMonkeys.Single(x => x.Name == "humn").Number = i;
        //    CalculateNumber(rootMonkey);
        //}

        // "4+2*5-3/4"
        // (sllz + lgvd) / lfqf
        // (4 + (ljgn * ptdq)) / 4
        // (4 + 2 * (humn - dvpt)) / 4
        // (4 + 2 * (5 - 3))) / 4
        // (4 + (2 * (5 - 3))) / 4
        // ((4 + (2 * (x - 3))) / 4) = 150 => x = ?

        Console.WriteLine($"tlpd: {allMonkeys.Single(x => x.Name == "tlpd").Number}");
        Console.WriteLine($"jjmw: {allMonkeys.Single(x => x.Name == "jjmw").Number}");
        Console.WriteLine($"humn: {allMonkeys.Single(x => x.Name == "humn").Number}");

        ReadWriteHelpers.WriteResult(DayName, "1", rootMonkey.Number);
    }

    string GetQueryString(string result, Monkey? monkey, Monkey humn, long expectedNumber)
    {
        if (monkey is null)
        {
            return string.Empty;
        }

        if (monkey.LeftMonkey is null && monkey.RightMonkey is null)
        {
            return monkey.Name == humn.Name ? "x" : monkey.Number.ToString();
        }

        result = monkey.Operation switch
        {
            MonkeyOperation.Plus => $"({GetQueryString(result, monkey.LeftMonkey, humn, expectedNumber)}+{GetQueryString(result, monkey.RightMonkey, humn, expectedNumber)})",
            MonkeyOperation.Minus => $"({GetQueryString(result, monkey.LeftMonkey, humn, expectedNumber)}-{GetQueryString(result, monkey.RightMonkey, humn, expectedNumber)})",
            MonkeyOperation.Multiply => $"({GetQueryString(result, monkey.LeftMonkey, humn, expectedNumber)}*{GetQueryString(result, monkey.RightMonkey, humn, expectedNumber)})",
            MonkeyOperation.Divide => $"({GetQueryString(result, monkey.LeftMonkey, humn, expectedNumber)}/{GetQueryString(result, monkey.RightMonkey, humn, expectedNumber)})",
            _ => throw new ArgumentException()
        };

        return result;
    }

    bool Find(Monkey monkey)
    {
        if (monkey is null)
        {
            return false;
        }

        var foundLeft = false;
        while ((monkey.LeftMonkey is not null && monkey.LeftMonkey.Name == "humn")
            || (monkey.RightMonkey is not null && monkey.RightMonkey.Name == "humn"))
        {
            return true;
        }

        return Find(monkey.LeftMonkey) || Find(monkey.RightMonkey);
    }

    private (string LeftName, string RightName, MonkeyOperation Operation) ParseOperation(string monkeyOperation)
    {
        var operation = monkeyOperation[4] switch
        {
            '+' => MonkeyOperation.Plus,
            '-' => MonkeyOperation.Minus,
            '*' => MonkeyOperation.Multiply,
            '/' => MonkeyOperation.Divide,
            _ => throw new ArgumentException()
        };

        var monkeyLeftName = monkeyOperation.Substring(0, 4);
        var monkeyRightName = monkeyOperation.Substring(5, 4);

        return new(monkeyLeftName, monkeyRightName, operation);
    }

    long? CalculateNumber(Monkey? monkey)
    {
        if (monkey is null)
        {
            return null;
        }

        if (monkey.LeftMonkey is null && monkey.RightMonkey is null)
        {
            return monkey.Number;
        }

        var result = monkey.Operation switch
        {
            MonkeyOperation.Plus => CalculateNumber(monkey.LeftMonkey) + CalculateNumber(monkey.RightMonkey),
            MonkeyOperation.Minus => CalculateNumber(monkey.LeftMonkey) - CalculateNumber(monkey.RightMonkey),
            MonkeyOperation.Multiply => CalculateNumber(monkey.LeftMonkey) * CalculateNumber(monkey.RightMonkey),
            MonkeyOperation.Divide => (long)(CalculateNumber(monkey.LeftMonkey) / CalculateNumber(monkey.RightMonkey)),
            _ => throw new ArgumentException()
        };

        monkey.Number = result;

        //Console.WriteLine($"{monkey.Name}: {monkey.LeftMonkey.Name} {monkey.Operation} {monkey.RightMonkey.Name} = {result}");

        return result;
    }

    class Monkey
    {
        public string Name { get; set; }
        public long? Number { get; set; } // either input or computed
        public MonkeyOperation? Operation { get; set; }
        public Monkey? LeftMonkey { get; set; }
        public Monkey? RightMonkey { get; set; }

        public Monkey(string name)
        {
            Name = name;
        }
    }

    long ExecuteOperation(MonkeyOperation operation, long left, long right)
    {
        return operation switch
        {
            MonkeyOperation.Plus => left + right,
            MonkeyOperation.Minus => left - right,
            MonkeyOperation.Multiply => left * right,
            MonkeyOperation.Divide => (long)(left / right),
            _ => throw new ArgumentException()
        };
    }

    enum MonkeyOperation
    {
        Plus,
        Minus,
        Multiply,
        Divide
    }
}
