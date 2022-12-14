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
        public static string SignIn(string login, string password)
        {
            string str = string.Format("{{" +
                "\"email\"" + ":" + $"\"{login}\"" + "," +
                "\"password\"" + ":" + $"\"{password}\"" + "," +
                "\"type\"" + ":" + "\"RD: SIGN_IN\"" + "}}");
            return str;
        }

        public static SignInResponseModel MakeAdminSignIn(string login, string password)
        {

            Http http = new Http();

            http.Accept = "application/json";

            string url = String.Concat(Endpoints.apiHost + "/Account/SignIn");
            string jsonRequestBody = SignIn(login, password);
            HttpResponse resp = http.PostJson2(url, "application/json", jsonRequestBody);
            if (http.LastMethodSuccess == false)
            {
                Debug.WriteLine(http.LastErrorText);
            }

            Debug.WriteLine("Response status code = " + Convert.ToString(resp.StatusCode));
            string jsonResponseStr = resp.BodyStr;
            var token = JsonConvert.DeserializeObject<SignInResponseModel>(resp.BodyStr);

            Debug.WriteLine(jsonResponseStr);
            return token;

        }
    }
}
