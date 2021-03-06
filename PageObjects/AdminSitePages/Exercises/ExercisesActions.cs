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

        [AllureStep(" Click \"Create exercise\" button")]

        public ExercisesAdmin ClickCreateExerciseBtn()
        {
            Button.Click(btnAddExercise);
            
            return this;
        }

        [AllureStep("Enter Exercise data")]

        public ExercisesAdmin EnterExerciseData()
        {
            
            InputBox.Element(fieldExerciseName, 5, "Test Exercise" + DateTime.Now.ToString("yyyy-MM-d hh:mm:ss"));
            InputBox.Element(fieldExerciseUrl, 5, "https://player.vimeo.com/video/478282179");

            Button.Click(btnTempoStart[0]);


            return this;
        }

        [AllureStep("Add Related exercises")]
        public ExercisesAdmin AddRelatedExercises(string[] relatedExercisesList)
        {
            Button.Click(btnAddRelatedExercise);


            return this;
        }

        [AllureStep("Search Exercise")]

        public ExercisesAdmin SearchExercise (string exercise)
        {
            InputBox.Element(fieldSearchExercise, 5, exercise);

            return this;
        }

        [AllureStep("Edit Exercise")]

        public ExercisesAdmin ClickEditExercise()
        {
            var exercisesList = btnEditExercise.Where(x=>x.Enabled).ToList();

            foreach (IWebElement exercise in exercisesList)
            {
                Button.Click(exercise);
                
            }
            
            return this;
        }

        [AllureStep("Click Add related exercises button")]

        public ExercisesAdmin ClickAddRelatedExercisesBtn(int i)
        {
            int q = 0;
            while (q < i)
            {
                q++;
                Button.Click(btnAddRelatedExercise);
            }
            

            return this;
        }

        [AllureStep("Add related exercises")]

        public ExercisesAdmin AddRelatedExercises(int i, string exercise)
        {
            int q = 0;
            while (q < i)
            {
                q++;
                Button.Click(fieldRelatedExercise[q]);
                InputBox.CbbxElement(fieldRelatedExercise[q],5, exercise);
            }

            return this;
        }

        [AllureStep("Remove Related Exercises")]

        public ExercisesAdmin RemoveRelatedExercises()
        {
            int q = 0;
            WaitUntil.CustomElevemtIsVisible(btnRemoveRelatedExeciseElem);
            var removeRelatedbtnList = btnRemoveRelatedExecise.Where(x=>x.Enabled).ToList();

            while (q < removeRelatedbtnList.Count)
            {
                q++;
                Button.Click(btnRemoveRelatedExecise[q]);
            }

            return this;
        }

        [AllureStep("Remove Exercise")]

        public ExercisesAdmin RemoveExercise(string exercise)
        {


            RemoveBtn.ClickRemoveExerciseBtn(exercise);
            Button.Click(btnConfirmationYes);
            

            return this;
        }
    }
}