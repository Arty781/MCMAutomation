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

            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenUsersPage();
            Pages.MembershipAdmin
                .SearchUser(email[0])
                .VerifyDisplayingOfUser(email[0])
                .EditUser(membership);
            Pages.Login
                .GetAdminLogout();

            #endregion

            Pages.Login
                .GetUserLogin(email, Credentials.password);
            Pages.MembershipUser
                .OpenMembership()
                .SelectPhase()
                .SelectWeekNumber()
                .OpenWorkouts()
                .AddWeight();

            string[] weightList = Pages.MembershipUser.GetEnteredWeight();

            Pages.MembershipUser
                .ClickCompleteWorkoutBtn()
                .OpenCompletedWorkouts()
                .VerifySavingWeight(weightList);

            Pages.Login
                .GetUserLogout();

        }


        #endregion

        #region TDEE

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

            string tier = null;
            string phase = null;
            string diet = null;
            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

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
            //tier = Pages.Nutrition.textActiveTier.Text;
            //Pages.Nutrition
            //    .ClickNextBtn();
            //.Step04SelectPhase1();
            //WaitUntil.WaitSomeInterval(1500);
            //phase = Pages.Nutrition.textActivePhase.Text;
            //Pages.Nutrition
            //    .ClickNextBtn();
            //.Step05SelectDiet1();
            WaitUntil.WaitSomeInterval(1500);
            diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);

            Pages.Login
                .GetUserLogout();

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

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void CheckTdeeForPP1ForFemaleSecondOption()
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

            string tier = null;
            string phase = null;
            string diet = null;
            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

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
                .SelectMetric(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalPpOption[1];

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
            //tier = Pages.Nutrition.textActiveTier.Text;
            //Pages.Nutrition
            //    .ClickNextBtn();
            //.Step04SelectPhase1();
            //WaitUntil.WaitSomeInterval(1500);
            //phase = Pages.Nutrition.textActivePhase.Text;
            //Pages.Nutrition
            //    .ClickNextBtn();
            //.Step05SelectDiet1();
            WaitUntil.WaitSomeInterval(1500);
            diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);

            Pages.Login
                .GetUserLogout();

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

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void CheckTdeeForPGForFemale()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("PG");

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

            string tier = null;
            string phase = null;
            string diet = null;
            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

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
                .SelectMetric(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalPgOption[0];

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
            Pages.Nutrition
            .Step05SelectDiet2();
            WaitUntil.WaitSomeInterval(1500);
            diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);

            Pages.Login
                .GetUserLogout();

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

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]
        public void CheckTdeeForARDForFemale()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("ARD");

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

            string tier = null;
            string phase = null;
            string diet = null;
            string selectedAdditionalOption = null;
            string[] textSelectedAdditionalOptions = null;
            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region FINDING YOUR ESTIMATED TDEE Page
            Pages.Nutrition
                .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);
            IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");

            Pages.Nutrition
                .SelectFemale(genderBtns);

            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");            
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
            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
                //.Step03SelectTier1();
            WaitUntil.WaitSomeInterval(1500);
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
            WaitUntil.WaitSomeInterval(1500);
            diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);

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
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

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
            //Pages.PopUp
            //    .ClosePopUp();
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
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                 .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);
            Pages.Login
                .GetUserLogout();

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

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForCutTier2Phase3()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

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
            //Pages.PopUp
            //    .ClosePopUp();
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
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);
            Pages.Login
                .GetUserLogout();

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


        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForCutTier3Phase1()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

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
            //Pages.PopUp
            //    .ClosePopUp();
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
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);
            Pages.Login
                .GetUserLogout();

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

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForCutTier1Phase2()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

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
            //Pages.PopUp
            //    .ClosePopUp();
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
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);
            Pages.Login
                .GetUserLogout();

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

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForBuildTier1Phase1()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

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
            //Pages.PopUp
            //    .ClosePopUp();
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
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);
            Pages.Login
                .GetUserLogout();

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

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForBuildTier3Phase2()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

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
            //Pages.PopUp
            //    .ClosePopUp();
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
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);
            Pages.Login
                .GetUserLogout();

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

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesForBuildTier2Phase1()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

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
            //Pages.PopUp
            //    .ClosePopUp();
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
                 .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);
            Pages.Login
                .GetUserLogout();

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


        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCarbsAndFatsFor1000Calories()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

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
            //Pages.PopUp
            //    .ClosePopUp();
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
                .SelectMetric(conversionsBtn);
            #endregion

            #region Select additional options

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn()
                .SetCalories();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                //.VerifyMaintainCaloriesStep01(userData, level, selectedGender, textSelectedAdditionalOptions, selectedAdditionalOption)
                .ClickNextBtn();
                //.Step02SelectBuild();
            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
                //.Step03SelectTier3();
            WaitUntil.WaitSomeInterval(1500);
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase3();
            WaitUntil.WaitSomeInterval(1500);
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            WaitUntil.WaitSomeInterval(1500);
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                 .ClickNextBtn()
                 .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);
            Pages.Login
                .GetUserLogout();

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

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesDiet1()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

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
            //Pages.PopUp
            //    .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.Nutrition
                .EnterAge("37");
                //.SelectHeight(30)
                //.EnterWeight("141")
                //.EnterBodyFat("20");

            //Pages.Nutrition
            //    .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);
            //IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");

            //Pages.Nutrition
            //    .SelectFemale(genderBtns);

            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            //IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            //Pages.Nutrition
            //    .SelectImperial(conversionsBtn);

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .ClickNextBtn();

            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;

            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();

            WaitUntil.WaitSomeInterval(1500);
            string tier = Pages.Nutrition.textActiveTier.Text;

            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase3();

            WaitUntil.WaitSomeInterval(1500);
            string phase = Pages.Nutrition.textActivePhase.Text;

            Pages.Nutrition
                .ClickNextBtn();
                //.Step05SelectDiet1();

            WaitUntil.WaitSomeInterval(1500);
            string diet = Pages.Nutrition.textActiveDiet.Text;

            Pages.Nutrition
                 .ClickNextBtn()
                 .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);
            Pages.Login
                .GetUserLogout();

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

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesDiet2()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

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
            //Pages.PopUp
            //    .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.Nutrition
                .EnterAge("37");
            //.SelectHeight(30)
            //.EnterWeight("141")
            //.EnterBodyFat("20");

            //Pages.Nutrition
            //    .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);
            //IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");

            //Pages.Nutrition
            //    .SelectFemale(genderBtns);

            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            //IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            //Pages.Nutrition
            //    .SelectImperial(conversionsBtn);

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .ClickNextBtn();

            WaitUntil.WaitSomeInterval(500);
            string goal = Pages.Nutrition.textActiveGoal.Text;

            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();

            WaitUntil.WaitSomeInterval(500);
            string tier = Pages.Nutrition.textActiveTier.Text;

            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase3();

            WaitUntil.WaitSomeInterval(500);
            string phase = Pages.Nutrition.textActivePhase.Text;

            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet2();

            WaitUntil.WaitSomeInterval(500);
            string diet = Pages.Nutrition.textActiveDiet.Text;

            Pages.Nutrition
                 .ClickNextBtn()
                 .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);
            Pages.Login
                .GetUserLogout();

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

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void VerifyCaloriesDiet3()
        {
            #region AdminActions
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] membership = AppDbContext.GetActiveMembershipsBySKU("CP_TEST_SUB");

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
            //Pages.PopUp
            //    .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();

            string textOfMoreThan2KgSelected = null;
            double previousCalories = 0.0;

            #region FINDING YOUR ESTIMATED TDEE page
            Pages.Nutrition
                .EnterAge("37");

            //Pages.Nutrition
            //    .SelectActivityLevel(0);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            string[] userData = AppDbContext.GetUserData(Credentials.login);
            string[] membershipData = AppDbContext.GetActiveMembershipsByEmail(userData[4]);
            //IList<IWebElement> genderBtns = SwitcherHelper.NutritionSelector("Gender");

            //Pages.Nutrition
            //    .SelectFemale(genderBtns);

            string[] selectedGender = SwitcherHelper.GetTexOfSelectedtNutritionSelector("Gender");
            //IList<IWebElement> conversionsBtn = SwitcherHelper.NutritionSelector("Preferred Conversion System");
            //Pages.Nutrition
            //    .SelectImperial(conversionsBtn);

            string selectedAdditionalOption = AdditionalOptions.additionalCommonOption[0];

            IList<IWebElement> additionalOption = SwitcherHelper.NutritionSelector(selectedAdditionalOption);
            Pages.Nutrition
                .SelectNoOfAdditionalOptions(additionalOption);

            string[] textSelectedAdditionalOptions = SwitcherHelper.GetTexOfSelectedtNutritionSelector(selectedAdditionalOption);

            #endregion

            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .ClickNextBtn();

            WaitUntil.WaitSomeInterval(1500);
            string goal = Pages.Nutrition.textActiveGoal.Text;

            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier3();

            WaitUntil.WaitSomeInterval(1500);
            string tier = Pages.Nutrition.textActiveTier.Text;

            Pages.Nutrition
                .ClickNextBtn()
            .Step04SelectPhase3();

            WaitUntil.WaitSomeInterval(1500);
            string phase = Pages.Nutrition.textActivePhase.Text;

            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();

            WaitUntil.WaitSomeInterval(1500);
            string diet = Pages.Nutrition.textActiveDiet.Text;

            Pages.Nutrition
                 .ClickNextBtn()
                 .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            double expectedCalories = Pages.Nutrition.GetCaloriesStep06(maintanceCalories, goal, tier, phase, membershipData[1], textOfMoreThan2KgSelected, previousCalories);

            Pages.Nutrition
            .VerifyProtein(userData, goal, tier, membershipData[1], selectedGender);

            double protein = Pages.Nutrition.GetProtein(userData, goal, tier, membershipData[1], selectedGender);

            Pages.Nutrition
                .VerifyFatAndCarbs(protein, expectedCalories, userData, diet);
            Pages.Login
                .GetUserLogout();

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


        #endregion

        #endregion

        #region User profile

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void EditUser()
        {
            string[] email = AppDbContext.GetUsersData();

            Pages.Login
                .GetUserLogin(email, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMyAccount();
            Pages.UserProfile
                .EnterHeight();

        }

        #endregion
    }
}
