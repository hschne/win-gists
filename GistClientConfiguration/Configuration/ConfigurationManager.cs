using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using GistClientConfiguration.Properties;

namespace GistClientConfiguration.Configuration
{
    public static class ConfigurationManager
    {
        public static String Folder = Settings.Default.ConfigDirectory;

        public static String FileName = "Configuration.xml";

        public static Configuration Configuration { get; set; }

        public static String Password{
            get { return Configuration.Password; }
            set { Configuration.Password = value; }
        }

        public static String Username{
            get { return Configuration.Username; }
            set { Configuration.Username = value; }
        }


        public static bool SaveCredentials{
            get { return Configuration.SaveCredentials; }
            set { Configuration.SaveCredentials = value; }
        }

        public static bool OpenAfterUpload{
            get { return Configuration.OpenAfterUpload; }
            set { Configuration.OpenAfterUpload = value; }
        }


        public static bool CopyUrlToClipboard{
            get { return Configuration.CopyUrlToClipboard; }
            set { Configuration.CopyUrlToClipboard = value; }
        }

        public static bool UploadAnonymously{
            get { return Configuration.UploadAnonymously; }
            set { Configuration.UploadAnonymously = value; }
        }

        public static Configuration LoadConfigurationFromFile(){
            if (!File.Exists(Folder + FileName)){
                CreateDefaultConfig();
            }
            var deserializer = new XmlSerializer(typeof (Configuration));
            TextReader reader = new StreamReader(Folder + FileName);
            try{
                object obj = deserializer.Deserialize(reader);
                var result = (Configuration) obj;
                return result;
            }
            catch (Exception e){
               HandleException(e);
            }
            finally{
                reader.Close();
            }
            return null;
        }

        private static void HandleException(Exception e){
            if (e.GetType() == typeof (XmlException)){
                throw new Exception("Error loading settings file: " + e.Message);
            }
            throw new Exception("Settings file cannot be opened: "+e.Message);
        }

        private static void CreateDefaultConfig(){
            var config = new Configuration{
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

        public static void Save(){
            if (!SaveCredentials){
                Username = "";
                Password = "";
            }
            var serializer = new XmlSerializer(typeof (Configuration));
            using (TextWriter writer = new StreamWriter(Folder + FileName)){
                serializer.Serialize(writer, Configuration);
            }
        }

        public static void ClearSettings(){
            Type type = Configuration.GetType();
            PropertyInfo[] myPropertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo propertyInfo in myPropertyInfo){
                ClearPropertyValue(propertyInfo);
            }
        }

        public static bool ConfigurationChanged(){
            Configuration configuration = LoadConfigurationFromFile();
            return !Configuration.Equals(configuration);
        }

        private static void ClearPropertyValue(PropertyInfo propertyInfo){
            Type type = propertyInfo.PropertyType;
            if (type == typeof (String)){
                propertyInfo.SetValue(Configuration, String.Empty, null);
            }
            if (type == typeof (Boolean)){
                propertyInfo.SetValue(Configuration, false, null);
            }
        }

        public static bool CredentialsExist(){
            bool userExists = Username != String.Empty;
            bool passwordExits = Password.Decrypt() != String.Empty;
            return userExists && passwordExits;
        }
    }
}