using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver;

namespace UriSudokuSolverTests.SolvingAlgorithm
{
    /*Test class for the hard boards.*/
    [TestClass]
    public class SudokuSolverHardBoardsTests
    {
        /*hard board 1.*/
        [TestMethod]
        public void HardBoard1Test()
        {
            //Arrange
            string boardString = "800000000003600000070090200050007000000045700000100030001000068008500010090000400";
            string expectedBoardString = "812753649943682175675491283154237896369845721287169534521974368438526917796318452";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }

        /*hard board 2.*/
        [TestMethod]
        public void HardBoard2Test()
        {
            // Arrange
            string boardString = "0E003000000000F000<0000000=00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000600000000000000009000000400000000000000000000000000000000000000000000000000000500000000000000000000000700000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000B000000000000000000000000000000000000000000000000000000000000000000C00000000000000000000000000000000000000000000A000";
            string expectedBoardString = "8E763=I9D>AH?GF524<1:CB;@H=GFD<724E@I8CB>;3:651?A9C>B@?1:83A;74=5FEDI92G<6H5;421GF6?@:9<EDCBAH8>3=7IAI<:9C5BH;1326>@?=G748FED7AC3H86124?G@F;D=E9IB<>:5IB9G:FE@53=2D>7A<C8H641?;D@>=FH<C;78413:?6B5GA29IE;26E8AD>9?5<CBI4:@13=7HGF<?154BG:I=E69AH;7>2FD@8C369?D=@34B:71>;<IAGF5EH28C>5;CI2?E=GF:693H8<7@1BD4AF<317>8HAD2=GICB4;E?965@:4G2AE715C9B8H@?63:D><;IF=@H:8B6;<FI4A5DE219C=3?G>7?8A95;2DGH3C74=EIFB<@:61>G:@4<5BF61>DI82=H7A;C9E3?13I7C4=?>8<BEHA9G6@:F5;D22FHB>39IEC6;:1@8D5?47=A<G=DE;6:A7@<9?F5G3C1>28I4HB31=<2DCA8BH@;:9GFI6E?>754:75>;94312IEA?8<@H=DGFCB6968IAE@=<5GFB74:>?;CHD321BCFHG?>;:6D53217984AIE@=<E4D?@IHG7FC>=<61523B;A:98";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());

        }

        /*hard board 3.*/
        [TestMethod]
        public void HardBoard3Test()
        {
            // Arrange
            string boardString = "000005080000601043000000000010500000000106000300000005530000061000000004000000000";
            string expectedBoardString = "000005080000601043000000000010500000000106000300000005530000061000000004000000000";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }

        /*hard board 4.*/
        [TestMethod]
        public void HardBoard4Test()
        {
            // Arrange
            string boardString = "030000;062<000@0001;:620@80>700400020@8900035=109>0030000;0=0000=000620080003?05:00008000030000<0@000450;<0000090?4500000900>@0004000<:10>600870000000060008040=0200070050?010<0@873050?0:000200;<060000300000=020>070?0000500:60000501000;020000001006;00098030";
            string expectedBoardString = "73?4=1;562<:9>@85=1;:62<@89>73?4<:62>@89?4735=1;9>@83?471;5=<:62=1;<629:87>@3?45:629@87>453?=1;<>@87?453;<=1:6293?451;<=29:6>@87?45=;<:19>62@8731;<:29>673@8?45=629>873@5=?41;<:@87345=?<:1;629>;<:69>@23?8745=129>@73?8=145;<:6873?5=14:6;<29>@45=1<:6;>@29873?";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }

        /*hard board 5.*/
        [TestMethod]
        public void HardBoard5Test()
        {
            // Arrange
            string boardString = "0000:=000000000?70050;01:00@90<8900800700004600=60:=080000070002=00030890>?500012;01@:000008007>00001000@0000900000<>?0740000000006@900000>0100002;0600=800<00500070002000000000<0900>?5;4020=0@0020=0@0<0907000>?500400=6000<803<0000002001@:0000=68000?0004020";
            string expectedBoardString = ";412:=6@3<8957>?7>?52;41:=6@93<893<8?57>12;46@:=6@:=<893>?57;412=6@:3<897>?52;412;41@:=693<8?57>57>?12;4@:=6893<893<>?57412;=6@::=6@93<857>?12;412;46@:=893<>?57?57>412;6@:=<893<8937>?5;412:=6@412;=6@:<8937>?5>?57;412=6@:3<893<8957>?2;41@:=6@:=6893<?57>412;";
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
