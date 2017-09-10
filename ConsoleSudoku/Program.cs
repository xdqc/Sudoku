using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Sudoku!");
            var startTime = DateTime.Now;

            var g1 = new Sudoku();
            Console.WriteLine();
            g1.PrintGrid();

            g1.PrintCandidates();

            g1.SolveNakedSingles();
            g1.PrintGrid();
            g1.PrintCandidates();

            Console.WriteLine(DateTime.Now-startTime);
            Console.ReadLine();
        }
    }
}
