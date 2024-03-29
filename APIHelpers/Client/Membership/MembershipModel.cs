﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace MCMAutomation.APIHelpers.Client.Membership
{
    public class MembershipModel
    {
        public partial class GetMembership
        {
            [JsonProperty("membershipId")]
            public long MembershipId { get; set; }

            [JsonProperty("sku")]
            public string Sku { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("startOn")]
            public DateTimeOffset? StartOn { get; set; }

            [JsonProperty("expiresOn")]
            public DateTimeOffset? ExpiresOn { get; set; }

            [JsonProperty("startDate")]
            public DateTimeOffset? StartDate { get; set; }

            [JsonProperty("endDate")]
            public DateTimeOffset? EndDate { get; set; }

            [JsonProperty("url")]
            public Uri Url { get; set; }

            [JsonProperty("price")]
            public long Price { get; set; }

            [JsonProperty("userMembershipId")]
            public long UserMembershipId { get; set; }

            [JsonProperty("isActive")]
            public bool IsActive { get; set; }

            [JsonProperty("creationDate")]
            public DateTimeOffset CreationDate { get; set; }

            [JsonProperty("image")]
            public Image Image { get; set; }

            [JsonProperty("accessWeekLength")]
            public long AccessWeekLength { get; set; }

            [JsonProperty("onPause")]
            public bool OnPause { get; set; }

            [JsonProperty("pauseTime")]
            public object PauseTime { get; set; }

            [JsonProperty("pauseStart")]
            public object PauseStart { get; set; }

            [JsonProperty("pauseEnd")]
            public object PauseEnd { get; set; }

            [JsonProperty("type")]
            public long Type { get; set; }

            [JsonProperty("subAllMemberships")]
            public List<object> SubAllMemberships { get; set; }
        }

        public partial class Image
        {
            [JsonProperty("fileName")]
            public string? FileName { get; set; }

            [JsonProperty("fileType")]
            public string? FileType { get; set; }

            [JsonProperty("url")]
            public string? Url { get; set; }
        }

        public partial class GetActiveMembership
        {
            [JsonProperty("id")]
            public int? Id { get; set; }

            [JsonProperty("sku")]
            public string? Sku { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("description")]
            public string? Description { get; set; }

            [JsonProperty("startDate")]
            public DateTime? StartDate { get; set; }

            [JsonProperty("endDate")]
            public DateTime? EndDate { get; set; }

            [JsonProperty("url")]
            public string? Url { get; set; }

            [JsonProperty("price")]
            public double? Price { get; set; }

            [JsonProperty("image")]
            public Image? Image { get; set; }

            [JsonProperty("accessWeekLength")]
            public int? AccessWeekLength { get; set; }

            [JsonProperty("isCustomMembership")]
            public bool? IsCustomMembership { get; set; }

            [JsonProperty("type")]
            public int? Type { get; set; }

            [JsonProperty("subAllMembershipId")]
            public int? SubAllMembershipId { get; set; }

            [JsonProperty("currentWeekNumber")]
            public int? CurrentWeekNumber { get; set; }

            [JsonProperty("programs")]
            public List<Program>? Programs { get; set; }

            [JsonProperty("pages")]
            public List<Page>? Pages { get; set; }
        }

        public partial class Page
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("navigationLabel")]
            public string NavigationLabel { get; set; }

            [JsonProperty("order")]
            public long Order { get; set; }

            [JsonProperty("content")]
            public string Content { get; set; }

            [JsonProperty("image")]
            public Image? Image { get; set; }
        }

        public partial class Program
        {
            [JsonProperty("id")]
            public int? Id { get; set; }

            [JsonProperty("programName")]
            public string? ProgramName { get; set; }

            [JsonProperty("numberOfWeeks")]
            public int? NumberOfWeeks { get; set; }

            [JsonProperty("membershipId")]
            public int? MembershipId { get; set; }

            [JsonProperty("steps")]
            public string? Steps { get; set; }

            [JsonProperty("type")]
            public int? Type { get; set; }

            [JsonProperty("availableDate")]
            public DateTime? AvailableDate { get; set; }

            [JsonProperty("expirationDate")]
            public DateTime? ExpirationDate { get; set; }

            [JsonProperty("image")]
            public Image? Image { get; set; }

            [JsonProperty("workouts")]
            public List<Workout>? Workouts { get; set; }

            [JsonProperty("userWorkouts")]
            public List<object>? UserWorkouts { get; set; }

            [JsonProperty("nextProgramId")]
            public long? NextProgramId { get; set; }
        }

        public partial class Workout
        {
            [JsonProperty("id")]
            public long? Id { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("numberOfWeeks")]
            public long? NumberOfWeeks { get; set; }

            [JsonProperty("weekDay")]
            public long? WeekDay { get; set; }

            [JsonProperty("type")]
            public long? Type { get; set; }

            [JsonProperty("programId")]
            public long? ProgramId { get; set; }
        }

        public partial class GetListByProgramWeekRequest
        {
            [JsonProperty("programId")]
            public int? ProgramId { get; set; }

            [JsonProperty("programWeek")]
            public int? ProgramWeek { get; set; }

            [JsonProperty("isMobile")]
            public bool IsMobile { get; set; }

            [JsonProperty("startOn")]
            public DateTime? StartOn { get; set; }

            [JsonProperty("userMembershipId")]
            public int? UserMembershipId { get; set; }
        }

        public partial class GetListByProgramWeekResponse
        {
            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("numberOfWeeks")]
            public long NumberOfWeeks { get; set; }

            [JsonProperty("weekDay")]
            public long WeekDay { get; set; }

            [JsonProperty("steps")]
            public string Steps { get; set; }

            [JsonProperty("type")]
            public long Type { get; set; }

            [JsonProperty("programId")]
            public long ProgramId { get; set; }

            [JsonProperty("isCompleted")]
            public bool IsCompleted { get; set; }

            [JsonProperty("isAvailable")]
            public bool IsAvailable { get; set; }

            [JsonProperty("isUserWorkout")]
            public bool IsUserWorkout { get; set; }
        }

        public class GetUserWorkoutExercisesRequest
        {
            [JsonProperty("workoutId")]
            public long? WorkoutId { get; set; }

            [JsonProperty("weekNumber")]
            public long? WeekNumber { get; set; }

            [JsonProperty("conversionSystem")]
            public long? ConversionSystem { get; set; }

            [JsonProperty("userMembershipId")]
            public long? UserMembershipId { get; set; }
        }

        public partial class GetUserWorkoutExercisesResponse
        {
            [JsonProperty("workoutId")]
            public int? WorkoutId { get; set; }

            [JsonProperty("isCompleted")]
            public bool? IsCompleted { get; set; }

            [JsonProperty("weekDay")]
            public int? WeekDay { get; set; }

            [JsonProperty("weekNumber")]
            public int? WeekNumber { get; set; }

            [JsonProperty("activeTab")]
            public int? ActiveTab { get; set; }

            [JsonProperty("workoutExercises")]
            public List<WorkoutExercise>? WorkoutExercises { get; set; }
        }

        public partial class WorkoutExercise
        {
            [JsonProperty("exerciseId")]
            public long? ExerciseId { get; set; }

            [JsonProperty("exerciseName")]
            public string? ExerciseName { get; set; }

            [JsonProperty("notes")]
            public string? Notes { get; set; }

            [JsonProperty("exerciseVideo")]
            public string? ExerciseVideo { get; set; }

            [JsonProperty("exercisePriority")]
            public int? ExercisePriority { get; set; }

            [JsonProperty("series")]
            public string? Series { get; set; }

            [JsonProperty("previousWeekWeight")]
            public int? PreviousWeekWeight { get; set; }

            [JsonProperty("previousWeekReps")]
            public object? PreviousWeekReps { get; set; }

            [JsonProperty("workoutExerciseId")]
            public long? WorkoutExerciseId { get; set; }

            [JsonProperty("tempoBold")]
            public int? TempoBold { get; set; }

            [JsonProperty("exerciseType")]
            public int? ExerciseType { get; set; }

            [JsonProperty("workoutExerciseGroupId")]
            public long? WorkoutExerciseGroupId { get; set; }

            [JsonProperty("userNotes")]
            public List<string>? UserNotes { get; set; }

            [JsonProperty("gymRelatedExercises")]
            public List<GymRelatedExercise>? GymRelatedExercises { get; set; }

            [JsonProperty("homeRelatedExercises")]
            public List<object>? HomeRelatedExercises { get; set; }

            [JsonProperty("userExercises")]
            public List<UserExercise>? UserExercises { get; set; }

            [JsonProperty("weightHistory")]
            public List<object>? WeightHistory { get; set; }
        }

        public partial class GymRelatedExercise
        {
            [JsonProperty("id")]
            public long? Id { get; set; }

            [JsonProperty("name")]
            public string? Name { get; set; }

            [JsonProperty("videoURL")]
            public string? VideoUrl { get; set; }

            [JsonProperty("tempoBold")]
            public string? TempoBold { get; set; }

            [JsonProperty("priority")]
            public int? Priority { get; set; }

            [JsonProperty("type")]
            public int? Type { get; set; }
        }

        public partial class UserExercise
        {
            [JsonProperty("id")]
            public long? Id { get; set; }

            [JsonProperty("set")]
            public int? Set { get; set; }

            [JsonProperty("reps")]
            public string? Reps { get; set; }

            [JsonProperty("tempo")]
            public string? Tempo { get; set; }

            [JsonProperty("rest")]
            public int? Rest { get; set; }

            [JsonProperty("weight")]
            public double? Weight { get; set; }

            [JsonProperty("date")]
            public DateTime? Date { get; set; }

            [JsonProperty("isDone")]
            public bool IsDone { get; set; }
        }

        public partial class SaveCompletedWorkoutRequest
        {
            [JsonProperty("workoutId")]
            public long? WorkoutId { get; set; }

            [JsonProperty("weekNumber")]
            public long? WeekNumber { get; set; }

            [JsonProperty("userMembershipId")]
            public long? UserMembershipId { get; set; }

            [JsonProperty("conversionSystem")]
            public long? ConversionSystem { get; set; }

            [JsonProperty("userExercises")]
            public List<SaveUserExercise>? UserExercises { get; set; }
        }

        public partial class SaveUserExercise
        {
            [JsonProperty("jsonUserExerciseId")]
            public long? JsonUserExerciseId { get; set; }

            [JsonProperty("isDone")]
            public bool? IsDone { get; set; }

            [JsonProperty("reps")]
            public string? Reps { get; set; }

            [JsonProperty("set")]
            public long? Set { get; set; }

            [JsonProperty("weight")]
            public long? Weight { get; set; }
        }

        public class SubAllMembershipsReq
        {
            [JsonProperty("subAllMembershipId")]
            public long? SubAllMembershipId { get; set; }

            [JsonProperty("description")]
            public string? Description { get; set; }

        }
    }
}
