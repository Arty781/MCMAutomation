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
    public partial class MembershipAdmin
    {

        [AllureStep("Click \"Create new membership\" btn")]

        public MembershipAdmin ClickCreateBtn()
        {
            WaitUntil.ElementIsVisible(_membershipCreateBtn);
            membershipCreateBtn.Click();
            return this;
        }

        [AllureStep("Enter membership data")]

        public MembershipAdmin EnterMembershipData()
        {
            WaitUntil.ElementIsVisible(_skuInput);
            skuInput.Clear();
            skuInput.SendKeys("CP_TEST_SUB");
            nameInput.Clear();
            nameInput.SendKeys("Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            descriptionInput.Clear();
            descriptionInput.SendKeys("Lorem ipsum dolor");

            WaitUntil.WaitSomeInterval(2);
            startDateInput.Click();
            startDateInput.SendKeys(DateTime.Now.ToString("yyyy-MM-d") + Keys.Enter);

            WaitUntil.WaitSomeInterval(2);
            endDateInput.Click();
            endDateInput.SendKeys(DateTime.Now.AddMonths(2).ToString("yyyy-MM-d") + Keys.Enter);
            genderBothToggle.Click();
            subscriptionToggleType.Click();
            priceInput.Clear();
            priceInput.SendKeys("99");
            urlInput.Clear();
            urlInput.SendKeys(Endpoints.websiteHost);
            availableForPurchaseCheckbox.Click();

            return this;
        }



        [AllureStep("Search Membership")]
        public MembershipAdmin SearchMembership(string membershipName)
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipTitle, 90);
            membershipSearchInput.Clear();
            membershipSearchInput.SendKeys(membershipName);

            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipTitle, 60);
            membershipSearchBtn.Click();

            return this;
        }


        [AllureStep("Click \"Delete\" button")]
        public MembershipAdmin ClickDeleteBtn()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipDeleteBtn);
            membershipDeleteBtn.Click();

            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipConfirmYesBtn);
            membershipConfirmYesBtn.Click();

            return this;
        }

        [AllureStep("Click \"Add programs\" button")]
        public MembershipAdmin ClickAddProgramsBtn()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipAddProgramsBtn);
            membershipAddProgramsBtn.Click();

            return this;
        }

        [AllureStep("Enter program data")]
        public MembershipAdmin CreatePrograms()
        {
            int i = 0;
            while (i < 5)
            {
                ++i;
                WaitUntil.VisibilityOfAllElementsLocatedBy(_addProgramBtn);
                addProgramBtn.Click();

                WaitUntil.VisibilityOfAllElementsLocatedBy(_programNameInput, 60);

                programNameInput.Clear();
                programNameInput.SendKeys("Phase " + DateTime.Now.ToString("hh-mm-ss"));
                programNumOfWeeksInput.Clear();
                programNumOfWeeksInput.SendKeys("4");
                programStepsInput.Clear();
                programStepsInput.SendKeys("10000");
                programAvailableDateInput.SendKeys(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                programExpiryDateInput.SendKeys(DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);
                programUploadFileInput.SendKeys(Browser.RootPath() + UploadedImages.PhaseImg1);

                Pages.Common.ClickSaveBtn();
            }
            


            return this;
        }

        [AllureStep("Create Workout for first program")]
        public MembershipAdmin DefineProgramsList()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_programAddWorkoutsBtn, 30);

            IReadOnlyCollection<IWebElement> programList = Browser._Driver.FindElements(_programAddWorkoutsBtn);

            List<string> urlList = new List<string>();
            for (int items = 1; items < programList.Count; items++)
            {
                string addBtn = "//div[@class='table-item'][" + items + "]//div[@class='membership-item_add add-workout']";
                WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(addBtn));
                IWebElement AddWorkoutsBtn = Browser._Driver.FindElement(By.XPath(addBtn));
                AddWorkoutsBtn.Click();

                urlList.Add(Browser._Driver.Url);

                WaitUntil.WaitSomeInterval(2);
                backBtn.Click();
            }
            return this;
        }



            [AllureStep("Create Workout for first program")]
        public MembershipAdmin CreateWorkoutsForFirstProgram()
        {

            WaitUntil.VisibilityOfAllElementsLocatedBy(_programAddWorkoutsBtn, 30);
            int i = 0;
            IReadOnlyCollection<IWebElement> programList = Browser._Driver.FindElements(_programAddWorkoutsBtn);
            foreach (var program in programList)
            {
                ++i;
                string addBtn = "//div[@class='table-item'][" + i + "]//div[@class='membership-item_add add-workout']";
                WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(addBtn));
                IWebElement AddWorkoutsBtn = Browser._Driver.FindElement(By.XPath(addBtn));
                AddWorkoutsBtn.Click();

                WaitUntil.VisibilityOfAllElementsLocatedBy(_addWorkoutBtn, 60);
                addWorkoutBtn.Click();
                workoutNameInput.SendKeys("Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                weekDayBtn.Click();
                weekDayBtn.SendKeys(Keys.ArrowDown + Keys.ArrowUp + Keys.Enter);
            }
            programAddWorkoutsBtn.Click();

            



            return this;
        }

        [AllureStep("Click \"Add exercises\" btn")]
        public MembershipAdmin ClickAddExerciseBtn()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_addExercisesBtn, 60);
            addExercisesBtn.Click();

            return this;
        }

            [AllureStep("Add exercises")]
        public MembershipAdmin AddExercises()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_addExerciseBtn, 90);
            addExerciseBtn.Click();

            int x = 0;
            while (x<15)
            {
                ++x;
                
                
                if (x < 5)
                {
                    seriesExerciseInput.Clear();
                    seriesExerciseInput.SendKeys("A" + x);
                }
                if(x > 4 && x < 9)
                {
                    seriesExerciseInput.Clear();
                    seriesExerciseInput.SendKeys("B" + x);
                }
                if (x > 8 && x < 12)
                {
                    seriesExerciseInput.Clear();
                    seriesExerciseInput.SendKeys("C" + x);
                }
                if (x > 11 && x <= 15)
                {
                    seriesExerciseInput.Clear();
                    seriesExerciseInput.SendKeys("E" + x);
                }
                exercisesCbbx.SendKeys(Exersises.exercise[x] + Keys.Enter);
                setsExerciseInput.SendKeys("4");
                repsExerciseInput.SendKeys("4,4,4,5");
                restExerciseInput.SendKeys("60");
                tempoExerciseInput.SendKeys("2010");
                notesExerciseInput.SendKeys(Exersises.exercise[x]);

                WaitUntil.WaitSomeInterval(1);
                Pages.Common.saveBtn.Click();
            }
            
            return this;
        }

        [AllureStep("Add user")]
        public MembershipAdmin AddUserToMembership()
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipAddUserBtn, 60);
            membershipAddUserBtn.Click();

            WaitUntil.VisibilityOfAllElementsLocatedBy(_addUserBtn, 60);
            userCbbx.SendKeys("qatester92311@xitroo.com" + Keys.Enter);
            addUserBtn.Click();


            return this;
        }




    }
}
