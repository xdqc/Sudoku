using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public abstract class House : IEnumerable<Cell>
    {
        public List<Cell> cells = new List<Cell>();

        public bool IsFull()
        {
            return !cells.Select(c => c.Digit).Contains(null);
        }

        public bool IsLegal()
        {
            var digitsG = cells.Where(c => c.Digit != null)
                               .Select(c => c.Digit).ToList();

            return digitsG.Distinct().Count() == digitsG.Count();
        }

        public bool IsComplete()
        {
            return IsFull() && IsLegal();
        }


        public IEnumerator<Cell> GetEnumerator()
        {
            return ((IEnumerable<Cell>)cells).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Cell>)cells).GetEnumerator();
        }
    }
}
