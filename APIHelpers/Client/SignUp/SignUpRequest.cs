using Chilkat;
using MCMAutomation.Helpers;
using Newtonsoft.Json;
using RimuTec.Faker;
using System;

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
            string url = String.Concat(Endpoints.apiHost + "/Account/SignUp");
            Http req = new();
            req.Accept = "application/json";
            HttpResponse resp = req.PostJson2(url, "application/json", Json(email));
            if (req.LastMethodSuccess == false)
            {
                Console.WriteLine(req.LastErrorText);
            }
            Console.WriteLine("Response status code = " + Convert.ToString(resp.StatusCode));
            Console.WriteLine("Response message is " + Convert.ToString(resp.BodyStr));
        }
    }
}
