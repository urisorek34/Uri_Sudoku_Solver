using UriSudokuSolver.Board;
using UriSudokuSolver.CustomExceptions;
using UriSudokuSolver.Result;
using UriSudokuSolver.SolvingAlgorithem;
using UriSudokuSolver.UserCommunication.Reader;
using UriSudokuSolver.UserCommunication.Writer;
using static UriSudokuSolver.UserCommunication.EnumConstants;

namespace UriSudokuSolver.UserCommunication
{
    /*Class for user communication using the console.*/
    internal class ConsoleUserCommunication : IUserCommunication
    {
        //constnts for the user communication
        private const string SUDOKU_MESSAGE = "  _   _ ___ ___ _      ___ _   _ ___   ___  _  ___   _   ___  ___  _ __   _____ ___ \r\n " +
                                                 "| | | | _ \\_ _( )___ / __| | | |   \\ / _ \\| |/ / | | | / __|/ _ \\| |\\ \\ / / __| _ \\\r\n" +
                                                " | |_| |   /| ||/(_-< \\__ \\ |_| | |) | (_) | ' <| |_| | \\__ \\ (_) | |_\\ V /| _||   /\r\n" +
                                                "  \\___/|_|_\\___| /__/ |___/\\___/|___/ \\___/|_|\\_\\\\___/  |___/\\___/|____\\_/ |___|_|_\\\r\n                                                                                    ";
        // Menu message
        private const string MENU_MESSAGE = "The following options are available:\r\n";
        //Menu options array
        private string[] MENUE_OPTIONS = { "Press 's' for getting a board input for the program to solve",
            "Press 'r' for the game rules",
            "Press 'm' for the menu", "Press 'e' (or ctrl c) for exit the game" };
        //Welcome message
        private const string WELCOME_MESSAGE = "Welcome to Uri's Sudoku Solver!\n" +
            "This Program will show you the result of any NXN (N has to be a square number) sudoku board in minimum time!\n" +
            "The github link for this project is: https://github.com/urisorek34/Uri_Sudoku_Solver";
        //Goodbye message
        private const string GOODBYE_MESSAGE = "Goodbye, and thanks for all the fish:))\n";
        //instraction for getting input
        private const string INSTRUCTIONS_MESSAGE = "\nPlease enter the sudoku board you want to solve as a string in one line." +
            "\nThe size (number of rows and columns) of the board has to be (square number)^2 --> 1X1,4X4,9X9,16X16,25X25." +
            "\nThe filled values must be between 1 to the size of the board (in ascii values)." +
            "\nUse 0 for empty spaces.";

        private const string BOARD_EXAMPLE = "003000002080050000700800049000000100006003000900500078009060014000400200100000500";

        //Class properties 
        private GameRules _gameRules;
        private GameType _gameType;
        private GameBoard _board;
        private IBoardReader _sudokuReader;
        private IBoardWriter _sudokuWriter;
        private IGameResult _gameResult;

        /*Constractor for user communication*/
        public ConsoleUserCommunication(GameType gameType)
        {
            _gameType = gameType;
            _gameRules = GameRules.GetRules(gameType);
            _board = null;
            _sudokuReader = null;
        }

        /*This is the main function for communicating with the user.*/
        public void Communicate()
        {
            // handling Control c pressed
            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs cancel) => { Console.WriteLine("Assuming the user wants to exit...\n" + GOODBYE_MESSAGE); };
            StartingMessage();

