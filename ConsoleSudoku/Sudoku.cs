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

        public IEnumerable<Cell> NakedSingles =>
            this.Where(c => c.Digit == null)
                .Where(c => c.Candidates.Count == 1);

        public IEnumerable<Tuple<Elements, Cell, House>> HiddenSingles =>
            Houses.Select(h => new Tuple<House, IEnumerable<Elements>>
                               (h, h.Where(ce => ce.Digit == null)
                                    .SelectMany(ce => ce.Candidates)
                                    .GroupBy(e => e)
                                    .Where(g => g.Count() == 1)
                                    .Select(g => g.Key)))
                  .SelectMany(h => h.Item1.Where(c => c.Digit == null)
                                    .Where(c => c.Candidates.Intersect(h.Item2).Any())
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
                //house.Where(c => c.Digit == null)
                //     .Where(c => c.Candidates.Count <= 3);

                         //.Where(g => g.Count() == 3)    // Three cells with same three candies
                         //.ToList()
                         //.ForEach(g => nt.Add(new Tuple<Elements, Elements, Elements, Cell, Cell, Cell, House>
                         //(g.Key[0], g.Key[1], g.Key[2], g.ToArray()[0], g.ToArray()[1], g.ToArray()[2], house)));
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
