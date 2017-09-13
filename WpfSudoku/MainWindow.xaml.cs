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
        private static Sudoku sudoku;
        private Elements? DigitToBeWrite;
        private Elements? MarksToBeWrite;
        private Cell CellToBeWrite;

        public MainWindow()
        {
            InitializeComponent();

            var VM = new SudokuViewModel(new Sudoku());
            DataContext = VM;
            sudoku = VM.Model;
            sudoku.GenerateFullGrid();

            AddEventHandlerToControls();

        }


        //Find all controls in WPF Window by type
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void AddEventHandlerToControls()
        {

            foreach (Button btn in FindVisualChildren<Button>(Matrix))
            {
                btn.Click += CellButton_Click;
            }

            foreach (Button btn in FindVisualChildren<Button>(DigitPanel))
            {
                btn.Click += DigitButton_Click;
            }

            foreach (Button btn in FindVisualChildren<Button>(MainGrid))
            {
                btn.Click += DisplayCell;
            }
        }



        #region CellDataBinding
        private string CellContentHelper(Cell cell)
        {
            var btnContent = string.Empty;
            if (cell.Digit != null)
            {
                // Cell digit to string
                return ((char)(cell.Digit + '1')).ToString();
            }
            else
            {
                // Cell candidate to string
                foreach (Elements ele in Enum.GetValues(typeof(Elements)))
                {
                    btnContent += cell.Candidates.Contains(ele)
                        ? (char)(ele + '1')
                        : ' ';
                }
                btnContent = btnContent.Substring(0, 3) + '\n' + btnContent.Substring(3, 3) + '\n' + btnContent.Substring(6, 3);
                return " " + String.Join<char>(" ", btnContent);
            }
        }

        private ContentControl CellContent(Cell cell)
        {
            return
                new ContentControl()
                {
                    FontFamily = new FontFamily("Consolas"),
                    FontSize = cell.Digit != null ? 50 : 18,
                    Content = CellContentHelper(cell)
                };
        }

        private void DisplayCell(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in FindVisualChildren<Button>(Matrix))
            {
                    btn.Content = CellContent(GetCell(btn));
            }
        }

        private Cell GetCell(Button btn)
        {
            if (btn.Name.Length != 3)
            {
                throw new ArgumentException("Passed in Wrong button. Valid button in matrix");
            }
            var cellIdx = (btn.Name[1] - '0') * 9 + (btn.Name[2] - '0');
            return sudoku.Cells[cellIdx];
        }
        #endregion

        #region ColorSettings
        private void ResetGridColor()
        {
            foreach (WrapPanel wp in FindVisualChildren<WrapPanel>(Matrix))
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


        private void ResetGridSelectColor()
        {
            foreach (WrapPanel wp in Matrix.Children)
            {
                int wpID = wp.Name[5] - '0';
                if (wpID % 2 == 0)
                {
                    foreach (Button btn in wp.Children)
                    {
                        if (btn.Background == SystemColors.HighlightBrush)
                        {
                            btn.Background = SystemColors.ControlBrush;
                        }
                    }
                }
                else
                {
                    foreach (Button btn in wp.Children)
                    {
                        if (btn.Background == SystemColors.HighlightBrush)
                        {
                            btn.Background = SystemColors.ControlLightBrush;
                        }
                    }
                }
            }
        }

        private void ResetHighlightDigit()
        {
            foreach (WrapPanel wp in FindVisualChildren<WrapPanel>(Matrix))
            {
                int wpID = wp.Name[5] - '0';
                if (wpID % 2 == 0)
                {
                    foreach (Button btn in wp.Children)
                    {
                        if (btn.Background != SystemColors.HighlightBrush)
                        {
                            btn.Background = SystemColors.ControlBrush;
                        }
                    }
                }
                else
                {
                    foreach (Button btn in wp.Children)
                    {
                        if (btn.Background != SystemColors.HighlightBrush)
                        {
                            btn.Background = SystemColors.ControlLightBrush;
                        }
                    }

                    foreach (Button btn in FindVisualChildren<Button>(DigitPanel))
                    {

                        btn.Background = SystemColors.InfoBrush;
                    }
                }
            }
        }

        private void HighlightDigitColor(Elements ele)
        {
            foreach (Button btn in FindVisualChildren<Button>(Matrix))
            {
                if (btn.Background != SystemColors.HighlightBrush)
                {
                    var cell = GetCell(btn);
                    if (cell.Digit != null)
                    {
                        if (cell.Digit == ele)
                        {
                            btn.Background = Brushes.Peru;
                        }
                    }
                    else
                    {
                        if (cell.Candidates.Contains(ele))
                        {
                            btn.Background = Brushes.Wheat;
                        }
                    }
                }
            }
        }

        private void HighlightDigitPanelColor(Elements ele)
        {
            foreach (Button btn in FindVisualChildren<Button>(DigitPanel))
            {
                if (btn.Name[6] - '1' == (int)ele)
                {
                    btn.Background = Brushes.Peru;
                }
            }
        }
        #endregion

        #region LogicClickEvent
        private void Btn_GenerateSudoku_Click(object sender, RoutedEventArgs e)
        {
            sudoku.GenerateSudoku();
            ResetGridColor();
        }

        private void Btn_LabelAllMarks_Click(object sender, RoutedEventArgs e)
        {
            sudoku.LabelAllCanditates();
        }

        #endregion

        #region DigitClickEvents

        private void DigitButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            var selectedElem = (Elements)(btn.Name[6] - '1');

            MarksToBeWrite = null;

            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit != selectedElem)
                {
                    CellToBeWrite.Digit = selectedElem;
                }
                else
                {
                    CellToBeWrite.Digit = null;
                }
            }
            else
            {
                ResetHighlightDigit();
                if (DigitToBeWrite != selectedElem)
                {
                    DigitToBeWrite = selectedElem;
                    HighlightDigitColor(selectedElem);
                    HighlightDigitPanelColor(selectedElem);
                }
                else
                {
                    DigitToBeWrite = null;
                }
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

        private void CellButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;

            if (CellToBeWrite == GetCell(btn))
            {
                CellToBeWrite = null;
                ResetGridColor();
                ResetHighlightDigit();
            }
            else
            {
                ResetGridSelectColor();
                var cell = GetCell(btn);
                if (DigitToBeWrite != null)
                {
                    if (cell.Digit == DigitToBeWrite)
                    {
                        cell.Digit = null;
                    }
                    else
                    {
                        cell.Digit = DigitToBeWrite;
                    }
                    HighlightDigitColor(DigitToBeWrite.Value);
                }
                else if (MarksToBeWrite != null)
                {
                    if (cell.Digit == null)
                    {
                        if (cell.Candidates.Contains(MarksToBeWrite.Value))
                        {
                            cell.Candidates.Remove(MarksToBeWrite.Value);
                        }
                        else
                        {
                            cell.Candidates.Add(MarksToBeWrite.Value);
                        }
                    }
                }
                else
                {
                    btn.Background = SystemColors.HighlightBrush;
                    CellToBeWrite = cell;
                    if (cell.Digit != null)
                    {
                        ResetHighlightDigit();
                        HighlightDigitColor(cell.Digit.Value);
                    }
                }


            }
        }


        #endregion
    }
}
