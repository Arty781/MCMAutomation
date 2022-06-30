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

        public ExercisesAdmin VerifyExerciseIsRemoved()
        {
            WaitUntil.WaitSomeInterval(5000);
            string[] status = AppDbContext.GetExerciseStatus();
            Assert.That("True" == status[0]);

            return this;
        }

    }
}