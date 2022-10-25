using Allure.Commons;
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
using RimuTec.Faker;

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
                .GetLogin(email, Credentials.password);
            Pages.MembershipUser
                .VerifyIsBuyBtnDisplayed();
            Pages.Sidebar
                .OpenMyAccount();
            Pages.UserProfile
                .EnterDOB()
                .EnterProteins()
                .EnterCalories()
                .EnterMaintenanceCalories()
                .EnterCarbs()
                .EnterFats()
                .EnterHeight()
                .EnterWeight()
                .EnterNewEmail()
                .EnterOldPass()
                .EnterNewPass()
                .EnterConfirmPass()
                .EnterEstimatedBodyFat();
            Pages.Common
                .ClickSaveBtn();
            Pages.PopUp
                .ClosePopUp();
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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("PP-1");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
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
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
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

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("PP-1");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
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
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectMetric(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalPpOption[1];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(additionalOption);

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("PG");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.PopUp
                .ClosePopUp();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
            Pages.Sidebar
                .OpenNutritionPage();

            #region Variables

            string tier = null;
            string phase = null;
            string diet;
            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select Activity lvl
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectMetric(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalPgOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectYesOfAdditionalOptions(additionalOption);

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region Postconditions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("ARD");
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
            Pages.Sidebar
                .OpenNutritionPage();
            #region Variables
            string tier;
            string phase;
            string diet;
            string selectedAdditionalOption = null;
            List<string> textSelectedAdditionalOptions = null;
            string textOfMoreThan2KgSelected = null;
            double previousCalories;

            #endregion

            #region FINDING YOUR ESTIMATED TDEE Page
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");

            Pages.Nutrition
                .SelectMale(genderBtns);

            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");

            Pages.Nutrition
                .SelectMetric(conversionsBtn);
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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            #endregion
            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("ARD");
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
            Pages.Sidebar
                .OpenNutritionPage();

            #region Variables
            string tier;
            string phase;
            string diet;
            string selectedAdditionalOption = null;
            List<string> textSelectedAdditionalOptions = null;
            string textOfMoreThan2KgSelected;
            double previousCalories = 0.0;
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region FINDING YOUR ESTIMATED TDEE Page
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");

            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");

            Pages.Nutrition
                .SelectMetric(conversionsBtn)
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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            #endregion

            Pages.Login
                .GetUserLogout();

            #region Postconditions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
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
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
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

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
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
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
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

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);
            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
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
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
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

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
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
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
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

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
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
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
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

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
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
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
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

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
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
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
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

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
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
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);

            #endregion

            #region Select gender
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");
            Pages.Nutrition
                .SelectFemale(genderBtns);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            #endregion

            #region Select Conversion System
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectMetric(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(additionalOption);

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
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
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");

            Pages.Nutrition
                .SelectFemale(genderBtns);

            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            Pages.Nutrition
                .SelectImperial(conversionsBtn);

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(additionalOption);

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.Nutrition
                .EnterAge("37");
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(additionalOption);

            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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
            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("BBB1");
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
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
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLoginForTdee(email, Credentials.password);
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.Nutrition
                .EnterAge("37");
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            List<string> userData = AppDbContext.GetUserData(email);
            List<string> membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);
            List<string> selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(additionalOption);
            List<string> textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

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
            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyNutritionData(userData, goal, tier, membershipData[1], selectedGender, expectedCalories, diet, maintanceCalories, phase, textOfMoreThan2KgSelected, previousCalories);

            Pages.Login
                .GetUserLogout();

            #region AdminActions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email);
            Pages.Login
                .GetAdminLogout();

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

        public void CompleteMembershipsWithData()
        {

            string email = AppDbContext.GetUserEmail();

            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string membership = AppDbContext.GetActiveMembershipsBySKU("CMC_TEST_SUB");

            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLogin(email, Credentials.password);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.MembershipUser
                .OpenMembership();
            

            int countPhases = Pages.MembershipUser.GetPhasesCount();

            Pages.Sidebar
                    .OpenMemberShipPageUser();

            for (int i = 0; i < countPhases; i++)
            {
                Pages.MembershipUser
                    .OpenMembership()
                    .SelectPhase(i)
                    .SelectWeekNumber1InPhase();
                WaitUntil.WaitSomeInterval(3000);
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
                Pages.MembershipUser
                    .SelectWeekNumber2();
                countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
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
                Pages.MembershipUser
                    .SelectWeekNumber3();
                countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
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
                Pages.MembershipUser
                    .SelectWeekNumber4();
                countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
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
                Pages.Sidebar
                    .OpenMemberShipPageUser();

            }

            Pages.Login
                .GetUserLogout();

        }


        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void CompleteMembershipsWithDataMega()
        {

            string email = AppDbContext.GetUserEmail();

            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLogin(email, Credentials.password);
            Pages.MembershipUser
                .OpenMembership();

            int countPhases = Pages.MembershipUser.GetPhasesCount();

            Pages.Sidebar
                    .OpenMemberShipPageUser();

            for (int i = 0; i < countPhases; i++)
            {
                Pages.MembershipUser
                    .OpenMembership()
                    .SelectPhase(i);

                int countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
                for (int j = 0; j < countWorkouts; j++)
                {
                    Pages.MembershipUser
                    .OpenWorkout()
                    .AddWeight()
                    .EnterNotes()
                    .ClickCompleteWorkoutBtn();
                }
                Pages.MembershipUser
                    .SelectWeekNumber2();
                countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
                for (int j = 0; j < countWorkouts; j++)
                {
                    Pages.MembershipUser
                    .OpenWorkout()
                    .AddWeight()
                    .EnterNotes()
                    .ClickCompleteWorkoutBtn();
                }
                Pages.MembershipUser
                    .SelectWeekNumber3();
                countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
                for (int j = 0; j < countWorkouts; j++)
                {
                    Pages.MembershipUser
                    .OpenWorkout()
                    .AddWeight()
                    .EnterNotes()
                    .ClickCompleteWorkoutBtn();
                }
                Pages.MembershipUser
                    .SelectWeekNumber4();
                countWorkouts = Pages.MembershipUser.GetWorkoutsCount();
                for (int j = 0; j < countWorkouts; j++)
                {
                    Pages.MembershipUser
                    .OpenWorkout()
                    .AddWeight()
                    .EnterNotes()
                    .ClickCompleteWorkoutBtn();
                }
                Pages.Sidebar
                    .OpenMemberShipPageUser();

            }

            Pages.Login
                .GetUserLogout();

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
            string email = AppDbContext.GetUserEmail();

            Pages.Login
                .GetUserLogin(email, Credentials.password);
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
                .EnterNewEmail();
            ScreenShotHelper.MakeScreenShot();

            List<string> dataBeforeSaving = Pages.UserProfile.GetUserDataBeforeSaving();

            Pages.Common
                .ClickSaveBtn();
            Pages.Sidebar
                .OpenMyAccount();

            List<string> dataAfterSaving = Pages.UserProfile.GetUserDataBeforeSaving();
            Pages.UserProfile
                .VerifyUserData(dataBeforeSaving, dataAfterSaving);

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

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string email = AppDbContext.GetUserEmail();
            string userId = AppDbContext.GetUserId(email);
            string membership = AppDbContext.GetActiveMembershipsBySKU("MCM_BIKINI_SUB");

            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLogin(email, Credentials.password);
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
                    .EnterThigh();

                List<string> progressBeforeSaving = Pages.Progress.GetProgressData();

                Pages.Progress
                    .ClickSubmitBtn()
                    .VerifyAddedProgress(progressBeforeSaving);

                AppDbContext.UpdateUserProgressDate(userId);

                Browser._Driver.Navigate().Refresh();
                Pages.PopUp
                    .ClosePopUp();
            }
            Pages.Login
                .GetUserLogout();
            #region Postconditions

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .DeleteMemebershipFromUser(email)
                .DeleteProgressFromUser();
            Pages.Login
                .GetAdminLogout();

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

            string email = AppDbContext.GetUserEmail();
            Pages.Login
                .CopyUserEmail(email)
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            string membership = AppDbContext.GetActiveMembershipsBySKU("MCM_BIKINI_SUB");

            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email);
            Pages.UsersAdmin
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLogin(email, Credentials.password);
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

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email)
                .DeleteProgressFromUser();
            Pages.Login
                .GetAdminLogout();

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

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string email = AppDbContext.GetUserEmail();
            string membership = AppDbContext.GetActiveMembershipsBySKU("MCM_BIKINI_SUB");

            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membership)
                .SelectActiveMembership(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLogin(email, Credentials.password);
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

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .DeleteMemebershipFromUser(email)
                .DeleteProgressFromUser();
            Pages.Login
                .GetAdminLogout();

            #endregion

        }


        #endregion

    }
}
