using MCMAutomation.Helpers;
using MCMAutomation.PageObjects;
using NUnit.Allure.Steps;
using NUnit.Framework;
using OpenQA.Selenium;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.PageObjects.ClientSitePages
{
    public partial class MembershipUser
    {
        [AllureStep("Open memberships page")]
        public MembershipUser OpenMembershipPage()
        {
            Browser.GoToUrl("https://mcmstaging-ui.azurewebsites.net/programs/all");
            Pages.CommonPages.PopUp.ClosePopUp();
            return this;
        }

        [AllureStep("Activate membership")]
        public MembershipUser ConfirmMembershipActivation()
        {
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
        public MembershipUser SelectPhaseAndWeek(int phaseNum, int weekNum, out int countWorkouts)
        {
            Button.Click(selectPhaseBtn[phaseNum]);
            Button.Click(weekSelectorInput);
            Button.Click(listWeekNumber[weekNum]);
            Button.Click(viewTrainingProgramBtn);
            countWorkouts = Pages.WebPages.MembershipUser.GetWorkoutsCount();
            return this;
        }

        public int GetWeekNumber()
        {
            WaitUntil.WaitForElementToAppear(weekSelectorCbbx, 15);
            Button.Click(weekSelectorInputEx);
            WaitUntil.WaitForElementToAppear(listWeekNumber.Last(), 15);
            Button.Click(weekSelectorInputEx);
            return listWeekNumber.Count;
        }

        #endregion

        #region Select week number

        [AllureStep("Select Week number")]
        public MembershipUser SelectWeekNumber(int weekNum)
        {
            
            WaitUntil.WaitForElementToAppear(weekSelectorCbbx, 15);
            weekSelectorInputEx.Click();
            Button.Click(listWeekNumber[weekNum]);
            WaitUntil.WaitSomeInterval(2500);


            return this;
        }

        #endregion

        #region Workouts page

        public MembershipUser AddWeightAndEnterNotes(int countWorkouts)
        {
            for (int j = 0; j < countWorkouts; j++)
            {
                Pages.WebPages.MembershipUser
                    .OpenWorkout()
                    .AddWeight(out List<string> addedWeightList)
                    .EnterNotes()
                    .ClickCompleteWorkoutBtn()
                    .OpenCompletedWorkout()
                    .VerifyAddedWeights(addedWeightList)
                    .ClickBackBtn();
            }
            return this;
        }

        [AllureStep("Open workout")]
        public MembershipUser OpenWorkout()
        {
            WaitUntil.WaitForElementToAppear(textDayTitleElem);
            WaitUntil.WaitSomeInterval(500);
            WaitUntil.WaitForElementToAppear(workoutBtn.First());
            Button.Click(workoutBtn.First());
            WaitUntil.WaitSomeInterval(500);

            return this;
        }

        [AllureStep("Open workout")]
        public MembershipUser OpenCompletedWorkout()
        {
            WaitUntil.WaitForElementToAppear(textDayTitle[0]);
            WaitUntil.WaitSomeInterval(1500);
            WaitUntil.WaitForElementToAppear(btnCompletedWorkoutsElem);
            Button.Click(btnCompletedWorkouts.LastOrDefault());

            return this;
        }

        [AllureStep("Click Complete Workout Btn")]
        public MembershipUser ClickCompleteWorkoutBtn()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Button.Click(completeWorkoutBtn);
            WaitUntil.WaitForElementToAppear(weekSelectorCbbx, 20);

            stopwatch.Stop();
            long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Request evaluation time: " + elapsedMilliseconds + " ms");

            return this;
        }



        #endregion

        #region Enter Data For Exercises

        [AllureStep("Add Weight")]
        public MembershipUser AddWeight(out List<string> addedWeightList)
        {
            WaitUntil.WaitForElementToDisappear(Pages.CommonPages.Common.loader, 10);
            WaitUntil.WaitForElementToAppear(weightInput.FirstOrDefault());
            int i = 0;
            IList<IWebElement> weightList = weightInput.Where(x => x.Displayed).ToList();
            
            foreach (var weight in weightList)
            {
                if (i == 0)
                {
                    InputBox.ElementCtrlA(weight, 5, RandomHelper.RandomNumFromOne(150).ToString());
                }
                else
                {
                    weight.SendKeys(RandomHelper.RandomNumFromOne(150).ToString());
                }
                i++;
            }

            addedWeightList = Pages.WebPages.MembershipUser.GetWeightData();

            return this;
        }

        [AllureStep("Enter Reps")]
        public MembershipUser EnterReps()
        {
            WaitUntil.WaitSomeInterval(2);
            WaitUntil.WaitForElementToAppear(repsInputElem);

            var repsList = repsInput.Where(x => x.Displayed).ToList();
            foreach (var reps in repsList)
            {
                WaitUntil.WaitSomeInterval(150);
                InputBox.ElementCtrlA(reps, 10, RandomHelper.RandomNumber(10));
            }

            return this;
        }

        [AllureStep("Mark Checkboxes")]
        public MembershipUser MarkCheckboxes()
        {
            WaitUntil.WaitSomeInterval(1000);
            WaitUntil.WaitForElementToAppear(checkboxInput.Last());
            var checkboxesList = checkboxInput.Where(x => x.Enabled).ToList();
            foreach (var checkbox in checkboxesList)
            {
                WaitUntil.WaitSomeInterval(150);
                checkbox.Click();
            }

            return this;
        }

        [AllureStep("Enter Notes")]
        public MembershipUser EnterNotes()
        {
            WaitUntil.WaitSomeInterval(1000);
            if (openNotesBtnelem.Displayed == true) { 
                
                WaitUntil.WaitForElementToAppear(openNotesBtnelem);
                    WaitUntil.WaitSomeInterval(1000);
                    var notesList = openNotesBtn.Where(x => x.Enabled).ToList();
                    for (int i = 0; i < notesList.Count; i++)
                    {
                        notesList[i].Click();
                        InputBox.ElementCtrlA(notesInputelem, 10, Lorem.ParagraphByChars(240));
                        Button.Click(saveNotesBtnElem);
                    }
            }
            else if (editNotesBtnelem.Displayed == true)
            {
                WaitUntil.WaitSomeInterval(1000);
                WaitUntil.WaitForElementToAppear(editNotesBtnelem);
                var notesList = editNotesBtn.Where(x => x.Enabled).ToList();
                for (int i = 0; i < notesList.Count; i++)
                {
                    notesList[i].Click();
                    InputBox.ElementCtrlA(notesInputelem, 10, Lorem.ParagraphByChars(240));
                    Button.Click(saveNotesBtnElem);
                }

            }


            return this;
        }

        [AllureStep("Get Weight data")]
        public List<string> GetCompleteWeightData()
        {
            WaitUntil.WaitForElementToAppear(inputAddedWeight.Where(x => x.Enabled).LastOrDefault());
            List<string> list = new List<string>();
            var addedWeightList = inputAddedWeight.Where(x => x.Enabled).ToList();
            foreach (var weight in addedWeightList)
            {
                list.Add(weight.Text);
            }

            return list;
        }

        [AllureStep("Get Weight data")]
        public List<string> GetWeightData()
        {
            WaitUntil.WaitForElementToAppear(weightInput.Where(x => x.Enabled).LastOrDefault());
            List<string> list = new List<string>();
            var addedWeightList = weightInput.Where(x => x.Enabled).ToList();
            foreach (var weight in addedWeightList)
            {
                list.Add(weight.GetAttribute("value"));
            }

            return list;
        }

        [AllureStep("Click Complete Workout Btn")]
        public MembershipUser ClickBackBtn()
        {

            Button.Click(btnBack);
            WaitUntil.WaitForElementToAppear(weekSelectorCbbx, 20);

            return this;
        }

        #endregion


        #region Methods

        public void SelectPhaseAndWeekAndEnterWeight(int programsCount)
        {
            for (int i = 0; i < programsCount; i++)
            {
                int phaseNum = i;
                int weekNum = i;
                OpenMembership();
                SelectPhaseAndWeek(phaseNum, weekNum, out int weeksCount);
                SelectWeek(weeksCount);
                OpenMembershipPage();
            } 
        }

        private void SelectWeek(int weekNumber)
        {
            
            for (int q = 0; q < weekNumber; q++)
            {
                Pages.WebPages.MembershipUser
                    .SelectWeekNumber(q);
                int workoutsCount = Pages.WebPages.MembershipUser.GetWorkoutsCount();
                for (int j = 0; j < workoutsCount; j++)
                {
                    OpenWorkoutAndEnterWeight();
                }
            }
        }
        private void OpenWorkoutAndEnterWeight()
        {
            OpenWorkout();
            AddWeight(out List<string> addedWeightList);
            EnterNotes();
            ClickCompleteWorkoutBtn();
            OpenCompletedWorkout();
            VerifyAddedWeights(addedWeightList);
            ClickBackBtn();
        }


        #endregion





    }
}
