﻿using System;
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

        //public Nutrition VerifyCaloriesStep06(double calories, string goal, string tier, string phase, string SKU, string valuMoreThan2Kg, double previousCalories)
        //{
        //    #region Conditions
        //    var finishcalories = 0.0;

        //    if (goal== TDEE.Goals[0])
        //    {
        //        if (SKU == MembershipsSKU.membershipSKU[0])
        //        {
        //            finishcalories = Math.Round((calories - 500), 0, MidpointRounding.AwayFromZero);
        //        }
        //        else if (tier == TDEE.Tiers[0])
        //        {
                    
        //            if (phase == TDEE.Phases[0])
        //            {
        //                finishcalories = Math.Round((calories * 0.8), 0, MidpointRounding.AwayFromZero);
        //            }
        //            else if (phase == TDEE.Phases[1])
        //            {
        //                finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
        //            }
        //            else if (phase == TDEE.Phases[2])
        //            {
        //                finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
        //            }
        //        }

        //        else if (tier == TDEE.Tiers[1])
        //        {
        //            if (phase == TDEE.Phases[0])
        //            {
        //                finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
        //            }
        //            else if (phase == TDEE.Phases[1])
        //            {
        //                finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
        //            }
        //            else if (phase == TDEE.Phases[2])
        //            {
        //                finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
        //            }
        //        }

        //        else if (tier == TDEE.Tiers[2])
        //        {
        //            if (phase == TDEE.Phases[0])
        //            {
        //                finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
        //            }
        //            else if (phase == TDEE.Phases[1])
        //            {
        //                finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
        //            }
        //            else if (phase == TDEE.Phases[2])
        //            {
        //                finishcalories = Math.Round((calories * 0.6), 0, MidpointRounding.AwayFromZero);
        //            }
        //        }
        //    }

        //    else if (goal == TDEE.Goals[1])
        //    {
        //        finishcalories = Math.Round((calories), 0, MidpointRounding.AwayFromZero);
               
        //    }

        //    else if (goal == TDEE.Goals[2])
        //    {
        //        if (tier == TDEE.Tiers[0])
        //        {
        //            if (phase == TDEE.Phases[0])
        //            {
        //                finishcalories = Math.Round((calories * 1), 0, MidpointRounding.AwayFromZero);
        //            }
        //        }

        //        else if (tier == TDEE.Tiers[1])
        //        {
        //            if (phase == TDEE.Phases[0])
        //            {
        //                finishcalories = Math.Round((calories * 1.05), 0, MidpointRounding.AwayFromZero);
        //            }
        //            else if (phase == TDEE.Phases[1])
        //            {
        //                finishcalories = Math.Round((calories * 1.1), 0, MidpointRounding.AwayFromZero);
        //            }
        //            else if (phase == TDEE.Phases[2])
        //            {
        //                finishcalories = Math.Round((calories * 1.15), 0, MidpointRounding.AwayFromZero);
        //            }
        //        }

        //        else if (tier == TDEE.Tiers[2])
        //        {
        //            if (phase == TDEE.Phases[0])
        //            {
        //                finishcalories = Math.Round((calories * 1.2), 0, MidpointRounding.AwayFromZero);
        //            }
        //            else if (phase == TDEE.Phases[1])
        //            {
        //                finishcalories = Math.Round((calories * 1.25), 0, MidpointRounding.AwayFromZero);
        //            }
        //            else if (phase == TDEE.Phases[2])
        //            {
        //                finishcalories = Math.Round((calories * 1.35), 0, MidpointRounding.AwayFromZero);
        //            }
        //        }
        //    }

        //    else if(goal == TDEE.Goals[3])
        //    {
                
        //            if (phase == ArdPhases.ardPhase[0])
        //            {
        //                finishcalories = Math.Round((previousCalories + 300), 0, MidpointRounding.AwayFromZero);
        //            }
        //            else
        //            {
        //                if (valuMoreThan2Kg == "No")
        //                {
        //                    if (tier == TDEE.Tiers[3])
        //                    {
        //                    //conservative
        //                    finishcalories = Math.Round((previousCalories + calories * 0.07), 0, MidpointRounding.AwayFromZero);
        //                    }
        //                    else if (tier == TDEE.Tiers[4])
        //                    {
        //                    //aggressive
        //                    finishcalories = Math.Round((previousCalories + calories * 0.1), 0, MidpointRounding.AwayFromZero);
        //                    }
        //                }
        //                else if (valuMoreThan2Kg == "Yes")
        //                {
        //                //weight increased more than 2kg
        //                finishcalories = Math.Round((previousCalories), 0, MidpointRounding.AwayFromZero);
        //                }

        //            }
                
        //    }

        //    #endregion

        //    var actualCalories = double.Parse(valueCalories.Text);
        //    Console.WriteLine($"Expected calories are \"{finishcalories}\", actual are \"{actualCalories}\"");
        //    //Assert.IsTrue((finishcalories - actualCalories) >= -5 && (finishcalories - actualCalories) <= 5);

        //    return this;
        //}

        public double GetCaloriesStep06(double calories, string goal, string tier, string phase, string SKU, string valuMoreThan2Kg, double previousCalories)
        {
            var finishcalories = 0.0;

            if (goal == TDEE.Goals[0])
            {
                if (SKU == MembershipsSKU.MEMBERSHIP_SKU[0])
                {
                    finishcalories = Math.Round((calories - 500), 0, MidpointRounding.AwayFromZero);
                }
                else if (tier == TDEE.Tiers[0])
                {

                    if (phase == TDEE.Phases[0])
                    {
                        finishcalories = Math.Round((calories * 0.8), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.Phases[1])
                    {
                        finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.Phases[2])
                    {
                        finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                    }
                }

                else if (tier == TDEE.Tiers[1])
                {
                    if (phase == TDEE.Phases[0])
                    {
                        finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.Phases[1])
                    {
                        finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.Phases[2])
                    {
                        finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
                    }
                }

                else if (tier == TDEE.Tiers[2])
                {
                    if (phase == TDEE.Phases[0])
                    {
                        finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.Phases[1])
                    {
                        finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.Phases[2])
                    {
                        finishcalories = Math.Round((calories * 0.6), 0, MidpointRounding.AwayFromZero);
                    }
                }
            }

            else if (goal == TDEE.Goals[1])
            {
                finishcalories = Math.Round((calories), 0, MidpointRounding.AwayFromZero);

            }

            else if (goal == TDEE.Goals[2])
            {
                if (tier == TDEE.Tiers[0])
                {
                    if (phase == TDEE.Phases[0])
                    {
                        finishcalories = Math.Round((calories * 1), 0, MidpointRounding.AwayFromZero);
                    }
                }

                else if (tier == TDEE.Tiers[1])
                {
                    if (phase == TDEE.Phases[0])
                    {
                        finishcalories = Math.Round((calories * 1.05), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.Phases[1])
                    {
                        finishcalories = Math.Round((calories * 1.1), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.Phases[2])
                    {
                        finishcalories = Math.Round((calories * 1.15), 0, MidpointRounding.AwayFromZero);
                    }
                }

                else if (tier == TDEE.Tiers[2])
                {
                    if (phase == TDEE.Phases[0])
                    {
                        finishcalories = Math.Round((calories * 1.2), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.Phases[1])
                    {
                        finishcalories = Math.Round((calories * 1.25), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (phase == TDEE.Phases[2])
                    {
                        finishcalories = Math.Round((calories * 1.35), 0, MidpointRounding.AwayFromZero);
                    }
                }
            }

            else if (goal == TDEE.Goals[3])
            {

                if (phase == ArdPhases.ardPhase[0])
                {
                    finishcalories = Math.Round((previousCalories + 300), 0, MidpointRounding.AwayFromZero);
                }
                else
                {
                    if (valuMoreThan2Kg == "No")
                    {
                        if (tier == TDEE.Tiers[3])
                        {
                            //conservative
                            finishcalories = Math.Round((previousCalories + calories * 0.07), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (tier == TDEE.Tiers[4])
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

        //public Nutrition VerifyProtein(string[] values, string goal, string tier, string SKU, string[] gender)
        //{
        //    var weight = double.Parse(values[1]);
        //    var bodyFat = double.Parse(values[3]);
        //    var protein = 0.0;

        //    if (goal == TDEE.Goals[1] || goal == TDEE.Goals[2] || goal == TDEE.Goals[3]) {
        //        protein = weight * 2;
        //    } else
        //    {
        //        if (
        //            (gender[0] == "Female" && bodyFat > 35) ||
        //            (gender[0] == "Male" && bodyFat > 20)
        //        )
        //        {
        //            protein = weight * 1.6;
        //        }
        //        else if (SKU == "PP-1")
        //        {
        //            protein = weight * 2;
        //        }
        //        else if (tier == TDEE.Tiers[0])
        //        {
        //            protein = weight * 2;
        //        }
        //        else if (tier == TDEE.Tiers[1])
        //        {
        //            protein = weight * 2;
        //        }
        //        else if (tier == TDEE.Tiers[2])
        //        {
        //            protein = weight * 2.2;
        //        }
        //    }
        //    protein = Math.Round(protein , 0, MidpointRounding.AwayFromZero);

        //    var actualProtein = valueOfProteinCarbsFat[0].Text;
        //    actualProtein = actualProtein.Trim(new char[] { 'g' });

        //    Console.WriteLine($"Expected protein is \"{protein}\", actual is \"{actualProtein}\"");
        //    //Assert.AreEqual(protein.ToString(), actualProtein);

        //    return this;

        //}

        //public double GetProtein(string[] values, string goal, string tier, string SKU, string[] gender)
        //{
        //    var weight = double.Parse(values[1]);
        //    var bodyFat = double.Parse(values[3]);
        //    var protein = 0.0;

        //    if (goal == TDEE.Goals[1] || goal == TDEE.Goals[2] || goal == TDEE.Goals[3])
        //    {
        //        protein = weight * 2;
        //    }
        //    else
        //    {
        //        if (
        //            (gender[0] == "Female" && bodyFat > 35) ||
        //            (gender[0] == "Male" && bodyFat > 20)
        //        )
        //        {
        //            protein = weight * 1.6;
        //        }
        //        else if (SKU == "PP-1")
        //        {
        //            protein = weight * 2;
        //        }
        //        else if (tier == TDEE.Tiers[0])
        //        {
        //            protein = weight * 2;
        //        }
        //        else if (tier == TDEE.Tiers[1])
        //        {
        //            protein = weight * 2;
        //        }
        //        else if (tier == TDEE.Tiers[2])
        //        {
        //            protein = weight * 2.2;
        //        }
        //    }
        //    protein = Math.Round(protein, 0, MidpointRounding.AwayFromZero);

        //    return protein;
        //}

        //public void VerifyFatAndCarbs(double protein, double expectedCalories, string[] values, string diet)
        //{
        //    var weight = double.Parse(values[1]);
        //    var fat = 0.0;

        //    var getFat = valueOfProteinCarbsFat[2].Text.Trim(new char[] { 'g' });
        //    var actualFat = double.Parse(getFat);

        //    if (diet == TDEE.Diets[0])
        //    {
        //        fat = weight * 0.8;
        //    }
        //    else if (diet == TDEE.Diets[1])
        //    {
        //        fat = weight * 1;
        //    }
        //    else if (diet == TDEE.Diets[2])
        //    {
        //        fat = weight * 1.2;
        //    }
        //    fat = Math.Round(fat, 0, MidpointRounding.AwayFromZero);

        //    var expectedFat = fat;

        //    var remainderCarbs = expectedCalories - protein * 4 - fat * 9;
        //    var carbs = remainderCarbs / 4;
        //    carbs = Math.Round(carbs, 0, MidpointRounding.AwayFromZero);
        //    if (carbs < 30)
        //    {
        //        carbs = 30;
        //        fat = weight * 0.6;
        //        fat = Math.Round(fat, 0, MidpointRounding.AwayFromZero);

        //        Console.WriteLine($"Carbs are \"{carbs}\"");
        //        Console.WriteLine($"Expected fat is \"{fat}\", actual is \"{actualFat}\"");
        //        Assert.AreEqual(fat, actualFat);
        //    }
        //    else if(carbs >= 30)
        //    {
        //        var Carbs = valueOfProteinCarbsFat[1].Text.Trim(new char[] { 'g' });
        //        var actualCarbs = double.Parse(Carbs);

        //        Console.WriteLine($"Expected Carbs is \"{carbs}\", actual is \"{actualCarbs}\"");
        //        Assert.AreEqual(carbs, actualCarbs);
        //        Console.WriteLine($"Expected fat is \"{fat}\", actual is \"{actualFat}\"");
        //        Assert.AreEqual(fat, actualFat);
        //    }
        //}

        public void VerifyNutritionData(DB.AspNetUsers userData, string goal, string tier, string SKU, string gender, double expectedCalories, string diet, double calories, string phase, string valuMoreThan2Kg, double previousCalories)
        {
            #region Protein Calculation

            var weight = ((double)userData.Weight);
            var bodyFat = userData.Bodyfat;
            var protein = 0.0;

            if (goal == TDEE.Goals[1] || goal == TDEE.Goals[2] || goal == TDEE.Goals[3])
            {
                protein = weight * 2;
            }
            else
            {
                if (
                    (gender == "Female" && bodyFat > 30) ||
                    (gender == "Male" && bodyFat > 25)
                )
                {
                    protein = weight * 1.6;
                }
                else if (SKU == "PP-1")
                {
                    protein = weight * 2;
                }
                else if (tier == TDEE.Tiers[0])
                {
                    protein = weight * 2;
                }
                else if (tier == TDEE.Tiers[1])
                {
                    protein = weight * 2;
                }
                else if (tier == TDEE.Tiers[2])
                {
                    protein = weight * 2.2;
                }
            }
            protein = Math.Round(protein, 0, MidpointRounding.AwayFromZero);

            var actualProtein = valueOfProteinCarbsFat[0].Text;
            actualProtein = actualProtein.Trim(new char[] { 'g' });

            Console.WriteLine($"Expected protein is \"{protein}\", actual is \"{actualProtein}\"");
            Assert.IsTrue(protein.ToString() == actualProtein);

            #endregion

            #region Carbs and fats Calculation

            var fat = 0.0;

            var getFat = valueOfProteinCarbsFat[2].Text.Trim(new char[] { 'g' });
            var actualFat = double.Parse(getFat);

            if (diet == TDEE.Diets[0])
            {
                fat = weight * 0.7;
            }
            else if (diet == TDEE.Diets[1])
            {
                fat = weight * 0.8;
            }
            else if (diet == TDEE.Diets[2])
            {
                fat = weight * 1;
            }
            fat = Math.Round(fat, 0, MidpointRounding.AwayFromZero);

            var remainderCarbs = expectedCalories - protein * 4 - fat * 9;
            var carbs = remainderCarbs / 4;
            carbs = Math.Round(carbs, 0, MidpointRounding.AwayFromZero);
            if (carbs < 30)
            {
                carbs = 30;
                fat = weight * 0.6;
                fat = Math.Round(fat, 0, MidpointRounding.AwayFromZero);

                calories = (carbs*4) + (protein*4) +(fat*9);

                var actualCalories = double.Parse(valueCalories.Text);
                Console.WriteLine($"Expected calories are \"{calories}\", actual are \"{actualCalories}\"");
                Assert.IsTrue((calories - actualCalories) >= -5 && (calories - actualCalories) <= 5);
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

                if (goal == TDEE.Goals[0])
                {
                    if (SKU == MembershipsSKU.MEMBERSHIP_SKU[0])
                    {
                        finishcalories = Math.Round((calories - 500), 0, MidpointRounding.AwayFromZero);
                    }
                    else if (tier == TDEE.Tiers[0])
                    {

                        if (phase == TDEE.Phases[0])
                        {
                            finishcalories = Math.Round((calories * 0.8), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (phase == TDEE.Phases[1])
                        {
                            finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (phase == TDEE.Phases[2])
                        {
                            finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                        }
                    }

                    else if (tier == TDEE.Tiers[1])
                    {
                        if (phase == TDEE.Phases[0])
                        {
                            finishcalories = Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (phase == TDEE.Phases[1])
                        {
                            finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (phase == TDEE.Phases[2])
                        {
                            finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
                        }
                    }

                    else if (tier == TDEE.Tiers[2])
                    {
                        if (phase == TDEE.Phases[0])
                        {
                            finishcalories = Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (phase == TDEE.Phases[1])
                        {
                            finishcalories = Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (phase == TDEE.Phases[2])
                        {
                            finishcalories = Math.Round((calories * 0.6), 0, MidpointRounding.AwayFromZero);
                        }
                    }
                }

                else if (goal == TDEE.Goals[1])
                {
                    finishcalories = Math.Round((calories), 0, MidpointRounding.AwayFromZero);

                }

                else if (goal == TDEE.Goals[2])
                {
                    if (tier == TDEE.Tiers[0])
                    {
                        if (phase == TDEE.Phases[0])
                        {
                            finishcalories = Math.Round((calories * 1), 0, MidpointRounding.AwayFromZero);
                        }
                    }

                    else if (tier == TDEE.Tiers[1])
                    {
                        if (phase == TDEE.Phases[0])
                        {
                            finishcalories = Math.Round((calories * 1.05), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (phase == TDEE.Phases[1])
                        {
                            finishcalories = Math.Round((calories * 1.1), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (phase == TDEE.Phases[2])
                        {
                            finishcalories = Math.Round((calories * 1.15), 0, MidpointRounding.AwayFromZero);
                        }
                    }

                    else if (tier == TDEE.Tiers[2])
                    {
                        if (phase == TDEE.Phases[0])
                        {
                            finishcalories = Math.Round((calories * 1.2), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (phase == TDEE.Phases[1])
                        {
                            finishcalories = Math.Round((calories * 1.25), 0, MidpointRounding.AwayFromZero);
                        }
                        else if (phase == TDEE.Phases[2])
                        {
                            finishcalories = Math.Round((calories * 1.35), 0, MidpointRounding.AwayFromZero);
                        }
                    }
                }

                else if (goal == TDEE.Goals[3])
                {

                    if (phase == ArdPhases.ardPhase[0])
                    {
                        finishcalories = Math.Round((previousCalories + 300), 0, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        if (valuMoreThan2Kg == "No")
                        {
                            if (tier == TDEE.Tiers[3])
                            {
                                //conservative
                                finishcalories = Math.Round((previousCalories + calories * 0.07), 0, MidpointRounding.AwayFromZero);
                            }
                            else if (tier == TDEE.Tiers[4])
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

                #endregion

                var actualCalories = double.Parse(valueCalories.Text);
                Console.WriteLine($"Expected calories are \"{finishcalories}\", actual are \"{actualCalories}\"");
                Assert.IsTrue((finishcalories - actualCalories) >= -5 && (finishcalories - actualCalories) <= 5);
                Console.WriteLine($"Expected Carbs is \"{carbs}\", actual is \"{actualCarbs}\"");
                Assert.AreEqual(carbs, actualCarbs);
                Console.WriteLine($"Expected fat is \"{fat}\", actual is \"{actualFat}\"");
                Assert.AreEqual(fat, actualFat);
            }

            #endregion
        }


    }
}
