using System;
using System.Collections.Generic;
using System.IO;
using GistClient.Client;
using GistClient.FileSystem;
using RestSharp;

namespace GistClient
{
    public class Program
    {
        private static String filepath;

        public static void Main(string[] args){
            SettingsManager.ClearSettings();
            if (IsValidInput(args)){
                filepath = args[0];
                SetCredentialsifNotExist();
                Client.GistClient.SetAuthentication(SettingsManager.GetUserName(),
                    SettingsManager.GetPassword());
                Console.WriteLine();
                Console.WriteLine("Uploading file...");
                CreateAndSendRequest();
            }
            Console.ReadLine();
        }

        private static void CreateAndSendRequest(){

            try{
                RestRequest request = RequestFactory.CreateRequest(filepath);
                Dictionary<string, string> response = Client.GistClient.SendRequest(request);
                String url = response["html_url"];
                Console.WriteLine("File " + FileReader.GetFileName(filepath) + " uploaded successfully.");
                Console.WriteLine("Url: " + url);
            }
            catch (IOException e){
                Console.WriteLine("Error: File is already used by another program.");
            }
            catch (Exception e){
                Console.WriteLine("An error occured: " + e.Message);
            }
            finally{
                Console.WriteLine("Exiting...");
            }
        }

        private static void SetCredentialsifNotExist(){
            if (!SettingsManager.CredentialsExist()){
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
            SettingsManager.SetPassword(password.Encrypt());
        }

        private static Boolean IsValidInput(String[] args){
            if (args.Length != 1){
                Console.WriteLine("Invalid number of arguments. Expected filepath.");
                return false;
            }
            if (File.Exists(args[0])) return true;
            Console.WriteLine("Invalid filepath.");
            return false;
        }
    }
}