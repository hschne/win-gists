using System;

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
        public void Save() {
        }
    }
}
