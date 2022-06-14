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

namespace MCMAutomation.WebTests
{
    [TestFixture]
    [AllureNUnit]
    public class WebTests : TestBaseWeb
    {

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


        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void CompleteMembershipsWithData()
        {
            
           string[] email = AppDbContext.GetUserData();
            
            Pages.Login
                .GetUserLogin(email, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.MembershipUser
                .OpenMembership()
                .SelectPhase()
                .SelectWeekNumber()
                .OpenWorkouts()
                .AddWeight();
            
        }

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Web")]
        [AllureSubSuite("Memberships")]

        public void CheckTdeeForPP1()
        {
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            string[] nameMembership = AppDbContext.GetMembershipsBySKU("PP-1");

            Pages.PopUp
                .ClosePopUp();
            Pages.MembershipAdmin
                .SearchUser(Credentials.login)
                .VerifyDisplayingOfUser(Credentials.login)
                .EditUser(nameMembership);
            Pages.Login
                .GetAdminLogout();

            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            //Pages.Sidebar
            //    .Open()
            //    .SelectPhase()
            //    .SelectWeekNumber()
            //    .OpenWorkouts()
            //    .AddWeight();

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.MembershipAdmin
                .SearchUser(Credentials.login)
                .VerifyDisplayingOfUser(Credentials.login)
                .DeleteMemebershipFromUser();
            Pages.Login
                .GetAdminLogout();

        }

        [Test]

        public void VerifyCaloriesForCutTier1Phase1()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            Pages.Nutrition
                .SelectActivityLevel(4);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            string[] userData = AppDbContext.GetUserData();
            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level)
                .ClickNextBtn();
                //.Step02SelectCut();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
                //.Step03SelectTier1();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn();
                //.Step04SelectPhase1();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn();
                //.Step05SelectDiet1();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase);
        }

        [Test]

        public void VerifyCaloriesForCutTier2Phase3()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            Pages.Nutrition
                .SelectActivityLevel(4);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            string[] userData = AppDbContext.GetUserData();
            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level)
                .ClickNextBtn();
            //.Step02SelectCut();
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
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase);
        }


        [Test]

        public void VerifyCaloriesForCutTier3Phase1()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            Pages.Nutrition
                .SelectActivityLevel(4);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            string[] userData = AppDbContext.GetUserData();
            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level)
                .ClickNextBtn();
            //.Step02SelectCut();
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
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase);
        }

        [Test]

        public void VerifyCaloriesForCutTier1Phase2()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            Pages.Nutrition
                .SelectActivityLevel(4);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            string[] userData = AppDbContext.GetUserData();
            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level)
                .ClickNextBtn();
            //.Step02SelectCut();
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
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase);
        }

        [Test]

        public void VerifyCaloriesForBuildTier1Phase3()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            Pages.Nutrition
                .SelectActivityLevel(4);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            string[] userData = AppDbContext.GetUserData();
            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level)
                .ClickNextBtn()
                .Step02SelectBuild();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn();
            //.Step03SelectTier1();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step04SelectPhase3();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn();
            //.Step05SelectDiet1();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase);
        }

        [Test]

        public void VerifyCaloriesForBuildTier3Phase2()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            Pages.Nutrition
                .SelectActivityLevel(4);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            string[] userData = AppDbContext.GetUserData();
            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level)
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
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase);
        }

        [Test]

        public void VerifyCaloriesForBuildTier2Phase1()
        {
            Pages.Login
                .GetUserLoginForTdee(Credentials.login, Credentials.password);
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenNutritionPage();
            Pages.Nutrition
                .SelectActivityLevel(4);
            string level = Pages.Nutrition.cbbxActivitylevel.Text;
            string[] userData = AppDbContext.GetUserData();
            Pages.Nutrition
                .ClickCalculateBtn();

            double maintanceCalories = Pages.Nutrition.GetCalories();

            Pages.Nutrition
                .VerifyMaintainCaloriesStep01(userData, level)
                .ClickNextBtn()
                .Step02SelectBuild();
            string goal = Pages.Nutrition.textActiveGoal.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step03SelectTier2();
            string tier = Pages.Nutrition.textActiveTier.Text;
            Pages.Nutrition
                .ClickNextBtn();
                //.Step04SelectPhase3();
            string phase = Pages.Nutrition.textActivePhase.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .Step05SelectDiet3();
            string diet = Pages.Nutrition.textActiveDiet.Text;
            Pages.Nutrition
                .ClickNextBtn()
                .VerifyCaloriesStep06(maintanceCalories, goal, tier, phase);
        }
    }
}
