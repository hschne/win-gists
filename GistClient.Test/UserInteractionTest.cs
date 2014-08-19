using System;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using GistClient.Request;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GistClient.Test
{
    /// <summary>
    /// Summary description for UserInteractionTest
    /// </summary>
    [TestClass]
    public class UserInteractionTest
    {
        [TestMethod]
        public void IsValidInput(){
            var args = new[] { @"..\..\testfiles\TestFile.txt" };
            Assert.IsTrue(UserInteraction.IsValidFilePath(args));
        }

        [TestMethod]
        public void IsInvalidInput()
        {
            var args = new[] { @"asdfeas" };
            Assert.IsFalse(UserInteraction.IsValidFilePath(args));
        }
    }
}
