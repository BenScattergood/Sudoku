using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    internal class Square
    {
        public List<string> LegalValues = new List<string>();
        public string Value;
        public double arrayPosition;

        public Square(int arrayPosition, List<string> legalValues = null,
            string value = null)
        {
            if (value != null)
            {
                Value = value;
            }
            if (legalValues != null)
            {
                this.LegalValues = legalValues;
            }
        }

        public void Init(SudokuProblem currentSudoku)
        {
            RemoveIllegalValues_PerRow(currentSudoku);
        }
        private void RemoveIllegalValues_PerRow(SudokuProblem currentSudoku)
        {
            double temp = arrayPosition / 9;
            double row = Math.Floor(temp);
            int start = int.Parse((9 * row).ToString());
            int increment = 1;
            int end = start + 9;
            RemoveIllegalValues(start, end, increment, currentSudoku);
        }
        private void RemoveIllegalValues_PerColumn(SudokuProblem currentSudoku)
        {
            double temp = arrayPosition / 9;
            double row = Math.Floor(temp);
            int start = int.Parse((arrayPosition - (9 * row)).ToString());
            int finish = 81;
            int increment = 9;
            RemoveIllegalValues(start, finish, increment, currentSudoku);
        }

        private void RemoveIllegalValues_PerBox()
        {
            //return a list of all affected square numbers, and then iterate over.
            //this can be resused later;
        }
        private void RemoveIllegalValues(int start, int end, int increment,
            SudokuProblem currentSudoku)
        {
            for (int i = start; i < end; i+=increment)
            {
                if (i == arrayPosition)
                {
                    continue;
                }
                if (LegalValues.Contains(currentSudoku.Squares[i].Value))
                {
                    LegalValues.Remove(currentSudoku.Squares[i].Value);
                }
            }
        }
    }
}
