using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.Board
{
    /*Class for getting the result of the game*/
    internal class SudokuResult : IGameResult
    {
        private byte[,] _resultMatrixBoard;
        private GameBoard _sudokuBoard;
        private ISolver _sudokuSolver;

        /*Constractor for the result of the game*/
        public SudokuResult(ISolver sudokuSolver,GameBoard sudokuBoard)
        {
            _sudokuSolver = sudokuSolver;
            _sudokuBoard = sudokuBoard;
        }

        /*Get the result of the game in string format*/
        public string GetResult()
        {
            return RunSolverInParallel();
        }


        /*Runs the solver in parallel --> once backwards once forward, with two differnt tasks. */
        private string RunSolverInParallel()
        {
            
            
            //calculating the time it took to solve
            Stopwatch sw = new Stopwatch();
            sw.Start();
            RunSolver();
            sw.Stop();
            // Sets the board in the gameBoard reference.
            _sudokuBoard.SetBoard(_resultMatrixBoard);
            //return the result of the game
            return "The sudoku has " + _sudokuSolver + "\nand it took " + sw.ElapsedMilliseconds + " milliseconds to solve.";

        }
        /*Runs the solver in the direction of the bool parameter.*/
        private void RunSolver()
        {
            _resultMatrixBoard = _sudokuSolver.Solve();
        }


    }
    
}
