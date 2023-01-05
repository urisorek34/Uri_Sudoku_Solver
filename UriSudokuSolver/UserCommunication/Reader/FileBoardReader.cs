﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;
using UriSudokuSolver.UserCommunication.Validation;

namespace UriSudokuSolver.UserCommunication.Reader
{
    /*Class reads a board form a file.*/
    internal class FileBoardReader : IBoardReader
    {
        private string filePath;
        private IValidator validator;
        private EnumConstants.GameType boardType;


        /*Constractor for the file board reader.*/
        public FileBoardReader(string filePath, EnumConstants.GameType boardType)
        {
            this.boardType = boardType;
            this.filePath = filePath;
            validator = ValidationFactory.GetValidator(boardType);
        }

        /*Function reads a board from a file to the game board.*/
        public GameBoard ReadBoard()
        {
            string gameBoard = File.ReadAllText(filePath);
            validator.ValidateBoard(gameBoard);
            GameBoard board = GetRightBoard((int)Math.Sqrt(gameBoard.Length));
            board.FillBoard(gameBoard);
            board.ValidateBoard();
            return board;
        }

        /*Function returns the right game board.*/
        public GameBoard GetRightBoard(int size)
        {
            switch (boardType)
            {
                case EnumConstants.GameType.SODOKU:
                    return new SudokuBoard(size);
                default:
                    throw new NoSuchGameException($"Invalid board type {boardType}.");
            }
        }


    }
}
