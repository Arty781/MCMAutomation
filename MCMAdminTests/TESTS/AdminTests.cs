using NUnit.Framework;
using MCMAutomation.Helpers;
using MCMAutomation.PageObjects;
using AdminSiteTests.BASE;
using NUnit.Allure.Core;
using OpenQA.Selenium;

namespace MCMAutomation.AdminSiteTests
{
    [TestFixture]
    [AllureNUnit]
    public class AdminSiteTests : TestBaseAdmin
    {

        [Test]
        
        public void CreateNewMembership()
        {
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .ClickCreateBtn()
                .EnterMembershipData();
            
            string membershipName = Pages.MembershipAdmin.GetMembershipName();

            Pages.Common
                .ClickSaveBtn();
            Pages.MembershipAdmin
                .SearchMembership(membershipName)
                .VerifyMembershipName(membershipName)
                .ClickAddProgramsBtn()
                .VerifyMembershipNameCbbx(membershipName)
                .CreatePrograms();
            Pages.Common
                .ClickSaveBtn();
            Pages.MembershipAdmin
                .CreateWorkoutsForFirstProgram();
            Pages.Common
                .ClickSaveBtn();
            Pages.MembershipAdmin
                .AddExercises();
            Pages.Common
                .ClickSaveBtn();
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .SearchMembership(membershipName)
                .VerifyMembershipName(membershipName)
                .AddUserToMembership()
                .VerifyAssignUser();
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .SearchMembership(membershipName)
                .VerifyMembershipName(membershipName)
                .ClickDeleteBtn()
                .VerifyDeletingMembership(membershipName);
            Pages.Login
                .GetAdminLogout();


                


        }


        [Test]
        public void ClientLogin()
        {
            Pages.Login
                .GetUserLogin();
        }

    }
}