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

                if (i == 1)
                {
                    WaitUntil.VisibilityOfAllElementsLocatedBy(_addProgramBtn);
                    addProgramBtn.Click();

                    WaitUntil.VisibilityOfAllElementsLocatedBy(_programNameInput, 60);

                    programNameInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                    programNameInput.SendKeys(i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    programNumOfWeeksInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                    programNumOfWeeksInput.SendKeys("4");
                    programStepsInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                    programStepsInput.SendKeys("10000");
                    programAvailableDateInput.SendKeys(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    programExpiryDateInput.SendKeys(DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.Common.ClickSaveBtn();
                }
                if (i == 2)
                {
                    WaitUntil.VisibilityOfAllElementsLocatedBy(_addProgramBtn);
                    addProgramBtn.Click();

                    WaitUntil.VisibilityOfAllElementsLocatedBy(_programNameInput, 60);

                    programNameInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                    programNameInput.SendKeys(i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    programNumOfWeeksInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                    programNumOfWeeksInput.SendKeys("4");
                    programStepsInput.Clear();
                    programStepsInput.SendKeys("Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
                    programAvailableDateInput.SendKeys(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    programExpiryDateInput.SendKeys(DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.Common.ClickSaveBtn();
                }
                if (i == 3)
                {
                    WaitUntil.VisibilityOfAllElementsLocatedBy(_addProgramBtn);
                    addProgramBtn.Click();

                    WaitUntil.VisibilityOfAllElementsLocatedBy(_programNameInput, 60);

                    programNameInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                    programNameInput.SendKeys(i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    programNumOfWeeksInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                    programNumOfWeeksInput.SendKeys("4");
                    programStepsInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                    programStepsInput.SendKeys("Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
                    programAvailableDateInput.SendKeys(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    programExpiryDateInput.SendKeys(DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.Common.ClickSaveBtn();
                }
                if (i == 4)
                {
                    WaitUntil.VisibilityOfAllElementsLocatedBy(_addProgramBtn);
                    addProgramBtn.Click();

                    WaitUntil.VisibilityOfAllElementsLocatedBy(_programNameInput, 60);

                    programNameInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                    programNameInput.SendKeys(i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    programNumOfWeeksInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                    programNumOfWeeksInput.SendKeys("4");
                    programStepsInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                    programStepsInput.SendKeys("Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
                    programAvailableDateInput.SendKeys(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    programExpiryDateInput.SendKeys(DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.Common.ClickSaveBtn();
                }
                
            }
            
            return this;
        }


        [AllureStep("Create Workouts")]
        public MembershipAdmin CreateWorkouts(string url)
        {
            int i = 0;
            int y = 0;
            
            Browser._Driver.Navigate().GoToUrl(url);
            Pages.PopUp.ClosePopUp();
            IReadOnlyCollection<IWebElement> programList = Browser._Driver.FindElements(_programAddWorkoutsBtn);

            for(int q = 0; q < programList.Count;)
            {
                ++q;
                
                string addWorkouts = "//div[@class='table-item'][" + q + "]//div[@class='membership-item_add add-workout']";
                WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(addWorkouts), 10);
                IWebElement addWorkoutsBtn = Browser._Driver.FindElement(By.XPath(addWorkouts));
                addWorkoutsBtn.Click();

                while (i < 5)
                {
                    ++i;
                    WaitUntil.WaitSomeInterval(5);
                    if (i == 1)
                    {
                        WaitUntil.VisibilityOfAllElementsLocatedBy(_addWorkoutBtn, 60);
                        addWorkoutBtn.Click();
                        workoutNameInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                        workoutNameInput.SendKeys("Monday " + "Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                        weekDayBtn.Click();
                        weekDayBtn.SendKeys(Keys.ArrowDown + Keys.ArrowUp + Keys.Enter);
                        Pages.Common.ClickSaveBtn();
                    }
                    if (i == 3)
                    {
                        WaitUntil.VisibilityOfAllElementsLocatedBy(_addWorkoutBtn, 60);
                        addWorkoutBtn.Click();
                        workoutNameInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                        workoutNameInput.SendKeys("Wednesday " + "Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                        weekDayBtn.Click();
                        while (y < i - 1)
                        {
                            ++y;
                            weekDayBtn.SendKeys(Keys.ArrowDown);
                        }
                        y = 0;
                        weekDayBtn.SendKeys(Keys.Enter);
                        Pages.Common.ClickSaveBtn();
                    }
                    if (i == 5)
                    {
                        WaitUntil.VisibilityOfAllElementsLocatedBy(_addWorkoutBtn, 60);
                        addWorkoutBtn.Click();
                        workoutNameInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                        workoutNameInput.SendKeys("Friday " + "Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                        weekDayBtn.Click();
                        while (y < i - 1)
                        {
                            ++y;
                            weekDayBtn.SendKeys(Keys.ArrowDown);
                        }
                        y = 0;
                        weekDayBtn.SendKeys(Keys.Enter);
                        Pages.Common.ClickSaveBtn();


                        string findElement = "//div[@class='table-items']/div[" + (i - 2) + "]//div[@class='table-item-name']";

                        WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(findElement));

                    }

                    
                }
                WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='btn_back']"));
                IWebElement backBtn = Browser._Driver.FindElement(By.XPath("//div[@class='btn_back']"));
                backBtn.Click();
                i = 0;
            }
               
            return this;
        }

        [AllureStep("Add exercises")]
        public MembershipAdmin AddExercises(string url, string[] exercises)
        {
            Browser._Driver.Navigate().GoToUrl(url);
            Pages.PopUp.ClosePopUp();
            IReadOnlyCollection<IWebElement> programList = Browser._Driver.FindElements(_programAddWorkoutsBtn);
            for (int q = 0; q < programList.Count;)
            {
                ++q;
                WaitUntil.WaitSomeInterval(5);
                string addWorkouts = "//div[@class='table-item'][" + q + "]//div[@class='membership-item_add add-workout']";
                WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(addWorkouts), 10);
                IWebElement addWorkoutsBtn = Browser._Driver.FindElement(By.XPath(addWorkouts));
                addWorkoutsBtn.Click();
                

                IReadOnlyCollection<IWebElement> workoutList = Browser._Driver.FindElements(By.XPath("//div[@class='membership-item_add add-workout']"));

                
                for (int items = 0; items < workoutList.Count;)
                {
                    ++items;
                    WaitUntil.WaitSomeInterval(5);
                    string addBtn = "//div[@class='table-items']/div[" + items + "]//div[@class='membership-item_add add-workout']";
                    WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(addBtn));
                    IWebElement AddWorkoutsBtn = Browser._Driver.FindElement(By.XPath(addBtn));
                    AddWorkoutsBtn.Click();

                    WaitUntil.WaitSomeInterval(3);
                    WaitUntil.VisibilityOfAllElementsLocatedBy(_addExerciseBtn);
                    addExerciseBtn.Click();

                    int x = 0;
                    int y = 0;
                    while (x < 6)
                    {
                        ++x;
                        WaitUntil.WaitSomeInterval(1);
                        if (x == 1)
                        {

                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("A");
                            exercisesCbbx.SendKeys(exercises[RandomHelper.RandomNumber(exercises.Length)] + Keys.Enter);
                            setsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            setsExerciseInput.SendKeys("6");
                            repsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            repsExerciseInput.SendKeys("4,4,4,5,7,8");
                            restExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            restExerciseInput.SendKeys("60");
                            tempoExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            tempoExerciseInput.SendKeys("2010");
                            notesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            WaitUntil.VisibilityOfAllElementsLocatedBy(_exercisesTitle, 20);
                            notesExerciseInput.SendKeys(exercisesTitle.GetAttribute("title"));

                            Pages.Common.ClickSaveBtn();
                        }
                        if (x == 2)
                        {

                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("B");
                            exercisesCbbx.SendKeys(exercises[RandomHelper.RandomNumber(exercises.Length)] + Keys.Enter);
                            setsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            setsExerciseInput.SendKeys("4");
                            repsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            repsExerciseInput.SendKeys("4,4,4,5");
                            restExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            restExerciseInput.SendKeys("60");
                            tempoExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            tempoExerciseInput.SendKeys("2010");
                            notesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            WaitUntil.VisibilityOfAllElementsLocatedBy(_exercisesTitle, 20);
                            notesExerciseInput.SendKeys(exercisesTitle.GetAttribute("title"));

                            Pages.Common.ClickSaveBtn();
                        }
                        if (x == 3)
                        {

                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("C1");
                            exercisesCbbx.SendKeys(exercises[RandomHelper.RandomNumber(exercises.Length)] + Keys.Enter);
                            setsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            setsExerciseInput.SendKeys("4");
                            repsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            repsExerciseInput.SendKeys("4,4,4,5");
                            restExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            restExerciseInput.SendKeys("120");
                            tempoExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            tempoExerciseInput.SendKeys("3010");
                            notesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            WaitUntil.VisibilityOfAllElementsLocatedBy(_exercisesTitle, 20);
                            notesExerciseInput.SendKeys(exercisesTitle.GetAttribute("title"));

                            Pages.Common.ClickSaveBtn();
                        }
                        if (x == 4)
                        {

                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("C2");
                            exercisesCbbx.SendKeys(exercises[RandomHelper.RandomNumber(exercises.Length)] + Keys.Enter);
                            setsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            setsExerciseInput.SendKeys("5");
                            repsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            repsExerciseInput.SendKeys("4,4,4,5,6");
                            restExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            restExerciseInput.SendKeys("60");
                            tempoExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            tempoExerciseInput.SendKeys("2010");
                            notesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            WaitUntil.VisibilityOfAllElementsLocatedBy(_exercisesTitle, 20);
                            notesExerciseInput.SendKeys(exercisesTitle.GetAttribute("title"));

                            Pages.Common.ClickSaveBtn();
                        }
                        if (x == 5)
                        {

                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("D");
                            exercisesCbbx.SendKeys(exercises[RandomHelper.RandomNumber(exercises.Length)] + Keys.Enter);
                            setsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            setsExerciseInput.SendKeys("3");
                            repsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            repsExerciseInput.SendKeys("4,4,4");
                            restExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            restExerciseInput.SendKeys("60");
                            tempoExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            tempoExerciseInput.SendKeys("2010");
                            notesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            WaitUntil.VisibilityOfAllElementsLocatedBy(_exercisesTitle, 20);
                            notesExerciseInput.SendKeys(exercisesTitle.GetAttribute("title"));

                            Pages.Common.ClickSaveBtn();
                        }
                        if (x == 6)
                        {

                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("E");
                            exercisesCbbx.SendKeys(exercises[RandomHelper.RandomNumber(exercises.Length)] + Keys.Enter);
                            setsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            setsExerciseInput.SendKeys("4");
                            repsExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            repsExerciseInput.SendKeys("4,4,4,5");
                            restExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            restExerciseInput.SendKeys("60");
                            tempoExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            tempoExerciseInput.SendKeys("2010");
                            notesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            WaitUntil.VisibilityOfAllElementsLocatedBy(_exercisesTitle, 20);
                            notesExerciseInput.SendKeys(exercisesTitle.GetAttribute("title"));

                            Pages.Common.ClickSaveBtn();

                            WaitUntil.WaitSomeInterval(3);
                            IReadOnlyCollection<IWebElement> titleList = Browser._Driver.FindElements(_exerciseTitleRow);

                            string findElement = "//div[@class='table-items']/div[" + titleList.Count + "]//p[@class='name']";

                            WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(findElement));

                        }

                    }


                    WaitUntil.WaitSomeInterval(3);
                    WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='btn_back']"));
                    IWebElement backBtn = Browser._Driver.FindElement(By.XPath("//div[@class='btn_back']"));
                    backBtn.Click();
                }
                WaitUntil.VisibilityOfAllElementsLocatedBy(_backBtn);
                
                backBtn.Click();
            }

            
            return this;
        }

        [AllureStep("Add user")]
        public MembershipAdmin AddUserToMembership(string email)
        {
            WaitUntil.VisibilityOfAllElementsLocatedBy(_membershipAddUserBtn, 60);
            membershipAddUserBtn.Click();

            WaitUntil.VisibilityOfAllElementsLocatedBy(_addUserBtn, 60);
            userCbbx.SendKeys(email + Keys.Enter);
            addUserBtn.Click();


            return this;
        }




    }
}
