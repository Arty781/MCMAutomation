using Chilkat;
using MCMAutomation.Helpers;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MCMAutomation.APIHelpers.Client.WeightTracker
{
    public class WeightTracker
    {
        public static void AddWeight(SignInResponseModel SignIn, int weight, string date, int conversionSystem, bool isConfirmed)
        {
            var req = new HttpRequest();
            req.HttpVerb = "POST";
            req.Path = String.Concat("/Progress/AddDaily");
            req.ContentType = "multipart/form-data";
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("Weight", $"{weight}");
            req.AddParam("Date", date);
            req.AddParam("ConversionSystem", $"{conversionSystem}");
            req.AddParam("IsConfirmedRewriting", $"{isConfirmed}");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                Debug.WriteLine(http.LastErrorText);
                return;
            }
            Debug.WriteLine("HTTP response status: " + Convert.ToString(resp.StatusCode));
        }

        public static List<WeightTrackerResponse> GetWeightList(SignInResponseModel SignIn, int conversionSystem, int countSkipRecords, int countTakeRecords)
        {
            Http http = new()
            {
                Accept = "application/json",
                AuthToken = "Bearer " + SignIn.AccessToken
            };

            string url = String.Concat(Endpoints.API_HOST + "/Progress/GetDailyList");
            HttpResponse resp = http.PostJson2(url, "application/json", JsonBody(conversionSystem, countSkipRecords, countTakeRecords));
                if (http.LastMethodSuccess == false)
                {
                    Debug.WriteLine(http.LastErrorText);
                }
                Debug.WriteLine(resp.BodyStr);

            return JsonConvert.DeserializeObject<List<WeightTrackerResponse>>(resp.BodyStr);
           
        }

        private static string JsonBody(int conversionSystem, int countSkipRecords, int countTakeRecords)
        {
            WeightTrackerRequest req = new()
            {
                Skip = countSkipRecords,
                Take = countTakeRecords,
                ConversionSystem = conversionSystem
            };

            return JsonConvert.SerializeObject(req);
        }

        public static void EditWeight(SignInResponseModel SignIn, int weight, int conversionSystem, int id)
        {
            var req = new HttpRequest();
            req.HttpVerb = "POST";
            req.Path = String.Concat("/Progress/AddDaily");
            req.ContentType = "multipart/form-data";
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("Weight", $"{weight}");
            req.AddParam("ConversionSystem", $"{conversionSystem}");
            req.AddParam("Id", $"{id}");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                Debug.WriteLine(http.LastErrorText);
                return;
            }
            Debug.WriteLine("HTTP response status: " + Convert.ToString(resp.StatusCode));
        }

        public static void DeleteWeight(SignInResponseModel SignIn, int id)
        {
            var req = new HttpRequest();
            req.HttpVerb = "GET";
            req.Path = String.Concat($"/Progress/DeleteDaily/{id}");
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");
            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                Debug.WriteLine(http.LastErrorText);
                return;
            }
            Debug.WriteLine("HTTP response status: " + Convert.ToString(resp.StatusCode));
        }

        public static void VerifyAverageOfWeightTracking(List<DB.ProgressDaily> userProgressExpected, List<WeightTrackerResponse> userProgressActual)
        {
            List<decimal> averageListExpected = new List<decimal>();
            List<decimal> averageListActual = new List<decimal>();
            decimal sum = 0m;
            int count = 0;

            for (int i = 0; i < userProgressExpected.Count; i++)
            {
                if(i <= 7)
                {
                    var partOfList = userProgressExpected.Take(i+1);
                    count++;
                    decimal average = (count == 1) ? 0m : partOfList.Average(p=> p.Weight);
                    average = decimal.Round(average, 4);
                    string formattedAverage = average.ToString("0.00000");
                    decimal finalAverage = decimal.Parse(formattedAverage);
                    averageListExpected.Add(finalAverage);
                }
                else if (i >= 8)
                {
                    var partOfList = userProgressExpected.Skip(i-7+1).Take(7);
                    count++;
                    decimal average = partOfList.Average(p => p.Weight);
                    average = decimal.Round(average, 4);
                    string formattedAverage = average.ToString("0.00000");
                    decimal finalAverage = decimal.Parse(formattedAverage);
                    averageListExpected.Add(finalAverage);
                }

            }
            averageListExpected.Reverse();
            count = 0;
            for (int i = userProgressActual.Count - 1; i >= 0; i--)
            {
                count++;
                decimal average = (decimal)userProgressActual[i].averageWeight;
                average = decimal.Round(average, 4);
                string formattedAverage = average.ToString("0.00000");
                decimal finalAverage = decimal.Parse(formattedAverage);
                averageListActual.Add(finalAverage);

            }
            averageListActual.Reverse();
            Assert.That(averageListExpected.Select(p => p).SequenceEqual(averageListActual), Is.True, "Weights don't match");

        }
    }
}
