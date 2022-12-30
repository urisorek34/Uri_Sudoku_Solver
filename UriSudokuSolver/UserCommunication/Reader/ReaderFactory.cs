using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.UserCommunication.Reader
{
    /*A factory for board readers*/
    internal class ReaderFactory
    {
        /*Creates a board reader according to the given reader type.*/
        public static IBoardReader GetReader(string readerType,string validatorType,GameBoard<char> gameBoard, string filePath = "")
        {
            switch (readerType)
            {
                case "console":
                    return new ConsoleBoardReader(gameBoard,validatorType);
                case "file":
                    return new FileBoardReader(gameBoard,filePath,validatorType);
                default:
                    throw new ArgumentException("Invalid reader type.");
            }
        }
    }
}
