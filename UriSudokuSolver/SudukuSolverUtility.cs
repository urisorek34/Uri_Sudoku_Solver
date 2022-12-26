using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{

    /*Sudoku solver static utility class.*/
    static class SudukuSolverUtility
    {

        /*Check if the value is valid for the row.*/
        private static bool IsValidForRow(GameBoard<int> board, int row, int value)
        {
            for (int col = 0; col < board.GetCols(); col++)
            {
                if (board[row, col] == value)
                {
                    return false;
                }
            }
            return true;
        }


        /*Check if the value is valid for the column.*/
        private static bool IsValidForCol(GameBoard<int> board, int col, int value)
        {
            for (int row = 0; row < board.GetRows(); row++)
            {
                if (board[row, col] == value)
                {
                    return false;
                }
            }
            return true;
        }


        /*Check if the value is valid for a section in the board.*/
        private static bool IsValidForSection(GameBoard<int> board, int row, int col, int value)
        {
            int boxRow = row - row % 3;
            int boxCol = col - col % 3;
            for (int checkedRow = boxRow; checkedRow < boxRow + 3; checkedRow++)
            {
                for (int checkedCol = boxCol; checkedCol < boxCol + 3; checkedCol++)
                {
                    if (board[checkedRow, checkedCol] == value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// Checks if the value is legal in specific place in the board
        /// </summary>
        /// <param name="board"> the board.</param>
        /// <param name="row"> the row index.</param>
        /// <param name="col"> the column index</param>
        /// <param name="value"> the value that seposed to be placed in the place on the board.</param>
        /// <returns>
        /// true if legal, false otherwise.
        /// </returns>
        public static bool IsLegalValue(SudokuBoard board, int row, int col, int value)
        {
            if (IsValidForCol(board, col, value) && IsValidForRow(board, row, value) && IsValidForSection(board, row, col, value))
            {
                return true;
            }
            return false;
        }

    }
}
