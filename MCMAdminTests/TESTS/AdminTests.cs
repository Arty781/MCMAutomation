using NUnit.Framework;
using MCMAutomation.Helpers;
using MCMAutomation.PageObjects;
using AdminSiteTests.BASE;
using NUnit.Allure.Core;
using OpenQA.Selenium;
using NUnit.Allure.Attributes;
using Allure.Commons;
using System.Collections.Generic;
using System;
using MCMAutomation.APIHelpers;

namespace AdminSiteTests
{
    [TestFixture]
    [AllureNUnit]
    public class Memberships : TestBaseAdmin
    {

        [Test, Category("Memberships")]
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

            string memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
                .SearchMembership(memberName)
                .VerifyMembershipName(memberName);

            Pages.Login
                .GetAdminLogout();

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        public void AddProgramsToExistingMembership()
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

            string memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
               .ClickAddProgramsBtn(memberName);
            Pages.MembershipAdmin
                .VerifyMembershipNameCbbx(memberName)
                .CreatePrograms();

            Pages.Login
                .GetAdminLogout();

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        public void EditPrograms()
        {
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();

            string memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
                .ClickAddProgramsBtn(memberName);
            Pages.MembershipAdmin
                .VerifyMembershipNameCbbx(memberName)
                .CreatePrograms();

            string[] programList = Pages.MembershipAdmin.GetProgramNames();

            Pages.MembershipAdmin
                .AddNextPhaseDependency(programList);
            Pages.Login
                .GetAdminLogout();

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        public void EditMembership()
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

            string memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
                .ClickEditMembershipBtn(memberName)
                .EditMembershipData();
            Pages.Common
                .ClickSaveBtn();

            string memberNameAfterEditing = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
                .SearchMembership(memberNameAfterEditing)
                .VerifyMembershipName(memberNameAfterEditing);

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
        //public void AddProgramsToNewMembership()
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

        //    string memberName = AppDbContext.GetLastMembership();

        //    Pages.MembershipAdmin
        //        .ClickAddProgramsBtn(memberName)
        //        .VerifyMembershipNameCbbx(memberName)
        //        .CreatePrograms();

        //    string[] programList = Pages.MembershipAdmin.GetProgramNames();

        //    Pages.MembershipAdmin
        //        .ClickAddWorkoutBtn()
        //        .CreateWorkouts(programList);

        //    Pages.Login
        //        .GetAdminLogout();

        //}

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        public void RemoveProgramsFromNewMembership()
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

            string memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
               .ClickAddProgramsBtn(memberName)
               .VerifyMembershipNameCbbx(memberName)
               .CreateProgramsMega();
            Pages.Sidebar
               .OpenMemberShipPage();
            Pages.MembershipAdmin
               .ClickAddProgramsBtn(memberName);

            string[] programList = Pages.MembershipAdmin.GetProgramNames();

            Pages.MembershipAdmin
                .DeletePrograms(programList)
                .VerifyDeletePrograms();

            Pages.Login
                .GetAdminLogout();

        }

        [Test, Category("Memberships")]
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

            string memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
                .ClickAddProgramsBtn(memberName)
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
            Pages.UsersAdmin
                .SearchUser()
                .ClickEditUser(email)
                .AddMembershipToUser(memberName)
                .SelectActiveMembership(memberName);

            Pages.Login
                .GetAdminLogout();

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        //[Ignore("Ignore")]
        
        public void CopyExercisesToNewMembership()
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

            string memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
                .ClickAddProgramsBtn(memberName)
                .VerifyMembershipNameCbbx(memberName)
                .CreatePrograms();

            string[] programList = Pages.MembershipAdmin.GetProgramNames();

            Pages.MembershipAdmin
                .ClickAddWorkoutBtn()
                .CreateWorkouts(programList);

            List<string> membershipData = AppDbContext.GetMembershipProgramWorkoutData();

            Pages.MembershipAdmin
                .CopyExercises(programList, membershipData, memberName);
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser()
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(memberName)
                .SelectActiveMembership(memberName);

            Pages.Login
                .GetAdminLogout();

        }


        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        public void CreateAndRemoveNewMembership()
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

            string memberName = AppDbContext.GetLastMembership();

