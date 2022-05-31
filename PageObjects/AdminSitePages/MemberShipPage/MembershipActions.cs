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
            WaitUntil.VisibilityOfAllElementsLocatedBy(_CreateBtn);
            Button.Click(membershipCreateBtn);
            return this;
        }

        [AllureStep("Enter membership data")]

        public MembershipAdmin EnterMembershipData()
        {
            InputBox.Element(skuInput, 20, "CP_TEST_SUB");
            InputBox.Element(membershipNameInput, 20, "Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            InputBox.Element(membershipDescriptionInput, 20, "Lorem ipsum dolor");

            InputBox.Element(startDateInput, 10, DateTime.Now.ToString("yyyy-MM-d") + Keys.Enter);
            InputBox.Element(endDateInput, 10, DateTime.Now.AddMonths(2).ToString("yyyy-MM-d") + Keys.Enter);

            Button.Click(genderBothToggle);
            Button.Click(productToggleType);
            InputBox.Element(priceInput, 10, "99");
            InputBox.Element(urlInput, 10, Endpoints.websiteHost);
            availableForPurchaseCheckboxElem.Click();
            

            return this;
        }



        [AllureStep("Search Membership")]
        public MembershipAdmin SearchMembership(string[] membershipName)
        {
            InputBox.Element(membershipSearchInput, 10, membershipName[0]);
            Button.Click(membershipSearchBtn);

            return this;
        }


        [AllureStep("Click \"Delete\" button")]
        public MembershipAdmin ClickDeleteBtn()
        {
            
            Button.Click(membershipDeleteBtn);
            Button.Click(membershipConfirmYesBtn);

            return this;
        }

        [AllureStep("Click \"Add programs\" button")]
        public MembershipAdmin ClickAddProgramsBtn()
        {
            Button.Click(membershipAddProgramsBtn);

            return this;
        }

        [AllureStep("Create Programs")]
        public MembershipAdmin CreatePrograms()
        {
            
            for (int i = 0; i < 5; ++i)
            {
               

                if (i == 1)
                {
                    Button.Click(btnAddProgram);
                    InputBox.Element(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.Element(inputProgramNumOfWeeks, 10, "4");
                    InputBox.Element(inputProgramSteps, 10, "10000");
                    InputBox.Element(inputProgramAvailableDate[0], 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.Element(inputProgramAvailableDate[1], 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.Common.ClickSaveBtn();
                }
                if (i == 2)
                {
                    Button.Click(btnAddProgram);
                    InputBox.Element(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.Element(inputProgramNumOfWeeks, 10, "4");
                    InputBox.Element(inputProgramSteps, 10, "Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
                    InputBox.Element(inputProgramAvailableDate[0], 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.Element(inputProgramAvailableDate[1], 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.Common.ClickSaveBtn();
                }
                if (i == 3)
                {
                    Button.Click(btnAddProgram);
                    InputBox.Element(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.Element(inputProgramNumOfWeeks, 10, "4");
                    InputBox.Element(inputProgramSteps, 10, "Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
                    InputBox.Element(inputProgramAvailableDate[0], 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.Element(inputProgramAvailableDate[1], 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.Common.ClickSaveBtn();
                }
                if (i == 4)
                {
                    Button.Click(btnAddProgram);
                    InputBox.Element(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.Element(inputProgramNumOfWeeks, 10, "4");
                    InputBox.Element(inputProgramSteps, 10, "10000");
                    InputBox.Element(inputProgramAvailableDate[0], 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.Element(inputProgramAvailableDate[1], 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.Common.ClickSaveBtn();
                }
                
            }
            
            return this;
        }

        [AllureStep("Click \"Add Workout\" button")]
        public MembershipAdmin ClickAddWorkoutBtn()
        {
            WaitUntil.WaitSomeInterval(2);
            WaitUntil.CustomElevemtIsVisible(btnProgramAddWorkoutsElem);
            var listPrograms = btnProgramAddWorkouts.Where(x => x.Displayed).ToList();
            Button.Click(listPrograms[0]);

            return this;
        }

        [AllureStep("Create Workouts")]
        public MembershipAdmin CreateWorkouts(string[] programNames)
        {
            foreach (string programName in programNames)
            {
                InputBox.CbbxElement(cbbxPhaseName, 10, programName + Keys.Enter);

                for (int i = 0; i <= 5; i++)
                {


                    if (i == 1)
                    {

                        Button.Click(btnAddWorkout);
                        InputBox.Element(inputWorkoutName, 10, "Monday " + "Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                        btnWeekDayElem.SendKeys(Keys.ArrowDown + Keys.Enter);

                        Pages.Common.ClickSaveBtn();
                    }
                    if (i == 3)
                    {
                        Button.Click(btnAddWorkout);
                        InputBox.Element(inputWorkoutName, 10, "Wednesday " + "Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                        //Button.Click(weekDayBtn);

                        for (int y = 0; y < (i); ++y)
                        {

                            btnWeekDayElem.SendKeys(Keys.ArrowDown);
                        }

                        btnWeekDayElem.SendKeys(Keys.Enter);
                        Pages.Common.ClickSaveBtn();
                    }
                    if (i == 5)
                    {
                        Button.Click(btnAddWorkout);
                        InputBox.Element(inputWorkoutName, 10, "Friday " + "Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                        //Button.Click(weekDayBtn);

                        for (int y = 0; y < (i); ++y)
                        {

                            btnWeekDayElem.SendKeys(Keys.ArrowDown);
                        }

                        btnWeekDayElem.SendKeys(Keys.Enter);
                        Pages.Common.ClickSaveBtn();



                    }


                }
            }

            return this;
        }


        [AllureStep("Add exercises")]
        public MembershipAdmin AddExercises(string[] programNames, string[] exercises)
        {
            foreach (string programName in programNames)
            {
                InputBox.CbbxElement(cbbxPhaseName, 10, programName + Keys.Enter);
                WaitUntil.WaitSomeInterval(2);
                WaitUntil.CustomElevemtIsVisible(btnAddExercisesElement);
                var listWorkouts = btnAddExercises.Where(x => x.Displayed).ToList();

                string[] workoutNames = GetWorkoutNames();

                Button.Click(listWorkouts[0]);
                WaitUntil.CustomElevemtIsVisible(addExerciseBtn);
                Button.Click(addExerciseBtn);
                foreach (string workoutName in workoutNames)
                {
                    InputBox.CbbxElement(cbbxWorkoutsTitle, 20, workoutName + Keys.Enter);
                    
                    for (int i = 0; i <= 6; ++i)
                    {

                        if (i == 1)
                        {
                            InputBox.Element(seriesExerciseInput, 10, "A");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Length)] + Keys.Enter);
                            InputBox.Element(setsExerciseInput, 10, "6");
                            InputBox.Element(repsExerciseInput, 10, "4,4,4,5,7,8");
                            InputBox.Element(restExerciseInput, 10, "60");
                            InputBox.Element(tempoExerciseInput, 10, "2010");
                            InputBox.Element(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));

                            Pages.Common.ClickSaveBtn();
                        }
                        if (i == 2)
                        {

                            InputBox.Element(seriesExerciseInput, 10, "B");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Length)] + Keys.Enter);
                            InputBox.Element(setsExerciseInput, 10, "4");
                            InputBox.Element(repsExerciseInput, 10, "10-12");
                            InputBox.Element(restExerciseInput, 10, "30");
                            InputBox.Element(tempoExerciseInput, 10, "2010");
                            InputBox.Element(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));

                            Pages.Common.ClickSaveBtn();
                        }
                        if (i == 3)
                        {
                            InputBox.Element(seriesExerciseInput, 10, "C1");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Length)] + Keys.Enter);
                            InputBox.Element(setsExerciseInput, 10, "7");
                            InputBox.Element(repsExerciseInput, 10, "15-20 Each");
                            InputBox.Element(restExerciseInput, 10, "60");
                            InputBox.Element(tempoExerciseInput, 10, "2010");
                            InputBox.Element(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));

                            Pages.Common.ClickSaveBtn();
                        }
                        if (i == 4)
                        {

                            InputBox.Element(seriesExerciseInput, 10, "C2");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Length)] + Keys.Enter);
                            InputBox.Element(setsExerciseInput, 10, "3");
                            InputBox.Element(repsExerciseInput, 10, "4,4,4");
                            InputBox.Element(restExerciseInput, 10, "60");
                            InputBox.Element(tempoExerciseInput, 10, "3010");
                            InputBox.Element(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));

                            Pages.Common.ClickSaveBtn();
                        }
                        if (i == 5)
                        {

                            InputBox.Element(seriesExerciseInput, 10, "D");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Length)] + Keys.Enter);
                            InputBox.Element(setsExerciseInput, 10, "6");
                            InputBox.Element(repsExerciseInput, 10, "4,4,4,5,7,8");
                            InputBox.Element(restExerciseInput, 10, "60");
                            InputBox.Element(tempoExerciseInput, 10, "2010");
                            InputBox.Element(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));

                            Pages.Common.ClickSaveBtn();
                        }
                        if (i == 6)
                        {
                            InputBox.Element(seriesExerciseInput, 10, "E");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Length)] + Keys.Enter);
                            InputBox.Element(setsExerciseInput, 10, "5");
                            InputBox.Element(repsExerciseInput, 10, "4,4,4,5,7");
                            InputBox.Element(restExerciseInput, 10, "60");
                            InputBox.Element(tempoExerciseInput, 10, "2010");
                            InputBox.Element(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));

                            Pages.Common.ClickSaveBtn();


                        }
                    }
                }

                for (int i = 0; i<4; i++)
                {
                    
                    Button.Click(backBtn);
                }
            }    

            return this;
        }

        [AllureStep("Add user")]
        public MembershipAdmin AddUserToMembership(string email)
        {

            Button.Click(membershipAddUserBtn);
            InputBox.Element(userCbbx, 60, email + Keys.Enter);

            Button.Click(addUserBtn);


            return this;
        }




    }
}
