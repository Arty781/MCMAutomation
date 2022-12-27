﻿using Chilkat;
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
            var responseLogin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            SignInAssertions
                .VerifyIsAdminSignInSuccesfull(responseLogin);
        }

        [Test]
        public void AddMembershipToUser()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.GetUserId(email);
            #endregion

            #region Add and Activate membership to User


            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin);
            List<DB.Memberships> membershipId = AppDbContext.GetLastMembership();
            var exercises = AppDbContext.GetExercisesData();
            for (int i = 0; i < 5; i++)
            {
                MembershipRequest.CreatePrograms(responseLoginAdmin, membershipId.FirstOrDefault().Id);
            }
            List<DB.Programs> programs = AppDbContext.GetLastPrograms();
            foreach (var program in programs)
            {
                MembershipRequest.CreateWorkouts(responseLoginAdmin, program.Id);
                var workouts = AppDbContext.GetLastWorkoutsData();
                foreach (var workout in workouts)
                {
                    MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workout, exercises);
                }

            }
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipId.FirstOrDefault().Id, userId);
            int userMembershipId = AppDbContext.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            #endregion
        }

        [Test]
        //[Repeat(4)]
        public void Demo()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeAdminSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.GetUserId(email);
            #endregion

            var responseLoginAdmin = SignInRequest.MakeAdminSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var memberships = MembershipRequest.GetMembershipsSummary(responseLoginAdmin);
            for(int i =0; i < 4; i++)
            {
                MembershipRequest.AddUsersToMembership(responseLoginAdmin, memberships[RandomHelper.RandomNumFromOne(memberships.Count)].Id, userId);
            }
            int userMembershipId = AppDbContext.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            List<int> usermemberships = AppDbContext.GetTop2UsermembershipId(email);
            AppDbContext.UpdateTop2UsermembershipToComingSoon(usermemberships.FirstOrDefault());
            AppDbContext.UpdateTop2UsermembershipToExpire(usermemberships.LastOrDefault());

        }
    }
}
