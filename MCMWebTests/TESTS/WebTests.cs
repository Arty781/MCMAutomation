﻿using Allure.Commons;
using MCMAutomation.PageObjects;
using MCMWebTests.BASE;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace MCMAutomation.WebTests
{
    [TestFixture]
    [AllureNUnit]
    public class WebTests : TestBaseWeb
    {
        #region Register

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void RegisterNewUser()
        {
            Pages.SignUpUser
                .GoToSignUpPage();

            string email = RandomHelper.RandomEmail();
            Pages.SignUpUser
                .EnterData(email)
                .ClickOnSignUpBtn()
                .VerifyDisplayingPopUp()
                .GoToLoginPage();

            Pages.Login
                .GetLogin(email, Credentials.password);
            Pages.MembershipUser
                .VerifyIsBuyBtnDisplayed();
        }


        #endregion

        #region Complete workouts

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void CompleteMembershipsWithData()
        {
            
           string[] email = AppDbContext.GetUsersData();
            
            Pages.Login
                .GetUserLogin(email, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.MembershipUser
                .OpenMembership()
                .SelectPhase()
                .SelectWeekNumber()
                .OpenWorkouts()
                .AddWeight()
                .ClickCompleteWorkoutBtn();
            
        }


        #endregion


        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void CheckTdeeForPP1ForFemale()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("PP-1");

            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.MembershipAdmin
                .SearchUser(Credentials.login)
                .VerifyDisplayingOfUser(Credentials.login)
                .EditUser(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectImperial(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalPpOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn();
            //.Step02SelectCut();
            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            //.Step03SelectTier1();
            //WaitUntil.WaitSomeInterval(1500);
            //string tier = Pages.Nutrition.textActiveTier.Text;
            //Pages.Nutrition
            //    .ClickNextBtn();
            ////.Step04SelectPhase1();
            //WaitUntil.WaitSomeInterval(1500);
            //string phase = Pages.Nutrition.textActivePhase.Text;
            //Pages.Nutrition
            //    .ClickNextBtn();
            //.Step05SelectDiet1();
            WaitUntil.WaitSomeInterval(1500);
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFat(userData, diet);

            double fat = Pages.Nutrition.GetFat(userData, diet);

            Pages.Nutrition
                .VerifyCarbs(protein, fat, expectedCalories);

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.MembershipAdmin
                .SearchUser(Credentials.login)
                .VerifyDisplayingOfUser(Credentials.login)
                .DeleteMemebershipFromUser();
            Pages.Login
                .GetAdminLogout();

            #endregion

        }

        #region Checks for Carbs, Fats, Proteins

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForCutTier1Phase1()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectImperial(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];
            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);

            Pages.Nutrition
                .SelectYesOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn();
            //.Step02SelectCut();
            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            //.Step03SelectTier1();
            WaitUntil.WaitSomeInterval(1500);
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn();
            //.Step04SelectPhase1();
            WaitUntil.WaitSomeInterval(1500);
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn();
            //.Step05SelectDiet1();
            WaitUntil.WaitSomeInterval(1500);
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFat(userData, diet);

            double fat = Pages.Nutrition.GetFat(userData, diet);

            Pages.Nutrition
                .VerifyCarbs(protein, fat, expectedCalories);
        }

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForCutTier2Phase3()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectImperial(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn();
            //.Step02SelectCut();
            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step03SelectTier2();
            WaitUntil.WaitSomeInterval(1500);
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase3();
            WaitUntil.WaitSomeInterval(1500);
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet2();
            WaitUntil.WaitSomeInterval(1500);
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFat(userData, diet);

            double fat = Pages.Nutrition.GetFat(userData, diet);

            Pages.Nutrition
                .VerifyCarbs(protein, fat, expectedCalories);
        }


        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForCutTier3Phase1()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectImperial(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn();
            //.Step02SelectCut();
            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step03SelectTier3();
            WaitUntil.WaitSomeInterval(1500);
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn();
            WaitUntil.WaitSomeInterval(1500);
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet2();
            WaitUntil.WaitSomeInterval(1500);
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFat(userData, diet);

            double fat = Pages.Nutrition.GetFat(userData, diet);

            Pages.Nutrition
                .VerifyCarbs(protein, fat, expectedCalories);
        }

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForCutTier1Phase2()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectImperial(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn();
            //.Step02SelectCut();
            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            WaitUntil.WaitSomeInterval(1500);
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase2();
            WaitUntil.WaitSomeInterval(1500);
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet3();
            WaitUntil.WaitSomeInterval(1500);
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFat(userData, diet);

            double fat = Pages.Nutrition.GetFat(userData, diet);

            Pages.Nutrition
                .VerifyCarbs(protein, fat, expectedCalories);
        }

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForBuildTier1Phase1()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectImperial(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            //.Step03SelectTier1();
            WaitUntil.WaitSomeInterval(1500);
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn();
            //.Step04SelectPhase3();
            WaitUntil.WaitSomeInterval(1500);
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn();
            //.Step05SelectDiet1();
            WaitUntil.WaitSomeInterval(1500);
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFat(userData, diet);

            double fat = Pages.Nutrition.GetFat(userData, diet);

            Pages.Nutrition
                .VerifyCarbs(protein, fat, expectedCalories);
        }

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForBuildTier3Phase2()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectImperial(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            WaitUntil.WaitSomeInterval(1500);
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            WaitUntil.WaitSomeInterval(1500);
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            WaitUntil.WaitSomeInterval(1500);
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFat(userData, diet);

            double fat = Pages.Nutrition.GetFat(userData, diet);

            Pages.Nutrition
                .VerifyCarbs(protein, fat, expectedCalories);
        }

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForBuildTier2Phase1()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectImperial(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier2();
            WaitUntil.WaitSomeInterval(1500);
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn();
            //.Step04SelectPhase3();
            WaitUntil.WaitSomeInterval(1500);
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            WaitUntil.WaitSomeInterval(1500);
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                 .ClickNextBtn()
                 .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1]);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFat(userData, diet);

            double fat = Pages.Nutrition.GetFat(userData, diet);

            Pages.Nutrition
                .VerifyCarbs(protein, fat, expectedCalories);
        }

        #endregion
    }
}
