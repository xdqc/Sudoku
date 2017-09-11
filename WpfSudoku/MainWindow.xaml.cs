using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ConsoleSudoku;
using System.Text.RegularExpressions;

namespace WpfSudoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected static Sudoku sudoku;
        private Elements? DigitToBeWrite;
        private Elements? MarksToBeWrite;
        private Cell CellToBeWrite;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            sudoku = new Sudoku();
            DisplayCells(sudoku);

        }


        private void DisplayCells(Sudoku grid)
        {
            foreach (WrapPanel wp in Matrix.Children)
            {
                foreach (Button btn in wp.Children)
                {
                    btn.Content = string.Empty;

                    var btnPos = new Tuple<int, int>(btn.Name[1]-'0', btn.Name[2]-'0');
                    var cellIdx = (btn.Name[1] - '0') * 9 + (btn.Name[2] - '0');
                    if (grid.Cells[cellIdx].Digit != null)
                    {
                        btn.FontSize = 50;
                        btn.Content = ((char)(grid.Cells[cellIdx].Digit + '1')).ToString();
                    }
                    
                }
            }
        }

        private void DisplayMarks(Sudoku grid)
        {
            foreach (WrapPanel wp in Matrix.Children)
            {
                foreach (Button btn in wp.Children)
                {
                    //btn.Content = string.Empty;

                    var cellIdx = (btn.Name[1] - '0') * 9 + (btn.Name[2] - '0');
                    if (grid.Cells[cellIdx].Digit == null)
                    {

                        btn.FontSize = 18;
                        btn.FontFamily = new FontFamily("Consolas");
                        var btnContent = string.Empty;
                        foreach (Elements ele in Enum.GetValues(typeof(Elements)))
                        {
                            btnContent += grid.Cells[cellIdx].Candidates.Contains(ele)
                                ? (char)(ele+'1')
                                : ' ';
                        }
                        btnContent = btnContent.Substring(0, 3) + '\n' + btnContent.Substring(3, 3) + '\n' + btnContent.Substring(6, 3);
                        btn.Content = " " + String.Join<char>(" ", btnContent);

                    }
                }
            }
        }

        private void ResetGridColor()
        {
            foreach (WrapPanel wp in Matrix.Children)
            {
                int wpID = wp.Name[5] - '0';
                if (wpID % 2 == 0)
                {
                    foreach (Button btn in wp.Children)
                    {
                        btn.Background = SystemColors.ControlBrush;
                    }
                }
                else
                {
                    foreach (Button btn in wp.Children)
                    {
                        btn.Background = SystemColors.ControlLightBrush;
                    }
                }
            }
        }

        private void HighlightDigitColor(Elements ele)
        {
            foreach (WrapPanel wp in Matrix.Children)
            {
                foreach (Button btn in wp.Children)
                {
                    var btnContent = btn.Content as string;
                    if (btnContent != null)
                    {
                        if (btnContent.Contains((char)(ele+'1')))
                        {
                            btn.Background = btnContent.Length ==1 ? Brushes.LightYellow: Brushes.Wheat;
                        }
                    }
                }
            }
        }


        private void Btn_GenerateSudoku_Click(object sender, RoutedEventArgs e)
        {
            sudoku.GenerateSudoku();
            DisplayCells(sudoku);
            ResetGridColor();
        }

        private void Btn_LabelAllMarks_Click(object sender, RoutedEventArgs e)
        {
            sudoku.LabelAllCanditates();
            DisplayCells(sudoku);
            DisplayMarks(sudoku);
        }

        #region DigitClickEvents

        private void Digit_1_Click(object sender, RoutedEventArgs e)
        {
            MarksToBeWrite = null;
            ResetGridColor();
            HighlightDigitColor(Elements.One);
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit != Elements.One)
                {
                    CellToBeWrite.Digit = Elements.One;
                }
                else
                {
                    CellToBeWrite.Digit = null;
                }
            }
            else if (DigitToBeWrite != Elements.One)
            {
                DigitToBeWrite = Elements.One;
            }
            else
            {
                DigitToBeWrite = null;
            }
        }

        private void Digit_2_Click(object sender, RoutedEventArgs e)
        {
            MarksToBeWrite = null;
            ResetGridColor();
            HighlightDigitColor(Elements.Two);
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit != Elements.Two)
                {
                    CellToBeWrite.Digit = Elements.Two;
                }
                else
                {
                    CellToBeWrite.Digit = null;
                }
            }
            else if (DigitToBeWrite != Elements.Two)
            {
                DigitToBeWrite = Elements.Two;
            }
            else
            {
                DigitToBeWrite = null;
            }
        }

        private void Digit_3_Click(object sender, RoutedEventArgs e)
        {
            MarksToBeWrite = null;
            ResetGridColor();
            HighlightDigitColor(Elements.Three);
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit != Elements.Three)
                {
                    CellToBeWrite.Digit = Elements.Three;
                }
                else
                {
                    CellToBeWrite.Digit = null;
                }
            }
            else if (DigitToBeWrite != Elements.Three)
            {
                DigitToBeWrite = Elements.Three;
            }
            else
            {
                DigitToBeWrite = null;
            }
        }

        private void Digit_4_Click(object sender, RoutedEventArgs e)
        {
            MarksToBeWrite = null;
            ResetGridColor();
            HighlightDigitColor(Elements.Four);
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit != Elements.Four)
                {
                    CellToBeWrite.Digit = Elements.Four;
                }
                else
                {
                    CellToBeWrite.Digit = null;
                }
            }
            else if (DigitToBeWrite != Elements.Four)
            {
                DigitToBeWrite = Elements.Four;
            }
            else
            {
                DigitToBeWrite = null;
            }
        }

        private void Digit_5_Click(object sender, RoutedEventArgs e)
        {
            MarksToBeWrite = null;
            ResetGridColor();
            HighlightDigitColor(Elements.Five);
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit != Elements.Five)
                {
                    CellToBeWrite.Digit = Elements.Five;
                }
                else
                {
                    CellToBeWrite.Digit = null;
                }
            }
            else if (DigitToBeWrite != Elements.Five)
            {
                DigitToBeWrite = Elements.Five;
            }
            else
            {
                DigitToBeWrite = null;
            }
        }

        private void Digit_6_Click(object sender, RoutedEventArgs e)
        {
            MarksToBeWrite = null;
            ResetGridColor();
            HighlightDigitColor(Elements.Six);
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit != Elements.Six)
                {
                    CellToBeWrite.Digit = Elements.Six;
                }
                else
                {
                    CellToBeWrite.Digit = null;
                }
            }
            else if (DigitToBeWrite != Elements.Six)
            {
                DigitToBeWrite = Elements.Six;
            }
            else
            {
                DigitToBeWrite = null;
            }
        }

        private void Digit_7_Click(object sender, RoutedEventArgs e)
        {
            MarksToBeWrite = null;
            ResetGridColor();
            HighlightDigitColor(Elements.Seven);
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit != Elements.Seven)
                {
                    CellToBeWrite.Digit = Elements.Seven;
                }
                else
                {
                    CellToBeWrite.Digit = null;
                }
            }
            else if (DigitToBeWrite != Elements.Seven)
            {
                DigitToBeWrite = Elements.Seven;
            }
            else
            {
                DigitToBeWrite = null;
            }
        }

        private void Digit_8_Click(object sender, RoutedEventArgs e)
        {
            MarksToBeWrite = null;
            ResetGridColor();
            HighlightDigitColor(Elements.Eight);
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit != Elements.Eight)
                {
                    CellToBeWrite.Digit = Elements.Eight;
                }
                else
                {
                    CellToBeWrite.Digit = null;
                }
            }
            else if (DigitToBeWrite != Elements.Eight)
            {
                DigitToBeWrite = Elements.Eight;
            }
            else
            {
                DigitToBeWrite = null;
            }
        }

        private void Digit_9_Click(object sender, RoutedEventArgs e)
        {
            MarksToBeWrite = null;
            ResetGridColor();
            HighlightDigitColor(Elements.Nine);
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit != Elements.Nine)
                {
                    CellToBeWrite.Digit = Elements.Nine;
                }
                else
                {
                    CellToBeWrite.Digit = null;
                }
            }
            else if (DigitToBeWrite != Elements.Nine)
            {
                DigitToBeWrite = Elements.Nine;
            }
            else
            {
                DigitToBeWrite = null;
            }
        }

        #endregion

        #region PencilClickEvents

        private void Pencil_1_Click(object sender, RoutedEventArgs e)
        {
            DigitToBeWrite = null;
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit == null)
                {
                    if (CellToBeWrite.Candidates.Contains(Elements.One))
                    {
                        CellToBeWrite.Candidates.Remove(Elements.One);
                    }
                    CellToBeWrite.Candidates.Add(Elements.One);
                }
            }
            else if (MarksToBeWrite != Elements.One)
            {
                MarksToBeWrite = Elements.One;
            }
            else
            {
                MarksToBeWrite = null;
            }
        }

        private void Pencil_2_Click(object sender, RoutedEventArgs e)
        {
            DigitToBeWrite = null;
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit == null)
                {
                    if (CellToBeWrite.Candidates.Contains(Elements.Two))
                    {
                        CellToBeWrite.Candidates.Remove(Elements.Two);
                    }
                    CellToBeWrite.Candidates.Add(Elements.Two);
                }
            }
            else if (MarksToBeWrite != Elements.Two)
            {
                MarksToBeWrite = Elements.Two;
            }
            else
            {
                MarksToBeWrite = null;
            }
        }

        private void Pencil_3_Click(object sender, RoutedEventArgs e)
        {
            DigitToBeWrite = null;
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit == null)
                {
                    if (CellToBeWrite.Candidates.Contains(Elements.Three))
                    {
                        CellToBeWrite.Candidates.Remove(Elements.Three);
                    }
                    CellToBeWrite.Candidates.Add(Elements.Three);
                }
            }
            else if (MarksToBeWrite != Elements.Three)
            {
                MarksToBeWrite = Elements.Three;
            }
            else
            {
                MarksToBeWrite = null;
            }
        }

        private void Pencil_4_Click(object sender, RoutedEventArgs e)
        {
            DigitToBeWrite = null;
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit == null)
                {
                    if (CellToBeWrite.Candidates.Contains(Elements.Four))
                    {
                        CellToBeWrite.Candidates.Remove(Elements.Four);
                    }
                    CellToBeWrite.Candidates.Add(Elements.Four);
                }
            }
            else if (MarksToBeWrite != Elements.Four)
            {
                MarksToBeWrite = Elements.Four;
            }
            else
            {
                MarksToBeWrite = null;
            }
        }

        private void Pencil_5_Click(object sender, RoutedEventArgs e)
        {
            DigitToBeWrite = null;
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit == null)
                {
                    if (CellToBeWrite.Candidates.Contains(Elements.Five))
                    {
                        CellToBeWrite.Candidates.Remove(Elements.Five);
                    }
                    CellToBeWrite.Candidates.Add(Elements.Five);
                }
            }
            else if (MarksToBeWrite != Elements.Five)
            {
                MarksToBeWrite = Elements.Five;
            }
            else
            {
                MarksToBeWrite = null;
            }
        }

        private void Pencil_6_Click(object sender, RoutedEventArgs e)
        {
            DigitToBeWrite = null;
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit == null)
                {
                    if (CellToBeWrite.Candidates.Contains(Elements.Six))
                    {
                        CellToBeWrite.Candidates.Remove(Elements.Six);
                    }
                    CellToBeWrite.Candidates.Add(Elements.Six);
                }
            }
            else if (MarksToBeWrite != Elements.Six)
            {
                MarksToBeWrite = Elements.Six;
            }
            else
            {
                MarksToBeWrite = null;
            }
        }

        private void Pencil_7_Click(object sender, RoutedEventArgs e)
        {
            DigitToBeWrite = null;
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit == null)
                {
                    if (CellToBeWrite.Candidates.Contains(Elements.Seven))
                    {
                        CellToBeWrite.Candidates.Remove(Elements.Seven);
                    }
                    CellToBeWrite.Candidates.Add(Elements.Seven);
                }
            }
            else if (MarksToBeWrite != Elements.Seven)
            {
                MarksToBeWrite = Elements.Seven;
            }
            else
            {
                MarksToBeWrite = null;
            }
        }

        private void Pencil_8_Click(object sender, RoutedEventArgs e)
        {
            DigitToBeWrite = null;
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit == null)
                {
                    if (CellToBeWrite.Candidates.Contains(Elements.Eight))
                    {
                        CellToBeWrite.Candidates.Remove(Elements.Eight);
                    }
                    CellToBeWrite.Candidates.Add(Elements.Eight);
                }
            }
            else if (MarksToBeWrite != Elements.Eight)
            {
                MarksToBeWrite = Elements.Eight;
            }
            else
            {
                MarksToBeWrite = null;
            }
        }

        private void Pencil_9_Click(object sender, RoutedEventArgs e)
        {
            DigitToBeWrite = null;
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit == null)
                {
                    if (CellToBeWrite.Candidates.Contains(Elements.Nine))
                    {
                        CellToBeWrite.Candidates.Remove(Elements.Nine);
                    }
                    CellToBeWrite.Candidates.Add(Elements.Nine);
                }
            }
            else if (MarksToBeWrite != Elements.Nine)
            {
                MarksToBeWrite = Elements.Nine;
            }
            else
            {
                MarksToBeWrite = null;
            }
        }

        #endregion

        #region CellClickEvents

        private void c00_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[0])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[0];
                ResetGridColor();
                c00.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c01_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[1])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[1];
                ResetGridColor();
                c01.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c02_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[2])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[2];
                ResetGridColor();
                c02.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c03_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[3])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[3];
                ResetGridColor();
                c03.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c04_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[4])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[4];
                ResetGridColor();
                c04.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c05_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[5])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[5];
                ResetGridColor();
                c05.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c06_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[6])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[6];
                ResetGridColor();
                c06.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c07_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[7])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[7];
                ResetGridColor();
                c07.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c08_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[8])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[8];
                ResetGridColor();
                c08.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c10_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[9])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[9];
                ResetGridColor();
                c10.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c11_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[10])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[10];
                ResetGridColor();
                c11.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c12_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[11])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[11];
                ResetGridColor();
                c12.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c13_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[12])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[12];
                ResetGridColor();
                c13.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c14_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[13])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[13];
                ResetGridColor();
                c14.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c15_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[14])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[14];
                ResetGridColor();
                c15.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c16_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[15])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[15];
                ResetGridColor();
                c16.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c17_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[16])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[16];
                ResetGridColor();
                c17.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c18_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[17])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[17];
                ResetGridColor();
                c18.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c20_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[18])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[18];
                ResetGridColor();
                c20.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c21_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[19])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[19];
                ResetGridColor();
                c21.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c22_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[20])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[20];
                ResetGridColor();
                c22.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c23_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[21])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[21];
                ResetGridColor();
                c23.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c24_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[22])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[22];
                ResetGridColor();
                c24.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c25_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[23])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[23];
                ResetGridColor();
                c25.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c26_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[24])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[24];
                ResetGridColor();
                c26.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c27_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[25])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[25];
                ResetGridColor();
                c27.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c28_Click(object sender, RoutedEventArgs e)
        {
            if (CellToBeWrite == sudoku.Cells[26])
            {
                CellToBeWrite = null;
                ResetGridColor();
            }
            else
            {
                CellToBeWrite = sudoku.Cells[26];
                ResetGridColor();
                c28.Background = SystemColors.HighlightBrush;
                if (DigitToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == DigitToBeWrite)
                    {
                        CellToBeWrite.Digit = null;
                    }
                    CellToBeWrite.Digit = DigitToBeWrite;
                }
                else if (MarksToBeWrite != null)
                {
                    if (CellToBeWrite.Digit == null)
                    {
                        if (CellToBeWrite.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            CellToBeWrite.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        CellToBeWrite.Candidates.Add(MarksToBeWrite.Value);
                    }
                }
                else
                {
                    if (CellToBeWrite.Digit != null)
                    {
                        HighlightDigitColor(CellToBeWrite.Digit.Value);
                    }
                }
            }
        }

        private void c30_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c31_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c32_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c33_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c34_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c35_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c36_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c37_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c38_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c40_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c41_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c42_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c43_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c44_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c45_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c46_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c47_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c48_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c50_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c51_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c52_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c53_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c54_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c55_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c56_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c57_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c58_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c60_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c61_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c62_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c63_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c64_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c65_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c66_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c67_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c68_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c70_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c71_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c72_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c73_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c74_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c75_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c76_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c77_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c78_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c80_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c81_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c82_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c83_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c84_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c85_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c86_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c87_Click(object sender, RoutedEventArgs e)
        {

        }

        private void c88_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
