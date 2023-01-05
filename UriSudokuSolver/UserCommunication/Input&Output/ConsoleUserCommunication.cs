using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.Board;
using UriSudokuSolver.Result;
using UriSudokuSolver.SolvingAlgorithem;
using UriSudokuSolver.UserCommunication.Reader;
using UriSudokuSolver.UserCommunication.Writer;

namespace UriSudokuSolver.UserCommunication
{
    /*Class for user communication using the console.*/
    internal class ConsoleUserCommunication : IUserCommunication
    {
        //constnts for the user communication
        private const string WELCOME_MESSAGE = "Welcome to Uri's Sudoku Solver! This Program will solve you any solvable board we'll bring with a square size!";
        private const string INSTRUCTIONS_MESSAGE = "Please enter the sudoku board you want to solve. Use 0 for empty spaces.";

        private EnumConstants.GameType gameType;
        private GameBoard board;
        private IBoardReader sudokuReader;
        private IBoardWriter sudokuWriter;
        private IGameResult gameResult;

        /*Constractor for user communication*/
        public ConsoleUserCommunication(EnumConstants.GameType gameType)
        {
            this.gameType = gameType;
            sudokuWriter = WriterFactory.GetWriter(gameType);
            board = null;
            sudokuReader = null;
        }

        /*This is the main function for communicating with the user.*/
        public void Communicate()
        {
            // handling Control c pressed
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs cancel) => { Console.WriteLine("Assuming the user wants to exit... Goodbye, and thanks for all the fish:))\n"); };

            string filePath = "";
            Console.WriteLine(WELCOME_MESSAGE);
            EnumConstants.RedearType readerType = EnumConstants.RedearType.CONSOLE;
            // the main communication loop
            while (readerType != EnumConstants.RedearType.EXIT)
            {
                Console.WriteLine(INSTRUCTIONS_MESSAGE);
                readerType = GetTheReaderTypeFromUser();
                sudokuReader = null;
                board = null;
                // address the reader as the right one.
                if (readerType == EnumConstants.RedearType.FILE)
                {
                    filePath = GetFilePath();
                    sudokuReader = ReaderFactory.GetReader(readerType, gameType, filePath);

                }
                else if (readerType == EnumConstants.RedearType.CONSOLE)
                {
                    sudokuReader = ReaderFactory.GetReader(readerType, gameType);
                    Console.WriteLine("\nEnter the board: ");
                }
                else
                {
                    // if the user exits
                    Console.WriteLine("Goodbye, and thanks for all the fish! :))\n");
                    break;
                }
                ReadSudokuWithExceptionHandling();
                // if the given board wasn't valid
                if (board == null)
                {
                    Console.WriteLine("You may try again!");
                    continue;
                }

                SolveBoard(board);

            }


        }

        /*Gets the reader type from the user.*/
        private EnumConstants.RedearType GetTheReaderTypeFromUser()
        {
            string readerType = "";

            Console.WriteLine("\nPlease enter the type of input you want to use --> c for console, f for file and e to exit: ");
            readerType = Console.ReadLine();

            while (readerType.ToLower() != "c" && readerType.ToLower() != "f" && readerType.ToLower() != "e")
            {
                Console.WriteLine("\nPlease enter a valid input type --> c for console, f for file: ");
                readerType = Console.ReadLine();
            }
            // decide what to return to user
            switch (readerType.ToLower())
            {
                case "c":
                    return EnumConstants.RedearType.CONSOLE;
                case "f":
                    return EnumConstants.RedearType.FILE;
                default:
                    return EnumConstants.RedearType.EXIT;
            }


        }

        /*Function gets from the user the file path to read from.*/
        private string GetFilePath()
        {
            Console.WriteLine("Please enter the path to the file you want to read from: ");
            return Console.ReadLine();

        }

        /*Solve the board and print the time it took.*/
        private void SolveBoard(GameBoard board)
        {
            Console.WriteLine("The Board before solving: ");
            Console.WriteLine(sudokuWriter.WriteBoard(board));

            ISolver solver = SolverFactory.GetSolver(board,gameType); // need to create a genric function
            //calculating the time it took to solve
            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Solving sudoku board:");
            gameResult = ResultFactory.GetResult(gameType, solver, board);
            // Print the result
            Console.WriteLine(gameResult.GetResult());

            Console.WriteLine("The Board After solving: ");
            Console.WriteLine(sudokuWriter.WriteBoard(board));
        }

        /*Read to the sudoku board the values with exceptions handled*/
        private void ReadSudokuWithExceptionHandling()
        {
            try
            {
                board = sudokuReader.ReadBoard();
            }
            catch (BoardNotValidException boardNotValid)
            {
                Console.WriteLine(boardNotValid.Message);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File path not found. please try again.\n");
            }
            catch (IOException)
            {
                Console.WriteLine($"File path not found. please try again.\n");
            }

        }
    }



}
