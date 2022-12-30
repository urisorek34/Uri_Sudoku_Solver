using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver
{
    /*Sudoku board for the sudoko solver.*/
    class SudokuBoard : GameBoard<char>
    {
        private const int MIN_VALUE = 1 + '0';

        /// <summary>
        /// Constractor for sudoku board.
        /// </summary>
        /// <param name="size"> the size of the board.</param>
        /// <param name="minValue"> the min value for the sudoko board</param>
        public SudokuBoard(int size) : base(size)
        {

        }

        //Getters for min and max values in the soduko.
        public int GetMinValue() { return MIN_VALUE; }
        public int GetMaxValue() { return MIN_VALUE + GetRows() - 1; }

        /*Function fill the board with the given string.*/
        public override void FillBoard(string boardString)
        {
            int row = 0;
            int col = 0;
            foreach (char c in boardString)
            {
                if (c == '.')
                {
                    board[row, col] = '0';
                }
                else
                {
                    board[row, col] = c;
                }
                col++;
                if (col == GetCols())
                {
                    col = 0;
                    row++;
                }
            }
        }

        /*Check that the board is folowing the rules of sudoku.*/
        public override void ValidateBoard()
        {
            for (int i = 0; i < GetRows(); i++)
            {
                for (int j = 0; j < GetCols(); j++)
                {
                    if (board[i, j] != '0')
                    {
                        if (!CheckRow(i, j) || !CheckCol(i, j) || !CheckSquare(i, j))
                        {
                            throw new BoardNotFollowGameRulesException($"The board is not folowing the rules of sudoku (see example in index {i * j - 1} in the string)");
                        }
                    }
                }
            }
        }

        /*Check if a given cell is folowing the rules of sudoku by the row.*/
        private bool CheckRow(int row, int col)
        {
            for (int i = 0; i < GetCols(); i++)
            {
                if (i != col && board[row, i] == board[row, col])
                {
                    return false;
                }
            }
            return true;
        }

        /*Check if a given cell is folowing the rules of sudoku by the col.*/
        private bool CheckCol(int row, int col)
        {
            for (int i = 0; i < GetRows(); i++)
            {
                if (i != row && board[i, col] == board[row, col])
                {
                    return false;
                }
            }
            return true;
        }

        /*Check if a given cell is folowing the rules of sudoku by the square.*/
        private bool CheckSquare(int row, int col)
        {
            int squareSize = (int)Math.Sqrt(GetRows());
            int squareRow = row / squareSize;
            int squareCol = col / squareSize;
            for (int i = squareRow * squareSize; i < (squareRow + 1) * squareSize; i++)
            {
                for (int j = squareCol * squareSize; j < (squareCol + 1) * squareSize; j++)
                {
                    if (i != row && j != col && board[i, j] == board[row, col])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }


}
