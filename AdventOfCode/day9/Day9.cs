using AdventOfCode.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.day9;

internal class Day9 : ISolver
{
    public string DayName => nameof(Day9).ToLower();
    private readonly string[] _lines;
    //private readonly List<PointInMap> _tailPoints;
    //private readonly List<MoveInstruction> _moveInstructions;

    public Day9()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName);

        //_tailPoints = new List<PointInMap>()
        //{
        //    new PointInMap(0, 0)
        //};

        //_moveInstructions = new List<MoveInstruction>();

        //foreach(var line in lines)
        //{
        //    var temp = line.Split(' ');

        //    _moveInstructions.Add(new(temp[0], Convert.ToInt32(temp[1])));
        //}
    }

    public void Solve()
    {
        List<Move> moves = Move.ParseMoves(_lines);
        Exercise1(moves);
        Exercise2(moves);

        //var headPosition = new PointInMap(0, 0);
        //var tailPosition = new PointInMap(0, 0);

        //foreach (var moveInstruction in _moveInstructions)
        //{
        //    switch(moveInstruction.WhereToMove) 
        //    {
        //        case "L":
        //            UpdateTailPosition(headPosition, tailPosition, moveInstruction);

        //            break;
        //        case "R":
        //            headPosition.X += moveInstruction.NumberOfSteps;

        //            UpdateTailPosition(headPosition, tailPosition, moveInstruction);

        //            break;
        //        case "U":
        //            UpdateTailPosition(headPosition, tailPosition, moveInstruction);

        //            break;
        //        case "D":
        //            UpdateTailPosition(headPosition, tailPosition, moveInstruction);

        //            break;
        //        default: break;
        //    }
        //}


        //ReadWriteHelpers.WriteResult(DayName, "2", resultExercise2);
    }

    private void Exercise1(List<Move> moves)
    {
        HeadPoint head = new HeadPoint();

        foreach (var m in moves)
        {
            head.Move(m);
        }

        var resultExercise1 = head.Tail.Visited.Count;

        ReadWriteHelpers.WriteResult(DayName, "1", resultExercise1);
    }

    private void Exercise2(List<Move> moves)
    {
        HeadPoint head = new HeadPoint(10);

        foreach (var m in moves)
        {
            head.Move(m);
        }

        TailPoint tail = head.Tail;

        while (tail.Tail != null)
        {
            tail = tail.Tail;
        }

        var resultExercise2 = tail.Visited.Count;
        ReadWriteHelpers.WriteResult(DayName, "1", resultExercise2);
    }

    //private void UpdateTailPosition(PointInMap headPosition, PointInMap tailPosition, MoveInstruction moveInstruction)
    //{
    //    if(headPosition.Y == tailPosition.Y) // move Left of Right
    //    {
    //        if(headPosition.X - tailPosition.X > 2) // move right
    //        {
    //            for(var i = tailPosition.X; i< headPosition.X; i++)
    //            {
    //                if(!_tailPoints.Any(t => t.Y == headPosition.Y && t.X == i))
    //                {
    //                    _tailPoints.Add(new(i, headPosition.Y));
    //                }
    //            }
    //        }

    //        if()
    //    }

    //    if(headPosition.X== tailPosition.X) // move Up or Down
    //    {

    //    }

    //    //if(headPosition.X - tailPosition.X)
    //}

    private class PointInMap
    {
        public int Row { get; private set; }
        public int Col { get; private set; }
        public (int Row, int Col) Position => (Row, Col);
        public HashSet<(int, int)> Visited = new();

        public PointInMap() : this(0, 0)
        {
        }

        public PointInMap(int startRow, int startCol)
        {
            SetPosition(startRow, startCol);
        }

        public void SetPosition(int row, int col)
        {
            this.Row = row;
            this.Col = col;
            this.Visited.Add((row, col));
        }
    }

    private class HeadPoint : PointInMap
    {
        public TailPoint Tail { get; }

        public HeadPoint()
        {
            this.Tail = new TailPoint(this);
        }

        public HeadPoint(int length)
        {
            this.Tail = new TailPoint(this, length - 1);
        }

        public void Move(Move move)
        {
            this.SetPosition(this.Row + move.Row, this.Col + move.Col);
            this.Tail.Follow();
        }
    }

    private class TailPoint : PointInMap
    {
        public PointInMap Head { get; }
        public TailPoint? Tail { get; set; }

        public TailPoint(PointInMap parent)
        {
            this.Head = parent;
        }

        public TailPoint(PointInMap parent, int length) : this(parent)
        {
            if (length > 1)
            {
                this.Tail = new TailPoint(this, length - 1);
            }
        }

        public void Follow()
        {
            if (IsAdjacent()) return;

            int rowDist = Normalize(Head.Row - this.Row);
            int colDist = Normalize(Head.Col - this.Col);

            this.SetPosition(this.Row + rowDist, this.Col + colDist);
            this.Tail?.Follow();
        }

        private static int Normalize(int n)
        {
            if (n == 0) return 0;

            return n > 0 ? 1 : -1;
        }

        private bool IsAdjacent()
        {
            int rowDist = Math.Abs(Head.Row - this.Row);
            int colDist = Math.Abs(Head.Col - this.Col);
            int dist = rowDist + colDist;

            return dist < 2 || (rowDist == 1 && colDist == 1) || (rowDist == 1 && colDist == 1);
        }
    }

    private record Move(int Row, int Col)
    {
        public static List<Move> ParseMoves(string[] rows)
        {
            List<Move> moves = new();

            foreach (var row in rows)
            {
                string[] tokens = row.Split();
                int times = int.Parse(tokens[1]);

                for (var i = 0; i < times; i++)
                {
                    Move move = tokens[0] switch
                    {
                        "R" => new Move(0, 1),
                        "L" => new Move(0, -1),
                        "U" => new Move(-1, 0),
                        "D" => new Move(1, 0),
                        _ => throw new Exception()
                    };

                    moves.Add(move);
                }
            }

            return moves;
        }
    }
}
