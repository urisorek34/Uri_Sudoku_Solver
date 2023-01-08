using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UriSudokuSolver;


namespace UriSudokuSolverTests.SolvingAlgorithm
{
    /*Testing for easy sudoku boards.*/
    [TestClass]
    public class SudokuSolverEasyBoardsTests
    {

        /*Easy board 1.*/
        [TestMethod]
        public void EasyBoard1Test()
        {
            //Arrange
            string boardString = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            string expectedBoardString = "275143869136798245849562713712835496463219578598476132654321987321987654987654321";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }

        /*Easy board 2.*/
        [TestMethod]
        public void EasyBoard2Test()
        {
            // Arrange
            string boardString = "003000002080050000700800049000000100006003000900500078009060014000400200100000500";
            string expectedBoardString = "693741852482659731715832649257984163846173925931526478579268314368415297124397586";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());

        }

        /*Easy board 3.*/
        [TestMethod]
        public void EasyBoard3Test()
        {
            // Arrange
            string boardString = "800000070006010053040600000000080400003000700020005038000000800004050061900002000";
            string expectedBoardString = "831529674796814253542637189159783426483296715627145938365471892274958361918362547";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }
            


    }
}
