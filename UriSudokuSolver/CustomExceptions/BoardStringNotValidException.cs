namespace UriSudokuSolver
{
    /*Exception for when a board string is not valid.*/
    public class BoardStringNotValidException : BoardNotValidException
    {
        public BoardStringNotValidException(string message) : base(message)
        {
        }
    }
}
