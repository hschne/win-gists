using System;
using System.IO;
using GistClientConfiguration.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GistClientConfiguration.Test
{
    [TestClass]
    public class ConfigurationManagerTest
    {
        private static Configuration.Configuration config;

        [TestInitialize]
        public void Initialize(){
            config = new Configuration.Configuration{
                CopyUrlToClipboard = true,
                OpenAfterUpload = false,
                Password = "testpassword".Encrypt(),
                SaveCredentials = true,
                UploadAnonymously = true,
                Username = "testuser"
            };
        }

        [TestMethod]
        public void ClearSettings(){
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
            ConfigurationManager.Configuration = ConfigurationManager.LoadConfigurationFromFile();
            Assert.IsTrue(ConfigurationManager.EncryptedPassword != "");
            Assert.IsFalse(ConfigurationManager.OpenAfterUpload);
        }

        [TestMethod]
        [ExpectedException(typeof (Exception))]
        public void LoadCorruptedFile(){
            ConfigurationManager.Folder = @"..\..\testfiles\";
            ConfigurationManager.FileName = "corruptedConfig.xml";
            ConfigurationManager.Configuration = ConfigurationManager.LoadConfigurationFromFile();
        }

        [TestMethod]
        public void CredentialsExist(){
            ConfigurationManager.Folder = @"..\..\testfiles\";
            ConfigurationManager.FileName = "testconfig.xml";
            ConfigurationManager.Configuration = ConfigurationManager.LoadConfigurationFromFile();
            Assert.IsTrue(ConfigurationManager.CredentialsExist());
        }

        [TestMethod]
        public void ConfigHasNotChanged(){
            ConfigurationManager.Folder = @"..\..\testfiles\";
            ConfigurationManager.FileName = "testconfig.xml";
            ConfigurationManager.Configuration = ConfigurationManager.LoadConfigurationFromFile();
            Assert.IsFalse(ConfigurationManager.ConfigurationChanged());
        }

        [TestMethod]
        public void ConfigHasChanged(){
            ConfigurationManager.Folder = @"..\..\testfiles\";
            ConfigurationManager.FileName = "testconfig.xml";
            ConfigurationManager.Configuration = ConfigurationManager.LoadConfigurationFromFile();
            ConfigurationManager.EncryptedPassword = "longdinglong";
            Assert.IsTrue(ConfigurationManager.ConfigurationChanged());
        }
    }
}