namespace UriSudokuSolver.CustomExceptions
{
    /*Exception for when a board string is not the size of the given size.*/
    public class BoardStringSizeIsNotValidException : BoardStringNotValidException
    {
        public BoardStringSizeIsNotValidException(string message) : base(message)
        {

        }
    }
}
