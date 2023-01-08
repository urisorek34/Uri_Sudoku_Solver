using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver;

namespace UriSudokuSolverTests.SolvingAlgorithm
{
    /*Test class for medium boards.*/
    [TestClass]
    public class SudokuSolverMediumBoardsTests
    {
        /*Medium board 1.*/
        [TestMethod]
        public void MediumBoard1Test()
        {
            //Arrange
            string boardString = "100000027000304015500170683430962001900007256006810000040600030012043500058001000";
            string expectedBoardString = "193586427867324915524179683435962871981437256276815349749658132612743598358291764";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }

        /*Medium board 2.*/
        [TestMethod]
        public void MediumBoard2Test()
        {
            // Arrange
            string boardString = "10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<";
            string expectedBoardString = "15:2349;<@6>?=78>@8=5?7<43129:6;9<47:@618=?;35>236;?2=8>75:94@<1=4>387;:5<261?@98;76412@9:>?<35=<91:=5?634@8>2;7@?259<>31;7=:68462@>;94=?1<587:37=91?235;>8:@<46583;1:<7264@=9?>?:<4>6@8=9372;152358<>:?6794;1=@:7=<@359>8;1642?;1?968=4@25<7>3:4>6@7;12:?=3589<";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());

        }

        /*Medium board 3.*/
        [TestMethod]
        public void MediumBoard3Test()
        {
            // Arrange
            string boardString = "000000000000003085001020000000507000004000100090000000500000073002010000000040009";
            string expectedBoardString = "987654321246173985351928746128537694634892157795461832519286473472319568863745219";
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
