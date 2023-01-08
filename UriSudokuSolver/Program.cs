using UriSudokuSolver;
using UriSudokuSolver.UserCommunication;
using UriSudokuSolver.UserCommunication.Input_Output;

const EnumConstants.GameType GAME_TYPE = EnumConstants.GameType.SODOKU;
const EnumConstants.RedearType COMMUNICATION_TYPE = EnumConstants.RedearType.CONSOLE;


IUserCommunication userCommunication = UserCommunicationFactory.GetUserCommunication(COMMUNICATION_TYPE, GAME_TYPE);
userCommunication.Communicate();



