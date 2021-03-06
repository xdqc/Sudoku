﻿using System;
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
using ConsoleSudoku;
using System.Text.RegularExpressions;
using System.IO;

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
                btn.KeyDown += CellButton_KeyDown;
                btn.KeyDown += DisplayCell;
                btn.GotKeyboardFocus += DisplayCell;
            }

            foreach (Button btn in FindVisualChildren<Button>(DigitPanel))
            {
                btn.Click += DigitButton_Click;
            }

            foreach (Button btn in FindVisualChildren<Button>(PencilPanel))
            {
                btn.Click += PencilButton_Click;
            }

            foreach (Button btn in FindVisualChildren<Button>(MainGrid))
            {
                btn.Click += DisplayCell;
            }

            foreach (CheckBox chk in FindVisualChildren<CheckBox>(MainGrid))
            {
                chk.Checked += DisplayCell;
                chk.Unchecked += DisplayCell;
            }

            foreach (Button CmbItem in Solvingstrategy.Items)
            {
                CmbItem.Click += DisplayCell;
            }
        }



        #region CellDataBinding

        private IEnumerable<Button> GetButtons(IEnumerable<Cell> cells)
        {
            foreach (Button btn in FindVisualChildren<Button>(Matrix))
            {
                if (cells.Contains(GetCell(btn)))
                {
                    yield return btn;
                }
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

            // Write cell content onto grid buttons
            foreach (Button btn in FindVisualChildren<Button>(Matrix))
            {
                btn.Content = CellContent(GetCell(btn));
            }

            // Change button color
            if (sender.GetType() == typeof(Button))
            {
                var clicked = (Button)sender;

                // clicked==button in panal
                if (clicked.Content.ToString().Length == 1)
                {
                    var elem = (Elements)(int.Parse(clicked.Content.ToString()) - 1);
                    ResetHighlightGridDigit();
                    ResetDigitPanelColor();
                
                    if (clicked.Name[0]=='D')
                    {
                        if (DigitToBeWrite == elem)
                        {
                            HighlightDigitPanelColor(elem);
                            HighlightGridDigitColor(elem);
                        }
                        else if (CellToBeWrite?.Digit == elem)
                        {
                            HighlightGridDigitColor(elem);
                        }
                    }
                    else if (clicked.Name[0]=='P')
                    {
                        if (MarksToBeWrite == elem)
                        {
                            HighlightPencilPanelColor(elem);
                            HighlightGridDigitColor(elem);
                        }
                    }
                }

                // clicked==button in matrix: Change highligited color accordingly
                if (clicked.Name[0] == 'c')
                {
                    ResetGridColor();

                    if (GetCell(clicked).Digit != null)
                    {
                        HighlightGridDigitColor(GetCell(clicked).Digit.Value);
                    }

                    if (CellToBeWrite == GetCell(clicked))
                    {
                        clicked.Background = SystemColors.HighlightBrush;
                    }
                }
            }

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

        private void ResetHighlightGridDigit()
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
                }
            }
        }

        private void HighlightGridDigitColor(Elements ele)
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
                            btn.Background = Brushes.LightGoldenrodYellow;
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
                    btn.Background = Brushes.MistyRose;
                }
            }
        }

        private void HighlightPencilPanelColor(Elements ele)
        {
            foreach (Button btn in FindVisualChildren<Button>(PencilPanel))
            {
                if (btn.Name[7] - '1' == (int)ele)
                {
                    btn.Background = Brushes.MistyRose;
                }
            }
        }

        private void ResetDigitPanelColor()
        {
            foreach (Button btn in FindVisualChildren<Button>(DigitPanel))
            {
                btn.Background = SystemColors.InfoBrush;
            }
            foreach (Button btn in FindVisualChildren<Button>(PencilPanel))
            {
                btn.Background = SystemColors.InfoBrush;
            }
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

                if (AutoMark.IsChecked.Value)
                {
                    sudoku.EliminateCandidates();
                }

            }
            else
            {
                if (DigitToBeWrite != selectedElem)
                {
                    DigitToBeWrite = selectedElem;
                }
                else
                {
                    DigitToBeWrite = null;
                }
            }
        }


        #endregion

        #region PencilClickEvents

        private void PencilButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.Source;
            var selectedElem = (Elements)(btn.Name[7] - '1');

            DigitToBeWrite = null;
            if (CellToBeWrite != null)
            {
                if (CellToBeWrite.Digit == null)
                {
                    if (CellToBeWrite.Candidates.Contains(selectedElem))
                    {
                        CellToBeWrite.Candidates.Remove(selectedElem);
                    }
                    else
                    {
                        CellToBeWrite.Candidates.Add(selectedElem);
                    }
                }
            }
            else if (MarksToBeWrite != selectedElem)
            {
                MarksToBeWrite = selectedElem;
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
            }
            else
            {
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

                    if (AutoMark.IsChecked.Value)
                    {
                        sudoku.EliminateCandidates();
                    }
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
                    CellToBeWrite = cell;
                }


            }
        }

        private void CellButton_KeyDown(object sender, KeyEventArgs e)
        {
            var btn = (Button)sender;
            var cell = GetCell(btn);
            switch (e.Key)
            {
                case Key.Back:
                case Key.Delete:
                case Key.D0:
                case Key.NumPad0:
                    cell.Digit = null;
                    break;
                case Key.D1:
                case Key.NumPad1:
                    cell.Digit = Elements.One;
                    break;
                case Key.D2:
                case Key.NumPad2:
                    cell.Digit = Elements.Two;
                    break;
                case Key.D3:
                case Key.NumPad3:
                    cell.Digit = Elements.Three;
                    break;
                case Key.D4:
                case Key.NumPad4:
                    cell.Digit = Elements.Four;
                    break;
                case Key.D5:
                case Key.NumPad5:
                    cell.Digit = Elements.Five;
                    break;
                case Key.D6:
                case Key.NumPad6:
                    cell.Digit = Elements.Six;
                    break;
                case Key.D7:
                case Key.NumPad7:
                    cell.Digit = Elements.Seven;
                    break;
                case Key.D8:
                case Key.NumPad8:
                    cell.Digit = Elements.Eight;
                    break;
                case Key.D9:
                case Key.NumPad9:
                    cell.Digit = Elements.Nine;
                    break;
                default:
                    break;
            }
            if (AutoMark.IsChecked.Value)
            {
                sudoku.LabelAllCandidates();
            }
        }
        #endregion


        #region SaveLoadEvent
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            string path = @"..\..\..\";
            sudoku.SaveGrid(path);
            MessageBox.Show("Saved at " + Path.GetFullPath(path));
        }

        private void Btn_Load_Click(object sender, RoutedEventArgs e)
        {
            LoadInputGrid.Visibility = Visibility.Visible;
        }

        private void Btn_CancelLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadInputGrid.Visibility = Visibility.Collapsed;
            LoadInputTextBox.Text = string.Empty;
        }

        private void Btn_LoadSudoku_Click(object sender, RoutedEventArgs e)
        {
            var reg = new Regex(@"^[\d.]{81}$", RegexOptions.Singleline);
            if (reg.IsMatch(LoadInputTextBox.Text))
            {
                sudoku = new Sudoku(LoadInputTextBox.Text);
                LoadInputGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Invalid Sudoku Infomation.");
                LoadInputTextBox.Text = string.Empty;
            }
        }

        #endregion

        #region SudokuLogicEvent
        private void Btn_GenerateSudoku_Click(object sender, RoutedEventArgs e)
        {
            sudoku.GenerateSudoku((int)EmptyCellsSlider.Value);
            ResetGridColor();
        }

        private void Btn_LabelAllMarks_Click(object sender, RoutedEventArgs e)
        {
            sudoku.LabelAllCandidates();
        }

        private void AutoMark_Checked(object sender, RoutedEventArgs e)
        {
            sudoku.LabelAllCandidates();
        }

        private void AutoMark_Unchecked(object sender, RoutedEventArgs e)
        {
            sudoku.UnLabelAllCandidates();
        }

        private void Btn_FillNakedSingle_Click(object sender, RoutedEventArgs e)
        {
            sudoku.ConfirmNakedSingle();
        }

        private void Btn_FillHiddenSingle_Click(object sender, RoutedEventArgs e)
        {
            var thisButton = (Button)sender;
            thisButton.Click -= DisplayCell;
            foreach (var hiddenSingle in sudoku.HiddenSingles)
            {
                var btn = GetButtons(new Cell[] { hiddenSingle.Item2 }).FirstOrDefault();
                btn.Background = Brushes.PaleGreen;
                btn.Content = new ContentControl
                {
                    Foreground = Brushes.RosyBrown,
                    FontSize = 50,
                    Content = (int)hiddenSingle.Item1 + 1
                };
            }
        }

        private void Btn_ConfirmNakedPair_Click(object sender, RoutedEventArgs e)
        {
            var nakedPairs = sudoku.NakedPairs;
            foreach (var nakedPair in nakedPairs)
            {
                var buttons = GetButtons(new Cell[] { nakedPair.Item3, nakedPair.Item4 });
                foreach (var btn in buttons)
                {
                      btn.Background = Brushes.LawnGreen;
                }
            }
            sudoku.ConfirmNakedPairs(nakedPairs);
        }

        private void Btn_ConfirmNakedTriple_Click(object sender, RoutedEventArgs e)
        {
            var nakedTriples = sudoku.NakedTriples();
            foreach (var nt in nakedTriples)
            {
                var buttons = GetButtons(new Cell[] { nt.Item4, nt.Item5, nt.Item6 });
                foreach (var btn in buttons)
                {
                        btn.Background = Brushes.PaleGreen;
                }
            }
            sudoku.ConfirmNakedTriples(nakedTriples);
        }

        private void Btn_ConfirmNakedQuad_Click(object sender, RoutedEventArgs e)
        {
            var nakedQuads = sudoku.NakedQuads();
            foreach (var nq in nakedQuads)
            {
                var buttons = GetButtons(new Cell[] { nq.Cell1, nq.Cell2, nq.Cell3, nq.Cell4 });
                foreach (var btn in buttons)
                {
                    btn.Background = Brushes.SpringGreen;
                }
            }
            sudoku.ConfirmNakedQuads(nakedQuads);
        }

        private void Btn_ConfirmHiddenPair_Click(object sender, RoutedEventArgs e)
        {
            var thisButton = (Button)sender;
            thisButton.Click -= DisplayCell;
            foreach (var hiddenPair in sudoku.HiddenPairs())
            {
                var buttons = GetButtons(new Cell[] { hiddenPair.Item3, hiddenPair.Item4 });
                foreach (var btn in buttons)
                {
                        btn.Background = Brushes.PowderBlue;
                }
            }
        }

        private void Btn_ConfirmHiddenTriple_Click(object sender, RoutedEventArgs e)
        {
            var thisButton = (Button)sender;
            thisButton.Click -= DisplayCell;
            foreach (var hiddenTriple in sudoku.HiddenTriples())
            {
                var buttons = GetButtons(new Cell[] { hiddenTriple.Item4, hiddenTriple.Item5, hiddenTriple.Item6 });
                foreach (var btn in buttons)
                {
                    btn.Background = Brushes.CadetBlue;
                }
            }
        }

        private void Btn_ConfirmLockedCandidates_Click(object sender, RoutedEventArgs e)
        {
            ResetGridColor();
            CellToBeWrite = null;

            var lockedCandidates = sudoku.LockedCandidates_SingleLine();
            if (lockedCandidates.Count() > 1)
            {
                var elem = lockedCandidates.First().Item1;
                var cells = lockedCandidates.First().Item2;
                HighlightGridDigitColor(elem);
                foreach (var btn in GetButtons(cells))
                {
                    btn.Background = Brushes.Pink;
                }
            }
            sudoku.ConfirmLockedCandidates(lockedCandidates);
        }


        #endregion

    }
}
