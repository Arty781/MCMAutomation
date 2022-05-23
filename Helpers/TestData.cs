using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class Endpoints
    {
        public const string websiteHost = "https://mcmstaging-ui.azurewebsites.net/";
    }

    public class Credentials
    {
        public const string login = "qatester91311@xitroo.com";
        public const string password = "Qaz11111!";

        public const string loginAdmin = "admin@coachmarkcarroll.com";
        public const string passwordAdmin = "Upgr@de21";
    }


    public class UploadedImages
    {
        public const string CreateMemberImg = $"\\Images\\alone-with-his-thoughts-1080x720.jpg";
        public const string PhaseImg1 = $"\\Images\\Photos-App.jpg";
        public const string PhaseImg2 = $"\\Images\\will-burrard-lucas-beetlecam-23-1024x683.webp"; 
    }

    public class DB
    {
        public const string GetConnectionString = "Data Source=tcp:markcarrollmethoddbserver.database.windows.net,1433;Initial Catalog=MarkCarrollMethodStaging; User Id=jps@coachmarkcarroll.com@markcarrollmethoddbserver;Password=Upgr@de21";
    }

    public class Exersises
    {
        public static string[] exercise = 
            { "15* Incline DB Tricep Extensions",
"30* Prone DB Row - Neutral",
"45* Back Extension",
"45* Back Extension - DB Behind Head",
"45* Back Extension - on Barbell",
"45* Back Extensions - BB on Back",
"45* Back Extensions - Rounded Back",
"45* Back Extentions - DB on Chest",
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
        public string exercise2 = "30* Prone DB Row - Neutral";
        public string exercise3 = "45* Back Extension";
        public string exercise4 = "45* Back Extension - DB Behind Head";
        public string exercise5 = "45* Back Extension - on Barbell";
        public string exercise6 = "45* Back Extensions - BB on Back";
        public string exercise7 = "45* Back Extensions - Rounded Back";
        public string exercise8 = "45* Back Extentions - DB on Chest";
        public string exercise9 = "45* DB Press Neutral Grip";
        public string exercise10 = "45* Incline BB Bench Press";
        public string exercise11 = "45* Incline DB Curls";
        public string exercise12 = "45* Incline DB Press - Pronated";
        public string exercise13 = "45* Incline Zottman Curls - Rear Delt Focused";
        public string exercise14 = "45* Prone DB Lateral Raises";
        public string exercise15 = "45* Prone DB Y-Raises";
        public string exercise16 = "45* Prone Trap 3 Raises";
        public string exercise17 = "45* Standing Straight Leg Kickback - Banded";
        public string exercise18 = "45* Standing Straight Leg Kickback - Cable";
        public string exercise19 = "65* DB Arnold Press";
        public string exercise20 = "65* DB Arnold Press - 1 & 1/4 Reps";
        public string exercise21 = "65* Incline DB Bench Press - Neutral";
        public string exercise22 = "65* Incline DB Bench Press - Pronated";
    }
}
