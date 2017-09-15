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
            //this.GenerateSudoku();
        }

        public Sudoku(string cellInfo) : base(cellInfo)
        {
        }

        public IEnumerable<Cell> NakedSingles =>
            this.Where(c => c.Digit == null)
                .Where(c => c.Candidates.Count == 1);

        public IEnumerable<Tuple<Elements, Cell, House>> HiddenSingles =>
            Houses.Select(h => new Tuple<House, IEnumerable<Elements>>
                               (h, h.Where(ce => ce.Digit == null)
                                    .SelectMany(ce => ce.Candidates)
                                    .GroupBy(e => e)
                                    .Where(g => g.Count() == 1)
                                    .Select(g => g.Key)))                               // Select all unique candie Elem in a house
                  .SelectMany(h => h.Item1.Where(c => c.Digit == null)
                                    .Where(c => c.Candidates.Intersect(h.Item2).Any())  // Select cells correspond to the Elems
                                    .Select(c => new Tuple<Elements, Cell, House>
                                    (c.Candidates.Intersect(h.Item2).First(), c, h.Item1)));



        public IEnumerable<Tuple<Elements, Elements, Cell, Cell, House>> NakedPairs =>
             Houses.SelectMany(h => h.Where(c => c.Digit == null)
                                        .Where(c => c.Candidates.Count == 2)
                                        .GroupBy(c => c.Candidates, new ListEqualComparer())
                                        .Where(g => g.Count() == 2)
                                        .Select(g => new Tuple<Elements, Elements, Cell, Cell, House>
                                            (g.Key[0], g.Key[1], g.ToArray()[0], g.ToArray()[1], h)));


        public IEnumerable<Tuple<Elements, Elements, Elements, Cell, Cell, Cell, House>> NakedTriples()
        {
            var nt = new List<Tuple<Elements, Elements, Elements, Cell, Cell, Cell, House>>();
            foreach (var house in Houses)
            {
                var cellLeq3Candies = house.Where(c => c.Digit == null)
                                           .Where(c => c.Candidates.Count <= 3).ToList();

                startOverFindingNewNakedTriples:
                while (cellLeq3Candies.Count >= 3)
                {
                    var cell1 = cellLeq3Candies[0];
                    List <Elements> elems = new List<Elements>(cell1.Candidates);
                    for (int i = 1; i < cellLeq3Candies.Count-1; i++)
                    {
                        if(elems.Union(cellLeq3Candies[i].Candidates).Count() == 3)
                        {
                            var cell2 = cellLeq3Candies[i];
                            elems = elems.Union(cell2.Candidates).ToList();
                            for (int j = i+1; j < cellLeq3Candies.Count; j++)
                            {
                                if (elems.Union(cellLeq3Candies[j].Candidates).Count() == 3)
                                {
                                    var cell3 = cellLeq3Candies[j];
                                    elems = elems.Union(cell3.Candidates).ToList();
                                    nt.Add(new Tuple<Elements, Elements, Elements, Cell, Cell, Cell, House>
                                        (elems[0], elems[1], elems[2], cell1, cell2, cell3, house));
                                    cellLeq3Candies.Remove(cell1);
                                    cellLeq3Candies.Remove(cell2);
                                    cellLeq3Candies.Remove(cell3);
                                    goto startOverFindingNewNakedTriples;
                                }
                            }
                        }
                    }
                    // Here we did not find any cells that can form a NakedTriple with cell1
                    cellLeq3Candies.Remove(cell1);
                }
                
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
            return obj.Aggregate((acc, e) => acc ^ e).GetHashCode();
        }
    }

}
