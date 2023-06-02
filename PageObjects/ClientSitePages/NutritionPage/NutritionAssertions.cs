using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MCMAutomation.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class Nutrition
    {
        //public Nutrition VerifyMaintainCaloriesStep01(DB.AspNetUsers userData, string level, string gender, string textOfSelectedAdditionalOption, string selectedAdditionalOption)
        //{
        //    DateTime birthdate = userData.Birthdate;
        //    var currentTime = DateTime.Now;
        //    var age = (int)((currentTime - birthdate).TotalDays)/365;
        //    Console.WriteLine($"Age: {age}");

        //    var weight = (double)userData.Weight;
        //    var bodyFat = (double)userData.Bodyfat;
        //    var leanBodyMass = weight*(1 - (bodyFat / 100));
        //    var calories = 370 + 21.6 * leanBodyMass;
        //    var maintainCalories = 0.0;

        //    int levelIndex = TDEE.ActivityLevel.IndexOf(level);
        //    if (levelIndex == 0)
        //    {
        //        maintainCalories = Math.Round((calories * 1.2), 0, MidpointRounding.AwayFromZero);
        //    }
        //    else if (levelIndex == 1)
        //    {
        //        maintainCalories = Math.Round((calories * 1.375), 0, MidpointRounding.AwayFromZero);
        //    }
        //    else if (levelIndex == 2)
        //    {
        //        maintainCalories = Math.Round((calories * 1.55), 0, MidpointRounding.AwayFromZero);
        //    }
        //    else if (levelIndex == 3)
        //    {
        //        maintainCalories = Math.Round((calories * 1.725), 0, MidpointRounding.AwayFromZero);
        //    }
        //    else if (levelIndex == 4)
        //    {
        //        maintainCalories = Math.Round((calories * 1.9), 0, MidpointRounding.AwayFromZero);
        //    }
        //    else
        //    {
        //        // handle invalid levelIndex
        //        maintainCalories = 0;
        //    }

        //    var calorieAdjustments = new Dictionary<string, int>
        //    {
        //        { AdditionalOptions.additionalPpOption[0], 500 },
        //        { AdditionalOptions.additionalPpOption[1], 300 },
        //        { AdditionalOptions.additionalPgOption, 200 },
        //        { AdditionalOptions.additionalCommonOption, -100 }
        //    };

        //    if (gender == "Male" && selectedAdditionalOption == AdditionalOptions.additionalCommonOption && textOfSelectedAdditionalOption == "Yes")
        //    {
        //        maintainCalories += 500;
        //    }
        //    else if (gender == "Female" && calorieAdjustments.TryGetValue(selectedAdditionalOption, out int adjustment))
        //    {
        //        if (textOfSelectedAdditionalOption == "Yes")
        //        {
        //            maintainCalories = Math.Round((maintainCalories + adjustment), 0, MidpointRounding.AwayFromZero);
        //        }
        //        maintainCalories *= 1;
        //    }

        //    Console.WriteLine($"Maintain calories are \"{maintainCalories}\"");
        //    double caloriesWeb = int.Parse(TextBox.GetAttribute(inputCalories, "value"));
        //    Assert.IsTrue(Math.Abs(maintainCalories - caloriesWeb) <= 3);

        //    return this;
        //}

        public Nutrition VerifyMaintainCaloriesStep01(DB.AspNetUsers userData, string level, string gender, string textOfSelectedAdditionalOption, string selectedAdditionalOption)
        {
            DateTime birthdate = userData.Birthdate;
            var currentTime = DateTime.Now;
            var age = (int)((currentTime - birthdate).TotalDays) / 365;
            var weight = ((double)userData.Weight);
            var bodyFat = ((double)userData.Bodyfat);
            var bodyFatPercentage = ((double)userData.Bodyfat)/100;
            var lbm = weight * (1 - bodyFatPercentage);
            var calories = 370 + (21.6 * lbm);
            var maintainCalories = 0.0;
            
            if (level == TDEE.ActivityLevel[0])
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
                if (selectedAdditionalOption == AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                {
                    if (textOfSelectedAdditionalOption == "Yes")
                    {
                        maintainCalories = Math.Round((maintainCalories -100), 0, MidpointRounding.AwayFromZero);
                    }
                    maintainCalories *= 1;
                }
                maintainCalories *= 1;

            }
            else if (gender == "Female")
            {
                if (selectedAdditionalOption == AdditionalOptions.ADDITIONAL_PP_OPTION[0])
                {
                    if (textOfSelectedAdditionalOption == "Yes")
                    {
                        maintainCalories = Math.Round((maintainCalories + 500), 0, MidpointRounding.AwayFromZero);
                    }
                    maintainCalories *= 1;
                }
                else if (selectedAdditionalOption == AdditionalOptions.ADDITIONAL_PP_OPTION[1])
                {
                    if (textOfSelectedAdditionalOption == "Yes")
                    {
                        maintainCalories = Math.Round((maintainCalories + 300), 0, MidpointRounding.AwayFromZero);
                    }
                    maintainCalories *= 1;
                }
                else if (selectedAdditionalOption == AdditionalOptions.ADDITIONAL_PG_OPTION)
                {
                    if (textOfSelectedAdditionalOption == "Yes")
                    {
                        maintainCalories = Math.Round((maintainCalories + 200), 0, MidpointRounding.AwayFromZero);
                    }
                    maintainCalories *= 1;
                }
                else if (selectedAdditionalOption == AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                {
                    if (textOfSelectedAdditionalOption == "Yes")
                    {
                        maintainCalories = Math.Round((maintainCalories - 100), 0, MidpointRounding.AwayFromZero);
                    }
                    maintainCalories *= 1;
                }
            }
            
            double caloriesWeb = int.Parse(TextBox.GetAttribute(inputCalories, "value"));
            Assert.IsTrue(Math.Abs(maintainCalories - caloriesWeb) <= 50, $"MaintainCalories difference is: {Math.Abs(maintainCalories - caloriesWeb)}");

            return this;
        }

        public double GetCalories()
        {
            return int.Parse(TextBox.GetAttribute(inputCalories, "value")); ;
        }

        public Nutrition SetCalories()
        {
            InputBox.ElementCtrlA(inputCalories, 10, "000");

            return this;
        }

        public double GetCaloriesStep06(double calories, string goal, string tier, string phase, string SKU, string valuMoreThan2Kg, double previousCalories, string eightWeekSKU)
        {
            double finishcalories;
            switch (goal)
            {
                case TDEE.GOAL_CUT:
                    if (SKU == MembershipsSKU.MEMBERSHIP_SKU[0])
                        finishcalories = calories - 500;
                    else
                    {
                        double multiplier;
                        if (eightWeekSKU.StartsWith("CH") == true)
                        {
                            multiplier = tier switch
                            {
                                TDEE.TIER_1 => 0.8,
                                TDEE.TIER_2 => 0.75,
                                TDEE.TIER_3 => 0.7,
                                _ => throw new ArgumentException("Invalid tier value"),
                            };
                            finishcalories = phase switch
                            {
                                TDEE.PHASE_1 => Math.Round(calories * (multiplier - 0.05)),
                                TDEE.PHASE_2 => Math.Round(calories * (multiplier - 0.1)),
                                _ => throw new ArgumentException("Invalid phase value"),
                            };
                        }
                        else
                        {
                            multiplier = tier switch
                            {
                                TDEE.TIER_1 => 0.8,
                                TDEE.TIER_2 => 0.75,
                                TDEE.TIER_3 => 0.7,
                                _ => throw new ArgumentException("Invalid tier value"),
                            };
                            finishcalories = phase switch
                            {
                                TDEE.PHASE_1 => Math.Round(calories * multiplier),
                                TDEE.PHASE_2 => Math.Round(calories * (multiplier - 0.05)),
                                TDEE.PHASE_3 => Math.Round(calories * (multiplier - 0.1)),
                                _ => throw new ArgumentException("Invalid phase value"),
                            };
                        }
                    }
                    break;

                case TDEE.GOAL_MAINTAIN:
                    finishcalories = Math.Round(calories);
                    break;

                case TDEE.GOAL_BUILD:
                    double buildMultiplier = tier switch
                    {
                        TDEE.TIER_1 => 1,
                        TDEE.TIER_2 => 1.05,
                        TDEE.TIER_3 => 1.2,
                        _ => throw new ArgumentException("Invalid tier value"),
                    };
                    finishcalories = phase switch
                    {
                        TDEE.PHASE_1 => Math.Round(calories * buildMultiplier),
                        TDEE.PHASE_2 => Math.Round(calories * (buildMultiplier + 0.05)),
                        TDEE.PHASE_3 => Math.Round(calories * (buildMultiplier + 0.15)),
                        _ => throw new ArgumentException("Invalid phase value"),
                    };
                    break;

                case TDEE.GOAL_REVERSE:
                    if (phase == ArdPhases.ardPhase[0])
                        finishcalories = previousCalories + 300;
                    else
                    {
                        double reverseMultiplier = tier switch
                        {
                            TDEE.TIER_CONSERVATIVE => 1.07,
                            TDEE.TIER_AGGRESSIVE => 1.2,
                            _ => throw new ArgumentException("Invalid tier value"),
                        };
                        finishcalories = Math.Round(previousCalories + (calories * (reverseMultiplier - 1)));
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid goal value");
            }

            return finishcalories;
        }

        public void VerifyNutritionData(DB.AspNetUsers userData, string goal, string tier, string SKU, string gender, double expectedCalories, string diet, double calories, string phase, string valuMoreThan2Kg, double previousCalories, int weekNumber)
        {
            var protein = CalculateProtein(userData, goal, tier, gender, SKU);
            var fat = CalculateFats(userData, diet, phase, goal);
            var carbs = CalculateCarbs(expectedCalories, protein, fat);
            CalculateCalories(userData, goal, tier, SKU, calories, expectedCalories, phase, valuMoreThan2Kg, previousCalories, carbs, protein, fat, weekNumber);
        }

        private int CalculateProtein(DB.AspNetUsers userData, string goal, string tier, string gender, string SKU)
        {
            double weight = (double)userData.Weight;
            double bodyFat = (double)userData.Bodyfat;
            double protein = goal switch
            {
                TDEE.GOAL_BUILD or
                TDEE.GOAL_MAINTAIN or
                TDEE.GOAL_REVERSE => (int)weight * 2,
                _ => gender == "Female" && bodyFat >= 30 || gender == "Male" && bodyFat >= 25
                    ? (int)(weight * 1.6)
                    : SKU == MembershipsSKU.MEMBERSHIP_SKU[0]
                        ? (int)(weight * 2)
                        : tier switch
                        {
                            TDEE.TIER_1 or TDEE.TIER_2 => (int)(weight * 2),
                            TDEE.TIER_CONSERVATIVE or TDEE.TIER_3 => (int)Math.Round(weight * 2.2, 0, MidpointRounding.AwayFromZero),
                            _ => throw new ArgumentException("Invalid tier value"),
                        }
            };


            double actualProtein = double.Parse(valueOfProteinCarbsFat[0].Text.Trim('g'));

            Assert.IsTrue(Math.Abs(protein - actualProtein) <= 10, $"Expected protein: {protein} but was: {actualProtein}");

            return (int)Math.Round(protein, 0, MidpointRounding.AwayFromZero);
        }

        private double CalculateFats(DB.AspNetUsers userData, string diet, string phase, string goal)
        {
            double weight = (double)userData.Weight;
            double fat = 0;

            switch (goal)
            {
                case TDEE.GOAL_CUT:
                    switch (phase)
                    {
                        case TDEE.PHASE_1:
                        case TDEE.PHASE_2:
                            fat = diet switch
                            {
                                TDEE.DIET_1 => (int)(weight * 0.8),
                                TDEE.DIET_2 => (int)(weight * 1),
                                TDEE.DIET_3 => (double)(int)(weight * 1.3),
                                _ => throw new ArgumentException($"Invalid diet value: {diet}"),
                            };
                            break;
                        case TDEE.PHASE_3:
                            fat = diet switch
                            {
                                TDEE.DIET_1 => (int)(weight * 0.7),
                                TDEE.DIET_2 => (int)(weight * 0.8),
                                TDEE.DIET_3 => (double)(int)(weight * 1),
                                _ => throw new ArgumentException($"Invalid diet value: {diet}"),
                            };
                            break;
                        default:
                            fat = diet switch
                            {
                                TDEE.DIET_1 => (int)(weight * 0.8),
                                TDEE.DIET_2 => (int)(weight * 1),
                                TDEE.DIET_3 => (double)(int)(weight * 1.3),
                                _ => throw new ArgumentException($"Invalid diet value: {diet}"),
                            };
                            break;
                    }
                    break;
                case TDEE.GOAL_MAINTAIN:
                case TDEE.GOAL_BUILD:
                case TDEE.GOAL_REVERSE:
                    switch (diet)
                    {
                        case TDEE.DIET_1:
                            fat = weight * 0.8;
                            fat = (int)fat;
                            break;
                        case TDEE.DIET_2:
                            fat = weight * 1;
                            fat = (int)fat;
                            break;
                        case TDEE.DIET_3:
                            fat = weight * 1.3;
                            fat = (int)fat;
                            break;
                        default:
                            throw new ArgumentException($"Invalid diet value: {diet}");
                    }

                    break;
            }
            

            double actualFat = Math.Round(fat, 0, MidpointRounding.AwayFromZero);
            Assert.IsTrue(Math.Abs(fat - actualFat) <= 5, $"Expected fat: {fat} but was: {actualFat}");

            return actualFat;
        }
        private double CalculateCarbs(double expectedCalories, double protein, double fat)
        {
            var remainderCarbs = expectedCalories - protein * 4 - fat * 9;
            var carbs = remainderCarbs / 4;
            carbs = Math.Round(carbs, 0, MidpointRounding.AwayFromZero);

            double actualCarbs = carbs;
            Assert.IsTrue(Math.Abs(carbs - actualCarbs) <= 5, $"Expected carbs: {carbs} but was: {actualCarbs}");

            return actualCarbs;
        }
        private void CalculateCalories(DB.AspNetUsers userData, string goal, string tier, string SKU, double calories, double expectedCalories, string phase, string valuMoreThan2Kg, double previousCalories, double carbs, double protein, double fat, int weekNumber)
        {
            var weight = ((double)userData.Weight);
            var getFat = valueOfProteinCarbsFat[2].Text.Trim(new char[] { 'g' });
            var actualFat = double.Parse(getFat);
            if (carbs < 50)
            {
                carbs = 50;                
                var remainder = expectedCalories - (carbs * 4) - (protein * 4);
                fat = Math.Round(remainder/9, 0, MidpointRounding.AwayFromZero); ;
                if(fat < weight * 0.6)
                {
                    fat = weight * 0.6;
                    fat = Math.Round(fat, 0, MidpointRounding.AwayFromZero);
                }

                calories = (carbs * 4) + (protein * 4) + (fat * 9);

                var actualCalories = double.Parse(valueCalories.Text);
                Assert.IsTrue(Math.Abs(calories - actualCalories) <= 25, $"Expected calories: {calories} but was: {actualCalories}");
                Assert.IsTrue(Math.Abs(fat - actualFat) <= 5, $"Expected fat: {fat} but was: {actualFat}");
            }
            else if (carbs >= 50)
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
                            if (weekNumber == 8)
                            {
                                switch (tier)
                                {
                                    case TDEE.TIER_1:
                                        finishcalories = phase switch
                                        {
                                            TDEE.PHASE_1 => Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero),
                                            TDEE.PHASE_2 => Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero),
                                            _ => throw new ArgumentException($"Invalid calories value: {calories}")
                                        };
                                        break;
                                    case TDEE.TIER_2:
                                        finishcalories = phase switch
                                        {
                                            TDEE.PHASE_1 => Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero),
                                            TDEE.PHASE_2 => Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero),
                                            _ => throw new ArgumentException($"Invalid calories value: {calories}")
                                        };
                                        break;
                                    case TDEE.TIER_3:
                                        finishcalories = phase switch
                                        {
                                            TDEE.PHASE_1 => Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero),
                                            TDEE.PHASE_2 => Math.Round((calories * 0.6), 0, MidpointRounding.AwayFromZero),
                                            _ => throw new ArgumentException($"Invalid calories value: {calories}")
                                        };
                                        break;
                                }
                            }
                            else
                            {
                                switch (tier)
                                {
                                    case TDEE.TIER_1:
                                        finishcalories = phase switch
                                        {
                                            TDEE.PHASE_1 => Math.Round((calories * 0.8), 0, MidpointRounding.AwayFromZero),
                                            TDEE.PHASE_2 => Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero),
                                            TDEE.PHASE_3 => Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero),
                                            _ => throw new ArgumentException($"Invalid calories value: {calories}"),
                                        };
                                        break;
                                    case TDEE.TIER_2:
                                        finishcalories = phase switch
                                        {
                                            TDEE.PHASE_1 => Math.Round((calories * 0.75), 0, MidpointRounding.AwayFromZero),
                                            TDEE.PHASE_2 => Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero),
                                            TDEE.PHASE_3 => Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero),
                                            _ => throw new ArgumentException($"Invalid calories value: {calories}"),
                                        };
                                        break;
                                    case TDEE.TIER_3:
                                        finishcalories = phase switch
                                        {
                                            TDEE.PHASE_1 => Math.Round((calories * 0.7), 0, MidpointRounding.AwayFromZero),
                                            TDEE.PHASE_2 => Math.Round((calories * 0.65), 0, MidpointRounding.AwayFromZero),
                                            TDEE.PHASE_3 => Math.Round((calories * 0.6), 0, MidpointRounding.AwayFromZero),
                                            _ => throw new ArgumentException($"Invalid calories value: {calories}"),
                                        };
                                        break;
                                }
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
                                finishcalories = phase switch
                                {
                                    TDEE.PHASE_1 => Math.Round((calories * 1), 0, MidpointRounding.AwayFromZero),
                                    _ => throw new ArgumentException($"Invalid calories value: {calories}"),
                                };
                                break;                                
                            case TDEE.TIER_2:
                                finishcalories = phase switch
                                {
                                    TDEE.PHASE_1 => Math.Round((calories * 1.05), 0, MidpointRounding.AwayFromZero),
                                    TDEE.PHASE_2 => Math.Round((calories * 1.1), 0, MidpointRounding.AwayFromZero),
                                    TDEE.PHASE_3 => Math.Round((calories * 1.15), 0, MidpointRounding.AwayFromZero),
                                    _ => throw new ArgumentException($"Invalid calories value: {calories}"),
                                };
                                break;
                            case TDEE.TIER_3:
                                finishcalories = phase switch
                                {
                                    TDEE.PHASE_1 => Math.Round((calories * 1.2), 0, MidpointRounding.AwayFromZero),
                                    TDEE.PHASE_2 => Math.Round((calories * 1.25), 0, MidpointRounding.AwayFromZero),
                                    TDEE.PHASE_3 => Math.Round((calories * 1.35), 0, MidpointRounding.AwayFromZero),
                                    _ => throw new ArgumentException($"Invalid calories value: {calories}"),
                                };
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
                Assert.IsTrue(Math.Abs(finishcalories - actualCalories) <= 10, $"Expected calories: {finishcalories} but was: {actualCalories}");
                Assert.IsTrue(Math.Abs(carbs - actualCarbs) < 5, $"Expected carbs: {carbs} but was: {actualCarbs}");
                Assert.IsTrue(Math.Abs(fat - actualFat) < 5, $"Expected fat: {fat} but was: {actualFat}");
                Console.WriteLine($"Expected calories are \"{finishcalories}\"");
                Console.WriteLine($"Expected carbs are \"{carbs}\"");
                Console.WriteLine($"Expected protein is \"{protein}\"");
                Console.WriteLine($"Expected fat is \"{fat}\"");
            }
        }
    }
}
