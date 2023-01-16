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
        public void WriteBoard(GameBoard board)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            int size = board.GetRows();
            int sqrtSize = (int)Math.Sqrt(size);
            ConsoleColor digitColor = DecideDigitColor(board);
            // print the first border
            BoarderWrite(sqrtSize * 2 - 1, size, sqrtSize);

            // for every row in the board paint border + numbers
            for (int row = 0; row < size * 2; row++)
            {
                Console.WriteLine();
                if (row % 2 == 0)
                {
                    DigitWrite(digitColor, row, sqrtSize, size, board);
                }
                else
                {
                    BoarderWrite(row, size, sqrtSize);
                }

            }
            Console.WriteLine();

        }


        /*Returns the digit color based on if the board is solved or not*/
        private ConsoleColor DecideDigitColor(GameBoard board)
        {
            if (board.IsFull())
            {
                return ConsoleColor.Green;
            }
            return ConsoleColor.Red;
        }

        /*Write a boarder*/
        private void BoarderWrite(int row, int size, int sqrtSize)
        {
            for (int col = 0; col < size; col++)
            {

                // write the '+', paint it in yellow if it is a border of a box
                if (col % sqrtSize == 0 || (row + 1) % (2 * sqrtSize) == 0)
                {

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                Console.Write("+");
                Console.ResetColor();
                // write the '-', paint it in yellow if it is border of a box
                if ((row + 1) % (2 * sqrtSize) == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;

                }

                Console.Write("———");
                Console.ResetColor();


            }
            // the last '+' in every row
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("+");
            Console.ResetColor();

        }

        /*prints the digit*/
        private void DigitWrite(ConsoleColor digitColor, int row, int sqrtSize, int size, GameBoard board)
        {
            // the first border
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("|");
            Console.ResetColor();
            // write all the digits in the row
            for (int col = 0; col < size; col++)
            {
                // write the digit with the right color
                Console.ForegroundColor = digitColor;
                Console.Write($" {(char)(board[row / 2, col] + '0')}");
                Console.ResetColor();
                // if it is a box border, paint it in yellow
                if (col % sqrtSize == sqrtSize - 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                Console.Write(" |");
                Console.ResetColor();
            }
        }

    }



}
