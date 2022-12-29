using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.UserCommunication.Validation;

namespace UriSudokuSolver.UserCommunication.Reader
{
    internal class FileBoardReader: IBoardReader
    {
        private string filePath;
        private IValidator validator;
        private GameBoard<int> board;


        public FileBoardReader(GameBoard<int> board, string filePath, string validatorType)
        {
            this.board = board;
            this.filePath = filePath;
            validator = ValidationFactory.GetValidator(validatorType);
        }

        public void ReadBoard()
        {
            string gameBoard = File.ReadAllText(filePath);
            validator.ValidateBoard(gameBoard);
            board.FillBoard(gameBoard);
        }

    }
}
