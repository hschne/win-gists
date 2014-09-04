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

        protected override void OnStartup(StartupEventArgs startupEvent){
            String[] args = startupEvent.Args;
            icon = new UploaderIcon();
            try{
                ConfigurationManager.Configuration = ConfigurationManager.LoadConfigurationFromFile();
                if (IsValidInput(args)){
                    InitializeExecutor(args[0]);
                    if (ConfigurationManager.ShowBubbleNotifications){
                        EnableNotifications();
                    }
                    executor.Execute();
                }
            }
            catch (Exception exception){
                Console.WriteLine("Error: " +exception.Message);
                icon.ShowErrorBallon("An error occured: "+exception.InnerException.Message);
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

        private static void EnableNotifications(){
            executor.OnError += icon.ShowErrorBallon;
            executor.OnNotify += icon.ShowStandardBaloon;
            executor.OnFinish += icon.ShowStandardBaloon;
        }

        private static Boolean IsValidInput(String[] args){
            if (args.Length != 1){
                throw new Exception("Invalid number of input arguments.");
            }
            if (!File.Exists(args[0])){
                throw new Exception("Error: File " + args + " does not exist.");
            }
            return true;
        }

    }
}