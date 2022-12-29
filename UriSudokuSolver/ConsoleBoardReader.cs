using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Class is responsable of reading a game board from the console.*/
    class ConsoleBoardReader:IBoardReader
    {
        private GameBoard<int> board;

        /*Constractor for the console board reader.*/
        public ConsoleBoardReader(GameBoard<int> board)
        {
            this.board = board;
        }
        
        /*Function read a board from the console to the game board.*/
        public void ReadBoard() 
        {
            string line = Console.ReadLine();
            // TODO: check if the board is valid.
            board.FillBoard(line);
        }
    }
}
