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
using MCMAutomation.APIHelpers.Client.EditUser;
using MCMAutomation.APIHelpers.Client.SignUp;
using System.Linq;
using Chilkat;

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
            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPage();
            Pages.AdminPages.MembershipAdmin
                .ClickCreateBtn()
                .EnterMembershipData();
            Pages.CommonPages.Common
                .ClickSaveBtn();
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            Pages.AdminPages.MembershipAdmin
                .SearchMembership(membershipData.Name)
                .VerifyMembershipName(membershipData.Name);

            Pages.CommonPages.Login
                .GetAdminLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);

            #endregion

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        //[Ignore("Report")]
        public void AddProgramsToExistingMembership()
        {
            #region Preconditions

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);

            #endregion

            #region Steps

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPage();
            var membershipData = AppDbContext.Memberships.GetLastMembership();
            Pages.AdminPages.MembershipAdmin
               .ClickAddProgramsBtn(membershipData.Name);
            Pages.AdminPages.MembershipAdmin
                .VerifyMembershipNameCbbx(membershipData.Name)
                .CreatePrograms();
            Pages.CommonPages.Login
                .GetAdminLogout();

            #endregion

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);

            #endregion

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
            #region Preconditions

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            var exercises = AppDbContext.Exercises.GetExercisesData();
            int programCount = 3;
            for (int i = 0; i < programCount; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(programCount);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id, programCount);
                var workouts = AppDbContext.Workouts.GetLastWorkoutsData(programCount);
                foreach (var workout in workouts)
                {
                    MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workout, exercises);
                }

            }

            #endregion

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPage();
            Pages.AdminPages.MembershipAdmin
                .ClickAddProgramsBtn(membershipData.Name);
            Pages.AdminPages.MembershipAdmin
                .VerifyMembershipNameCbbx(membershipData.Name)
                .CreatePrograms()
                .AddNextPhaseDependency();
            Pages.CommonPages.Login
                .GetAdminLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);

            #endregion

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
            #region Preconditions

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            var membershipData = AppDbContext.Memberships.GetLastMembership();
            var exercises = AppDbContext.Exercises.GetExercisesData();
            int programCount = 3;
            for (int i = 0; i < programCount; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(programCount);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id, programCount);
                var workouts = AppDbContext.Workouts.GetLastWorkoutsData(programCount);
                foreach (var workout in workouts)
                {
                    MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workout, exercises);
                }

            }

            #endregion

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPage();
            Pages.AdminPages.MembershipAdmin
                .ClickEditMembershipBtn(membershipData.Name)
                .EditMembershipData();
            Pages.CommonPages.Common
                .ClickSaveBtn();
            WaitUntil.WaitForElementToAppear(Pages.AdminPages.MembershipAdmin.searchUserInput, 30);
            DB.Memberships membershipDataAfterEditing = AppDbContext.Memberships.GetLastMembership();
            Pages.AdminPages.MembershipAdmin
                .SearchMembership(membershipDataAfterEditing.Name)
                .VerifyMembershipName(membershipData.Name, membershipDataAfterEditing.Name);

            Pages.CommonPages.Login
                .GetAdminLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);

            #endregion

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        public void RemoveProgramsFromNewMembership()
        {
            #region Preconditions

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            var exercises = AppDbContext.Exercises.GetExercisesData();
            int programCount = 3;
            for (int i = 0; i < programCount; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(programCount);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id, programCount);
                var workouts = AppDbContext.Workouts.GetLastWorkoutsData(programCount);
                foreach (var workout in workouts)
                {
                    MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workout, exercises);
                }

            }

            #endregion

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPage();
            Pages.AdminPages.MembershipAdmin
               .ClickAddProgramsBtn(membershipData.Name)
               .VerifyMembershipNameCbbx(membershipData.Name);
            Pages.AdminPages.MembershipAdmin
               .DeletePrograms(programs)
               .VerifyDeletePrograms();

            Pages.CommonPages.Login
                .GetAdminLogout();

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);

            #endregion

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        public void AddWorkoutsAndExercisesToNewProductMembership()
        {
            #region Preconditions

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();

            #region Create New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            #endregion

            #region create Product Membership for New User
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            #endregion

            #region Add Programs and Workouts to Product membership
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            int programCount = 3;
            for (int i = 0; i < programCount; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(programCount);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id, programCount);
            }
            #endregion

            #endregion

            #region Steps

            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPage();
            Pages.AdminPages.MembershipAdmin
                .ClickAddProgramsBtn(membershipData.Name)
                .VerifyMembershipNameCbbx(membershipData.Name);
            List<string> programList = Pages.AdminPages.MembershipAdmin.GetProgramNames();
            Pages.AdminPages.MembershipAdmin
                .ClickAddWorkoutBtn();
            var exercise = AppDbContext.Exercises.GetExercisesData();
            Pages.AdminPages.MembershipAdmin
                .AddWorkoutExercises(programList, exercise);
            Pages.CommonPages.Sidebar
                .OpenUsersPage();
            Pages.AdminPages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membershipData.Name)
                .SelectActiveMembership(membershipData.Name);

            Pages.CommonPages.Login
                .GetAdminLogout();

            #endregion

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);

            #endregion

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        //[Ignore("Ignore")]
        
        public void CopyExercisesToNewProductMembership()
        {
            #region Preconditions

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();

            #region Create user
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            #endregion

            #region Create Membership
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            int programCount = 3;
            for (int i = 0; i < programCount; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(programCount);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id, programCount);
            }

            #endregion

            #endregion

            #region Steps

            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPage();
            Pages.AdminPages.MembershipAdmin
                .ClickAddProgramsBtn(membershipData.Name)
                .VerifyMembershipNameCbbx(membershipData.Name);
            List<string> programList = Pages.AdminPages.MembershipAdmin.GetProgramNames();
            Pages.AdminPages.MembershipAdmin
                .ClickAddWorkoutBtn();
            List<DB.CopyMembershipPrograms> membershipDataForCopy = AppDbContext.Workouts.GetMembershipProgramWorkoutData();
            Pages.AdminPages.MembershipAdmin
                .CopyExercises(programList, membershipDataForCopy, membershipData.Name);
            Pages.CommonPages.Sidebar
                .OpenUsersPage();
            Pages.AdminPages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membershipData.Name)
                .SelectActiveMembership(membershipData.Name);

            Pages.CommonPages.Login
                .GetAdminLogout();

            #endregion

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);

            #endregion

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        //[Ignore("Ignore")]

        public void CreateNewMultilevelMembership()
        {
            #region Steps

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPage();
            Pages.AdminPages.MembershipAdmin
                .ClickCreateBtn()
                .EnterMembershipData()
                .SelectMembershipType(MembershipType.MULTILEVEL)
                .AddLevels(4);
            Pages.CommonPages.Common
                .ClickSaveBtn();
            WaitUntil.WaitForElementToAppear(Pages.AdminPages.MembershipAdmin.membershipSearchInput, 30);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();

            Pages.CommonPages.Login
                .GetAdminLogout();

            #endregion

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);

            #endregion

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        //[Ignore("Ignore")]

        public void CopyExercisesToNewSubscriptionMembership()
        {
            #region Preconditions

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();

            #region Create User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            #endregion

            #region Create membership

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateSubscriptionMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            int programCount = 3;
            for (int i = 0; i < programCount; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(programCount);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id, programCount);
            }
            #endregion

            #endregion

            #region Steps

            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPage();
            Pages.AdminPages.MembershipAdmin
                .ClickAddProgramsBtn(membershipData.Name)
                .VerifyMembershipNameCbbx(membershipData.Name);
            List<string> programList = Pages.AdminPages.MembershipAdmin.GetProgramNames();
            Pages.AdminPages.MembershipAdmin
                .ClickAddWorkoutBtn();
            List<DB.CopyMembershipPrograms> membershipDataForCopy = AppDbContext.Workouts.GetMembershipProgramWorkoutData();
            Pages.AdminPages.MembershipAdmin
                .CopyExercises(programList, membershipDataForCopy, membershipData.Name);
            Pages.CommonPages.Sidebar
                .OpenUsersPage();
            Pages.AdminPages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membershipData.Name)
                .SelectActiveMembership(membershipData.Name);

            Pages.CommonPages.Login
                .GetAdminLogout();

            #endregion

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);

            #endregion

        }

        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        //[Ignore("Ignore")]

        public void CopyExercisesToNewCustomMembership()
        {
            #region Preconditions

            #region Create User
            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Create custom membership

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateCustomMembership(responseLoginAdmin, userId);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            const int programCount = 3;
            var programs = MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData, programCount);
            var workouts = MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount);
            
            #endregion

            #endregion

            #region Steps

            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPage();
            Pages.AdminPages.MembershipAdmin
                .ClickAddProgramsBtn(membershipData.Name)
                .VerifyMembershipNameCbbx(membershipData.Name);
            List<string> programList = Pages.AdminPages.MembershipAdmin.GetProgramNames();
            Pages.AdminPages.MembershipAdmin
                .ClickAddWorkoutBtn();
            List<DB.CopyMembershipPrograms> membershipDataForCopy = AppDbContext.Workouts.GetMembershipProgramWorkoutData();
            Pages.AdminPages.MembershipAdmin
                .CopyExercises(programList, membershipDataForCopy, membershipData.Name);
            Pages.CommonPages.Login
                .GetAdminLogout();

            #endregion

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);

            #endregion

        }


        [Test, Category("Memberships")]
        [AllureTag("Regression")]
        [AllureOwner("Artem Sukharevskyi")]
        [AllureSeverity(SeverityLevel.critical)]
        [Author("Artem", "qatester91311@gmail.com")]
        [AllureSuite("Admin")]
        [AllureSubSuite("Memberships")]
        public void CreateAndRemoveNewMembershipThatAddedToUser()
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
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            const int programCount = 3;
            var programs = MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData, programCount);
            var workouts = MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion

            #endregion

            #region Steps

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenMemberShipPage();
            Pages.AdminPages.MembershipAdmin
                .SearchMembership(membershipData.Name)
                .VerifyMembershipName(membershipData.Name)
                .ClickDeleteBtn()
                .VerifyDeletingMembership(membershipData.Name);
            Pages.CommonPages.Login
                .GetAdminLogout();

            #endregion

            #region Postconditions

            AppDbContext.Memberships.DeleteMembership(membershipData.Name);

            #endregion
        }

        //[Test, Category("Memberships")]
        //[AllureTag("Regression")]
        //[AllureOwner("Artem Sukharevskyi")]
        //[AllureSeverity(SeverityLevel.critical)]
        //[Author("Artem", "qatester91311@gmail.com")]
        //[AllureSuite("Admin")]
        //[AllureSubSuite("Memberships")]
        //public void OpenMemberShipPage()
        //{
        //    Pages.CommonPages.Login
        //        .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
        //    Pages.CommonPages.Sidebar
        //        .VerifyIsLogoDisplayed();

        //    Pages.CommonPages.Sidebar
        //        .OpenMemberShipPage();
        //    Pages.CommonPages.Login
        //        .GetAdminLogout();

        //    Browser._Driver.Navigate().GoToUrl("https://markcarrollmethod.com/");

        //    Pages.CommonPages.Login
        //        .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
        //    Pages.CommonPages.Sidebar
        //        .VerifyIsLogoDisplayed();

        //    Pages.CommonPages.Sidebar
        //        .OpenMemberShipPage();
        //    Pages.CommonPages.Login
        //        .GetAdminLogout();
        //}

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
            #region Preconditions

            var list = AppDbContext.Exercises.GetExercisesData();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            ExercisesRequestPage.AddExercisesWithoutRelated(responseLoginAdmin, list);
            var lastExercise = AppDbContext.Exercises.GetLastExerciseData();

            #endregion

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenExercisesPage();
            Pages.AdminPages.ExercisesAdmin
                .VerifyExerciseIsCreated(lastExercise.Name)
                .ClickEditExercise(lastExercise.Name)
                .ClickAddRelatedExercisesBtn(5)
                .AddRelatedExercises(list);
            Pages.CommonPages.Common
                .ClickSaveBtn();
            Pages.CommonPages.Login
                .GetAdminLogout();

            #region Postconditions

            AppDbContext.Exercises.DeleteExercises(lastExercise.Name);

            #endregion
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
            #region Preconditions

            var list = AppDbContext.Exercises.GetExercisesData();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            ExercisesRequestPage.AddExercisesWithRelated(responseLoginAdmin, list);
            var lastExercise = AppDbContext.Exercises.GetLastExerciseData();

            #endregion

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenExercisesPage();
            Pages.AdminPages.ExercisesAdmin
                .VerifyExerciseIsCreated(lastExercise.Name);
            Pages.AdminPages.ExercisesAdmin
                .ClickEditExercise(lastExercise.Name)
                .RemoveRelatedExercises();
            Pages.CommonPages.Common
                .ClickSaveBtn();
            Pages.CommonPages.Login
                .GetAdminLogout();

            #region Postconditions

            AppDbContext.Exercises.DeleteExercises(lastExercise.Name);

            #endregion
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
            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenExercisesPage();
            Pages.AdminPages.ExercisesAdmin
                .ClickCreateExerciseBtn()
                .EnterExerciseData();
            string exerciseName = TextBox.GetAttribute(Pages.AdminPages.ExercisesAdmin.fieldExerciseName, "value");
            Pages.CommonPages.Common
                .ClickSaveBtn();
            Pages.AdminPages.ExercisesAdmin
                .VerifyExerciseIsCreated(exerciseName);
            Pages.CommonPages.Login
                .GetAdminLogout();

            #region Postconditions

            AppDbContext.Exercises.DeleteExercises(exerciseName);

            #endregion
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
            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenExercisesPage();
            List<string> relatedExerciseList = Pages.AdminPages.ExercisesAdmin.GetExercisesList();
            Pages.AdminPages.ExercisesAdmin
                .ClickCreateExerciseBtn()
                .EnterExerciseData()
                .ClickAddRelatedExercisesBtn(5)
                .AddRelatedExercises(relatedExerciseList);
            string exerciseName = TextBox.GetAttribute(Pages.AdminPages.ExercisesAdmin.fieldExerciseName, "value");
            Pages.CommonPages.Common
                .ClickSaveBtn();
            Pages.AdminPages.ExercisesAdmin
                .VerifyExerciseIsCreated(exerciseName);
            Pages.CommonPages.Login
                .GetAdminLogout();

            #region Postconditions

            AppDbContext.Exercises.DeleteExercises(exerciseName);

            #endregion
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
            #region Preconditions

            var list = AppDbContext.Exercises.GetExercisesData();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            ExercisesRequestPage.AddExercisesWithoutRelated(responseLoginAdmin, list);
            var lastExercise = AppDbContext.Exercises.GetLastExerciseData();

            #endregion

            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenExercisesPage();
            Pages.AdminPages.ExercisesAdmin
                .VerifyExerciseIsCreated(lastExercise.Name)
                .RemoveExercise(lastExercise.Name)
                .VerifyExerciseIsRemoved(lastExercise.Name);

            Pages.CommonPages.Login
                .GetAdminLogout();

            #region Postconditions

            AppDbContext.Exercises.DeleteExercises(lastExercise.Name);

            #endregion
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
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 20, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            var userData = AppDbContext.User.GetUserData(email);
            Pages.WebPages.Nutrition.VerifyMaintainCaloriesStep01(userData, TDEE.ActivityLevel[0], "Male", "Have you been dieting long term?", "Yes");
        }

        [Test]
        [AllureIssue("Test")]
        //[Ignore("Debugging test")]
        public void Test1()
        {
            //var responseLogin = SignInRequest.MakeSignIn(Credentials.loginAdmin, Credentials.passwordAdmin);
            //MembershipsWithUsersRequest.GetMembershipsWithUsersList(responseLogin);



            Pages.CommonPages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.CommonPages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.CommonPages.PopUp
                .ClosePopUp();
            Pages.CommonPages.Sidebar
                .OpenExercisesPage();
        }
    }
}