using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using RestSharp;
using Uploader.Request;
using WinGistsConfiguration.Configuration;

namespace Uploader
{
    public class Executor
    {
        public Executor(ExecutionConfiguration executionConfiguration){
            ExecutionConfiguration = executionConfiguration;
        }

        private ExecutionConfiguration ExecutionConfiguration { get; set; }

        public delegate void ExecutionEventHandler(String message);

        public event ExecutionEventHandler OnError;

        public event ExecutionEventHandler OnNotify;

        public event ExecutionEventHandler OnFinish;

        public void Execute(){
            ConfigurationManager.Configuration = ExecutionConfiguration.Configuration;
            if (ConfigurationManager.UploadAnonymously){
                CreateAndSendRequest();
                NotifyEnd("File uploaded anonymously.");
                return;
            }
            if (!ConfigurationManager.CredentialsExist()){
                WriteError("No credentials set.");
                return;
            }
            Client.SetAuthentication(ConfigurationManager.Username,
                ConfigurationManager.EncryptedPassword);
            CreateAndSendRequest();
            NotifyEnd("File uploaded successfully.");
        }


        private void CreateAndSendRequest(){
            try{
                RestRequest request = RequestFactory.CreateRequest(ExecutionConfiguration.Filepath);
                Dictionary<string, string> response = Client.SendRequest(request);
                String url = response["html_url"];
                if (ConfigurationManager.CopyUrlToClipboard){
                    Clipboard.SetText(url);
                    WriteNotification("Url has been copied to clipboard.");
                }
                if (ConfigurationManager.OpenAfterUpload){
                    Process.Start(url);
                }
            }
            catch (IOException){
                WriteError("Error reading from file.");
            }
            catch (Exception e){
                WriteError(e.Message);
            }
        }

        private void WriteNotification(String message){
            ExecutionEventHandler handler = OnNotify;
            if (handler != null){
                handler(message);
            }
        }

        private void WriteError(String message){
            ExecutionEventHandler handler = OnError;
            if (handler != null){
                handler(message);
            }
        }

        private void NotifyEnd(String message){
            ExecutionEventHandler handler = OnFinish;
            if (handler != null){
                handler("Upload successfull.");
            }
        }
    }
}