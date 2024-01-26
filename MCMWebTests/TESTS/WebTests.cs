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
using static MCMAutomation.Helpers.TDEE;

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
            string email = $"qatesterOutsite@xitroo.com"; //RandomHelper.RandomEmail();
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
            EditUserRequest
                .EditUser(responseLogin);
            Pages.WebPages.UserProfile
                .VerifyDisplayingReferringBtn();
            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

            //AppDbContext.User.DeleteUser(email);

            #endregion
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
            var bodyFat = decimal.Parse("15."+ RandomHelper.RandomNumFromOne(99));
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, bodyFat, UserAccount.FEMALE);
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

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.PpOptions.BREASTFEEDING_LESS;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectImperial()
                .SelectActivityLevel(ActivityLevelNumber.SEDETARY, out level)
                .SelectFemale(out selectedGender)
                .SelectYesOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step05SelectDiet1(out diet)
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.User.DeleteUser(email);

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
            var bodyFat = decimal.Parse("15." + RandomHelper.RandomNumFromOne(99));
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, bodyFat, UserAccount.FEMALE);
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

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.PpOptions.BREASTFEEDING_MORE;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectMetric()
                .SelectActivityLevel(ActivityLevelNumber.SEDETARY, out level)
                .SelectFemale(out selectedGender)
                .SelectYesOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step05SelectDiet1(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.User.DeleteUser(email);

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
            var bodyFat = decimal.Parse("15." + RandomHelper.RandomNumFromOne(99));
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15.45, UserAccount.FEMALE);
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

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectMetric()
                .SelectActivityLevel(ActivityLevelNumber.SEDETARY, out level)
                .SelectFemale(out selectedGender)
                .SelectYesOfAdditionalOptions(TDEE.AdditionalOptions.ADDITIONAL_PG_OPTION, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, TDEE.AdditionalOptions.ADDITIONAL_PG_OPTION)
                .ClickNextBtn()
                .Step02SelectMainTain(out goal)
                .ClickNextBtn()
                .Step05SelectDiet2(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.User.DeleteUser(email);

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
            var bodyFat = decimal.Parse("15." + RandomHelper.RandomNumFromOne(99));
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, bodyFat, UserAccount.MALE);
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

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectMetric()
                .SelectActivityLevel(ActivityLevelNumber.SEDETARY, out level)
                .SelectMale(out selectedGender)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectReverse(out goal)
                .ClickNextBtn()
                .Step03SelectTier1(out tier)
                .ClickNextBtn();
            SwitcherHelper.SelectNumberOfWeekForARD(TDEE.ArdPhases.ARD_PHASE_ONE_TWO, out phase);
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .GetPreviousCalories(maintanceCalories, out previousCalories)
                .ClickNextBtn()
                .Step05SelectDiet2(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

           //AppDbContext.User.DeleteUser(email);

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

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = string.Empty;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectReverse(out goal)
                .ClickNextBtn()
                .Step03SelectTier1(out tier)
                .ClickNextBtn();            
            SwitcherHelper.SelectNumberOfWeekForARD(TDEE.ArdPhases.ARD_PHASE_THREE_FOUR, out phase);
            Pages.WebPages.Nutrition
                .ClickNextBtn()
                .GetPreviousCalories(maintanceCalories, out previousCalories)
                .ClickNextBtn()
                .GetTextOnStep06(out textOfMoreThan2KgSelected)
                .ClickNextBtn()
                .Step05SelectDiet2(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            #endregion

            Pages.CommonPages.Login
                .GetUserLogout();

            #region Postconditions

           //AppDbContext.User.DeleteUser(email);

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

            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectImperial()
                .SelectActivityLevel(TDEE.ActivityLevelNumber.SEDETARY, out level)
                .SelectFemale(out selectedGender)
                .SelectYesOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier1(out tier)
                .ClickNextBtn()
                .Step04SelectPhase1(out phase)
                .ClickNextBtn()
                .Step05SelectDiet1(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectImperial()
                .SelectMale(out selectedGender)
                .SelectYesOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier2(out tier)
                .ClickNextBtn()
                .Step04SelectPhase3(out phase)
                .ClickNextBtn()
                .Step05SelectDiet2(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);
            
            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectImperial()
                .SelectActivityLevel(TDEE.ActivityLevelNumber.SEDETARY, out level)
                .SelectFemale(out selectedGender)
                .SelectYesOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier3(out tier)
                .ClickNextBtn()
                .Step04SelectPhase3(out phase)
                .ClickNextBtn()
                .Step05SelectDiet2(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectImperial()
                .SelectActivityLevel(TDEE.ActivityLevelNumber.SEDETARY, out level)
                .SelectFemale(out selectedGender)
                .SelectYesOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier1(out tier)
                .ClickNextBtn()
                .Step04SelectPhase2(out phase)
                .ClickNextBtn()
                .Step05SelectDiet3(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectMetric()
                .SelectActivityLevel(TDEE.ActivityLevelNumber.MODERATE, out level)
                .SelectMale(out selectedGender)
                .SelectYesOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild(out goal)
                .ClickNextBtn()
                .Step03SelectTier1(out tier)
                .ClickNextBtn()
                .Step04SelectPhase1(out phase)
                .ClickNextBtn()
                .Step05SelectDiet1(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectImperial()
                .SelectActivityLevel(TDEE.ActivityLevelNumber.SEDETARY, out level)
                .SelectFemale(out selectedGender)
                .SelectYesOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild(out goal)
                .ClickNextBtn()
                .Step03SelectTier3(out tier)
                .ClickNextBtn()
                .Step04SelectPhase2(out phase)
                .ClickNextBtn()
                .Step05SelectDiet2(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps
            Pages.WebPages.Nutrition
                .SelectImperial()
                .SelectActivityLevel(TDEE.ActivityLevelNumber.SEDETARY, out level)
                .SelectFemale(out selectedGender)
                .SelectYesOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectBuild(out goal)
                .ClickNextBtn()
                .Step03SelectTier2(out tier)
                .ClickNextBtn()
                .Step04SelectPhase1(out phase)
                .ClickNextBtn()
                .Step05SelectDiet3(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectMetric()
                .SelectActivityLevel(TDEE.ActivityLevelNumber.SEDETARY, out level)
                .SelectFemale(out selectedGender)
                .SelectNoOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .SetCalories(out maintanceCalories)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier1(out tier)
                .ClickNextBtn()
                .Step04SelectPhase1(out phase)
                .ClickNextBtn()
                .Step05SelectDiet3(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectImperial()
                .EnterAge("37")
                .SelectActivityLevel(TDEE.ActivityLevelNumber.SEDETARY, out level)
                .SelectFemale(out selectedGender)
                .SelectNoOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier3(out tier)
                .ClickNextBtn()
                .Step04SelectPhase2(out phase)
                .ClickNextBtn()
                .Step05SelectDiet1(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectImperial()
                .SelectMale(out selectedGender)
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString())
                .SelectActivityLevel(TDEE.ActivityLevelNumber.HEAVY, out level)
                .SelectNoOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier3(out tier)
                .ClickNextBtn()
                .Step04SelectPhase3(out phase)
                .ClickNextBtn()
                .Step05SelectDiet2(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, "BBB1");
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("BBB1");
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString())
                .SelectActivityLevel(TDEE.ActivityLevelNumber.MODERATE, out level)
                .SelectFemale(out selectedGender)
                .SelectNoOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier3(out tier)
                .ClickNextBtn()
                .Step04SelectPhase2(out phase)
                .ClickNextBtn()
                .Step05SelectDiet3(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString())
                .SelectActivityLevel(TDEE.ActivityLevelNumber.SEDETARY, out level)
                .SelectMale(out selectedGender)
                .SelectYesOfAdditionalOptions(TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier1(out tier)
                .ClickNextBtn()
                .Step04SelectPhase1(out phase)
                .ClickNextBtn()
                .Step05SelectDiet1(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString())
                .SelectActivityLevel(TDEE.ActivityLevelNumber.MODERATE, out level)
                .SelectMale(out selectedGender)
                .SelectYesOfAdditionalOptions(TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier2(out tier)
                .ClickNextBtn()
                .Step04SelectPhase2(out phase)
                .ClickNextBtn()
                .Step05SelectDiet3(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);
            
            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            var bodyFat = decimal.Parse("15." + RandomHelper.RandomNumFromOne(99));
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, bodyFat, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectImperial()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString())
                .SelectActivityLevel(TDEE.ActivityLevelNumber.MODERATE, out level)
                .SelectMale(out selectedGender)
                .SelectYesOfAdditionalOptions(TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier3(out tier)
                .ClickNextBtn()
                .Step04SelectPhase1(out phase)
                .ClickNextBtn()
                .Step05SelectDiet3(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);
            
            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectImperial()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString())
                .SelectActivityLevel(TDEE.ActivityLevelNumber.MODERATE, out level)
                .SelectFemale(out selectedGender)
                .SelectNoOfAdditionalOptions(TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier3(out tier)
                .ClickNextBtn()
                .Step04SelectPhase2(out phase)
                .ClickNextBtn()
                .Step05SelectDiet1(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);
            
            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectMetric()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString())
                .SelectActivityLevel(TDEE.ActivityLevelNumber.SEDETARY, out level)
                .SelectFemale(out selectedGender)
                .SelectYesOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier3(out tier)
                .ClickNextBtn()
                .Step04SelectPhase1(out phase)
                .ClickNextBtn()
                .Step05SelectDiet2(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions


           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            var bodyFat = decimal.Parse("15." + RandomHelper.RandomNumFromOne(99));
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, bodyFat, UserAccount.FEMALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add and Activate membership to User

            bool eightWeeks = false;
            MembershipRequest.CreateProductMembership();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.SKU_PRODUCT);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion

            #region Variables

            string tier = TDEE.Tiers.TIER_1;
            string phase = TDEE.Phases.PHASE_1;
            string diet = TDEE.Diets.DIET_1;
            string textOfMoreThan2KgSelected = String.Empty;
            double previousCalories = 0.0;
            string level = TDEE.ActivityLevel.MODERATE;
            var userData = AppDbContext.User.GetUserData(email);
            var membershipData = AppDbContext.Memberships.GetActiveMembershipsNameAndSkuByEmail(userData.Email);
            string selectedGender = string.Empty;
            string selectedAdditionalOption = TDEE.AdditionalOptions.ADDITIONAL_COMMON_OPTION;
            string textSelectedAdditionalOptions = string.Empty;
            double maintanceCalories = 0d;
            string goal = TDEE.Goals.GOAL_CUT;
            double expectedCalories = 0d;
            

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

            #endregion

            #region Steps

            Pages.WebPages.Nutrition
                .SelectImperial()
                .EnterAge(RandomNumber.Next(18, 65).ToString())
                .SelectHeight()
                .EnterWeight(RandomNumber.Next(50, 250).ToString())
                .SelectActivityLevel(TDEE.ActivityLevelNumber.MODERATE, out level)
                .SelectFemale(out selectedGender)
                .SelectYesOfAdditionalOptions(selectedAdditionalOption, out textSelectedAdditionalOptions)
                .ClickCalculateBtn(out maintanceCalories)
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn()
                .Step02SelectCut(out goal)
                .ClickNextBtn()
                .Step03SelectTier3(out tier)
                .ClickNextBtn()
                .Step04SelectPhase2(out phase)
                .ClickNextBtn()
                .Step05SelectDiet2(out diet)
                .ClickNextBtn()
                .GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData.SKU, textOfMoreThan2KgSelected, previousCalories, out expectedCalories)
                .VerifyNutritionData(userData, goal, tier, membershipData.SKU, selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

           //AppDbContext.Memberships.DeleteMembership(membershipData.Name);
           //AppDbContext.User.DeleteUser(email);

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
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membership);
            AppDbContext.Memberships.Insert.InsertMembership(membership.Id, MembershipsSKU.SKU_PRODUCT, eightWeeks);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            const int programCount = 3;
            MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out List<DB.Programs> programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
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

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            const int programCount = 3;
            MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out List<DB.Programs> programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
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
                .SelectPhaseAndWeek(1, 0, out int countWorkouts)
                .AddWeightAndEnterNotes(countWorkouts);

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
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships lastMembership);
            AppDbContext.Memberships.Insert.InsertMembership(lastMembership.Id, MembershipsSKU.SKU_PRODUCT, eightWeeks);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            const int programCount = 3;
            MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out List<DB.Programs> programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);

            #endregion

            #region Create Custom membership

            MembershipRequest.CreateCustomMembership(responseLoginAdmin, userId);
            AppDbContext.Memberships.GetLastMembership(out membershipData);
            MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out workouts);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);

            #endregion

            #region Create Subscription membership

            AppDbContext.Memberships.GetLastMembership(out lastMembership);
            AppDbContext.Memberships.Insert.InsertMembership(lastMembership.Id, MembershipsSKU.SKU_SUBSCRIPTION, eightWeeks);
            AppDbContext.Memberships.GetLastMembership(out membershipData);
            MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out workouts);
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
                .SelectPhaseAndWeek(1, 1, out int countWorkouts)
                .VerifyDisplayedDownloadBtn();
            Pages.WebPages.MembershipUser
                .OpenMembershipPage();
            SwitcherHelper.ActivateMembership(membershipList[1].Name);
            Pages.WebPages.MembershipUser
                .ConfirmMembershipActivation()
                .OpenMembership()
                .SelectPhaseAndWeek(1, 1, out countWorkouts)
                .VerifyDisplayedDownloadBtn();
            Pages.WebPages.MembershipUser
                .OpenMembershipPage();
            SwitcherHelper.ActivateMembership(membershipList[2].Name);
            Pages.WebPages.MembershipUser
                .ConfirmMembershipActivation()
                .OpenMembership()
                .SelectPhaseAndWeek(1, 1, out countWorkouts)
                .VerifyDisplayedDownloadBtn();

            Pages.CommonPages.Login
                .GetUserLogout();

            #endregion

            #region Postconditions

            foreach (var member in membershipList)
            {
               //AppDbContext.Memberships.DeleteMembership(member.Name);
            }
           //AppDbContext.User.DeleteUser(email);

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

           //AppDbContext.User.DeleteUser(email);

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
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipId);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipId.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            const int conversionSystem = ConversionSystem.METRIC;
            #endregion

            #region Add Progress as User
            for (int i = 0; i < 60; i++)
            {
                ProgressRequest.AddProgress(responseLoginUser, conversionSystem);
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

           //AppDbContext.User.DeleteUser(email);

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
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membership);
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId.Id);
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

           //AppDbContext.User.DeleteUser(email);

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

           //AppDbContext.User.DeleteUser(email);

            #endregion

        }


        #endregion

    }
}
