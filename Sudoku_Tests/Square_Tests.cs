using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Sudoku;
using System.Collections.Generic;

namespace Sudoku_Tests
{
    [TestClass]
    public class Square_Tests
    {
        public static List<string> Lines;
        [TestInitialize]
        public void Init()
        {
            Sudoku.Sudoku.CreateSudokuProblems();
        }

        [TestMethod]
        public void ReturnSquaresInRow_Test()
        {
           Sudoku.Sudoku.SudokuProblems[1].Squares[18].ReturnSquaresInRow(out List<int> squaresInRow);
            Assert.AreEqual(21, squaresInRow[2]);
        }
        [TestMethod]
        public void ReturnSquaresInColumn_Test()
        {
            Sudoku.Sudoku.SudokuProblems[1].Squares[17].ReturnSquaresInColumn(out List<int> squaresInColumn);
            Assert.AreEqual(80, squaresInColumn[7]);
        }
        [TestMethod]
        public void ReturnSquaresInBox_Test()
        {
            Sudoku.Sudoku.SudokuProblems[1].Squares[17].ReturnSquaresInBox(out List<int> squaresInBox);
            Assert.AreEqual(24, squaresInBox[5]);
            Sudoku.Sudoku.SudokuProblems[1].Squares[59].ReturnSquaresInBox(out List<int> squaresInBox2);
            Assert.AreEqual(67, squaresInBox2[3]);
        }

        [TestMethod]
        public void ReturnAdjacentSquares_Test()
        {
            var adjacentSquares = Sudoku.Sudoku.SudokuProblems[1].Squares[17].ReturnAdjacentSquares();
            Assert.AreEqual(25, adjacentSquares[12]);
        }

        [TestMethod]
        public void RemoveIllegalValues_Test()
        {
            Sudoku.Sudoku.SudokuProblems[1].Squares[1].RemoveIllegalValues(Sudoku.Sudoku.SudokuProblems[1]);
            int temp = Sudoku.Sudoku.SudokuProblems[1].Squares[1].LegalValues.Count;
            Assert.AreEqual(5, temp);
        }

        [TestMethod]
        public void MergeLegalAndRemovedValues_Test()
        {
            var temp = Sudoku.Sudoku.SudokuProblems[1].Squares[1].MergeLegalAndRemovedValues(7);
            Assert.AreEqual(4, temp.Count);
            var temp2 = Sudoku.Sudoku.SudokuProblems[1].Squares[1].MergeLegalAndRemovedValues(6);
            Assert.AreNotEqual(4, temp2.Count);
        }
    }
}
