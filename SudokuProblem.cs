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
        internal List<Square> Squares = new List<Square>();
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

            InitalizeSquares();
        }

        public void InitalizeSquares()
        {
            answersArr = problemArr;
            int indx = 0;
            foreach (string square in answersArr)
            {
                if (square != "0")
                {
                    Squares.Add(new Square(arrayPosition: indx, 
                        value: square));
                }
                else
                {
                    List<string> legalValues = new List<string>()
                    { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    Squares.Add(new Square(arrayPosition: indx, 
                        legalValues: legalValues));
                }
                indx++;
            }

            foreach (Square square in Squares)
            {
                square.Init(this);
            }
        }
    }
}
