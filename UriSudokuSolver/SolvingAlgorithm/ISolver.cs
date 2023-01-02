using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /// <summary>
    /// Interface for solver of a game.
    /// </summary>
    interface ISolver
    {
        public int[,] Solve();
    }
}
