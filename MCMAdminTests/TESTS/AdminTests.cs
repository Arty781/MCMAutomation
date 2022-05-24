using NUnit.Framework;
using MCMAutomation.Helpers;
using MCMAutomation.PageObjects;
using AdminSiteTests.BASE;
using NUnit.Allure.Core;
using OpenQA.Selenium;
using NUnit.Allure.Attributes;
using Allure.Commons;
using System.Collections.Generic;

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
        [Author("Artem", "qatester91311@gmail.com")]
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

        public void AddProgramsToNewMembership()
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
                .CreatePrograms();

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

        public void AddWorkoutsAndExercisesToNewMembership()
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
                .CreatePrograms();
            IList<string> programLinks = ListHelper.DefineProgramList(url);

            Pages.MembershipAdmin
                .CreateWorkouts(programLinks);
           
            Pages.MembershipAdmin
                .AddExercises(programLinks);
            Pages.Sidebar
                .OpenUsersPage();
            Pages.MembershipAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .EditUser(membershipName);
            
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

            string url = Browser._Driver.Url;

            Pages.MembershipAdmin
                 .VerifyMembershipNameCbbx(membershipName)
                 .CreatePrograms();
            IList<string> programLinks = ListHelper.DefineProgramList(url);

            Pages.MembershipAdmin
                .CreateWorkouts(programLinks);
            IList<string> exercisesLinks = ListHelper.DefineWorkoutList(programLinks);
            Pages.MembershipAdmin
                .AddExercises(exercisesLinks);
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .SearchMembership(membershipName)
                .VerifyMembershipName(membershipName)
                .AddUserToMembership("qatester91323@xitroo.com")
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
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]

        public void OpenMemberShipPage()
        {
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            /*Pages.Sidebar
                .VerifyIsLogoDisplayed();*/
            
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.PopUp
                .ClosePopUp();
        }


            [Test]
        public void AssignMembershipToUser()
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
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .SearchMembership("Jenna")
                .VerifyMembershipName("Jenna")
                .AddUserToMembership(email)
                .VerifyAssignUser();
            AppDbContext
                .GetUserExercisesList("qatester2022-05-20-08-12@xitroo.com", "Jenna");
            Pages.Sidebar
                .OpenMemberShipPage();

        }

       

    }
}