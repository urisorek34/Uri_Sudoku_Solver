using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.UserCommunication
{
    /*Interface for writing the solved board*/
    internal interface IBoardWriter
    {
        public string WriteBoard(GameBoard<char> gameBoard);
    }
}

