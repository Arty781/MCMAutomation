using Chilkat;
using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using static MCMAutomation.APIHelpers.NewAppAPI.Admin.Membership.MembershipModelR;

namespace MCMAutomation.APIHelpers.NewAppAPI.Admin.Membership
{
    public class MembershipsRequest
    {
        private static T GetValueOrDefault<T>(CsvDataReader reader, int index, T defaultValue = default)
        {
            if (!reader.IsDBNull(index))
            {
                return (T)reader.GetValue(index);
            }
            else
            {
                return defaultValue;
            }
        }

        public static string csvFilePath = @"D:\RecipesResponse.csv";
        public static async System.Threading.Tasks.Task<AddMembership> CreateMembership(string token, MembershipModel membership)
        {
            string url = $"mcm-gateway-dev.azurewebsites.net/";
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = $"/workout/admin/membership/add",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {token}");



            Http http = new();
            var resp = http.SynchronousRequest(url, 443, true, CreateMultiPartFormBody(req, membership));
            var respons = http.LastMethodSuccess
                ? JsonConvert.DeserializeObject <AddMembership>(resp.BodyStr) ?? throw new Exception("Response body is null.")
                : throw new ArgumentException(http.LastErrorText);

            return respons;
        }

        public static async System.Threading.Tasks.Task<AddMembership> CreateProgram(string token, ProgramModel program)
        {
            string url = $"mcm-recipe-dev.azurewebsites.net";
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = $"/workout/admin/program/add",
                ContentType = "multipart/form-data"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {token}");



            Http http = new();
            var resp = http.SynchronousRequest(url, 443, true, CreateMultiPartFormBody(req, program));
            var respons = http.LastMethodSuccess
                ? JsonConvert.DeserializeObject<AddMembership>(resp.BodyStr) ?? throw new Exception("Response body is null.")
                : throw new ArgumentException(http.LastErrorText);

            return respons;
        }

        public static async System.Threading.Tasks.Task<AddMembership> CreateWorkout(string token, WorkoutModel program)
        {
            string url = $"mcm-recipe-dev.azurewebsites.net";
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = "/workout/admin/workout/add",
                ContentType = "application/json"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {token}");

            req.LoadBodyFromString(MembershipModelR.JsonBody(program.ProgramId, program.Name, program.WeekDay), "UTF-8");

            Http http = new();
            HttpResponse resp = http.SynchronousRequest(url, 443, true, req);
            var respons = http.LastMethodSuccess
                ? JsonConvert.DeserializeObject<AddMembership>(resp.BodyStr) ?? throw new Exception("Response body is null.")
                : throw new ArgumentException(http.LastErrorText);

            return respons;
        }

        public static async System.Threading.Tasks.Task<AddMembership> CreateWorkoutExercise(string token, WorkoutExerciseModelCsv program)
        {
            string url = $"mcm-recipe-dev.azurewebsites.net";
            HttpRequest req = new()
            {
                HttpVerb = "POST",
                Path = $"/workout/admin/workout-exercise/create-range",
                ContentType = "application/json"
            };
            req.AddHeader("Connection", "Keep-Alive");
            req.AddHeader("accept-encoding", "gzip, deflate, br");
            req.AddHeader("Authorization", $"Bearer {token}");

            req.LoadBodyFromString(MembershipModelR.JsonBody(program), "UTF-8");

            Http http = new();
            HttpResponse resp = http.SynchronousRequest(url, 443, true, req);
            var respons = http.LastMethodSuccess
                ? JsonConvert.DeserializeObject<AddMembership>(resp.BodyStr) ?? throw new Exception("Response body is null.")
                : throw new ArgumentException(http.LastErrorText);

            return respons;
        }

        private static HttpRequest CreateMultiPartFormBody(HttpRequest req, MembershipModel membership)
        {
            
            req.AddParam("Name", membership.Name);
            req.AddParam("Sku", membership.SKU);
            req.AddParam("Description", membership.Description);
            req.AddParam("Url", membership.URL);
            req.AddParam("Type", membership.Type.ToString());
            req.AddParam("AccessWeekLength", membership.AccessWeekLength.ToString());
            req.AddParam("Price", membership.Price.ToString());
            req.AddParam("ForPurchase", membership.ForPurchase.ToString());
            req.AddParam("Gender", membership.Gender.ToString());
            //req.AddParam("RelatedMembershipIds", membership.RelatedMembershipIds.ToString());
            //req.AddParam("SubAllMemberships", membership.SubAllMemberships.ToString());
            

            return req;
        }

