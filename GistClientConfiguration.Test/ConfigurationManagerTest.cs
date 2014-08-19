using System;
using System.IO;
using GistClientConfiguration.Configuration;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GistClientConfiguration.Test {
    [TestClass]
    public class ConfigurationManagerTest {
        private static Configuration.Configuration config;

        [TestInitialize]
        public void Initialize() {
            config = new Configuration.Configuration() {
                CopyUrlToClipboard = true,
                OpenAfterUpload = false,
                Password = "testpassword".Encrypt(),
                SaveCredentials = true,
                UploadAnonymously = true,
                Username = "testuser"
            };
        }

        [TestMethod]
        public void ClearSettings() {
            ConfigurationManager.Configuration = config;
            ConfigurationManager.ClearSettings();
            Assert.IsTrue(config.CopyUrlToClipboard == false);
            Assert.IsTrue(config.Username == "");
        }

        [TestMethod]
        public void Save(){
            ConfigurationManager.Configuration = config;
            ConfigurationManager.Folder = @"..\..\testfiles\";
            ConfigurationManager.FileName = "testconfig.xml";
            File.Delete(ConfigurationManager.Folder + ConfigurationManager.FileName);
            ConfigurationManager.Save();
            Assert.IsTrue(File.Exists(ConfigurationManager.Folder + ConfigurationManager.FileName));
        }

        [TestMethod]
        public void Load(){
            ConfigurationManager.Folder = @"..\..\testfiles\";
            ConfigurationManager.FileName = "testconfig.xml";
            ConfigurationManager.Load();
            Assert.IsTrue(ConfigurationManager.Password != "");
            Assert.IsFalse(ConfigurationManager.OpenAfterUpload);
        }

        [TestMethod]
        public void CredentialsExist(){
            ConfigurationManager.Folder = @"..\..\testfiles\";
            ConfigurationManager.FileName = "testconfig.xml";
            ConfigurationManager.Load();
            Assert.IsTrue(ConfigurationManager.CredentialsExist());
        }
    }
}
