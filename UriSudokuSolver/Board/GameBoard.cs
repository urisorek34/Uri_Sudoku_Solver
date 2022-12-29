using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UriSudokuSolver
{
    /*Represent abstract game board*/
    abstract class GameBoard<T>
    {

        protected T[,] board;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameBoard{T}"/> class.
        /// </summary>
        /// <param name="size">The size of the board.</param>
        public GameBoard(int size)
        {
            board = new T[size, size];
        }

        public GameBoard(int rows, int cols)
        {
            board = new T[rows, cols];
        }

        /*copy constractor*/
        public GameBoard(T[,] board)
        {
            this.board = new T[board.GetLength(0), board.GetLength(1)];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    this.board[i, j] = board[i, j];
                }
            }
        }

        /*Indexer for the game board.*/
        public T this[int row, int col]
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

        public int GetRows()
        {
            return board.GetLength(0);
        }

        public int GetCols()
        {
            return board.GetLength(1);
        }
        public abstract void FillBoard(string boardString);
    }
}