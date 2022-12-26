using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Sudoku board for the sudoko solver.*/
    class SudokuBoard : GameBoard<int>
    {
        private int minValue;
        private int maxValue;
        
        /// <summary>
        /// Constractor for sudoku board.
        /// </summary>
        /// <param name="size"> the size of the board.</param>
        /// <param name="minValue"> the min value for the sudoko board</param>
        public SudokuBoard(int size,int minValue) : base(size)
        {
            this.minValue = minValue;
            this.maxValue = minValue + size - 1;
        }

        //Getters for min and max values in the soduko.
        public int GetMinValue() { return minValue; }
        public int GetMaxValue() { return maxValue; }

        public void FillBoard(string b)
        {
            int row = 0;
            int col = 0;
            foreach (char c in b)
            {
                if (c == '.')
                {
                    this[row, col] = 0;
                }
                else
                {
                    this[row, col] = c - '0';
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
