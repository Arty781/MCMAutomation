using Chilkat;
using MCMAutomation.APIHelpers.Client.SignUp;
using MCMAutomation.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers
{
    public class SignInRequest
    {
        private static string JsonBody(string login, string password)
        {
            var req = new SignInRequestModel()
            {
                Email = login,
                Password = password,
                Type = "RD: SIGN_IN"
            }; 
            return JsonConvert.SerializeObject(req);
        }

        public static SignInResponseModel MakeSignIn(string login, string password)
        {

            Http http = new Http();

            http.Accept = "application/json";

            string url = String.Concat(Endpoints.API_HOST + "/Account/SignIn");
            HttpResponse resp = http.PostJson2(url, "application/json", JsonBody(login, password));
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                throw new Exception($"{resp.BodyStr}");
            }

            Debug.WriteLine("Response status code = " + Convert.ToString(resp.StatusCode));
            Debug.WriteLine(resp.BodyStr);
            var token = JsonConvert.DeserializeObject<SignInResponseModel>(resp.BodyStr);
            return token;

        }
    }

    public class DemoTests
    {
        private static string JsonBody(string login, string password)
        {
            var req = new SignInRequestModel()
            {
                Email = login,
                Password = password,
                Type = "RD: SIGN_IN"
            };
            return JsonConvert.SerializeObject(req);
        }
        public static SignInResponseModel MakeSignIn(string login, string password)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Http http = new Http();

            http.Accept = "application/json";

            string url = String.Concat(Endpoints.API_HOST + "/Account/SignIn");
            HttpResponse resp = http.PostJson2(url, "application/json", JsonBody(login, password));
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                Debug.WriteLine(http.LastErrorText);
            }

            Debug.WriteLine("Response status code = " + Convert.ToString(resp.StatusCode));
            Debug.WriteLine(resp.BodyStr);
            var token = JsonConvert.DeserializeObject<SignInResponseModel>(resp.BodyStr);

            stopwatch.Stop();

            double requestsPerSecond = 1 / stopwatch.Elapsed.TotalSeconds;

            Console.WriteLine($"Requests per second: {requestsPerSecond}");

            return token;

           

        }

        public static void ImportUserData(SignInResponseModel token, int count)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            HttpRequest req = new()
            {
                HttpVerb = "GET",
                Path = "/Account/ImportUserData"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {token.AccessToken}");

            Http http = new();
            Parallel.For(0, 50, (int i) =>
            {
                for (int j = 0; j < count; j++)
                {
                    HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
                    if (http.LastMethodSuccess != true)
                    {
                        throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
                    }
                }
            });

            stopwatch.Stop();

            double requestsPerSecond = count * 10 / stopwatch.Elapsed.TotalSeconds;

            Console.WriteLine($"Requests per second: {requestsPerSecond}");
        }

        public static void GetUsermemberships(SignInResponseModel token)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Http http = new();
            Parallel.For(0, 500, (int i) =>
            {
                
                for (int j = 0; j < 10; j++)
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    HttpRequest req = new()
                    {
                        HttpVerb = "GET",
                        Path = "/Admin/GetMembershipsWithUsers"
                    };
                    req.AddHeader("Connection", "Keep-Alive");
                    req.AddHeader("accept-encoding", "gzip, deflate, br");
                    req.AddHeader("authorization", $"Bearer {token.AccessToken}");

                    Http http = new Http();

                    HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
                    if (http.LastMethodSuccess != true)
                    {
                        throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
                    }

                    stopwatch.Stop();
                    double duration = stopwatch.Elapsed.TotalSeconds;
                    Console.WriteLine($"Duration of request: {duration}");
                }
                
            });
            stopwatch.Stop();

            double requestsPerSecond = 1*1000 / stopwatch.Elapsed.TotalSeconds;

            Console.WriteLine($"Requests per second: {requestsPerSecond}");



        }
    }
}
