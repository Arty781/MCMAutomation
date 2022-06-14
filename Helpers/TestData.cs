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
        public const string login = "qatester91323@xitroo.com";
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
}
