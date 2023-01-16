using UriSudokuSolver.UserCommunication.Rules;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver.UserCommunication
{
    /*abstract class for rules of a game.*/
    internal abstract class GameRules
    {
        protected string _rules;

        /*Function prints game rules.*/
        public void ShowRules()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(_rules);
            Console.ResetColor();
        }
        /*static method to determine what game is played and return the right Game rules.*/
        public static GameRules GetRules(EnumConstants.GameType gameType)
        {
            switch (gameType)
            {
                case EnumConstants.GameType.SODOKU:
                    return new SudokuRules();
                default:
                    throw new NoSuchGameException("The game does not exists");
            }
        }
    }
}
