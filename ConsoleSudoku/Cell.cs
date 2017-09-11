using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    public class Cell
    {
        public Cell(int x, int y)
        {
            Position = new Tuple<int, int>(x, y);
        }

        public Tuple<int,int> Position { get; set; }

        public Elements? Digit { get; set; }

        private List<Elements> _candidates = new List<Elements>();

        public List<Elements> Candidates
        {
            get {
                if (Digit != null) {
                    //_candidates = null;
                    return null;
                }
                return _candidates;
            }
            set { _candidates = value; }
        }

        public override string ToString()
        {
            return (Digit == null) ? "   " : $" {(char)(Digit+'1')} ";
        }
    }

    public enum Elements
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
    }
}
