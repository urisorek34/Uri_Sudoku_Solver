namespace UriSudokuSolver.SolvingAlgorithm
{
    /*Static class that contains all the human tactics.*/
    public static class HumanTactics
    {
        /*Try to solve board once with human tactics*/
        public static int HumanTacticsSolver(Stack<int> savedValues, Dictionary<int, List<int>> cellsPeers, List<int> peersQueue, byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int sqrSize, int boardSize, int cellForPeers)
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
                    hiddenSingle = HiddenSingles(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col, validValues, boardSize);
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
                }

            }

            return found;
        }

        /*Update the board with a value and save the changes in the stack*/
        private static void UpdateAndPush(Stack<int> savedValues, List<int> peersQueue, byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int[] masks, int boardSize, int sqrSize, int row, int col, int value)
        {
            SudokuSolverUtility.UpdateBoard(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, row, col, value);
            savedValues.Push(row * boardSize + col);
            peersQueue.Add(row * boardSize + col);
        }


        /*The function get board, row and column of an empty cell and seek for its only value as naked single --> if it can't be in any other cell in the same box, it has to be there.
         the function returns 0 if not found hidden, the possible value for a cell if found, and -1 if the board is unsolveable.                                                       */
        private static int HiddenSingles(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int sqrSize, int row, int col, int validValues, int boardSize)
        {
            int possibleInOtherCellsBox = 0, possibleInOtherCellsRow = 0, possibleInOtherCellsCol = 0;
            // Get the cell index in the box.
            int cellInBox = row % sqrSize * sqrSize + col % sqrSize;
            // check the box number
            int boxNumber = SudokuSolverUtility.GetBoxIndex(row, col, sqrSize);

            int possibleForCellBox, possibleForCellRow, possibleForCellCol;

            // Get possible vlues for box, row and column
            for (int indexInGroup = 0; indexInGroup < boardSize; indexInGroup++)
            {
                possibleInOtherCellsBox = GetPossibleValuesForOtherCellsInBox(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, cellInBox, boxNumber, boardSize, indexInGroup, possibleInOtherCellsBox);
                possibleInOtherCellsRow = GetPossibleValuesForOtherCellsInRow(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col, boardSize, indexInGroup, possibleInOtherCellsRow);
                possibleInOtherCellsCol = GetPossibleValuesForOtherCellsInCol(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, col, boardSize, indexInGroup, possibleInOtherCellsCol);
            }


            // get the possible values for the cell by using the NOT and OR bit operators to remove the possible values for all the cells in the row, col and box
            possibleForCellBox = ~(~validValues | possibleInOtherCellsBox);
            possibleForCellRow = ~(~validValues | possibleInOtherCellsRow);
            possibleForCellCol = ~(~validValues | possibleInOtherCellsCol);
            //if the cell has only one valid value
            if (SudokuSolverUtility.CountBits(possibleForCellBox) == 1)
            {
                return possibleForCellBox;
            }
            // check if there is a hidden ningles in the row
            if (SudokuSolverUtility.CountBits(possibleForCellRow) == 1)
            {
                return possibleForCellRow;
            }
            // check if there is a hidden ningles in the column
            if (SudokuSolverUtility.CountBits(possibleForCellCol) == 1)
            {
                return possibleForCellCol;
            }

            // if one of the row, col, box is not 0 and not 1 then the board has no solution
            if (possibleForCellBox != 0 || possibleForCellRow != 0 || possibleForCellCol != 0)
            {
                return -1;
            }
            // if the cell has no valid values
            return 0;
        }



        /*Get possible values in other cells in the box */

        private static int GetPossibleValuesForOtherCellsInBox(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int sqrSize, int cellInBox, int boxNumber, int boardSize, int sameBoxCell, int possibleInOtherCells)
        {
            int rowInsideBox, colInsideBox;
            //if the box is not the current box
            if (sameBoxCell != cellInBox)
            {
                //get the row and column of the cell inside the box
                rowInsideBox = sqrSize * (boxNumber / sqrSize) + sameBoxCell / sqrSize;
                colInsideBox = (boxNumber % sqrSize) * sqrSize + sameBoxCell % sqrSize;
                //if the cell is empty
                if (board[rowInsideBox, colInsideBox] == 0)
                {
                    // add the valid values of the cell to the possible values for all the cells in the box
                    possibleInOtherCells |= SudokuSolverUtility.GetValidValuesForCell(validValuesRow, validValuesColumn, validValuesBox, sqrSize, rowInsideBox, colInsideBox, boardSize);
                }
            }
            return possibleInOtherCells;


        }

        /*Get possible values in other cells in column*/
        private static int GetPossibleValuesForOtherCellsInCol(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int sqrSize, int row, int col, int boardSize, int sameColCell, int possibleInOtherCells)
        {
            if (sameColCell != row)
            {
                // if the cell is empty
                if (board[sameColCell, col] == 0)
                {
                    // add the valid values of the cell to the possible values for all the cells in the column
                    possibleInOtherCells |= SudokuSolverUtility.GetValidValuesForCell(validValuesRow, validValuesColumn, validValuesBox, sqrSize, sameColCell, col, boardSize);
                }
            }
            return possibleInOtherCells;
        }

        /*Get possible values in other cells in row*/
        private static int GetPossibleValuesForOtherCellsInRow(byte[,] board, int[] validValuesRow, int[] validValuesColumn, int[] validValuesBox, int sqrSize, int row, int col, int boardSize, int sameRowCell, int possibleInOtherCells)
        {

            if (sameRowCell != col)
            {
                // if the cell is empty
                if (board[row, sameRowCell] == 0)
                {
                    // add the valid values of the cell to the possible values for all the cells in the row
                    possibleInOtherCells |= SudokuSolverUtility.GetValidValuesForCell(validValuesRow, validValuesColumn, validValuesBox, sqrSize, row, sameRowCell, boardSize);
                }
            }
            return possibleInOtherCells;
        }





    }
}
