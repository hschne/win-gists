using System;
using System.IO;
using GistClientConfiguration.Configuration;

namespace GistClient.Request
{
    public static class UserInteraction
    {

        public static String Username { get; set; }

        public static String Password { get; set; }

        public static Boolean IsValidFilePath(String[] args){
            if (args.Length != 1){
                Console.WriteLine(@"Invalid number of arguments. Expected filepath.");
                return false;
            }
            if (File.Exists(args[0])) return true;
            Console.WriteLine(@"Invalid filepath.");
            return false;
        }


        public static void SetCredentialsIfNotExist(){
            if (!ConfigurationManager.CredentialsExist()){
                Console.WriteLine(@"Please enter your username:");
                Username = Console.ReadLine();
                Console.WriteLine(@"Please enter your password: ");
                Password = ReadPassword().Encrypt();
                if (ConfigurationManager.SaveCredentials){
                    SetCredentials(Username, Password);
                }
            }
        }


        private static void SetCredentials(String userName, String password){
            ConfigurationManager.Username = userName;
            ConfigurationManager.Password = password;
            ConfigurationManager.Save();
        }

        private static String ReadPassword(){
            String password = "";

            ConsoleKeyInfo key;
            do{
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter){
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else{
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0){
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            return password;
        }
    }
}