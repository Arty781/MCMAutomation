using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class RandomHelper
    {
        public static string RandomNumber()
        {
            Random r = new Random();
            int genRand = r.Next(10, 50);
            string randomNum = genRand.ToString();

            return randomNum;
        }

        public static string RandomEmail()
        {
            string email = "qatester" + DateTime.Now.ToString("yyyy-MM-d-hh-mm") + "@xitroo.com";

            return email;
        }
    }
}
