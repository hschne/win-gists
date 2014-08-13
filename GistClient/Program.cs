﻿using System;
using System.Collections.Generic;
using System.IO;
using GistClient.Client;
using GistClient.FileSystem;
using RestSharp;
using System.Windows.Forms;
using System.Diagnostics;

namespace GistClient
{
    public class Program
    {
        private static String filepath;

        [STAThread]
        public static void Main(string[] args){
            if (UserInteraction.IsValidFilePath(args)){
                filepath = args[0];
                UserInteraction.SetCredentialsIfNotExist();
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
                Console.WriteLine("Url has been copied to clipboard...");
                Console.WriteLine("Opening in browser....");
                Process.Start(url);
                Clipboard.SetText(url);
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
    }
}