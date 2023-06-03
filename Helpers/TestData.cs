using Chilkat;
using MCMAutomation.APIHelpers;
using MCMAutomation.APIHelpers.Client.WeightTracker;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;
using OpenQA.Selenium;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static Chilkat.Http;

namespace MCMAutomation.Helpers
{
    public class Endpoints
    {
        public const string WEBSITE_HOST = "https://mcmstaging-ui.azurewebsites.net/";
        public const string API_HOST = "https://mcmstaging-api.azurewebsites.net";
        public const string API_HOST_GET = "mcmstaging-api.azurewebsites.net";
    }

    public class Credentials
    {
        public const string LOGIN = "qatester91311@gmail.com";
        public const string PASSWORD = "Qaz11111!";

        public const string LOGIN_ADMIN = "admin@coachmarkcarroll.com";
        public const string PASSWORD_ADMIN = "Upgr@de21";
    }

    public class UploadedImages
    {
        public const string CREATE_MEMBER_IMG =  @"\Images\alone-with-his-thoughts-1080x720.jpg";
        public const string PHASE_IMG_1 = @"\Images\Photos-App.jpg";
        public const string PHASE_IMG_2 = @"\Images\will-burrard-lucas-beetlecam-23-1024x683.jpg"; 
    }

    public class DB
    {
        public const string GET_CONNECTION_STRING = "Data Source=tcp:markcarrollmethoddbserver.database.windows.net,1433;Initial Catalog=MarkCarrollMethodStaging; User Id=jps@coachmarkcarroll.com@markcarrollmethoddbserver;Password=Upgr@de21";
        public const string GET_CONNECTION_STRING_Live = "Data Source=tcp:markcarrollmethoddbserver.database.windows.net,1433;Initial Catalog=MarkCarrollMethod; User Id=jps@coachmarkcarroll.com@markcarrollmethoddbserver;Password=Upgr@de21";

        public class Exercises
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public DateTime CreationDate { get; set; }
            public bool IsDeleted { get; set; }
            public string VideoURL { get; set; }
            public int TempoBold { get; set; }
        }

