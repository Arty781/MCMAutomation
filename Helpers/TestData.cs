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
        public const string login = "qatester91311@xitroo.com!";
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

    public class MembershipData
    {
        public static object[] Membership()
        {
            return new object[]
              {
                  new object[] { "CP_TEST_SUB", "The challenge Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"), "Lorem ipsum dolor" },
              };
        }
    }
}
