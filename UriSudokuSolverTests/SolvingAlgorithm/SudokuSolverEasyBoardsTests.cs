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

        /*Easy board 4.*/
        [TestMethod]
        public void EasyBoard4Test()
        {
            // Arrange
            string boardString = ">000?006;00=20000@000090700:0300300;080000096?0060000=;3000000<90>40000000300000820004<007600005;300@0020>40760:700:05=080009000076?00500820<0000900000:0=03080000209>40000000;30;002@00<0>0:700@002<0000?0000=0500000@040900:00000600350182000>0090:700300;0180";
            string expectedBoardString = ">4<9?:76;35=2@182@184<9>76?:;35=35=;182@>4<96?:76?:75=;32@18>4<99>4<6?:7=;3582@182@1>4<9:76?=;35;35=@1829>4<76?:76?:35=;82@19>4<:76?;35=182@<9>4<9>476?:5=;3182@182@9>4<?:765=;3=;352@18<9>4:76?@182<9>46?:735=;5=;382@14<9>?:76?:76=;35@1824<9>4<9>:76?35=;@182";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }


        /*Easy board 5.*/
        [TestMethod]
        public void EasyBoard5Test()
        {
            // Arrange
            string boardString = "8001?796000:000007000:4000000@=1<002500;=0000000000;8@0190?000000008746000:00005000?030<;00=@0000020000010@9040000;5@9086070002<0;0000000@004000005>000@07420000968@0207000;=050000030<:0>0006002<000003000000@000:000>=0000000000>06?00040<;0006?@00000:3;0180=";
            string expectedBoardString = "8@=1?79642<:5>3;?796<:423;5>8@=1<:425>3;=18@?7965>3;8@=196?7<:42@918746?2<:3>=;5746?:32<;5>=@918:32<>=;518@9746?>=;5@9186?74:32<3;<:=15>8@9642?7=15>968@?7423;<:968@42?7<:3;=15>42?73;<:5>=1968@2<74;5:3>=186?@9;5:318>=@96?2<7418>=6?@9742<;5:36?@92<74:3;518>=";
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
