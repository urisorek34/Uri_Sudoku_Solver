using Microsoft.VisualStudio.TestTools.UnitTesting;
using UriSudokuSolver.UserCommunication.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UriSudokuSolver.CustomExceptions;

namespace UriSudokuSolver.UserCommunication.Reader.Tests
{
    /*Check if the file board reader works correctly.*/
    [TestClass()]
    public class FileBoardReaderTests
    {
        /*Check if the file board reader works correctly when .*/
        [TestMethod()]
        public void FileReadTestUnautherizedException()
        {
            //Arrange
            string path = "C:\\";
            FileBoardReader reader = new FileBoardReader(path, EnumConstants.GameType.SODOKU);
            //Act
            //Assert
            Assert.ThrowsException<IlegalFilePathInputException>(() => reader.ReadBoard());
        }

        /*Check if the file board reader works correctly when wrong file path nonsens.*/
        [TestMethod()]
        public void FileReadTestWrongFilePath()
        {
            //Arrange
            string path = "nonsens";
            FileBoardReader reader = new FileBoardReader(path, EnumConstants.GameType.SODOKU);
            //Act
            //Assert
            Assert.ThrowsException<IlegalFilePathInputException>(() => reader.ReadBoard());
        }

        /*Check if the file board reader works correctly when wrong file type.*/
        [TestMethod()]
        public void FileReadTestWrongFileType()
        {
            //Arrange
            string path = "C:\\TASM\\BIN\\p.asm";
            FileBoardReader reader = new FileBoardReader(path, EnumConstants.GameType.SODOKU);
            //Act
            //Assert
            Assert.ThrowsException<IlegalFilePathInputException>(() => reader.ReadBoard());
        }

        /*Check if the file board reader works correctly when file path too long*/
        [TestMethod()]
        public void FileReadTestFilePathTooLong()
        {
            //Arrange
            string path = "gaerggrh4ov23ht4i2htvo4h2hnot4hovt5oi5hto4vtu5h3utvh53nvthtvh3vhc53hnc3htc5hn3oicth5htuc5iohutoci5huh5oi3chtnoiu54hnt3oihncoi3hnocithn53ihntc5hnoicthno5ic4o3htoc35hnuth5n3ht5iu3uch5n3ioctnhi5hnichntui5ch.txtr";
            FileBoardReader reader = new FileBoardReader(path, EnumConstants.GameType.SODOKU);
            //Act
            //Assert
            Assert.ThrowsException<IlegalFilePathInputException>(() => reader.ReadBoard());
        }



    }
}