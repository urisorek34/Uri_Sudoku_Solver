using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Represent abstract game board*/
    abstract class GameBoard
    {

        protected byte[,] board;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoard{T}"/> class.
        /// </summary>
        /// <param name="size">The size of the board.</param>
        public GameBoard(int size)
        {
            board = new byte[size, size];
        }

        public GameBoard(int rows, int cols)
        {
            board = new byte[rows, cols];
        }

        /*copy constractor*/
        public GameBoard(byte[,] board)
        {
            this.board = new byte[board.GetLength(0), board.GetLength(1)];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    this.board[i, j] = board[i, j];
                }
            }
        }

        /*Indexer for the game board.*/
        public byte this[int row, int col]
        {
            get
            {
                return board[row, col];
            }
            set
            {
                board[row, col] = value;
            }
        }
        /*Get board rows number of rows*/
        public int GetRows()
        {
            return board.GetLength(0);
        }
        /*Get board number of cols*/
        public int GetCols()
        {
            return board.GetLength(1);
        }
        /*Return instance of the board matrix */
        public byte[,] GetBoard()
        {
            return board;
        }
        public void SetBoard(byte[,] board)
        {
            this.board = board;
        }
        public abstract void FillBoard(string boardString);

        public abstract void ValidateBoard();
    }
}