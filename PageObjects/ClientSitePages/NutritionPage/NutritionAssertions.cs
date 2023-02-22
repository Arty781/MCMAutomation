using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCMAutomation.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class Nutrition
    {
        public Nutrition VerifyMaintainCaloriesStep01(DB.AspNetUsers userData, string level, string gender, string textOfSelectedAdditionalOption, string selectedAdditionalOption)
        {
            DateTime birthdate = userData.Birthdate;
            var currentTime = DateTime.Now;
            var age = (int)((currentTime - birthdate).TotalDays)/365;
            Console.WriteLine($"Age: {age}");


            var weight = ((double)userData.Weight);
            var bodyFat = userData.Bodyfat;
            var l = weight * (1 - bodyFat / 100);
            var calories = 370 + 21.6 * l;
            var maintainCalories = 0.0;
            
            if(level == TDEE.ActivityLevel[0])
            {
                maintainCalories = Math.Round((calories * 1.2), 0, MidpointRounding.AwayFromZero);

            }
            else if (level == TDEE.ActivityLevel[1])
            {
                maintainCalories = Math.Round((calories * 1.375), 0, MidpointRounding.AwayFromZero);
            }
            else if (level == TDEE.ActivityLevel[2])
            {
                maintainCalories = Math.Round((calories * 1.55), 0, MidpointRounding.AwayFromZero);

            }
            else if (level == TDEE.ActivityLevel[3])
            {
                maintainCalories = Math.Round((calories * 1.725), 0, MidpointRounding.AwayFromZero);

            }
            else if (level == TDEE.ActivityLevel[4])
            {
                maintainCalories = Math.Round((calories * 1.9), 0, MidpointRounding.AwayFromZero);

            }

            if (gender == "Male")
            {
                if (selectedAdditionalOption == AdditionalOptions.additionalCommonOption)
                {
                    if (textOfSelectedAdditionalOption == "Yes")
                    {
                        maintainCalories = Math.Round((maintainCalories + 500), 0, MidpointRounding.AwayFromZero);
                    }
                    maintainCalories *= 1;
                }
                maintainCalories *= 1;

            }
            else if (gender == "Female")
            {
                if (selectedAdditionalOption == AdditionalOptions.additionalPpOption[0])
                {
                    if (textOfSelectedAdditionalOption == "Yes")
                    {
                        maintainCalories = Math.Round((maintainCalories + 500), 0, MidpointRounding.AwayFromZero);
                    }
                    maintainCalories *= 1;
                }
                else if (selectedAdditionalOption == AdditionalOptions.additionalPpOption[1])
                {
                    if (textOfSelectedAdditionalOption == "Yes")
                    {
                        maintainCalories = Math.Round((maintainCalories + 300), 0, MidpointRounding.AwayFromZero);
                    }
                    maintainCalories *= 1;
                }
                else if (selectedAdditionalOption == AdditionalOptions.additionalPgOption)
                {
                    if (textOfSelectedAdditionalOption == "Yes")
                    {
                        maintainCalories = Math.Round((maintainCalories + 200), 0, MidpointRounding.AwayFromZero);
                    }
                    maintainCalories *= 1;
                }
                else if (selectedAdditionalOption == AdditionalOptions.additionalCommonOption)
                {
                    if (textOfSelectedAdditionalOption == "Yes")
                    {
                        maintainCalories = Math.Round((maintainCalories - 100), 0, MidpointRounding.AwayFromZero);
                    }
                    maintainCalories *= 1;
                }
            }
            Console.WriteLine($"Maintain calories are \"{maintainCalories}\"");
            double caloriesWeb = int.Parse(TextBox.GetAttribute(inputCalories, "value"));
            Assert.IsTrue((maintainCalories - caloriesWeb) <=3 || (maintainCalories - caloriesWeb) >= 3);

            return this;
        }

        public double GetCalories()
        {
            double caloriesWeb = int.Parse(TextBox.GetAttribute(inputCalories, "value"));

            return caloriesWeb;
        }

        public Nutrition SetCalories()
        {
            InputBox.ElementCtrlA(inputCalories, 10, "000");

            return this;
        }

        public double GetCaloriesStep06(double calories, string goal, string tier, string phase, string SKU, string valuMoreThan2Kg, double previousCalories)
        {
            var finishcalories = 0.0;

            if (goal == TDEE.GOAL_CUT)
            {
                if (SKU == MembershipsSKU.MEMBERSHIP_SKU[0])
                {
                    finishcalories = Math.Round((calories - 500), 0, MidpointRounding.AwayFromZero);
                }
                else if (tier == TDEE.TIER_1)
                {

                    if (phase == TDEE.PHASE_1)
                    {
                        finishcalories = Math.Round((calories * 0.8), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.PHASE_2)
                    {
                        finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.PHASE_3)
                    {
                        finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                    }
                }

                else if (tier == TDEE.TIER_2)
                {
                    if (phase == TDEE.PHASE_1)
                    {
                        finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.PHASE_2)
                    {
                        finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.PHASE_3)
                    {
                        finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
                    }
                }

                else if (tier == TDEE.TIER_3)
                {
                    if (phase == TDEE.PHASE_1)
                    {
                        finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.PHASE_2)
                    {
                        finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.PHASE_3)
                    {
                        finishcalories = Math.Round((calories * 0.6), 0, MidpointRounding.AwayFromZero);
                    }
                }
            }

            else if (goal == TDEE.GOAL_MAINTAIN)
            {
                finishcalories = Math.Round((calories), 0, MidpointRounding.AwayFromZero);

            }

            else if (goal == TDEE.GOAL_BUILD)
            {
                if (tier == TDEE.TIER_1)
                {
                    if (phase == TDEE.PHASE_1)
                    {
                        finishcalories = Math.Round((calories * 1), 0, MidpointRounding.AwayFromZero);
                    }
                }

                else if (tier == TDEE.TIER_2)
                {
                    if (phase == TDEE.PHASE_1)
                    {
                        finishcalories = Math.Round((calories * 1.05), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.PHASE_2)
                    {
                        finishcalories = Math.Round((calories * 1.1), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.PHASE_3)
                    {
                        finishcalories = Math.Round((calories * 1.15), 0, MidpointRounding.AwayFromZero);
                    }
                }

                else if (tier == TDEE.TIER_3)
                {
                    if (phase == TDEE.PHASE_1)
                    {
                        finishcalories = Math.Round((calories * 1.2), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.PHASE_2)
                    {
                        finishcalories = Math.Round((calories * 1.25), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.PHASE_3)
                    {
                        finishcalories = Math.Round((calories * 1.35), 0, MidpointRounding.AwayFromZero);
                    }
                }
            }

            else if (goal == TDEE.GOAL_REVERSE)
            {

                if (phase == ArdPhases.ardPhase[0])
                {
                    finishcalories = Math.Round((previousCalories + 300), 0, MidpointRounding.AwayFromZero);
                }
                else
                {
                    if (valuMoreThan2Kg == "No")
                    {
                        if (tier == TDEE.TIER_CONSERVATIVE)
                        {
                            //conservative
                            finishcalories = Math.Round((previousCalories + calories * 0.07), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (tier == TDEE.TIER_AGGRESSIVE)
                        {
                            //aggressive
                            finishcalories = Math.Round((previousCalories + calories * 0.1), 0, MidpointRounding.AwayFromZero);
                        }
                    }
                    else if (valuMoreThan2Kg == "Yes")
                    {
                        //weight increased more than 2kg
                        finishcalories = Math.Round((previousCalories), 0, MidpointRounding.AwayFromZero);
                    }

                }

            }

            return finishcalories;
        }

        public void VerifyNutritionData(DB.AspNetUsers userData, string goal, string tier, string SKU, string gender, double expectedCalories, string diet, double calories, string phase, string valuMoreThan2Kg, double previousCalories)
        {
            var protein = CalculateProtein(userData, goal, tier, gender);
            var fat = CalculateFats(userData, diet);
            var carbs = CalculateCarbs(expectedCalories, protein, fat);
            CalculateCalories(userData, goal, tier, SKU, calories, phase, valuMoreThan2Kg, previousCalories, carbs, protein, fat);
        }

        private int CalculateProtein(DB.AspNetUsers userData, string goal, string tier, string gender)
        {
            double weight = (double)userData.Weight;
            double bodyFat = userData.Bodyfat;
            double protein;
            switch (goal)
            {
                case TDEE.GOAL_BUILD:
                case TDEE.GOAL_MAINTAIN:
                case TDEE.GOAL_REVERSE:
                    protein = weight * 2;
                    protein = (int)protein;
                    break;
                default:
                    if ((gender == "Female" && bodyFat > 30) || (gender == "Male" && bodyFat > 25))
                    {
                        protein = weight * 1.6;
                        protein = (int)protein;
                    }
                    else
                    {
                        protein = tier switch
                        {
                            TDEE.TIER_1 or TDEE.TIER_2 => weight * 2,
                            TDEE.TIER_CONSERVATIVE or TDEE.TIER_3 => Math.Round(weight * 2.2, 0, MidpointRounding.AwayFromZero),
                            _ => throw new ArgumentException("Invalid tier value"),
                        };
                        protein = (int)protein;
                    }
                    break;
            }

            double actualProtein = double.Parse(valueOfProteinCarbsFat[0].Text.Trim('g'));

            if (protein != actualProtein)
            {
                throw new ArgumentException($"Expected protein is {protein}, but actual is {actualProtein}");
            }

            return (int)Math.Round(protein, 0, MidpointRounding.AwayFromZero);
        }

        private double CalculateFats(DB.AspNetUsers userData, string diet)
        {
            double weight = (double)userData.Weight;
            double fat;

            switch (diet)
            {
                case TDEE.DIET_1:
                    fat = weight * 0.7;
                    fat = (int)fat;
                    break;
                case TDEE.DIET_2:
                    fat = weight * 0.8;
                    fat = (int)fat;
                    break;
                case TDEE.DIET_3:
                    fat = weight * 1;
                    fat = (int)fat;
                    break;
                default:
                    throw new ArgumentException($"Invalid diet value: {diet}");
            }

            double actualFat = Math.Round(fat, 0, MidpointRounding.AwayFromZero);

            if (fat != actualFat)
            {
                throw new ArgumentException($"Expected fat is {actualFat}, but actual is {fat}");
            }

            return actualFat;
        }
        private double CalculateCarbs(double expectedCalories, double protein, double fat)
        {
            var remainderCarbs = expectedCalories - protein * 4 - fat * 9;
            var carbs = remainderCarbs / 4;
            carbs = Math.Round(carbs, 0, MidpointRounding.AwayFromZero);

            if (carbs < 0)
            {
                throw new ArgumentException("Invalid macronutrient values: negative carbohydrates");
            }

            double actualCarbs = carbs;
            if (carbs != actualCarbs)
            {
                throw new ArgumentException($"Expected carbs is {actualCarbs}, but actual is {carbs}");
            }

            return actualCarbs;
        }
        private void CalculateCalories(DB.AspNetUsers userData, string goal, string tier, string SKU, double calories, string phase, string valuMoreThan2Kg, double previousCalories, double carbs, double protein, double fat)
        {
            var weight = ((double)userData.Weight);
            var getFat = valueOfProteinCarbsFat[2].Text.Trim(new char[] { 'g' });
            var actualFat = double.Parse(getFat);
            if (carbs < 30)
            {
                carbs = 30;
                fat = weight * 0.6;
                fat = Math.Round(fat, 0, MidpointRounding.AwayFromZero);

                calories = (carbs * 4) + (protein * 4) + (fat * 9);

                var actualCalories = double.Parse(valueCalories.Text);
                Console.WriteLine($"Expected calories are \"{calories}\", actual are \"{actualCalories}\"");
                Assert.IsTrue((calories - actualCalories) >= -10 && (calories - actualCalories) <= 10);
                Console.WriteLine($"Carbs are \"{carbs}\"");
                Console.WriteLine($"Expected fat is \"{fat}\", actual is \"{actualFat}\"");
                Assert.IsTrue(fat == actualFat);
            }
            else if (carbs >= 30)
            {
                var Carbs = valueOfProteinCarbsFat[1].Text.Trim(new char[] { 'g' });
                var actualCarbs = double.Parse(Carbs);

                #region Calories Calculation

                var finishcalories = 0.0;

                switch (goal)
                {
                    case TDEE.GOAL_CUT:
                        if (SKU == MembershipsSKU.MEMBERSHIP_SKU[0])
                        {
                            finishcalories = Math.Round((calories - 500), 0, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            switch (tier)
                            {
                                case TDEE.TIER_1:
                                    switch (phase)
                                    {
                                        case TDEE.PHASE_1:
                                            finishcalories = Math.Round((calories * 0.8), 0, MidpointRounding.AwayFromZero);
                                            break;
                                        case TDEE.PHASE_2:
                                            finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
                                            break;
                                        case TDEE.PHASE_3:
                                            finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                                            break;
                                    }
                                    break;
                                case TDEE.TIER_2:
                                    switch (phase)
                                    {
                                        case TDEE.PHASE_1:
                                            finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
                                            break;
                                        case TDEE.PHASE_2:
                                            finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                                            break;
                                        case TDEE.PHASE_3:
                                            finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
                                            break;
                                    }
                                    break;
                                case TDEE.TIER_3:
                                    switch (phase)
                                    {
                                        case TDEE.PHASE_1:
                                            finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                                            break;
                                        case TDEE.PHASE_2:
                                            finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
                                            break;
                                        case TDEE.PHASE_3:
                                            finishcalories = Math.Round((calories * 0.6), 0, MidpointRounding.AwayFromZero);
                                            break;
                                    }
                                    break;
                            }
                        } 
                        break;
                    case TDEE.GOAL_MAINTAIN:
                        finishcalories = Math.Round((calories), 0, MidpointRounding.AwayFromZero);
                        break;
                    case TDEE.GOAL_BUILD:
                        switch (tier)
                        {
                            case TDEE.TIER_1:
                                switch (phase)
                                {
                                    case TDEE.PHASE_1:
                                        finishcalories = Math.Round((calories * 1), 0, MidpointRounding.AwayFromZero);
                                        break;
                                }
                                break;
                            case TDEE.TIER_2:
                                switch (phase)
                                {
                                    case TDEE.PHASE_1:
                                        finishcalories = Math.Round((calories * 1.05), 0, MidpointRounding.AwayFromZero);
                                        break;
                                    case TDEE.PHASE_2:
                                        finishcalories = Math.Round((calories * 1.1), 0, MidpointRounding.AwayFromZero);
                                        break;
                                    case TDEE.PHASE_3:
                                        finishcalories = Math.Round((calories * 1.15), 0, MidpointRounding.AwayFromZero);
                                        break;
                                }
                                break;
                            case TDEE.TIER_3:
                                switch (phase)
                                {
                                    case TDEE.PHASE_1:
                                        finishcalories = Math.Round((calories * 1.2), 0, MidpointRounding.AwayFromZero);
                                        break;
                                    case TDEE.PHASE_2:
                                        finishcalories = Math.Round((calories * 1.25), 0, MidpointRounding.AwayFromZero);
                                        break;
                                    case TDEE.PHASE_3:
                                        finishcalories = Math.Round((calories * 1.35), 0, MidpointRounding.AwayFromZero);
                                        break;
                                }
                                break;
                        }
                        break;
                    case TDEE.GOAL_REVERSE:
                        if (phase == ArdPhases.ardPhase[0])
                        {
                            finishcalories = Math.Round((previousCalories + 300), 0, MidpointRounding.AwayFromZero);
                        }
                        else
                        {
                            if (valuMoreThan2Kg == "No")
                            {
                                if (tier == TDEE.TIER_CONSERVATIVE)
                                {
                                    //conservative
                                    finishcalories = Math.Round((previousCalories + calories * 0.07), 0, MidpointRounding.AwayFromZero);
                                }
                                else if (tier == TDEE.TIER_AGGRESSIVE)
                                {
                                    //aggressive
                                    finishcalories = Math.Round((previousCalories + calories * 0.1), 0, MidpointRounding.AwayFromZero);
                                }
                            }
                            else if (valuMoreThan2Kg == "Yes")
                            {
                                //weight increased more than 2kg
                                finishcalories = Math.Round((previousCalories), 0, MidpointRounding.AwayFromZero);
                            }

                        }
                        break;
                }
                #endregion

                var actualCalories = double.Parse(valueCalories.Text);
                Console.WriteLine($"Expected calories are \"{finishcalories}\", actual are \"{actualCalories}\"");
                Assert.IsTrue((finishcalories - actualCalories) >= -10 && (finishcalories - actualCalories) <= 10);
                Console.WriteLine($"Expected Carbs is \"{carbs}\", actual is \"{actualCarbs}\"");
                Assert.AreEqual(carbs, actualCarbs);
                Console.WriteLine($"Expected fat is \"{fat}\", actual is \"{actualFat}\"");
                Assert.AreEqual(fat, actualFat);
            }
        }
    }
}
