using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.Board;
using UriSudokuSolver.SolvingAlgorithm;

namespace UriSudokuSolver
{
    /*Class for solving a sudoku in backtracking using bit board.*/
    public class SudokuSolver : ISolver
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
        // Board size
        private int _boardSize;
        // Dictionary that contains cell index as key and list of peer index (the cells that it affects --> in the same row, column or box)
        private static Dictionary<int, List<int>> _peersForCell = new Dictionary<int, List<int>>();
        // queue for the peers of a changed cell
        private List<int> _peersQueue;



        /*Constractor for the solver. Initialize all the solver properties.*/
        public SudokuSolver(SudokuBoard board)
        {
            _board = board.GetBoard();
            _sqrSize = (int)Math.Sqrt(board.GetRows());
            _boardSize = board.GetRows();
            _peersForCell = SudokuSolverUtility.SetPeers(_board,_boardSize, _sqrSize);
            // Initialize board masks
            SudokuSolverUtility.InitializeBoardMasks(out _masks, _boardSize);
            // Initialize the valid values for each row, column and box
            SudokuSolverUtility.SetValidValues(_board, _masks, out _validValuesRow, out _validValuesColumn, out _validValuesBox, _boardSize);
            _result = "";
            _valuesSaver = new Stack<int>();
            _peersQueue = SudokuSolverUtility.SetStartPeersQueue(_board, _boardSize);


        }

        /*Function tries to solve the sudoku board.*/
        public byte[,] Solve()
        {
            if (SolveOptimizedSudoku())
            {
                _result = "a valid solution!";
            }
            else
            {
                _result = "no solution!";
            }
            return _board;

        }

        /*Function Solves the sudoku board using backtracking using bit board.*/
        private bool SolveOptimizedSudoku()
        {
            //Backtracking using bitwise and human solving tactics
            // 1.Try to solve the board using human solving tactics until there is nothing to solve using human tactics
            // 2.Find the empty cell with the least possible values
            // 3.Try to solve the board using backtracking (place a value in the cell and try to solve the board and call the solving function recursively --> from stage 1.)
            // 4.If the board is solved return true
            // 5.If the board is not solved, remove the value from the cell and try to solve the board with the next possible value
            // 6.If there is no possible value for the cell, return false

            int totalChanged = TryToSolveWithHumanTactics();
            if (totalChanged == -1)
            {
                return false;
            }

            int emptyCellRow, emptyCellCol;
            // Find the cell with the minimum number of valid values
            SudokuSolverUtility.FindBestEmptyCell(_board, _validValuesRow, _validValuesColumn, _validValuesBox, _sqrSize, _boardSize, out emptyCellRow, out emptyCellCol);
            if (emptyCellRow == -1)
            {

                return true;
            }

            // if not solved after human tactics, try to solve using backtracking brute force
            if (TryBruteForce(emptyCellRow, emptyCellCol))
            {
                return true;
            }
            SudokuSolverUtility.CleanStack(_board, _valuesSaver, _validValuesRow, _validValuesColumn, _validValuesBox, _masks, _sqrSize, totalChanged, _boardSize);
            return false;
        }

        /*Try to solve the sudoku board using backtracing brute force.*/
        private bool TryBruteForce(int emptyCellRow, int emptyCellCol)
        {
            // Try to solve the sudoku by assigning a value to the empty cell
            for (int valueIndex = 1; valueIndex <= _boardSize; valueIndex++)
            {
                // Check if is safe to try to assign the value to the empty cell
                if (SudokuSolverUtility.IsSafe(emptyCellRow, emptyCellCol, valueIndex - 1, _validValuesRow, _validValuesColumn, _validValuesBox, _masks, _sqrSize))
                {
                    // Assign the value to the empty cell
                    AssignValueInBoard(emptyCellRow, emptyCellCol, valueIndex);
                    // Try to solve the sudoku with the new value
                    if (SolveOptimizedSudoku())
                    {
                        return true;
                    }
                    //Undo the assignment after the recursive path failed
                    RemoveValueFromBoard(emptyCellRow, emptyCellCol);

                }

            }
            return false;

        }

        /*Assign value to the board.*/
        private void AssignValueInBoard(int row, int col, int value)
        {
            _board[row, col] = (byte)value;
            SudokuSolverUtility.UpdateValidValues(_masks, _validValuesRow, _validValuesColumn, _validValuesBox, _sqrSize, row, col, value);
            _peersQueue.Add(row * _boardSize + col);
            
        }

        /*Remove signed values from the board*/
        private void RemoveValueFromBoard(int row, int col)
        {
            SudokuSolverUtility.AddValueToValidValues(_board, _validValuesRow, _validValuesColumn, _validValuesBox, _masks, _sqrSize, row, col);
            _board[row, col] = 0;

        }

        /*Try to solve in human tactics untill there is no more cells that can be solved in human tactics.*/
        private int TryToSolveWithHumanTactics()
        {
            int isSolved, totalChanged = 0;
            while (_peersQueue.Count != 0)
            {
                isSolved = HumanTactics.HumanTacticsSolver(_valuesSaver, _peersForCell, _peersQueue, _board, _validValuesRow, _validValuesColumn, _validValuesBox, _masks, _sqrSize, _boardSize, _peersQueue[0]);
                _peersQueue.RemoveAt(0);
                // If is solved is -1 there is no solution to the board 
                if (isSolved == -1)
                {
                    SudokuSolverUtility.CleanStack(_board, _valuesSaver, _validValuesRow, _validValuesColumn, _validValuesBox, _masks, _sqrSize, totalChanged, _boardSize);
                    return -1;
                }                
                // remove Duplicates peers  
                _peersQueue = _peersQueue.ToHashSet().ToList();
                totalChanged += isSolved;
            }
            return totalChanged;
        }

        /*To string function, return the result of the board.*/
        public override string ToString()
        {
            return _result;
        }

    }
}
