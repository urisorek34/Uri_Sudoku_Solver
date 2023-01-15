using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver
{
    /*Class responseble of validating the given input.*/
    public class SudokuBoardValidator : IValidator
    {

        private const int MAX_STRING_SIZE = 25 * 25;
        /*Empty constractor*/
        public SudokuBoardValidator()
        {

        }
        /*Validation function for the sudoku given values of the board.*/
        public void ValidateBoard(string gameBoard)
        {
            int size = gameBoard.Length;
            // check if the string is empty
            if (size == 0)
            {
                throw new BoardIsEmptyException("The board is empty. A valid board has a (square size)^2.");
            }
            // check if the string is too long
            if (size > MAX_STRING_SIZE)
            {
                throw new BoardStringSizeIsNotValidException($"The board is too big. The max size is {MAX_STRING_SIZE} --> 25X25.");
            }
            // check if can be a sudoku board of sizeXsize
            if (Math.Sqrt(Math.Sqrt(size)) % 1 != 0)
            {
                throw new BoardStringSizeIsNotValidException($"The size of the board {size} is not valid --> board string size must be (square number)^2");
            }
            // check if the given gameBoard contains any Ilegal chars
            char ilegalChar = CheckIlegalCharsInBoard(gameBoard);
            if (ilegalChar != '0')
            {
                throw new BoardStringIlegalCharException($"The board has ilegal chars in it the first one is {ilegalChar}");
            }

        }

        /*Function checks if there is an Ilegal Value in the gameBoard string*/
        private char CheckIlegalCharsInBoard(string gameBoard)
        {
            for (int i = 0; i < gameBoard.Length; i++)
            {
                if (gameBoard[i] - '0' > (int)Math.Sqrt(gameBoard.Length) || gameBoard[i] - '0' < 0)
                {
                    return gameBoard[i];
                }
            }
            return '0';
        }
    }
}
