using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Interface for reading a game board.*/
    internal interface IBoardReader
    {
        public GameBoard<char> ReadBoard();
        public GameBoard<char> GetRightBoard(int size);
    }
}
