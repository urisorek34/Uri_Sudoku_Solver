using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Exception when a board is not follow the rules of the game that he is representing.*/
namespace UriSudokuSolver.CustomExceptions
{
    public class BoardNotFollowGameRulesException : BoardNotValidException
    {
        public BoardNotFollowGameRulesException(string message) : base(message)
        {
        }
    }

}
