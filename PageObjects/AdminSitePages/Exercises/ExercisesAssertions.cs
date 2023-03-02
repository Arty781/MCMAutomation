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
            var matchingExercise = nameExerciseTitle.FirstOrDefault(x => x.Text.Contains(exercise));
            Assert.IsNotNull(matchingExercise, $"Exercise '{exercise}' was not found.");
            Assert.AreEqual(exercise, matchingExercise.Text, $"Exercise title '{matchingExercise.Text}' does not match expected '{exercise}'.");
            return this;
        }

        [AllureStep("Verify exercise is removed")]

        public ExercisesAdmin VerifyExerciseIsRemoved(string exercise)
        {
            WaitUntil.WaitSomeInterval(2500);
            WaitUntil.WaitForElementToDisappear(nameExerciseTitle.FirstOrDefault(x => x.Text.Contains(exercise)));

            Assert.IsFalse(nameExerciseTitle.Any(x => x.Text.Contains(exercise)), $"Exercise '{exercise}' was found but should have been removed.");

            return this;
        }

        public List<string> GetExercisesList()
        {
            WaitUntil.WaitForElementToAppear(nameExerciseTitleElem);
            var exerciseList = nameExerciseTitle.Where(x => x.Enabled).Select(x => x.Text).ToList();
            return exerciseList;
        }

    }
}