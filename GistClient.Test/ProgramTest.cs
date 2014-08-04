using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GistClient.Test
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void RunProgram(){
            String filepath = @"E:\Source\win-gists\GistClient.Test\testfiles\TestFile.txt";
            Program.Main(new []{filepath});
        }
    }
}
