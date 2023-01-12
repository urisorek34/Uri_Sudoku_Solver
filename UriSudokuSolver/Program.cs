using UriSudokuSolver;
using UriSudokuSolver.UserCommunication;
using UriSudokuSolver.UserCommunication.Input_Output;

const EnumConstants.GameType GAME_TYPE = EnumConstants.GameType.SODOKU;
const EnumConstants.ReaderType COMMUNICATION_TYPE = EnumConstants.ReaderType.CONSOLE;


IUserCommunication userCommunication = UserCommunicationFactory.GetUserCommunication(COMMUNICATION_TYPE, GAME_TYPE);
userCommunication.Communicate();



