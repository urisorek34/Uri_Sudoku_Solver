using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver.UserCommunication.Validation
{
    /*Factory for validation of a board string.*/
    internal static class ValidationFactory
    {
        /*Creates a validation object for a the Board reader.*/
        public static IValidator GetValidator(EnumConstants.GameType validationType)
        {
            switch (validationType)
            {
                case EnumConstants.GameType.SODOKU:
                    return new SudokuBoardValidator();
                default:
                    throw new NoSuchGameException($"The Game {validationType} Not Found.");
            }
        }
    }
}
