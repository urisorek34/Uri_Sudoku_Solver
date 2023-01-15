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

        /*Medium board 4.*/
        [TestMethod]
        public void MediumBoard4Test()
        {
            // Arrange
            string boardString = ";0?0=>010690000000710000500:?0;4000000<0400070=005<3000800000000500@000:?80>10004<30>?8;00=20000>?8;270060000000000000900000000?0000?00000>0=000?3:0000>0026000000;>61029@0<00000100<0@00:40000800500:0?;>012600800?0;0000090<0@0;07000005<00?8:00003050:4080709";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(solver.ToString(), "no solution!");
        }

        /*Medium board 5.*/
        [TestMethod]
        public void MediumBoard5Test()
        {
            // Arrange
            string boardString = "0500030000192000:000000000004100001900?000>;<00060001900?0=00>;5980100:0;57>000030@04190:00=000;>0000<309000?0=00:?0500006@0800000;060000000:?20200?;50><00@08010004020:0;0060<3<00@00000:?00000070;000<41900:0002=00007@000100400080?000>0536000<0008000=00>000";
            string expectedBoardString = ";57><36@84192=:?:?2=7>;56@<341988419=:?257>;<36@6@<31984?2=:7>;598412=:?;57>@<3636@<4198:?2=57>;>;57@<369841?2=:=:?257>;36@<84197>;56@<31984:?2=2=:?;57><36@98411984?2=:>;576@<3<36@8419=:?2;57>57>;36@<4198=:?2?2=:>;57@<3619844198:?2=7>;536@<@<3698412=:?>;57";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }

        /*Medium board 6.*/
        [TestMethod]
        public void MediumBoard6Test()
        {
            // Arrange
            string boardString = "00000000000000003000000000000?<00000<;000000000000>800000000000000000509000200000000080000000>00600@0720000=190000000000000000000000000000000000000000;:00000000000000500000080000000?<>600040001<00000000090000000000060410000000000000?0>@01000080000000000000";
            string expectedBoardString = "<9?7>:=@354;86123;@46918:=275?<>512=<;4?>986:@73:6>852371@?<9;=4>371=5@98?62;:4<25=;18?<@:947>366?:@4723<>;=1958489<:>6;53712=@?7>159@84;23:6<?=846?7=;:91<>352@@2<:365147=?>8;99=;32?<>68@547:11<3>@4:=7659?28;;752?<96=418@3>:=:498372?;>@<165?@86;1>52<:3=497";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }

        /*Medium board 7.*/
        [TestMethod]
        public void MediumBoard7Test()
        {
            // Arrange
            string boardString = "0000000004;000000000000000300000;=00000@20000000000600000000200?0500000?000007@090<7000=005@0060000000000000900300007000902001=00<090000=00000:0000000000000000600000000000000000000000080005000000400800010000;51000;00000000<00300005000000000<0000403?;70000>";
            string expectedBoardString = "37821?=9:4;>6<5@49?<;5>26@381=7:;=15:64@2?<78>39:>@6837<59=124;?>56:921?;38=<7@49?<7483=1>5@;:6212=;@><5764:9?83@8437:;69<2?>1=58<2937?>=56;4@:1=4315928>:@<7;?66@5><1:;47?2389=7;:?=@64819352><?674><8:3=19@52;51>=6;97@2:4?3<823;@?=51<8>6:947<:9824@3?;75=61>";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }

        /*Medium board 8.*/
        [TestMethod]
        public void MediumBoard8Test()
        {
            // Arrange
            string boardString = "000000008003000400090020060000079000000061200060502070008000500010000020405000003";
            string expectedBoardString = "621943758783615492594728361142879635357461289869532174238197546916354827475286913";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }


        /*Medium board 9.*/
        [TestMethod]
        public void MediumBoard9Test()
        {
            // Arrange
            string boardString = "<000509000000062000000005000?400000000=000000:000000<06070008000700000000050403050004000000000000800000000600?00000005>;04000000:;000000608000000000000000000000609>70000000004042000009000006>00020000@000008?0?0000:000@0000000001000000020000000010000=000000";
            string expectedBoardString = "<:8;5@9?431=>762@1=782:359>6?4<;946?>;=728@<5:13>532<4617:;?8@=979;:@=8<?2514>365?><49763;=821@:=84@213:<76>9?;52613?5>;@49:<=87:;?=3>@46189752<17<8=6;5>?2439:@639>7<?2=5:@1;4842@5:819;<37=6>?3=29674@:><5;8?1?>54;:281@736<9=;<:19?5=8642@37>8@7613<>9=?;:254";
            SudokuBoard board = new SudokuBoard((int)Math.Sqrt(boardString.Length));
            board.FillBoard(boardString);
            SudokuSolver solver = new SudokuSolver(board);
            //Act
            solver.Solve();
            //Assert
            Assert.AreEqual(expectedBoardString, board.ToString());
        }

        /*Medium board 10.*/
        [TestMethod]
        public void MediumBoard10Test()
        {
            // Arrange
            string boardString = "0E003000000000F000<0000000=00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000600000000000000009000000400000000000000000000000000000000000000000000000000000500000000000000000000000700000000000000000000000000000000000000000000000000000000003000000000000000000F000000000000C000000000000000000000000000004000000000000000005000000000000000000000000000000000000;00000000000000000000:000000000000000000B000000000000G00000000000000000000000000000010000000000000000000000C00000000000000000000200000000000000000000000A000";
            string expectedBoardString = "4E1235=@8D>?:GFCI9<6H7BA;F=D:CA31HI8;79B>?E@46<52G?IB9;:C47>65A<=82HGD@E3F1>HA86GB<E9243C@5;1F7=?:ID<G@7526F?;HDI1EB:=A3>8C49IF<CE328G61B4@?=5:9H7D>;AHD;@=E?BF1<6C7>3GI8A459:2GB:?8<5>;493FAI267DC1@HE=615>7=H:9AG8D2;4FBE@3C?<I9A432D@IC7:E5=H;<?>18BFG6E@I=HC<;B8F9?3:AD465G21>72895G14D6H;@>B<ECF7I?3A=::?F<D>A25@47=IGH13;B968CE7>C;BF93:GA12E6@8<=?D4I5H1634A7E?I=5C8HDG>2:9<F;B@3<2I1B;9=C7FE65D@>HGA:4?8D;=H@?1A>:3IB84<9C2E5G67FC:8G?4D53<@21>967AIFE;=HBB57F>6IG2E=:HDA?384;C9@1<A46E987H@F?G<;C:B51=2ID3>52>6IHFEDBCA@?87=;3<:1G94@9HDFIGCA?E=;:714652B><83=7G1<;:643B>952FA@?8IHEDC;CEA:98715D<643IHGB>F=2@?83?B4@>=<2IHGF19EDC:;A765";
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
