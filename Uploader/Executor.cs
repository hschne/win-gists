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
                //add notification
                return;
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
                if (ConfigurationManager.CopyUrlToClipboard){
                    // add notification
                    Clipboard.SetText(url);
                }
                if (ConfigurationManager.OpenAfterUpload){
                    // add notification
                    Process.Start(url);
                }
            }
            catch (IOException){
                //notification
            }
            catch (Exception e){
                //notification
            }
        }
    }
}