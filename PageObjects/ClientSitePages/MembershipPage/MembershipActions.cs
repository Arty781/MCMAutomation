using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipUser
    {
        [AllureStep("Activate membership")]
        public MembershipUser ActivateMembership()
        {
            
            Button.Click(selectProgramBtn);

            Button.Click(popupYesBtn);

            return this;
        }

        [AllureStep("Open membership")]
        public MembershipUser OpenMembership()
        {
            
            Button.Click(programTitle, 20);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase()
        {


            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);

            
                Button.Click(selectPhaseBtn[1]);


                Button.Click(weekSelectorInput);
                weekSelectorInput.SendKeys(Keys.Enter);

                Button.Click(viewTrainingProgramBtn);




            return this;
        }

        [AllureStep("Select Week number")]
        public MembershipUser SelectWeekNumber()
        {
            WaitUntil.WaitSomeInterval(2);
            WaitUntil.CustomElevemtIsVisible(weekSelectorCbbx);

            weekSelectorInputEx.SendKeys(Keys.ArrowDown+Keys.Enter);


            return this;
        }

        [AllureStep("Open workout")]
        public MembershipUser OpenWorkouts()
        {
           
            Button.Click(workoutBtn[0]);

            return this;
        }

        [AllureStep("Enter Weight")]
        public MembershipUser EnterWeight()
        {
            WaitUntil.WaitSomeInterval(2);
            WaitUntil.CustomElevemtIsVisible(weightInputElem);
            var weightList1 = weightInput.Where(x => x.Displayed).ToList();
            for (int w = 0; w < weightList1.Count; w++)
            {

                WaitUntil.CustomElevemtIsVisible(weightInputElem);
                var weightList = weightInput.Where(x => x.Displayed).ToList();
                InputBox.Element(weightList[w], 10, RandomHelper.RandomNumber(150));
            }


            return this;
        }

        [AllureStep("Enter Reps")]
        public MembershipUser EnterReps()
        {
            WaitUntil.WaitSomeInterval(2);
            WaitUntil.CustomElevemtIsVisible(repsInputElem);
            var repsList1 = repsInput.Where(x => x.Displayed).ToList();
            for (int w = 0; w < repsList1.Count; w++)
            {
                
                WaitUntil.CustomElevemtIsVisible(repsInputElem);
                var repsList = repsInput.Where(x => x.Displayed).ToList();
                InputBox.Element(repsList[w], 10, RandomHelper.RandomNumber(10));
            }


            return this;
        }

        [AllureStep("Mark Checkboxes")]
        public MembershipUser MarkCheckboxes()
        {
            WaitUntil.WaitSomeInterval(2);
            WaitUntil.CustomElevemtIsVisible(checkboxInputElem);
            var checkboxesList1 = checkboxInput.Where(x => x.Enabled).ToList();
            for (int w = 0; w < checkboxesList1.Count; w++)
            {
                
                WaitUntil.CustomElevemtIsVisible(checkboxInputElem);
                var checkboxesList = checkboxInput.Where(x => x.Enabled).ToList();
                checkboxesList[w].Click();
            }

            return this;
        }

        [AllureStep("Enter Notes")]
        public MembershipUser EnterNotes()
        {
            WaitUntil.WaitSomeInterval(2);
            WaitUntil.CustomElevemtIsVisible(openNotesBtnelem);
            var notesList1 = openNotesBtn.Where(x => x.Enabled).ToList();
            for (int w = 0; w < notesList1.Count; w++)
            {
                WaitUntil.WaitSomeInterval(2);
                WaitUntil.CustomElevemtIsVisible(openNotesBtnelem);
                var notesList = openNotesBtn.Where(x => x.Enabled).ToList();
                openNotesBtnelem.Click();
                InputBox.Element(notesInputelem, 10, RandomHelper.RandomText(25));
                Button.Click(saveNotesBtnElem);
            }


            return this;
        }

        [AllureStep("Click Complete Workout Btn")]
        public MembershipUser ClickCompleteWorkoutBtn()
        {
           
            Button.Click(completeWorkoutBtn);
            WaitUntil.CustomElevemtIsVisible(weekSelectorCbbx, 20);

            return this;
        }


    }
}
