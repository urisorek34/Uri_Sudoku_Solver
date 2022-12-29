using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Class responseble of validating the given input.*/
    internal class SudokuValidator : IValidator
    {
        public void ValidateBoard(string gameBoard, int size)
        {
            // check if can be a sudoku board of sizeXsize
            if (Math.Sqrt(size) % 1 != 0)
            {
                throw new BoardNotInRightSizeException("The size of the board is not valid --> board size must be a square number");
            }
            
        }
    }
}
