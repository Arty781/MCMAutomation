using Chilkat;
using MCMAutomation.Helpers;
using MCMAutomation.PageObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers.NewAppAPI.User.Progress
{
    public partial class ProgressNewReq
    {
        public static async System.Threading.Tasks.Task<ProgressModelResponse> AddWeeklyProgress(string token)
        {
            string url = $"mcm-gateway-dev.azurewebsites.net";
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = $"/progress/weekly/add",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {token}");




            Http http = new();
            var resp = http.SynchronousRequest(url, 443, true, CreateMultiPartFormBody(req));
            var respons = resp.StatusCode.ToString().StartsWith("2")
                ? JsonConvert.DeserializeObject<ProgressModelResponse>(resp.BodyStr) ?? throw new Exception("Response body is null.")
                : throw new ArgumentException(http.LastErrorText);

            return respons;
        }

        public static async System.Threading.Tasks.Task<ProgressModelResponse> AddDailyProgress(string token)
        {
            string url = $"mcm-gateway-dev.azurewebsites.net";
            HttpRequest req = new()
            {
                HttpVerb = "PUT",
                Path = $"/progress/daily/add",
                ContentType = "application/json"
            };
            
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept", "application /json, text/plain, */*");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {token}");

            req.LoadBodyFromString(JsonBody(), "UTF-8");

            Http http = new();
            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);

            var respons = resp.StatusCode.ToString().StartsWith("2")
                ? JsonConvert.DeserializeObject<ProgressModelResponse>(resp.BodyStr) ?? throw new Exception("Response body is null.")
                : throw new ArgumentException(http.LastErrorText);

            return respons;
        }

        private static HttpRequest CreateMultiPartFormBody(HttpRequest req)
        {
            var progressReq = ProgressNewReq.GenerateReq();
            req.AddParam("CalorieTargetMet", progressReq.CalorieTargetMet);
            req.AddParam("PeriodAffectsWeight", progressReq.PeriodAffectsWeight);
            req.AddParam("StepGoalMet", progressReq.StepGoalMet);
            req.AddParam("SleepHours", progressReq.SleepHours);
            req.AddParam("HungerLevel", progressReq.HungerLevel);
            req.AddParam("EnergyLevel", progressReq.EnergyLevel);
            req.AddParam("StressLevel", progressReq.StressLevel);
            req.AddParam("Weight", progressReq.Weight);
            req.AddParam("Waist", progressReq.Waist);
            req.AddParam("Hip", progressReq.Hip);
            req.AddParam("Thigh ", progressReq.Thigh);
            req.AddParam("Chest ", progressReq.Chest);
            req.AddParam("Arm ", progressReq.Arm);
            //req.AddParam("FrontPhoto ", progressReq.FrontPhoto);
            //req.AddParam("BackPhoto ", progressReq.BackPhoto);
            //req.AddParam("SidePhoto ", progressReq.SidePhoto);
            req.AddParam("MeasurementUnit ", progressReq.MeasurementUnit);
            req.AddParam("GoalId ", progressReq.GoalId);
            req.AddParam("TrackMacros  ", progressReq.TrackMacros);

            return req;
        }

        private static string JsonBody()
        {
            var body = new ProgressDailyModel
            {
                date = DateTime.Now.ToString("yyyy-MM-dd"),
                weight = RandomHelper.RandomProgressData("weight"),
                calorieTargetMet = RandomHelper.RandomBool(),
                stepGoalMet = RandomHelper.RandomBool(),
                measurementUnit = RandomHelper.RandomNum(1)
            };

            return JsonConvert.SerializeObject(body);
        }
    }
}
