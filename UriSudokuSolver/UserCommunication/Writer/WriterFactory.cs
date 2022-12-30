using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver.UserCommunication.Writer
{
    /*Factory for board writers.*/
    internal class WriterFactory
    {
        /*Returns a new instance of a writer object depands on the given writer type.*/
        public static IBoardWriter GetWriter(EnumConstants.GameType writerType)
        {
            switch (writerType)
            {
                case EnumConstants.GameType.SODOKU:
                    return new SudokuBoardWriter();
                default:
                    throw new NoSuchGameException($"The Game {writerType} Not Found.");
            }
        }
    }
}
