using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.CustomExceptions
{
    /*General exception for when a part of the game is not valid.*/
    public class GameException : Exception
    {
        public GameException(string message) : base(message)
        {
        }
    }
   
}
