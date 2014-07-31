using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;
using RestSharp.Deserializers;

namespace GistClient {
    class Program {
        static void Main(string[] args) {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("/gists", Method.POST);
            request.RequestFormat = DataFormat.Json;
            var file = new JsonObject();
            file.Add("myFile.txt", new { content = "this is some file content"});
            var jsonObject = new JsonObject();
            jsonObject.Add("description", "x2762-1s");
            jsonObject.Add("files", file);
            jsonObject.Add("public",true);
            request.AddBody(jsonObject);
            var response = client.Execute(request);
            var deserializer = new JsonDeserializer();
            var jsonResponse = deserializer.Deserialize<Dictionary<String,String>>(response);
            Console.ReadLine();

        }
    }
}
