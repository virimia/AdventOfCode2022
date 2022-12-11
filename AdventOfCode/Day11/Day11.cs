using AdventOfCode.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day11;

internal class Day11 : ISolver
{
    public string DayName => nameof(Day11).ToLower();
    private readonly string[] _lines;

    public Day11()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName);
    }

    public void Solve()
    {
        PerformExercise1();
        PerformExercise2();
    }

    void PerformExercise1()
    {
        var monkeys = GetMonkeys();

        Console.WriteLine("---- Exercise 1 -----");

        var result = 0;

        for (int i = 1; i <= 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey.StartingItems)
                {
                    monkey.IncreaseVisitedItems();

                    var newWorryLevel = GetWorryLevel(monkey.OperationString, item);

                    var worryLevelAdjusted = Math.Floor(Convert.ToDouble(newWorryLevel / 3));
                    var worryLevelAdjustedAsInt = Convert.ToInt32(worryLevelAdjusted);

                    if (worryLevelAdjustedAsInt % monkey.TestCondition == 0)
                    {
                        monkeys.Single(m => m.Index == monkey.TrueConditionThrowToMonkeyIndex).StartingItems.Enqueue(worryLevelAdjustedAsInt);
                    }
                    else
                    {
                        monkeys.Single(m => m.Index == monkey.FalseConditionThrowToMonkeyIndex).StartingItems.Enqueue(worryLevelAdjustedAsInt);
                    }
                }

                monkey.StartingItems.Clear();
            }
        }

        result = monkeys
            .OrderByDescending(x => x.VisitedItems)
            .Take(2)
            .Select(m => m.VisitedItems)
            .Aggregate(1, (x, y) => (int)(x * y));

        ReadWriteHelpers.WriteResult(DayName, "1", result);
    }

    void PerformExercise2()
    {
        Console.WriteLine("---- Exercise 2 -----");
        var monkeys = GetMonkeys();

        var mod = 1;

        foreach (var monkey in monkeys)
        {
            mod *= monkey.TestCondition;
        }

        for (int i = 1; i <= 10000; i++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey.StartingItems)
                {
                    monkey.IncreaseVisitedItems();

                    var newWorryLevel = GetWorryLevel(monkey.OperationString, item);

                    var worryLevelAdjusted = newWorryLevel % mod;

                    if (worryLevelAdjusted % monkey.TestCondition == 0)
                    {
                        monkeys.Single(m => m.Index == monkey.TrueConditionThrowToMonkeyIndex).StartingItems.Enqueue(worryLevelAdjusted);
                    }
                    else
                    {
                        monkeys.Single(m => m.Index == monkey.FalseConditionThrowToMonkeyIndex).StartingItems.Enqueue(worryLevelAdjusted);
                    }
                }

                monkey.StartingItems.Clear();
            }
        }

        var maxTimesItems = monkeys
            .OrderByDescending(x => x.VisitedItems)
            .Take(2)
            .ToList();

        var result = maxTimesItems[0].VisitedItems * maxTimesItems[1].VisitedItems;

        ReadWriteHelpers.WriteResult(DayName, "2", result);
    }

    private List<Monkey> GetMonkeys()
    {
        var monkeys = new List<Monkey>();
        var firstMonkey = new Monkey();
        monkeys.Add(firstMonkey);

        foreach (var line in _lines)
        {
            var lastInsertedMoneky = monkeys.Last();

            switch (true)
            {
                case true when line.StartsWith("Monkey"):
                    lastInsertedMoneky.Index = GetIntFromString(line);

                    break;
                case true when line.Trim().StartsWith("Starting items: "):
                    var tokens0 = line.Split(": ");
                    var items = tokens0[1].Split(", ");

                    var tempList = new List<int>();

                    foreach (var item in items)
                    {
                        tempList.Add(Convert.ToInt32(item));
                    }

                    lastInsertedMoneky.SetStartingItems(tempList);

                    break;
                case true when line.Trim().StartsWith("Operation"):
                    var tokens1 = line.Split("new = ");
                    lastInsertedMoneky.OperationString = tokens1[1];

                    break;
                case true when line.Trim().StartsWith("Test"):
                    lastInsertedMoneky.TestCondition = GetIntFromString(line);

                    break;
                case true when line.Trim().StartsWith("If true"):
                    lastInsertedMoneky.TrueConditionThrowToMonkeyIndex = GetIntFromString(line);

                    break;
                case true when line.Trim().StartsWith("If false"):
                    lastInsertedMoneky.FalseConditionThrowToMonkeyIndex = GetIntFromString(line);

                    break;
                default:
                    break;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                var newMonkey = new Monkey();

                monkeys.Add(newMonkey);
            }
        }

        return monkeys;
    }

    private static int GetIntFromString(string input)
    {
        return Convert.ToInt16(Regex.Match(input, @"\d+").Value);
    }

    private long GetWorryLevel(string operationString, long item)
    {
        return true switch
        {
            true when operationString == "old * old" => item * item,
            true when operationString.StartsWith("old * ") => item * Convert.ToInt32(operationString.Split("* ")[1]),
            true when operationString.StartsWith("old + ") => item + Convert.ToInt32(operationString.Split("+ ")[1]),
            _ => throw new ArgumentException("Unknown operation")
        };
    }

    private class Monkey
    {
        public int Index { get; set; }
        public Queue<long> StartingItems { get; set; } = new Queue<long>();
        public string OperationString { get; set; }
        public int TestCondition { get; set; }
        public int TrueConditionThrowToMonkeyIndex { get; set; }
        public int FalseConditionThrowToMonkeyIndex { get; set; }

        public void SetStartingItems(List<int> items)
        {
            foreach (var item in items)
            {
                this.StartingItems.Enqueue(item);
            }
        }

        public long VisitedItems { get; private set; }

        public void IncreaseVisitedItems()
        {
            VisitedItems++;
        }
    }
}
