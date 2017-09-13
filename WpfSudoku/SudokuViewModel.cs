using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleSudoku;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace WpfSudoku
{
    public class SudokuViewModel : ViewModelBase
    {
        public SudokuViewModel(Sudoku sudoku)
        {
            this.Model = sudoku;
        }

        public Sudoku Model { get; private set; }

        private string CellCandidatsContentHelper(Cell cell)
        {
            var btnContent = string.Empty;
            foreach (Elements ele in Enum.GetValues(typeof(Elements)))
            {
                btnContent += cell.Candidates.Contains(ele)
                    ? (char)(ele + '1')
                    : ' ';
            }
            btnContent = btnContent.Substring(0, 3) + '\n' + btnContent.Substring(3, 3) + '\n' + btnContent.Substring(6, 3);
            return " " + String.Join<char>(" ", btnContent);
        }

        public ContentControl Cell00 =>
            new ContentControl()
            {
                FontFamily = new FontFamily("Consolas"),
                FontSize = Model.Cells[0].Digit != null ? 50 : 18,
                Content = Model.Cells[0].Digit != null
                    ? ((char)(Model.Cells[0].Digit + '1')).ToString()
                    : CellCandidatsContentHelper(Model.Cells[0])
            };



    }
}
