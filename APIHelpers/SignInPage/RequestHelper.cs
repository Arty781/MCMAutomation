using MCMAutomation.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers
{
    public class SignInRequestHelper
    {
       
        public static string SignIn(string login, string password)
        {
            string str = string.Format("{{" + 
                "\"email\"" + ":" + $"\"{login}\"" + "," + 
                "\"password\"" + ":" + $"\"{password}\"" + "," + 
                "\"type\"" + ":" + "\"RD: SIGN_IN\"" + "}}");
            return str;
        }

        public static string MakeSignIn(string login, string password)
        {
            
            
            var restDriver = new RestClient(Endpoints.apiHost);
            RestRequest? request = new RestRequest("/Account/SignIn", Method.Post);
            request.AddHeaders(headers: Headers.HeadersCommon());
            request.AddJsonBody(SignIn(login, password));

            var response = restDriver.Execute(request);
            var content = response.Content;

            var token = JsonConvert.DeserializeObject<SignInResponseModelHelper>(content);

            return token.AccessToken;
        }

    }
}
