using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public static class GridManipulate
    {

        public static void RotateClockwise(this Grid grid)
        {
            foreach (var cell in grid)
            {
                var x = cell.Position.Item1;
                var y = cell.Position.Item2;
                cell.Position = new Tuple<int, int>(y, 8 - x);
            }
        }

        public static void RotateCounterClockwise(this Grid grid)
        {
            foreach (var cell in grid)
            {
                var x = cell.Position.Item1;
                var y = cell.Position.Item2;
                cell.Position = new Tuple<int, int>(8 - y, x);
            }
        }

        public static void Rotate180(this Grid grid)
        {
            grid.RotateClockwise();
            grid.RotateClockwise();
        }

        public static void MirrorHorizontal(this Grid grid)
        {
            foreach (var cell in grid)
            {
                var x = cell.Position.Item1;
                var y = cell.Position.Item2;
                cell.Position = new Tuple<int, int>(8 - x, y);
            }
        }

        public static void MirrorVertical(this Grid grid)
        {
            foreach (var cell in grid)
            {
                var x = cell.Position.Item1;
                var y = cell.Position.Item2;
                cell.Position = new Tuple<int, int>(x, 8 - y);
            }
        }

        public static void MirrorDiagonal(this Grid grid)
        {
            grid.RotateCounterClockwise();
            grid.MirrorHorizontal();
        }

        public static void MirrorAntiDiagonal(this Grid grid)
        {
            grid.RotateCounterClockwise();
            grid.MirrorVertical();
        }

        public static void PrintGrid(this Grid grid)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int ii = 3 * i; ii < 3 * i + 3; ii++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int jj = 3 * j; jj < 3 * j + 3; jj++)
                        {
                            var cell = grid.First(c => c.Position.Item1 == ii && c.Position.Item2 == jj);
                            Console.Write(cell.ToString());
                        }
                        Console.Write('|');
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("------------------------------");
            }
        }

        public static void PrintCandidates(this Grid grid)
        {
            foreach (var cell in grid)
            {
                var candidates = string.Empty;
                if (cell.Candidates != null)
                {
                    candidates = string.Concat(cell.Candidates);
                    Console.WriteLine($"{cell.Position} could be({cell.Candidates.Count}) : {candidates}");
                }
            }

        }
    }
}