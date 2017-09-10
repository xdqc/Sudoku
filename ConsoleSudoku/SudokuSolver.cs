﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public static class SudokuSolver
    {
        public static bool IsSolved(this Grid grid)
        {
            var rowSolved = grid.Rows.All(r => r.IsComplete());
            var colSolved = grid.Columns.All(c => c.IsComplete());
            var blockSolved = true;
            foreach (var block in grid.Blocks)
            {
                blockSolved = blockSolved && block.IsComplete();
            }
            return rowSolved && colSolved && blockSolved;
        }

        public static void SolveStage_1(this Sudoku grid)
        {
            while (grid.NakedSingles.Count()>0 && !grid.IsSolved())
            {
                grid.SolveNakedSingles();
                grid.ConfirmNakedPairs();
                grid.ConfirmNakedTriples();

            }
        }

        #region SoverHelper

        private static void LabelCandidatesOneCell(this Grid grid, Cell cell)
        {
            // To all unfilled cells
            if (cell.Candidates != null)
            {
                cell.Candidates.Clear();
                var neighbors = grid.FindNeighbors(cell).Select(c => c.Digit);
                // If all neigbbors don't contain such element, add it to canditate of the cell
                foreach (Elements ele in Enum.GetValues(typeof(Elements)))
                {
                    if (!neighbors.Contains(ele))
                    {
                        cell.Candidates.Add(ele);
                    }
                }
            }
        }

        public static void LabelAllCanditates(this Grid grid)
        {
            foreach (var cell in grid)
            {
                grid.LabelCandidatesOneCell(cell);
            }
        }

        private static void ConfirmNakedSingle(this Sudoku grid)
        {
            foreach (var cell in grid.NakedSingles)
            {
                cell.Digit = cell.Candidates[0];
                // remove the ele out of all neighbor' candidates 
                grid.FindNeighbors(cell)
                    .Where(c => c.Digit == null)
                    .Where(c => c.Candidates.Contains(cell.Digit.Value))
                    .ToList()
                    .ForEach(c => c.Candidates.Remove(cell.Digit.Value));
            }
        }

        public static void SolveNakedSingles(this Sudoku grid)
        {
            bool hasSingleCandidate = true;
            do
            {
                grid.ConfirmNakedSingle();
                var unfilledCells = grid.Where(c => c.Digit == null);
                hasSingleCandidate = unfilledCells.Any(c => c.Candidates.Count == 1);
            } while (hasSingleCandidate);
        }

        public static void ConfirmNakedPairs(this Sudoku grid)
        {
            foreach (var np in grid.NakedPairs())
            {
                var house = np.Item5;
                var cell1 = np.Item3;
                var cell2 = np.Item4;
                var elem1 = np.Item1;
                var elem2 = np.Item2;
                // candidates of the naked pair could have been eliminated by other naked pair in other house

                house.Where(c => c.Digit == null)
                     .Where(c => c != cell1 && c != cell2)
                     .ToList()
                     .ForEach(c => c.Candidates.RemoveAll(e => e == elem1 || e == elem2));

            }
        }

        public static void ConfirmNakedTriples(this Sudoku grid)
        {
            foreach (var nt in grid.NakedTriples())
            {
                var house = nt.Item7;
                var cell1 = nt.Item4;
                var cell2 = nt.Item5;
                var cell3 = nt.Item6;
                var elem1 = nt.Item1;
                var elem2 = nt.Item2;
                var elem3 = nt.Item3;
                house.Where(c => c.Digit == null)
                     .Where(c => c != cell1 && c != cell2 && c != cell3)
                     .ToList()
                     .ForEach(c => c.Candidates.RemoveAll(e => e == elem1 || e == elem2 || e == elem3));

            }
        }
        #endregion
    }
}
