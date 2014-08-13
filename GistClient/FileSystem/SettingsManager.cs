using System;
using System.Configuration;

using GistClient.Properties;

namespace GistClient.FileSystem {
    public static class SettingsManager {


        public static String Password {
            get {
                return Settings.Default.Password;
            }
            set {
                Settings.Default.Password = value;
                Settings.Default.Save();
            }
        }

        public static String Username {
            get {
                return Settings.Default.Username;
            }
            set {
                Settings.Default.Username = value;
                Settings.Default.Save();
            }
        }

        public static  bool CredentialsExist() {
            bool userExists = Username != String.Empty;
            bool passwordExits = Password.Decrypt() != String.Empty;
            return userExists && passwordExits;
        }

        public static bool SaveCredentials {
            get {
                return Settings.Default.SaveCredentials;
            }
            set {
                Settings.Default.SaveCredentials = value;
                Settings.Default.Save();
            }
        }

        public static bool OpenAfterUpload {
            get {
                return Settings.Default.OpenAfterUpload;
            }
            set {
                Settings.Default.OpenAfterUpload = value;
                Settings.Default.Save();
            }
        }

        public static void ClearSettings() {
            foreach (SettingsProperty item in Settings.Default.Properties) {
                if (item.PropertyType == typeof(String)) {
                    Settings.Default[item.Name] = String.Empty;
                }
                if (item.PropertyType == typeof(Boolean)) {
                    Settings.Default[item.Name] = false;
                }
            }
            Settings.Default.Save();
        }

        public static bool CopyUrlToClipboard {
            get {
                return Settings.Default.CopyUrlToClipboard;
            }
            set {
                Settings.Default.CopyUrlToClipboard = value;
                Settings.Default.Save();
            }
        }

        public static bool UploadAnonymously {
            get {
                return Settings.Default.UploadAnonymously;
            }
            set {
                Settings.Default.UploadAnonymously = value;
                Settings.Default.Save();
            }
        }
    }
}