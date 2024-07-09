using Chilkat;
using MCMAutomation.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MCMAutomation.APIHelpers.NewAppAPI.SignIn.SignInModel;

namespace MCMAutomation.APIHelpers.NewAppAPI.SignIn
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

        public static SignInResponse MakeSignIn(string login, string password)
        {

            Http http = new Http();

            http.Accept = "application/json";

            string url = String.Concat("https://mcm-gateway-dev.azurewebsites.net/" + "/workout/account/sign-in");
            HttpResponse resp = http.PostJson2(url, "application/json", JsonBody(login, password));
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                throw new Exception($"{resp.BodyStr}");
            }

            Debug.WriteLine("Response status code = " + Convert.ToString(resp.StatusCode));
            Debug.WriteLine(resp.BodyStr);
            var token = JsonConvert.DeserializeObject<SignInResponse>(resp.BodyStr);
            return token;

        }
    }
}