        private static HttpRequest CreateMultiPartFormBody(HttpRequest req, ProgramModel program)
        {

            req.AddParam("Name", program.MembershipId);
            req.AddParam("Sku", program.Name);
            req.AddParam("Description", program.NumberOfWeeks);
            req.AddParam("Url", program.Steps);
            req.AddParam("Type", program.Type.ToString());


            return req;
        }

        private static HttpRequest CreateMultiPartFormBody(HttpRequest req, WorkoutModel workout)
        {

            req.AddParam("Name", workout.Name);
            req.AddParam("Sku", workout.WeekDay);
            req.AddParam("Description", workout.ProgramId);


            return req;
        }

        public partial class AddMembership
        {
            [JsonProperty("content")]
            public Guid Content { get; set; }

            [JsonProperty("isSuccess")]
            public bool IsSuccess { get; set; }

            [JsonProperty("responseCode")]
            public long ResponseCode { get; set; }

            [JsonProperty("errorMessage")]
            public object ErrorMessage { get; set; }
        }

        public class MembershipForPrograms
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }
        public class MembershipModel
        {
            public string? Name { get; set; }
            public string? SKU { get; set; }
            public string? Description { get; set; }
            public string? StartDate { get; set; }
            public string? EndDate { get; set; }
            public string? URL { get; set; }
            public string? Price { get; set; }
            public string? IsCustom { get; set;}
            public string? ForPurchase { get; set; }
            public string? AccessWeekLength { get; set; }
            public string? Gender { get; set; }
            public string? Type { get; set; }
            public string? RelatedMembershipIds { get; set; }
            public string? SubAllMemberships { get; set; }

        }

        public class ProgramModel
        {
            public string? MembershipId { get; set; }
            public string? Name { get; set; }
            public string? NumberOfWeeks { get; set; }
            public string? Steps { get; set; }
            public string? Type { get; set; }
            public string? ProgramId { get; set; }

        }

        public class WorkoutModel
        {
            public string? Name { get; set; }
            public string? WeekDay { get; set; }
            public string? ProgramId { get; set; }
            public string? WorkoutId { get; set; }

        }

        public class WorkoutExerciseModelCsv
        {
            public string? WorkoutId { get; set; }
            public string? ExeerciseId { get; set; }
            public string? Sets { get; set; }
            public string? Reps { get; set; }
            public string? Tempo { get; set; }
            public string? Rest { get; set; }
            public string? Series { get; set; }
            public string? Notes { get; set; }
            public string? WeeksNumber { get; set; }

        }

        public class WorkoutExerciseModelRequest
        {
            public string? WorkoutId { get; set; }
            public string? ExeerciseId { get; set; }
            public string? Series { get; set; }
            public string? Notes { get; set; }
            public WorkoutExercisesForWeekRequest WorkoutExercisesForWeek { get; set; }

        }

        public class WorkoutExercisesForWeekRequest
        {
            public string? Sets { get; set; }
            public string? Reps { get; set; }
            public string? Tempo { get; set; }
            public string? Rest { get; set; }
            public string? WeekNumber { get; set; }
        }

        public static List<MembershipModel> GetMembershipDataFromCSV(string filePath)
        {
            List<MembershipModel> records = new();
            using var reader = new StreamReader(filePath);

            CsvReader c = new(reader, CultureInfo.InvariantCulture);
            
            using CsvDataReader csv = new(c);
            while (csv.Read())
            {
                MembershipModel record = new MembershipModel();
                record.SKU = GetValueOrDefault<string>(csv, 0);
                record.Name = GetValueOrDefault<string>(csv, 1);
                
                record.Description = GetValueOrDefault<string>(csv, 2);
                record.StartDate = GetValueOrDefault<string>(csv, 3);
                record.EndDate = GetValueOrDefault<string>(csv, 4);
                record.URL = GetValueOrDefault<string>(csv, 5);
                record.Price = GetValueOrDefault<string>(csv, 6);
                record.IsCustom = GetValueOrDefault<string>(csv, 7);
                record.ForPurchase = GetValueOrDefault<string>(csv, 8);
                record.AccessWeekLength = GetValueOrDefault<string>(csv, 9);
                record.Gender = GetValueOrDefault<string>(csv, 10);
                record.Type = GetValueOrDefault<string>(csv, 11);
                record.RelatedMembershipIds = GetValueOrDefault<string>(csv, 12);
                record.SubAllMemberships = GetValueOrDefault<string>(csv, 13);
                records.Add(record);

            }
            return records;
        }

