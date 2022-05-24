using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class RestSharpHelper
    {
        public static async void GetLoginAdmin()
        {
            const string json = "{\"email\":\"admin@coachmarkcarroll.com\",\"password\":\"Upgr@de21\",\"type\":\"RD:SIGN_IN\"}";
            var client = new RestClient("https://mcmstaging-api.azurewebsites.net/");

            var request = new RestRequest("Account/SignIn", Method.Post);
            request.AddStringBody(json, ContentType.Json);

            var response = await client.GetAsync(request);
        }
        
    }
}
