using AdventOfCode.Helpers;

namespace AdventOfCode.Day09;

internal class Day9 : ISolver
{
    public string DayName => nameof(Day9).ToLower();
    private readonly string[] _lines;

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
            Row = row;
            Col = col;
            Visited.Add((row, col));
        }
    }

    private class HeadPoint : PointInMap
    {
        public TailPoint Tail { get; }

        public HeadPoint()
        {
            Tail = new TailPoint(this);
        }

        public HeadPoint(int length)
        {
            Tail = new TailPoint(this, length - 1);
        }

        public void Move(Move move)
        {
            SetPosition(Row + move.Row, Col + move.Col);
            Tail.Follow();
        }
    }

    private class TailPoint : PointInMap
    {
        public PointInMap Head { get; }
        public TailPoint? Tail { get; set; }

        public TailPoint(PointInMap parent)
        {
            Head = parent;
        }

        public TailPoint(PointInMap parent, int length) : this(parent)
        {
            if (length > 1)
            {
                Tail = new TailPoint(this, length - 1);
            }
        }

        public void Follow()
        {
            if (IsAdjacent()) return;

            int rowDist = Normalize(Head.Row - Row);
            int colDist = Normalize(Head.Col - Col);

            SetPosition(Row + rowDist, Col + colDist);
            Tail?.Follow();
        }

        private static int Normalize(int n)
        {
            if (n == 0) return 0;

            return n > 0 ? 1 : -1;
        }

        private bool IsAdjacent()
        {
            int rowDist = Math.Abs(Head.Row - Row);
            int colDist = Math.Abs(Head.Col - Col);
            int dist = rowDist + colDist;

            return dist < 2 || rowDist == 1 && colDist == 1 || rowDist == 1 && colDist == 1;
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
