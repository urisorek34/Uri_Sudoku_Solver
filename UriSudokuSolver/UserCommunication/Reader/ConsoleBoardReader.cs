using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;
using UriSudokuSolver.UserCommunication;
using UriSudokuSolver.UserCommunication.Validation;

namespace UriSudokuSolver
{
    /*Class is responsable of reading a game board from the console.*/
    public class ConsoleBoardReader : IBoardReader
    {
        private IValidator boardValidator;
        EnumConstants.GameType boardType;

        /*Constractor for the console board reader.*/
        public ConsoleBoardReader(EnumConstants.GameType boardType)
        {
            this.boardType = boardType;
            boardValidator = ValidationFactory.GetValidator(boardType);
        }

        /*Function read a board from the console to the game board.*/
        public GameBoard ReadBoard()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            // Get string board as input from the user.
            string line = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            // Make sure the strung is valid.
            boardValidator.ValidateBoard(line);
            GameBoard board = GetRightBoard((int)Math.Sqrt(line.Length));
            board.FillBoard(line);
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
    }
}
