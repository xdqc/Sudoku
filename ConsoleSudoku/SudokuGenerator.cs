using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public static class SudokuGenerator
    {
        const int NumberOfEmptyCells = 40;

        static Random r = new Random();

        public static void GenerateSudoku(this Grid grid)
        {
            grid.GenerateEmptyGrid();
            grid.GenerateFullGrid();
            grid.HideCells(NumberOfEmptyCells);
        }

        public static void GenerateEmptyGrid(this Grid grid)
        {
            grid.Clear();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid.Add(new Cell(i, j));
                }
            }
        }

        public static void GenerateFullGrid(this Grid grid)
        {
            int testGrid = 0;
            // Fill in grid cells with digit
            for (int i = 0; i < 9; i++)
            {
                // Fill in a row with digit
                int testRow = 0;
                for (int j = 0; j < 9; j++)
                {
                    var cell = grid.First(c => c.Position.Item1 == i && c.Position.Item2 == j);
                    // Fill a cell, keep the grid valid.
                    int test = 0;
                    do
                    {
                        cell.Digit = (Elements)r.Next(9);
                        test++;
                        testRow++;
                        testGrid++;

                        // If tried too many times, refill a row
                        if (test > 20)
                        {
                            foreach (var cellRow in grid.Rows[i])
                            {
                                cellRow.Digit = null;
                            }
                            j = -1;
                            test = 0;
                        }

                        // If tried too many times for a row, refill the grid
                        if (testRow > 600)
                        {
                            foreach (var cellGrid in grid)
                            {
                                cellGrid.Digit = null;
                            }
                            i = 0;
                            j = -1;
                            testRow = 0;
                        }
                    } while (grid.FindNeighbors(cell).Select(c => c.Digit).Contains(cell.Digit));
                }
                //Console.WriteLine($"row {i}, row tested {testRow}, grid tested {testGrid}");
            }
        }

        private static void HideCells(this Grid grid, int hideNum)
        {
            for (int i = 0; i < hideNum; i++)
            {
                var cells = grid.Where(c => c.Digit != null);
                cells.ElementAt(r.Next(cells.Count())).Digit = null;
            }
        }

    }
}
