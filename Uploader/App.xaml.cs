using System;
using System.IO;
using System.Windows;
using GistClientConfiguration.Configuration;

namespace Uploader
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e){
            String[] args = new [] { @"E:\Source\win-gists\README.md"};
            if (IsValidInput(args)){
                String filepath = args[0];
                var executionConfiguration = new ExecutionConfiguration{
                    filepath = filepath,
                    Configuration = ConfigurationManager.LoadConfigurationFromFile()
                };
                var executor = new Executor(executionConfiguration);
                executor.Execute();
            }
        }

        private static Boolean IsValidInput(String[] args){
            if (args.Length != 1)
            {
                Console.WriteLine(@"Error: Invalid number of arguments. Expected filepath.");
                return false;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Error: File " + args + " does not exist.");
                return false;
            }
            return true;
        }
    }
}