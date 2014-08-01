using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using RestSharp.Deserializers;

namespace GistClient {
    class Program {
        static void Main(string[] args){
            const string filepath = @"E:\Source\win-gists\GistClient.Test\testfiles\TestFile.txt";
            var request = RequestFactory.CreateRequest(filepath);
            var response = GistClient.SendRequest(request);
            Console.ReadLine();

        }
    }
}
