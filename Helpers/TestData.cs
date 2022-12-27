using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions;
using OpenQA.Selenium;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chilkat.Http;

namespace MCMAutomation.Helpers
{
    public class Endpoints
    {
        public const string websiteHost = "https://mcmstaging-ui.azurewebsites.net/";
        public const string apiHost = "https://mcmstaging-api.azurewebsites.net";
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

        public class Exercises
        {
            public int Id {get; set;}
            public string Name { get; set; }
            public DateTime CreationDate { get; set;}
            public bool IsDeleted { get; set;}
            public string VideoURL { get; set;}
            public int TempoBold { get; set;}
        }

        public class Workouts
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int WeekDay { get; set;}
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
            public DateTime CreationDate { get; set;}
            public bool IsDeleted { get; set; }
            public string Steps { get; set; }
            public DateTime? AvailableDate { get; set; }
            public int? NextProgramId { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public int Type { get; set;}

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
    }

    public class Exersises
    {
        public static string[] exercise = 
        {
                "15* Incline DB Tricep Extensions",
                "30* Prone DB Row - Neutral",
                "45* Back Extension",
                "45* DB Press Neutral Grip",
                "45* Incline BB Bench Press",
                "45* Incline DB Curls",
                "45* Incline DB Press - Pronated",
                "45* Incline Zottman Curls - Rear Delt Focused",
                "45* Prone DB Lateral Raises",
                "45* Prone DB Y-Raises",
                "45* Prone Trap 3 Raises",
                "45* Standing Straight Leg Kickback - Banded",
                "45* Standing Straight Leg Kickback - Cable",
                "65* DB Arnold Press",
                "65* DB Arnold Press - 1 & 1/4 Reps",
                "65* Incline DB Bench Press - Neutral",
                "65* Incline DB Bench Press - Pronated"
        };
    }

    public class ActivityLevel
    {
        public static string[] level =
        {
            "Sedentary (office job)",
            "Light Exercise (1-2 days/week)",
            "Moderate Exercise (3-5 days/week)",
            "Heavy Exercise (6-7 days/week)",
            "Athlete (2x per day)"
        };
    }

    public class Goals
    {
        public static string[] goal =
        {
            "CUT",
            "MAINTAIN",
            "BUILD",
            "REVERSE"
        };
    }

    public class Tiers
    {
        public static string[] tier =
        {
            "TIER 1",
            "TIER 2",
            "TIER 3",
            "CONSERVATIVE",
            "AGGRESSIVE"
        };
    }

    public class Phases
    {
        public static string[] phase =
        {
            "Phase 1",
            "Phase 2",
            "Phase 3"
        };
    }

    public class Diets
    {
        public static string[] diet =
        {
            "LOW FAT / HIGH CARB",
            "MODERATE FAT / MODERATE CARB",
            "HIGH FAT / LOW CARB"
        };
    }

    public class MembershipsSKU
    {
        public static string[] MEMBERSHIP_SKU =
        {
            "PP-1",
            "CMC_TEST_SKU"
        };
    }

    public class AdditionalOptions
    {
        public static string[] additionalCommonOption =
        {
            "Have you been dieting long term?"
        };

        public static string[] additionalPgOption =
        {
            "Are you in the third trimester of pregnancy?"
        };

        public static string[] additionalPpOption =
        {
            "Are you breastfeeding (less than 5 months postpartum)?",
            "Are you breastfeeding (5-12 months postpartum)?"
        };
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
}
