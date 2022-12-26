using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    class SudokuSolver : ISolver
    {
        private SudokuBoard board;
        /// <summary>
        /// Initializes a new instance of the <see cref="SudokuSolver"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public SudokuSolver(SudokuBoard board)
        {
            this.board = board;
        }
        




    }
}
