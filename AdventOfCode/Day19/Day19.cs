using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day19
{
    internal class Day19 : ISolver
    {
        public string DayName => nameof(Day19).ToLower();
        private readonly string[] _lines;
        private List<Blueprint> _blueprints;
        private List<(Material Material, int MaxAmountToSpend)> _maxSpendings;

        public Day19()
        {
            _lines = ReadWriteHelpers.ReadTextFile(DayName).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            _blueprints = new List<Blueprint>();



            foreach (var line in _lines)
            {
                var blueprintTokens = line.Split(": ");
                var currentBlueprint = new Blueprint
                {
                    Id = int.Parse(blueprintTokens[0].Split(' ')[1])
                };
                var robotsTokens = blueprintTokens[1].Split(". ");

                var maxOre = 0;
                var maxClay = 0;
                var maxObsidian = 0;

                for (var i = 0; i < 4; i++)
                {
                    var robotAndCosts = new RobotAndCost((RobotType)i);

                    foreach (Match match in Regex.Matches(robotsTokens[i], @"(\d+) (\w+)"))
                    {
                        var tokens = match.Value.Split(' ');
                        var material = tokens[1] switch
                        {
                            "ore" => Material.Ore,
                            "clay" => Material.Clay,
                            "obsidian" => Material.Obsidian,
                            _ => throw new ArgumentException()
                        };

                        var cost = int.Parse(tokens[0]);
                        robotAndCosts.MaterialsAndCosts.Add(new(material, cost));

                        if (material == Material.Ore)
                        {
                            maxOre = Math.Max(maxOre, cost);
                        }
                        if (material == Material.Clay)
                        {
                            maxClay = Math.Max(maxClay, cost);
                        }
                        if (material == Material.Obsidian)
                        {
                            maxObsidian = Math.Max(maxObsidian, cost);
                        }
                    }

                    currentBlueprint.RobotsAndCosts.Add(robotAndCosts);
                }

                currentBlueprint.MaxSpent.Add(new(Material.Ore, maxOre));
                currentBlueprint.MaxSpent.Add(new(Material.Clay, maxClay));
                currentBlueprint.MaxSpent.Add(new(Material.Obsidian, maxObsidian));

                _blueprints.Add(currentBlueprint);
            }
        }

        public void Solve()
        {
            ResolveExercise1();
        }

        void ResolveExercise1()
        {
            //var totalGeods = 0;

            //for (var i = 0; i < _blueprints.Count; i++)
            //{
            //    var result = GetMaxGeods(_blueprints[i], 24);

            //    totalGeods += (_blueprints[i].Id + 1) * result;
            //}

            //ReadWriteHelpers.WriteResult(DayName, "1", totalGeods);
        }

        //int GetMaxGeods(Blueprint blueprint, int time)
        //{
        //    return GetGeodeForCurrentState(
        //        blueprint,
        //        new CurrentState(
        //            time,
        //            new List<RobotAndCost>
        //            {
        //                new RobotAndCost(RobotType.Ore)
        //                {
        //                    MaterialsAndCosts = new List<(Material Material, int Cost)>
        //                    {
        //                        new(Material.Ore, 0),
        //                        new(Material.Clay, 0),
        //                        new(Material.Obsidian, 0),
        //                        new(Material.Geode, 0),
        //                    }
        //                }
        //            },
        //            new List<RobotAndCost>
        //            {
        //                new RobotAndCost(RobotType.Ore)
        //                {
        //                    MaterialsAndCosts = new List<(Material Material, int Cost)>
        //                    {
        //                        new(Material.Ore, 1),
        //                        new(Material.Clay, 0),
        //                        new(Material.Obsidian, 0),
        //                        new(Material.Geode, 0),
        //                    }
        //                }
        //            }),
        //        new Dictionary<CurrentState, int>());
        //}

        //int GetGeodeForCurrentState(Blueprint blueprint, CurrentState state, Dictionary<CurrentState, int> cache)
        //{
        //    if(state.timeLeft == 0)
        //    {
        //        return state.available.SelectMany(x => x.MaterialsAndCosts).Single(x => x.Material == Material.Geode).Cost;
        //    }

        //    if (!cache.ContainsKey(state))
        //    {
        //        cache[state] = ()
        //    }
        //}
    }

    class Blueprint
    {
        public int Id { get; set; }
        public List<RobotAndCost> RobotsAndCosts { get; set; } = new List<RobotAndCost>();
        public List<(Material Material, int MaxAmountToSpend)> MaxSpent { get; set; } = new List<(Material Material, int MaxAmountToSpend)>();
    }

    class RobotAndCost
    {
        public RobotType RobotType { get; set; }
        public List<(Material Material, int Cost)> MaterialsAndCosts { get; set; } = new();

        public RobotAndCost(RobotType robotType)
        {
            RobotType = robotType;
        }
    }

    record CurrentState(int timeLeft, List<RobotAndCost> available, List<RobotAndCost> produced);

    enum RobotType
    {
        Ore,
        Clay,
        Obsidian,
        Geode
    }

    enum Material
    {
        Ore,
        Clay,
        Obsidian,
        Geode
    }
}