            ReaderType readerType = ReaderType.CONSOLE;
            GetInputFromUser(readerType);
        }
        /*Print the starting message*/
        private void StartingMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(SUDOKU_MESSAGE);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(WELCOME_MESSAGE);
            Console.ResetColor();
            Console.WriteLine("\n\nHere is the menu:");
            PrintMenu();

        }


        /*User communication main loop*/
        private void GetInputFromUser(ReaderType readerType)
        {
            UserCommand userCommand;
            // the main communication loop
            while (readerType != ReaderType.EXIT)
            {
                userCommand = GetMenuOption();
                Console.ResetColor();
                switch (userCommand)
                {
                    case UserCommand.SOLVE:
                        // if the user chose 's' option
                        readerType = GetTheReaderTypeFromUser();
                        ReadAndSolve(readerType);
                        break;
                    case UserCommand.RULES:
                        // if the user chose 'r' option
                        _gameRules.ShowRules();
                        break;
                    case UserCommand.MENU:
                        // if the user chose 'm' option
                        PrintMenu();
                        break;
                    case UserCommand.EXIT:
                        // if the user chose 'e' option
                        Console.WriteLine(GOODBYE_MESSAGE);
                        return;
                }
            }
        }

        /*Gets the reader type from the user.*/
        private ReaderType GetTheReaderTypeFromUser()
        {
            string readerType;

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
                    return ReaderType.CONSOLE;
                case "f":
                    return ReaderType.FILE;
                default:
                    return ReaderType.EXIT;
            }


        }

        /*Get chosen menu option from the user.*/
        private UserCommand GetMenuOption()
        {
            string menuOption;
            do
            {
                Console.WriteLine("\nPlease enter valid choice (press 'm' for menu): ");
                menuOption = Console.ReadLine();
            } while (menuOption != "s" && menuOption != "r" && menuOption != "m" && menuOption != "e");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            // return the user comand that was chosen.
            switch (menuOption)
            {
                case "s":
                    Console.WriteLine("\nYou chose to 'Solve' :) ");
                    return UserCommand.SOLVE;
                case "r":
                    Console.WriteLine("\nYou chose to 'Show Rules' :) ");
                    return UserCommand.RULES;
                case "m":
                    Console.WriteLine("\nYou chose to 'Show Menu' :) ");
                    return UserCommand.MENU;
                default:
                    Console.WriteLine("\nYou chose to 'Exit' :) ");
                    return UserCommand.EXIT;
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
            Console.WriteLine("\nThe Board before solving: \n");
            _sudokuWriter.WriteBoard(board);

            ISolver solver = SolverFactory.GetSolver(board, _gameType); // need to create a genric function
            //calculating the time it took to solve
            Console.WriteLine("Solving the sudoku board...\n\n\n");
            _gameResult = ResultFactory.GetResult(_gameType, solver, board);
            // Print the result
            string result = _gameResult.GetResult();
            // Paint the board green if solved, red if not solved
            if (board.IsFull())
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(result);
            Console.ResetColor();
            Console.WriteLine("The Board After solving: \n");
            _sudokuWriter.WriteBoard(board);
            
        }

        /*Read to the sudoku board the values with exceptions handled*/
        private void ReadSudokuWithExceptionHandling()
        {
            try
            {
                _board = _sudokuReader.ReadBoard();
            }
            catch (BoardNotValidException boardNotValid)
            {
                Console.WriteLine(boardNotValid.Message);
            }
            catch (IlegalFilePathInputException ileaglePath)
            {
                Console.WriteLine(ileaglePath.Message);
            }
            Console.ResetColor();

        }

        /*Function prints the menu to the user.*/
        private void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(MENU_MESSAGE);
            foreach (string option in MENUE_OPTIONS)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("#  ");
                Console.ResetColor();
                Console.WriteLine(option);
            }
        }

        /*Function reads and solves the board*/
        private void ReadAndSolve(ReaderType readerType)
        {

            ReadTheSudokuBoard(readerType);
            // if the given board wasn't valid
            if (_board == null)
            {
                Console.WriteLine("You may try again!");
            }
            else
            {
                SolveBoard(_board);
            }
        }

        /*Print an example of a board to the user.*/
        private void PrintBoardExample()
        {
            Console.WriteLine("This board:");
            Console.WriteLine(BOARD_EXAMPLE);
            Console.WriteLine("\nBecome this board:\n");
            _board = new SudokuBoard((int)Math.Sqrt(BOARD_EXAMPLE.Length));
            _board.FillBoard(BOARD_EXAMPLE);
            _sudokuWriter.WriteBoard(_board);
            _board = null;
            Console.ResetColor();
        }


        private void ReadTheSudokuBoard(ReaderType readerType)
        {
            string filePath;
            _board = null;
            // address the reader as the right one.
            if (readerType == ReaderType.FILE)
            {
                filePath = GetFilePath();
                _sudokuReader = ReaderFactory.GetReader(readerType, _gameType, filePath);
                _sudokuWriter = WriterFactory.GetWriter(readerType, filePath);

            }
            else if (readerType == ReaderType.CONSOLE)
            {
                _sudokuReader = ReaderFactory.GetReader(readerType, _gameType);
                _sudokuWriter = WriterFactory.GetWriter(readerType);
                Console.WriteLine(INSTRUCTIONS_MESSAGE);
                //Example of a board
                PrintBoardExample();

                Console.WriteLine("\nEnter the board:\n ");
            }
            else
            {
                // if the user exits
                Console.WriteLine(GOODBYE_MESSAGE);
                return;
            }
            ReadSudokuWithExceptionHandling();
        }
    }



}
