using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.UserCommunication.Rules
{
    /*Representing the rules of the game sudoku.*/
    internal class SudokuRules : GameRules
    {
        // constant filled with the rules of the game sudoku.
        private const string SUDOKU_RULES = "\nSudoku Rules:\n" +
            "Sudoku is played on a grid of N x N spaces.\n Within the rows and columns are N “squares” " +
            "(made up of Sqrt(N) x Sqrt(N) spaces).\n Each row, column and square (N spaces each) needs to be filled " +
            "out with the numbers 1-N, without repeating any numbers within the row, column or square.\n" +
            "in this game the empty spaces are represented by 0.\n";
        public SudokuRules() 
        {
            _rules = SUDOKU_RULES;

        }

    }
}
