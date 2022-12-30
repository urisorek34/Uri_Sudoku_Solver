using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Sudoku board for the sudoko solver.*/
    class SudokuBoard : GameBoard<char>
    {
        private const int MIN_VALUE = 1 + '0';

        /// <summary>
        /// Constractor for sudoku board.
        /// </summary>
        /// <param name="size"> the size of the board.</param>
        /// <param name="minValue"> the min value for the sudoko board</param>
        public SudokuBoard(int size) : base(size)
        {
            
        }

        //Getters for min and max values in the soduko.
        public int GetMinValue() { return MIN_VALUE; }
        public int GetMaxValue() { return MIN_VALUE + GetRows() - 1; }

        /*Function fill the board with the given string.*/
        public override void FillBoard(string boardString)
        {
            int row = 0;
            int col = 0;
            foreach (char c in boardString)
            {
                if (c == '.')
                {
                    board[row, col] = '0';
                }
                else
                {
                    board[row, col] = c;
                }
                col++;
                if (col == GetCols())
                {
                    col = 0;
                    row++;
                }
            }
        }
    }
}
