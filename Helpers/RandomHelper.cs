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

        public static int RandomNumFromOne(int maxNumber = 99)
        {
            return Random.Next(1, maxNumber);
        }

        public static int RandomWeight(int maxNumber = 150)
        {
            return Random.Next(50, maxNumber);
        }

        public static int RandomProgressData(string bodyPart) =>
            bodyPart.ToLower() switch
            {
                "weight" => (minValue: 45, maxValue: 150),
                "waist" => (minValue: 45, maxValue: 120),
                "hip" => (minValue: 80, maxValue: 150),
                "thigh" => (minValue: 45, maxValue: 150),
                "chest" => (minValue: 80, maxValue: 200),
                "arm" => (minValue: 20, maxValue: 100),
                _ => throw new ArgumentException($"Invalid body part: {bodyPart}")
            } switch
            {
                (int minValue, int maxValue) => new Random().Next(minValue, maxValue + 1)
            };

        public static int RandomExercise(int exerciseCount)
        {
            return Random.Next(1, exerciseCount);
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
            return DateTime.Now.AddYears(-Random.Next(18, 65)).ToString("yyyy-MM-dd hh:mm:ss");
        }

        public static string RandomDateInThePast()
        {
            return DateTime.Now.AddDays(-Random.Next(1, 365)).ToString("yyyy-MM-dd");
        }
    }
}
