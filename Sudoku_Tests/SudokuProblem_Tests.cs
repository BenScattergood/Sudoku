using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;

namespace Sudoku_Tests
{
    [TestClass]
    public class SudokuProblem_Tests
    {
        [TestInitialize]
        public void Init()
        {
            Sudoku.Sudoku.CreateSudokuProblems();
        }

        [TestMethod]
        public void isAValidSolution_Test()
        {
            var problem = Sudoku.Sudoku.SudokuProblems[1];
            problem.Squares[1].Value = 1;
            bool temp = problem.isAValidSolution(problem.Squares[1]);
            Assert.IsTrue(temp);
        }
    }
}
