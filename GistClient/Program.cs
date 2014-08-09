using System;
using System.Collections.Generic;
using GistClient.Client;
using GistClient.FileSystem;
using RestSharp;

namespace GistClient
{
    public class Program
    {
        public static void Main(string[] args){
            String filepath = args[0]; 
            SetCredentialsifNotExist();
            Client.GistClient.SetAuthentication(SettingsManager.GetUserName(), SettingsManager.GetPassword().Decrypt());
            Console.WriteLine("Uploading file...");
            RestRequest request = RequestFactory.CreateRequest(filepath);
            Dictionary<string, string> response = Client.GistClient.SendRequest(request);
            String url = response["html_url"];
            Console.WriteLine("File " + filepath + " uploaded successfully.");
            Console.WriteLine("Url: " + url);
            Console.ReadLine();
        }

        private static void SetCredentialsifNotExist(){
            if (!SettingsManager.CredentialsExist())
            {
                Console.WriteLine("Please enter your username:");
                String username = Console.ReadLine();
                ReadPassword();
                SettingsManager.SetUsername(username);
            }
        }

        private static void ReadPassword(){
            String password = "";
            Console.WriteLine("Please enter your password: ");
            ConsoleKeyInfo key;
            do{
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
            } 
            while (key.Key != ConsoleKey.Enter);
            SettingsManager.SetPassword(password.Encrypt());
        }
    }
}