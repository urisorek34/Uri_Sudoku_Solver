using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Exception for when a board is not a valid board.*/
    internal class BoardNotValidException : ArgumentException
    {
        
        public BoardNotValidException(string message) : base(message)
        {
        }
    }
   
}
