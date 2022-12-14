using MCMAutomation.Helpers;
using Newtonsoft.Json;
using RestSharp;
using Chilkat;
using MCMAutomation.PageObjects;
using System.Diagnostics;
using System;
using RimuTec.Faker;
using NUnit.Framework.Internal;

namespace MCMAutomation.APIHelpers
{
    public class MembershipsWithUsersRequest
    {
        public static MembershipsWithUsersResponse GetMembershipsWithUsersList(SignInResponseModel SignIn)
        {
            HttpRequest req = new()
            {
                HttpVerb = "GET",
                Path = "/Admin/GetMembershipsWithUsers"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                Console.WriteLine(http.LastErrorText);
            }
            var countdownResponse = JsonConvert.DeserializeObject<MembershipsWithUsersResponse>(resp.BodyStr);
            return countdownResponse;
        }

        public static void CreateMembership(SignInResponseModel SignIn)
        {
            HttpRequest req = new HttpRequest
            {
                HttpVerb = "POST",
                Path = "/Admin/AddMembership",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("sku", MembershipsSKU.MEMBERSHIP_SKU[1]);
            req.AddParam("name", "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            req.AddParam("description", Lorem.ParagraphByChars(792));
            req.AddParam("startDate", "");
            req.AddParam("endDate", "");
            req.AddParam("price", "0");
            req.AddParam("accessWeekLength", "12");
            req.AddParam("url", "https://mcmstaging-ui.azurewebsites.net/programs/all");
            req.AddParam("type", "0");
            req.AddParam("image", "undefined");
            req.AddParam("gender", "0");
            req.AddParam("relatedMembershipIds", "");
            req.AddParam("forPurchase", "true");
            req.AddParam("membershipLevels", "[]");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                Console.WriteLine(http.LastErrorText);
                return;
            }
            Console.WriteLine("Error message is " + Convert.ToString(resp.BodyStr));
        }

        public static void CreateCustomMembership(SignInResponseModel SignIn, string UserId)
        {
            HttpRequest req = new HttpRequest
            {
                HttpVerb = "POST",
                Path = "/Admin/AddMembership",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("name", "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            req.AddParam("description", Lorem.ParagraphByChars(792));
            req.AddParam("startDate", "");
            req.AddParam("endDate", "");
            req.AddParam("accessWeekLength", "16");
            req.AddParam("image", "undefined");
            req.AddParam("userId", $"{UserId}");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                Console.WriteLine(http.LastErrorText);
                return;
            }
            Console.WriteLine("Error message is " + Convert.ToString(resp.BodyStr));
        }

        public static void CreatePrograms(SignInResponseModel SignIn, int MembershipId)
        {
            HttpRequest req = new HttpRequest
            {
                HttpVerb = "POST",
                Path = "/Admin/AddProgram",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("programName", "Phase " + DateTime.Now.ToString("O"));
            req.AddParam("numberOfWeeks", "4");
            req.AddParam("steps", "Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
            req.AddParam("availableDate", String.Empty);
            req.AddParam("expirationDate", String.Empty);
            req.AddParam("membershipId", $"{MembershipId}");
            req.AddParam("image", "undefined");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                Debug.WriteLine(http.LastErrorText);
                return;
            }
            Debug.WriteLine(resp.BodyStr);
        }

        public static void CreateWorkouts(SignInResponseModel SignIn, int ProgramId)
        {
            Http http = new Http();

            http.Accept = "application/json";
            http.AuthToken= "Bearer " + SignIn.AccessToken;

            string url = String.Concat(Endpoints.apiHost + "/Admin/AddWorkout");
            string jsonRequestBody = "{"+
                                        "\r\n"+$"\"programId\": {ProgramId}"+ ","+
                                        "\r\n \"name\": \"Phase" +$"{DateTime.Now.ToString("O")}\""+","+
                                        $"\r\n \"weekDay\": {RandomHelper.RandomNumFromOne(7)}"+"}";
            HttpResponse resp = http.PostJson2(url, "application/json", jsonRequestBody);
            if (http.LastMethodSuccess == false)
            {
                Debug.WriteLine(http.LastErrorText);
            }
            Debug.WriteLine(resp.BodyStr);
        }
    }
}
