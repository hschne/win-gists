using System;
using System.Security.Cryptography;
using GistClient.Client;
using GistClient.FileSystem;


namespace GistClient {
    public class Program {
        public static void Main(string[] args){
            String filepath = @"E:\Source\win-gists\GistClient\bin\Debug\RestSharp.xml"; //args[0]; 
           
            if (!SettingsManager.CredentialsExist()){
                Console.WriteLine("Please enter your username:");
                String username = Console.ReadLine();
                Console.WriteLine("Please enter your password: ");
                String password = Console.ReadLine().Encrypt();
                SettingsManager.SetUsername(username);
                SettingsManager.SetPassword(password);
            }
            Client.GistClient.SetAuthentication(SettingsManager.GetUserName(),SettingsManager.GetPassword().Decrypt());
            Console.WriteLine("Uploading file...");
            var request = RequestFactory.CreateRequest(filepath);
            var response = Client.GistClient.SendRequest(request);
            String url = response["html_url"];
            Console.WriteLine("File " +filepath+ " uploaded successfully.");
            Console.WriteLine("Url: " +url);
            Console.ReadLine();
        }
    }
}
