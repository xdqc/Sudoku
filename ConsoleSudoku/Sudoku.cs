using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public class Sudoku : Grid
    {
        public Sudoku()
        {
            this.GenerateSudoku();
        }
    }
}
