using System;
using System.Collections.Generic;
using GistClient.Request;
using GistClientConfiguration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace GistClient.Test {
    [TestClass]
    public class GistClientTest {
        [TestMethod]
        public void SendAnonymously() {
            String filepath = @"..\..\testfiles\TestFile.txt";
            RestRequest request = RequestFactory.CreateRequest(filepath);
            Dictionary<string, string> response = Request.Client.SendRequest(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SendRequestBadCredentials(){
            String filepath = @"..\..\testfiles\TestFile.txt";
            RestRequest request = RequestFactory.CreateRequest(filepath);
            Client.SetAuthentication("My", "Credentials".Encrypt());
            Dictionary<string, string> response = Client.SendRequest(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod]
        public void SendRequest()
        {
            String filepath = @"..\..\testfiles\TestFile.txt";
            RestRequest request = RequestFactory.CreateRequest(filepath);
            Client.SetAuthentication("1234", "1234".Encrypt());
            Dictionary<string, string> response = Client.SendRequest(request);
            Assert.IsTrue(response != null);
        }
    }
}
