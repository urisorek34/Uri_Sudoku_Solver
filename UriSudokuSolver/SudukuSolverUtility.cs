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
        /// <summary>
        /// This method finds all the allowed values in all the empty places in the board and 
        /// returns a dict of location to list of values that can be in that location.
        /// </summary>
        /// <param name="board"> the sudoku board.</param>
        /// <param name="minValue"> the sudoku starting min value (1 if it's 1-9)</param>
        /// <param name="maxValue"> the sudoku starting maxValue (9 if it's 1-9)</param>
        /// <returns>
        /// A dictionary of value tuples of places as key and list of optional values in that place.
        /// </returns>
        public static Dictionary<(int, int), List<int>> CacheValidValues(SudokuBoard board, int minValue, int maxValue)
        {

            Dictionary<(int, int), List<int>> cache = new Dictionary<(int, int), List<int>>();
            for (int row = 0; row < board.GetRows(); row++)
            {
                for (int col = 0; col < board.GetCols(); col++)
                {
                    if (board[row, col] == 0)
                    {
                        cache.Add((row, col), AllowedValues(board, minValue, maxValue, row, col));
                    }
                }
            }
            return cache;
        }

        /*Returns true if a value on the board is legal, false otherwise.*/
        private static bool IsLegalValue(SudokuBoard board, int row, int col, int value)
        {
            if (IsValidForCol(board, col, value) && IsValidForRow(board, row, value) && IsValidForSection(board, row, col, value))
            {
                return true;
            }
            return false;
        }

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
            int factor = (int)Math.Sqrt(board.GetRows());
            int boxRow = row - row % factor;
            int boxCol = col - col % factor;
            for (int checkedRow = boxRow; checkedRow < boxRow + factor; checkedRow++)
            {
                for (int checkedCol = boxCol; checkedCol < boxCol + factor; checkedCol++)
                {
                    if (board[checkedRow, checkedCol] == value)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        /*Return list of legal allowed values in place in the board*/
        private static List<int> AllowedValues(SudokuBoard board, int minValue, int maxValue, int row, int col)
        {
            List<int> valuesList = new List<int>();
            for (int value = minValue; value <= maxValue; value++)
            {
                if (IsLegalValue(board, row, col, value))
                    valuesList.Add(value);
            }
            return valuesList;
        }


    }
}