        public class Workouts
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int WeekDay { get; set; }
            public int ProgramId { get; set; }
            public DateTime CreationDate { get; set; }
            public bool IsDeleted { get; set; }
            public int Type { get; set; }

        }

        public class Programs
        {
            public int Id { get; set; }
            public int MembershipId { get; set; }
            public string Name { get; set; }
            public int NumberOfWeeks { get; set; }
            public DateTime CreationDate { get; set; }
            public bool IsDeleted { get; set; }
            public string Steps { get; set; }
            public DateTime? AvailableDate { get; set; }
            public int? NextProgramId { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public int Type { get; set; }

        }

        public class Memberships
        {
            public int Id { get; set; }
            public string? SKU { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string? URL { get; set; }
            public decimal? Price { get; set; }
            public DateTime CreationDate { get; set; }
            public bool IsDeleted { get; set; }
            public bool IsCustom { get; set; }
            public bool ForPurchase { get; set; }
            public int? AccessWeekLength { get; set; }
            public int? RelatedMembershipGroupId { get; set; }
            public int Gender { get; set; }
            public int? PromotionalPopupId { get; set; }
            public int Type { get; set; }
        }

        public class CopyMembershipPrograms
        {
            public string MembershipName { get; set; }
            public string ProgramName { get; set; }
            public string WorkoutName { get; set; }
        }

        public class AspNetUsers
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public int ConversionSystem { get; set; }
            public int Gender { get; set; }
            public DateTime Birthdate { get; set; }
            public decimal Weight { get; set; }
            public int Height { get; set; }
            public int ActivityLevel { get; set; }
            public decimal Bodyfat { get; set; }
            public int Calories { get; set; }
            public bool Active { get; set; }
            public DateTime DateTime { get; set; }
            public string UserName { get; set; }
            public string NormalizedUserName { get; set; }
            public string NormalizedEmail { get; set; }
            public bool EmailConfirmed { get; set; }
            public string PasswordHash { get; set; }
            public string SecurityStamp { get; set; }
            public string ConcurrencyStamp { get; set; }
            public string? PhoneNumber { get; set; }
            public bool PhoneNumberConfirmed { get; set; }
            public bool TwoFactorEnabled { get; set; }
            public DateTime? LockoutEnd { get; set; }
            public bool LockoutEnabled { get; set; }
            public int AccessFailedCount { get; set; }
            public bool IsDeleted { get; set; }
            public bool IsMainAdmin { get; set; }
            public string? LastGeneratedIdentityToken { get; set; }
            public int Carbs { get; set; }
            public int Fats { get; set; }
            public int MaintenanceCalories { get; set; }
            public int Protein { get; set; }

        }

        public class ProgressDaily
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public decimal Weight { get; set; }
            public string UserId { get; set; }
            public DateTime CreationDate { get; set; }
            public bool IsDeleted { get; set; }
        }

        public class UserMemberships
        {
            public int? Id {get; set;}
            public int? MembershipId    {get; set;}
            public string? UserId {get; set;}
            public DateTime? StartOn {get; set;}
            public bool Active {get; set;}
            public DateTime? CreationDate    {get; set;}
            public bool IsDeleted {get; set;}
            public bool OnPause {get; set;}
            public DateTime? PauseEnd {get; set;}
            public DateTime? PauseStart  {get; set;}
            public bool DisplayedPromotionalPopupId {get; set;}
            public DateTime? ExpirationDate { get; set; }
        }

        public class JsonUserExercises
        {
            public int? Id { get; set; }
            public string? SetDescription   { get; set; }
            public int? WorkoutExerciseId  { get; set; }
            public string? UserId   { get; set; }
            public bool? IsDone  { get; set; }
            public DateTime? CreationDate     { get; set; }
            public bool? IsDeleted  { get; set; }
            public DateTime? UpdateDate   { get; set; }
            public int? UserMembershipId { get; set; }

        }

        
        public class CombinedUserMemberAndJsonUserEx
        {
            public int? Id { get; set; }
            public int? MembershipId { get; set; }
            public string? UserId { get; set; }
            public DateTime? StartOn { get; set; }
            public bool? Active { get; set; }
            public DateTime? CreationDate { get; set; }
            public bool? IsDeleted { get; set; }
            public bool? OnPause { get; set; }
            public DateTime? PauseEnd { get; set; }
            public DateTime? PauseStart { get; set; }
            public bool? DisplayedPromotionalPopupId { get; set; }
            public DateTime? ExpirationDate { get; set; }

            public int? Idjue { get; set; }
            public string? SetDescription { get; set; }
            public int? WorkoutExerciseId { get; set; }
            public string? UserIdjue { get; set; }
            public bool? IsDone { get; set; }
            public DateTime? CreationDatejue { get; set; }
            public bool? IsDeletedjue { get; set; }
            public DateTime? UpdateDate { get; set; }
            public int? UserMembershipId { get; set; }
        }

        public class JsonUserExOneField
        {
            public int? UserMembershipId { get; set; }
        }
    }

    public class UserAccount
    {
        public static readonly int MALE = 1;
        public static readonly int FEMALE = 2;
    }

    public class ConversionSystem
    {
        public const int METRIC = 0;
        public const int IMPERIAL = 1;
    }

    public class TDEE
    {
        public class ActivityLevel
        {
            public const string SEDETARY = "Sedentary (office job)";
            public const string LIGHT = "Light Exercise (1-2 days/week)";
            public const string MODERATE = "Moderate Exercise (3-5 days/week)";
            public const string HEAVY = "Heavy Exercise (6-7 days/week)";
            public const string ATHLETE = "Athlete (2x per day)";
        }

        public const string GOAL_CUT = "CUT";
        public const string GOAL_MAINTAIN = "MAINTAIN";
        public const string GOAL_BUILD = "BUILD";
        public const string GOAL_REVERSE = "REVERSE";

        public const string TIER_1 = "TIER 1";
        public const string TIER_2 = "TIER 2";
        public const string TIER_3 = "TIER 3";
        public const string TIER_CONSERVATIVE = "CONSERVATIVE";
        public const string TIER_AGGRESSIVE = "AGGRESSIVE";

        public const string PHASE_1 = "Phase 1";
        public const string PHASE_2 = "Phase 2";
        public const string PHASE_3 = "Phase 3";

        public const string DIET_1 = "LOW FAT / HIGH CARB";
        public const string DIET_2 = "MODERATE FAT / MODERATE CARB";
        public const string DIET_3 = "HIGH FAT / LOW CARB";

        public static List<int> BODY_FAT_FACTOR_MALE = new()
        {
            14,
            20,
            28,
            29
        };
        public static List<int> BODY_FAT_FACTOR_FEMALE = new()
        {
            18,
            28,
            38,
            39
        };

    }

    public class MembershipsSKU
    {
        public const string SKU_PP1 = "PP-1";
        public const string SKU_PRODUCT = "CMC_TEST_PRODUCT";
        public const string SKU_SUBSCRIPTION = "CMC_TEST_SUBSCRIPTION";
        public const string SKU_CHF_FREE = "CHF-FREE";
        public const string SKU_CHM_FREE = "CHM-FREE";
        public const string SKU_YGC3 = "YGC3";
        public readonly static string SKU_CHALLENGE = $"CH{DateTime.Now.Date}-MS";

    }

    public class AdditionalOptions
    {
        public const string ADDITIONAL_COMMON_OPTION = "Have you been dieting long term?";

        public const string ADDITIONAL_PG_OPTION = "Are you in the third trimester of pregnancy?";

        public class PpOptions
        {
            public const string BREASTFEEDING_LESS = "Are you breastfeeding (less than 5 months postpartum)?";
            public const string BREASTFEEDING_MORE = "Are you breastfeeding (5-12 months postpartum)?";

        }
    }

    public class ArdPhases
    {
        public static string[] ardPhase =
        {
            "1-2",
            "3-4",
            "5-6",
            "7-8",
            "9-10",
            "11-12"
        };
    }

    public class Headers
    {
        public static ICollection<KeyValuePair<string, string>> HeadersCommon()

        {
            var headersCommon = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("accept", "application/json, text/plain, /"),
                new KeyValuePair<string, string>("accept-encoding", "gzip, deflate, br")
            };

            return headersCommon;
        }
    }

    public class Workouts
    {
        public static string[] STRING =
        {
            String.Concat("Refer to the <a href = "+"\"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\""+">Welcome Pack</a> for your Cardio and Step Requirements"),
            "10000",
            String.Concat("Refer to the <a href = "+"\"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\""+">Welcome Pack</a> for your Cardio and Step Requirements")
        };
    }

    public class MembershipType
    {
        public const string PRODUCT = "Product";
        public const string SUBSCRIPTION = "Subscription";
        public const string MULTILEVEL = "Multilevel";
        public const string CUSTOM = "Custom";
        public const string ALL = "All";
    }

    public class Keyss
    {
        public static string Control()
        {
            string control = String.Empty;
            if(OperatingSystem.IsWindows())
            {
                control = Keys.Control;
            }else if(OperatingSystem.IsLinux()||OperatingSystem.IsMacOS())
            {
                control = Keys.Command;
            }

            return control;
        }
    }

    public class ProgressBodyPart
    {
        public const string WEIGHT = "weight";
        public const string WAIST = "waist";
        public const string HIP = "hip";
        public const string THIGH = "thigh";
        public const string CHEST = "chest";
        public const string ARM = "arm";
    }
}
