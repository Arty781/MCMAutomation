using Chilkat;
using MCMAutomation.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers
{
    public class SignInRequest
    {
        private static string JsonBody(string login, string password)
        {
            var req = new SignInRequestModel()
            {
                Email = login,
                Password = password,
                Type = "RD: SIGN_IN"
            }; 
            return JsonConvert.SerializeObject(req);
        }

        public static SignInResponseModel MakeAdminSignIn(string login, string password)
        {

            Http http = new Http();

            http.Accept = "application/json";

            string url = String.Concat(Endpoints.apiHost + "/Account/SignIn");
            HttpResponse resp = http.PostJson2(url, "application/json", JsonBody(login, password));
            if (http.LastMethodSuccess == false)
            {
                Debug.WriteLine(http.LastErrorText);
            }

            Debug.WriteLine("Response status code = " + Convert.ToString(resp.StatusCode));
            Debug.WriteLine(resp.BodyStr);
            var token = JsonConvert.DeserializeObject<SignInResponseModel>(resp.BodyStr);
            return token;

        }
    }
}
