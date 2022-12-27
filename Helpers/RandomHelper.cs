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

        public static int RandomProgressData(string bodyPart)
        {
            int r = 0;
            if(bodyPart.ToLower() == "weight")
            {
                r = new Random().Next(45, 150);
            }
            else if(bodyPart.ToLower() == "waist")
            {
                r = new Random().Next(45, 120);
            }
            else if (bodyPart.ToLower() == "hip")
            {
                r = new Random().Next(80, 150);
            }
            else if (bodyPart.ToLower() == "thigh")
            {
                r = new Random().Next(45, 150);
            }
            else if (bodyPart.ToLower() == "chest")
            {
                r = new Random().Next(80, 200);
            }
            else if (bodyPart.ToLower() == "arm")
            {
                r = new Random().Next(20, 100);
            }

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
