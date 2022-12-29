using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Exception for when the given size of the board is not valid.*/
    internal class BoardNotInRightSizeException : BoardNotValidException
    {
        public BoardNotInRightSizeException(string message) : base(message)
        {
        }
    }
}
