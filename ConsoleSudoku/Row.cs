using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleSudoku
{
    public class Row : House
    {
        public Row(HashSet<Cell>grid, int n)
        {
            cells = grid.Where(c => c.Position.Item1 == n).ToList();
        }
    }
}