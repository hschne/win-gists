using System;
using System.Collections.Generic;
using System.Linq;
using GistClientConfiguration.Configuration;
using RestSharp;
using RestSharp.Deserializers;

namespace Uploader.Request
{
    public static class Client
    {
        private static readonly RestClient RestClient;

        static Client(){
            RestClient = new RestClient("https://api.github.com");
        }

        public static Dictionary<String, String> SendRequest(RestRequest request){
            IRestResponse response = RestClient.Execute(request);
            HandleResponse(response);
            Dictionary<string, string> jsonResponse = TrySerializeResponse(response);
            return jsonResponse;
        }

        public static void SetAuthentication(String username, String password){
            RestClient.Authenticator = new HttpBasicAuthenticator(username, password.Decrypt());
        }

        private static void HandleResponse(IRestResponse response){
            Parameter statusHeader = response.Headers.FirstOrDefault(x => x.Name == "Status");
            if (statusHeader != null){
                string statusValue = statusHeader.Value.ToString();
                if (!statusValue.Contains("201")){
                    String message = TrySerializeResponse(response)["message"];
                    throw new Exception(statusValue + ", " + message);
                }
            }
            else{
                throw new Exception("Github could not be reached. Verify your connection.");
            }
        }

        private static Dictionary<string, string> TrySerializeResponse(IRestResponse response){
            var deserializer = new JsonDeserializer();
            var jsonResponse = deserializer.Deserialize<Dictionary<String, String>>(response);
            return jsonResponse;
        }
    }
}