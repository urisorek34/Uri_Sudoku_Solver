namespace UriSudokuSolver.CustomExceptions
{
    /*Exception for when a reader type not exist.*/
    internal class NoSuchReadingTypeException : GameException
    {
        public NoSuchReadingTypeException(string message) : base(message)
        {
        }
    }
}
