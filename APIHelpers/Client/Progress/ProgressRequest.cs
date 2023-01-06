﻿using Chilkat;
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
        public static void AddProgress(SignInResponseModel SignIn)
        {


            var req = new HttpRequest();
            req.HttpVerb = "POST";
            req.Path = String.Concat("/Progress/Add");
            req.ContentType = "multipart/form-data";
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("weight", $"{RandomHelper.RandomProgressData("weight")}");
            req.AddParam("waist", $"{RandomHelper.RandomProgressData("waist")}");
            req.AddParam("hip", $"{RandomHelper.RandomProgressData("hip")}");
            req.AddParam("thigh", $"{RandomHelper.RandomProgressData("thigh")}");
            req.AddParam("chest", $"{RandomHelper.RandomProgressData("chest")}");
            req.AddParam("arm", $"{RandomHelper.RandomProgressData("arm")}");
            req.AddParam("conversionSystem", "2");
            req.AddParam("frontPhoto", "");
            req.AddParam("backPhoto", "");
            req.AddParam("sidePhoto", "");
            req.AddParam("type", "ADD_PROGRESS");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                Debug.WriteLine(http.LastErrorText);
                return;
            }
            Debug.WriteLine("HTTP response status: " + Convert.ToString(resp.StatusCode));
        }
    }
}