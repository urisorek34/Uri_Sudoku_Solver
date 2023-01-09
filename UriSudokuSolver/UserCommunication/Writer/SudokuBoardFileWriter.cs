using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.UserCommunication.Writer
{
    /*Writer to file*/
    public class SudokuBoardFileWriter : IBoardWriter
    {
        // file path to write to
        private string _filePath;
        /*Empty constractor for the SudokuBoardWriter class.*/
        public SudokuBoardFileWriter(string filePath)
        {
            _filePath = filePath;
        }
        

        /*Write board to solved(filename).txt*/
        public void WriteBoard(GameBoard gameBoard)
        {
            string fileName = "_solved.txt";

            // write to file the solved string
            using (StreamWriter file = new StreamWriter(_filePath + fileName))
            {
                file.WriteLine(gameBoard.ToString());
                SudokuBoardWriter sudokuBoardWriter = new SudokuBoardWriter();
                sudokuBoardWriter.WriteBoard(gameBoard);
            }
        }
    }
}
