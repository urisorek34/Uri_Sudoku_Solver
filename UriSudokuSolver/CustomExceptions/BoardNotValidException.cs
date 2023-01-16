using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver
{
    /*Exception for when a board is not a valid board.*/
    public class BoardNotValidException : GameException
    {

        public BoardNotValidException(string message) : base(message)
        {
        }
    }

}
