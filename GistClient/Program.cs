using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using RestSharp.Deserializers;

namespace GistClient {
    public class Program {
        public static void Main(string[] args){
            String filepath = args[0];
            Console.WriteLine("Please enter your username:");
            String username = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            String password = Console.ReadLine();
            GistClient.SetAuthentication(username,password);
            Console.WriteLine("Uploading file...");
            var request = RequestFactory.CreateRequest(filepath);
            var response = GistClient.SendRequest(request);
            String url = response["html_url"];
            Console.WriteLine("File " +filepath+ " uploaded successfully.");
            Console.WriteLine("Url: " +url);
            Console.ReadLine();
        }
    }
}
