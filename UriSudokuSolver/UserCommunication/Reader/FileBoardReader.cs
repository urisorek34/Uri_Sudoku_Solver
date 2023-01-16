using UriSudokuSolver.CustomExceptions;
using UriSudokuSolver.UserCommunication.Validation;

namespace UriSudokuSolver.UserCommunication.Reader
{
    /*Class reads a board form a file.*/
    public class FileBoardReader : IBoardReader
    {
        private string filePath;
        private IValidator validator;
        private EnumConstants.GameType boardType;
        private const string FILE_FORMAT = ".txt";


        /*Constractor for the file board reader.*/
        public FileBoardReader(string filePath, EnumConstants.GameType boardType)
        {
            this.boardType = boardType;
            this.filePath = filePath;
            validator = ValidationFactory.GetValidator(boardType);
        }

        /*Function reads a board from a file to the game board.*/
        public GameBoard ReadBoard()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            // Get string board as input from the user.
            string gameBoard = ReadFile();
            // Make sure the string is valid.
            validator.ValidateBoard(gameBoard);
            GameBoard board = GetRightBoard((int)Math.Sqrt(gameBoard.Length));
            board.FillBoard(gameBoard);
            // board chek if the board is legal.
            board.CheckIfFollowGameRules();
            return board;
        }

        /*Function returns the right game board.*/
        public GameBoard GetRightBoard(int size)
        {
            switch (boardType)
            {
                case EnumConstants.GameType.SODOKU:
                    return new SudokuBoard(size);
                default:
                    throw new NoSuchGameException($"Invalid board type {boardType}.");
            }

        }

        /*Read file with exception handling.*/
        private string ReadFile()
        {
            try
            {
                if (!filePath.EndsWith(FILE_FORMAT))
                {
                    throw new IlegalFilePathInputException($"File path {filePath} not legal. It has to end with a {FILE_FORMAT} format. please try again.\n");
                }
                return File.ReadAllText(filePath);
            }
            catch (FileNotFoundException)
            {
                throw new IlegalFilePathInputException($"File path {filePath} not found. please try again.\n");
            }
            catch (UnauthorizedAccessException)
            {
                throw new IlegalFilePathInputException($"File path {filePath} not uatherized. please try again.\n");
            }
            catch (DirectoryNotFoundException)
            {
                throw new IlegalFilePathInputException($"Directory path of {filePath} not found. please try again.\n");
            }
            catch (PathTooLongException)
            {
                throw new IlegalFilePathInputException($"File path {filePath} is too long. please try again.\n");
            }
            catch (IOException)
            {
                throw new IlegalFilePathInputException($"File path {filePath} not found. please try again.\n");
            }

        }


    }
}
