using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using GistClient.FileSystem;
using RestSharp;
using RestSharp.Deserializers;

namespace GistClient.Client
{
    public static class GistClient
    {
        private static readonly RestClient Client;

        static GistClient(){
            Client =  new RestClient("https://api.github.com");
        }

        public static Dictionary<String,String> SendRequest(RestRequest request){
            var response = Client.Execute(request);
            HandleResponse(response);
            var jsonResponse = TrySerializeResponse(response);
            return jsonResponse;
        }

        public static void SetAuthentication(String username, String password){
            Client.Authenticator = new HttpBasicAuthenticator(username,password.Decrypt());
        }

        public static void HandleResponse(IRestResponse response){
            var statusHeader = response.Headers.FirstOrDefault(x => x.Name == "Status");
            if (statusHeader != null){
                var statusValue = statusHeader.Value.ToString();
                if (!statusValue.Contains("201")){
                    String message = TrySerializeResponse(response)["message"];
                    throw new Exception(statusValue + ", " + message);
                }
            }
            else{
                throw new Exception("Github could not be reached. Verify your connection.");
            }
        }

        private static Dictionary<string, string> TrySerializeResponse(IRestResponse response)
        {
            var deserializer = new JsonDeserializer();
            var jsonResponse = deserializer.Deserialize<Dictionary<String, String>>(response);
            return jsonResponse;
        } 
    }
}
