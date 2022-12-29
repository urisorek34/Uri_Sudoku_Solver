using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.CustomExceptions
{
    /*Exception for when a board string has an unvalid char.*/
    internal class BoardStringIlegalCharException : BoardStringNotValidException
    {
        public BoardStringIlegalCharException(string message) : base(message)
        {
        }
    }

}
