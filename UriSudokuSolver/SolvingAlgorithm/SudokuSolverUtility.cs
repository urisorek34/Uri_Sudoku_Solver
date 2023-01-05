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
        public static void InitializeBoardMasks(byte[,] board, out int[] masks)
        {
            masks = new int[board.GetLength(0)];

            //Initialize masks
            for (int valueIndex = 0; valueIndex < board.GetLength(0); valueIndex++)
            {
                // Shift 1 to the left by value bits representing the mask of the value in the board by index (example 00000100 is the mask for the value 3).
                masks[valueIndex] = 1 << valueIndex;
            }
        }

        /*Set the valid values for each row column and box.*/
        public static void SetValidValues(byte[,] board, int[] masks, out int[] validValuesRow, out int[] validValuesColumn, out int[] validValuesBox)
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
                        UpdateValidValues(masks, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col, board[row, col]);
                    }
                }
            }
        }

        /*Finds the best empty cell.*/
        public static void FindBestEmptyCell(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, out int emptyCellRow, out int emptyCellCol)
        {
            emptyCellRow = -1;
            emptyCellCol = -1;
            int minValidValues = board.GetLength(0) + 1;
            int validValues, bitCount;
            // Find the cell with the minimum number of valid values
            for (int row = 0; row < board.GetLength(0); row++)
            {
                // check if the row is full
                if (!CheckIfGroupFilled(validValuesRow[row], board.GetLength(0)))
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        if (board[row, col] == 0)
                        {

                            validValues = GetValidValuesForCell(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col);
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
            int full = (int)Math.Pow(2, size) - 1;
            // Check if the group is full
            if (group == full)
                return true;
            return false;
        }



        /*Get valid values for a given cell.*/
        private static int GetValidValuesForCell(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int sqrSize, int row, int col)
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
        public static void UpdateValidValues(int[] masks, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int sqrSize, int row, int col, int value)
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
                    (validValuesBox[(row / sqrSize) * sqrSize + col / sqrSize] & masks[value]) == 0;
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

        /*Add value to valid values.*/
        public static void AddValueToValidValues(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int row, int col)
        {
            int value = board[row, col];
            // Set the valid value for the row col and box by using the XOR operator to add the mask of the value to the valid values.
            validValuesRow[row] &= ~masks[value - 1];
            validValuesColumn[col] &= ~masks[value - 1];
            validValuesBox[(row / sqrSize) * sqrSize + col / sqrSize] &= ~masks[value - 1];
        }

        /*Try to solve board once with human tactics*/
        public static int HumanTactics(Stack<int> savedValues, byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize)
        {
            // how many changes made
            int found = 0;
            int validValues, bitCount, hiddenSingle;
            // Go over all the matrix and search empry spaces
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    // if empty space
                    if (board[row, col] == 0)
                    {
                        //get the values that this cell can get (in binary)
                        validValues = GetValidValuesForCell(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col);
                        // count them
                        bitCount = CountBits(validValues);
                        if (bitCount == 1)
                        {
                            // if it has only one option (simple elimination) update the board with the option
                            UpdateBoard(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, validValues);
                            // push the changes into the stack (pack the row and col into one int)
                            savedValues.Push(row * board.GetLength(0) + col);
                            found++;
                            continue;
                        }
                        if (bitCount == 0)
                        {
                            // if is empty and doesn't have valid values then the board is unsolvale
                            CleanStack(board, savedValues, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, found);
                            return -1;
                        }
                        // try to search for hidden singles --> for cells that have an option that no other cell in the row, column or box can have
                        hiddenSingle = HiddenSingles(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, validValues);
                        if (hiddenSingle == 1)
                        {
                            // save the changes in the stack
                            savedValues.Push(row * board.GetLength(0) + col);
                            found++;
                        }
                        if (hiddenSingle == -1)
                        {
                            // if is empty and can't get valid values then the board is unsolvale
                            CleanStack(board, savedValues, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, found);
                            return -1;
                        }
                    }
                }
            }
            return found;
        }


        /*Cleans the stack*/
        public static void CleanStack(byte[,] board, Stack<int> validStack, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int cellNum)
        {
            int index, row, col;
            for (int i = 0; i < cellNum; i++)
            {
                index = validStack.Pop();
                row = index / board.GetLength(0);
                col = index % board.GetLength(0);
                //deletes from valide values arrays
                AddValueToValidValues(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col);
                board[row, col] = 0;
            }

        }
        /*The function get board, row and column of an empty cell and seek for its only value as naked single --> if it can't be in any other row col and box it has to be there.*/
        public static int HiddenSingles(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int row, int col, int validValues)
        {
            // Get the cell index in the box.
            int cellInBox = row % sqrSize * sqrSize + col % sqrSize;
            int possibleInOtherCells = 0;
            int rowInsideBox, colInsideBox, possibleForCell;
            // check the box number
            int boxNumber = (row - row % sqrSize) + (col - col % sqrSize) / sqrSize;
            //Go over all the boxes in the board
            for (int i = 0; i < board.GetLength(0); i++)
            {
                //if the box is not the current box
                if (i != cellInBox)
                {
                    //get the row and column of the cell inside the box
                    rowInsideBox = sqrSize * (boxNumber / sqrSize) + i / sqrSize;
                    colInsideBox = (boxNumber % sqrSize) * sqrSize + i % sqrSize;
                    //if the cell is empty
                    if (board[rowInsideBox, colInsideBox] == 0)
                    {
                        // add the valid values of the cell to the possible values for all the cells in the box
                        possibleInOtherCells |= GetValidValuesForCell(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, rowInsideBox, colInsideBox);
                    }
                }

            }
            //if the cell has no valid values
            possibleForCell = ~(~validValues | possibleInOtherCells);

            //if the cell has only one valid value
            if (CountBits(possibleForCell) == 1)
            {
                UpdateBoard(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, possibleForCell);
                return 1;
            }
            if (possibleForCell == 0)
                return 0;
            return -1;


        }






    }
}
