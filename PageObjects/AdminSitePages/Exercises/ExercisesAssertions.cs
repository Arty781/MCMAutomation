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
            var list = nameExerciseTitle.Where(x=>x.Text.Contains(exercise)).First();
            Assert.AreEqual(exercise, list.Text);

            return this;
        }

        [AllureStep("Verify exercise is removed")]

        public ExercisesAdmin VerifyExerciseIsRemoved(string exercise)
        {
            WaitUntil.WaitSomeInterval(2500);
            WaitUntil.CustomElevemtIsVisible(nameExerciseTitle.Where(x => x.Displayed).First());
             

            Assert.Throws<NoSuchElementException>(() => Browser._Driver.FindElement(By.XPath($".//div[@class='table-item-row']/p[contains(text(), '{exercise}')]")));
            

            return this;
        }

        public List<string> GetExercisesList()
        {
            WaitUntil.CustomElevemtIsVisible(nameExerciseTitleElem);

            var exerciseList = new List<string>();

            var list = nameExerciseTitle.Where(x => x.Enabled).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                string exerciseName = list[i].Text;
                exerciseList.Add(exerciseName);
            }

            return exerciseList;
        }

    }
}