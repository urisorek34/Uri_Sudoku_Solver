using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Class for solving a sudoku in backtracking using bit board.*/
    class SudokuSolver : ISolver
    {
        // the board to solve
        private int[,] board;
        // a box size
        private int sqrSize;
        // valid values in a cell in a row represented by a binary number
        private int[] validValuesRow;
        // valid values in a cell in a column represented by a binary number
        private int[] validValuesColumn;
        // valid values in a cell in a box represented by a binary number
        private int[] validValuesBox;
        // masks for each value in the board
        private int[] masks;


        /*Constractor for the solver. Initialize all the solver properties.*/
        public SudokuSolver(SudokuBoard board)
        {
            this.board = board.GetBoard();
            this.sqrSize = (int)Math.Sqrt(board.GetRows());

            // Initialize board masks
            SudukuSolverUtility.InitializeBoardMasks(this.board, out masks);
            // Initialize the valid values for each row, column and box
            SudukuSolverUtility.GetValidValues(this.board, masks, out validValuesRow, out validValuesColumn, out validValuesBox);

        }

        /*Function tries to solve the sudoku board.*/
        public int[,] Solve()
        {
            if (SolveOptimizedSudoku())
            {
                Console.WriteLine("Solved!");
            }
            else
            {
                Console.WriteLine("No solution!");
            }
            return board;

        }

        /*Function Solves the sudoku board using backtracking using bit board.*/
        private bool SolveOptimizedSudoku()
        {
            // copy the current matrixes to the test matrixes 
            int[,] testMatrix = SudukuSolverUtility.CopyMatrix(board);
            int[] testValidValuesRow = new int[validValuesRow.Length];
            int[] testValidValuesColumn = new int[validValuesColumn.Length];
            int[] testValidValuesBox = new int[validValuesBox.Length];
            CopyToTestArrays(testValidValuesRow, testValidValuesColumn, testValidValuesBox);

            //Optimisation algorithms here 
            SudukuSolverUtility.HiddenSingles(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize);

            int emptyCellRow, emptyCellCol;
            // Find the cell with the minimum number of valid values
            SudukuSolverUtility.FindBestEmptyCell(board, validValuesRow, validValuesColumn, validValuesBox, sqrSize, out emptyCellRow, out emptyCellCol);

            if (emptyCellRow == -1)
            {
                return true;
            }

            // Try to solve the sudoku by assigning a value to the empty cell
            for (int valueIndex = 0; valueIndex < board.GetLength(0); valueIndex++)
            {
                // Check if is safe to try to assign the value to the empty cell
                if (SudukuSolverUtility.IsSafe(emptyCellRow, emptyCellCol, valueIndex, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize))
                {
                    // Assign the value to the empty cell
                    board[emptyCellRow, emptyCellCol] = valueIndex + 1;
                    UpdateValidValues(emptyCellRow, emptyCellCol, valueIndex);

                    // Try to solve the sudoku with the new value
                    if (SolveOptimizedSudoku())
                    {
                        return true;
                    }
                    //Undo the assignment after the recursive path failed
                    RestoreInitialValues(testMatrix, testValidValuesRow, testValidValuesColumn, testValidValuesBox);
                }

            }
            return false;
        }

        /*Update the valid values for a cell.*/
        private void UpdateValidValues(int row, int col, int value)
        {
            validValuesRow[row] |= masks[value];
            validValuesColumn[col] |= masks[value];
            validValuesBox[(row / sqrSize) * sqrSize + col / sqrSize] |= masks[value];
        }

        /*Copy all the arrays to test arrays*/
        private void CopyToTestArrays(int[] testValidValuesRow, int[] testValidValuesColumn, int[] testValidValuesBox)
        {
            Array.Copy(validValuesRow, testValidValuesRow, validValuesRow.Length);
            Array.Copy(validValuesBox, testValidValuesBox, validValuesBox.Length);
            Array.Copy(validValuesColumn, testValidValuesColumn, validValuesColumn.Length);
        }

        /*Restore arrays values if the recursion fails*/
        private void RestoreInitialValues(int[,] testMatrix, int[] testValidValuesRow, int[] testValidValuesColumn, int[] testValidValuesBox)
        {
            board = testMatrix;
            validValuesRow = testValidValuesRow;
            validValuesColumn = testValidValuesColumn;
            validValuesBox = testValidValuesBox;
        }


    }
}
