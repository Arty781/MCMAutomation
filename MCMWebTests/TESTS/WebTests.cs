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
            Pages.SignUpUser
                .GoToSignUpPage();

            string email = RandomHelper.RandomEmail();
            Pages.SignUpUser
                .EnterData(email)
                .ClickOnSignUpBtn()
                .VerifyDisplayingPopUp()
                .GoToLoginPage();

            Pages.Login
                .GetLogin(email, Credentials.PASSWORD);
            Pages.MembershipUser
                .VerifyIsBuyBtnDisplayed();
            Pages.PopUp
                .ClosePopUp();
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            Pages.UserProfile
                .VerifyDisplayingReferringBtn();
            Pages.Login
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
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("PP-1");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string tier = null;
            string phase = null;
            string diet;
            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender
            
            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            
            Pages.Nutrition
                .SelectImperial();
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalPpOption[0];
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);
            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            diet = Pages.Nutrition.textActiveDiet.Text;
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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
            #region AdminActions

            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("PP-1");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string tier = null;
            string phase = null;
            string diet;
            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.Nutrition
                .SelectMetric();
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalPpOption[1];

            Pages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("PG");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            #region Variables

            string tier = null;
            string phase = null;
            string diet;
            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.Nutrition
                .SelectMetric();
            #endregion

            #region Select additional options

            Pages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.additionalPgOption);
            var textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.additionalPgOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.additionalPgOption)
                .ClickNextBtn()
                .Step02SelectMainTain();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            Pages.Nutrition
            .Step05SelectDiet2();
            diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("ARD");
            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            #region Variables

            string tier = string.Empty;
            string phase = string.Empty;
            string diet = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            string textOfMoreThan2KgSelected = string.Empty;
            double previousCalories = 0;

            #endregion

            #region FINDING YOUR ESTIMATED TDEE Page

            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            Pages.Nutrition
                .SelectMale();
            var selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            Pages.Nutrition
                .SelectMetric();
            Pages.Nutrition
                .ClickCalculateBtn();

            #endregion

            #region Steps
            double maintanceCalories = Pages.Nutrition.GetCalories();
            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectReverse();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn();
            SwitcherHelper.SelectNumberOfWeekForARD("1-2");
            WaitUntil.WaitSomeInterval(1500);
            phase = SwitcherHelper.GetTextOfSelectNumberOfWeekForARD();
            Pages.Nutrition
                .ClickNextBtn();
            previousCalories = double.Parse(TextBox.GetAttribute(Pages.Nutrition.inputPrevCalories, "value"));
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            #endregion

            Pages.Login
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

            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("ARD");
            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .RemoveAddedMembership()
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            #region Variables
            string tier = string.Empty;
            string phase = string.Empty;
            string diet = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            string textOfMoreThan2KgSelected = string.Empty;
            double previousCalories = 0;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region FINDING YOUR ESTIMATED TDEE Page
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            Pages.Nutrition
                .SelectFemale();
            var selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            Pages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString())
                .EnterBodyFat("15");
            Pages.Nutrition
                .ClickCalculateBtn();

            #endregion

            #region Steps
            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectReverse();

            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn();
            SwitcherHelper.SelectNumberOfWeekForARD("3-4");
            WaitUntil.WaitSomeInterval(1500);
            phase = SwitcherHelper.GetTextOfSelectNumberOfWeekForARD();
            Pages.Nutrition
                .ClickNextBtn();
            previousCalories = Pages.Nutrition.GetPreviousCalories(previousCalories, maintanceCalories);
            Pages.Nutrition
                .ClickNextBtn();
            textOfMoreThan2KgSelected = Pages.Nutrition.GetTextOnStep06();
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            #endregion

            Pages.Login
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
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();


            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.Nutrition
                .SelectImperial();
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier1();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCaloriesForCutTier2Phase3()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();


            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.Nutrition
                .SelectImperial();
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();

            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier2();

            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase3();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet2();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);
            Pages.Login
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

        public void VerifyCaloriesForCutTier3Phase1()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.Nutrition
                .SelectImperial();
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step03SelectTier3();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet2();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCaloriesForCutTier1Phase2()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.Nutrition
                .SelectImperial();
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase2();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet3();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCaloriesForBuildTier1Phase1()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender
            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.Nutrition
                .SelectImperial();
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier1();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCaloriesForBuildTier3Phase2()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.Nutrition
                .SelectImperial();
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCaloriesForBuildTier2Phase1()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.Nutrition
                .SelectImperial();
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier2();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCarbsAndFatsFor1000Calories()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            Pages.Nutrition
                .SelectMetric();
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn()
                .SetCalories();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .ClickNextBtn();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase3();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                 .ClickNextBtn();

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCaloriesDiet1()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.Nutrition
                .EnterAge("37");

            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            Pages.Nutrition
                .SelectFemale();

            var selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            Pages.Nutrition
                .SelectImperial();

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .ClickNextBtn();
            string goal = Pages.Nutrition.textActiveGoal.Text;

            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            string tier = Pages.Nutrition.textActiveTier.Text;

            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase3();
            string phase = Pages.Nutrition.textActivePhase.Text;

            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            string diet = Pages.Nutrition.textActiveDiet.Text;

            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCaloriesDiet2()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.Nutrition
                .EnterAge("37");
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            var selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .ClickNextBtn();
            string goal = Pages.Nutrition.textActiveGoal.Text;

            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            string tier = Pages.Nutrition.textActiveTier.Text;

            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase3();
            string phase = Pages.Nutrition.textActivePhase.Text;

            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            string diet = Pages.Nutrition.textActiveDiet.Text;

            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCaloriesDiet3()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("BBB1");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.Nutrition
                .EnterAge("37");
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            var selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            var selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(selectedAdditionalOption);
            var textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();
            double maintanceCalories = Pages.Nutrition.GetCalories();
            Pages.Nutrition
                .ClickNextBtn();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase3();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCalculationsForMaleWithMore25Fats()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name)
                .EnterEstimatedBodyFat("26");
            Pages.Common
                .ClickSaveBtn();
            WaitUntil.CustomElevemtIsVisible(Pages.UsersAdmin.inputSearch);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region FINDING YOUR ESTIMATED TDEE

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender
            Pages.Nutrition
                .SelectMale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            Pages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.additionalCommonOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.additionalCommonOption);

            #endregion

            
            Pages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString());

            Pages.Nutrition
                .ClickCalculateBtn();

            #endregion

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.additionalCommonOption)
                .ClickNextBtn()
                .Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier1();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet1();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCalculationsForMaleWith25Fats()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name)
                .EnterEstimatedBodyFat("25");
            Pages.Common
                .ClickSaveBtn();
            WaitUntil.CustomElevemtIsVisible(Pages.UsersAdmin.inputSearch);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectMale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            Pages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.additionalCommonOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.additionalCommonOption);

            #endregion

            Pages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString());

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.additionalCommonOption)
                .ClickNextBtn()
                .Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier2();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase2();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCalculationsForMaleWithLess25Fats()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name)
                .EnterEstimatedBodyFat("24");
            Pages.Common
                .ClickSaveBtn();
            WaitUntil.CustomElevemtIsVisible(Pages.UsersAdmin.inputSearch);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectMale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            Pages.Nutrition
                .SelectYesOfAdditionalOptions(AdditionalOptions.additionalCommonOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.additionalCommonOption);

            #endregion

            Pages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString());

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.additionalCommonOption)
                .ClickNextBtn()
                .Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCalculationsForFemaleWithMore35Fats()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin, 36);
            var user = AppDbContext.User.GetUserData(email);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.MEMBERSHIP_SKU[1]);
            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, user.Id);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, user.Id);

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = string.Empty;
            double previousCalories = 0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender
            
            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");

            #endregion

            #region Select additional options

            Pages.Nutrition
                .SelectNoOfAdditionalOptions(AdditionalOptions.additionalCommonOption);

            string textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(AdditionalOptions.additionalCommonOption);

            #endregion

            Pages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString())
                .EnterBodyFat("36");

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, AdditionalOptions.additionalCommonOption)
                .ClickNextBtn()
                .Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step03SelectTier3();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase3();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step05SelectDiet1();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCalculationsForFemaleWith35Fats()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name)
                .EnterEstimatedBodyFat("35");
            Pages.Common
                .ClickSaveBtn();
            WaitUntil.CustomElevemtIsVisible(Pages.UsersAdmin.inputSearch);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            var textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");

            Pages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString());

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase1();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
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

        public void VerifyCalculationsForFemaleWithLess35Fats()
        {
            #region AdminActions
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name)
                .EnterEstimatedBodyFat("34");
            Pages.Common
                .ClickSaveBtn();
            WaitUntil.CustomElevemtIsVisible(Pages.UsersAdmin.inputSearch);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select User data
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);

            #endregion

            #region Select gender

            Pages.Nutrition
                .SelectFemale();
            string selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption;
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(selectedAdditionalOption);

            var textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");

            Pages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString());

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase3();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn();
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region Postconditions

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
            var responseLoginUser = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User


            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            var exercises = AppDbContext.Exercises.GetExercisesData();
            for (int i = 0; i < 2; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(2);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id);
                var workouts = AppDbContext.Workouts.GetLastWorkoutsData();
                foreach (var workout in workouts)
                {
                    MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workout, exercises);
                }

            }
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #endregion

            Pages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.MembershipUser
                .OpenMembership();
            Pages.Sidebar
                .OpenMemberShipPageUser();

            for (int i = 0; i < programs.Count; i++)
            {
                Pages.MembershipUser
                    .OpenMembership()
                    .SelectPhaseAndWeek(i + 1, i + 1);
                int weekNum = Pages.MembershipUser.GetWeekNumber();
                for (int q = 0; q < weekNum; q++)
                {
                    Pages.MembershipUser
                        .SelectWeekNumber(q);
                    int countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
                    for (int j = 0; j < countWorkouts; j++)
                    {
                        Pages.MembershipUser
                            .OpenWorkout()
                            .AddWeight();
                        List<string> addedWeightList = Pages.MembershipUser.GetWeightData();
                        Pages.MembershipUser
                            .EnterNotes()
                            .ClickCompleteWorkoutBtn()
                            .OpenCompletedWorkout()
                            .VerifyAddedWeight(addedWeightList);
                        Pages.MembershipUser
                            .ClickBackBtn();
                    }
                }
                Pages.Sidebar
                    .OpenMemberShipPageUser();
            }
            Pages.Login
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

        public void CompleteFirstPhase()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User


            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            var exercises = AppDbContext.Exercises.GetExercisesData();
            for (int i = 0; i < 2; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(2);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id);
                var workouts = AppDbContext.Workouts.GetLastWorkoutsData();
                foreach (var workout in workouts)
                {
                    MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workout, exercises);
                }

            }
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            Pages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.MembershipUser
                .OpenMembership()
                .SelectPhaseAndWeek(1, 3);
            int countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
            for (int j = 0; j < countWorkouts; j++)
            {
                Pages.MembershipUser
                    .OpenWorkout()
                    .AddWeight();
                List<string> addedWeightList = Pages.MembershipUser.GetWeightData();
                Pages.MembershipUser
                    .EnterNotes()
                    .ClickCompleteWorkoutBtn()
                    .OpenCompletedWorkout()
                    .VerifyAddedWeight(addedWeightList);
                Pages.MembershipUser
                    .ClickBackBtn();
            }

            Pages.Login
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

            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            string userId = AppDbContext.User.GetUserData(email).Id;
            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membershipData = AppDbContext.Memberships.GetSubProdAndCustomMemberships();
            foreach(var membership in membershipData)
            {
                MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            }

            #endregion

            Pages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            SwitcherHelper.ActivateMembership(membershipData[0].Name);
            Pages.MembershipUser
                .ConfirmMembershipActivation()
                .OpenMembership()
                .SelectPhaseAndWeek(1, 1)
                .VerifyDisplayedDownloadBtn();
            Pages.MembershipUser
                .OpenMembershipPage();
            SwitcherHelper.ActivateMembership(membershipData[1].Name);
            Pages.MembershipUser
                .ConfirmMembershipActivation()
                .OpenMembership()
                .SelectPhaseAndWeek(1, 1)
                .VerifyDisplayedDownloadBtn();
            Pages.MembershipUser
                .OpenMembershipPage();
            SwitcherHelper.ActivateMembership(membershipData[2].Name);
            Pages.MembershipUser
                .ConfirmMembershipActivation()
                .OpenMembership()
                .SelectPhaseAndWeek(1, 1)
                .VerifyDisplayedDownloadBtn();

            Pages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }


        //[Test, Category("Memberships")]
        //[AllureTag("Regression")]
        //[AllureOwner("Artem Sukharevskyi")]
        //[AllureSeverity(SeverityLevel.critical)]
        //[Author("Artem", "qatester91311@gmail.com")]
        //[AllureSuite("Web")]
        //[AllureSubSuite("Memberships")]

        //public void CompleteMembershipsWithDataMega()
        //{

        //    string email = AppDbContext.GetUserEmail();

        //    #region AdminActions
        //    Pages.Login
        //        .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
        //    Pages.Sidebar
        //        .VerifyIsLogoDisplayed();

        //    var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("CP_TEST_SUB");

        //    Pages.PopUp
        //        .ClosePopUp();
        //    Pages.Sidebar
        //        .OpenUsersPage();
        //    Pages.UsersAdmin
        //        .SearchUser(email)
        //        .VerifyDisplayingOfUser(email)
        //        .ClickEditUser(email)
        //        .AddMembershipToUser(membership.Name)
        //        .SelectActiveMembership(membership.Name);
        //    Pages.Login
        //        .GetAdminLogout();

        //    #endregion

        //    Pages.Login
        //        .GetUserLogin(email, Credentials.password);
        //    Pages.MembershipUser
        //        .OpenMembership();

        //    int countPhases = Pages.MembershipUser.GetPhasesCount();

        //    Pages.Sidebar
        //            .OpenMemberShipPageUser();

        //    for (int i = 0; i < countPhases; i++)
        //    {
        //        Pages.MembershipUser
        //            .OpenMembership()
        //            .SelectPhase(i);

        //        int countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
        //        for (int j = 0; j < countWorkouts; j++)
        //        {
        //            Pages.MembershipUser
        //            .OpenWorkout()
        //            .AddWeight()
        //            .EnterNotes()
        //            .ClickCompleteWorkoutBtn();
        //        }
        //        Pages.MembershipUser
        //            .SelectWeekNumber2();
        //        countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
        //        for (int j = 0; j < countWorkouts; j++)
        //        {
        //            Pages.MembershipUser
        //            .OpenWorkout()
        //            .AddWeight()
        //            .EnterNotes()
        //            .ClickCompleteWorkoutBtn();
        //        }
        //        Pages.MembershipUser
        //            .SelectWeekNumber3();
        //        countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
        //        for (int j = 0; j < countWorkouts; j++)
        //        {
        //            Pages.MembershipUser
        //            .OpenWorkout()
        //            .AddWeight()
        //            .EnterNotes()
        //            .ClickCompleteWorkoutBtn();
        //        }
        //        Pages.MembershipUser
        //            .SelectWeekNumber4();
        //        countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
        //        for (int j = 0; j < countWorkouts; j++)
        //        {
        //            Pages.MembershipUser
        //            .OpenWorkout()
        //            .AddWeight()
        //            .EnterNotes()
        //            .ClickCompleteWorkoutBtn();
        //        }
        //        Pages.Sidebar
        //            .OpenMemberShipPageUser();

        //    }

        //    Pages.Login
        //        .GetUserLogout();

        //}


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
            SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);

            Pages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMyAccount();
            Pages.UserProfile
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
            ScreenShotHelper.MakeScreenShot();

            List<string> dataBeforeSaving = Pages.UserProfile.GetUserDataBeforeSaving();

            Pages.Common
                .ClickSaveBtn();
            Pages.Sidebar
                .OpenMyAccount();

            List<string> dataAfterSaving = Pages.UserProfile.GetUserDataBeforeSaving();
            Pages.UserProfile
                .VerifyUserData(dataBeforeSaving, dataAfterSaving);

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
            var responseLoginUser = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            #endregion

            #region Add and Activate membership to User
            string userId = AppDbContext.User.GetUserData(email).Id;
            DB.Memberships membershipId = AppDbContext.Memberships.GetLastMembership();
            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
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

            Pages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenProgressPage();
            for (int i = 0; i < 2; i++)
            {
                Pages.Progress
                    .ClickAddProgressBtnA()
                    .EnterWeight()
                    .EnterWaist()
                    .EnterChest()
                    .EnterArm()
                    .EnterHips()
                    .EnterThigh()
                    .AddImages();

                List<string> progressBeforeSaving = Pages.Progress.GetProgressData();

                Pages.Progress
                    .ClickSubmitBtn()
                    .VerifyAddedProgress(progressBeforeSaving);

                AppDbContext.Progress.UpdateUserProgressDate(userId);

                Browser._Driver.Navigate().Refresh();
                Pages.PopUp
                    .ClosePopUp();
            }
            Pages.Login
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
            var responseLoginUser = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            #endregion

            #region Add and Activate membership to User
            var userId = AppDbContext.User.GetUserData(email);
            DB.Memberships membershipId = AppDbContext.Memberships.GetLastMembership();
            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipId.Id, userId.Id);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId.Id);
            #endregion

            #endregion

            Pages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenProgressPage();
            Pages.Progress
                .ClickAddProgressBtnA()
                .EnterWeight()
                .EnterWaist()
                .EnterThigh()
                .EnterChest()
                .EnterArm()
                .EnterHips();

            List<string> progressBeforeSaving = Pages.Progress.GetProgressData();

            Pages.Progress
                .ClickSubmitBtn()
                .VerifyAddedProgress(progressBeforeSaving);
            Pages.Login
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
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("MCM_BIKINI_SUB");
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership.Name)
                .SelectActiveMembership(membership.Name);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLogin(email, Credentials.PASSWORD);
            Pages.Sidebar
                .OpenProgressPage();
            Pages.Progress
                .ClickAddProgressBtnA()
                .EnterWeight()
                .EnterWaist()
                .EnterChest()
                .EnterArm()
                .EnterHips()
                .EnterThigh();

            List<string> progressBeforeSaving = Pages.Progress.GetProgressData();

            Pages.Progress
                .ClickSubmitBtn()
                .VerifyAddedProgress(progressBeforeSaving);

            Pages.Progress
                .ClickEditProgressBtnA()
                .EnterWeight()
                .EnterWaist()
                .EnterThigh()
                .EnterChest()
                .EnterArm()
                .EnterHips();

            progressBeforeSaving = Pages.Progress.GetProgressData();

            Pages.Progress
                .ClickSubmitBtn()
                .VerifyAddedProgress(progressBeforeSaving);
            Pages.Login
                .GetUserLogout();

            #region Postconditions

            AppDbContext.User.DeleteUser(email);

            #endregion

        }


        #endregion

    }
}
