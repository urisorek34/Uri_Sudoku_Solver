using Microsoft.VisualStudio.TestTools.UnitTesting;

using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver.Tests
{
    /*Board class tests.*/
    [TestClass()]
    public class SudokuBoardTests
    {
        /*Check if the board filling is correct.*/
        [TestMethod()]
        public void SudokuBoardFillTest()
        {
            string boardString = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SudokuBoard sudokuBoard = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            sudokuBoard.FillBoard(boardString);
            Assert.AreEqual(9, sudokuBoard.GetRows());
            Assert.AreEqual(9, sudokuBoard.GetCols());
        }

        /*Check if the board validation is correct (if the board legal).*/
        [TestMethod()]
        public void SudokuBoardValidateTest()
        {
            string boardString = "000000000000000010000000002030000000090090000000000000003303000000000000000000000";
            SudokuBoard sudokuBoard = new SudokuBoard(boardString.Length);
            sudokuBoard.FillBoard(boardString);
            Assert.ThrowsException<BoardNotFollowGameRulesException>(() => sudokuBoard.ValidateBoard());
        }
        
    }
}