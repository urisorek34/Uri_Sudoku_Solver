namespace UriSudokuSolver
{
    /*Interface for the solvers of the abstract game board*/
    interface ISolver
    {
        public byte[,] Solve();
    }
}
