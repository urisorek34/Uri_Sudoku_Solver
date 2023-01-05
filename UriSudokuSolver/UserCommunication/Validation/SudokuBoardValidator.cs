using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver
{
    /*Class responseble of validating the given input.*/
    internal class SudokuBoardValidator : IValidator
    {
        /*Empty constractor*/
        public SudokuBoardValidator()
        {

        }
        /*Validation function for the sudoku given values of the board.*/
        public void ValidateBoard(string gameBoard)
        {
            int size = gameBoard.Length;
            if (size == 0)
            {
                throw new BoardIsEmptyException("The board is empty. Avalid board have a square size.");
            }
            // check if can be a sudoku board of sizeXsize
            if (Math.Sqrt(Math.Sqrt(size)) % 1 != 0)
            {
                throw new BoardStringSizeIsNotValidException($"The size of the board {size} is not valid --> board size must be a square number");
            }
            // check if the given gameBoard contains any Ilegal chars
            char ilegalChar = CheckIlegalCharsInBoard(gameBoard);
            if (ilegalChar != '0')
            {
                throw new BoardStringIlegalCharException($"The board has ilegal char/s in it the first one is {ilegalChar}");
            }

        }

        /*Function checks if there is an Ilegal Value in the gameBoard string*/
        private char CheckIlegalCharsInBoard(string gameBoard)
        {
            for (int i = 0; i < gameBoard.Length; i++)
            {
                if (gameBoard[i] - '0' > gameBoard.Length || gameBoard[i] - '0' < 0)
                {
                    return gameBoard[i];
                }
            }
            return '0';
        }
    }
}
