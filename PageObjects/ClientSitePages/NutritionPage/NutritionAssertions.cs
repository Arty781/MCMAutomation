using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMAutomation.Helpers;
using NUnit.Framework;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class Nutrition
    {
        public Nutrition VerifyMaintainCalories(string[] values, string level)
        {
            DateTime birthdate = DateTime.Parse(values[0]);
            var currentTime = DateTime.Now;
            var age = (int)((currentTime - birthdate).TotalDays)/365;
            Console.WriteLine(age);


            var weight = double.Parse(values[1]);
            var bodyFat = int.Parse(values[3]);
            var l = weight * (1 - (double)bodyFat / 100);
            var calories = 370 + 21.6 * l;
            var maintainCalories = 0.0;
            
            if(level == ActivityLevel.level[0])
            {
                maintainCalories = Math.Round((calories * 1.2), 0, MidpointRounding.AwayFromZero);

            }
            if (level == ActivityLevel.level[1])
            {
                maintainCalories = Math.Round((calories * 1.375), 0, MidpointRounding.AwayFromZero);
            }
            if (level == ActivityLevel.level[2])
            {
                maintainCalories = Math.Round((calories * 1.55), 0, MidpointRounding.AwayFromZero);

            }
            if (level == ActivityLevel.level[3])
            {
                maintainCalories = Math.Round((calories * 1.725), 0, MidpointRounding.AwayFromZero);

            }
            if (level == ActivityLevel.level[4])
            {
                maintainCalories = Math.Round((calories * 1.9), 0, MidpointRounding.AwayFromZero);

            }
            Console.WriteLine(maintainCalories);
            int caloriesWeb = int.Parse(TextBox.GetAttribute(inputCalories, "value"));

            Assert.AreEqual(maintainCalories, caloriesWeb);



            return this;
        }
    }
}
