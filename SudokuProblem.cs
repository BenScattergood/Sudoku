using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class SudokuProblem
    {
        //public static List<SudokuProblem> SudokuProblems = new List<SudokuProblem>();
        public string Name;
        public string[] problemArr = new string[81];
        public string[] answersArr = new string[81];
        public SudokuProblem(List<string> Lines, int position)
        {
            Name = "Problem" + position;
            int indx = 0;
            foreach (string line in Lines)
            {
                foreach (var digit in line)
                {
                    problemArr[indx] = digit.ToString();
                    indx++;
                }
            }
        }
    }
}
