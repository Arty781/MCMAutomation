using Chilkat;
using MCMAutomation.APIHelpers.Client.SignUp;
using MCMAutomation.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MCMAutomation.APIHelpers.Client.Membership.MembershipModel;

namespace MCMAutomation.APIHelpers.Client.Membership
{
    public class ClientMembershipRequest
    {
        private static string JsonBody(MembershipModel.GetActiveMembership activeMembership, int numberOfDays)
        {
            var body = new MembershipModel.GetListByProgramWeekRequest()
            {
                ProgramId = activeMembership.Programs[RandomHelper.RandomNum(activeMembership.Programs.Count)].Id,
                ProgramWeek = 1,
                StartOn = DateTime.Now.AddDays(numberOfDays).Date,
                IsMobile= true,
                UserMembershipId = activeMembership.Id
            };

            return JsonConvert.SerializeObject(body);

        }
        private static string JsonBody(MembershipModel.GetActiveMembership activeMembership, MembershipModel.Program program, int numberOfDays, int weekNumber)
        {
            MembershipModel.GetListByProgramWeekRequest body = new();
            
                body.ProgramId = program.Id;
                body.ProgramWeek = weekNumber;
            if (weekNumber == 1)
            {
                body.StartOn = DateTime.Now.AddDays(numberOfDays).Date;
            }
                body.IsMobile = true;
                body.UserMembershipId = activeMembership.Id;
            

            return JsonConvert.SerializeObject(body);

        }

        private static string JsonBody(MembershipModel.GetActiveMembership activeMembership, List<MembershipModel.GetListByProgramWeekResponse> workoutsListByProgramWeek)
        {
            var body = new MembershipModel.SaveCompletedWorkoutRequest()
            {
                WorkoutId = workoutsListByProgramWeek[0].Id,
                WeekNumber = 1,
                ConversionSystem = 0,
                UserMembershipId = activeMembership.Id
            };
            return JsonConvert.SerializeObject(body);
        }

        private static string JsonBody(MembershipModel.GetActiveMembership activeMembership, MembershipModel.GetListByProgramWeekResponse workoutsListByProgramWeek, int weekNumber)
        {
            var body = new MembershipModel.SaveCompletedWorkoutRequest()
            {
                WorkoutId = workoutsListByProgramWeek.Id,
                WeekNumber = weekNumber,
                ConversionSystem = 0,
                UserMembershipId = activeMembership.Id
            };
            return JsonConvert.SerializeObject(body);
        }

        private static string JsonBody(MembershipModel.GetActiveMembership activeMembership, List<MembershipModel.GetListByProgramWeekResponse> workoutsListByProgramWeek, MembershipModel.GetUserWorkoutExercisesResponse userWorkoutExercises)
        {
            var body = new MembershipModel.SaveCompletedWorkoutRequest()
            {
                WorkoutId = workoutsListByProgramWeek[0].Id,
                WeekNumber = 1,
                UserMembershipId = activeMembership.Id,
                ConversionSystem = 0,
                UserExercises = new List<SaveUserExercise>()
            };

            foreach (var workoutExercise in userWorkoutExercises.WorkoutExercises)
            {
                foreach (var userExercise in workoutExercise.UserExercises)
                {
                    var saveUserExercise = new SaveUserExercise()
                    {
                        JsonUserExerciseId = userExercise.Id,
                        Set = userExercise.Set,
                        IsDone = true,
                        Reps = userExercise.Reps,
                        Weight = RandomHelper.RandomWeight()
                    };

                    body.UserExercises.Add(saveUserExercise);
                }
            }
            return JsonConvert.SerializeObject(body);
        }

        private static string JsonBody(MembershipModel.GetActiveMembership activeMembership, MembershipModel.GetListByProgramWeekResponse workoutsListByProgramWeek, MembershipModel.GetUserWorkoutExercisesResponse userWorkoutExercises, int weekNum)
        {
            var body = new MembershipModel.SaveCompletedWorkoutRequest()
            {
                WorkoutId = workoutsListByProgramWeek.Id,
                WeekNumber = weekNum,
                UserMembershipId = activeMembership.Id,
                ConversionSystem = 0,
                UserExercises = new List<SaveUserExercise>()
            };

            foreach (var workoutExercise in userWorkoutExercises.WorkoutExercises)
            {
                foreach (var userExercise in workoutExercise.UserExercises)
                {
                    var saveUserExercise = new SaveUserExercise()
                    {
                        JsonUserExerciseId = userExercise.Id,
                        Set = userExercise.Set,
                        IsDone = true,
                        Reps = userExercise.Reps,
                        Weight = RandomHelper.RandomWeight()
                    };

                    body.UserExercises.Add(saveUserExercise);
                }
            }
            return JsonConvert.SerializeObject(body);
        }

