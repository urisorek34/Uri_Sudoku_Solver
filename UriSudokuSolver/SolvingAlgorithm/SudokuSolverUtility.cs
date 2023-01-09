using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Sudoku solver static utility class.*/
    static class SudokuSolverUtility
    {
        /*Gets the board masks for a given board.*/
        public static void InitializeBoardMasks(byte[,] board, out int[] masks, int boardSize)
        {
            masks = new int[boardSize];

            //Initialize masks
            for (int valueIndex = 0; valueIndex < boardSize; valueIndex++)
            {
                // Shift 1 to the left by value bits representing the mask of the value in the board by index (example 00000100 is the mask for the value 3).
                masks[valueIndex] = 1 << valueIndex;
            }
        }

        /*Set the valid values for each row column and box.*/
        public static void SetValidValues(byte[,] board, int[] masks, out int[] validValuesRow, out int[] validValuesColumn, out int[] validValuesBox, int boardSize)
        {
            // create arrays
            int sqrSize = (int)Math.Sqrt(boardSize);
            validValuesRow = new int[boardSize];
            validValuesColumn = new int[boardSize];
            validValuesBox = new int[boardSize];

            // Initialize the valid values for each row, column and box
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (board[row, col] != 0)
                    {
                        // Set the valid value for the row col and box by using the OR operator to add the mask of the value to the valid values.
                        UpdateValidValues(masks, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col, board[row, col]);
                    }
                }
            }
        }

        /*Finds the best empty cell.*/
        public static void FindBestEmptyCell(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int sqrSize, int boardSize, out int emptyCellRow, out int emptyCellCol)
        {
            emptyCellRow = -1;
            emptyCellCol = -1;
            int minValidValues = boardSize + 1;
            int validValues, bitCount;
            // Find the cell with the minimum number of valid values
            for (int row = 0; row < boardSize; row++)
            {
                // check if the row is full
                if (!CheckIfGroupFilled(validValuesRow[row], boardSize))
                {
                    for (int col = 0; col < boardSize; col++)
                    {
                        if (board[row, col] == 0)
                        {

                            validValues = GetValidValuesForCell(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col, boardSize);
                            bitCount = CountBits(validValues);
                            // If the empty cell doesn't have any valid values, then send this cell back 
                            if (bitCount == 0)
                            {
                                // No valid values for the cell
                                emptyCellRow = row;
                                emptyCellCol = col;
                                return;
                            }

                            // find min valid values
                            else if (bitCount <= minValidValues)
                            {
                                minValidValues = bitCount;
                                emptyCellRow = row;
                                emptyCellCol = col;
                            }

                        }
                    }
                }

            }
        }


        /*Function checks if a  group is full*/
        private static bool CheckIfGroupFilled(int group, int size)
        {
            int full = (size * size) - 1;
            // Check if the group is full
            if (group == full)
                return true;
            return false;
        }



        /*Get valid values for a given cell.*/
        public static int GetValidValuesForCell(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int sqrSize, int row, int col, int boardSize)
        {
            // OR bit operator on the unvalid values (bit in the index - 1 of the value is 1) of the row, column and box representing the valid values of the cell
            int nonValidValuesRowColSqr = validValuesRow[row] | validValuesColumn[col] | validValuesBox[GetBoxIndex(row, col, sqrSize)];
            // NOT bit operator on the sum of the unvalid values to get the valid values
            int validValues = ~nonValidValuesRowColSqr;
            // AND bit operator on the valid values and the sum of the valid values to get the number of valid values
            validValues = validValues & ((1 << boardSize) - 1);
            return validValues;
        }

        /*updates valid values that is filled*/
        public static void UpdateValidValues(int[] masks, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int sqrSize, int row, int col, int value)
        {
            // Set the valid value for the row col and box by using the OR operator to add the mask of the value to the valid values.
            validValuesRow[row] |= masks[value - 1];
            validValuesColumn[col] |= masks[value - 1];
            validValuesBox[GetBoxIndex(row, col, sqrSize)] |= masks[value - 1];
        }


        /*Count the number of bits in a binary number.*/
        public static int CountBits(int value)
        {
            int count = 0;
            // check how many bits by AND operation with the value and the value - 1 (example 00000100 & 00000011 = 00000000)
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
                    (validValuesBox[GetBoxIndex(row, col, sqrSize)] & masks[value]) == 0;
        }


        /*Updates the board with a value */
        public static void UpdateBoard(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int row, int col, int validValues)
        {
            int valueIndex = GetBitIndex(validValues);
            // update board and allowed values
            board[row, col] = (byte)valueIndex;
            UpdateValidValues(masks, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col, valueIndex);
        }

        /*Get value of in a binary number when has one option.*/
        private static int GetBitIndex(int value)
        {
            // if it has only one bit then it's a power of 2
            double powerOf2 = Math.Log(value, 2);
            return (int)powerOf2 + 1;
        }

        /*Add value to valid values (remove value from board).*/
        public static void AddValueToValidValues(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int row, int col)
        {
            int value = board[row, col];
            // Set the valid value for the row col and box by using the XOR operator to add the mask of the value to the valid values.
            validValuesRow[row] &= ~masks[value - 1];
            validValuesColumn[col] &= ~masks[value - 1];
            validValuesBox[GetBoxIndex(row, col, sqrSize)] &= ~masks[value - 1];
        }



        /*Cleans the stack form saved values and return them to the board*/
        public static void CleanStack(byte[,] board, Stack<int> validStack, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int cellNum, int boardSize)
        {
            int index, row, col;
            for (int i = 0; i < cellNum; i++)
            {
                index = validStack.Pop();
                row = index / boardSize;
                col = index % boardSize;
                //deletes from valide values arrays
                AddValueToValidValues(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col);
                board[row, col] = 0;
            }

        }

        /*Get box index*/
        public static int GetBoxIndex(int row, int col, int sqrSize)
        {
            return (row / sqrSize) * sqrSize + col / sqrSize;
        }




    }
}
