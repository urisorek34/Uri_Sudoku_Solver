using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.Board
{
    /*Class for a cell in a board.*/
    abstract class Cell
    {
        /*The value of the cell.*/
        public char Value { get; set; }
        /*The row of the cell.*/
        public int Row { get; set; }
        /*The column of the cell.*/
        public int Column { get; set; }
        /*The box of the cell.*/
        public int Box { get; set; }

    }
}
