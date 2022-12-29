using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.UserCommunication.Validation;

namespace UriSudokuSolver.UserCommunication.Reader
{
    /*Class reads a board form a file.*/
    internal class FileBoardReader: IBoardReader
    {
        private string filePath;
        private IValidator validator;
        private GameBoard<char> board;


        /*Constractor for the file board reader.*/
        public FileBoardReader(GameBoard<char> board, string filePath, string validatorType)
        {
            this.board = board;
            this.filePath = filePath;
            validator = ValidationFactory.GetValidator(validatorType);
        }

        /*Function reads a board from a file to the game board.*/
        public void ReadBoard()
        {
            string gameBoard = File.ReadAllText(filePath);
            validator.ValidateBoard(gameBoard);
            board.FillBoard(gameBoard);
        }

    }
}
