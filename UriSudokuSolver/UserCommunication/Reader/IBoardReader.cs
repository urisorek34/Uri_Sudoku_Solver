namespace UriSudokuSolver
{
    /*Interface for reading a game board.*/
    internal interface IBoardReader
    {
        public GameBoard ReadBoard();
        public GameBoard GetRightBoard(int size);
    }
}
