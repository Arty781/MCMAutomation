using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using static System.Net.Mime.MediaTypeNames;

namespace MCMAutomation.APIHelpers
{
    #region Memberships With Users

    [JsonObject]
    public class MembershipsWithUsersResponse
    {
        [JsonProperty("memberships")]
        public List<Membership> Memberships { get; set; }

        [JsonProperty("users")]
        public List<User> Users { get; set; }
    }

    #endregion

    #region Membership Summary

    [JsonObject]
    public class MembershipSummaryResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("startDate")]
        public DateTimeOffset? StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTimeOffset? EndDate { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("isCustom")]
        public bool IsCustom { get; set; }

        [JsonProperty("gender")]
        public long Gender { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("relatedMembershipGroupId")]
        public long? RelatedMembershipGroupId { get; set; }

        [JsonProperty("accessWeekLength")]
        public long? AccessWeekLength { get; set; }

        [JsonProperty("forPurchase")]
        public bool ForPurchase { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("relatedMembershipIds")]
        public List<long> RelatedMembershipIds { get; set; }

        [JsonProperty("programs")]
        public List<Program> Programs { get; set; }

        [JsonProperty("membershipLevels")]
        public List<MembershipLevel> MembershipLevels { get; set; }
    }

    
    public partial class Image
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileType")]
        public string FileType { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    [JsonObject]
    public partial class MembershipLevel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("membershipId")]
        public long MembershipId { get; set; }
    }

    [JsonObject]
    public partial class Program
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("programName")]
        public string ProgramName { get; set; }

        [JsonProperty("numberOfWeeks")]
        public long NumberOfWeeks { get; set; }

        [JsonProperty("membershipId")]
        public long MembershipId { get; set; }

        [JsonProperty("steps")]
        public string Steps { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("availableDate")]
        public string? AvailableDate { get; set; }

        [JsonProperty("expirationDate")]
        public string? ExpirationDate { get; set; }

        [JsonProperty("image")]
        public object Image { get; set; }

        [JsonProperty("workouts")]
        public List<Workout> Workouts { get; set; }

        [JsonProperty("nextProgramId")]
        public long? NextProgramId { get; set; }
    }

    [JsonObject]
    public partial class Workout
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("numberOfWeeks")]
        public long NumberOfWeeks { get; set; }

        [JsonProperty("weekDay")]
        public long WeekDay { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("programId")]
        public long ProgramId { get; set; }
    }

    #endregion

    [JsonObject]
    public partial class Membership
    {
        [JsonProperty("membershipId")]
        public long MembershipId { get; set; }

        [JsonProperty("membershipName")]
        public string MembershipName { get; set; }

        [JsonProperty("membershipUsers")]
        public List<MembershipUser> MembershipUsers { get; set; }
    }

    [JsonObject]
    public partial class MembershipUser
    {
        [JsonProperty("userId")]
        public Guid UserId { get; set; }

        [JsonProperty("userMembershipId")]
        public long UserMembershipId { get; set; }
    }

    [JsonObject]
    public partial class User
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public class WorkoutExercises
    {
        [JsonProperty("series")]
        public string? Series { get; set; }

        [JsonProperty("exerciseId")]
        public long? ExerciseId { get; set; }

        [JsonProperty("weekWorkoutExercises")]
        public List<WeekWorkoutExercise> WeekWorkoutExercises { get; set; }

        [JsonProperty("notes")]
        public string? Notes { get; set; }

        [JsonProperty("workoutId")]
        public long? WorkoutId { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }
    }

    public partial class WeekWorkoutExercise
    {
        [JsonProperty("sets")]
        public string Sets { get; set; }

        [JsonProperty("reps")]
        public string Reps { get; set; }

        [JsonProperty("tempo")]
        public string Tempo { get; set; }

        [JsonProperty("rest")]
        public string Rest { get; set; }

        [JsonProperty("week")]
        public long Week { get; set; }
    }

    public class WorkoutsModel
    {
        [JsonProperty("programId")]
        public long? ProgramId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("weekDay")]
        public long? WeekDay { get; set; }
    }


}
