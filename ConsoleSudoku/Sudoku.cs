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

        public IEnumerable<Cell> NakedSingles =>
            this.Where(c => c.Digit == null)
                .Where(c => c.Candidates.Count == 1);

        public IEnumerable<Tuple<Elements, Elements, Cell, Cell, House>> NakedPairs()
        {
            var np = new List<Tuple<Elements, Elements, Cell, Cell, House>>();
            foreach (var house in Houses)
            {
                house.Where(c => c.Digit == null)
                        .Where(c => c.Candidates.Count == 2)
                        .GroupBy(c => c.Candidates, new ListEqualComparer())
                        .Where(g => g.Count() == 2)     // Two cells with same two candies
                        .ToList()
                        .ForEach(g => np.Add(new Tuple<Elements, Elements, Cell, Cell, House>
                            (g.Key[0], g.Key[1], g.ToArray()[0], g.ToArray()[1], house)));

            }
            return np;
        }

        public IEnumerable<Tuple<Elements, Elements, Elements, Cell, Cell, Cell, House>> NakedTriples()
        {
                var nt = new List<Tuple<Elements, Elements, Elements, Cell, Cell, Cell, House>>();
                foreach (var house in Houses)
                {
                    house.Where(c => c.Digit == null)
                         .Where(c => c.Candidates.Count == 3)
                         .GroupBy(c => c.Candidates, new ListEqualComparer())
                         .Where(g => g.Count() == 3)    // Three cells with same three candies
                         .ToList()
                         .ForEach(g => nt.Add(new Tuple<Elements, Elements, Elements, Cell, Cell, Cell, House>
                         (g.Key[0], g.Key[1], g.Key[2], g.ToArray()[0], g.ToArray()[1], g.ToArray()[2], house)));
                }
                return nt;
        }


    }

    public class ListEqualComparer : IEqualityComparer<List<Elements>>
    {
        public bool Equals(List<Elements> x, List<Elements> y)
        {
            return x.SequenceEqual(y);
        }

        public int GetHashCode(List<Elements> obj)
        {
            return obj.GetHashCode();
        }
    }

}
