using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public static class RandomHelper
    {
        private static readonly Random Random = new Random();

        public static string RandomNumber(int maxWeight)
        {
            return Random.Next(0, maxWeight).ToString();
        }

        public static int RandomNum(int maxNum)
        {
            return Random.Next(0, maxNum);
        }

        public static int RandomNumFromOne(int maxNumber)
        {
            return Random.Next(1, maxNumber);
        }

        public static int RandomProgressData(string bodyPart)
        {
            int minValue = 0;
            int maxValue = 0;

            switch (bodyPart.ToLower())
            {
                case "weight":
                    minValue = 45;
                    maxValue = 150;
                    break;
                case "waist":
                    minValue = 45;
                    maxValue = 120;
                    break;
                case "hip":
                    minValue = 80;
                    maxValue = 150;
                    break;
                case "thigh":
                    minValue = 45;
                    maxValue = 150;
                    break;
                case "chest":
                    minValue = 80;
                    maxValue = 200;
                    break;
                case "arm":
                    minValue = 20;
                    maxValue = 100;
                    break;
            }

            return Random.Next(minValue, maxValue);
        }

        public static int RandomExercise(int maxWeight)
        {
            return Random.Next(1, maxWeight);
        }

        public static string RandomText(int size)
        {
            const string chars = " ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, size)
              .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string RandomEmail()
        {
            return $"qatester{DateTime.Now:yyyy-MM-d-hh-mm-ss}@xitroo.com";
        }

        public static string RandomAge()
        {
            return DateTime.Now.AddYears(-Random.Next(18, 65)).ToString("yyyy-MM-dd");
        }

        public static string RandomDateInThePast()
        {
            return DateTime.Now.AddDays(-Random.Next(1, 365)).ToString("yyyy-MM-dd");
        }
    }
}
