using AdventOfCode.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace AdventOfCode.Day13;

internal class Day13 : ISolver
{
    public string DayName => nameof(Day13).ToLower();
    private readonly string[] _lines;
    private List<Packet> _allPackets;
    private List<(Packet Left, Packet Right)> _allPacketPairs;

    public Day13()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        _allPackets = new List<Packet>();
        _allPacketPairs = new List<(Packet Left, Packet Right)>();

        InitPackets();
    }

    private void InitPackets()
    {
        foreach(var line in _lines)
        {
            _allPackets.Add(StringToPacket(line));
        }

        for (var i = 0; i < _lines.Length - 1; i = i + 2)
        {
            _allPacketPairs.Add(new(StringToPacket(_lines[i]), StringToPacket(_lines[i + 1])));
        }
    }

    public void Solve()
    {
        Exercise1();
        Exercise2();
    }

    private void Exercise1()
    {
        List<int> indexesOfRightOrderPackets = new List<int>();

        foreach (var packetPair in _allPacketPairs)
        {
            var comparison = ComparePackets(packetPair.Left, packetPair.Right);

            if (comparison == -1)
            {
                indexesOfRightOrderPackets.Add(_allPacketPairs.IndexOf(packetPair) + 1);
            }
        }

        ReadWriteHelpers.WriteResult(DayName, "1", indexesOfRightOrderPackets.Sum());
    }

    private void Exercise2()
    {
        var secondPacket = new ListPacket(new[] { new NumberPacket(2) });
        var sixthPacket = new ListPacket(new[] { new NumberPacket(6) });

        _allPackets.Add(secondPacket);
        _allPackets.Add(sixthPacket);
        _allPackets.Sort(ComparePackets);

        var secondPacketIndex = _allPackets.FindIndex(x => x == secondPacket) + 1;
        var sixthPacketIndex = _allPackets.FindIndex(x => x == sixthPacket) + 1;

        ReadWriteHelpers.WriteResult(DayName, "2", secondPacketIndex * sixthPacketIndex);
    }

    private Packet StringToPacket(string str)
    {
        var json = (JsonElement)JsonSerializer.Deserialize<object>(str)!;

        return FromJsonElement(json);
    }

    private static Packet FromJsonElement(JsonElement json)
    {
        return json.ValueKind switch
        {
            JsonValueKind.Number => new NumberPacket(json.GetInt32()),
            JsonValueKind.Array => new ListPacket(json.EnumerateArray().Select(FromJsonElement).ToArray()),
            _ => throw new ArgumentException()
        };
    }

    private int ComparePackets(Packet left, Packet right)
    {
        if (left is NumberPacket leftNunmber && right is NumberPacket rightNumber)
        {
            return leftNunmber.value.CompareTo(rightNumber.value);
        }

        var leftList = ConvertToListPacket(left);
        var rightList = ConvertToListPacket(right);
        var lowerLimit = Math.Min(leftList.Values.Length, rightList.Values.Length);

        if (leftList.Values.Length == rightList.Values.Length && lowerLimit == 0)
        {
            return 0;
        }

        for (int i = 0; i < lowerLimit; i++)
        {
            var comparisonResult = ComparePackets(leftList.Values[i], rightList.Values[i]);

            if (comparisonResult != 0)
            {
                return comparisonResult;
            }
        }

        return leftList.Values.Length.CompareTo(rightList.Values.Length);
    }

    private ListPacket ConvertToListPacket(Packet packet)
    {
        return packet switch
        {
            ListPacket listPacket => listPacket,
            NumberPacket numberPacket => new ListPacket(new Packet[] { numberPacket }),
            _ => throw new ArgumentException()
        };
    }

    private record Packet;
    private record ListPacket(Packet[] Values) : Packet;
    private record NumberPacket(int value) : Packet;
}
