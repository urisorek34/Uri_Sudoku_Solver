namespace UriSudokuSolver.CustomExceptions
{
    /*Exception for empty board.*/
    public class BoardIsEmptyException : BoardStringSizeIsNotValidException
    {
        public BoardIsEmptyException(string message) : base(message)
        {
        }
    }
}
