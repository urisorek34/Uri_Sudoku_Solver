using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Represent abstract game board*/
    public abstract class GameBoard
    {

        protected byte[,] _board;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoard{T}"/> class.
        /// </summary>
        /// <param name="size">The size of the board.</param>
        public GameBoard(int size)
        {
            _board = new byte[size, size];
        }

        public GameBoard(int rows, int cols)
        {
            _board = new byte[rows, cols];
        }

        /*copy constractor*/
        public GameBoard(byte[,] board)
        {
            _board = new byte[board.GetLength(0), board.GetLength(1)];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    _board[i, j] = board[i, j];
                }
            }
        }

        /*Indexer for the game board.*/
        public byte this[int row, int col]
        {
            get
            {
                return _board[row, col];
            }
            set
            {
                _board[row, col] = value;
            }
        }
        /*Get board rows number of rows*/
        public int GetRows()
        {
            return _board.GetLength(0);
        }
        /*Get board number of cols*/
        public int GetCols()
        {
            return _board.GetLength(1);
        }
        /*Return instance of the board matrix */
        public byte[,] GetBoard()
        {
            return _board;
        }
        public void SetBoard(byte[,] board)
        {
            _board = board;
        }
        public abstract void FillBoard(string boardString);

        public abstract void ValidateBoard();
        public abstract bool IsFull();
    }
}