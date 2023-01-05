using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.SolvingAlgorithm;

namespace UriSudokuSolver
{
    /*Class for solving a sudoku in backtracking using bit board.*/
    class SudokuSolver : ISolver
    {
        // the board to solve
        private byte[,] _board;
        // a box size
        private int _sqrSize;
        // valid values in a cell in a row represented by a binary number
        private int[] _validValuesRow;
        // valid values in a cell in a column represented by a binary number
        private int[] _validValuesColumn;
        // valid values in a cell in a box represented by a binary number
        private int[] _validValuesBox;
        // masks for each value in the board
        private int[] _masks;
        //result string
        private string _result;
        // Stack for saving changes done to the board in the human tactics
        private Stack<int> _valuesSaver;


        /*Constractor for the solver. Initialize all the solver properties.*/
        public SudokuSolver(SudokuBoard board)
        {
            _board = board.GetBoard();
            _sqrSize = (int)Math.Sqrt(board.GetRows());

            // Initialize board masks
            SudokuSolverUtility.InitializeBoardMasks(_board, out _masks);
            // Initialize the valid values for each row, column and box
            SudokuSolverUtility.SetValidValues(_board, _masks, out _validValuesRow, out _validValuesColumn, out _validValuesBox);
            _result = "";
            _valuesSaver = new Stack<int>();

        }

        /*Function tries to solve the sudoku board.*/
        public byte[,] Solve()
        {
            if (SolveOptimizedSudoku())
            {
                _result = "Solution!";
            }
            else
            {
                _result = "No solution!";
            }
            return _board;

        }

        /*Function Solves the sudoku board using backtracking using bit board.*/
        private bool SolveOptimizedSudoku()
        {
            // Backtracking using bit board and human solving tactics
            // 1. Find a cell with only one possible value
            // 2. Fill it with the value
            // 3. Repeat until no more cells with only one possible value
            // 4. If there are no more cells with only one possible value, find a cell with the least possible values
            // 5. Fill it with one of the possible values
            // 6. Repeat until the board is solved

            int isSolved, totalChanged = 0;
            do
            {
                isSolved = HumanTactics.HumanTacticsSolver(_valuesSaver, _board, _validValuesRow, _validValuesColumn, _validValuesBox, _masks, _sqrSize);
                // If is solved is -1 there is no solution to the board 
                if (isSolved == -1)
                {
                    SudokuSolverUtility.CleanStack(_board, _valuesSaver, _validValuesRow, _validValuesColumn, _validValuesBox, _masks, _sqrSize, totalChanged);
                    return false;
                }
                totalChanged += isSolved;
            } while (isSolved != 0);


            int emptyCellRow, emptyCellCol;
            // Find the cell with the minimum number of valid values
            SudokuSolverUtility.FindBestEmptyCell(_board, _validValuesRow, _validValuesColumn, _validValuesBox, _masks, _sqrSize, out emptyCellRow, out emptyCellCol);

            if (emptyCellRow == -1)
            {
                return true;
            }

            // Try to solve the sudoku by assigning a value to the empty cell
            for (int valueIndex = 1; valueIndex <= _board.GetLength(0); valueIndex++)
            {
                // Check if is safe to try to assign the value to the empty cell
                if (SudokuSolverUtility.IsSafe(emptyCellRow, emptyCellCol, valueIndex - 1, _validValuesRow, _validValuesColumn, _validValuesBox, _masks, _sqrSize))
                {
                    // Assign the value to the empty cell
                    _board[emptyCellRow, emptyCellCol] = (byte)valueIndex;
                    SudokuSolverUtility.UpdateValidValues(_masks, _validValuesRow, _validValuesColumn, _validValuesBox, _sqrSize, emptyCellRow, emptyCellCol, valueIndex);

                    // Try to solve the sudoku with the new value
                    if (SolveOptimizedSudoku())
                    {
                        return true;
                    }
                    //Undo the assignment after the recursive path failed
                    SudokuSolverUtility.AddValueToValidValues(_board, _validValuesRow, _validValuesColumn, _validValuesBox, _masks, _sqrSize, emptyCellRow, emptyCellCol);
                    _board[emptyCellRow, emptyCellCol] = 0;

                    //RestoreInitialValues(testMatrix, testValidValuesRow, testValidValuesColumn, testValidValuesBox);
                }

            }
            SudokuSolverUtility.CleanStack(_board, _valuesSaver, _validValuesRow, _validValuesColumn, _validValuesBox, _masks, _sqrSize, totalChanged);
            return false;
        }



        /*To string function, return the result of the board.*/
        public override string ToString()
        {
            return _result;
        }

    }
}
