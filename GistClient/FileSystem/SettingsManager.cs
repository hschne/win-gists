using System;
using System.Configuration;
using GistClient.Properties;

namespace GistClient.FileSystem
{
    public static class SettingsManager
    {
        public static String GetPassword(){
            return Settings.Default.Password;
        }

        public static String GetUserName(){
            return Settings.Default.Username;
        }

        public static void SetUsername(String username){
            Settings.Default.Username = username;
        }

        public static void SetPassword(String password){
            Settings.Default.Password = password.Encrypt();
        }

        public static bool CredentialsExist(){
            bool userExists = GetUserName() != String.Empty;
            bool passwordExits = GetPassword().Decrypt() != String.Empty;
            return userExists && passwordExits;
        }

        public static void ClearSettings(){
            foreach (SettingsProperty item in Settings.Default.Properties){
                Settings.Default[item.Name] = String.Empty;
            }
        }
    }
}