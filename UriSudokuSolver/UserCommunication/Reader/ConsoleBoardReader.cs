using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;
using UriSudokuSolver.UserCommunication.Validation;

namespace UriSudokuSolver
{
    /*Class is responsable of reading a game board from the console.*/
    class ConsoleBoardReader : IBoardReader
    {
        private IValidator boardValidator;
        string boardType;

        /*Constractor for the console board reader.*/
        public ConsoleBoardReader(string boardType)
        {
            this.boardType = boardType;
            boardValidator = ValidationFactory.GetValidator(boardType);
        }

        /*Function read a board from the console to the game board.*/
        public GameBoard<char> ReadBoard()
        {
            string line = Console.ReadLine();
            boardValidator.ValidateBoard(line);
            GameBoard<char> board = GetRightBoard((int)Math.Sqrt(line.Length));
            board.FillBoard(line);
            return board;
        }

        /*Function returns the right game board.*/
        public GameBoard<char> GetRightBoard(int size)
        {
            switch (boardType)
            {
                case "sudoku":
                    return new SudokuBoard(size);
                default:
                    throw new NoSuchGameException($"Invalid board type {boardType}.");
            }
        }
    }
}
