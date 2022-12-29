using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.UserCommunication.Validation;

namespace UriSudokuSolver
{
    /*Class is responsable of reading a game board from the console.*/
    class ConsoleBoardReader : IBoardReader
    {
        private GameBoard<int> board;
        private IValidator boardValidator;

        /*Constractor for the console board reader.*/
        public ConsoleBoardReader(GameBoard<int> board, string boardValidatorType)
        {
            this.board = board;
            boardValidator = ValidationFactory.GetValidator(boardValidatorType);
        }

        /*Function read a board from the console to the game board.*/
        public void ReadBoard()
        {
            string line = Console.ReadLine();
            boardValidator.ValidateBoard(line);
            board.FillBoard(line);
        }
    }
}
