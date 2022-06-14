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
        public Nutrition VerifyMaintainCaloriesStep01(string[] values, string level)
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
            double caloriesWeb = int.Parse(TextBox.GetAttribute(inputCalories, "value"));

            Assert.AreEqual(maintainCalories, caloriesWeb);



            return this;
        }

        public double GetCalories()
        {
            double caloriesWeb = int.Parse(TextBox.GetAttribute(inputCalories, "value"));

            return caloriesWeb;
        }

        public Nutrition VerifyCaloriesStep06(double calories, string goal, string tier, string phase)
        {
            var finishcalories = 0.0;

            if (goal== Goals.goal[0])
            {
                if(tier == Tiers.tier[0])
                {
                    if(phase == Phases.phase[0])
                    {
                        finishcalories = Math.Round((calories * 0.8), 0, MidpointRounding.AwayFromZero);
                    }
                    if (phase == Phases.phase[1])
                    {
                        finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
                    }
                    if (phase == Phases.phase[2])
                    {
                        finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                    }
                }

                if (tier == Tiers.tier[1])
                {
                    if (phase == Phases.phase[0])
                    {
                        finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
                    }
                    if (phase == Phases.phase[1])
                    {
                        finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                    }
                    if (phase == Phases.phase[2])
                    {
                        finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
                    }
                }

                if (tier == Tiers.tier[2])
                {
                    if (phase == Phases.phase[0])
                    {
                        finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                    }
                    if (phase == Phases.phase[1])
                    {
                        finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
                    }
                    if (phase == Phases.phase[2])
                    {
                        finishcalories = Math.Round((calories * 0.6), 0, MidpointRounding.AwayFromZero);
                    }
                }
            }

            if (goal == Goals.goal[1])
            {
                finishcalories = Math.Round((calories), 0, MidpointRounding.AwayFromZero);
               
            }

            if (goal == Goals.goal[2])
            {
                if (tier == Tiers.tier[0])
                {
                    if (phase == Phases.phase[0])
                    {
                        finishcalories = Math.Round((calories * 1), 0, MidpointRounding.AwayFromZero);
                    }
                }

                if (tier == Tiers.tier[1])
                {
                    if (phase == Phases.phase[0])
                    {
                        finishcalories = Math.Round((calories * 1.05), 0, MidpointRounding.AwayFromZero);
                    }
                    if (phase == Phases.phase[1])
                    {
                        finishcalories = Math.Round((calories * 1.1), 0, MidpointRounding.AwayFromZero);
                    }
                    if (phase == Phases.phase[2])
                    {
                        finishcalories = Math.Round((calories * 1.15), 0, MidpointRounding.AwayFromZero);
                    }
                }

                if (tier == Tiers.tier[2])
                {
                    if (phase == Phases.phase[0])
                    {
                        finishcalories = Math.Round((calories * 1.2), 0, MidpointRounding.AwayFromZero);
                    }
                    if (phase == Phases.phase[1])
                    {
                        finishcalories = Math.Round((calories * 1.25), 0, MidpointRounding.AwayFromZero);
                    }
                    if (phase == Phases.phase[2])
                    {
                        finishcalories = Math.Round((calories * 1.35), 0, MidpointRounding.AwayFromZero);
                    }
                }
            }

            Console.WriteLine(finishcalories);

            string actualCalories = valueCalories.Text;

            Assert.AreEqual(finishcalories.ToString(), actualCalories);

            return this;
        }
    }
}
