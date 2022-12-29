using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Interface for validating a game board.*/
    internal interface IValidator
    {
        public void ValidateBoard(string gameBoard);
    }
}
