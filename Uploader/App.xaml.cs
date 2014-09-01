using System;
using System.IO;
using System.Threading;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using WinGistsConfiguration.Configuration;

namespace Uploader
{
    public partial class App : Application
    {
        private static UploaderIcon icon;

        private static Executor executor;

        protected override void OnStartup(StartupEventArgs e){
            String[] args = e.Args;
            if (IsValidInput(args)){
                InitializeExecutor(args[0]);
                ConfigurationManager.Configuration = ConfigurationManager.LoadConfigurationFromFile();
                if (ConfigurationManager.ShowBubbleNotifications){
                    InitializeNotifications();
                }
                executor.Execute();
            }
            Current.Shutdown();
        }

        private static void InitializeExecutor(String filepath){
            var executionConfiguration = new ExecutionConfiguration
            {
                Filepath = filepath,
                Configuration = ConfigurationManager.LoadConfigurationFromFile()
            };
            executor = new Executor(executionConfiguration);
        }

        private static void InitializeNotifications(){
            icon = new UploaderIcon();
            executor.OnError += icon.ShowErrorBallon;
            executor.OnNotify += icon.ShowStandardBaloon;
            executor.OnFinish += icon.ShowStandardBaloon;
        }

        private static Boolean IsValidInput(String[] args){
            if (args.Length != 1){
                Console.WriteLine(@"Error: Invalid number of arguments. Expected filepath.");
                return false;
            }
            if (!File.Exists(args[0])){
                Console.WriteLine("Error: File " + args + " does not exist.");
                return false;
            }
            return true;
        }

    }
}