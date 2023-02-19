using Chilkat;
using MCMAutomation.APIHelpers;
using MCMAutomation.APIHelpers.Client.AddProgress;
using MCMAutomation.APIHelpers.Client.EditUser;
using MCMAutomation.APIHelpers.Client.SignUp;
using MCMAutomation.APIHelpers.SignInPage;
using MCMAutomation.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace MCMApiTests
{
    [TestFixture]
    
    public class Tests
    {
        [Test]
        public void MakeSignIn()
        {
            var responseLogin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            SignInAssertions
                .VerifyIsAdminSignInSuccesfull(responseLogin);
        }

        [Test]
        public void AddMembershipToUser()
        {
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
            DB.Memberships membershipId = AppDbContext.Memberships.GetLastMembership();
            var exercises = AppDbContext.Exercises.GetExercisesData();
            int programCount = 3;
            for (int i = 0; i < programCount; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipId.Id);
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
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipId.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion
        }

        [Test]
        public void CreateProductMembership()
        {
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            DB.Memberships membershipId = AppDbContext.Memberships.GetLastMembership();
            var exercises = AppDbContext.Exercises.GetExercisesData();
            int programCount = 2;
            for (int i = 0; i < programCount; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipId.Id);
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
        }

        [Test]
        public void Demo()
        {
            var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("MCM_BIKINI_SUB");

            //#region Register New User
            //string email = RandomHelper.RandomEmail();
            //SignUpRequest.RegisterNewUser(email);
            //var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            //EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            //string userId = AppDbContext.User.GetUserData(email).Id;
            //#endregion

            //#region Add and Activate membership to User

            //var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            //var membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU("ARD");
            //MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            //int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            //MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            //membership = AppDbContext.Memberships.GetActiveMembershipNameBySKU(MembershipsSKU.MEMBERSHIP_SKU[1]);
            //MembershipRequest.AddUsersToMembership(responseLoginAdmin, membership.Id, userId);
            //userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            //MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            //#endregion

        }
    }
}
