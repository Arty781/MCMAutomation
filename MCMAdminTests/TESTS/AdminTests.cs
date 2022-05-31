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
            Pages.Common
                .ClickSaveBtn();

            string[] memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
                .SearchMembership(memberName)
                .VerifyMembershipName(memberName);

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
            Pages.Common
                .ClickSaveBtn();

            string[] memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
                .SearchMembership(memberName)
                .VerifyMembershipName(memberName);
            Pages.MembershipAdmin
               .ClickAddProgramsBtn();

            string url = Browser._Driver.Url;

            Pages.MembershipAdmin
                .VerifyMembershipNameCbbx(memberName)
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
            Pages.Common
                .ClickSaveBtn();
            
            string[] memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
                .SearchMembership(memberName)
                .VerifyMembershipName(memberName);
            Pages.MembershipAdmin
                .ClickAddProgramsBtn();

            string url = Browser._Driver.Url;

            Pages.MembershipAdmin
                .VerifyMembershipNameCbbx(memberName)
                .CreatePrograms();

            string[] programList = Pages.MembershipAdmin.GetProgramNames();
           
            Pages.MembershipAdmin
                .ClickAddWorkoutBtn()
                .CreateWorkouts(programList);
            
            string[] exercise = AppDbContext.GetExercisesData();

            Pages.MembershipAdmin
                .AddExercises(programList, exercise);
            Pages.Sidebar
                .OpenUsersPage();
            Pages.MembershipAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .EditUser(memberName);

            Pages.Login
                .GetAdminLogout();

        }


        //[Test]
        //[AllureTag("Regression")]
        //[AllureOwner("Artem Sukharevskyi")]
        //[AllureSeverity(SeverityLevel.critical)]
        //[Author("Artem", "qatester91311@gmail.com")]
        //[AllureSuite("Admin")]
        //[AllureSubSuite("Memberships")]
        //public void CreateAndRemoveNewMembership()
        //{
        //    Pages.Login
        //        .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
        //    Pages.Sidebar
        //        .VerifyIsLogoDisplayed();
        //    Pages.PopUp
        //        .ClosePopUp();
        //    Pages.Sidebar
        //        .OpenMemberShipPage();
        //    Pages.MembershipAdmin
        //        .ClickCreateBtn()
        //        .EnterMembershipData();
        //    Pages.Common
        //        .ClickSaveBtn();

        //    string[] memberName = AppDbContext.GetLastMembership();

        //    Pages.MembershipAdmin
        //        .SearchMembership(memberName)
        //        .VerifyMembershipName(memberName)
        //        .ClickAddProgramsBtn()
        //        .VerifyMembershipNameCbbx(memberName)
        //        .CreatePrograms();

        //    string url = Browser._Driver.Url;

        //    Pages.MembershipAdmin
        //         .VerifyMembershipNameCbbx(memberName)
        //         .CreatePrograms();
        //    IList<IWebElement> programLinks = ListHelper.DefineProgramList(url);

        //    Pages.MembershipAdmin
        //        .CreateWorkouts(programLinks);

        //    IList<string> exercisesLinks = ListHelper.DefineWorkoutList(programLinks);
        //    string[] exercise = AppDbContext.GetExercisesData();

        //    Pages.MembershipAdmin
        //        .AddExercises(url, exercise);
        //    Pages.Sidebar
        //        .OpenMemberShipPage();
        //    Pages.MembershipAdmin
        //        .SearchMembership(memberName)
        //        .VerifyMembershipName(memberName)
        //        .AddUserToMembership("qatester91323@xitroo.com")
        //        .VerifyAssignUser();
        //    Pages.Sidebar
        //        .OpenMemberShipPage();
        //    Pages.MembershipAdmin
        //        .SearchMembership(memberName)
        //        .VerifyMembershipName(memberName)
        //        .ClickDeleteBtn()
        //        .VerifyDeletingMembership(memberName);
        //    Pages.Login
        //        .GetAdminLogout();





        //}

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
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.PopUp
                .ClosePopUp();
        }


        [Test]
        public void AddProgramsWorkoutsAndExercises()
        {
            
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();

            string[] memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
                .SearchMembership(memberName)
                .VerifyMembershipName(memberName);
            Pages.MembershipAdmin
                .ClickAddProgramsBtn();

            string url = Browser._Driver.Url;

            /*Pages.MembershipAdmin
                .VerifyMembershipNameCbbx(memberName)
                .CreatePrograms();*/
           /* Pages.MembershipAdmin
                .CreateWorkouts(url);*/
            string[] exercise = AppDbContext.GetExercisesData();

            //Pages.MembershipAdmin
            //    .AddExercises(url, exercise);
            

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

        public void EditExercise()
        {
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenExercisesPage();
            Pages.PopUp
                .ClosePopUp();
            Pages.ExercisesAdmin
                .ClickEditExercise();
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

        public void CreateExerciseWithoutRelated()
        {
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenExercisesPage();
            Pages.PopUp
                .ClosePopUp();

            string[] relatedExerciseList = AppDbContext.GetExercisesData();

            Pages.ExercisesAdmin
                .ClickCreateExerciseBtn()
                .EnterExerciseData();

            string exerciseName = TextBox.GetAttribute(Pages.ExercisesAdmin.fieldExerciseName, "value");

            Pages.Common
                .ClickSaveBtn();

            Pages.ExercisesAdmin
                .VerifyExerciseIsCreated(exerciseName);
            
            
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

        public void RemoveExercise()
        {
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenExercisesPage();
            Pages.PopUp
                .ClosePopUp();
            Pages.ExercisesAdmin
                .ClickCreateExerciseBtn()
                .EnterExerciseData();

            string exerciseName = TextBox.GetAttribute(Pages.ExercisesAdmin.fieldExerciseName, "value");

            Pages.Common
                .ClickSaveBtn();

            Pages.ExercisesAdmin
                .VerifyExerciseIsCreated(exerciseName)
                .RemoveExercise(exerciseName)
                .VerifyExerciseIsRemoved();

            Pages.Login
                .GetAdminLogout();
        }

        [Test]

        public void Test()
        {
            AppDbContext.GetExerciseStatus();
        }
    }
}