        public static void GetUserMemberships(SignInResponseModel loginResponse, out List<MembershipModel.GetMembership> response)
        {
            HttpRequest req = new()
            {
                HttpVerb = "GET",
                Path = "/Membership/GetUserMemberships"
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

            response = JsonConvert.DeserializeObject<List<MembershipModel.GetMembership>>(resp.BodyStr);
        }

        public static void GetActiveMembershipForRandomPhase(SignInResponseModel loginResponse, out MembershipModel.GetActiveMembership response)
        {
            HttpRequest req = new()
            {
                HttpVerb = "GET",
                Path = "/Membership/GetActiveMembership"
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

            response = JsonConvert.DeserializeObject<MembershipModel.GetActiveMembership>(resp.BodyStr);
        }

        public static void GetWorkoutsListForFirstWeek(SignInResponseModel loginResponse, MembershipModel.GetActiveMembership activeMembership, int numberOfDays, out List<MembershipModel.GetListByProgramWeekResponse> response)
        {
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/Workout/GetListByProgramWeek",
                ContentType= "application/json"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept", "application /json, text/plain, */*");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {loginResponse.AccessToken}");

            req.LoadBodyFromString(JsonBody(activeMembership, numberOfDays), "UTF-8");

            Http http = new();
            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                throw new ArgumentException("Request " + resp.Domain + req.Path + "\r\n was failed with: " + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }

            response = JsonConvert.DeserializeObject<List<MembershipModel.GetListByProgramWeekResponse>>(resp.BodyStr);
        }

        public static void GetUserWorkoutExercisesForFirstWeek(SignInResponseModel loginResponse, MembershipModel.GetActiveMembership activeMembership, List<MembershipModel.GetListByProgramWeekResponse> workoutsListByProgramWeek, out MembershipModel.GetUserWorkoutExercisesResponse response)
        {
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/Workout/GetUserWorkoutExercises",
                ContentType = "application/json"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept", "application /json, text/plain, */*");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {loginResponse.AccessToken}");

            req.LoadBodyFromString(JsonBody(activeMembership, workoutsListByProgramWeek), "UTF-8");

            Http http = new();
            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                throw new ArgumentException("Request " + resp.Domain + req.Path + "\r\n was failed with: " + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }

            response = JsonConvert.DeserializeObject<MembershipModel.GetUserWorkoutExercisesResponse>(resp.BodyStr);
        }

        public static void SaveCompletedWorkoutForFirstWeek(SignInResponseModel loginResponse, MembershipModel.GetActiveMembership activeMembership, List<MembershipModel.GetListByProgramWeekResponse> workoutsListByProgramWeek, MembershipModel.GetUserWorkoutExercisesResponse userWorkoutExercises)
        {
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/Workout/SaveCompletedWorkout",
                ContentType = "application/json"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept", "application /json, text/plain, */*");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {loginResponse.AccessToken}");

            req.LoadBodyFromString(JsonBody(activeMembership, workoutsListByProgramWeek, userWorkoutExercises), "UTF-8");

            Http http = new();
            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                throw new ArgumentException("Request " + resp.Domain + req.Path + "\r\n was failed with: " + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }

        public static void GetActiveMembershipForAllPhases(SignInResponseModel loginResponse)
        {
            HttpRequest req = new()
            {
                HttpVerb = "GET",
                Path = "/Membership/GetActiveMembership"
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

            var response = JsonConvert.DeserializeObject<MembershipModel.GetActiveMembership>(resp.BodyStr);
            foreach (var phase in response.Programs)
            {
                GetWorkoutsListForAllPhases(loginResponse, response, phase, -45);
            }
            
        }

        private static void GetWorkoutsListForAllPhases(SignInResponseModel loginResponse, MembershipModel.GetActiveMembership activeMembership, MembershipModel.Program program, int numberOfDays)
        {
            for (int i = 1; i <= program.NumberOfWeeks; i++)
            {
                HttpRequest req = new()
                {
                    HttpVerb = "POST",
                    Path = "/Workout/GetListByProgramWeek",
                    ContentType = "application/json"
                };
                req.AddHeader("Connection", "Keep-Alive");
                req.AddHeader("Accept", "application /json, text/plain, */*");
                req.AddHeader("Accept-Encoding", "gzip, deflate, br");
                req.AddHeader("Authorization", $"Bearer {loginResponse.AccessToken}");

                req.LoadBodyFromString(JsonBody(activeMembership, program, numberOfDays, i), "UTF-8");

                Http http = new();
                HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
                if (!resp.StatusCode.ToString().StartsWith("2"))
                {
                    throw new ArgumentException("Request " + resp.Domain + req.Path + "\r\n was failed with: " + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
                }

                var response = JsonConvert.DeserializeObject<List<MembershipModel.GetListByProgramWeekResponse>>(resp.BodyStr);

                foreach (var workout in response)
                {
                    GetUserWorkoutExercisesForAllPhases(loginResponse, activeMembership, workout, i);
                }
            }


        }

        private static void GetUserWorkoutExercisesForAllPhases(SignInResponseModel loginResponse, MembershipModel.GetActiveMembership activeMembership, MembershipModel.GetListByProgramWeekResponse workout, int weekNum)
        {
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/Workout/GetUserWorkoutExercises",
                ContentType = "application/json"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept", "application /json, text/plain, */*");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {loginResponse.AccessToken}");

            req.LoadBodyFromString(JsonBody(activeMembership, workout, weekNum), "UTF-8");

            Http http = new();
            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                throw new ArgumentException("Request " + resp.Domain + req.Path + "\r\n was failed with: " + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
            var response = JsonConvert.DeserializeObject<MembershipModel.GetUserWorkoutExercisesResponse>(resp.BodyStr);

            SaveCompletedWorkoutForAllPhases(loginResponse, activeMembership, workout, response, weekNum);
            
        }

        private static void SaveCompletedWorkoutForAllPhases(SignInResponseModel loginResponse, MembershipModel.GetActiveMembership activeMembership, MembershipModel.GetListByProgramWeekResponse workoutListByProgramWeek, MembershipModel.GetUserWorkoutExercisesResponse userWorkoutExercises, int weekNum)
        {
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/Workout/SaveCompletedWorkout",
                ContentType = "application/json"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept", "application /json, text/plain, */*");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {loginResponse.AccessToken}");

            req.LoadBodyFromString(JsonBody(activeMembership, workoutListByProgramWeek, userWorkoutExercises, weekNum), "UTF-8");

            Http http = new();
            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                throw new ArgumentException("Request " + resp.Domain + req.Path + "\r\n was failed with: " + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }
    }
}