            Pages.MembershipAdmin
                .ClickAddProgramsBtn(memberName)
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
            Pages.UsersAdmin
                .SearchUser()
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(memberName)
                .SelectActiveMembership(memberName);
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .SearchMembership(memberName)
                .VerifyMembershipName(memberName)
                .ClickDeleteBtn()
                .VerifyDeletingMembership(memberName);
            Pages.Login
                .GetAdminLogout();
        }

        [Test, Category("Memberships")]
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
            Pages.Login
                .GetAdminLogout();

            Browser._Driver.Navigate().GoToUrl("https://markcarrollmethod.com/");

            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.Login
                .GetAdminLogout();
        }

    }

    [TestFixture]
    [AllureNUnit]
    public class Exercises : TestBaseAdmin
    {

        [Test, Category("Exercises")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Exercises")]
        public void EditExercise()
        {
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed()
                .OpenExercisesPage();
            Pages.PopUp
                .ClosePopUp();

            List<string> relatedExerciseList = Pages.ExercisesAdmin.GetExercisesList();

            Pages.ExercisesAdmin
                .ClickCreateExerciseBtn()
                .EnterExerciseData();

            string exerciseName = TextBox.GetAttribute(Pages.ExercisesAdmin.fieldExerciseName, "value");

            Pages.Common
                .ClickSaveBtn();

            Pages.ExercisesAdmin
                .VerifyExerciseIsCreated(exerciseName)
                .ClickEditExercise(exerciseName)
                .ClickAddRelatedExercisesBtn(5)
                .AddRelatedExercises(relatedExerciseList);
            Pages.Common
                .ClickSaveBtn();
            Pages.Login
                .GetAdminLogout();
        }

        [Test, Category("Exercises")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Exercises")]
        public void DeleteRelatedExercises()
        {
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenExercisesPage();
            Pages.PopUp
                .ClosePopUp();

            List<string> relatedExerciseList = Pages.ExercisesAdmin.GetExercisesList();

            Pages.ExercisesAdmin
                .ClickCreateExerciseBtn()
                .EnterExerciseData();

            string exerciseName = TextBox.GetAttribute(Pages.ExercisesAdmin.fieldExerciseName, "value");

            Pages.Common
                .ClickSaveBtn();

            Pages.ExercisesAdmin
                .VerifyExerciseIsCreated(exerciseName);
            Pages.ExercisesAdmin
                .ClickEditExercise(exerciseName)
                .ClickAddRelatedExercisesBtn(5)
                .AddRelatedExercises(relatedExerciseList);
            Pages.Common
                .ClickSaveBtn();
            Pages.PopUp
                .ClosePopUp();
            Pages.ExercisesAdmin
                .ClickEditExercise(exerciseName)
                .RemoveRelatedExercises();
            Pages.Common
                .ClickSaveBtn();
            Pages.Login
                .GetAdminLogout();
        }

        [Test, Category("Exercises")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Exercises")]
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

        [Test, Category("Exercises")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Exercises")]
        public void CreateExerciseWithRelated()
        {
            Pages.Login
                .GetLogin(Credentials.loginAdmin, Credentials.passwordAdmin);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.Sidebar
                .OpenExercisesPage();
            Pages.PopUp
                .ClosePopUp();
            List<string> relatedExerciseList = Pages.ExercisesAdmin.GetExercisesList();
            Pages.ExercisesAdmin
                .ClickCreateExerciseBtn()
                .EnterExerciseData()
                .ClickAddRelatedExercisesBtn(5)
                .AddRelatedExercises(relatedExerciseList);
            string exerciseName = TextBox.GetAttribute(Pages.ExercisesAdmin.fieldExerciseName, "value");
            Pages.Common
                .ClickSaveBtn();
            Pages.ExercisesAdmin
                .VerifyExerciseIsCreated(exerciseName);
            Pages.Login
                .GetAdminLogout();
        }

        [Test, Category("Exercises")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Exercises")]
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
                .VerifyExerciseIsRemoved(exerciseName);

            Pages.Login
                .GetAdminLogout();
        }


    }

    [TestFixture]
    [AllureNUnit]
    public class Debug : TestBaseAdmin
    {
        [Test]
        [AllureIssue("Test")]
        //[Ignore("Debugging test")]
        public void Test()
        {
            var responseLogin = SignInRequest.MakeAdminSignIn(Credentials.loginAdmin, Credentials.passwordAdmin);
            MembershipsWithUsersRequest.GetMembershipsWithUsersList(responseLogin);

        }
    }
}