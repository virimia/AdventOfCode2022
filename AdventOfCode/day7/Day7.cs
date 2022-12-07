using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.day7;

public class Day7 : ISolver
{
    private readonly string[] lines;
    public string DayName => nameof(Day7).ToLower();
    private const string _dirCommand = "dir ";
    private const string _cdCommand = "cd ";
    private const int _totalDiskSpaceAvailable = 70000000;
    private const int _unusedSpaceNeeded = 30000000;

    public Day7()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
    }

    public void Solve()
    {
        var paths = new Stack<string>();
        var pathsAndSizes = new Dictionary<string, int>();

        foreach(var line in lines)
        {
            var splitedLine = line.Split(' ');

            if (splitedLine[1] == "cd")
            {
                if (splitedLine[2] == "..")
                {
                    paths.Pop();
                }
                else
                {
                    paths.Push(splitedLine[2]);
                }
            }
            else if (splitedLine[1] == "ls" || splitedLine[0] == "dir")
            {
                continue;
            }
            else
            {
                var size = Convert.ToInt32(splitedLine[0]);

                var tempList = new List<string>();
                tempList.AddRange(paths);
                tempList.Reverse();

                for (int i=0; i< tempList.Count; i++)
                {
                    var range = tempList.GetRange(0, i + 1);
                    var currentKey = string.Join('/', range);

                    if (!pathsAndSizes.ContainsKey(currentKey))
                    {
                        pathsAndSizes.Add(currentKey, size);
                    }
                    else
                    {
                        pathsAndSizes[currentKey] += size;
                    }
                }
            }
        }

        var resultExercise1 = 0;
        var resultExercise2 = _totalDiskSpaceAvailable;
        var totalUsed = pathsAndSizes.First().Value; // root
        var unusedSpace = _totalDiskSpaceAvailable - totalUsed;
        var sizeRequiredToDelete = _unusedSpaceNeeded - unusedSpace;
        
        foreach (var item in pathsAndSizes)
        {
            if(item.Value <= 100000)
            {
                resultExercise1 += item.Value;
            }

            if (item.Value >= sizeRequiredToDelete)
            {
                resultExercise2 = Math.Min(item.Value, resultExercise2);
            }
        }

        ReadWriteHelpers.WriteResult(DayName, "1", resultExercise1);
        ReadWriteHelpers.WriteResult(DayName, "2", resultExercise2);
    }
}
