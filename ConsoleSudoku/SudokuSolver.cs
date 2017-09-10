using System;
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

        private static void LabelCandidates(this Grid grid, Cell cell)
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
                grid.LabelCandidates(cell);
            }
        }

        private static void ConfirmSingleCandidate(this Grid grid)
        {
            foreach (var cell in grid)
            {
                if (cell.Candidates != null)
                {
                    if (cell.Candidates.Count == 1)
                    {
                        cell.Digit = cell.Candidates[0];
                    }
                }
            }
            grid.LabelAllCanditates();
        }

        public static void ConfirmSingleCandidates(this Grid grid)
        {
            bool hasSingleCandidate = true;
            do
            {
                grid.ConfirmSingleCandidate();
                var unfilledCells = grid.Where(c => c.Digit == null);
                hasSingleCandidate = unfilledCells.Any(c => c.Candidates.Count == 1);
            } while (hasSingleCandidate);
        }
    }
}
