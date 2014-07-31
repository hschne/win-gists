using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GistClient.Test
{
    [TestClass]
    public class FileReaderTest
    {
        [TestMethod]
        public void ReadFile(){
            const string filepath = @"..\..\testfiles\TestFile.txt";
            String content = FileReader.Read(filepath);
            Assert.IsTrue(content.Equals("TestFileContent"));
        }

        [TestMethod]
        public void ReadEmptyFile(){
            const string filepath = @"..\..\testfiles\EmptyFile.txt";
            String content = FileReader.Read(filepath);
            Assert.IsTrue(content.Equals(""));
        }

        [TestMethod]
        public void GetFileName(){
            const string filepath = @"..\..\testfiles\TestFile.txt";
            String fileName = FileReader.GetFileName(filepath);
            Assert.IsTrue(fileName == "TestFile.txt");
        }

        [TestMethod]
        public void GetFileDescription(){
            const string filepath = @"..\..\testfiles\TestFile.txt";
            String fileDescription = FileReader.GetFileDescription(filepath);
            Assert.IsTrue(fileDescription == "File TestFile.txt, uploaded by WinGists.");
        }
    }
}