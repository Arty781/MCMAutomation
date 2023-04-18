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

namespace MCMAutomation.APIHelpers
{
    public class MembershipRequest
    {
        #region Json Bodies

        private static string JsonBody(int count, int programId)
        {
            WorkoutsModel req = null;
            if (count == 0)
            {
                req = new()
                {
                    ProgramId = programId,
                    Name = "Workout " + $"{DateTime.Now:yyyy-MMM-ddd HH-mm-ss -ff}",
                    WeekDay = 1,
                };
            }
            else if (count == 1)
            {
                req = new()
                {
                    ProgramId = programId,
                    Name = "Workout " + $"{DateTime.Now:yyyy-MMM-ddd HH-mm-ss -ff}",
                    WeekDay = 2,
                };
            }
            else if (count == 2)
            {
                req = new()
                {
                    ProgramId = programId,
                    Name = "Workout " + $"{DateTime.Now:yyyy-MMM-ddd HH-mm-ss -ff}",
                    WeekDay = 3,
                };
            }
            else if (count == 3)
            {
                req = new()
                {
                    ProgramId = programId,
                    Name = "Workout " + $"{DateTime.Now:yyyy-MMM-ddd HH-mm-ss -ff}",
                    WeekDay = 4,
                };
            }
            else if (count == 4)
            {
                req = new()
                {
                    ProgramId = programId,
                    Name = "Workout " + $"{DateTime.Now:yyyy-MMM-ddd HH-mm-ss -ff}",
                    WeekDay = 5,
                };
            }
            else if (count == 5)
            {
                req = new()
                {
                    ProgramId = programId,
                    Name = "Workout " + $"{DateTime.Now:yyyy-MMM-ddd HH-mm-ss -ff}",
                    WeekDay = 6,
                };
            }
            else if (count == 6)
            {
                req = new()
                {
                    ProgramId = programId,
                    Name = "Workout " + $"{DateTime.Now:yyyy-MMM-ddd HH-mm-ss -ff}",
                    WeekDay = 7,
                };
            }


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
            WorkoutExercises req = null;
            switch (count)
            {
                case 0:
                    req = new()
                    {
                        ExerciseId = exercises[RandomHelper.RandomExercise(exercises.Count)].Id,
                        Notes = Lorem.Sentence(),
                        Series = "A",
                        Type = "RD : ADD__RANGE_WORKOUT_EXERCISE",
                        WorkoutId = workoutId,
                        WeekWorkoutExercises = new List<WeekWorkoutExercise>()
                    {
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "10-15",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "2010",
                            Week = 1
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "10-15",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "2010",
                            Week = 2
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "10-15",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "2010",
                            Week = 3
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "10-15",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "2010",
                            Week = 4
                        }
                    }
                    };
                    break;
                case 1:
                    req = new()
                    {
                        ExerciseId = exercises[RandomHelper.RandomExercise(exercises.Count)].Id,
                        Notes = Lorem.Sentence(),
                        Series = "B",
                        Type = "RD : ADD__RANGE_WORKOUT_EXERCISE",
                        WorkoutId = workoutId,
                        WeekWorkoutExercises = new List<WeekWorkoutExercise>()
                    {
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 1
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 2
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 3
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 4
                        }
                    }
                    };
                    break;
                case 2:
                    req = new()
                    {
                        ExerciseId = exercises[RandomHelper.RandomExercise(exercises.Count)].Id,
                        Notes = Lorem.Sentence(),
                        Series = "C1",
                        Type = "RD : ADD__RANGE_WORKOUT_EXERCISE",
                        WorkoutId = workoutId,
                        WeekWorkoutExercises = new List<WeekWorkoutExercise>()
                    {
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 1
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                           Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 2
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 3
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 4
                        }
                    }
                    };
                    break;
                case 3:
                    req = new()
                    {
                        ExerciseId = exercises[RandomHelper.RandomExercise(exercises.Count)].Id,
                        Notes = Lorem.Sentence(),
                        Series = "C2",
                        Type = "RD : ADD__RANGE_WORKOUT_EXERCISE",
                        WorkoutId = workoutId,
                        WeekWorkoutExercises = new List<WeekWorkoutExercise>()
                    {
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 1
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 2
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 3
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 4
                        }
                    }
                    };
                    break;
                case 4:
                    req = new()
                    {
                        ExerciseId = exercises[RandomHelper.RandomExercise(exercises.Count)].Id,
                        Notes = Lorem.Sentence(),
                        Series = "D",
                        Type = "RD : ADD__RANGE_WORKOUT_EXERCISE",
                        WorkoutId = workoutId,
                        WeekWorkoutExercises = new List<WeekWorkoutExercise>()
                    {
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3011",
                            Week = 1
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3011",
                            Week = 2
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3011",
                            Week = 3
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-7",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3011",
                            Week = 4
                        }
                    }
                    };
                    break;
                case 5:
                    req = new()
                    {
                        ExerciseId = exercises[RandomHelper.RandomExercise(exercises.Count)].Id,
                        Notes = Lorem.Sentence(),
                        Series = "E",
                        Type = "RD : ADD__RANGE_WORKOUT_EXERCISE",
                        WorkoutId = workoutId,
                        WeekWorkoutExercises = new List<WeekWorkoutExercise>()
                    {
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-6",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 1
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-6",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 2
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-6",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 3
                        },
                        new WeekWorkoutExercise()
                        {
                            Sets = RandomHelper.RandomNumFromOne(10).ToString(),
                            Reps = "5-6",
                            Rest = RandomHelper.RandomNumFromOne(90).ToString(),
                            Tempo = "3010",
                            Week = 4
                        }
                    }
                    };
                    break;
            }
            
            return JsonConvert.SerializeObject(req);
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
                Console.WriteLine(http.LastErrorText);
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
                Console.WriteLine(http.LastErrorText);
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

        public static void CreateProductMembership(SignInResponseModel SignIn)
        {
            HttpRequest req = new HttpRequest
            {
                HttpVerb = "POST",
                Path = "/Admin/AddMembership",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("Accept-Encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("sku", MembershipsSKU.MEMBERSHIP_SKU[1]);
            req.AddParam("name", "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            req.AddParam("description", Lorem.ParagraphByChars(792));
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

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                Console.WriteLine(http.LastErrorText);
                return;
            }
            Console.WriteLine("Error message is " + Convert.ToString(resp.BodyStr));
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

            req.AddParam("sku", MembershipsSKU.MEMBERSHIP_SKU[2]);
            req.AddParam("name", "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            req.AddParam("description", Lorem.ParagraphByChars(792));
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
                Console.WriteLine(http.LastErrorText);
                return;
            }
            Console.WriteLine("Error message is " + Convert.ToString(resp.BodyStr));
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

            req.AddParam("sku", MembershipsSKU.MEMBERSHIP_SKU[1]);
            req.AddParam("name", "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            req.AddParam("description", Lorem.ParagraphByChars(792));
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
                Path = "/Admin/AddCustomMembership",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("name", "00Created New Custom Membership " + DateTime.Now.ToString("yyyy-MMM-ddd HH-mm-ss -ff"));
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
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/Admin/AddProgram",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("authorization", $"Bearer {SignIn.AccessToken}");

            req.AddParam("programName", "Phase " + $"{DateTime.Now:yyyy-MMM-ddd HH-mm-ss -ff}");
            req.AddParam("numberOfWeeks", "4");
            req.AddParam("steps", "Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
            req.AddParam("availableDate", String.Empty);
            req.AddParam("expirationDate", String.Empty);
            req.AddParam("membershipId", $"{MembershipId}");
            req.AddParam("image", "undefined");

            Http http = new();

            HttpResponse resp = http.SynchronousRequest("mcmstaging-api.azurewebsites.net", 443, true, req);
            if (http.LastMethodSuccess != true)
            {
                Debug.WriteLine(http.LastErrorText);
                return;
            }
            Debug.WriteLine(resp.BodyStr);
        }

        public static void CreateWorkouts(SignInResponseModel SignIn, int programId, int programCount)
        {
            Http http = new()
            {
                Accept = "application/json",
                AuthToken = "Bearer " + SignIn.AccessToken
            };

            string url = String.Concat(Endpoints.API_HOST + "/Admin/AddWorkout");
            for(int i = 0; i < programCount; i++)
            {
                HttpResponse resp = http.PostJson2(url, "application/json", JsonBody(i, programId));
                if (http.LastMethodSuccess == false)
                {
                    Debug.WriteLine(http.LastErrorText);
                }
                Debug.WriteLine(resp.BodyStr);
            }
            
        }
        
        public static void AddExercisesToMembership(SignInResponseModel SignIn, DB.Workouts workouts, List<DB.Exercises> exercises)
        {
            Http http = new()
            {
                Accept = "application/json",
                AuthToken = "Bearer " + SignIn.AccessToken
            };

            string url = String.Concat(Endpoints.API_HOST + "/Admin/AddRangeWorkoutExercises");
            for (int i = 0; i < 5; i++)
            {
                HttpResponse resp = http.PostJson2(url, "application/json", JsonBody(i, workouts.Id, exercises));
                if (http.LastStatus != 200 )
                {
                    Debug.WriteLine(http.LastErrorText);
                }
                Debug.WriteLine(resp.BodyStr);
            }
        }

        public static void AddUsersToMembership(SignInResponseModel SignIn, int MembershipId, string UserId)
        {
            Http http = new Http();

            http.Accept = "application/json";
            http.AuthToken = "Bearer " + SignIn.AccessToken;

            string url = String.Concat(Endpoints.API_HOST + "/Admin/AddUsersToMembership");
            string jsonRequestBody = "{" +
                                        $"\r\n\"membershipId\": {MembershipId}" + "," +
                                        "\r\n \"type\": \"RD : ADD_MEMBERSHIP_TO_USER\"" + "," +
                                        $"\r\n \"userId\": \"{UserId}\"" + "}";
            HttpResponse resp = http.PostJson2(url, "application/json", jsonRequestBody);
            if (http.LastMethodSuccess == false)
            {
                Debug.WriteLine(http.LastErrorText);
            }
            Debug.WriteLine(resp.BodyStr);
        }

        public static void ActivateUserMembership(SignInResponseModel SignIn, int userMembershipId, string userId)
        {
            HttpRequest req = new HttpRequest
            {
                HttpVerb = "GET",
                Path = $"/Admin/SelectActiveMembership/{userId}/{userMembershipId}"
            };
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
            Debug.WriteLine("Error message is " + Convert.ToString(resp.BodyStr));
        }

        #endregion

        #region Methods

        public static List<DB.Programs> CreatePrograms(SignInResponseModel responseLoginAdmin, DB.Memberships membership, int programCount)
        {
            for (int i = 0; i < programCount; i++)
            {
                CreatePrograms(responseLoginAdmin, membership.Id);
            }
            var programs = AppDbContext.Programs.GetLastPrograms(programCount);
            return programs;
        }

        public static List<DB.Workouts> CreateWorkouts(SignInResponseModel responseLoginAdmin, List<DB.Programs> programs, int programCount)
        {
            foreach (var program in programs)
            {
                CreateWorkouts(responseLoginAdmin, program.Id, programCount);
            }
            var workouts = AppDbContext.Workouts.GetLastWorkoutsData(programCount);
            return workouts;
        }

        public static void AddExercisesToWorkouts(SignInResponseModel responseLoginAdmin, List<DB.Workouts> workouts)
        {
            var exercises = AppDbContext.Exercises.GetExercisesData();
            foreach (var workout in workouts)
            {
                AddExercisesToMembership(responseLoginAdmin, workout, exercises);
            }
        }

        #endregion
    }
}
