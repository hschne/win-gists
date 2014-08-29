using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Uploader.Request;
using WinGistsConfiguration.Configuration;

namespace Uploader.Test
{
    [TestClass]
    public class UploaderTest
    {
        [TestMethod]
        public void SendAnonymously(){
            string filepath = @"..\..\testfiles\TestFile.txt";
            RestRequest request = RequestFactory.CreateRequest(filepath);
            Dictionary<string, string> response = Client.SendRequest(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void SendRequestBadCredentials(){
            const string filepath = @"..\..\testfiles\TestFile.txt";
            RestRequest request = RequestFactory.CreateRequest(filepath);
            "tha".Encrypt();
            Client.SetAuthentication("My", "Credentials".Encrypt());
            Dictionary<string, string> response = Client.SendRequest(request);
            Assert.IsTrue(response != null);
        }

        [TestMethod]
        public void SendRequest(){
            const string filepath = @"..\..\testfiles\TestFile.txt";
            RestRequest request = RequestFactory.CreateRequest(filepath);
            Client.SetAuthentication("1234", "1234".Encrypt());
            Dictionary<string, string> response = Client.SendRequest(request);
            Assert.IsTrue(response != null);
        }
    }
}