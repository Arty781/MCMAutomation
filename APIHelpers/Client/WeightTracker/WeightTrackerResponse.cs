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
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
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
                if (!resp.StatusCode.ToString().StartsWith("2"))
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
            req.Path = String.Concat("/Progress/EditDaily");
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
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
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
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }

        public static void VerifyIsDeletedProgressDaily(SignInResponseModel responseLoginUser, string userId)
        {
            WaitUntil.WaitSomeInterval(1000);
            var userProgress = AppDbContext.User.GetAllProgressDailyByUserId(userId);
            foreach (var progress in userProgress)
            {
                Assert.That(progress.IsDeleted, Is.True, "Ids don't match");
            }
            var s = WeightTracker.GetWeightList(responseLoginUser, ConversionSystem.METRIC, 0, userProgress.Count);

            Assert.IsTrue(s.Count == 0);
        }

        public static void VerifyAverageOfWeightTracking(SignInResponseModel responseLoginUser, int conversionSystem, string userId)
        {
            var userProgressExpected = AppDbContext.User.GetProgressDailyByUserId(userId);
            var expectedAverages = CalculateAverages(userProgressExpected, conversionSystem);
            var weightList = GetWeightList(responseLoginUser, conversionSystem, 0, userProgressExpected.Count);
            var actualAverages = weightList.Select(x => decimal.Round((decimal)x.averageWeight, 1)).ToList();
            Assert.Multiple(() =>
            {
                Assert.That(actualAverages, Is.EqualTo(expectedAverages).Within(0.6), "Weights don't match");

                // Ensure that expected and actual average lists have the same number of elements
                Assert.That(expectedAverages.Count, Is.EqualTo(actualAverages.Count), "Number of elements doesn't match");

                // Define a tolerance range for the weights
                decimal tolerance = 0.8m;

                // Find the indices where the expected and actual averages don't match within the tolerance range
                var mismatchedIndices = expectedAverages.Select((avg, index) => new { avg, index })
                    .Where(item => Math.Abs(item.avg - actualAverages[item.index]) > tolerance)
                    .Select(item => item.index)
                    .ToList();

                // If there are any mismatched indices, output an error message indicating where the mismatch occurred
                if (mismatchedIndices.Count > 0)
                {
                    Assert.Fail($"Max count is: {expectedAverages.Count} \r\n");
                    foreach (var mismatchedIndex in mismatchedIndices)
                    {
                        string errorMessage = $"Weights don't match at index(es): {string.Join(", ", mismatchedIndex)}{string.Concat("\r\n Expected average: ", expectedAverages[mismatchedIndex])}{string.Concat("\r\n Actual average: ", actualAverages[mismatchedIndex])}";
                        Assert.Fail(errorMessage);
                    }

                }
            });

        }

        private static List<decimal> CalculateAverages(List<DB.ProgressDaily> progress, int convesionSystem)
        {
            var averages = new List<decimal>();
            int count = 0;

            foreach (var partOfList in progress.Select((p, i) => i <= 6 ? progress.Take(i + 1) : progress.Skip(i - 7 + 1).Take(7)))
            {
                count++;
                decimal average =  
                  convesionSystem == ConversionSystem.METRIC ?
                  partOfList.Average(p => p.Weight) :
                  partOfList.Average(p => decimal.Round(p.Weight * 2.2m, 8));

                averages.Add(ConvertToDecimalWithFixedDecimalPlaces(average));
            }

            averages.Reverse();
            return averages;
        }

        private static decimal ConvertToDecimalWithFixedDecimalPlaces(decimal value)
        {
            return (value == 0 || value % 1 == 0) ? decimal.ToInt32(value) : Math.Round(value, 1);
        }


        public static void VerifyAddedWeight(SignInResponseModel responseLoginUser, int conversionSystem, string userId)
        {
            const string dateFormat = "yyyy-MM-ddTHH:mm:ss";
            var userProgress = AppDbContext.User.GetProgressDailyByUserId(userId);
            var weightList = GetWeightList(responseLoginUser, conversionSystem, 0, userProgress.Count);

            IEnumerable<decimal> weightListWeights = weightList
                .Select(w => conversionSystem == ConversionSystem.IMPERIAL ? Math.Round(w.weight / 2.2m, 1) : w.weight)
                .Reverse();

            Assert.Multiple(() =>
            {
                Assert.That(userProgress.Select(p => p.Id), Is.EqualTo(weightList.Select(w => w.id).Reverse()), "Record IDs don't match.");
                Assert.That(userProgress.Select(p => p.Date.ToString(dateFormat)), Is.EqualTo(weightList.Select(w => w.date).Reverse()), "Record dates don't match.");
                Assert.That(userProgress.Select(p => p.Weight), Is.EqualTo(weightListWeights).Within(0.5), "Record weights don't match.");
            });
        }

        public static void VerifyChangedWeekWeight(SignInResponseModel responseLoginUser, int conversionSystem, string userId)
        {
            var userProgressExpected = AppDbContext.User.GetProgressDailyByUserId(userId);
            List<decimal> expectedAverages = CalculateAverages(userProgressExpected, conversionSystem);
            var weightList = GetWeightList(responseLoginUser, conversionSystem, 0, userProgressExpected.Count);
            var expectedChangeWeek = CalculateWeeklyWeightChanges(expectedAverages);
            var actualChangeWeek = weightList.Select(x => decimal.Round((decimal)x.changeWeek, 1)).ToList();
            Assert.Multiple(() =>
            {
                Assert.That(actualChangeWeek, Is.EqualTo(expectedChangeWeek).Within(0.6), "Weights don't match");

                // Ensure that expected and actual average lists have the same number of elements
                Assert.That(expectedChangeWeek.Count, Is.EqualTo(actualChangeWeek.Count), "Number of elements doesn't match");

                // Define a tolerance range for the weights
                decimal tolerance = 0.8m;

                // Find the indices where the expected and actual averages don't match within the tolerance range
                var mismatchedIndices = expectedChangeWeek.Select((avg, index) => new { avg, index })
                    .Where(item => Math.Abs(item.avg - actualChangeWeek[item.index]) > tolerance)
                    .Select(item => item.index)
                    .ToList();

                // If there are any mismatched indices, output an error message indicating where the mismatch occurred
                if (mismatchedIndices.Count > 0)
                {
                    Assert.Fail($"Max count is: {expectedChangeWeek.Count} \r\n");
                    foreach (var mismatchedIndex in mismatchedIndices)
                    {
                        string errorMessage = $"Weights don't match at index(es): {string.Join(", ", mismatchedIndex)}{string.Concat("\r\n Expected ChangeWeek: ", expectedChangeWeek[mismatchedIndex])}{string.Concat("\r\n Actual ChangeWeek: ", actualChangeWeek[mismatchedIndex])}";
                        Assert.Fail(errorMessage);
                    }

                }
            });
        }

        private static List<decimal> CalculateWeeklyWeightChanges(List<decimal> averages)
        {
            averages.Reverse();
            var changeWeeks = averages.Select((average, i) =>
            {
                if (i < 7)
                {
                    return 0m;
                }
                //if (i == 7)
                //{
                //    return ConvertToDecimalWithFixedDecimalPlaces(averages[i] - averages[i - 6]);
                //}
                else
                {

                    return ConvertToDecimalWithFixedDecimalPlaces(averages[i] - averages[i - 7]);
                }
            }).ToList();
            changeWeeks.Reverse();
            return changeWeeks;
        }



    }
}
