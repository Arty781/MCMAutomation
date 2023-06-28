using Chilkat;
using MCMAutomation.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace MCMAutomation.APIHelpers
{
    public class ExercisesRequestPage
    {
        private static List<RelatedExerciseRequest> AddRelatedexercises(List<DB.Exercises> exercises, bool home, bool all)
        {
            var relatedExercises = new List<RelatedExerciseRequest>();
            if (all == true)
            {
                for (int i = 0; i < 10; i++)
                {
                    var relatedExercise = new RelatedExerciseRequest();
                    if (i <= 5)
                    {
                        relatedExercise.ExerciseId = exercises[RandomHelper.RandomExercise(exercises.Count)].Id;
                        relatedExercise.ExerciseType = 0;
                    }
                    else if (i <= 10)
                    {
                        relatedExercise.ExerciseId = exercises[RandomHelper.RandomExercise(exercises.Count)].Id;
                        relatedExercise.ExerciseType = 1;
                    }
                    relatedExercises.Add(relatedExercise);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    var relatedExercise = new RelatedExerciseRequest();
                    if (home == false && i <= 5)
                    {
                        relatedExercise.ExerciseId = exercises[RandomHelper.RandomExercise(exercises.Count)].Id;
                        relatedExercise.ExerciseType = 0;
                        relatedExercises.Add(relatedExercise);
                    }
                    else if (home == true && i > 5 && i <= 10)
                    {
                        relatedExercise.ExerciseId = exercises[RandomHelper.RandomExercise(exercises.Count)].Id;
                        relatedExercise.ExerciseType = 1;
                        relatedExercises.Add(relatedExercise);
                    }
                    
                }
            }

            return relatedExercises;
        }
        private static string JsonBody(List<DB.Exercises> exercises, bool home, bool all)
        {
            RequestAddExercises req = new();
            req.Name = string.Concat("Test Exercise", DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fff"));
            req.VideoURL = exercises[RandomHelper.RandomExercise(exercises.Count)].VideoURL;
            req.TempoBold = 1;
            req.RelatedExercises = AddRelatedexercises(exercises, home, all);
           

            return JsonConvert.SerializeObject(req);
        }
        private static string JsonBodyWithoutRelated(List<DB.Exercises> exercises)
        {
            RequestAddExercises req = new()
            {
                Name = "Test Exercise" + DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fff"),
                VideoURL = exercises[RandomHelper.RandomExercise(exercises.Count)].VideoURL,
                TempoBold = 1,
                RelatedExercises = new List<RelatedExerciseRequest>() { }
            };

            return JsonConvert.SerializeObject(req);
        }
        private static string JsonBody(List<DB.Exercises> exercises, List<ResponseGetExercises> listGetExercises, string exerciseName, bool home, bool all)
        {
            RequestEditExercises req = new()
            {
                Id = listGetExercises.Where(p=>p.Name.Contains(exerciseName)).Select(x=>x.Id).First(),
                Name = "Test Edited Exercise" + DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fff"),
                VideoURL = exercises[RandomHelper.RandomExercise(exercises.Count)].VideoURL,
                TempoBold = 3,
                RelatedExercises = AddRelatedexercises(exercises, home, all),
                GroupId = listGetExercises.Where(p => p.Name.Contains(exerciseName)).Select(x => x.GroupId).First()
            };

            return JsonConvert.SerializeObject(req);
        }

        private static string JsonBodyWithoutRelated(List<DB.Exercises> exercises, List<ResponseGetExercises> listGetExercises, string exerciseName)
        {
            RequestEditExercises req = new()
            {
                Id = listGetExercises.Where(p => p.Name.Contains(exerciseName)).Select(x => x.Id).First(),
                Name = "Test Edited Exercise" + DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fff"),
                VideoURL = exercises[RandomHelper.RandomExercise(exercises.Count)].VideoURL,
                TempoBold = 3,
                RelatedExercises = new List<RelatedExerciseRequest>() { },
                GroupId = listGetExercises.Where(p => p.Name.Contains(exerciseName)).Select(x => x.GroupId).First()
            };

            return JsonConvert.SerializeObject(req);
        }
        public static void AddExercisesWithRelated(SignInResponseModel SignIn, List<DB.Exercises> exercises, bool home, bool all)
        {
            Http http = new()
            {
                Accept = "application/json",
                AuthToken = "Bearer " + SignIn.AccessToken
            };
            string url = String.Concat(Endpoints.API_HOST + "/Admin/AddExercise");
            HttpResponse resp = http.PostJson2(url, "application/json", JsonBody(exercises, home, all));
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                Debug.WriteLine(http.LastErrorText);
            }
            Debug.WriteLine(resp.BodyStr);
        }
        public static void AddExercisesWithoutRelated(SignInResponseModel SignIn, List<DB.Exercises> exercises)
        {
            Http http = new()
            {
                Accept = "application/json",
                AuthToken = "Bearer " + SignIn.AccessToken
            };
            string url = String.Concat(Endpoints.API_HOST + "/Admin/AddExercise");
            HttpResponse resp = http.PostJson2(url, "application/json", JsonBodyWithoutRelated(exercises));
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                Debug.WriteLine(http.LastErrorText);
            }
            Debug.WriteLine(resp.BodyStr);
        }

        public static List<ResponseGetExercises> GetExercisesList(SignInResponseModel SignIn)
        {
            HttpRequest req = new()
            {
                HttpVerb = "GET",
                Path = "/Admin/GetExerciseList"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
            var countdownResponse = JsonConvert.DeserializeObject<List<ResponseGetExercises>>(resp.BodyStr);
            return countdownResponse;
        }

        public static void VerifyExerciseAdded(SignInResponseModel SignIn, string exerciseName)
        {
            HttpRequest req = new()
            {
                HttpVerb = "GET",
                Path = "/Admin/GetExerciseList"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
            var countdownResponse = JsonConvert.DeserializeObject<List<ResponseGetExercises>>(resp.BodyStr);
            Assert.IsTrue(countdownResponse.Any(e => e.Name.Contains(exerciseName)), $"Exercise with name {exerciseName} was not found.");
        }

        public static void EditExercisesWithRelated(SignInResponseModel SignIn, List<DB.Exercises> exercises, List<ResponseGetExercises> listGetExercises, string exerciseName, bool home, bool all)
        {
            Http http = new()
            {
                Accept = "application/json",
                AuthToken = "Bearer " + SignIn.AccessToken
            };
            string url = String.Concat(Endpoints.API_HOST + "/Admin/EditExercise");
            HttpResponse resp = http.PostJson2(url, "application/json", JsonBody(exercises, listGetExercises, exerciseName, home, all));
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                Debug.WriteLine(http.LastErrorText);
            }
            Debug.WriteLine(resp.BodyStr);
        }

        public static void EditExercisesWithoutRelated(SignInResponseModel SignIn, List<DB.Exercises> exercises, List<ResponseGetExercises> listGetExercises, string exerciseName)
        {
            Http http = new()
            {
                Accept = "application/json",
                AuthToken = "Bearer " + SignIn.AccessToken
            };
            string url = String.Concat(Endpoints.API_HOST + "/Admin/EditExercise");
            HttpResponse resp = http.PostJson2(url, "application/json", JsonBodyWithoutRelated(exercises, listGetExercises, exerciseName));
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                Debug.WriteLine(http.LastErrorText);
            }
            Debug.WriteLine(resp.BodyStr);
        }

    }
}
