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
        private int minValue;
        private int maxValue;
        private int rows;
        private int cols;

        /// <summary>
        /// Initializes a new instance of the <see cref="SudokuSolver"/> class.
        /// </summary>
        /// <param name="board">The board.</param>
        public SudokuSolver(SudokuBoard board)
        {
            this.board = board;
            minValue = board.GetMinValue();
            maxValue = board.GetMaxValue();
            rows = board.GetRows();
            cols = board.GetCols();
            
        }
       




    }
}
