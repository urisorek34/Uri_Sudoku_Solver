using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver
{
    /*Exception for when a board is not a valid board.*/
    public class BoardNotValidException : GameException
    {
        
        public BoardNotValidException(string message) : base(message)
        {
        }
    }
   
}
