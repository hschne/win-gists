using System;
using System.IO;
using GistClientConfiguration.Configuration;

namespace GistClient
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args){
            if (IsValidInput(args[0])){
                String filepath = args[0];
                var executionConfiguration = new ExecutionConfiguration{
                    filepath = filepath,
                    Configuration = ConfigurationManager.LoadConfigurationFromFile()
                };
                var executor = new Executor(executionConfiguration);
                executor.Execute();
            }
            Console.ReadLine();
        }

        private static Boolean IsValidInput(String filepath){
            if (filepath.Length != 1){
                Console.WriteLine(@"Error: Invalid number of arguments. Expected filepath.");
                return false;
            }
            if (!File.Exists(filepath)){
                Console.WriteLine("Error: File " + filepath + " does not exist.");
                return false;
            }
            return false;
        }
    }
}