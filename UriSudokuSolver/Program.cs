//using System.Diagnostics;
//using UriSudokuSolver;


//void PrintBoard(SudokuBoard b)
//{
//    for (int i = 0; i < b.GetRows(); i++)
//    {
//        for (int j = 0; j < b.GetCols(); j++)
//        {
//            Console.Write(b[i, j] + " ");
//        }
//        Console.WriteLine();
//    }
//}

//const string GAME_TYPE = "sudoku";


//Console.WriteLine("Hello, World!");

//Stopwatch sw = new Stopwatch();

//SudokuBoard board = new SudokuBoard(9);


////board.FillBoard(new int[]{8,0,0,0,0,0,0,7,0,0,0,6,0,1,0,0,5,3,0,4,0,6,0,0,0,0,0,0,0,0,0,8,0,4,0,0,0,0,3,0,0,0,7,0,0,0,2,0,0,0,5,0,3,8,0,0,0,0,0,0,8,0,0,0,0,4,0,5,0,0,6,1,9,0,0,0,0,2,0,0,0,
////1,0,0,0,0,0,0,2,7,0,0,0,3,0,4,0,1,5,5,0,0,1,7,0,6,8,3,4,3,0,9,6,2,0,0,1,9,0,0,0,0,7,2,5,6,0,0,6,8,1,0,0,0,0,0,4,0,6,0,0,0,3,0,0,1,2,0,4,3,5,0,0,0,5,8,0,0,1,0,0,0});

//board.FillBoard("003000002080050000700800049000000100006003000900500078009060014000400200100000500");

//SudokuSolver solver = new SudokuSolver(board);

//Console.WriteLine("Solving sudoku board:");
//sw.Start();
//solver.Solve();
//sw.Stop();
//Console.WriteLine(sw.ElapsedMilliseconds);


//PrintBoard(board);
using UriSudokuSolver.UserCommunication;
const string GAME_TYPE = "sudoku";


ConsoleUserCommunication userCommunication = new ConsoleUserCommunication(GAME_TYPE);
userCommunication.Communicate();




