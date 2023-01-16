using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver.UserCommunication.Reader
{
    /*A factory for board readers*/
    internal class ReaderFactory
    {
        /*Creates a board reader according to the given reader type.*/
        public static IBoardReader GetReader(EnumConstants.ReaderType readerType, EnumConstants.GameType boardType, string filePath = "")
        {
            switch (readerType)
            {
                case EnumConstants.ReaderType.CONSOLE:
                    return new ConsoleBoardReader(boardType);
                case EnumConstants.ReaderType.FILE:
                    return new FileBoardReader(filePath, boardType);
                default:
                    throw new NoSuchReadingTypeException($"Invalid reader type {readerType}.");
            }
        }
    }
}
