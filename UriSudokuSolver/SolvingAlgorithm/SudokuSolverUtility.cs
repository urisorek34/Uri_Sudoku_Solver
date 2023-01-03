using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Sudoku solver static utility class.*/
    static class SudokuSolverUtility
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
            // create arrays
            int sqrSize = (int)Math.Sqrt(board.GetLength(0));
            validValuesRow = new int[board.GetLength(0)];
            validValuesColumn = new int[board.GetLength(0)];
            validValuesBox = new int[board.GetLength(0)];

            // Initialize the valid values for each row, column and box
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] != 0)
                    {
                        // Set the valid value for the row col and box by using the OR operator to add the mask of the value to the valid values.
                        UpdateValidValues(board, masks, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col, board[row, col]);
                    }
                }
            }
        }

        /*Finds the best empty cell.*/
        public static void FindBestEmptyCell(int[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, out int emptyCellRow, out int emptyCellCol)
        {
            emptyCellRow = -1;
            emptyCellCol = -1;
            int minValidValues = board.GetLength(0) + 1;
            int validValues, bitCount, value;
            // Find the cell with the minimum number of valid values
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == 0)
                    {

                        validValues = GetValidValuesForCell(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col);
                        bitCount = CountBits(validValues);
                        // If the cell has only one valid value return 1
                        if (validValues == 1)
                        {
                            HiddenSingles(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, validValues);
                        }
                        // find min valid values
                        else if (bitCount < minValidValues)
                        {
                            minValidValues = bitCount;
                            emptyCellRow = row;
                            emptyCellCol = col;
                        }

                    }
                }
            }
        }

        /*Get valid values for a given cell.*/
        private static int GetValidValuesForCell(int[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int sqrSize, int row, int col)
        {
            // OR bit operator on the unvalid values (bit in the index - 1 of the value is 1) of the row, column and box representing the valid values of the cell
            int nonValidValuesRowColSqr = validValuesRow[row] | validValuesColumn[col] | validValuesBox[(row / sqrSize) * sqrSize + col / sqrSize];
            // NOT bit operator on the sum of the unvalid values to get the valid values
            int validValues = ~nonValidValuesRowColSqr;
            // AND bit operator on the valid values and the sum of the valid values to get the number of valid values
            validValues = validValues & ((1 << board.GetLength(0)) - 1);
            return validValues;
        }

        /*updates valid values filed*/
        public static void UpdateValidValues(int[,] board, int[] masks, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int sqrSize, int row, int col, int value)
        {
            // Set the valid value for the row col and box by using the OR operator to add the mask of the value to the valid values.
            validValuesRow[row] |= masks[value - 1];
            validValuesColumn[col] |= masks[value - 1];
            validValuesBox[(row / sqrSize) * sqrSize + col / sqrSize] |= masks[value - 1];
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
        public static bool IsSafe(int row, int col, int value, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize)
        {
            // Check with the AND bit operator on the mask of the value, if the value is valid in the row, column and box (if the 1 bit is on in the value index then it's not safe).
            return (validValuesRow[row] & masks[value]) == 0 &&
                    (validValuesColumn[col] & masks[value]) == 0 &&
                    (validValuesBox[(row / sqrSize) * sqrSize + col / sqrSize] & masks[value]) == 0;
        }


        /*Hidden singles constraint--> update cell with only on posibile value*/
        private static void HiddenSingles(int[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize,int row, int col, int validValues)
        {
            int valueIndex = GetBitIndex(validValues);
            // update board and allowed values
            board[row, col] = valueIndex;
            UpdateValidValues(board, masks, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col, valueIndex);
        }

        /*Get value of in a binary number when has one option.*/
        private static int GetBitIndex(int value)
        {
            int index = 0;
            // count the number of bits until the value is 1 (valid value).
            while (value != 0)
            {
                index++;
                value >>= 1;
            }
            return index;
        }


        /*Copy matrix*/
        public static int[,] CopyMatrix(int[,] matrix)
        {
            int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }
            return newMatrix;
        }


    }
}
