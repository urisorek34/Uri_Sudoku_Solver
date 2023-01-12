using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.SolvingAlgorithm
{
    /*Static class that contains all the human tactics.*/
    public static class HumanTactics
    {
        /*Try to solve board once with human tactics*/
        public static int HumanTacticsSolver(Stack<int> savedValues, Dictionary<int, List<int>> cellsPeers, Queue<int> peersQueue, byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int boardSize, int cellForPeers)
        {
            // how many changes made
            int found = 0;
            int validValues, bitCount, hiddenSingle;
            int row, col;
            // go over the peers of the changes cell
            foreach (int cell in cellsPeers[cellForPeers])
            {
                row = cell / boardSize;
                col = cell % boardSize;
                // if empty space
                if (board[row, col] == 0)
                {
                    //get the values that this cell can get (in binary)
                    validValues = SudokuSolverUtility.GetValidValuesForCell(validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col, boardSize);
                    // count them
                    bitCount = SudokuSolverUtility.CountBits(validValues);
                    if (bitCount == 1)
                    {
                        // if only one value is valid for this cell, put it in the board and push to the saving stack
                        UpdateAndPush(savedValues, peersQueue, board, validValuesRow, validValuesColumn, validValuesBox, masks, boardSize, sqrSize, row, col, validValues);
                        found++;
                        continue;
                    }
                    if (bitCount == 0)
                    {
                        // if is empty and doesn't have valid values then the board is unsolvale
                        SudokuSolverUtility.CleanStack(board, savedValues, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, found, boardSize);
                        return -1;
                    }
                    // try to search for hidden singles --> for cells that have an option that no other cell in the row, column or box can have
                    hiddenSingle = HiddenSingles(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, validValues, boardSize);
                    if (hiddenSingle > 0)
                    {
                        // if found hidden single, put it in the board and push to the saving stack
                        UpdateAndPush(savedValues, peersQueue, board, validValuesRow, validValuesColumn, validValuesBox, masks, boardSize, sqrSize, row, col, hiddenSingle);
                        found++;
                    }
                    if (hiddenSingle == -1)
                    {
                        // if is empty and can't get valid values then the board is unsolvale
                        SudokuSolverUtility.CleanStack(board, savedValues, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, found, boardSize);
                        return -1;
                    }
                    //NakedPairs(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, validValues);

                }
            }

            return found;
        }

        /*Function finds naked pairs and updates all other cells that this two options has to be in those two cells*/
        public static void NakedPairs(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int row, int col, int validValues, int boardSize)
        {
            int bitCount, validValues2;
            // count them
            bitCount = SudokuSolverUtility.CountBits(validValues);
            if (bitCount == 2)
            {
                // if only two values are valid for this cell, search for other cells that can get the same two values
                for (int row2 = 0; row2 < board.GetLength(0); row2++)
                {
                    for (int col2 = 0; col2 < board.GetLength(1); col2++)
                    {
                        // if empty space
                        if (board[row2, col2] == 0 && (row != row2 || col != col2))
                        {
                            //get the values that this cell can get (in binary)
                            validValues2 = SudokuSolverUtility.GetValidValuesForCell(validValuesRow, validValuesColumn, validValuesBox, sqrSize, row2, col2, boardSize);
                            // count them
                            if (validValues2 == validValues)
                            {
                                // if they are the same, update all other cells in the row, column and box that can't get these two values
                                UpdateNakedPairs(validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, row2, col2, validValues);
                            }
                        }
                    }
                }
            }
        }
        /*Update the valid values for the cells in the row, culumn and a box that the only two cells that can get the same two values are the given cell and the given cell2*/
        private static void UpdateNakedPairs(int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int row, int col, int row2, int col2, int validValues)
        {
            // update all other cells in the row, column and box that can't get these two values
            int cellInBox = row % sqrSize * sqrSize + col % sqrSize;
            int cellInBox2 = row2 % sqrSize * sqrSize + col2 % sqrSize;
            for (int i = 0; i < validValuesRow.Length; i++)
            {
                if (i != row && i != row2)
                {
                    validValuesRow[i] |= validValues;
                }
                if (i != col && i != col2)
                {
                    validValuesColumn[i] |= validValues;
                }
                if (i != cellInBox && i != cellInBox2)
                {
                    validValuesBox[i] |= validValues;
                }
            }



        }

        /*Update the board with a value and save the changes in the stack*/
        private static void UpdateAndPush(Stack<int> savedValues, Queue<int> peersQueue, byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int boardSize, int sqrSize, int row, int col, int value)
        {
            SudokuSolverUtility.UpdateBoard(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, value);
            savedValues.Push(row * boardSize + col);
            peersQueue.Enqueue(row * boardSize + col);
            peersQueue.Enqueue(row * boardSize + col);
        }




        /*The function get board, row and column of an empty cell and seek for its only value as naked single --> if it can't be in any other cell in the same box, it has to be there.*/
        public static int HiddenSingles(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int row, int col, int validValues, int boardSize)
        {
            // Get the cell index in the box.
            int cellInBox = row % sqrSize * sqrSize + col % sqrSize;
            int possibleInOtherCells = 0;
            int rowInsideBox, colInsideBox, possibleForCell;
            // check the box number
            int boxNumber = SudokuSolverUtility.GetBoxIndex(row, col, sqrSize);
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
                        possibleInOtherCells |= SudokuSolverUtility.GetValidValuesForCell(validValuesRow, validValuesColumn, validValuesBox, sqrSize, rowInsideBox, colInsideBox, boardSize);
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
