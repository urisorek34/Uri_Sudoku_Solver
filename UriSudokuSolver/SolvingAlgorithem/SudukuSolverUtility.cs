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
        /*Gets the board masks for a given board.*/
        public static void InitializeBoardMasks(int[,] board, out int[] masks)
        {
            masks = new int[board.GetLength(0)];

            //Initialize masks
            for (int valueIndex = 0; valueIndex < board.GetLength(0); valueIndex++)
            {
                // Shift 1 to the left by value bits representing the mask of the value in the board by index (example 00000100 is the mask for the value 3).
                masks[valueIndex] = 1 << valueIndex;
            }
        }

        /*Gets the valid values for each row column and box.*/
        public static void GetValidValues(int[,] board, int[] masks, out int[] validValuesRow, out int[] validValuesColumn, out int[] validValuesBox)
        {
            int sqrSize = (int)Math.Sqrt(board.GetLength(0));
            validValuesRow = new int[board.GetLength(0)];
            validValuesColumn = new int[board.GetLength(0)];
            validValuesBox = new int[board.GetLength(0)];

            // Initialize the valid values for each row, column and box
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] != 0)
                    {
                        // Set the valid value for the row col and box by using the OR operator to add the mask of the value to the valid values.
                        validValuesRow[i] |= masks[board[i, j] - 1];
                        validValuesColumn[j] |= masks[board[i, j] - 1];
                        validValuesBox[(i / sqrSize) * sqrSize + j / sqrSize] |= masks[board[i, j] - 1];
                    }
                }
            }
        }

        /*Finds the best empty cell.*/
        public static int FindBestEmptyCell(int[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox,int sqrSize,out int emptyCellRow, out int emptyCellCol)
        {
            emptyCellRow = -1;
            emptyCellCol = -1;
            int minValidValues = board.GetLength(0) + 1;
            int validValues, nonValidValuesRowColSqr;
            // Find the cell with the minimum number of valid values
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == 0)
                    {
                        // OR bit operator on the unvalid values (bit in the index - 1 of the value is 1) of the row, column and box representing the valid values of the cell
                        nonValidValuesRowColSqr = validValuesRow[i] | validValuesColumn[j] | validValuesBox[(i / sqrSize) * sqrSize + j / sqrSize];
                        // NOT bit operator on the sum of the unvalid values to get the valid values
                        validValues = ~nonValidValuesRowColSqr;
                        // AND bit operator on the valid values and the sum of the valid values to get the number of valid values
                        validValues = validValues & ((1 << board.GetLength(0)) - 1);
                        if (minValidValues == 1)
                        {
                            return 1;
                        }
                        // find max valid values
                        if (CountBits(validValues) < minValidValues)
                        {
                            minValidValues = CountBits(validValues);
                            emptyCellRow = i;
                            emptyCellCol = j;
                            if (minValidValues == 1)
                            {
                                return 1;
                            }
                        }
                        if (minValidValues == 1)
                        {
                            return 1;
                        }
                    }
                }
            }
            return 0;
        }
        /*Count the number of bits in a binary number.*/
        private static int CountBits(int value)
        {
            int count = 0;
            while (value != 0)
            {
                count++;
                value &= value - 1;
            }
            return count;
        }

        /*Check if a cell can be in a certian place.*/
        public static bool IsSafe(int row, int col, int value, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks,int sqrSize)
        {
            return (validValuesRow[row] & masks[value]) == 0 &&
                    (validValuesColumn[col] & masks[value]) == 0 &&
                    (validValuesBox[(row / sqrSize) * sqrSize + col / sqrSize] & masks[value]) == 0;
        }









    }
}