        public static List<ProgramModel> GetProgramDataFromCSV(string filePath)
        {
            List<ProgramModel> records = new();
            using var reader = new StreamReader(filePath);

            CsvReader c = new(reader, CultureInfo.InvariantCulture);

            using CsvDataReader csv = new(c);
            while (csv.Read())
            {
                ProgramModel record = new ProgramModel();
                record.MembershipId = GetValueOrDefault<string>(csv, 0);
                record.Name = GetValueOrDefault<string>(csv, 1);
                record.NumberOfWeeks = GetValueOrDefault<string>(csv, 2);
                record.Steps = GetValueOrDefault<string>(csv, 3);
                record.Type = GetValueOrDefault<string>(csv, 4);
                record.ProgramId = GetValueOrDefault<string>(csv, 5);
                records.Add(record);

            }
            return records;
        }

        public static List<WorkoutModel> GetWorkoutDataFromCSV(string filePath)
        {
            List<WorkoutModel> records = new();
            using var reader = new StreamReader(filePath);

            CsvReader c = new(reader, CultureInfo.InvariantCulture);

            using CsvDataReader csv = new(c);
            while (csv.Read())
            {
                var record = new WorkoutModel();
                record.Name = GetValueOrDefault<string>(csv, 0);
                record.WeekDay = GetValueOrDefault<string>(csv, 1);
                record.ProgramId = GetValueOrDefault<string>(csv, 2);
                record.WorkoutId = GetValueOrDefault<string>(csv, 3);
                records.Add(record);

            }
            return records;
        }

        public static List<WorkoutExerciseModelCsv> GetWorkoutExerciseDataFromCSV(string filePath)
        {
            List<WorkoutExerciseModelCsv> records = new();
            using var reader = new StreamReader(filePath);

            CsvReader c = new(reader, CultureInfo.InvariantCulture);

            using CsvDataReader csv = new(c);
            while (csv.Read())
            {
                var record = new WorkoutExerciseModelCsv();
                record.WorkoutId = GetValueOrDefault<string>(csv, 0);
                record.ExeerciseId = GetValueOrDefault<string>(csv, 1);
                record.Sets = GetValueOrDefault<string>(csv, 2);
                record.Reps = GetValueOrDefault<string>(csv, 3);
                record.Tempo = GetValueOrDefault<string>(csv, 4);
                record.Rest = GetValueOrDefault<string>(csv, 5);
                record.Series = GetValueOrDefault<string>(csv, 6);
                record.Notes = GetValueOrDefault<string>(csv, 7);
                record.WeeksNumber = GetValueOrDefault<string>(csv, 8);
                records.Add(record);

            }
            return records;
        }

        public static List<MembershipForPrograms> GetExerciseDataFromCSV(string filePath)
        {
            List<MembershipForPrograms> records = new();
            using var reader = new StreamReader(filePath);

            CsvReader c = new(reader, CultureInfo.InvariantCulture);

            using CsvDataReader csv = new(c);
            while (csv.Read())
            {
                var record = new MembershipForPrograms();
                record.Id = GetValueOrDefault<string>(csv, 0);
                record.Name = GetValueOrDefault<string>(csv, 1);
                records.Add(record);

            }
            return records;
        }

        public static void ReplaceMembershipId(List<ProgramModel> programList, List<MembershipForPrograms> membershipList)
        {
            var membershipDict = membershipList.ToDictionary(m => m.Name, m => m.Id);

            foreach (var program in programList)
            {
                if (program.MembershipId != null && membershipDict.TryGetValue(program.MembershipId, out var id))
                {
                    program.MembershipId = id;
                }
            }
        }

        public static void ReplaceProgramId(List<WorkoutModel> workoutList, List<MembershipForPrograms> programList)
        {
            var membershipDict = programList.ToDictionary(m => m.Name, m => m.Id);

            foreach (var workout in workoutList)
            {
                if (workout.ProgramId != null && membershipDict.TryGetValue(workout.ProgramId, out var id))
                {
                    workout.ProgramId = id;
                }
            }
        }

        public static void ReplaceWorkoutsId(List<WorkoutExerciseModelCsv> workoutList, List<MembershipForPrograms> programList)
        {
            var membershipDict = programList.ToDictionary(m => m.Name, m => m.Id);

            foreach (var workout in workoutList)
            {
                if (workout.WorkoutId != null && membershipDict.TryGetValue(workout.WorkoutId, out var id))
                {
                    workout.WorkoutId = id;
                }
            }
        }

        public static void ReplaceExerciseId(List<WorkoutExerciseModelCsv> workoutList, List<MembershipForPrograms> programList)
        {
            var membershipDict = programList.ToDictionary(m => m.Name, m => m.Id);

            foreach (var workout in workoutList)
            {
                if (workout.ExeerciseId != null && membershipDict.TryGetValue(workout.ExeerciseId, out var id))
                {
                    workout.ExeerciseId = id;
                }
            }
        }
    }
}
