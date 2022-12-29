using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.CustomExceptions
{
    internal class NoSuchGameException : ArgumentException
    {
        public NoSuchGameException(string message) : base(message)
        {
            
        }
    }
}
