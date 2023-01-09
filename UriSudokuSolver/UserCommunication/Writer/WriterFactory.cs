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
        public static IBoardWriter GetWriter(EnumConstants.RedearType writerType, string filePath = "")
        {
            switch (writerType)
            {
                case EnumConstants.RedearType.CONSOLE:
                    return new SudokuBoardWriter();
                case EnumConstants.RedearType.FILE:
                    return new SudokuBoardFileWriter(filePath);
                default:
                    throw new NoSuchGameException($"The Game {writerType} Not Found.");
            }
        }

        
    }
}
