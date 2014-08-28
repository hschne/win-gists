using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GistClientConfiguration.Configuration;

namespace Uploader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e){
            String[] args = e.Args;
            if (IsValidInput(args[0]))
            {
                String filepath = args[0];
                var executionConfiguration = new ExecutionConfiguration
                {
                    filepath = filepath,
                    Configuration = ConfigurationManager.LoadConfigurationFromFile()
                };
                var executor = new Executor(executionConfiguration);
                executor.Execute();
            }
        }

        private static Boolean IsValidInput(String filepath)
        {
            if (filepath.Length != 1)
            {
                Console.WriteLine(@"Error: Invalid number of arguments. Expected filepath.");
                return false;
            }
            if (!File.Exists(filepath))
            {
                Console.WriteLine("Error: File " + filepath + " does not exist.");
                return false;
            }
            return false;
        }
    }
}
