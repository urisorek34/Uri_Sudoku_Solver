using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.Board
{
    internal class SudokuCell : Cell
    {

        /*Constructor for the ccell.*/
        public SudokuCell(int value, int size)
        {
            this.Value = value;
        }
    }
}
