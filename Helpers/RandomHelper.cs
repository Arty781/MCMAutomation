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
            var r = new Random().Next(0, maxWeight);
            return r.ToString();
        }

        public static int RandomNum(int maxNum)
        {
            var r = new Random().Next(0, maxNum);

            return r;
        }

        public static int RandomNumFromOne(int maxNumber)
        {
            var r = new Random().Next(1, maxNumber);

            return r;
        }

        public static int RandomExercise(int maxWeight)
        {
            Random r = new Random();
            int genRand = r.Next(1, maxWeight);

            return genRand;
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
            string email = "qatester" + DateTime.Now.ToString("yyyy-MM-d-hh-mm-ss") + "@xitroo.com";

            return email;
        }

        public static string RandomAge()
        {
            Random r = new Random();
            int genRand = r.Next(18, 65);

            string age = DateTime.Now.AddYears(-genRand).ToString("yyyy-MM-dd");

            return age;
        }
    }
}
