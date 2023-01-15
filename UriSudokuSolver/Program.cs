using UriSudokuSolver;
using UriSudokuSolver.UserCommunication;
using UriSudokuSolver.UserCommunication.Input_Output;
/*This is the main program*/

//Choose the game type
const EnumConstants.GameType GAME_TYPE = EnumConstants.GameType.SODOKU;
//Choose the input type
const EnumConstants.ReaderType COMMUNICATION_TYPE = EnumConstants.ReaderType.CONSOLE;

//Select the chosen user communication 
IUserCommunication userCommunication = UserCommunicationFactory.GetUserCommunication(COMMUNICATION_TYPE, GAME_TYPE);
userCommunication.Communicate();



