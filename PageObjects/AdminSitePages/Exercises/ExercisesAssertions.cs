using NUnit.Allure.Steps;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MCMAutomation.PageObjects
{
    public partial class ExercisesAdmin
    {
        [AllureStep("Verify exercise is created")]

        public ExercisesAdmin VerifyExerciseIsCreated(string exercise)
        {
            WaitUntil.WaitSomeInterval(2500);
            InputBox.Element(fieldSearchExercise, 5, exercise);

            var exerciseList = nameExerciseTitle.Where(x => x.Displayed).ToList();

            Assert.AreEqual(exercise, TextBox.GetText(exerciseList[0]));

            return this;
        }

        [AllureStep("Verify exercise is removed")]

        public ExercisesAdmin VerifyExerciseIsRemoved(string exercise)
        {
            WaitUntil.WaitSomeInterval(500);
            WaitUntil.CustomElevemtIsVisible(nameExerciseTitle.Where(x => x.Displayed).Last());

            Assert.Throws<NoSuchElementException>(() => Browser._Driver.FindElement(By.XPath($".//div[@class='table-item-row']/p[contains(text(), '{exercise}')]")));
            

            return this;
        }

        public static List<string> GetExercisesList()
        {
            WaitUntil.CustomElevemtIsVisible(nameExerciseTitle.Where(x => x.Displayed).Last());

            var exerciseList = new List<string>();

            var list = nameExerciseTitle.Where(x => x.Enabled).ToList();

            foreach (var exercise in list)
            {
                exerciseList.Add(exercise.Text);
            }

            return exerciseList;
        }

    }
}