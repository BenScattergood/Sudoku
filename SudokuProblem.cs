using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SudokuProblem
    {
        //public static List<SudokuProblem> SudokuProblems = new List<SudokuProblem>();
        public string Name;
        public string[] problemArr = new string[81];
        public string[] answersArr = new string[81];
        public string[] usersInputArr = new string[81];
        public List<Square> Squares = new List<Square>();
        public SudokuProblem(List<string> Lines, int position)
        {
            Name = "Problem" + position;
            int indx = 0;
            foreach (string line in Lines)
            {
                foreach (var digit in line)
                {
                    problemArr[indx] = digit.ToString();
                    usersInputArr[indx] = digit.ToString();
                    indx++;
                }
            }

            InitalizeSquares();
            PopulateAnswersArr();
            
        }
        public void InitalizeSquares()
        {
            int indx = 0;
            foreach (string square in problemArr)
            {
                if (square != "0")
                {
                    Squares.Add(new Square(arrayPosition: indx, 
                        value: int.Parse(square)));
                }
                else
                {
                    List<int> legalValues = new List<int>()
                    { 1,2,3,4,5,6,7,8,9};
                    Squares.Add(new Square(arrayPosition: indx, 
                        legalValues: legalValues));
                }
                indx++;
            }

            foreach (Square square in Squares)
            {
                square.RemoveIllegalValues(this);
            }
        }

        public void PopulateAnswersArr()
        {
            bool isComplete = false;
            RecursiveAlgorithm(0, ref isComplete);
            int i = -1;
            foreach (Square square in Squares)
            {
                i++;
                answersArr[i] = square.Value.ToString();
            }
        }

        public void RecursiveAlgorithm(int i, ref bool isComplete)
        {
            if (i == 81)
            {
                isComplete = true;
                return;
            }
            Square currentSquare = this.Squares[i];
            if (currentSquare.Value == 0)
            {
                foreach (var legalValue in currentSquare.MergeLegalAndRemovedValues())
                {
                    currentSquare.Value = legalValue;
                    if (isAValidSolution(currentSquare))
                    {
                        Console.WriteLine(legalValue);
                        UpdateRemovedValues(currentSquare);
                        RecursiveAlgorithm(i + 1, ref isComplete);
                        if (isComplete)
                        {
                            return;
                        }
                        RemoveRemovedValues(currentSquare);
                        
                    }
                    currentSquare.Value = 0;
                    
                }
            }
            else
            {
                RecursiveAlgorithm(i + 1, ref isComplete);
            }
            
        }

        public bool isAValidSolution(Square currentSquare)
        {
            foreach (var squarePosition in currentSquare.ReturnAdjacentSquares())
            {
                if (this.Squares[squarePosition].Value == 0)
                {
                    if (this.Squares[squarePosition].MergeLegalAndRemovedValues(currentSquare.Value).Count == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void UpdateRemovedValues(Square currentSquare)
        {
            foreach (var squarePosition in currentSquare.ReturnAdjacentSquares())
            {
                if (this.Squares[squarePosition].Value == 0)
                {
                    this.Squares[squarePosition].removedValues.Add(currentSquare.Value);
                }
            }
        }
        private void RemoveRemovedValues(Square currentSquare)
        {
            foreach (var squarePosition in currentSquare.ReturnAdjacentSquares())
            {
                if (this.Squares[squarePosition].Value == 0)
                {
                    this.Squares[squarePosition].removedValues.Remove(currentSquare.Value);
                }
            }
        }
    }
}
