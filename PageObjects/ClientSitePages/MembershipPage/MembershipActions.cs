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
            WaitUntil.VisibilityOfAllElementsLocatedBy(_selectProgramBtn, 20);
            selectProgramBtn.Click();

            WaitUntil.VisibilityOfAllElementsLocatedBy(_popupYesBtn, 20);
            popupYesBtn.Click();

            return this;
        }

        [AllureStep("Open membership")]
        public MembershipUser OpenMembership()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_programTitle, 20);
            programTitle.Click();

            return this;
        }

        [AllureStep("Open workout")]
        public MembershipUser OpenWorkoutsAndEnterWeight()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_workoutBtn, 20);
            workoutBtn.Click();


            WaitUntil.VisibilityOfAllElementsLocatedBy(_weightInput, 20);
            IReadOnlyCollection<IWebElement> weightInputsList = Browser._Driver.FindElements(_weightInput);
            IReadOnlyCollection<IWebElement> repsInputsList = Browser._Driver.FindElements(_repsInput);
            IReadOnlyCollection<IWebElement> checkboxesInputsList = Browser._Driver.FindElements(_checkboxInput);
            IReadOnlyCollection<IWebElement> openNotesBtnList = Browser._Driver.FindElements(_openNotesBtn);

            foreach (var weightInput in weightInputsList)
            {
                weightInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                weightInput.SendKeys(RandomHelper.RandomNumber(150).ToString());
            }
            foreach(var repsInput in repsInputsList)
            {
                repsInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                repsInput.SendKeys(RandomHelper.RandomNumber(10).ToString());
            }
            foreach (var checkboxesInput in checkboxesInputsList)
            {
                checkboxesInput.Click();
            }
            foreach (var openNotesBtn in openNotesBtnList)
            {
                openNotesBtn.Click();
                WaitUntil.VisibilityOfAllElementsLocatedBy(_notesInput, 20);
                notesInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                notesInput.SendKeys(RandomHelper.RandomText(25));
                saveNotesBtn.Click();
            }
            completeWorkoutBtn.Click();

            WaitUntil.VisibilityOfAllElementsLocatedBy(_workoutBtn, 20);
            IReadOnlyCollection<IWebElement> workoutsList = Browser._Driver.FindElements(_workoutBtn);
            int i = 0;
            while(workoutsList.Count >= 1)
            {
                ++i;
                
                if (i % 2 != 0)
                {
                    i = i + 2;
                    string workoutBtn = "//div[@class='program-overview_days '][" + i + "]//div[@class='program-overview_days-btn ']";
                    WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(workoutBtn));
                    IWebElement WorkoutsBtn = Browser._Driver.FindElement(By.XPath(workoutBtn));
                    WorkoutsBtn.Click();

                    WaitUntil.VisibilityOfAllElementsLocatedBy(_weightInput, 20);
                    IReadOnlyCollection<IWebElement> weightInputList = Browser._Driver.FindElements(_weightInput);
                    IReadOnlyCollection<IWebElement> repsInputList = Browser._Driver.FindElements(_repsInput);
                    IReadOnlyCollection<IWebElement> checkboxInputsList = Browser._Driver.FindElements(_checkboxInput);
                    IReadOnlyCollection<IWebElement> openNoteBtnList = Browser._Driver.FindElements(_openNotesBtn);

                    foreach (var weightInput in weightInputList)
                    {
                        weightInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                        weightInput.SendKeys(RandomHelper.RandomNumber(150).ToString());
                    }
                    foreach (var repsInput in repsInputList)
                    {
                        repsInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                        repsInput.SendKeys(RandomHelper.RandomNumber(10).ToString());
                    }
                    foreach (var checkboxesInput in checkboxInputsList)
                    {
                        checkboxesInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                        checkboxesInput.Click();
                    }
                    foreach (var openNotesBtn in openNoteBtnList)
                    {
                        openNotesBtn.Click();
                        WaitUntil.VisibilityOfAllElementsLocatedBy(_notesInput, 20);
                        notesInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                        notesInput.SendKeys(RandomHelper.RandomText(25));
                        saveNotesBtn.Click();
                    }
                    completeWorkoutBtn.Click();
                    i = i - 1;
                }
                WaitUntil.WaitSomeInterval(3);
                workoutsList = Browser._Driver.FindElements(_workoutBtn);
            }
                
           
            return this;
        }

        [AllureStep("Select Week number")]
        public MembershipUser SelectWeekNumber()
        {
            int i = 0;
            int y = 0;
            while(i < 4)
            {
                ++i;
                WaitUntil.WaitSomeInterval(3);
                if (i == 1)
                {
                    WaitUntil.VisibilityOfAllElementsLocatedBy(_weekSelectorCbbx);
                    weekSelector.Click();
                    weekSelectorInputEx.SendKeys(Keys.ArrowDown + Keys.ArrowUp + Keys.Enter);
                    Pages.MembershipUser.OpenWorkoutsAndEnterWeight();
                }
                if (i == 2)
                {
                    weekSelector.Click();
                    while (y < i - 1)
                    {
                        ++y;
                        WaitUntil.VisibilityOfAllElementsLocatedBy(_weekSelectorCbbx);
                        weekSelectorInputEx.SendKeys(Keys.ArrowDown);
                    }
                    y = 0;
                    weekSelectorInputEx.SendKeys(Keys.Enter);

                    Pages.MembershipUser.OpenWorkoutsAndEnterWeight();
                }
                
                if (i == 3)
                {
                    weekSelector.Click();
                    while (y < i - 1)
                    {
                        ++y;
                        WaitUntil.VisibilityOfAllElementsLocatedBy(_weekSelectorCbbx);
                        weekSelectorInputEx.SendKeys(Keys.ArrowDown);
                    }
                    y = 0;
                    weekSelectorInputEx.SendKeys(Keys.Enter);

                    Pages.MembershipUser.OpenWorkoutsAndEnterWeight();
                }
                if (i == 4)
                {
                    weekSelector.Click();
                    while (y < i - 1)
                    {
                        ++y;
                        WaitUntil.VisibilityOfAllElementsLocatedBy(_weekSelectorCbbx);
                        weekSelectorInputEx.SendKeys(Keys.ArrowDown);
                    }
                    y = 0;
                    weekSelectorInputEx.SendKeys(Keys.Enter);

                    Pages.MembershipUser.OpenWorkoutsAndEnterWeight();
                }

            }
            
            return this;
        }

        [AllureStep("Select Week number")]
        public MembershipUser SelectPhase()
        {
            int i = 0;
            IReadOnlyCollection<IWebElement> phasesList = Browser._Driver.FindElements(_selectPhaseBtn);
            while(i < phasesList.Count)
            {
                ++i;
                
                string phase = "//div[@class='phase'][" + i + "]";

                WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(phase));
                IWebElement PhaseBtn = Browser._Driver.FindElement(By.XPath(phase));
                PhaseBtn.Click();
                weekSelectorInput.Click();
                weekSelectorInput.SendKeys(Keys.ArrowDown + Keys.ArrowUp + Keys.Enter);

                viewTrainingProgramBtn.Click();

                Pages.MembershipUser.SelectWeekNumber();

                Browser._Driver.Navigate().GoToUrl("https://mcmstaging-ui.azurewebsites.net/programs/training-program");
                
                Pages.PopUp.ClosePopUp();
                WaitUntil.WaitSomeInterval(3);

            }


            return this;
        }
    }
}
