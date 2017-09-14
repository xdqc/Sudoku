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

            for (int i = 0; i < 20; i++)
            {
                Run();
            }



            Console.ReadLine();
        }

        static void Run()
        {
            var startTime = DateTime.Now;
            var g1 = new Sudoku();
            Console.WriteLine();
            g1.PrintGrid();
            g1.LabelAllCandidates();

            g1.SolveStage_1();

            Console.WriteLine();
            g1.PrintGrid();

            Console.WriteLine(DateTime.Now-startTime);
        }
    }
}
