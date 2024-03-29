﻿using MCMAutomation.Helpers;
using RestSharp;
using System;
using Chilkat;
using System.Diagnostics;
using RimuTec.Faker;

namespace MCMAutomation.APIHelpers.Client.EditUser
{
    public class EditUserRequest
    {
        public static void EditUser(SignInResponseModel SignIn)
        {
            var req = new HttpRequest
            {
                HttpVerb = "POST",
                Path = String.Concat("/Account/EditAccount"),
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("lastName", Name.FirstName());
            req.AddParam("firstName", Name.LastName());
            req.AddParam("birthdate", Date.Birthday().ToString());
            req.AddParam("gender", "2");
            req.AddParam("carbs", "260");
            req.AddParam("photo", "null");
            req.AddParam("weight", $"{RandomHelper.RandomWeight()}.{RandomHelper.RandomNumFromOne()}");
            req.AddParam("calories", "1000");
            req.AddParam("type", "RD:EDIT_ACCOUNT_ACTION");
            req.AddParam("conversionSystem", "2");
            req.AddParam("bodyfat", "12");
            req.AddParam("maintenanceCalories", "1500");
            req.AddParam("fats", "52");
            req.AddParam("protein", "122");
            req.AddParam("activityLevel", "3");
            req.AddParam("email", $"{SignIn.Email}");
            req.AddParam("height", "175");
            req.AddParam("oldPassword", "");
            req.AddParam("changePassword", "");
            req.AddParam("confirmPassword", "");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }

        public static void EditUser(SignInResponseModel SignIn, dynamic bodyFat, int gender)
        {


            var req = new HttpRequest
            {
                HttpVerb = "POST",
                Path = String.Concat("/Account/EditAccount"),
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("lastName", Name.FirstName());
            req.AddParam("firstName", Name.LastName());
            req.AddParam("birthdate", DateTime.Now.AddYears(-31).ToString("yyyy-MM-dd' 'HH:mm:ss.ffff"));
            req.AddParam("gender", $"{gender}");
            req.AddParam("carbs", "260");
            req.AddParam("photo", "null");
            req.AddParam("weight", $"{RandomHelper.RandomWeight()}.{RandomHelper.RandomNumFromOne()}");
            req.AddParam("calories", "1000");
            req.AddParam("type", "RD:EDIT_ACCOUNT_ACTION");
            req.AddParam("conversionSystem", "2");
            req.AddParam("bodyfat", $"{bodyFat}");
            req.AddParam("maintenanceCalories", "1500");
            req.AddParam("fats", "52");
            req.AddParam("protein", "122");
            req.AddParam("activityLevel", "3");
            req.AddParam("email", $"{SignIn.Email}");
            req.AddParam("height", "175");
            req.AddParam("oldPassword", "");
            req.AddParam("changePassword", "");
            req.AddParam("confirmPassword", "");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }
    }
}
