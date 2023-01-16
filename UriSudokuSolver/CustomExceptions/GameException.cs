namespace UriSudokuSolver.CustomExceptions
{
    /*General exception for when a part of the game is not valid.*/
    public class GameException : Exception
    {
        public GameException(string message) : base(message)
        {
        }
    }
   
}
