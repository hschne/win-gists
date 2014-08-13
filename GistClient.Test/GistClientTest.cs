using System;
using System.Collections.Generic;

using GistClient.Client;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using RestSharp;

namespace GistClient.Test {
    [TestClass]
    public class GistClientTest {
        [TestMethod]
        public void SendAnonymously() {
            String filepath = @"..\..\testfiles\TestFile.txt";
            RestRequest request = RequestFactory.CreateRequest(filepath);
            Dictionary<string, string> response = Client.GistClient.SendRequest(request);
            Assert.IsTrue(response != null);
        }
    }
}
