using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver.UserCommunication.Input_Output
{
    /*Factory that creates user communication objects.*/
    internal static class UserCommunicationFactory
    {
        /*Creates a UserCommunication object depends on the communication type.*/
        public static IUserCommunication GetUserCommunication(EnumConstants.ReaderType communicationType, EnumConstants.GameType gameType)
        {
            switch (communicationType)
            {
                case EnumConstants.ReaderType.CONSOLE:
                    return new ConsoleUserCommunication(gameType);
                default:
                    throw new NoSuchReadingTypeException("No such communication type.");
            }
        }
    }
}
