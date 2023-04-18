using Chilkat;
using MCMAutomation.Helpers;
using Newtonsoft.Json;
using RimuTec.Faker;
using System;
using System.Diagnostics;

namespace MCMAutomation.APIHelpers.Client.SignUp
{
    public class SignUpRequest
    {
        public static string Json(string email) 
        {
            var reqModel = new SignUp()
            {
                FirstName = Name.FirstName(),
                LastName = Name.LastName(),
                Email = email,
                ConfirmEmail = email,
                Password= "Qaz11111!",
                ConfirmPassword = "Qaz11111!",
                Type = "RD : SIGN_UP"
            };

            return JsonConvert.SerializeObject(reqModel);
        }


        public static void RegisterNewUser(string email)
        {
            string url = String.Concat(Endpoints.API_HOST_GET + "/Account/SignUp");
            Http req = new()
            {
                Accept = "application/json"
            };
            HttpResponse resp = req.PostJson2(url, "application/json", Json(email));
            if (req.LastMethodSuccess == false)
            {
                Debug.WriteLine(req.LastErrorText);
            }
            Debug.WriteLine("Response status code = " + Convert.ToString(resp.StatusCode));
            Debug.WriteLine("Response message is " + Convert.ToString(resp.BodyStr));
        }
    }
}
