using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver.UserCommunication.Writer
{
    /*Interface for writing the sudoku game board.*/
    internal class SudokuBoardWriter : IBoardWriter
    {
        /*Constractor for the SudokuBoardWriter class.*/
        public SudokuBoardWriter()
        {
        }

        /*Return a prity sudoku board string*/
        public string WriteBoard(GameBoard board)
        {
            string boardString = "";
            int size = board.GetRows();
            int sqrtSize = (int)Math.Sqrt(size);
            //first up border
            for (int k = 0; k < size + sqrtSize; k++)
            {
                boardString += "-";
            }
            boardString += "\n";
            for (int i = 0; i < size; i++)
            {
                // Add a line break after every sqrtSize rows
                for (int j = 0; j < size; j++)
                {
                    if (j % sqrtSize == 0)
                    {
                        boardString += "|";
                    }
                    boardString += (char)(board[i, j] + '0');
                }
                boardString += "|";
                if (i % sqrtSize == sqrtSize - 1)
                {
                    boardString += "\n";
                    // Add "-" line after every sqrtSize rows
                    for (int k = 0; k < size + sqrtSize; k++)
                    {
                        boardString += "-";
                    }
                    boardString += "\n";
                }
                else
                {
                    boardString += "\n";
                }
            }
            return boardString;
        }
    }

}
