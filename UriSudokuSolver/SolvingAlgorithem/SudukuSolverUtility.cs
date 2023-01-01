using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        /// <returns>
        /// A dictionary of value tuples of places as key and list of optional values in that place.
        /// </returns>
        public static Dictionary<(int, int), List<int>> CacheValidValues(int[,] board )
        {

            Dictionary<(int, int), List<int>> cache = new Dictionary<(int, int), List<int>>();
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == 0)
                    {
                        cache.Add((row, col), AllowedValues(board, row, col));
                    }
                }
            }
            return cache;
        }


        /*Returns true if a value on the board is legal, false otherwise.*/
        public static bool IsLegalValue(int[,] board, int row, int col, int value)
        {
            if (IsValidForCol(board, col, value) && IsValidForRow(board, row, value) && IsValidForSection(board, row, col, value))
            {
                return true;
            }
            return false;
        }

        /*Find the first empty place in the board*/
        public static (int, int) FindFirstEmpty(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 0)
                    {
                        return (i, j);
                    }
                }
            }
            return (board.GetLength(0), board.GetLength(1));
        }

        /*Check if the value is valid for the row.*/
        private static bool IsValidForRow(int[,] board, int row, int value)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == value)
                {
                    return false;
                }
            }
            return true;
        }


        /*Check if the value is valid for the column.*/
        private static bool IsValidForCol(int[,] board, int col, int value)
        {
            for (int row = 0; row < board.GetLength(0); row++)
            {
                if (board[row, col] == value)
                {
                    return false;
                }
            }
            return true;
        }


        /*Check if the value is valid for a section in the board.*/
        private static bool IsValidForSection(int[,] board, int row, int col, int value)
        {
            int factor = (int)Math.Sqrt(board.GetLength(0));
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
        private static List<int> AllowedValues(int[,] board, int row, int col)
        {
            List<int> valuesList = new List<int>();
            for (int value = 1; value <= board.GetLength(0); value++)
            {
                if (IsLegalValue(board, row, col, value))
                    valuesList.Add((char)value);
            }
            return valuesList;
        }

        /*Return the allowed bit mask for a place in the board*/
        public static int AllowedBitMask(int[,] board, int row, int col)
        {
            int mask = 0;
            for (int value = 1; value <= board.GetLength(0); value++)
            {
                if (IsLegalValue(board, row, col, value))
                    mask |= 1 << (value - 1);
            }
            return mask;
        }
        
        




    }
}
