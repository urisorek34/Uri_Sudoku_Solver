using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver
{
    /*Class responseble of validating the given input.*/
    internal class SudokuValidator : IValidator
    {
        /*Validation function for the sudoku given values of the board.*/
        public void ValidateBoard(string gameBoard, int size)
        {
            // check if can be a sudoku board of sizeXsize
            if (Math.Sqrt(size) % 1 != 0)
            {
                throw new BoardNotInRightSizeException($"The size of the board {size} is not valid --> board size must be a square number");
            }
            // check if the given board string is the right size
            if (gameBoard.Length != size*size)
            {
                throw new BoardStringSizeIsNotValidException($"The size of the given input board ,{gameBoard.Length}, is not the size expected: {size}");
            }
            // check if the given gameBoard contains any Ilegal chars
            char ilegalChar = CheckIlegalCharsInBoard(gameBoard, size);
            if (ilegalChar != '0')
            {
                throw new BoardStringIlegalCharException($"The board has ilegal char/s in it the first one is {ilegalChar}");
            }

        }

        /*Function checks if there is an Ilegal Value in the gameBoard string*/
        private char CheckIlegalCharsInBoard(string gameBoard, int maxBoardValue)
        {
            for (int i = 0; i < gameBoard.Length; i++)
            {
                if (gameBoard[i] - '0' > maxBoardValue)
                {
                    return gameBoard[i];
                }
            }
            return '0';
        }
    }
}
