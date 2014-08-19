using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace GistClientConfiguration.Configuration {
    public static class ConfigurationManager
    {
        public static String Folder = Properties.Settings.Default.ConfigDirectory;

        public static String FileName = "Configuration.xml";

        public static Configuration Configuration { get; set; }

        public static void Load(){
            if (!File.Exists(Folder + FileName)){
                CreateDefaultConfig();
            }
            var deserializer = new XmlSerializer(typeof(Configuration));
                TextReader reader = new StreamReader(Folder + FileName);
                object obj = deserializer.Deserialize(reader);
                Configuration = (Configuration)obj;
                reader.Close();
        }

        private static void CreateDefaultConfig(){
            var config = new Configuration(){
                CopyUrlToClipboard = false,
                OpenAfterUpload = false,
                Password = "",
                SaveCredentials = true,
                UploadAnonymously = false,
                Username = ""
            };
            Configuration = config;
            Save();
        }

        public static void Save() {
            var serializer = new XmlSerializer(typeof(Configuration));
            using (TextWriter writer = new StreamWriter(Folder + FileName)) {
                serializer.Serialize(writer, Configuration);
            }
        }

        public static void ClearSettings()
        {
            Type type = Configuration.GetType();
            PropertyInfo[] myPropertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var propertyInfo in myPropertyInfo)
            {
                ClearPropertyValue(propertyInfo);
            }
        }

        private static void ClearPropertyValue(PropertyInfo propertyInfo)
        {
            Type type = propertyInfo.PropertyType;
            if (type == typeof(String))
            {
                propertyInfo.SetValue(Configuration, String.Empty, null);
            }
            if (type == typeof(Boolean))
            {
                propertyInfo.SetValue(Configuration, false, null);
            }
        }

        public static bool CredentialsExist()
        {
            bool userExists = Username != String.Empty;
            bool passwordExits = Password.Decrypt() != String.Empty;
            return userExists && passwordExits;
        }

        public static String Password {
            get {
                return Configuration.Password;
            }
            set {
                Configuration.Password = value;
            }
        }

        public static String Username {
            get {
                return Configuration.Username;
            }
            set {
                Configuration.Username = value;
            }
        }

        

        public static bool SaveCredentials {
            get {
                return Configuration.SaveCredentials;
            }
            set {
                Configuration.SaveCredentials = value;
            }
        }

        public static bool OpenAfterUpload {
            get {
                return Configuration.OpenAfterUpload;
            }
            set {
                Configuration.OpenAfterUpload = value;
            }
        }

       

        public static bool CopyUrlToClipboard {
            get {
                return Configuration.CopyUrlToClipboard;
            }
            set {
                Configuration.CopyUrlToClipboard = value;
            }
        }

        public static bool UploadAnonymously {
            get {
                return Configuration.UploadAnonymously;
            }
            set {
                Configuration.UploadAnonymously = value;
            }
        }
    }
}