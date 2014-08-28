using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using GistClientConfiguration.Configuration;
using RestSharp;
using Uploader.Request;

namespace Uploader
{
    public class Executor
    {
        public Executor(ExecutionConfiguration executionConfiguration){
            ExecutionConfiguration = executionConfiguration;
        }

        private ExecutionConfiguration ExecutionConfiguration { get; set; }

        public void Execute(){
            ConfigurationManager.Configuration = ExecutionConfiguration.Configuration;
            if (ConfigurationManager.UploadAnonymously){
                Console.WriteLine();
                Console.WriteLine("Uploading file anonymously...");
                CreateAndSendRequest();
                return;
            }
            if (!ConfigurationManager.CredentialsExist()){
                Console.WriteLine(
                    "Error: No username or password have been set. Please set credentials in configuration manager.");
                Console.ReadLine();
            }
            Client.SetAuthentication(ConfigurationManager.Username,
                ConfigurationManager.EncryptedPassword);
            CreateAndSendRequest();
        }


        private void CreateAndSendRequest(){
            try{
                RestRequest request = RequestFactory.CreateRequest(ExecutionConfiguration.filepath);
                Dictionary<string, string> response = Client.SendRequest(request);
                String url = response["html_url"];
                Console.WriteLine("File " + FileReader.GetFileName(ExecutionConfiguration.filepath) +
                                  " uploaded successfully.");
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
                Console.ReadLine();
            }
        }
    }
}