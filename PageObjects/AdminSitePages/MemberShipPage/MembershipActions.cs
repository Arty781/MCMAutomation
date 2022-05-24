﻿using NUnit.Allure.Steps;
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

                    programNameInput.Clear();
                    programNameInput.SendKeys(i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    programNumOfWeeksInput.Clear();
                    programNumOfWeeksInput.SendKeys("4");
                    programStepsInput.Clear();
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

                    programNameInput.Clear();
                    programNameInput.SendKeys(i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    programNumOfWeeksInput.Clear();
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

                    programNameInput.Clear();
                    programNameInput.SendKeys(i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    programNumOfWeeksInput.Clear();
                    programNumOfWeeksInput.SendKeys("3");
                    programStepsInput.Clear();
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

                    programNameInput.Clear();
                    programNameInput.SendKeys(i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    programNumOfWeeksInput.Clear();
                    programNumOfWeeksInput.SendKeys("6");
                    programStepsInput.Clear();
                    programStepsInput.SendKeys("Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
                    programAvailableDateInput.SendKeys(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    programExpiryDateInput.SendKeys(DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.Common.ClickSaveBtn();
                }
                if (i == 5)
                {
                    WaitUntil.VisibilityOfAllElementsLocatedBy(_addProgramBtn);
                    addProgramBtn.Click();

                    WaitUntil.VisibilityOfAllElementsLocatedBy(_programNameInput, 60);

                    programNameInput.Clear();
                    programNameInput.SendKeys(i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    programNumOfWeeksInput.Clear();
                    programNumOfWeeksInput.SendKeys("7");
                    programStepsInput.Clear();
                    programStepsInput.SendKeys("Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
                    programAvailableDateInput.SendKeys(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    programExpiryDateInput.SendKeys(DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.Common.ClickSaveBtn();
                }
            }
            
            return this;
        }


        [AllureStep("Create Workouts")]
        public MembershipAdmin CreateWorkouts(IList<string> workoutLinks)
        {
            int i= 0;
            int y= 0;
            foreach (var workoutLink in workoutLinks)
            {
                Browser._Driver.Navigate().GoToUrl(workoutLink);
                Pages.PopUp.ClosePopUp();
                while (i < 5)
                {
                    
                    ++i;
                    if (i == 1)
                    {
                        WaitUntil.VisibilityOfAllElementsLocatedBy(_addWorkoutBtn, 60);
                        addWorkoutBtn.Click();
                        workoutNameInput.SendKeys("Monday " + "Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                        weekDayBtn.Click();
                        weekDayBtn.SendKeys(Keys.ArrowDown + Keys.ArrowUp + Keys.Enter);
                        Pages.Common.ClickSaveBtn();
                    }
                    if (i == 3)
                    {
                        WaitUntil.VisibilityOfAllElementsLocatedBy(_addWorkoutBtn, 60);
                        addWorkoutBtn.Click();
                        workoutNameInput.SendKeys("Wednesday " + "Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                        weekDayBtn.Click();
                        while (y < i - 1)
                        {
                            ++y;
                            weekDayBtn.SendKeys(Keys.ArrowDown);
                        }
                        y= 0;
                        weekDayBtn.SendKeys(Keys.Enter);
                        Pages.Common.ClickSaveBtn();
                    }
                    if (i == 5)
                    {
                        WaitUntil.VisibilityOfAllElementsLocatedBy(_addWorkoutBtn, 60);
                        addWorkoutBtn.Click();
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
                    }
                }
                i= 0;
                
            }
            

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
        public MembershipAdmin AddExercises(IList<string> links)
        {
            
            foreach (var link in links)
            {
                Browser._Driver.Navigate().GoToUrl(link);
                Pages.PopUp.ClosePopUp();
                WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='membership-item_add add-workout']"), 30);

                IReadOnlyCollection<IWebElement> workoutList = Browser._Driver.FindElements(By.XPath("//div[@class='membership-item_add add-workout']"));

                
                for (int items = 0; items < workoutList.Count;)
                {
                    ++items;
                    string addBtn = "//div[@class='table-items']/div[" + items + "]//div[@class='membership-item_add add-workout']";
                    WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath(addBtn));
                    IWebElement AddWorkoutsBtn = Browser._Driver.FindElement(By.XPath(addBtn));
                    AddWorkoutsBtn.Click();

                    WaitUntil.VisibilityOfAllElementsLocatedBy(_addExerciseBtn);
                    addExerciseBtn.Click();

                    int x = 0;
                    while (x < 6)
                    {
                        ++x;

                        if (x == 1)
                        {


                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("A");
                            exercisesCbbx.SendKeys(Exersises.exercise[x] + Keys.Enter);
                            setsExerciseInput.SendKeys("4");
                            repsExerciseInput.SendKeys("4,4,4,5");
                            restExerciseInput.SendKeys("60");
                            tempoExerciseInput.SendKeys("2010");
                            notesExerciseInput.SendKeys(Exersises.exercise[x]);

                            WaitUntil.WaitSomeInterval(1);
                            Pages.Common.saveBtn.Click();
                        }
                        if (x == 2)
                        {

                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("B");
                            exercisesCbbx.SendKeys(Exersises.exercise[x] + Keys.Enter);
                            setsExerciseInput.SendKeys("3");
                            repsExerciseInput.SendKeys("4,4,4");
                            restExerciseInput.SendKeys("10");
                            tempoExerciseInput.SendKeys("3010");
                            notesExerciseInput.SendKeys(Exersises.exercise[x]);

                            WaitUntil.WaitSomeInterval(1);
                            Pages.Common.saveBtn.Click();
                        }
                        if (x == 3)
                        {

                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("C1");
                            exercisesCbbx.SendKeys(Exersises.exercise[x] + Keys.Enter);
                            setsExerciseInput.SendKeys("5");
                            repsExerciseInput.SendKeys("4,4,4,5,6");
                            restExerciseInput.SendKeys("120");
                            tempoExerciseInput.SendKeys("2010");
                            notesExerciseInput.SendKeys(Exersises.exercise[x]);

                            WaitUntil.WaitSomeInterval(1);
                            Pages.Common.saveBtn.Click();
                        }
                        if (x == 4)
                        {

                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("C2");
                            exercisesCbbx.SendKeys(Exersises.exercise[x] + Keys.Enter);
                            setsExerciseInput.SendKeys("4");
                            repsExerciseInput.SendKeys("4,4,4,5");
                            restExerciseInput.SendKeys("60");
                            tempoExerciseInput.SendKeys("2010");
                            notesExerciseInput.SendKeys(Exersises.exercise[x]);

                            WaitUntil.WaitSomeInterval(1);
                            Pages.Common.saveBtn.Click();
                        }
                        if (x == 5)
                        {

                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("E");
                            exercisesCbbx.SendKeys(Exersises.exercise[x] + Keys.Enter);
                            setsExerciseInput.SendKeys("4");
                            repsExerciseInput.SendKeys("4,4,4,5");
                            restExerciseInput.SendKeys("60");
                            tempoExerciseInput.SendKeys("2010");
                            notesExerciseInput.SendKeys(Exersises.exercise[x]);

                            WaitUntil.WaitSomeInterval(1);
                            Pages.Common.saveBtn.Click();
                        }
                        if (x == 6)
                        {

                            WaitUntil.VisibilityOfAllElementsLocatedBy(_seriesInput, 20);
                            seriesExerciseInput.SendKeys(Keys.Control + "A" + Keys.Delete);
                            seriesExerciseInput.SendKeys("D");
                            exercisesCbbx.SendKeys(Exersises.exercise[x] + Keys.Enter);
                            setsExerciseInput.SendKeys("4");
                            repsExerciseInput.SendKeys("4,4,4,5");
                            restExerciseInput.SendKeys("60");
                            tempoExerciseInput.SendKeys("2010");
                            notesExerciseInput.SendKeys(Exersises.exercise[x]);

                            WaitUntil.WaitSomeInterval(1);
                            Pages.Common.saveBtn.Click();
                        }

                    }


                    WaitUntil.WaitSomeInterval(3);
                    WaitUntil.VisibilityOfAllElementsLocatedBy(By.XPath("//div[@class='btn_back']"));
                    IWebElement backBtn = Browser._Driver.FindElement(By.XPath("//div[@class='btn_back']"));
                    backBtn.Click();
                }

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
