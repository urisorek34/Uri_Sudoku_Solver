using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.CustomExceptions
{
    /*Exception for empty board.*/
    public class BoardIsEmptyException : BoardStringSizeIsNotValidException
    {
        public BoardIsEmptyException(string message) : base(message)
        {
        }
    }
}
