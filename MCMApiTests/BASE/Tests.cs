using Chilkat;
using MCMAutomation.APIHelpers;
using MCMAutomation.APIHelpers.Client.AddProgress;
using MCMAutomation.APIHelpers.Client.EditUser;
using MCMAutomation.APIHelpers.Client.SignUp;
using MCMAutomation.APIHelpers.Client.WeightTracker;
using MCMAutomation.APIHelpers.SignInPage;
using MCMAutomation.Helpers;
using Newtonsoft.Json.Linq;
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

    public class AuthTests
    {
        [Test]
        public void MakeSignIn()
        {
            var responseLogin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            SignInAssertions
                .VerifyIsAdminSignInSuccesfull(responseLogin);
        }

    }

    [TestFixture]

    public class AdminTests {

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
    }

    [TestFixture]
    public class UserTests
    {
        [Test]
        public void AddWeight()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add Weight daily
            int countOfRecods = 1000;
            for (int i = 0; i < countOfRecods; i++)
            {
                WeightTracker.AddWeight(responseLoginUser, RandomHelper.RandomProgressData("weight"), RandomHelper.RandomDateInThePast(), 0, false);
            }
            const string dateFormat = "yyyy-MM-ddTHH:mm:ss";
            var userProgress = AppDbContext.User.GetProgressDailyByUserId(userId);
            var weightList = WeightTracker.GetWeightList(responseLoginUser, ConversionSystem.METRIC, 0, countOfRecods + 100);

            var weightListIds = weightList.Select(w => w.id);
            var weightListDates = weightList.Select(w => w.date);
            var weightListWeights = weightList.Select(w => w.weight);

            Assert.That(userProgress.Select(p => p.Id).SequenceEqual(weightListIds), Is.True, "Ids don't match");
            Assert.That(userProgress.Select(p => p.Date.ToString(dateFormat)).SequenceEqual(weightListDates), Is.True, "Dates don't match");
            Assert.That(userProgress.Select(p => p.Weight).SequenceEqual(weightListWeights), Is.True, "Weights don't match");

            WeightTracker.VerifyAverageOfWeightTracking(userProgress, weightList);

            #endregion

        }

        [Test]
        public void EditWeight()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add Weight daily
            int countOfRecods = 5;
            for (int i = 0; i < countOfRecods; i++)
            {
                WeightTracker.AddWeight(responseLoginUser, RandomHelper.RandomProgressData("weight"), RandomHelper.RandomDateInThePast(), 0, false);
            }
            const string dateFormat = "yyyy-MM-ddTHH:mm:ss";
            var userProgress = AppDbContext.User.GetProgressDailyByUserId(userId);
            var weightList = WeightTracker.GetWeightList(responseLoginUser, ConversionSystem.METRIC, 0, countOfRecods + 100);

            var weightListIds = weightList.Select(w => w.id);
            var weightListDates = weightList.Select(w => w.date);
            var weightListWeights = weightList.Select(w => w.weight);

            Assert.That(userProgress.Select(p => p.Id).SequenceEqual(weightListIds), Is.True, "Ids don't match");
            Assert.That(userProgress.Select(p => p.Date.ToString(dateFormat)).SequenceEqual(weightListDates), Is.True, "Dates don't match");
            Assert.That(userProgress.Select(p => p.Weight).SequenceEqual(weightListWeights), Is.True, "Weights don't match");

            #endregion

            #region Edit Weight daily

            foreach (var weightId in weightListIds)
            {
                WeightTracker.EditWeight(responseLoginUser, RandomHelper.RandomProgressData("weight"), ConversionSystem.METRIC, weightId);
            }
            userProgress = AppDbContext.User.GetProgressDailyByUserId(userId);
            weightList = WeightTracker.GetWeightList(responseLoginUser, ConversionSystem.METRIC, 0, countOfRecods + 100);

            weightListIds = weightList.Select(w => w.id);
            weightListDates = weightList.Select(w => w.date);
            weightListWeights = weightList.Select(w => w.weight);

            Assert.That(userProgress.Select(p => p.Id).SequenceEqual(weightListIds), Is.True, "Ids don't match");
            Assert.That(userProgress.Select(p => p.Date.ToString(dateFormat)).SequenceEqual(weightListDates), Is.True, "Dates don't match");
            Assert.That(userProgress.Select(p => p.Weight).SequenceEqual(weightListWeights), Is.True, "Weights don't match");

            #endregion
        }

        [Test]
        public void DeleteWeight()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add Weight daily
            int countOfRecods = 5;
            for (int i = 0; i < countOfRecods; i++)
            {
                WeightTracker.AddWeight(responseLoginUser, RandomHelper.RandomProgressData("weight"), RandomHelper.RandomDateInThePast(), 0, false);
            }
            const string dateFormat = "yyyy-MM-ddTHH:mm:ss";
            var userProgress = AppDbContext.User.GetProgressDailyByUserId(userId);
            var weightList = WeightTracker.GetWeightList(responseLoginUser, ConversionSystem.METRIC, 0, countOfRecods + 100);

            var weightListIds = weightList.Select(w => w.id);
            var weightListDates = weightList.Select(w => w.date);
            var weightListWeights = weightList.Select(w => w.weight);

            Assert.That(userProgress.Select(p => p.Id).SequenceEqual(weightListIds), Is.True, "Ids don't match");
            Assert.That(userProgress.Select(p => p.Date.ToString(dateFormat)).SequenceEqual(weightListDates), Is.True, "Dates don't match");
            Assert.That(userProgress.Select(p => p.Weight).SequenceEqual(weightListWeights), Is.True, "Weights don't match");

            #endregion

            #region Delete Weight daily

            foreach (var weighId in weightListIds)
            {
                WeightTracker.DeleteWeight(responseLoginUser, weighId);
            }
            WaitUntil.WaitSomeInterval(1000);
            userProgress = AppDbContext.User.GetProgressDailyByUserId(userId);
            foreach (var progress in userProgress)
            {
                Assert.That(progress.IsDeleted, Is.True, "Ids don't match");
            }
            var s = WeightTracker.GetWeightList(responseLoginUser, ConversionSystem.METRIC, 0, countOfRecods + 100);

            Assert.IsTrue(s.Count == 0);

            #endregion
        }
    }

    [TestFixture]
    public class Demo { 

        [Test]
        public void DemoTest()
        {
            #region Register New User

            var userData = AppDbContext.User.GetLastUser();
            var responseLoginUser = SignInRequest.MakeSignIn(userData.Email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);

            #endregion

            #region Add Weight daily
            int countOfRecods = 1000;
            //for (int i = 0; i < countOfRecods; i++)
            //{
            //    WeightTracker.AddWeight(responseLoginUser, RandomHelper.RandomProgressData("weight"), RandomHelper.RandomDateInThePast(), 0, false);
            //}
            const string dateFormat = "yyyy-MM-ddTHH:mm:ss";
            var userProgress = AppDbContext.User.GetProgressDailyByUserId(userData.Id);
            var weightList = WeightTracker.GetWeightList(responseLoginUser, ConversionSystem.METRIC, 0, countOfRecods + 100);

            var weightListIds = weightList.Select(w => w.id).Reverse();
            var weightListDates = weightList.Select(w => w.date).Reverse();
            var weightListWeights = weightList.Select(w => w.weight).Reverse();
            //Assert.Multiple(() =>
            //{
            //    Assert.That(userProgress.Select(p => p.Id).SequenceEqual(weightListIds), Is.True, "Ids don't match");
            //    Assert.That(userProgress.Select(p => p.Date.ToString(dateFormat)).SequenceEqual(weightListDates), Is.True, "Dates don't match");
            //    Assert.That(userProgress.Select(p => p.Weight).SequenceEqual(weightListWeights), Is.True, "Weights don't match");
            //});
            WeightTracker.VerifyAverageOfWeightTracking(userProgress, weightList);

            #endregion
        }
    }
}
