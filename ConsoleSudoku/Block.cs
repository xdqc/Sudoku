using System.Linq;
using System.Collections.Generic;

namespace ConsoleSudoku
{
    public class Block : House
    {
        public Block(Grid grid, int m, int n)
        {
            cells = grid.Where(c => c.Position.Item1 >= 3 * m && c.Position.Item1 < 3 * m + 3)
                        .Where(c => c.Position.Item2 >= 3 * n && c.Position.Item2 < 3 * n + 3)
                        .ToList();
        }
    }
}