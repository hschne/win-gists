using System;
using GistClient.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GistClient.Test
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void InvalidUser()
        {
            var args = new [] { @"..\..\testfiles\TestFile.txt" };
            SettingsManager.SetUsername("testuser");
            SettingsManager.SetPassword("120[sdff");
            Program.Main(args);
        }

        [TestMethod]
        public void InvalidFile(){
            var args = new[] { @"asdfp98124adff" };
            SettingsManager.SetUsername("testuser");
            SettingsManager.SetPassword("120[sdff");
            Program.Main(args);
        }

        [TestMethod]
        public void InvalidArguments(){
            var args = new[] { @"..\..\testfiles\TestFile.txt", "sadl;kfjasd-s" };
            SettingsManager.SetUsername("testuser");
            SettingsManager.SetPassword("120[sdff");
            Program.Main(args);
        }
    }
}
