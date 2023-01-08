using Microsoft.VisualStudio.TestTools.UnitTesting;
using UriSudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.Tests
{
    /*SolvingAlgorithm tests.*/
    [TestClass()]
    public class SudokuSolverTests
    {
        /*Test 1X1 board.*/
        [TestMethod()]
        public void OneDigitTest()
        {
            string boardString = "0";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual("a valid solution!", solver.ToString());
            Assert.AreEqual("1", board.ToString());
        }

        /*Test 4X4 board.*/
        [TestMethod()]
        public void FourOnFourTest()
        {
            string boardString = "0000300040020000";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual("a valid solution!", solver.ToString());
        }

        /*Test 9X9 board.*/
        [TestMethod()]
        public void NineOnNineTest()
        {
            string boardString = "100000027000304015500170683430962001900007256006810000040600030012043500058001000";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual("a valid solution!", solver.ToString());
        }

        /*Test 16X16 board.*/
        [TestMethod()]
        public void SixteenOnSixteenTest()
        {
            string boardString = "10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);

            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual("a valid solution!", solver.ToString());

        }

        /*Test 25X25 board.*/
        [TestMethod()]
        public void TwentyfiveOnTwentyfiveTest()
        {
            string boardString = "0E003000000000F000<0000000=00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000600000000000000009000000400000000000000000000000000000000000000000000000000000500000000000000000000000700000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B000000000000000000000000000000000000000000000000000000000000000000C00000000000000000000000000000000000000000000A000";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual("a valid solution!", solver.ToString());

        }

        /*Test no solution board.*/

        [TestMethod()]
        public void NoSolutionTest()
        {
            string boardString = "000005080000601043000000000010500000000106000300000005530000061000000004000000000";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual("no solution!", solver.ToString());
        }

        /*Test 4X4 board witgh only 0's.*/
        [TestMethod()]
        public void FourOnFourZeroTest()
        {
            string boardString = "0000000000000000";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            SudokuSolver solver = new SudokuSolver(board);
            board.FillBoard(boardString);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual("a valid solution!", solver.ToString());
        }

        /*Test 9X9 board with only 0's.*/
        [TestMethod()]
        public void NineOnNineZeroTest() 
        {
            string boardString = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            SudokuSolver solver = new SudokuSolver(board);
            board.FillBoard(boardString);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual("a valid solution!", solver.ToString());
        }

        /*Test 16X16 board with only 0's.*/
        [TestMethod()]
        public void SixteenOnSixteenZeroTest()
        {
            string boardString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            SudokuSolver solver = new SudokuSolver(board);
            board.FillBoard(boardString);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual("a valid solution!", solver.ToString());
        }


        /*Test 25X25 board with only 0's.*/
        [TestMethod()]
        public void TwentyfiveOnTwentyfiveZeroTest()
        {
            string boardString = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            SudokuSolver solver = new SudokuSolver(board);
            board.FillBoard(boardString);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual("a valid solution!", solver.ToString());

        }

        /*Test filled board.*/
        [TestMethod()]
        public void FilledBoardTest()
        {
            string boardString = "123456789456789123789123456234567891567891234891234567345678912678912345912345678";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual("a valid solution!", solver.ToString());
        }

    }
}