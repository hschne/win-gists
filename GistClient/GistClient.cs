using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;

namespace GistClient
{
    public static class GistClient
    {
        private static readonly RestClient Client;

        static GistClient(){
            Client =  new RestClient("https://api.github.com");
        }

        public static Dictionary<String,String> SendRequest(RestRequest request){
            var response = Client.Execute(request);
            var deserializer = new JsonDeserializer();
            var jsonResponse = deserializer.Deserialize<Dictionary<String, String>>(response);
            return jsonResponse;
        }

        public static void SetAuthentication(String username, String password){
            Client.Authenticator = new HttpBasicAuthenticator(username,password);
        }
    }
}
