using System.Linq;
using System.Collections.Generic;

namespace ConsoleSudoku
{
    public class Column : House
    {
        public Column(HashSet<Cell> grid, int n)
        {
            cells = grid.Where(c => c.Position.Item2 == n).ToList();
        }


    }
}