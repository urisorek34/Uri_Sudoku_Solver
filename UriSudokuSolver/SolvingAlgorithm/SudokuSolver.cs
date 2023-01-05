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
        //result string
        private string _result;

        private Stack<int> _valuesSaver;


        /*Constractor for the solver. Initialize all the solver properties.*/
        public SudokuSolver(SudokuBoard board)
        {
            this.board = board.GetBoard();
            this.sqrSize = (int)Math.Sqrt(board.GetRows());

            // Initialize board masks
            SudokuSolverUtility.InitializeBoardMasks(this.board, out masks);
            // Initialize the valid values for each row, column and box
            SudokuSolverUtility.SetValidValues(this.board, masks, out validValuesRow, out validValuesColumn, out validValuesBox);
            _result = "";
            _valuesSaver = new Stack<int>();

        }

        /*Function tries to solve the sudoku board.*/
        public int[,] Solve()
        {
            if (SolveOptimizedSudoku())
            {
                _result = "Solution!";
            }
            else
            {
                _result = "No solution!";
            }
            return board;

        }

        /*Function Solves the sudoku board using backtracking using bit board.*/
        private bool SolveOptimizedSudoku()
        {
            // copy the current matrixes to the test matrixes 


            // Human solving algorithm
            // 1. Find a cell with only one possible value
            // 2. Fill it with the value
            // 3. Repeat until no more cells with only one possible value
            // 4. If there are no more cells with only one possible value, find a cell with the least possible values
            // 5. Fill it with one of the possible values
            // 6. Repeat until the board is solved
            // 7. If the board is not solved, backtrack and try another value
            int isSolved, totalChanged = 0;
            do
            {
                isSolved = SudokuSolverUtility.HumanTactics(_valuesSaver, board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize);
                if (isSolved == -1)
                {
                    SudokuSolverUtility.CleanStack(board, _valuesSaver, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, totalChanged);
                    return false;
                }
                totalChanged += isSolved;
            } while (isSolved != 0);


            int emptyCellRow, emptyCellCol;
            // Find the cell with the minimum number of valid values
            SudokuSolverUtility.FindBestEmptyCell(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, out emptyCellRow, out emptyCellCol);

            if (emptyCellRow == -1)
            {
                return true;
            }

            // Try to solve the sudoku by assigning a value to the empty cell
            for (int valueIndex = 1; valueIndex <= board.GetLength(0); valueIndex++)
            {
                // Check if is safe to try to assign the value to the empty cell
                if (SudokuSolverUtility.IsSafe(emptyCellRow, emptyCellCol, valueIndex - 1, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize))
                {
                    // Assign the value to the empty cell
                    board[emptyCellRow, emptyCellCol] = valueIndex;
                    SudokuSolverUtility.UpdateValidValues(masks, validValuesRow, validValuesColumn, validValuesBox, sqrSize, emptyCellRow, emptyCellCol, valueIndex);

                    // Try to solve the sudoku with the new value
                    if (SolveOptimizedSudoku())
                    {
                        return true;
                    }
                    //Undo the assignment after the recursive path failed
                    SudokuSolverUtility.AddValueToValidValues(board, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, emptyCellRow, emptyCellCol);
                    board[emptyCellRow, emptyCellCol] = 0;

                    //RestoreInitialValues(testMatrix, testValidValuesRow, testValidValuesColumn, testValidValuesBox);
                }

            }
            SudokuSolverUtility.CleanStack(board, _valuesSaver, validValuesRow, validValuesColumn, validValuesBox, masks, sqrSize, totalChanged);
            return false;
        }



        /*To string function*/
        public override string ToString()
        {
            return _result;
        }

    }
}
