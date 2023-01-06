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
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
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

            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();

            Pages.MembershipAdmin
                .SearchMembership(membershipData.Name)
                .VerifyMembershipName(membershipData.Name);

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
        //[Ignore("Report")]
        public void AddProgramsToExistingMembership()
        {
            #region Preconditions

            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);

            #endregion

            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            Pages.MembershipAdmin
               .ClickAddProgramsBtn(membershipData.Name);
            Pages.MembershipAdmin
                .VerifyMembershipNameCbbx(membershipData.Name)
                .CreatePrograms();
            Pages.Login
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
        public void EditPrograms()
        {
            #region Preconditions

            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            var exercises = AppDbContext.Exercises.GetExercisesData();
            for (int i = 0; i < 3; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(3);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id);
                var workouts = AppDbContext.Workouts.GetLastWorkoutsData(5);
                foreach (var workout in workouts)
                {
                    MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workout, exercises);
                }

            }

            #endregion

            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .ClickAddProgramsBtn(membershipData.Name);
            Pages.MembershipAdmin
                .VerifyMembershipNameCbbx(membershipData.Name)
                .CreatePrograms();
            List<string> programList = Pages.MembershipAdmin.GetProgramNames();
            Pages.MembershipAdmin
                .AddNextPhaseDependency(programList);
            Pages.Login
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

            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            var exercises = AppDbContext.Exercises.GetExercisesData();
            for (int i = 0; i < 3; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(3);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id);
                var workouts = AppDbContext.Workouts.GetLastWorkoutsData(5);
                foreach (var workout in workouts)
                {
                    MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workout, exercises);
                }

            }

            #endregion

            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.MembershipAdmin
                .ClickEditMembershipBtn(membershipData.Name)
                .EditMembershipData();
            Pages.Common
                .ClickSaveBtn();

            DB.Memberships membershipDataAfterEditing = AppDbContext.Memberships.GetLastMembership();

            Pages.MembershipAdmin
                .SearchMembership(membershipDataAfterEditing.Name)
                .VerifyMembershipName(membershipData.Name, membershipDataAfterEditing.Name);

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

        //    string memberName = AppDbContext.Memberships.GetLastMembership();

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
            #region Preconditions

            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            var exercises = AppDbContext.Exercises.GetExercisesData();
            for (int i = 0; i < 3; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(3);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id);
                var workouts = AppDbContext.Workouts.GetLastWorkoutsData(5);
                foreach (var workout in workouts)
                {
                    MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workout, exercises);
                }

            }

            #endregion

            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
               .ClickAddProgramsBtn(membershipData.Name)
               .VerifyMembershipNameCbbx(membershipData.Name)
               .CreateProgramsMega();
            Pages.Sidebar
               .OpenMemberShipPage();
            Pages.MembershipAdmin
               .ClickAddProgramsBtn(membershipData.Name);

            List<string> programList = Pages.MembershipAdmin.GetProgramNames();

            Pages.MembershipAdmin
                .DeletePrograms(programList)
                .VerifyDeletePrograms();

            Pages.Login
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
            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            #region Create New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            #endregion

            #region create Product Membership for New User
            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            #endregion

            #region Add Programs and Workouts to Product membership
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            for (int i = 0; i < 3; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(3);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id);
            }
            #endregion

            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .ClickAddProgramsBtn(membershipData.Name)
                .VerifyMembershipNameCbbx(membershipData.Name);
            List<string> programList = Pages.MembershipAdmin.GetProgramNames();
            Pages.MembershipAdmin
                .ClickAddWorkoutBtn();
            var exercise = AppDbContext.Exercises.GetExercisesData();
            Pages.MembershipAdmin
                .AddWorkoutExercises(programList, exercise);
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membershipData.Name)
                .SelectActiveMembership(membershipData.Name);

            Pages.Login
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
        //[Ignore("Ignore")]
        
        public void CopyExercisesToNewProductMembership()
        {
            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            for (int i = 0; i < 3; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(3);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id);
            }
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .ClickAddProgramsBtn(membershipData.Name)
                .VerifyMembershipNameCbbx(membershipData.Name);
            List<string> programList = Pages.MembershipAdmin.GetProgramNames();
            Pages.MembershipAdmin
                .ClickAddWorkoutBtn();
            List<DB.CopyMembershipPrograms> membershipDataForCopy = AppDbContext.Workouts.GetMembershipProgramWorkoutData();
            Pages.MembershipAdmin
                .CopyExercises(programList, membershipDataForCopy, membershipData.Name);
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membershipData.Name)
                .SelectActiveMembership(membershipData.Name);

            Pages.Login
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
        //[Ignore("Ignore")]

        public void CreateNewMultilevelMembership()
        {
            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .ClickCreateBtn()
                .EnterMembershipData()
                .SelectMembershipType(MembershipType.MULTILEVEL)
                .AddLevels(4);
            Pages.Common
                .ClickSaveBtn();
            WaitUntil.CustomElevemtIsVisible(Pages.MembershipAdmin.membershipSearchInput, 30);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();

            Pages.Login
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
        //[Ignore("Ignore")]

        public void CopyExercisesToNewSubscriptionMembership()
        {
            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateSubscriptionMembership(responseLoginAdmin);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            for (int i = 0; i < 3; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(3);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id);
            }
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .ClickAddProgramsBtn(membershipData.Name)
                .VerifyMembershipNameCbbx(membershipData.Name);
            List<string> programList = Pages.MembershipAdmin.GetProgramNames();
            Pages.MembershipAdmin
                .ClickAddWorkoutBtn();
            List<DB.CopyMembershipPrograms> membershipDataForCopy = AppDbContext.Workouts.GetMembershipProgramWorkoutData();
            Pages.MembershipAdmin
                .CopyExercises(programList, membershipDataForCopy, membershipData.Name);
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membershipData.Name)
                .SelectActiveMembership(membershipData.Name);

            Pages.Login
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
        //[Ignore("Ignore")]

        public void CopyExercisesToNewCustomMembership()
        {
            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            string userId = AppDbContext.User.GetUserData(email).Id;
            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateCustomMembership(responseLoginAdmin, userId);
            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();
            for (int i = 0; i < 3; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id);
            }
            List<DB.Programs> programs = AppDbContext.Programs.GetLastPrograms(3);
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id);
            }
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .ClickAddProgramsBtn(membershipData.Name)
                .VerifyMembershipNameCbbx(membershipData.Name);
            List<string> programList = Pages.MembershipAdmin.GetProgramNames();
            Pages.MembershipAdmin
                .ClickAddWorkoutBtn();
            List<DB.CopyMembershipPrograms> membershipDataForCopy = AppDbContext.Workouts.GetMembershipProgramWorkoutData();
            Pages.MembershipAdmin
                .CopyExercises(programList, membershipDataForCopy, membershipData.Name);
            Pages.Login
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
        public void CreateAndRemoveNewMembership()
        {
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLogin = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLogin);
            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
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

            DB.Memberships membershipData = AppDbContext.Memberships.GetLastMembership();

            Pages.MembershipAdmin
                .ClickAddProgramsBtn(membershipData.Name)
                .VerifyMembershipNameCbbx(membershipData.Name)
                .CreatePrograms();

            List<string> programList = Pages.MembershipAdmin.GetProgramNames();

            Pages.MembershipAdmin
                .ClickAddWorkoutBtn()
                .CreateWorkouts(programList);

            var exercise = AppDbContext.Exercises.GetExercisesData();

            Pages.MembershipAdmin
                .AddWorkoutExercises(programList, exercise);
            Pages.Sidebar
                .OpenUsersPage();
            Pages.UsersAdmin
                .SearchUser(email)
                .VerifyDisplayingOfUser(email)
                .ClickEditUser(email)
                .AddMembershipToUser(membershipData.Name)
                .SelectActiveMembership(membershipData.Name);
            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.MembershipAdmin
                .SearchMembership(membershipData.Name)
                .VerifyMembershipName(membershipData.Name)
                .ClickDeleteBtn()
                .VerifyDeletingMembership(membershipData.Name);
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
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();

            Pages.Sidebar
                .OpenMemberShipPage();
            Pages.Login
                .GetAdminLogout();

            Browser._Driver.Navigate().GoToUrl("https://markcarrollmethod.com/");

            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
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
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenExercisesPage();

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
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenExercisesPage();

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
            WaitUntil.WaitSomeInterval(2000);
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
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenExercisesPage();
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
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenExercisesPage();
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
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.PopUp
                .ClosePopUp();
            Pages.Sidebar
                .OpenExercisesPage();
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
        [Ignore("Debugging test")]
        public void Test()
        {
            //var responseLogin = SignInRequest.MakeAdminSignIn(Credentials.loginAdmin, Credentials.passwordAdmin);
            //MembershipsWithUsersRequest.GetMembershipsWithUsersList(responseLogin);

            

            Pages.Login
                .GetLogin(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            Pages.Sidebar
                .VerifyIsLogoDisplayed();
            Pages.Sidebar
                .OpenExercisesPage();
        }
    }
}