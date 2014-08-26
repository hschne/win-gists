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
            if (UserInteraction.IsValidFilePath(args)){
                filepath = args[0];
                var executionConfiguration = new ExecutionConfiguration(){
                    filepath = filepath,
                    Configuration = ConfigurationManager.LoadConfigurationFromFile()
                };
                var executor = new Executor(executionConfiguration);
                executor.Execute();
            }
            else{
                Console.WriteLine("Invalid filepath!");
                Console.ReadLine();
            }
        }
    }
}