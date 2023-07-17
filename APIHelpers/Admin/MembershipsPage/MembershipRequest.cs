using MCMAutomation.Helpers;
using Newtonsoft.Json;
using Chilkat;
using System.Diagnostics;
using System;
using RimuTec.Faker;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Linq;
using static MCMAutomation.Helpers.AppDbContext;
using static MCMAutomation.APIHelpers.Client.Membership.MembershipModel;

namespace MCMAutomation.APIHelpers
{
    public class MembershipRequest
    {
        #region Json Bodies

        private static string JsonBody(int count, int programId)
        {
            WorkoutsModel req = new()
            {
                ProgramId = programId,
                Name = "Workout " + $"{DateTime.Now:yyyy-MMM-ddd HH-mm-ss -ff}"
            };

            req.WeekDay = count switch
            {
                0 => 1,
                1 => 2,
                2 => 3,
                3 => 4,
                4 => 5,
                5 => 6,
                6 => (long)7,
                _ => throw new Exception("Invalid count value"),
            };
            return JsonConvert.SerializeObject(req);
        }

        private static string JsonBody(int programId)
        {
            WorkoutsModel req = new WorkoutsModel()
            {
                ProgramId = programId,
                Name = "Phase" + $"{DateTime.Now:yyyy-MMM-ddd HH-mm-ss -ff}",
                WeekDay = RandomHelper.RandomNumFromOne(7),
            };

            return JsonConvert.SerializeObject(req);
        }

        private static string JsonBody(int count, int workoutId, List<DB.Exercises> exercises)
        {
            
            var exercisesWithTempoZero = exercises.Where(ex=>ex.TempoBold == 0).Select(ex=>ex).ToList();
            var exercisesWithTempoOne = exercises.Where(ex => ex.TempoBold == 1).Select(ex => ex).ToList();
            var exercisesWithTempoThree = exercises.Where(ex => ex.TempoBold == 3).Select(ex => ex).ToList();
            WorkoutExercises? req = new()
            {
                Type = "RD : ADD__RANGE_WORKOUT_EXERCISE",
                WorkoutId = workoutId,
                Notes = Lorem.Sentence()
            };
            switch (count)
            {
                case 0:
                    req.ExerciseId = exercisesWithTempoZero[RandomHelper.RandomExercise(exercisesWithTempoZero.Count)].Id;
                    req.Series = "A";
                    req.WeekWorkoutExercises = Enumerable.Range(1, 4)
                        .Select(week => new WeekWorkoutExercise
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = week == 2 ? "3x10-3x15" : "10-15",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = week switch
                            {
                                1 => "2010",
                                2 => "2010+3010",
                                3 => "3 x 2310 + 3 x 2010",
                                4 => "3x2310+3x2010",
                                _ => throw new InvalidOperationException("Invalid week value")
                            },
                            Week = week
                        })
                        .ToList();
                    break;

                case 1:
                    req.ExerciseId = exercisesWithTempoOne[RandomHelper.RandomExercise(exercisesWithTempoOne.Count)].Id;
                    req.Series = "B";
                    req.WeekWorkoutExercises = Enumerable.Range(1, 4)
                        .Select(week => new WeekWorkoutExercise
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = week switch
                            {
                                1 => "2013 + 2010",
                                2 => "2010+5020",
                                3 => "30X0",
                                4 => "X010",
                                _ => throw new InvalidOperationException("Invalid week value")
                            },
                            Week = week
                        })
                        .ToList();
                    break;

                case 2:
                    req.ExerciseId = exercisesWithTempoThree[RandomHelper.RandomExercise(exercisesWithTempoThree.Count)].Id;
                    req.Series = "C1";
                    req.WeekWorkoutExercises = Enumerable.Range(1, 4)
                        .Select(week => new WeekWorkoutExercise
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = week == 2 ? "3 x 2310 + 3 x 2010" : "3010",
                            Week = week
                        })
                        .ToList();
                    break;

