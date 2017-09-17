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

        public IEnumerable<Tuple<Elements, Elements, Elements, Elements, Cell, Cell, Cell, Cell, House>> NakedQuads()
        {
            var nq = new List<Tuple<Elements, Elements, Elements, Elements, Cell, Cell, Cell, Cell, House>>();
            foreach (var house in Houses)
            {
                var cellLeq4Candies = house.Where(c => c.Digit == null)
                                           .Where(c => c.Candidates.Count <= 4).ToList();

                startOverFindingNewNakedQuad:
                while (cellLeq4Candies.Count >= 4)
                {
                    var cell1 = cellLeq4Candies[0];
                    List<Elements> elems = new List<Elements>(cell1.Candidates);
                    for (int i = 1; i < cellLeq4Candies.Count-2; i++)
                    {
                        if (elems.Union(cellLeq4Candies[i].Candidates).Count() <= 4)
                        {
                            var cell2 = cellLeq4Candies[i];
                            elems = elems.Union(cell2.Candidates).ToList();
                            for (int j = i+1; j < cellLeq4Candies.Count-1; j++)
                            {
                                if (elems.Union(cellLeq4Candies[j].Candidates).Count() <=4)
                                {
                                    var cell3 = cellLeq4Candies[j];
                                    elems = elems.Union(cell3.Candidates).ToList();
                                    for (int k = j+1; k < cellLeq4Candies.Count; k++)
                                    {
                                        if (elems.Union(cellLeq4Candies[k].Candidates).Count() == 4)
                                        {
                                            var cell4 = cellLeq4Candies[k];
                                            elems = elems.Union(cell4.Candidates).ToList();
                                            nq.Add(new Tuple<Elements, Elements, Elements, Elements, Cell, Cell, Cell, Cell, House>
                                                (elems[0], elems[1], elems[2], elems[3], cell1, cell2, cell3, cell4, house));
                                            cellLeq4Candies.Remove(cell1);
                                            cellLeq4Candies.Remove(cell2);
                                            cellLeq4Candies.Remove(cell3);
                                            cellLeq4Candies.Remove(cell4);
                                            goto startOverFindingNewNakedQuad;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // Here we did not find any cells that can form a NakedQuad with cell1
                    cellLeq4Candies.Remove(cell1);
                }
            }

            return nq;
        }

        public IEnumerable<Tuple<Elements, Cell, House>> HiddenSingles =>
            Houses.Select(h => new Tuple<House, IEnumerable<Elements>>
                       (h, h.Where(ce => ce.Digit == null)
                            .SelectMany(ce => ce.Candidates)
                            .GroupBy(e => e)
                            .Where(g => g.Count() == 1)
                            .Select(g => g.Key)))                               // Select all unique candie Elem in a house
          .SelectMany(h => h.Item1.Where(c => c.Digit == null)
                            .Where(c => c.Candidates.Intersect(h.Item2).Any())  // Select cell whose candidate contains the Uniqe Elems
                            .Select(c => new Tuple<Elements, Cell, House>
                            (c.Candidates.Intersect(h.Item2).First(), c, h.Item1)));


        public IEnumerable<Tuple<Elements, Elements, Cell, Cell, House>> HiddenPairs()
        {
            var hp = new List<Tuple<Elements, Elements, Cell, Cell, House>>();
            foreach (var house in Houses)
            {
                var pairElems =  house.Where(c => c.Digit == null)
                                     .SelectMany(c => c.Candidates)
                                     .GroupBy(e => e)
                                     .Where(g => g.Count() == 2)
                                     .Select(g => g.Key)
                                     .ToList();

                if (pairElems.Count == 2)
                {
                    var pairCells = house.Where(c => c.Digit == null)
                                         .Where(c => c.Candidates.Intersect(pairElems).Count()==2)
                                         .ToList();
                    if (pairCells.Count == 2)
                    {
                        hp.Add(new Tuple<Elements, Elements, Cell, Cell, House>(
                            pairElems[0], pairElems[1], pairCells[0], pairCells[1], house));
                    }
                }

            }
            return hp;
        }

        public IEnumerable<Tuple<Elements, Elements, Elements, Cell, Cell, Cell, House>> HiddenTriples()
        {
            var ht = new List<Tuple<Elements, Elements, Elements, Cell, Cell, Cell, House>>();
            foreach (var house in Houses)
            {
                var tripleElems = house.Where(c => c.Digit == null)
                                       .SelectMany(c => c.Candidates)
                                       .GroupBy(e => e)
                                       .Where(g => g.Count() <= 3)
                                       .Select(g => g.Key)
                                       .ToList();

                if (tripleElems.Count == 3)
                {
                    var tripleCells = house.Where(c => c.Digit == null)
                                           .Where(c => c.Candidates.Intersect(tripleElems).Count() <= 3)
                                           .ToList();
                    if (tripleCells.Count == 3)
                    {
                        ht.Add(new Tuple<Elements, Elements, Elements, Cell, Cell, Cell, House>
                            (tripleElems[0], tripleElems[1], tripleElems[2], tripleCells[0], tripleCells[1], tripleCells[2], house));
                    }
                }
            }
            return ht;
        }
    }





    public class Tuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>
    {
        public Tuple(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9)
        {
            Elem1 = t1;
            Elem2 = t2;
            Elem3 = t3;
            Elem4 = t4;
            Cell1 = t5;
            Cell2 = t6;
            Cell3 = t7;
            Cell4 = t8;
            House = t9;
        }

        public T1 Elem1 { get; set; }
        public T2 Elem2 { get; set; }
        public T3 Elem3 { get; set; }
        public T4 Elem4 { get; set; }
        public T5 Cell1 { get; set; }
        public T6 Cell2 { get; set; }
        public T7 Cell3 { get; set; }
        public T8 Cell4 { get; set; }
        public T9 House { get; set; }
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
