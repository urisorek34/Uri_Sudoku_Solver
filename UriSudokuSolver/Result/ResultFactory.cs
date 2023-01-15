using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.Board;
using UriSudokuSolver.CustomExceptions;
using UriSudokuSolver.UserCommunication;

namespace UriSudokuSolver.Result
{
    /*Result factory for generating results of games*/
    internal static class ResultFactory
    {
        public static IGameResult GetResult(EnumConstants.GameType gameType, ISolver solver, GameBoard gameBoard)
        {
            switch (gameType)
            {
                case EnumConstants.GameType.SODOKU:
                    return new SudokuResult(solver, gameBoard);
                default:
                    throw new NoSuchGameException("The game does not exists");
            }
        }
    }
}
