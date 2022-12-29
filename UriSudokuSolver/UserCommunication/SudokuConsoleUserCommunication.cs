using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.UserCommunication.Writer;

namespace UriSudokuSolver.UserCommunication
{
    /*Class for user communication using the console.*/
    internal class SudokuConsoleUserCommunication : IUserCommunication
    {
        private IBoardReader sudokuReader;
        private SudokuSolver sudokuSolver;
        private IBoardWriter sudokuWriter;
        public void Communicate()
        {
            
        }
    }
}
