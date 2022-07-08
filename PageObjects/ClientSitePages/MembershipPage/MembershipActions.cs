using MCMAutomation.Helpers;
using NUnit.Allure.Steps;
using NUnit.Framework;
using OpenQA.Selenium;
using RimuTec.Faker;
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

        #region Select phase

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

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase5()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[4]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase6()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[5]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase7()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[6]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase8()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[7]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase9()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[8]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase10()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[9]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase11()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[10]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase12()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[11]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase13()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[12]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase14()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[13]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase15()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[14]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase16()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[15]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase17()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[16]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase18()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[17]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase19()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[18]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase20()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[19]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase21()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[20]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase22()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[21]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase23()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[22]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase24()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[23]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase25()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[24]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase26()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[25]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase27()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[26]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }


        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase28()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[27]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        [AllureStep("Select Phase")]
        public MembershipUser SelectPhase29()
        {

            WaitUntil.CustomElevemtIsVisible(selectPhaseBtnElem, 20);
            Button.Click(selectPhaseBtn[28]);
            Button.Click(weekSelectorInput);
            weekSelectorInput.SendKeys(Keys.Enter);
            Button.Click(viewTrainingProgramBtn);

            return this;
        }

        #endregion

        #region Select week number

        [AllureStep("Select Week number")]
        public MembershipUser SelectWeekNumber1()
        {
            WaitUntil.WaitSomeInterval(500);
            WaitUntil.CustomElevemtIsVisible(titleModalWindow, 10);

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

        #endregion

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
            if (openNotesBtnelem.Displayed == true) { 
                
                WaitUntil.CustomElevemtIsVisible(openNotesBtnelem);
                    WaitUntil.WaitSomeInterval(1000);
                    var notesList = openNotesBtn.Where(x => x.Enabled).ToList();
                    for (int i = 0; i < notesList.Count; i++)
                    {
                        notesList[i].Click();
                        InputBox.Element(notesInputelem, 10, Lorem.Sentence());
                        Button.Click(saveNotesBtnElem);
                    }
            }
            else if (editNotesBtnelem.Displayed == true)
            {
                WaitUntil.WaitSomeInterval(1000);
                WaitUntil.CustomElevemtIsVisible(editNotesBtnelem);
                var notesList = editNotesBtn.Where(x => x.Enabled).ToList();
                for (int i = 0; i < notesList.Count; i++)
                {
                    notesList[i].Click();
                    InputBox.Element(notesInputelem, 10, Lorem.Sentence());
                    Button.Click(saveNotesBtnElem);
                }

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
