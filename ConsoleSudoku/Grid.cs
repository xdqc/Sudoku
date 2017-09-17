using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public class Grid : ISet<Cell>
    {
        public Grid()
        {
            grid = new HashSet<Cell>();
            this.GenerateEmptyGrid();
        }

        public Grid(string cellInfo)
        {
            var reg = new Regex(@"^[\d.]{81}$", RegexOptions.Singleline);
            if (!reg.IsMatch(cellInfo))
            {
                throw new ArgumentException("Invalid Grid Cells Info.");
            }

            grid = new HashSet<Cell>();
            this.GenerateEmptyGrid();
            for (int i = 0; i < 80; i++)
            {
                if (cellInfo[i] != '.')
                {
                    Cells[i].Digit = (Elements)(cellInfo[i] - '1');
                }
                else
                {
                    Cells[i].Digit = null;
                }
            }
        }

        private HashSet<Cell> grid;
        private House[] rows = new Row[9];
        private House[] columns = new Column[9];
        private House[,] blocks = new Block[3, 3];
        private List<House> houses = new List<House>();

        public List<Cell> Cells => grid.ToList();

        public House[] Columns
        {
            get
            {
                if (columns[0] != null)
                {
                    return columns;
                }
                for (int c = 0; c < 9; c++)
                {
                    columns[c] = new Column(grid, c);
                }
                return columns;
            }
            set
            {
                columns = value;
            }
        }

        public House[] Rows
        {
            get
            {
                if (rows[0] != null)
                {
                    return rows;
                }
                for (int r = 0; r < 9; r++)
                {
                    rows[r] = new Row(grid, r);
                }
                return rows;
            }
            set
            {
                rows = value;
            }
        }

        public House[,] Blocks
        {
            get
            {
                if (blocks[0,0] != null)
                {
                    return blocks;
                }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        blocks[i, j] = new Block(this, i, j);
                    }
                }
                return blocks;
            }
            set
            {
                blocks = value;
            }
        }

        public List<House> Houses
        {
            get
            {
                if (houses.Count>0)
                {
                    return houses;
                }
                foreach (Row house in Rows)
                {
                    houses.Add(house);
                }
                foreach (Column house in Columns)
                {
                    houses.Add(house);
                }
                foreach (Block house in Blocks)
                {
                    houses.Add(house);
                }
                return houses;
            }
            set
            {
                houses = value;
            }
        }



        public IEnumerable<Cell> FindNeighbors(Cell c)
        {
            // return other cells that can see the cell
            return Enumerable.Concat(Rows[c.Position.Item1], Columns[c.Position.Item2])
                      .Concat(Blocks[c.Position.Item1 / 3, c.Position.Item2 / 3])
                      .Where(x => x.Position != c.Position);
        }


        #region ISet<Cell> Implementation

        public int Count => ((ISet<Cell>)grid).Count;

        public bool IsReadOnly => ((ISet<Cell>)grid).IsReadOnly;

        public bool Add(Cell item)
        {
            return ((ISet<Cell>)grid).Add(item);
        }

        public void UnionWith(IEnumerable<Cell> other)
        {
            ((ISet<Cell>)grid).UnionWith(other);
        }

        public void IntersectWith(IEnumerable<Cell> other)
        {
            ((ISet<Cell>)grid).IntersectWith(other);
        }

        public void ExceptWith(IEnumerable<Cell> other)
        {
            ((ISet<Cell>)grid).ExceptWith(other);
        }

        public void SymmetricExceptWith(IEnumerable<Cell> other)
        {
            ((ISet<Cell>)grid).SymmetricExceptWith(other);
        }

        public bool IsSubsetOf(IEnumerable<Cell> other)
        {
            return ((ISet<Cell>)grid).IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<Cell> other)
        {
            return ((ISet<Cell>)grid).IsSupersetOf(other);
        }

        public bool IsProperSupersetOf(IEnumerable<Cell> other)
        {
            return ((ISet<Cell>)grid).IsProperSupersetOf(other);
        }

        public bool IsProperSubsetOf(IEnumerable<Cell> other)
        {
            return ((ISet<Cell>)grid).IsProperSubsetOf(other);
        }

        public bool Overlaps(IEnumerable<Cell> other)
        {
            return ((ISet<Cell>)grid).Overlaps(other);
        }

        public bool SetEquals(IEnumerable<Cell> other)
        {
            return ((ISet<Cell>)grid).SetEquals(other);
        }

        void ICollection<Cell>.Add(Cell item)
        {
            ((ISet<Cell>)grid).Add(item);
        }

        public void Clear()
        {
            ((ISet<Cell>)grid).Clear();
        }

        public bool Contains(Cell item)
        {
            return ((ISet<Cell>)grid).Contains(item);
        }

        public void CopyTo(Cell[] array, int arrayIndex)
        {
            ((ISet<Cell>)grid).CopyTo(array, arrayIndex);
        }

        public bool Remove(Cell item)
        {
            return ((ISet<Cell>)grid).Remove(item);
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return ((ISet<Cell>)grid).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((ISet<Cell>)grid).GetEnumerator();
        }

        #endregion
    }
}
