using System;
using GistClient.FileSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GistClient.Test
{
    [TestClass]
    public class SettingsManagerTest
    {
       
        [TestMethod]
        public void GetPassword() {
            SettingsManager.Password = "testpassword".Encrypt();
            String password = SettingsManager.Password;
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