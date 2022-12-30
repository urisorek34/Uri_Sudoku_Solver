using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver.UserCommunication.Reader
{
    /*A factory for board readers*/
    internal class ReaderFactory
    {
        /*Creates a board reader according to the given reader type.*/
        public static IBoardReader GetReader(string readerType,string boardType, string filePath = "")
        {
            switch (readerType)
            {
                case "console":
                    return new ConsoleBoardReader(boardType);
                case "file":
                    return new FileBoardReader(filePath,boardType);
                default:
                    throw new NoSuchReadingTypeException($"Invalid reader type {readerType}.");
            }
        }
    }
}