                case 3:
                    req.ExerciseId = exercisesWithTempoZero[RandomHelper.RandomExercise(exercisesWithTempoZero.Count)].Id;
                    req.Series = "C2";
                    req.WeekWorkoutExercises = Enumerable.Range(1, 4)
                        .Select(week => new WeekWorkoutExercise
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = week == 3 ? "3 x 2310 + 3 x 2010" : "3010",
                            Week = week
                        })
                        .ToList();
                    break;

                case 4:
                    req.ExerciseId = exercisesWithTempoThree[RandomHelper.RandomExercise(exercisesWithTempoThree.Count)].Id;
                    req.Series = "D";
                    req.WeekWorkoutExercises = Enumerable.Range(1, 4)
                        .Select(week => new WeekWorkoutExercise
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3011",
                            Week = week
                        })
                        .ToList();
                    break;

                case 5:
                    req.ExerciseId = exercisesWithTempoOne[RandomHelper.RandomExercise(exercisesWithTempoOne.Count)].Id;
                    req.Series = "E";
                    req.WeekWorkoutExercises = Enumerable.Range(1, 4)
                        .Select(week => new WeekWorkoutExercise
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-6",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = week
                        })
                        .ToList();
                    break;

                default:
                    throw new InvalidOperationException("Invalid count value");
            }

            return JsonConvert.SerializeObject(req);
        }

        private static string JsonSubAllBody(List<DB.Memberships>? memberships, int numberOfMemberships)
        {
            List<SubAllMembershipsReq> body = new();
            for (int i=1; i<numberOfMemberships; i++)
            {
                SubAllMembershipsReq req = new()
                {
                    SubAllMembershipId = memberships[RandomHelper.RandomNumFromOne(memberships.Count)].Id,
                    Description = Lorem.ParagraphByChars(50)
                };
                body.Add(req);
            }

            return JsonConvert.SerializeObject(body);
        }


        #endregion

        #region Requests

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
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
            var countdownResponse = JsonConvert.DeserializeObject<MembershipsWithUsersResponse>(resp.BodyStr);
            return countdownResponse;
        }

        public static List<MembershipSummaryResponse> GetMembershipsSummary(SignInResponseModel SignIn)
        {
            HttpRequest req = new()
            {
                HttpVerb = "GET",
                Path = "/Admin/GetMembershipSummary"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
            var countdownResponse = JsonConvert.DeserializeObject<List<MembershipSummaryResponse>>(resp.BodyStr);
            List<MembershipSummaryResponse> list= new List < MembershipSummaryResponse >();
            foreach (MembershipSummaryResponse response in countdownResponse)
            {
                if(response.StartDate == null && response.EndDate ==null && response.Type == 0)
                {
                    list.Add(response);
                }
            }
            return list;
        }

        public static void CreateProductMembership(SignInResponseModel SignIn, string sku)
        {
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/Admin/AddMembership",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept","application /json, text/plain, */*");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("sku", sku);
            req.AddParam("name", "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d-hh-mm"));
            req.AddParam("description", Lorem.ParagraphByChars(400));
            req.AddParam("startDate", "");
            req.AddParam("endDate", "");
            req.AddParam("price", "100");
            req.AddParam("accessWeekLength", "12");
            req.AddParam("url", "https://mcmstaging-ui.azurewebsites.net/programs/all");
            req.AddParam("type", "0");
            req.AddParam("image", "undefined");
            req.AddParam("gender", "0");
            req.AddParam("relatedMembershipIds", "");
            req.AddParam("forPurchase", "true");
            //req.AddParam("membershipLevels", "[]");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }

        public static void CreateSubAllMembership(SignInResponseModel SignIn, string sku, List<DB.Memberships>? memberships, int numberOfMemberships)
        {
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/Admin/AddMembership",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept", "application /json, text/plain, */*");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("sku", sku);
            req.AddParam("name", "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d-hh-mm") + " of SubAll");
            req.AddParam("description", Lorem.ParagraphByChars(400));
            req.AddParam("startDate", "");
            req.AddParam("endDate", "");
            req.AddParam("price", "100");
            req.AddParam("accessWeekLength", "");
            req.AddParam("url", "https://mcmstaging-ui.azurewebsites.net/programs/all");
            req.AddParam("type", "2");
            req.AddParam("image", "undefined");
            req.AddParam("gender", "0");
            req.AddParam("relatedMembershipIds", "");
            req.AddParam("forPurchase", "true");
            req.AddParam("SubAllMemberships", JsonSubAllBody(memberships, numberOfMemberships));

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path + "\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }

        public static void EditSubAllMembership(SignInResponseModel SignIn, string sku, int id, List<DB.SubAllMembershipModel> subAllmemberships)
        {
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/Admin/EditMembership",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept", "application /json, text/plain, */*");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("id", $"{id}");
            req.AddParam("sku", sku);
            req.AddParam("name", "00Edited New Membership " + DateTime.Now.ToString("yyyy-MM-d-hh-mm") + " of SubAll");
            req.AddParam("description", Lorem.ParagraphByChars(400));
            req.AddParam("startDate", "");
            req.AddParam("endDate", "");
            req.AddParam("price", "100");
            req.AddParam("accessWeekLength", "");
            req.AddParam("url", "https://mcmstaging-ui.azurewebsites.net/programs/all");
            req.AddParam("type", "2");
            req.AddParam("image", "undefined");
            req.AddParam("gender", "0");
            req.AddParam("relatedMembershipIds", "");
            req.AddParam("forPurchase", "true");
            req.AddParam("SubAllMemberships", "[" +
                $"{{\r\n\"id\": {subAllmemberships[0].Id}, \r\n\"subAllMembershipId\": 53,\r\n\"description\": \"{Lorem.ParagraphByChars(50)}\"\r\n}}," +
                $"\r\n{{\r\n\"id\": {subAllmemberships[1].Id}, \r\n\"subAllMembershipId\": 54,\r\n\"description\": \"{Lorem.ParagraphByChars(50)}\"\r\n}}," +
                $"\r\n{{\r\n\"id\": {subAllmemberships[2].Id}, \r\n\"subAllMembershipId\": 55,\r\n\"description\": \"{Lorem.ParagraphByChars(50)}\"\r\n}}," +
                $"\r\n{{\r\n\"subAllMembershipId\": 241,\r\n\"description\": \"{Lorem.ParagraphByChars(50)}\"\r\n}}," +
                $"\r\n{{\r\n\"subAllMembershipId\": 242,\r\n\"description\": \"{Lorem.ParagraphByChars(50)}\"\r\n}}," +
                $"\r\n{{\r\n\"subAllMembershipId\": 243,\r\n\"description\": \"{Lorem.ParagraphByChars(50)}\"\r\n}}" +
                "]");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path + "\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }

        public static void CreateSubscriptionMembership(SignInResponseModel SignIn)
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

            req.AddParam("sku", MembershipsSKU.SKU_SUBSCRIPTION);
            req.AddParam("name", "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            req.AddParam("description", Lorem.ParagraphByChars(450));
            req.AddParam("startDate", "");
            req.AddParam("endDate", "");
            req.AddParam("price", "100");
            req.AddParam("accessWeekLength", "");
            req.AddParam("url", "https://mcmstaging-ui.azurewebsites.net/programs/all");
            req.AddParam("type", "1");
            req.AddParam("image", "undefined");
            req.AddParam("gender", "0");
            req.AddParam("relatedMembershipIds", "");
            req.AddParam("forPurchase", "true");
            req.AddParam("membershipLevels", "[]");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }

        public static void CreateMultilevelMembership(SignInResponseModel SignIn)
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

            req.AddParam("sku", MembershipsSKU.SKU_PRODUCT);
            req.AddParam("name", "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            req.AddParam("description", Lorem.ParagraphByChars(450));
            req.AddParam("startDate", "");
            req.AddParam("endDate", "");
            req.AddParam("price", "0");
            req.AddParam("accessWeekLength", "");
            req.AddParam("url", "https://mcmstaging-ui.azurewebsites.net/programs/all");
            req.AddParam("type", "1");
            req.AddParam("image", "undefined");
            req.AddParam("gender", "0");
            req.AddParam("relatedMembershipIds", "");
            req.AddParam("forPurchase", "true");
            req.AddParam("membershipLevels", "[]");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }

        public static void CreateCustomMembership(SignInResponseModel SignIn, string UserId)
        {
            HttpRequest req = new HttpRequest
            {
                HttpVerb = "POST",
                Path = "/Admin/AddCustomMembership",
                ContentType = "multipart/form-data"                
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("Accept", "application/json, text/plain, */*");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("name", "00Created New Custom Membership " + DateTime.Now.ToString("yyyy-MMM-ddd HH-mm-ss -ff"));
            req.AddParam("description", Lorem.ParagraphByChars(450));
            req.AddParam("startDate", "");
            req.AddParam("endDate", "");
            req.AddParam("accessWeekLength", "16");
            req.AddParam("image", "undefined");
            req.AddParam("userId", $"{UserId}");

            Http http = new();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }

        public static void CreatePrograms(SignInResponseModel SignIn, int MembershipId, int countPrograms, out List<DB.Programs> programs)
        {
            for (int i = 0; i < countPrograms; i++)
            {
                HttpRequest req = new()
                {
                    HttpVerb = "POST",
                    Path = "/Admin/AddProgram",
                    ContentType = "multipart/form-data"
                };
                req.AddHeader("Connection", "Keep-Alive");
                req.AddHeader("accept-encoding", "gzip, deflate, br");
                req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

                req.AddParam("programName", "Phase " + $"{DateTime.Now:yyyy-MMM-ddd HH-mm-ss -fffff}");
                req.AddParam("numberOfWeeks", "4");
                req.AddParam("steps", "Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
                req.AddParam("availableDate", String.Empty);
                req.AddParam("expirationDate", String.Empty);
                req.AddParam("membershipId", $"{MembershipId}");
                req.AddParam("image", "undefined");


                Http http = new();
                HttpResponse resp = http.SynchronousRequest(Endpoints.API_HOST_GET, 443, true, req);
                if (http.LastMethodSuccess != true)
                {
                    throw new ArgumentException(resp.Domain + req.Path +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
                }
            }
            programs = AppDbContext.Programs.GetLastPrograms(countPrograms);

        }

        public static void CreateWorkouts(SignInResponseModel SignIn, List<DB.Programs> programs, int programCount, out List<DB.Workouts> workouts)
        {
            Http http = new()
            {
                Accept = "application/json",
                AuthToken = "Bearer " + SignIn.AccessToken
            };
            foreach (var program in programs)
            {
                for (int i = 0; i < programCount; i++)
                {
                    HttpResponse resp = http.PostJson2(String.Concat(Endpoints.API_HOST + "/Admin/AddWorkout"), "application/json", JsonBody(i, program.Id));
                    if (!resp.StatusCode.ToString().StartsWith("2"))
                    {
                        Debug.WriteLine(http.LastErrorText);
                    }
                    Debug.WriteLine(resp.BodyStr);
                }
            }
            
            workouts = AppDbContext.Workouts.GetLastWorkoutsData(programs);
        }
        
        public static void AddExercisesToMembership(SignInResponseModel SignIn, List<DB.Workouts> workouts, List<DB.Exercises> exercises)
        {
            Http http = new()
            {
                Accept = "application/json",
                AuthToken = "Bearer " + SignIn.AccessToken
            };

            string url = String.Concat(Endpoints.API_HOST + "/Admin/AddRangeWorkoutExercises");
            foreach (var workout in workouts)
            {
                for (int i = 0; i < 3; i++)
                {
                    HttpResponse resp = http.PostJson2(url, "application/json", JsonBody(i, workout.Id, exercises));
                    if (http.LastStatus != 200)
                    {
                        Debug.WriteLine(http.LastErrorText);
                    }
                    Debug.WriteLine(resp.BodyStr);
                }
            }
            
        }

        public static void AddUsersToMembership(SignInResponseModel SignIn, int MembershipId, string UserId)
        {
            string url = String.Concat(Endpoints.API_HOST + "/Admin/AddUsersToMembership");
            string jsonRequestBody = "{" +
                                        $"\r\n\"membershipId\": {MembershipId}" + "," +
                                        "\r\n\"type\": \"RD : ADD_MEMBERSHIP_TO_USER\"" + "," +
                                        $"\r\n\"userId\": \"{UserId}\"" + "}";
            Http http = new()
            {
                Accept = "application/json",
                AuthToken = "Bearer " + SignIn.AccessToken
            };

            HttpResponse resp = http.PostJson2(url, "application/json", jsonRequestBody);
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                throw new ArgumentException(url +"\r\n" + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }

        public static void ActivateUserMembership(SignInResponseModel SignIn, int? userMembershipId, string userId)
        {
            HttpRequest req = new HttpRequest
            {
                HttpVerb = "GET",
                Path = $"/Admin/SelectActiveMembership/{userId}/{userMembershipId}"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");
            req.AddHeader("accept", "application/json");

            Http http = new Http();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (!resp.StatusCode.ToString().StartsWith("2"))
            {
                throw new ArgumentException("Request " + resp.Domain + req.Path + "\r\n was failed with: " + resp.StatusCode.ToString() + "\r\n" + resp.StatusText);
            }
        }

        #endregion

        #region Methods

        //public static List<DB.Programs> CreatePrograms(SignInResponseModel responseLoginAdmin, DB.Memberships membership, int programCount)
        //{
        //    CreatePrograms(responseLoginAdmin, membership.Id, programCount, out List<DB.Programs> programs);
        //    return programs;
        //}

        //public static List<DB.Workouts> CreateWorkouts(SignInResponseModel responseLoginAdmin, List<DB.Programs> programs, int programCount)
        //{
        //    CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
        //    return workouts;
        //}

        public static void AddExercisesToWorkouts(SignInResponseModel responseLoginAdmin, List<DB.Workouts> workouts)
        {
            var exercises = AppDbContext.Exercises.GetExercisesData();
            AddExercisesToMembership(responseLoginAdmin, workouts, exercises);
        }

        public static void CreateProductMembership()
        {
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membership);
            var exercises = AppDbContext.Exercises.GetExercisesData();
            int programCount = 1;
            MembershipRequest.CreatePrograms(responseLoginAdmin, membership.Id, programCount, out List<DB.Programs> programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
            MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workouts, exercises);
        }

        #endregion


    }
}
