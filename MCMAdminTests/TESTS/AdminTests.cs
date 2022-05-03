using NUnit.Framework;
using MCMAutomation.Helpers;
using MCMAutomation.PageObjects;
using AdminSiteTests.BASE;
using NUnit.Allure.Core;
using OpenQA.Selenium;
using NUnit.Allure.Attributes;
using Allure.Commons;

namespace MCMAutomation.AdminSiteTests
{
    [TestFixture]
    [AllureNUnit]
    public class AdminSiteTests : TestBaseAdmin
    {

        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem","qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        
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
                .VerifyMembershipName(membershipName);
            Pages.MembershipAdmin
                .ClickAddProgramsBtn();

            string url = Browser._Driver.Url;

            Pages.MembershipAdmin
                .VerifyMembershipNameCbbx(membershipName)
                .CreatePrograms()
                .DefineProgramsList();
            Pages.MembershipAdmin
                .CreateWorkoutsForFirstProgram();
            Pages.Common
                .ClickSaveBtn();
            Pages.MembershipAdmin
                .ClickAddExerciseBtn()
                .AddExercises();
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
                .VerifyMembershipName(membershipName);
            Pages.Login
                .GetAdminLogout();

        }


        [Test]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        public void CreateAndRemoveNewMembership()
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
            string programsUrl = Browser._Driver.Url;
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
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();
            Browser._Driver.Navigate().GoToUrl("https://mcmstaging-ui.azurewebsites.net/admin/membership/204/programs");
            Pages.PopUp
                .ClosePopUp();
            string url = Browser._Driver.Url;
            Pages.MembershipAdmin
                .CreatePrograms()
                .DefineProgramsList();
        }

    }
}