using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using GistClient.Request;
using GistClientConfiguration.Configuration;
using RestSharp;

namespace GistClient
{
    public class Program
    {
        private static String filepath;

        [STAThread]
        public static void Main(string[] args){
            ConfigurationManager.Configuration = ConfigurationManager.LoadConfigurationFromFile();
            if (UserInteraction.IsValidFilePath(args)){
                filepath = args[0];
                if (!ConfigurationManager.UploadAnonymously){
                    UserInteraction.PersistCredentialsIfNotExist();
                    SetRestClientAuthentication();
                }
                Console.WriteLine();
                Console.WriteLine("Uploading file...");
                CreateAndSendRequest();
            }
            Console.ReadLine();
        }


        private static void SetRestClientAuthentication(){
            if (ConfigurationManager.SaveCredentials)
            {
                Client.SetAuthentication(ConfigurationManager.Username,
                    ConfigurationManager.EncryptedPassword);
            }
            else
            {
                Client.SetAuthentication(UserInteraction.Username,
                    UserInteraction.Password);
            }
        }

        private static void CreateAndSendRequest(){
            try{
                RestRequest request = RequestFactory.CreateRequest(filepath);
                Dictionary<string, string> response = Client.SendRequest(request);
                String url = response["html_url"];
                Console.WriteLine("File " + FileReader.GetFileName(filepath) + " uploaded successfully.");
                Console.WriteLine("Url: " + url);
                if (ConfigurationManager.CopyUrlToClipboard){
                    Console.WriteLine("Url has been copied to clipboard...");
                    Clipboard.SetText(url);
                }
                if (ConfigurationManager.OpenAfterUpload){
                    Console.WriteLine("Opening in browser....");
                    Process.Start(url);
                }
            }
            catch (IOException){
                Console.WriteLine("Error: File is already used by another program.");
            }
            catch (Exception e){
                Console.WriteLine("An error occured: " + e.Message);
            }
            finally{
                Console.WriteLine(@"Exiting...");
            }
        }
    }
}