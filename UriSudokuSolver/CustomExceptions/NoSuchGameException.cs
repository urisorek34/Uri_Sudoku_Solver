using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.CustomExceptions
{
    /*Exception for when a game type does not exist.*/
    internal class NoSuchGameException : GameException
    {
        public NoSuchGameException(string message) : base(message)
        {
            
        }
    }
}
