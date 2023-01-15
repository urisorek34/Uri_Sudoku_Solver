using Microsoft.VisualStudio.TestTools.UnitTesting;
using UriSudokuSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver.Tests
{
    /*Tests for the SudokuBoardValidator class.*/
    [TestClass()]
    public class SudokuBoardValidatorTests
    {
        /*Test valid input.*/
        [TestMethod()]
        public void ValidBoardTest()
        {
            string boardString = "123456789456789123789123456234567891567891234891234567345678912678912345912345678";
            SudokuBoardValidator validator = new SudokuBoardValidator();
            //Act
            validator.ValidateBoard(boardString);
            //Assert
            //No exception thrown.
        }
        
        /*Test for when the board is null.*/
        [TestMethod()]
        public void ValidateStringBoardNullTest()
        {
            //Arrange
            string board = "";
            SudokuBoardValidator validator = new SudokuBoardValidator();
            // return right exception
            //Act + Assert
            Assert.ThrowsException<BoardIsEmptyException>(() => validator.ValidateBoard(board));
        }

        /*Test for when the board is not the right size.*/
        [TestMethod()]
        public void ValidateStringBoardNotRightSizeTest()
        {
            //Arrange
            string board = "123456789";
            SudokuBoardValidator validator = new SudokuBoardValidator();
            // return right exception
            // Act + Assert
            Assert.ThrowsException<BoardStringSizeIsNotValidException>(() => validator.ValidateBoard(board));
        }

        /*Test for when the board has unvalid char.*/
        [TestMethod()]
        public void ValidateStringBoardNotValidTest()
        {
            //Arrange
            string board = "00000508000060104300000000001050000000010600030000000553:000061000000004000000000";
            SudokuBoardValidator validator = new SudokuBoardValidator();
            // return right exception
            // Act + Assert
            Assert.ThrowsException<BoardStringIlegalCharException>(() => validator.ValidateBoard(board));

        }
    }
}