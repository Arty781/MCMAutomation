using Chilkat;
using MCMAutomation.Helpers;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers.Client.AddProgress
{
    public class ProgressRequest
    {
        public static void AddProgress(SignInResponseModel SignIn, int conversionSystem)
        {


            var req = new HttpRequest();
            req.HttpVerb = "POST";
            req.Path = String.Concat("/Progress/Add");
            req.ContentType = "multipart/form-data";
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("weight", $"{RandomHelper.RandomProgressData(ProgressBodyPart.WEIGHT)}");
            req.AddParam("waist", $"{RandomHelper.RandomProgressData(ProgressBodyPart.WAIST)}");
            req.AddParam("hip", $"{RandomHelper.RandomProgressData(ProgressBodyPart.HIP)}");
            req.AddParam("thigh", $"{RandomHelper.RandomProgressData(ProgressBodyPart.THIGH)}");
            req.AddParam("chest", $"{RandomHelper.RandomProgressData(ProgressBodyPart.CHEST)}");
            req.AddParam("arm", $"{RandomHelper.RandomProgressData(ProgressBodyPart.ARM)}");
            req.AddParam("conversionSystem", $"{conversionSystem}");
            req.AddParam("frontPhoto", "");
            req.AddParam("backPhoto", "");
            req.AddParam("sidePhoto", "");
            req.AddParam("type", "ADD_PROGRESS");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }
    }
}
