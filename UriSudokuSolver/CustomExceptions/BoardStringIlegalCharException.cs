namespace UriSudokuSolver.CustomExceptions
{
    /*Exception for when a board string has an unvalid char.*/
    public class BoardStringIlegalCharException : BoardStringNotValidException
    {
        public BoardStringIlegalCharException(string message) : base(message)
        {
        }
    }

}
