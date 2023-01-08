using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.UserCommunication
{
    /*Class for enum constants used in user communication.*/
    public static class EnumConstants
    {
        /*Enum for the different types of Readers.*/
        public enum RedearType
        {
            CONSOLE,
            FILE,
            EXIT
        }
        /*Enum for differnt types of games.*/
        public enum GameType
        {
            SODOKU
        }

        /*Enum for user commands options.*/
        public enum UserCommand
        {
            SOLVE,
            RULES,
            MENU,
            EXIT
        }

    }
}
