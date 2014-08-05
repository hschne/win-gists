using System;
using GistClient.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GistClient.Test
{
    [TestClass]
    public class SettingsManagerTest
    {
        [TestMethod]
        public void SavePassword(){
            const string password = "testpassword";
            SettingsManager.SetPassword(password);
        }

        [TestMethod]
        public void SaveUserName(){
            const string userName = "user";
            SettingsManager.SetUsername(userName);
        }

        [TestMethod]
        public void GetPassword(){
            SavePassword();
            String password = SettingsManager.GetPassword();
            String clearText = password.Decrypt();
            Assert.IsTrue(clearText.Equals("testpassword"));
        }

        [TestMethod]
        public void ClearSettings(){
            SettingsManager.ClearSettings();
            Assert.IsFalse(SettingsManager.CredentialsExist());
        }
    }
}