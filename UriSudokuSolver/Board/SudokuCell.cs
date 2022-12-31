using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.Board
{
    internal class SudokuCell:Cell
    {
        /*The possible values of the cell.*/
        private List<char> PossibleValues { get; set; }

        /*Constructor for the ccell.*/
        public SudokuCell(char value, int size)
        {
            this.Value = value;
            this.PossibleValues = new List<char>();
            for (int i = 1; i <= size; i++)
            {
                this.PossibleValues.Add((char)(i + '0'));
            }
        }
    }
}
