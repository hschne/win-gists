using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GistClient.FileSystem;

namespace GistClient
{
    public static class UserInteraction
    {

        public static Boolean IsValidFilePath(String[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Invalid number of arguments. Expected filepath.");
                return false;
            }
            if (File.Exists(args[0])) return true;
            Console.WriteLine("Invalid filepath.");
            return false;
        }


        public static void SetCredentialsIfNotExist(){
            if (!SettingsManager.CredentialsExist()){
                Console.WriteLine("Please enter your username:");
                String username = Console.ReadLine();
                Console.WriteLine("Please enter your password: ");
                String password = ReadPassword().Encrypt();
                SetCredentials(username,password);
            }
        }


        private static void SetCredentials(String userName, String password)
        {
            SettingsManager.SetUsername(userName);
            SettingsManager.SetPassword(password);
        }

        private static String ReadPassword()
        {
            String password = "";
            
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            return password;
        }
    }
}
