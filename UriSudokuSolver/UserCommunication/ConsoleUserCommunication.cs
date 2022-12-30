using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.UserCommunication.Reader;
using UriSudokuSolver.UserCommunication.Writer;

namespace UriSudokuSolver.UserCommunication
{
    /*Class for user communication using the console.*/
    internal class ConsoleUserCommunication : IUserCommunication
    {
        //constnts for the user communication
        private const string WELCOME_MESSAGE = "Welcome to Uri's Sudoku Solver!";
        private const string INSTRUCTIONS_MESSAGE = "Please enter the sudoku board you want to solve. Use 0 for empty spaces.";

        private string gameType;
        private GameBoard<char> board;
        private IBoardReader sudokuReader;
        private IBoardWriter sudokuWriter;

        /*Constractor for user communication*/
        public ConsoleUserCommunication(string gameType)
        {
            this.gameType = gameType;
            sudokuWriter = WriterFactory.GetWriter(gameType);
        }

        /*This is the main function for communicating with the user.*/
        public void Communicate()
        {
            Console.WriteLine(WELCOME_MESSAGE);
            Console.WriteLine(INSTRUCTIONS_MESSAGE);
            string readerType = GetTheReaderTypeFromUser();

            if (readerType == "file")
            {
                sudokuReader = ReaderFactory.GetReader(readerType, gameType, GetFilePath());
            }
            else
            {
                sudokuReader = ReaderFactory.GetReader(readerType, gameType);
            }

            board = sudokuReader.ReadBoard();
            Console.WriteLine(sudokuWriter.WriteBoard(board));

        }

        /*Gets the reader type from the user.*/
        private string GetTheReaderTypeFromUser()
        {
            string readerType = "";

            Console.WriteLine("Please enter the type of input you want to use --> c for console, f for file: ");
            readerType = Console.ReadLine();

            while (readerType.ToLower() != "c" || readerType.ToLower() != "f")
            {
                Console.WriteLine("Please enter a valid input type --> c for console, f for file: ");
                readerType = Console.ReadLine();
            }
            if (readerType.ToLower() == "c")
            {
                return "console";
            }
            return "file";

        }

        /*Function gets from the user the file path to read from.*/
        private string GetFilePath()
        {
            Console.WriteLine("Please enter the path to the file you want to read from: ");
            return Console.ReadLine();

        }
    }
}
