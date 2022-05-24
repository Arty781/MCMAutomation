using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class RandomHelper
    {
        public static string RandomNumber(int maxWeight)
        {
            Random r = new Random();
            int genRand = r.Next(1, maxWeight);
            string randomNum = genRand.ToString();

            return randomNum;
        }

        public static string RandomText(int size)
        {
            var chars = " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[size];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }

        public static string RandomEmail()
        {
            string email = "qatester" + DateTime.Now.ToString("yyyy-MM-d-hh-mm") + "@xitroo.com";

            return email;
        }
    }
}
