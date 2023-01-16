namespace UriSudokuSolver.CustomExceptions
{
    /*Exception for when a file path is not valid.*/
    public class IlegalFilePathInputException : GameException
    {
        public IlegalFilePathInputException(string message) : base(message)
        {
        }
    }

}
