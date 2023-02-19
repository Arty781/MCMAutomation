using NUnit.Allure.Steps;
using OpenQA.Selenium;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RimuTec.Faker;

namespace MCMAutomation.PageObjects
{
    public partial class MembershipAdmin
    {

        [AllureStep("Click \"Create new membership\" btn")]
        public MembershipAdmin ClickCreateBtn()
        {

            WaitUntil.CustomElevemtIsVisible(btnCreateMembership);
            Button.Click(btnCreateMembership);

            return this;
        }

        [AllureStep("Click \"Edit membership\" btn")]
        public MembershipAdmin ClickEditMembershipBtn(string title)
        {

            WaitUntil.CustomElevemtIsVisible(membershipTitle.FirstOrDefault());
            SwitcherHelper.ClickEditMembershipBtn(title);

            return this;
        }

        [AllureStep("Enter membership data")]
        public MembershipAdmin EnterMembershipData()
        {
            InputBox.ElementCtrlA(skuInput, 20, MembershipsSKU.MEMBERSHIP_SKU[1] + DateTime.Now.ToString("hh:mm:ss"));
            InputBox.ElementCtrlA(membershipNameInput, 20, "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            InputBox.ElementCtrlA(membershipDescriptionInput, 20, Lorem.ParagraphByChars(792));
            InputBox.ElementCtrlA(inputAccessWeek, 10, "16" + Keys.Enter);
            InputBox.ElementCtrlA(priceInput, 10, "100");
            InputBox.ElementCtrlA(urlInput, 10, Endpoints.WEBSITE_HOST);
            

            return this;
        }

        [AllureStep("Enter membership data")]
        public MembershipAdmin EnterSubscriptionMembershipData()
        {
            InputBox.ElementCtrlA(skuInput, 20, MembershipsSKU.MEMBERSHIP_SKU[1] + DateTime.Now.ToString("hh:mm:ss"));
            InputBox.ElementCtrlA(membershipNameInput, 20, "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            InputBox.ElementCtrlA(membershipDescriptionInput, 20, Lorem.ParagraphByChars(792));
            InputBox.ElementCtrlA(priceInput, 10, "100");
            InputBox.ElementCtrlA(urlInput, 10, Endpoints.WEBSITE_HOST);


            return this;
        }

        [AllureStep("Enter membership data")]
        public MembershipAdmin EnterCustomMembershipData()
        {
            InputBox.ElementCtrlA(membershipNameInput, 20, "00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            InputBox.ElementCtrlA(membershipDescriptionInput, 20, Lorem.ParagraphByChars(792));
            InputBox.ElementCtrlA(inputAccessWeek, 10, "16");


            return this;
        }

        [AllureStep("Select membership type")]
        public MembershipAdmin SelectMembershipType(string typeName)
        {
            if(typeName == MembershipType.PRODUCT)
            {
                Button.Click(toggleProductType);
            }
            else if(typeName == MembershipType.SUBSCRIPTION)
            {
                Button.Click(toggleSubscriptionType);
            }
            else if(typeName == MembershipType.MULTILEVEL)
            {
                Button.Click(toggleMultilevelType);
            }
            
            return this;
        }


        [AllureStep("Add levels")]
        public MembershipAdmin AddLevels(int count)
        {
            for(int i=0; i<count; i++)
            {
                Button.Click(btnAddLevel);
                InputBox.ElementCtrlA(inputLevelName, 5, String.Concat("Test Level " + DateTime.Now.ToString("yyyy-MM-d hh:mm:ss")));
                Button.Click(inputLevelMembership);
                Button.ClickJS(listLevelMembership[RandomHelper.RandomNum(10)]);
                Button.Click(btnSaveLevel);
            }
            
            return this;
        }

        [AllureStep("Select gender")]
        public MembershipAdmin SelectGender()
        {
            Button.Click(genderBothToggle);

            return this;
        }


        [AllureStep("Enter membership data edited")]
        public MembershipAdmin EditMembershipData()
        {
            InputBox.ElementCtrlA(skuInput, 20, MembershipsSKU.MEMBERSHIP_SKU[1]);
            InputBox.ElementCtrlA(membershipNameInput, 20, "Edited New Membership " + DateTime.Now.ToString("yyyy-MM-d hh:mm"));
            InputBox.ElementCtrlA(membershipDescriptionInput, 20, Lorem.Paragraph());
            InputBox.ElementCtrlA(inputAccessWeek, 10, "16");
            Button.Click(genderMaleToggle);
            InputBox.ElementCtrlA(priceInput, 10, "");
            InputBox.ElementCtrlA(urlInput, 10, Endpoints.WEBSITE_HOST);
            Button.Click(availableForPurchaseCheckbox);

            return this;
        }

        [AllureStep("Search Membership")]
        public MembershipAdmin SearchMembership(string membershipName)
        {
            InputBox.ElementCtrlA(membershipSearchInput, 10, membershipName);
            Button.Click(membershipSearchBtn);

            return this;
        }


        [AllureStep(@"Click ""Delete"" button")]
        public MembershipAdmin ClickDeleteBtn()
        {
            
            Button.Click(membershipDeleteBtn);
            Button.Click(membershipConfirmYesBtn);

            return this;
        }

        [AllureStep("Click \"Add programs\" button")]
        public MembershipAdmin ClickAddProgramsBtn(string memberName)
        {
            WaitUntil.CustomElevemtIsVisible(membershipAddProgramsBtn, 30);
            SwitcherHelper.ClickAddProgramBtn(memberName);
            WaitUntil.CustomElevemtIsVisible(btnAddProgram, 30);

            return this;
        }

        [AllureStep("Create Programs")]
        public MembershipAdmin CreateProgramsWithStartEndDate()
        {
            
            for (int i = 0; i < 4; ++i)
            {
               

                if (i == 1)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase 1 " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, Workouts.STRING[RandomHelper.RandomNum(3)]);
                    InputBox.ElementCtrlA(inputProgramAvailableDate.FirstOrDefault(), 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.ElementCtrlA(inputProgramAvailableDate.LastOrDefault(), 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);
                    Pages.CommonPages.Common.ClickSaveBtn();
                    WaitUntil.WaitSomeInterval(1500);
                }
                else if (i == 2)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase 2 " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, Workouts.STRING[RandomHelper.RandomNum(3)]);
                    InputBox.ElementCtrlA(inputProgramAvailableDate.FirstOrDefault(), 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.ElementCtrlA(inputProgramAvailableDate.LastOrDefault(), 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);
                    Pages.CommonPages.Common.ClickSaveBtn();
                    WaitUntil.WaitSomeInterval(1500);
                }
                else if (i == 3)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase 3 " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, Workouts.STRING[RandomHelper.RandomNum(3)]);
                    InputBox.ElementCtrlA(inputProgramAvailableDate.FirstOrDefault(), 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.ElementCtrlA(inputProgramAvailableDate.LastOrDefault(), 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);
                    Pages.CommonPages.Common.ClickSaveBtn();
                    WaitUntil.WaitSomeInterval(1500);
                }
                else if (i == 4)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase 4 " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, Workouts.STRING[RandomHelper.RandomNum(3)]);
                    InputBox.ElementCtrlA(inputProgramAvailableDate.FirstOrDefault(), 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.ElementCtrlA(inputProgramAvailableDate.LastOrDefault(), 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);
                    Pages.CommonPages.Common.ClickSaveBtn();
                    WaitUntil.WaitSomeInterval(1500);
                }
                else if (i > 4)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase 5 " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, Workouts.STRING[RandomHelper.RandomNum(3)]);
                    InputBox.ElementCtrlA(inputProgramAvailableDate.FirstOrDefault(), 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.ElementCtrlA(inputProgramAvailableDate.LastOrDefault(), 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);
                    Pages.CommonPages.Common.ClickSaveBtn();
                    WaitUntil.WaitSomeInterval(1500);
                }

            }
            
            return this;
        }

        [AllureStep("Create Programs")]
        public MembershipAdmin CreatePrograms()
        {

            for (int i = 0; i < 2; ++i)
            {


                if (i == 1)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, Workouts.STRING[RandomHelper.RandomNum(3)]);
                    Pages.CommonPages.Common.ClickSaveBtn();
                    WaitUntil.WaitSomeInterval(1500);
                }
                else if (i == 2)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, Workouts.STRING[RandomHelper.RandomNum(3)]);
                    Pages.CommonPages.Common.ClickSaveBtn();
                    WaitUntil.WaitSomeInterval(1500);
                }
                else if (i == 3)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, Workouts.STRING[RandomHelper.RandomNum(3)]);
                    Pages.CommonPages.Common.ClickSaveBtn();
                    WaitUntil.WaitSomeInterval(1500);
                }
                else if (i == 4)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, Workouts.STRING[RandomHelper.RandomNum(3)]);
                    Pages.CommonPages.Common.ClickSaveBtn();
                    WaitUntil.WaitSomeInterval(1500);
                }
                else if (i > 4)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, Workouts.STRING[RandomHelper.RandomNum(3)]);
                    Pages.CommonPages.Common.ClickSaveBtn();
                    WaitUntil.WaitSomeInterval(1500);
                }

            }

            return this;
        }

        [AllureStep("Create Programs")]
        public MembershipAdmin CreateProgramsMega()
        {

            for (int i = 0; i < 9; ++i)
            {


                if (i == 1)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, "10000");
                    InputBox.ElementCtrlA(inputProgramAvailableDate[0], 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.ElementCtrlA(inputProgramAvailableDate[1], 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.CommonPages.Common.ClickSaveBtn();
                }
                else if (i == 2)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, "Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
                    InputBox.ElementCtrlA(inputProgramAvailableDate[0], 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.ElementCtrlA(inputProgramAvailableDate[1], 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.CommonPages.Common.ClickSaveBtn();
                }
                else if (i == 3)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, "Refer to the <a href = \"https://guidebooksmc.s3.ap-southeast-2.amazonaws.com/Challenge+OCT21/Welcome+Pack+Challenge+9.0.pdf\">Welcome Pack</a> for your Cardio and Step Requirements");
                    InputBox.ElementCtrlA(inputProgramAvailableDate[0], 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.ElementCtrlA(inputProgramAvailableDate[1], 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.CommonPages.Common.ClickSaveBtn();
                }
                else if (i == 4)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, "10000");
                    InputBox.ElementCtrlA(inputProgramAvailableDate[0], 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.ElementCtrlA(inputProgramAvailableDate[1], 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.CommonPages.Common.ClickSaveBtn();
                }
                else if (i > 4)
                {
                    Button.Click(btnAddProgram);
                    InputBox.ElementCtrlA(inputProgramName, 10, i + " " + "Phase " + DateTime.Now.ToString("hh-mm-ss"));
                    InputBox.ElementCtrlA(inputProgramNumOfWeeks, 10, "4");
                    InputBox.ElementCtrlA(inputProgramSteps, 10, "10000");
                    InputBox.ElementCtrlA(inputProgramAvailableDate[0], 10, DateTime.Now.AddMonths(-1).ToString("yyyy-MM-d") + Keys.Enter);
                    InputBox.ElementCtrlA(inputProgramAvailableDate[1], 10, DateTime.Now.AddMonths(1).ToString("yyyy-MM-d") + Keys.Enter);

                    Pages.CommonPages.Common.ClickSaveBtn();
                }

            }

            return this;
        }

        [AllureStep("Delete programs")]
        public MembershipAdmin DeletePrograms(List<DB.Programs> programList)
        {
            foreach (var program in programList)
            {
                SwitcherHelper.ClickDeleteProgramBtn(program.Name);
            }
            

            return this;
        }

        [AllureStep("Click \"Add Workout\" button")]
        public MembershipAdmin ClickAddWorkoutBtn()
        {
            WaitUntil.WaitSomeInterval(1000);
            WaitUntil.CustomElevemtIsVisible(btnProgramAddWorkoutsElem);
            var listPrograms = btnProgramAddWorkouts.Where(x => x.Displayed).ToList();
            Button.Click(listPrograms[0]);

            return this;
        }

        [AllureStep("Create Workouts")]
        public MembershipAdmin CreateWorkouts(List<string> programNames)
        {
            foreach (string programName in programNames)
            {
                InputBox.CbbxElement(cbbxPhaseName, 10, programName);

                for (int i = 0; i <= 3; i++)
                {


                    if (i == 1)
                    {

                        Button.Click(btnAddWorkout);
                        InputBox.ElementCtrlA(inputWorkoutName, 10, "Monday " + "Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                        btnWeekDayElem.SendKeys(Keys.ArrowDown + Keys.Enter);

                        Pages.CommonPages.Common.ClickSaveBtn();
                    }
                    if (i == 3)
                    {
                        Button.Click(btnAddWorkout);
                        InputBox.ElementCtrlA(inputWorkoutName, 10, "Wednesday " + "Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                        //Button.Click(weekDayBtn);

                        for (int y = 0; y < i; ++y)
                        {

                            btnWeekDayElem.SendKeys(Keys.ArrowDown);
                        }

                        btnWeekDayElem.SendKeys(Keys.Enter);
                        Pages.CommonPages.Common.ClickSaveBtn();
                    }
                    if (i == 5)
                    {
                        Button.Click(btnAddWorkout);
                        InputBox.ElementCtrlA(inputWorkoutName, 10, "Friday " + "Workout " + DateTime.Now.ToString("MM-d hh-mm-ss"));
                        //Button.Click(weekDayBtn);

                        for (int y = 0; y < i; ++y)
                        {

                            btnWeekDayElem.SendKeys(Keys.ArrowDown);
                        }

                        btnWeekDayElem.SendKeys(Keys.Enter);
                        Pages.CommonPages.Common.ClickSaveBtn();



                    }


                }
            }

            return this;
        }

        [AllureStep("Get Workout names")]
        public List<string> GetWorkoutNames()
        {
            WaitUntil.CustomElevemtIsVisible(nameWorkoutTitleElem, 30);

            var workoutNames = nameWorkoutTitle.Where(x => x.Displayed).Select(x => x.Text).ToList();

            return workoutNames;
        }

        [AllureStep("Copy exercises")]
        public MembershipAdmin CopyExercises(List<string> programNames, List<DB.CopyMembershipPrograms> membershipData, string currentMembership)
        {
            foreach (string programName in programNames)
            {
                SelectPhase(programName);
                var workoutButtons = FindWorkoutButtons();
                var workoutNames = GetWorkoutNames(workoutButtons);
                foreach (string workoutName in workoutNames)
                {
                    CopyWorkoutMembership(workoutName, membershipData, currentMembership);
                }
                ClickBackButton(workoutNames.Count);
            }
            return this;
        }

        #region CopyExercises private methods
        private void SelectPhase(string programName)
        {
            InputBox.CbbxElement(cbbxPhaseName, 10, programName);
            WaitUntil.CustomElevemtIsVisible(Browser.FindElement($"//h3[text()='Program']/parent::div//span[@title='{programName}']"));
            WaitUntil.WaitSomeInterval(500);
        }

        private List<IWebElement> FindWorkoutButtons()
        {
            return btnAddExercises.Where(x => x.Displayed).ToList();
        }

        [AllureStep("Get Workout names")]
        private List<string> GetWorkoutNames(List<IWebElement> workoutButtons)
        {
            WaitUntil.CustomElevemtIsVisible(nameWorkoutTitleElem, 30);

            var workoutNames = nameWorkoutTitle.Where(x => x.Displayed).Select(x => x.Text).ToList();
            Button.Click(workoutButtons.FirstOrDefault());
            WaitUntil.CustomElevemtIsVisible(cbbxWorkoutsTitle);

            return workoutNames;
        }

        private void CopyWorkoutMembership(string workoutName, List<DB.CopyMembershipPrograms> membershipData, string currentMembership)
        {
            int randomIndex = RandomHelper.RandomNumFromOne(membershipData.Count);
            InputBox.CbbxElement(cbbxWorkoutsTitle, 20, workoutName + Keys.Enter);
            SwitcherHelper.SelectCopyMembership(membershipData[randomIndex].MembershipName, currentMembership);
            SwitcherHelper.SelectCopyProgram(membershipData[randomIndex].ProgramName);
            SwitcherHelper.SelectCopyWorkout(membershipData[randomIndex].WorkoutName);
            Button.Click(btnCopy);
            WaitUntil.CustomElevemtIsVisible(exerciseTitleRow);
        }

        private void ClickBackButton(int times)
        {
            for (int i = 0; i <= times; i++)
            {
                Button.Click(backBtn);
            }
            WaitUntil.WaitSomeInterval(1500);
        }

        #endregion

        [AllureStep("Add exercises")]
        public MembershipAdmin AddWorkoutExercises(List<string> programNames, List<DB.Exercises> exercises)
        {
            foreach (string programName in programNames)
            {
                InputBox.CbbxElement(cbbxPhaseName, 10, programName);
                WaitUntil.WaitSomeInterval(5000);
                var listWorkouts = btnAddExercises.Where(x => x.Displayed).ToList();
                List<string> workoutNames = GetWorkoutNames();
                Button.Click(btnAddExercisesElement);
                WaitUntil.CustomElevemtIsVisible(addExerciseBtn);
                Button.Click(addExerciseBtn);
                foreach (string workoutName in workoutNames)
                {
                    InputBox.CbbxElement(cbbxWorkoutsTitle, 20, workoutName + Keys.Enter);
                    
                    for (int i = 0; i <= 6; ++i)
                    {

                        if (i == 1)
                        {
                            InputBox.ElementCtrlA(seriesExerciseInput, 10, "A");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Count)].Name + Keys.Enter);
                            InputBox.ElementCtrlA(setsExerciseInput.FirstOrDefault(), 10, "6");
                            InputBox.ElementCtrlA(repsExerciseInput.FirstOrDefault(), 10, "4,4,4,5,7,8");
                            InputBox.ElementCtrlA(restExerciseInput.FirstOrDefault(), 10, "60");
                            InputBox.ElementCtrlA(tempoExerciseInput.FirstOrDefault(), 10, "20X0");
                            InputBox.ElementCtrlA(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));

                            Pages.CommonPages.Common.ClickSaveBtn();
                        }
                        else if (i == 2)
                        {
                            
                            InputBox.ElementCtrlA(seriesExerciseInput, 10, "B");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Count)].Name + Keys.Enter);
                            for(int q =0; q<3; q++)
                            {
                                Button.Click(addExerciseRowBtn);
                            }
                            for (int q=0; q<4; q++)
                            {
                                if (q == 0)
                                {
                                    InputBox.ElementCtrlA(setsExerciseInput[q], 10, "4");
                                    InputBox.ElementCtrlA(repsExerciseInput[q], 10, "10-12");
                                    InputBox.ElementCtrlA(restExerciseInput[q], 10, "30");
                                    InputBox.ElementCtrlA(tempoExerciseInput[q], 10, "2X10");
                                }
                                else if (q == 1)
                                {
                                    InputBox.ElementCtrlA(setsExerciseInput[q], 10, "3");
                                    InputBox.ElementCtrlA(repsExerciseInput[q], 10, "7-9");
                                    InputBox.ElementCtrlA(restExerciseInput[q], 10, "60");
                                    InputBox.ElementCtrlA(tempoExerciseInput[q], 10, "2010");
                                }
                                else if (q == 2)
                                {
                                    InputBox.ElementCtrlA(setsExerciseInput[q], 10, "5");
                                    InputBox.ElementCtrlA(repsExerciseInput[q], 10, "12-16");
                                    InputBox.ElementCtrlA(restExerciseInput[q], 10, "10");
                                    InputBox.ElementCtrlA(tempoExerciseInput[q], 10, "2010");
                                }
                                else if (q == 3)
                                {
                                    InputBox.ElementCtrlA(setsExerciseInput[q], 10, "7");
                                    InputBox.ElementCtrlA(repsExerciseInput[q], 10, "10-12");
                                    InputBox.ElementCtrlA(restExerciseInput[q], 10, "60");
                                    InputBox.ElementCtrlA(tempoExerciseInput[q], 10, "2010");
                                }

                            }
                            InputBox.ElementCtrlA(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));
                            Pages.CommonPages.Common.ClickSaveBtn();
                        }
                        else if (i == 3)
                        {
                            InputBox.ElementCtrlA(seriesExerciseInput, 10, "C1");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Count)].Name + Keys.Enter);
                            InputBox.ElementCtrlA(setsExerciseInput.FirstOrDefault(), 10, "7");
                            InputBox.ElementCtrlA(repsExerciseInput.FirstOrDefault(), 10, "15-20 Each");
                            InputBox.ElementCtrlA(restExerciseInput.FirstOrDefault(), 10, "60");
                            InputBox.ElementCtrlA(tempoExerciseInput.FirstOrDefault(), 10, "201X");
                            InputBox.ElementCtrlA(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));

                            Pages.CommonPages.Common.ClickSaveBtn();
                        }
                        else if (i == 4)
                        {

                            InputBox.ElementCtrlA(seriesExerciseInput, 10, "C2");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Count)].Name + Keys.Enter);
                            for (int q = 0; q < 3; q++)
                            {
                                Button.Click(addExerciseRowBtn);
                            }
                            for (int q = 0; q < 4; q++)
                            {
                                if (q == 0)
                                {
                                    InputBox.ElementCtrlA(setsExerciseInput[q], 10, "4");
                                    InputBox.ElementCtrlA(repsExerciseInput[q], 10, "3,4,5,6");
                                    InputBox.ElementCtrlA(restExerciseInput[q], 10, "30");
                                    InputBox.ElementCtrlA(tempoExerciseInput[q], 10, "3010");
                                }
                                else if (q == 1)
                                {
                                    InputBox.ElementCtrlA(setsExerciseInput[q], 10, "3");
                                    InputBox.ElementCtrlA(repsExerciseInput[q], 10, "5,5,5");
                                    InputBox.ElementCtrlA(restExerciseInput[q], 10, "60");
                                    InputBox.ElementCtrlA(tempoExerciseInput[q], 10, "3010");
                                }
                                else if (q == 2)
                                {
                                    InputBox.ElementCtrlA(setsExerciseInput[q], 10, "5");
                                    InputBox.ElementCtrlA(repsExerciseInput[q], 10, "6,8,9,7,3");
                                    InputBox.ElementCtrlA(restExerciseInput[q], 10, "10");
                                    InputBox.ElementCtrlA(tempoExerciseInput[q], 10, "3010");
                                }
                                else if (q == 3)
                                {
                                    InputBox.ElementCtrlA(setsExerciseInput[q], 10, "7");
                                    InputBox.ElementCtrlA(repsExerciseInput[q], 10, "4,4,4,5,7,8");
                                    InputBox.ElementCtrlA(restExerciseInput[q], 10, "5");
                                    InputBox.ElementCtrlA(tempoExerciseInput[q], 10, "3010");
                                }

                            }
                            InputBox.ElementCtrlA(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));

                            Pages.CommonPages.Common.ClickSaveBtn();
                        }
                        else if (i == 5)
                        {

                            InputBox.ElementCtrlA(seriesExerciseInput, 10, "D");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Count)].Name + Keys.Enter);
                            InputBox.ElementCtrlA(setsExerciseInput.FirstOrDefault(), 10, "6");
                            InputBox.ElementCtrlA(repsExerciseInput.FirstOrDefault(), 10, "4,4,4,5,7,8");
                            InputBox.ElementCtrlA(restExerciseInput.FirstOrDefault(), 10, "60");
                            InputBox.ElementCtrlA(tempoExerciseInput.FirstOrDefault(), 10, "2010");
                            InputBox.ElementCtrlA(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));

                            Pages.CommonPages.Common.ClickSaveBtn();
                        }
                        else if (i == 6)
                        {
                            InputBox.ElementCtrlA(seriesExerciseInput, 10, "E");
                            exercisesCbbxElem.SendKeys(exercises[RandomHelper.RandomExercise(exercises.Count)].Name + Keys.Enter);
                            InputBox.ElementCtrlA(setsExerciseInput.FirstOrDefault(), 10, "5");
                            InputBox.ElementCtrlA(repsExerciseInput.FirstOrDefault(), 10, "4,4,4,5,7");
                            InputBox.ElementCtrlA(restExerciseInput.FirstOrDefault(), 10, "60");
                            InputBox.ElementCtrlA(tempoExerciseInput.FirstOrDefault(), 10, "2111");
                            InputBox.ElementCtrlA(notesExerciseInput, 10, TextBox.GetAttribute(exercisesTitle, "title"));

                            Pages.CommonPages.Common.ClickSaveBtn();


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
            WaitUntil.CustomElevemtIsVisible(userCbbx);
            InputBox.ElementCtrlA(userCbbx, 60, email + Keys.Enter);

            Button.Click(addUserBtn);


            return this;
        }

        [AllureStep("Add next phases dependency")]
        public MembershipAdmin AddNextPhaseDependency()
        {
            WaitUntil.CustomElevemtIsVisible(nameProgramTitleElem);
            var programNames = nameProgramTitle.Where(x => x.Displayed).Select(x=>x).ToList();
            foreach(var program in programNames)
            {
                Button.Click(program);
                Button.Click(inputNextPhase);
                inputNextPhase.SendKeys(Keys.ArrowDown + Keys.Enter);
                Pages.CommonPages.Common.ClickSaveBtn();
                WaitUntil.WaitSomeInterval(500);
            }
            return this;
        }




    }
}
