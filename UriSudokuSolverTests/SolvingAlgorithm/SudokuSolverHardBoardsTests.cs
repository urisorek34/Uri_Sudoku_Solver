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

        /*Medium board 2.*/
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

        /*Medium board 3.*/
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

    }
}
