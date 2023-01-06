using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.SolvingAlgorithm
{
    /*Static class that contains all the human tactics.*/
    internal static class HumanTactics
    {
        /*Try to solve board once with human tactics*/
        public static int HumanTacticsSolver(Stack<int> savedValues, byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize)
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
                        validValues = SudokuSolverUtility.GetValidValuesForCell(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col);
                        // count them
                        bitCount = SudokuSolverUtility.CountBits(validValues);
                        if (bitCount == 1)
                        {
                            UpdateAndPush(savedValues, board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, validValues);
                            found++;
                            continue;
                        }
                        if (bitCount == 0)
                        {
                            // if is empty and doesn't have valid values then the board is unsolvale
                            SudokuSolverUtility.CleanStack(board, savedValues, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, found);
                            return -1;
                        }
                        // try to search for hidden singles --> for cells that have an option that no other cell in the row, column or box can have
                        hiddenSingle = HiddenSingles(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, validValues);
                        if (hiddenSingle > 0)
                        {
                            UpdateAndPush(savedValues, board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, hiddenSingle);
                            found++;
                        }
                        if (hiddenSingle == -1)
                        {
                            // if is empty and can't get valid values then the board is unsolvale
                            SudokuSolverUtility.CleanStack(board, savedValues, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, found);
                            return -1;
                        }
                    }
                }
            }
            return found;
        }

        /*Update the board with a value and save the changes in the stack*/
        private static void UpdateAndPush(Stack<int> savedValues, byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int row, int col, int value)
        {
            SudokuSolverUtility.UpdateBoard(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, value);
            savedValues.Push(row * board.GetLength(0) + col);
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
                        possibleInOtherCells |= SudokuSolverUtility.GetValidValuesForCell(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, rowInsideBox, colInsideBox);
                    }
                }

            }
            // get the possible values for the cell by using the NOT and OR bit operators to remove the possible values for all the cells in the box
            possibleForCell = ~(~validValues | possibleInOtherCells);

            //if the cell has only one valid value
            if (SudokuSolverUtility.CountBits(possibleForCell) == 1)
            {

                return possibleForCell;
            }

            // if the cell has no valid values
            if (possibleForCell == 0)
                return 0;
            return -1;


        }
    }

}
