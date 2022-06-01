using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Square
    {
        public List<int> LegalValues = new List<int>();
        public List<int> removedValues = new List<int>();
        public int Value;
        public double arrayPosition;

        public Square(int arrayPosition, List<int> legalValues = null,
            int value = 0)
        {
            if (value != 0)
            {
                Value = value;
            }
            if (legalValues != null)
            {
                this.LegalValues = legalValues;
            }
            this.arrayPosition = arrayPosition;
        }

        public void ReturnSquaresInBox(out List<int> squaresInBox)
        {
            squaresInBox = new List<int>();
            double row = ReturnRow(arrayPosition);
            double column = ReturnColumn(arrayPosition);
            double start = arrayPosition - ((row % 3) * 9);
            start = start - (column % 3);
            int temp = int.Parse(start.ToString());
            squaresInBox.Add(temp);
            squaresInBox.Add(temp+1);
            squaresInBox.Add(temp+2);
            squaresInBox.Add(temp+9);
            squaresInBox.Add(temp+10);
            squaresInBox.Add(temp + 11);
            squaresInBox.Add(temp+18);
            squaresInBox.Add(temp+19);
            squaresInBox.Add(temp+20);
            int position = int.Parse(arrayPosition.ToString());
            if (squaresInBox.Contains(position))
            {
                squaresInBox.Remove(position);
            }
        }
        public void ReturnSquaresInRow(out List<int> squaresInRow)
        {
            squaresInRow = new List<int>();
            int start = ReturnRow(arrayPosition) * 9;
            int end = start + 9;
            for (int i = start; i < end; i++)
            {
                if (i == arrayPosition)
                {
                    continue;
                }
                squaresInRow.Add(i);
            }
        }
        public void ReturnSquaresInColumn(out List<int> squaresInColumn)
        {
            squaresInColumn = new List<int>();
            int start = ReturnColumn(arrayPosition);
            int finish = 81;
            int increment = 9;
            for (int i = start; i < finish; i+=increment)
            {
                if (i == arrayPosition)
                {
                    continue;
                }
                squaresInColumn.Add(i);
            }
        }
        public static int ReturnColumn(double arrayPosition)
        {
            double row = ReturnRow(arrayPosition);
            int column = int.Parse((arrayPosition - (9 * row)).ToString());
            return column;
        }
        public static int ReturnRow(double arrayPosition)
        {
            double temp = arrayPosition / 9;
            int row = int.Parse(Math.Floor(temp).ToString());
            return row;
        }
        public void RemoveIllegalValues(SudokuProblem currentSudoku)
        {
            var adjacentSquares = ReturnAdjacentSquares();
            foreach (int i in adjacentSquares)
            {
                if (LegalValues.Contains(currentSudoku.Squares[i].Value))
                {
                    LegalValues.Remove(currentSudoku.Squares[i].Value);
                }
            }
        }
        public List<int> ReturnAdjacentSquares()
        {
            ReturnSquaresInBox(out List<int> squaresInBox);
            ReturnSquaresInColumn(out List<int> squaresInColumn);
            ReturnSquaresInRow(out List<int> squaresInRow);
            List<int> adjacentSquares = new List<int>();
            adjacentSquares.AddRange(squaresInBox);
            adjacentSquares.AddRange(squaresInColumn);
            adjacentSquares.AddRange(squaresInRow);
            adjacentSquares = adjacentSquares.Distinct().ToList();
            adjacentSquares.Sort();
            return adjacentSquares;
        }
        public List<int> MergeLegalAndRemovedValues(int currentValue = 0)
        {
            List<int> mergedValues = new List<int>();
            foreach (var item in LegalValues)
            {
                if (removedValues.Contains(item))
                {
                    continue;
                }
                else
                {
                    mergedValues.Add(item);
                }
            }
            if (mergedValues.Contains(currentValue))
            {
                mergedValues.Remove(currentValue);
            }
            return mergedValues;
        }
    }
}
