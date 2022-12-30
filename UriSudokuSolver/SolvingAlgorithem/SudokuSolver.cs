using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    class SudokuSolver : ISolver
    {
        private SudokuBoard board;
        /// <summary>
        /// Initializes a new instance of the <see cref="SudokuSolver"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public SudokuSolver(SudokuBoard board)
        {
            this.board = board;

        }

        /*Function tries to solve the sudoku board.*/
        public void Solve()
        {
            if (SolveOptimizedSudoku(SudukuSolverUtility.CacheValidValues(board)))
            {
                Console.WriteLine("Solved!");
            }
            else
            {
                Console.WriteLine("No solution!");
            }
        }

        /*Function Solves the sudoku board using backtracking.*/
        private bool SolveOptimizedSudoku(Dictionary<(int, int), List<char>> cache)
        {

            (int, int) blanck = SudukuSolverUtility.FindFirstEmpty(board);
            int row = blanck.Item1;
            int col = blanck.Item2;
            // Recursion stoping condition.
            if (row == board.GetRows())
            {
                return true;
            }
            // Go over all the options for each empty place in the board.
            for (int value = 0; value < cache[blanck].Count; value++)
            {

                if (SudukuSolverUtility.IsLegalValue(board, row, col, cache[blanck][value]))
                {
                    board[row, col] = cache[blanck][value];

                    if (SolveOptimizedSudoku(cache))
                    {
                        return true;
                    }
                    board[row, col] = '0';

                }

            }

            return false;

        }

    }
}
