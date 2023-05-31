using Allure.Commons;
using MCMAutomation.PageObjects;
using MCMWebTests.BASE;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using MCMAutomation.Helpers;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using RimuTec.Faker;
using MCMAutomation.APIHelpers.Client.EditUser;
using MCMAutomation.APIHelpers;
using MCMAutomation.APIHelpers.Client.SignUp;
using MCMAutomation.APIHelpers.Client.AddProgress;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MCMAutomation.WebTests
{
    [TestFixture]
    [AllureNUnit]
    public class Register : TestBaseWeb
    {
        #region Register

        [Test, Category("Register")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Register")]

        public void RegisterNewUser()
        {
            Pages.WebPages.SignUpUser
            .GoToSignUpPage();

            string email = RandomHelper.RandomEmail();
            Pages.WebPages.SignUpUser
                .EnterData(email)
                .ClickOnSignUpBtn()
                .VerifyDisplayingPopUp()
                .GoToLoginPage();

            Pages.CommonPages.Login
                .GetLogin(email, Credentials.PASSWORD);
            Pages.WebPages.MembershipUser
                .VerifyIsBuyBtnDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            var responseLogin = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            Pages.WebPages.UserProfile
                .VerifyDisplayingReferringBtn();
            Pages.CommonPages.Login
                .GetUserLogout();
        }


        #endregion
    }

    [TestFixture]
    [AllureNUnit]
    public class Tdee : TestBaseWeb
    {
        #region TDEE

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void CheckTdeeForPP1ForFemale()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("PP-1");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Selected Activity lvl
            
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Selected gender

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            #endregion

            #region Select Conversion System
            
            Pages.WebPages.Nutrition
                .SelectImperial();

            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_PP_OPTION[0];
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);
            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void CheckTdeeForPP1ForFemaleSecondOption()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User


            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("PP-1");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl

            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            #endregion

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectMetric();
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_PP_OPTION[1];

            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void CheckTdeeForPGForFemale()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User


            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("PG");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectMetric();
            #endregion

            #region Select additional options

            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.ADDITIONAL_PG_OPTION);
            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.ADDITIONAL_PG_OPTION);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.ADDITIONAL_PG_OPTION)
                .ClickNextBtn()
                .Step02SelectMainTain();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            Pages.WebPages.Nutrition
            .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]
        public void CheckTdeeForARDForMale()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User


            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("ARD");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region FINDING YOUR ESTIMATED TDEE Page

            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;
            Pages.WebPages.Nutrition
                .SelectMale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            Pages.WebPages.Nutrition
                .SelectMetric();
            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #endregion

            #region Steps
            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();
            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectReverse();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            SwitcherHelper.SelectNumberOfWeekForARD("1-2");
            WaitUntil.WaitSomeInterval(1500);
            phase = SwitcherHelper.GetTextOfSelectNumberOfWeekForARD();
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            previousCalories = double.Parse(TextBox.GetAttribute(Pages.WebPages.Nutrition.inputPrevCalories, "value"));
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]
        public void CheckTdeeForARDForFemaleWeek2()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("ARD");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            level = Pages.WebPages.Nutrition.SelectedLevel();
            selectedGender = Pages.WebPages.Nutrition.Selectedgender();

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #region Steps

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectReverse();

            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            SwitcherHelper.SelectNumberOfWeekForARD("3-4");
            WaitUntil.WaitSomeInterval(1500);
            phase = SwitcherHelper.GetTextOfSelectNumberOfWeekForARD();
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            previousCalories = Pages.WebPages.Nutrition.GetPreviousCalories(previousCalories, maintanceCalories);
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            textOfMoreThan2KgSelected = Pages.WebPages.Nutrition.GetTextOnStep06();
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        #region Checks for Carbs, Fats, Proteins

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCaloriesForCutTier1Phase1()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Conversion System

            Pages.WebPages.Nutrition
                .SelectImperial();

            #endregion

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier1();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        //[Test, Category("TDEE")]
        //[AllureTag("Regression")]
        //[AllureOwner("Artem Sukharevskyi")]
        //[AllureSeverity(SeverityLevel.critical)]
        //[Author("Artem", "qatester91311@gmail.com")]
        //[AllureSuite("Web")]
        //[AllureSubSuite("TDEE")]

        //public void VerifyCaloriesForCutTier2Phase3()
        //{
        //    #region Preconditions

        //    #region Register New User
        //    string email = RandomHelper.RandomEmail();
        //    SignUpRequest.RegisterNewUser(email);
        //    var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
        //    EditUserRequest.EditUser(responseLoginUser);
        //    string userId = AppDbContext.User.GetUserData(email).Id;
        //    #endregion

        //    #region Add and Activate membership to User

        //    MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1]);
        //    var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
        //    var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
        //    MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
        //    int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
        //    MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
        //    #endregion

        //    #region Variables

        //    string tier = String.Empty;
        //    string phase = String.Empty;
        //    string diet = String.Empty;
        //    string textOfMoreThan2KgSelected = String.Empty;
        //    double previousCalories = 0.0;
        //    string level = string.Empty;
        //    var userData = AppDbContext.User.GetUserData(email);
        //    var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
        //    string selectedGender = string.Empty;
        //    string selectedAdditionalOption = string.Empty;
        //    string textSelectedAdditionalOptions = string.Empty;
        //    double maintanceCalories = 0d;
        //    string goal = string.Empty;
        //    double expectedCalories = 0d;
        //    var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

        //    #endregion

        //    #endregion

        //    Pages.CommonPages.Login
        //        .GetUserLoginForTdee(email, Credentials.PASSWORD);
        //    Pages.CommonPages.Sidebar
        //        .VerifyIsLogoDisplayed()
        //        .VerifyEmailDisplayed(email);
        //    Pages.CommonPages.PopUp
        //        .ClosePopUp();
        //    Pages.CommonPages.Sidebar
        //        .OpenNutritionPage();

        //    #region Select Activity lvl
            
        //    level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

        //    #endregion

        //    #region Select gender

        //    selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
        //    #endregion

        //    #region Select Conversion System
        //    Pages.WebPages.Nutrition
        //        .SelectImperial();
        //    #endregion

        //    #region Select additional options

        //    selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
        //    Pages.WebPages.Nutrition
        //        .SelectYesOfAdditionalOptions(selectedAdditionalOption);

        //    textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

        //    #endregion

        //    Pages.WebPages.Nutrition
        //        .ClickCalculateBtn();

        //    #region TDEE Steps

        //    maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

        //    Pages.WebPages.Nutrition
        //        .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
        //        .ClickNextBtn()
        //        .Step02SelectCut();

        //    goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
        //    Pages.WebPages.Nutrition
        //        .ClickNextBtn()
        //        .Step03SelectTier2();

        //    tier = Pages.WebPages.Nutrition.textActiveTier.Text;
        //    Pages.WebPages.Nutrition
        //        .ClickNextBtn()
        //    .Step04SelectPhase3();
        //    phase = Pages.WebPages.Nutrition.textActivePhase.Text;
        //    Pages.WebPages.Nutrition
        //        .ClickNextBtn()
        //    .Step05SelectDiet2();
        //    diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
        //    Pages.WebPages.Nutrition
        //        .ClickNextBtn();
        //    expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
        //    Pages.WebPages.Nutrition
        //        .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);
        //    #endregion

        //    Pages.CommonPages.Login
        //        .GetUserLogout();

        //    #region Postconditions

        //    AppDbContext.Memberships.DeleteMembership(membershipData.Name);
        //    AppDbContext.User.DeleteUser(email);

        //    #endregion
        //}


        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCaloriesForCutTier3Phase1()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectImperial();
            #endregion

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .Step04SelectPhase1()
                .ClickNextBtn();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCaloriesForCutTier1Phase2()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectImperial();
            #endregion

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCaloriesForBuildTier1Phase1()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 20, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl

            Pages.WebPages.Nutrition
                .SelectActivityLevel(2);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectMale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectMetric();
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier1();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCaloriesForBuildTier3Phase2()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectImperial();
            #endregion

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCaloriesForBuildTier2Phase1()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectImperial();
            #endregion

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier2();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }


        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCarbsAndFatsFor1000Calories()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectMetric();
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn()
                .SetCalories();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                 .ClickNextBtn();

            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCaloriesDiet1()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.WebPages.Nutrition
                .EnterAge("37");

            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;
            

            Pages.WebPages.Nutrition
                .SelectFemale();

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            Pages.WebPages.Nutrition
                .SelectImperial();

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCaloriesDiet2()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.WebPages.Nutrition
                .EnterAge("37");
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;
            
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCaloriesDiet3()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("BBB1");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.WebPages.Nutrition
                .EnterAge("37");
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;
            
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);
            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();
            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCalculationsForMaleWithMore25Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 35, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region FINDING YOUR ESTIMATED TDEE

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender
            Pages.WebPages.Nutrition
                .SelectMale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            #endregion

            
            Pages.WebPages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString());

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #endregion

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier1();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCalculationsForMaleWith25Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 25, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl
            
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #region TDEE Steps

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier2();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);
            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCalculationsForMaleWithLess25Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl
            
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #region TDEE Steps

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);
            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCalculationsForFemaleWithMore35Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 36, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1],eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl
            
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender
            
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            #endregion

            #region Select additional options

            Pages.WebPages.Nutrition
                .SelectNoOfAdditionalOptions(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #region TDEE Steps

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet1();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);
            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCalculationsForFemaleWith30Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 30, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1],eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");

            Pages.WebPages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString());

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions


            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE")]

        public void VerifyCalculationsForFemaleWithLess35Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl
            
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();
            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();
            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        #endregion

        #endregion
    }

    [TestFixture]
    [AllureNUnit]
    public class Tdee8Weeks : TestBaseWeb
    {
        #region TDEE

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void CheckTdeeForPP1ForFemale()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("PP-1");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Selected Activity lvl

            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Selected gender

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            #endregion

            #region Select Conversion System

            Pages.WebPages.Nutrition
                .SelectImperial();

            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_PP_OPTION[0];
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);
            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void CheckTdeeForPP1ForFemaleSecondOption()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User


            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("PP-1");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl

            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            #endregion

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectMetric();
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_PP_OPTION[1];

            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void CheckTdeeForPGForFemale()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User


            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("PG");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectMetric();
            #endregion

            #region Select additional options

            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.ADDITIONAL_PG_OPTION);
            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.ADDITIONAL_PG_OPTION);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.ADDITIONAL_PG_OPTION)
                .ClickNextBtn()
                .Step02SelectMainTain();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            Pages.WebPages.Nutrition
            .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]
        public void CheckTdeeForARDForMale()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User


            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("ARD");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region FINDING YOUR ESTIMATED TDEE Page

            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;
            Pages.WebPages.Nutrition
                .SelectMale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            Pages.WebPages.Nutrition
                .SelectMetric();
            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #endregion

            #region Steps
            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();
            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectReverse();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            SwitcherHelper.SelectNumberOfWeekForARD("1-2");
            WaitUntil.WaitSomeInterval(1500);
            phase = SwitcherHelper.GetTextOfSelectNumberOfWeekForARD();
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            previousCalories = double.Parse(TextBox.GetAttribute(Pages.WebPages.Nutrition.inputPrevCalories, "value"));
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]
        public void CheckTdeeForARDForFemaleWeek2()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("ARD");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            level = Pages.WebPages.Nutrition.SelectedLevel();
            selectedGender = Pages.WebPages.Nutrition.Selectedgender();

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #region Steps

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectReverse();

            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            SwitcherHelper.SelectNumberOfWeekForARD("3-4");
            WaitUntil.WaitSomeInterval(1500);
            phase = SwitcherHelper.GetTextOfSelectNumberOfWeekForARD();
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            previousCalories = Pages.WebPages.Nutrition.GetPreviousCalories(previousCalories, maintanceCalories);
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            textOfMoreThan2KgSelected = Pages.WebPages.Nutrition.GetTextOnStep06();
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        #region Checks for Carbs, Fats, Proteins

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCaloriesForCutTier1Phase1()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Conversion System

            Pages.WebPages.Nutrition
                .SelectImperial();

            #endregion

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier1();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCaloriesForCutTier3Phase1()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectImperial();
            #endregion

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .Step04SelectPhase1()
                .ClickNextBtn();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCaloriesForCutTier1Phase2()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectImperial();
            #endregion

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCaloriesForBuildTier1Phase1()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 20, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl

            Pages.WebPages.Nutrition
                .SelectActivityLevel(2);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectMale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectMetric();
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier1();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCaloriesForBuildTier3Phase2()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectImperial();
            #endregion

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCaloriesForBuildTier2Phase1()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectImperial();
            #endregion

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier2();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }


        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCarbsAndFatsFor1000Calories()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.WebPages.Nutrition
                .SelectFemale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.WebPages.Nutrition
                .SelectMetric();
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn()
                .SetCalories();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                 .ClickNextBtn();

            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCaloriesDiet1()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.WebPages.Nutrition
                .EnterAge("37");

            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;


            Pages.WebPages.Nutrition
                .SelectFemale();

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            Pages.WebPages.Nutrition
                .SelectImperial();

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCaloriesDiet2()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.WebPages.Nutrition
                .EnterAge("37");
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;

            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCaloriesDiet3()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("BBB1");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.WebPages.Nutrition
                .EnterAge("37");
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);
            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();
            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCalculationsForMaleWithMore25Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 35, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region FINDING YOUR ESTIMATED TDEE

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender
            Pages.WebPages.Nutrition
                .SelectMale();
            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            #endregion


            Pages.WebPages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString());

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #endregion

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier1();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCalculationsForMaleWith25Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 25, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl

            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #region TDEE Steps

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier2();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);
            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCalculationsForMaleWithLess25Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl

            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #region TDEE Steps

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);
            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCalculationsForFemaleWithMore35Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 36, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl

            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            #endregion

            #region Select additional options

            Pages.WebPages.Nutrition
                .SelectNoOfAdditionalOptions(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.ADDITIONAL_COMMON_OPTION);

            #endregion

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            #region TDEE Steps

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet1();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);
            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCalculationsForFemaleWith30Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 30, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl
            Pages.WebPages.Nutrition
                .SelectActivityLevel(0);
            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");

            Pages.WebPages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString());

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();

            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();

            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions


            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        [Test, Category("TDEE")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("TDEE, 8-Week membership")]

        public void VerifyCalculationsForFemaleWithLess35Fats()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = true;
            MembershipRequest.CreateProductMembership(MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CMC_TEST_PRODUCT");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = String.Empty;
            string phase = String.Empty;
            string diet = String.Empty;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = string.Empty;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = string.Empty;
            double expectedCalories = 0d;
            var weekNumber = Pages.WebPages.Nutrition.GetNumberOfWeeks(email);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenNutritionPage();

            #region Select Activity lvl

            level = Pages.WebPages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            selectedAdditionalOption = AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            Pages.WebPages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");

            Pages.WebPages.Nutrition
                .ClickCalculateBtn();
            maintanceCalories = Pages.WebPages.Nutrition.GetCalories();
            Pages.WebPages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            goal = Pages.WebPages.Nutrition.textActiveGoal.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            tier = Pages.WebPages.Nutrition.textActiveTier.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            phase = Pages.WebPages.Nutrition.textActivePhase.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.WebPages.Nutrition.textActiveDiet.Text;
            Pages.WebPages.Nutrition
                .ClickNextBtn();
            expectedCalories = Pages.WebPages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.WebPages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories, weekNumber);

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion
        }

        #endregion

        #endregion
    }

    [TestFixture]
    [AllureNUnit]
    public class Memberships : TestBaseWeb
    {

        #region Complete workouts

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void CompleteMembershipWithData()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var lastmemberId = AppDbContext.Memberships.GetLastMembership().Id;
            AppDbContext.Memberships.Insert.InsertMembership(lastmemberId, MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            const int programCount = 3;
            var programs = MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData, programCount);
            var workouts = MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed()
                .VerifyEmailDisplayed(email);
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.WebPages.MembershipUser
                .OpenMembership();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPageUser();
            Pages.WebPages.MembershipUser
                .SelectPhaseAndWeekAndEnterWeight(programs.Count);
            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            //AppDbContext.User.DeleteUser(email);

            #endregion

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void CompleteFirstPhase()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var lastmemberId = AppDbContext.Memberships.GetLastMembership().Id;
            AppDbContext.Memberships.Insert.InsertMembership(lastmemberId, MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            const int programCount = 3;
            var programs = MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData, programCount);
            var workouts = MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion

            Pages.CommonPages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.WebPages.MembershipUser
                .OpenMembership()
                .SelectPhaseAndWeek(1, 3);
            int countWorkouts = Pages.WebPages.MembershipUser.GetWorkoutsCount();
            for (int j = 0; j < countWorkouts; j++)
            {
                Pages.WebPages.MembershipUser
                    .OpenWorkout()
                    .AddWeight();
                List<string> addedWeightList = Pages.WebPages.MembershipUser.GetWeightData();
                Pages.WebPages.MembershipUser
                    .EnterNotes()
                    .ClickCompleteWorkoutBtn()
                    .OpenCompletedWorkout()
                    .VerifyAddedWeights(addedWeightList);
                Pages.WebPages.MembershipUser
                    .ClickBackBtn();
            }

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);
            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyDisplayingOfDownloadReportBtn()
        {
            #region Preconditions

            #region Create User

            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            string userId = AppDbContext.User.GetUserData(email).Id;

            #endregion

            #region Create Product membership

            bool eightWeeks = false;
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var lastmemberId = AppDbContext.Memberships.GetLastMembership().Id;
            AppDbContext.Memberships.Insert.InsertMembership(lastmemberId, MembershipsSKU.MEMBERSHIP_SKU[1], eightWeeks);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            const int programCount = 3;
            var programs = MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData, programCount);
            var workouts = MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);

            #endregion

            #region Create Custom membership

            MembershipRequest.CreateCustomMembership(responseLoginAdmin, userId);
            membershipData = AppDbContext.Memberships.GetLastMembership();
            programs = MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData, programCount);
            workouts = MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);

            #endregion

            #region Create Subscription membership

            lastmemberId = AppDbContext.Memberships.GetLastMembership().Id;
            AppDbContext.Memberships.Insert.InsertMembership(lastmemberId, MembershipsSKU.MEMBERSHIP_SKU[2], eightWeeks);
            membershipData = AppDbContext.Memberships.GetLastMembership();
            programs = MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData, programCount);
            workouts = MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);

            #endregion

            #region Add memberships to user

            var membershipList = AppDbContext.Memberships.GetListOfLastMembershipsByType();
            foreach(var membership in membershipList)
            {
                MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            }

            #endregion

            #endregion

            #region Steps

            Pages.CommonPages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            SwitcherHelper.ActivateMembership(membershipList[0].Name);
            Pages.WebPages.MembershipUser
                .ConfirmMembershipActivation()
                .OpenMembership()
                .SelectPhaseAndWeek(1, 1)
                .VerifyDisplayedDownloadBtn();
            Pages.WebPages.MembershipUser
                .OpenMembershipPage();
            SwitcherHelper.ActivateMembership(membershipList[1].Name);
            Pages.WebPages.MembershipUser
                .ConfirmMembershipActivation()
                .OpenMembership()
                .SelectPhaseAndWeek(1, 1)
                .VerifyDisplayedDownloadBtn();
            Pages.WebPages.MembershipUser
                .OpenMembershipPage();
            SwitcherHelper.ActivateMembership(membershipList[2].Name);
            Pages.WebPages.MembershipUser
                .ConfirmMembershipActivation()
                .OpenMembership()
                .SelectPhaseAndWeek(1, 1)
                .VerifyDisplayedDownloadBtn();

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

            foreach (var member in membershipList)
            {
                AppDbContext.Memberships.DeleteMembership(member.Name);
            }
            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        #endregion

    }

    [TestFixture]
    [AllureNUnit]
    public class UserProfile : TestBaseWeb
    {
        #region User profile

        [Test, Category("UserProfile")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("UserProfile")]

        public void EditUser()
        {
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            SignInRequest.MakeSignIn(email, Credentials.PASSWORD);

            Pages.CommonPages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMyAccount(email);
            Pages.WebPages.UserProfile
                .AddFirstName()
                .AddLastName()
                .EnterDOB()
                .EnterProteins()
                .EnterCalories()
                .EnterMaintenanceCalories()
                .EnterCarbs()
                .EnterFats()
                .EnterHeight()
                .EnterWeight()
                .EnterNewEmail()
                .EnterEstimatedBodyFat("26");

            List<string> expectedData = Pages.WebPages.UserProfile.GetUserDataBeforeSaving();

            Pages.CommonPages.Common
                .ClickSaveBtn();
            Pages.CommonPages.Sidebar
                .OpenMyAccount(expectedData.LastOrDefault());

            List<string> actualData = Pages.WebPages.UserProfile.GetUserDataAfterSaving();
            Pages.WebPages.UserProfile
                .VerifyUserData(expectedData, actualData);

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        #endregion

    }

    [TestFixture]
    [AllureNUnit]
    public class Progress : TestBaseWeb
    {
        #region Progress

        [Test, Category("Progress")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Progress")]

        public void AddProgressToCheckingTheGraph()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            #endregion

            #region Add and Activate membership to User
            string userId = AppDbContext.User.GetUserData(email).Id;
            DB.Memberships membershipId = AppDbContext.Memberships.GetLastMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipId.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Add Progress as User
            for (int i = 0; i < 60; i++)
            {
                ProgressRequest.AddProgress(responseLoginUser);
                AppDbContext.Progress.UpdateUserProgressDate(userId);
            }
            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .OpenProgressPage();
            for (int i = 0; i < 15; i++)
            {
                Pages.WebPages.Progress
                    .ClickAddProgressBtnA()
                    .EnterWeight()
                    .EnterWaist()
                    .EnterChest()
                    .EnterArm()
                    .EnterHips()
                    .EnterThigh()
                    .AddImages();

                List<string> progressBeforeSaving = Pages.WebPages.Progress.GetProgressData();

                Pages.WebPages.Progress
                    .ClickSubmitBtn()
                    .VerifyAddedProgress(progressBeforeSaving);

                AppDbContext.Progress.UpdateUserProgressDate(userId);

                Browser._Driver.Navigate().Refresh();
                Pages.CommonPages.PopUp
                    .ClosePopUp();
            }
            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }

        [Test, Category("Progress")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Progress")]

        public void AddProgress()
        {
            #region Preconditions

            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            #endregion

            #region Add and Activate membership to User
            var userId = AppDbContext.User.GetUserData(email);
            DB.Memberships membershipId = AppDbContext.Memberships.GetLastMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipId.Id, userId.Id);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId.Id);
            #endregion

            #endregion

            Pages.CommonPages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .OpenProgressPage();
            Pages.WebPages.Progress
                .ClickAddProgressBtnA()
                .EnterWeight()
                .EnterWaist()
                .EnterThigh()
                .EnterChest()
                .EnterArm()
                .EnterHips();

            List<string> progressBeforeSaving = Pages.WebPages.Progress.GetProgressData();

            Pages.WebPages.Progress
                .ClickSubmitBtn()
                .VerifyAddedProgress(progressBeforeSaving);
            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion


        }

        [Test, Category("Progress")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Progress")]

        public void EditProgress()
        {
            #region Preconditions

            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            string userId = AppDbContext.User.GetUserData(email).Id;
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("MCM_BIKINI_SUB");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion

            Pages.CommonPages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenProgressPage();
            Pages.WebPages.Progress
                .ClickAddProgressBtnA()
                .EnterWeight()
                .EnterWaist()
                .EnterChest()
                .EnterArm()
                .EnterHips()
                .EnterThigh();

            List<string> progressBeforeSaving = Pages.WebPages.Progress.GetProgressData();

            Pages.WebPages.Progress
                .ClickSubmitBtn()
                .VerifyAddedProgress(progressBeforeSaving);

            Pages.WebPages.Progress
                .ClickEditProgressBtnA()
                .EnterWeight()
                .EnterWaist()
                .EnterThigh()
                .EnterChest()
                .EnterArm()
                .EnterHips();

            progressBeforeSaving = Pages.WebPages.Progress.GetProgressData();

            Pages.WebPages.Progress
                .ClickSubmitBtn()
                .VerifyAddedProgress(progressBeforeSaving);
            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }


        #endregion

    }
}
