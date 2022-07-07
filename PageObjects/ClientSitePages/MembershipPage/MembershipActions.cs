using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
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
            
            Button.Click(programTitle);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase1()
        {


            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);

            
                Button.Click(selectPhaseBtn[0]);


                Button.Click(weekSelectorInput);
                weekSelectorInput.SendKeys(Keys.Enter);

                Button.Click(viewTrainingProgramBtn);




            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase2()
        {


            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);


            Button.Click(selectPhaseBtn[1]);


            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);

            Button.Click(viewTrainingProgramBtn);




            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase3()
        {


            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);


            Button.Click(selectPhaseBtn[2]);


            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);

            Button.Click(viewTrainingProgramBtn);




            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase4()
        {


            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);


            Button.Click(selectPhaseBtn[3]);


            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);

            Button.Click(viewTrainingProgramBtn);




            return this;
        }

        [AllureStep("Select Week number")]
        public MembershipUser SelectWeekNumber1()
        {
            WaitUntil.WaitSomeInterval(500);
            WaitUntil.CustomElevemtIsVisible(viewTrainingProgramBtn, 10);

            weekSelectorInputEx.SendKeys(Keys.ArrowDown+Keys.Enter);


            return this;
        }

        [AllureStep("Select Week number")]
        public MembershipUser SelectWeekNumber2()
        {
            
            WaitUntil.CustomElevemtIsVisible(weekSelectorCbbx);

            weekSelectorInputEx.SendKeys(Keys.ArrowDown + Keys.ArrowDown + Keys.Enter);
            WaitUntil.WaitSomeInterval(5000);


            return this;
        }

        [AllureStep("Select Week number")]
        public MembershipUser SelectWeekNumber3()
        {

            WaitUntil.CustomElevemtIsVisible(weekSelectorCbbx);

            weekSelectorInputEx.SendKeys(Keys.ArrowDown + Keys.ArrowDown + Keys.ArrowDown + Keys.Enter);
            WaitUntil.WaitSomeInterval(5000);


            return this;
        }

        [AllureStep("Select Week number")]
        public MembershipUser SelectWeekNumber4()
        {

            WaitUntil.CustomElevemtIsVisible(weekSelectorCbbx);

            weekSelectorInputEx.SendKeys(Keys.ArrowDown + Keys.ArrowDown + Keys.ArrowDown + Keys.ArrowDown + Keys.Enter);
            WaitUntil.WaitSomeInterval(5000);


            return this;
        }

        [AllureStep("Open workout")]
        public MembershipUser OpenWorkout()
        {
            WaitUntil.CustomElevemtIsVisible(textDayTitle[0]);
            WaitUntil.WaitSomeInterval(2500);
            WaitUntil.CustomElevemtIsVisible(workoutBtn[0]);
            Button.Click(workoutBtn[0]);

            return this;
        }

        [AllureStep("Open completed workout")]
        public MembershipUser OpenCompletedWorkouts()
        {

            Button.Click(btnCompletedWorkouts[0]);

            return this;
        }

        [AllureStep("Add Weight")]
        public MembershipUser AddWeight()
        {
            WaitUntil.WaitSomeInterval(1000);
            WaitUntil.CustomElevemtIsVisible(weightInputElem);

            var weightList = weightInput.Where(x => x.Displayed).ToList();
            var repsList = repsInput.Where(x => x.Displayed).ToList();
            var checkboxesList = checkboxInput.Where(x => x.Enabled).ToList();
            for (int i=0; i<weightList.Count; i++)
            {
                WaitUntil.CustomElevemtIsVisible(weightList[i], 10);
                InputBox.Element(weightList[i], 5, RandomHelper.RandomNumber(150));
                WaitUntil.WaitSomeInterval(150);
            }
               
            return this;
        }

        //[AllureStep("Enter Reps")]
        //public MembershipUser EnterReps()
        //{
        //    WaitUntil.WaitSomeInterval(2);
        //    WaitUntil.CustomElevemtIsVisible(repsInputElem);

        //    var repsList = repsInput.Where(x => x.Displayed).ToList();
        //    foreach(var reps in repsList)
        //    {
        //        InputBox.Element(reps, 10, RandomHelper.RandomNumber(10));
        //    }
            
        //    return this;
        //}

        //[AllureStep("Mark Checkboxes")]
        //public MembershipUser MarkCheckboxes()
        //{
        //    WaitUntil.WaitSomeInterval(2);
        //    WaitUntil.CustomElevemtIsVisible(checkboxInputElem);

        //    var checkboxesList = checkboxInput.Where(x => x.Enabled).ToList();
        //    foreach( var checkbox in checkboxesList)
        //    {
        //        checkbox.Click();
        //    }
                
        //    return this;
        //}

        [AllureStep("Enter Notes")]
        public MembershipUser EnterNotes()
        {
            WaitUntil.WaitSomeInterval(1000);
            WaitUntil.CustomElevemtIsVisible(openNotesBtnelem);
            var notesList1 = openNotesBtn.Where(x => x.Enabled).ToList();
            for (int w = 0; w < notesList1.Count; w++)
            {
                WaitUntil.WaitSomeInterval(1000);
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
