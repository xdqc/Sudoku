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

        // Naked Pair contains two cell and their correspond house (where they belong) 
        public IEnumerable<Tuple<Cell,Cell,House>> NakedPairs { get{
                var np = new List<Tuple<Cell, Cell, House>>();
                foreach (var house in Rows)
                {
                  house.Where(c => c.Digit == null)
                       .Where(c => c.Candidates.Count == 2)
                       .GroupBy(c => c.Candidates)
                       .Where(g => g.Count() == 2)  // Two cells with same two candies
                       .ToList()
                       .ForEach(g => np.Add(new Tuple<Cell,Cell, House>(g.ToList()[0], g.ToList()[1], house)));
                }
                foreach (var house in Columns)
                {
                  house.Where(c => c.Digit == null)
                       .Where(c => c.Candidates.Count == 2)
                       .GroupBy(c => c.Candidates)
                       .Where(g => g.Count() == 2)  // Two cells with same two candies
                       .ToList()
                       .ForEach(g => np.Add(new Tuple<Cell, Cell>(g.ToList()[0], g.ToList()[1])));
                }
                foreach (var house in Blocks)
                {
                    house.Where(c => c.Digit == null)
                         .Where(c => c.Candidates.Count == 2)
                         .GroupBy(c => c.Candidates)
                         .Where(g => g.Count() == 2)  // Two cells with same two candies
                         .ToList()
                         .ForEach(g => np.Add(new Tuple<Cell, Cell>(g.ToList()[0], g.ToList()[1])));
                }
                return np;
            } }

        public IEnumerable<Tuple<Cell, Cell, Cell>> NakedTriples
        {
            get
            {
                var nt = new List<Tuple<Cell, Cell, Cell>>();
                foreach (var house in Rows)
                {
                    house.Where(c => c.Digit == null)
                         .Where(c => c.Candidates.Count == 3)
                         .GroupBy(c => c.Candidates)
                         .Where(g => g.Count() == 3)  // Two cells with same two candies
                         .ToList()
                         .ForEach(g => nt.Add(new Tuple<Cell, Cell, Cell>(g.ToArray()[0], g.ToArray()[1], g.ToArray()[2])));
                }
                foreach (var house in Columns)
                {
                    house.Where(c => c.Digit == null)
                         .Where(c => c.Candidates.Count == 3)
                         .GroupBy(c => c.Candidates)
                         .Where(g => g.Count() == 3)  // Two cells with same two candies
                         .ToList()
                         .ForEach(g => nt.Add(new Tuple<Cell, Cell>(g.ToList()[0], g.ToList()[1])));
                }
                foreach (var house in Blocks)
                {
                    house.Where(c => c.Digit == null)
                         .Where(c => c.Candidates.Count == 3)
                         .GroupBy(c => c.Candidates)
                         .Where(g => g.Count() == 3)  // Two cells with same two candies
                         .ToList()
                         .ForEach(g => nt.Add(new Tuple<Cell, Cell>(g.ToList()[0], g.ToList()[1])));
                }
                return nt;
            }
        }
    }

}
