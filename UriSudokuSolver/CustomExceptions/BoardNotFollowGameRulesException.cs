namespace UriSudokuSolver.CustomExceptions
{
    /*Exception when a board is not follow the rules of the game that he is representing.*/
    public class BoardNotFollowGameRulesException : BoardNotValidException
    {
        public BoardNotFollowGameRulesException(string message) : base(message)
        {
        }
    }

}
