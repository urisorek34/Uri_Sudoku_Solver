using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;
using UriSudokuSolver.UserCommunication;

namespace UriSudokuSolver.SolvingAlgorithem
{
    /*Factory for creating solvers.*/
    static class SolverFactory
    {
        /*Creates a solver for the given game board depends on the game type.*/
        public static ISolver GetSolver(GameBoard gameBoard, EnumConstants.GameType gameType)
        {
            switch (gameType)
            {
                case EnumConstants.GameType.SODOKU:
                    return new SudokuSolver((SudokuBoard)gameBoard);
                default:
                    throw new NoSuchGameException("the game not exists.");
            }
        }
    }
}
