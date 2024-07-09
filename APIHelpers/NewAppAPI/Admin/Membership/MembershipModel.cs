using Newtonsoft.Json;
using System.Collections.Generic;
using static MCMAutomation.APIHelpers.NewAppAPI.Admin.Membership.MembershipsRequest;

namespace MCMAutomation.APIHelpers.NewAppAPI.Admin.Membership
{
    public class MembershipModelR
    {
        public static string JsonBody(string programId, string name, string weekDay)
        {
            var body = new CreateWorkout()
            {
                ProgramId = programId,
                Name = name,
                WeekDay = int.Parse(weekDay)
            };

            return JsonConvert.SerializeObject(body);

        }

        public static string JsonBody(WorkoutExerciseModelCsv model)
        {
            var body = new CreateWorkoutExercise()
            {
                WorkoutId = model.WorkoutId,
                ExerciseId = model.ExeerciseId,
                Series = model.Series,
                Notes = model.Notes,
                WorkoutExercisesForWeek = AddExercisesForWeek(model, int.Parse(model.WeeksNumber))
            };

            return JsonConvert.SerializeObject(body);

        }

        public partial class CreateWorkout
        {
            [JsonProperty("programId")]
            public string ProgramId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("weekDay")]
            public int WeekDay { get; set; }
        }

        public partial class CreateWorkoutExercise
        {
            [JsonProperty("workoutId")]
            public string WorkoutId { get; set; }

            [JsonProperty("exerciseId")]
            public string ExerciseId { get; set; }

            [JsonProperty("series")]
            public string Series { get; set; }

            [JsonProperty("notes")]
            public string Notes { get; set; }

            [JsonProperty("workoutExercisesForWeek")]
            public List<WorkoutExercisesForWeek> WorkoutExercisesForWeek { get; set; }
        }

        public partial class WorkoutExercisesForWeek
        {
            [JsonProperty("sets")]
            public int Sets { get; set; }

            [JsonProperty("reps")]
            public string Reps { get; set; }

            [JsonProperty("tempo")]
            public string Tempo { get; set; }

            [JsonProperty("rest")]
            public int Rest { get; set; }

            [JsonProperty("weekNumber")]
            public int WeekNumber { get; set; }
        }

        private static List<WorkoutExercisesForWeek> AddExercisesForWeek(MembershipsRequest.WorkoutExerciseModelCsv bodyData, int count)
        {
            List<WorkoutExercisesForWeek> req = new();
            for (int i = 1; i <= count; i++)
            {
                var record = new WorkoutExercisesForWeek();
                record.Sets = int.Parse(bodyData.Sets);
                record.Reps = bodyData.Reps;
                record.Tempo = bodyData.Tempo;
                record.Rest = int.Parse(bodyData.Rest);
                record.WeekNumber = i;
                req.Add(record);

            }
            return req;
        }
    }
}
