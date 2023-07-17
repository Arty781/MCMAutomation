using Chilkat;
using MCMAutomation.APIHelpers.Client.Membership;
using MCMAutomation.APIHelpers.Client.SignUp;
using MCMAutomation.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telegram.Bot.Types;

namespace MCMAutomation.APIHelpers.Admin.VideosPage
{
    public partial class Videos
    {
        private static string JsonBody(int numOfRecords)
        {
            VideoFilterReq req = new()
            {
                Skip= 0,
                Take= numOfRecords,
                IsForAllMemberships= false,
                Name = "",
                CategoryIds = new List<int> { },
                MembershipIds = new List<int> { },
                TagIds = new List<int> { }
            };
            return JsonConvert.SerializeObject(req);
        }

        private static string JsonBody(EditVideoRequest video, string pageName)
        {
            EditVideoRequest req = new()
            {
                Id = video.Id,
                Name = pageName,
                Description = video.Description,
                Url = video.Url,
                IsForAllMemberships= video.IsForAllMemberships,
                CategoryIds = video.CategoryIds,
                MembershipIds = video.MembershipIds,
                TagIds = video.TagIds
            };
            return JsonConvert.SerializeObject(req);
        }

        public static void AddVideoTags(SignInResponseModel loginResponse)
        {
            foreach(var tag in VideoAdmin.TAGS.Distinct())
            {
                WaitUntil.WaitSomeInterval(700);
                HttpRequest req = new()
                {
                    HttpVerb = "GET",
                    Path = $"/Admin/AddVideoTag/{ToCamelCase(tag)}"
                };
                req.AddHeader("Connection", "Keep-Alive");
                req.AddHeader("Accept", "application /json, text/plain, */*");
                req.AddHeader("Accept-Encoding", "gzip, deflate, br");
                req.AddHeader("Authorization", $"Bearer {loginResponse.AccessToken}");
                Http http = new();
                HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
                if (!resp.StatusCode.ToString().StartsWith("2"))
                {
                    throw new ArgumentException("Request " + resp.Domain + req.Path + "\r\n was failed with: " + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
                }
            }

            static string ToCamelCase(string input)
            {
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

                string[] words = input.Split(' ');

                for (int i = 1; i < words.Length; i++)
                {
                    words[i] = textInfo.ToTitleCase(words[i]);
                }

                return string.Concat(words);
            }

        }

        public static void AddCategories(SignInResponseModel loginResponse)
        {
            foreach (var category in VideoAdmin.CATEGORIES.Distinct())
            {
                WaitUntil.WaitSomeInterval(700);
                HttpRequest req = new()
                {
                    HttpVerb = "GET",
                    Path = $"/Admin/AddVideoCategory/{category}"
                };
                req.AddHeader("Connection", "Keep-Alive");
                req.AddHeader("Accept", "application /json, text/plain, */*");
                req.AddHeader("Accept-Encoding", "gzip, deflate, br");
                req.AddHeader("Authorization", $"Bearer {loginResponse.AccessToken}");
                Http http = new();
                HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
                if (!resp.StatusCode.ToString().StartsWith("2"))
                {
                    throw new ArgumentException("Request " + resp.Domain + req.Path + "\r\n was failed with: " + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
                }
            }
        }

        public static void GetVideosByFilter(SignInResponseModel loginResponse, out List<EditVideoRequest>? response)
        {
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/Admin/GetVideosByFilter",
                ContentType = "application/json"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept", "application /json, text/plain, */*");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {loginResponse.AccessToken}");

            req.LoadBodyFromString(JsonBody(1000), "UTF-8");

            Http http = new();
            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                throw new ArgumentException("Request " + resp.Domain + req.Path + "\r\n was failed with: " + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }

            response = JsonConvert.DeserializeObject<List<EditVideoRequest>>(resp.BodyStr);
        }

        public static void EditVideo(SignInResponseModel loginResponse, EditVideoRequest video, string pageName)
        {
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/Admin/EditVideo",
                ContentType = "application/json"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept", "application /json, text/plain, */*");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {loginResponse.AccessToken}");

            req.LoadBodyFromString(JsonBody(video, pageName), "UTF-8");

            Http http = new();
            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                throw new ArgumentException("Request " + resp.Domain + req.Path + "\r\n was failed with: " + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
            WaitUntil.WaitSomeInterval(700);

        }
    }
}
