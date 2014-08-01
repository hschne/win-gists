using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace GistClient
{
    public static class RequestFactory
    {
        public static RestRequest CreateRequest(String filepath){
            String fileContent = FileReader.GetContent(filepath);
            String fileName = FileReader.GetFileName(filepath);
            String fileDescription = FileReader.GetFileDescription(filepath);
            var request = new RestRequest("/gists", Method.POST){RequestFormat = DataFormat.Json};
            var jsonObject = BuildJsonObject(fileName, fileDescription, fileContent);
            request.AddBody(jsonObject);
            return request;
        }

        private static JsonObject BuildJsonObject(String fileName, String fileDescription, String fileContent){
            var file = new JsonObject();
            file.Add(fileName, new { content = fileContent });
            var jsonObject = new JsonObject();
            jsonObject.Add("description", fileDescription);
            jsonObject.Add("files", file);
            jsonObject.Add("public", true);
            return jsonObject;
        }
    }
}
