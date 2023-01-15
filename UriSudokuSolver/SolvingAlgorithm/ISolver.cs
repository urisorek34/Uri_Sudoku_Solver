using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Interface for the solvers of the abstract game board*/
    interface ISolver
    {
        public byte[,] Solve();
    }
}
