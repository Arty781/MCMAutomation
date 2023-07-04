﻿using AngleSharp.Dom;
using MCMAutomation.APIHelpers;
using MCMAutomation.APIHelpers.Client.AddProgress;
using MCMAutomation.APIHelpers.Client.EditUser;
using MCMAutomation.APIHelpers.Client.Membership;
using MCMAutomation.APIHelpers.Client.SignUp;
using MCMAutomation.APIHelpers.Client.WeightTracker;
using MCMAutomation.APIHelpers.SignInPage;
using MCMAutomation.Helpers;
using NUnit.Framework;

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

    public class AdminMembershipsTests 
    {

        [Test, Category("Memberships")]
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
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            const int programCount = 3;
            MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out List<DB.Programs> programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion
        }

        [Test, Category("Memberships")]
        public void CreateProductMembership()
        {
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membership);
            var exercises = AppDbContext.Exercises.GetExercisesData();
            int programCount = 3;
            MembershipRequest.CreatePrograms(responseLoginAdmin, membership.Id, programCount, out List<DB.Programs> programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
            MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workouts, exercises);
                

            
        }

        [Test, Category("Memberships")]
        public void CreateSubAllMembership()
        {
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var listOfMemberships = AppDbContext.Memberships.GetAllMemberships().Where(x=> x.SKU != null && !x.SKU.StartsWith("CH")).ToList();
            MembershipRequest.CreateSubAllMembership(responseLoginAdmin, MembershipsSKU.SKU_SUBALL_MEMBER, listOfMemberships, 6);

        }

        [Test, Category("Memberships")]
        public void EditSubAllMembership()
        {
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var listOfMemberships = AppDbContext.Memberships.GetAllMemberships().Where(x => x.Type == 0 && x.IsDeleted == false).ToList();
            MembershipRequest.CreateSubAllMembership(responseLoginAdmin, MembershipsSKU.SKU_SUBALL_MEMBER, listOfMemberships, 6);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membership);
            var subAllMemberships = AppDbContext.SubAllMemberships.GetSubAllMembershipsGroup(membership.Id);
            MembershipRequest.EditSubAllMembership(responseLoginAdmin, MembershipsSKU.SKU_SUBALL_MEMBER, membership.Id, subAllMemberships);
            AppDbContext.Memberships.GetLastMembership(out membership);
            AppDbContext.Memberships.DeleteMembership(membership.Name);

        }




    }

    [TestFixture]
    public class AdminExercisesTests
    {
        [Test, Category("Exercises")]
        public void AddExerciseWithGymRelated()
        {
            bool home = false;
            bool all = false;
            var list = AppDbContext.Exercises.GetExercisesData();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            ExercisesRequestPage.AddExercisesWithRelated(responseLoginAdmin, list, home, all);
            var lastExercise = AppDbContext.Exercises.GetLastExerciseData();
            ExercisesRequestPage.VerifyExerciseAdded(responseLoginAdmin, lastExercise.Name);

            #region Postconditions

            AppDbContext.Exercises.DeleteExercises(lastExercise.Name);

            #endregion
        }

        [Test, Category("Exercises")]
        public void AddExerciseWithHomeRelated()
        {
            bool home = true;
            bool all = false;
            var list = AppDbContext.Exercises.GetExercisesData();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            ExercisesRequestPage.AddExercisesWithRelated(responseLoginAdmin, list, home, all);
            var lastExercise = AppDbContext.Exercises.GetLastExerciseData();
            ExercisesRequestPage.VerifyExerciseAdded(responseLoginAdmin, lastExercise.Name);

            #region Postconditions

            AppDbContext.Exercises.DeleteExercises(lastExercise.Name);

            #endregion
        }

        [Test, Category("Exercises")]
        public void AddExerciseWithRelated()
        {
            bool home = false;
            bool all = true;
            var list = AppDbContext.Exercises.GetExercisesData();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            ExercisesRequestPage.AddExercisesWithRelated(responseLoginAdmin, list, home, all);
            var lastExercise = AppDbContext.Exercises.GetLastExerciseData();
            ExercisesRequestPage.VerifyExerciseAdded(responseLoginAdmin, lastExercise.Name);

            #region Postconditions

            AppDbContext.Exercises.DeleteExercises(lastExercise.Name);

            #endregion
        }

        [Test, Category("Exercises")]
        public void AddExerciseWithoutRelated()
        {
            var list = AppDbContext.Exercises.GetExercisesData();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            ExercisesRequestPage.AddExercisesWithoutRelated(responseLoginAdmin, list);
            var lastExercise = AppDbContext.Exercises.GetLastExerciseData();
            ExercisesRequestPage.VerifyExerciseAdded(responseLoginAdmin, lastExercise.Name);

            #region Postconditions

            AppDbContext.Exercises.DeleteExercises(lastExercise.Name);

            #endregion
        }

        [Test, Category("Exercises")]
        public void EditExerciseWithRelated()
        {
            bool home = false;
            bool all = true;
            var list = AppDbContext.Exercises.GetExercisesData();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            ExercisesRequestPage.AddExercisesWithRelated(responseLoginAdmin, list, home, all);

            // Get the last exercise data
            var lastExercise = AppDbContext.Exercises.GetLastExerciseData();

            // Verify that the exercise was added
            ExercisesRequestPage.VerifyExerciseAdded(responseLoginAdmin, lastExercise.Name);

            // Get the list of exercises and edit exercises with related
            var exercisesList = ExercisesRequestPage.GetExercisesList(responseLoginAdmin);
            ExercisesRequestPage.EditExercisesWithRelated(responseLoginAdmin, list, exercisesList, lastExercise.Name, home, all);

            #region Postconditions

            AppDbContext.Exercises.DeleteExercises(lastExercise.Name);

            #endregion
        }

        [Test, Category("Exercises")]
        public void EditExerciseWithoutRelated()
        {
            bool home = false;
            bool all = true;
            var list = AppDbContext.Exercises.GetExercisesData();
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            ExercisesRequestPage.AddExercisesWithRelated(responseLoginAdmin, list, home, all);
            var lastExercise = AppDbContext.Exercises.GetLastExerciseData();
            ExercisesRequestPage.VerifyExerciseAdded(responseLoginAdmin, lastExercise.Name);
            var getResponseExercises = ExercisesRequestPage.GetExercisesList(responseLoginAdmin);
            ExercisesRequestPage.EditExercisesWithRelated(responseLoginAdmin, list, getResponseExercises, lastExercise.Name, home, all);

            #region Postconditions

            AppDbContext.Exercises.DeleteExercises(lastExercise.Name);

            #endregion

        }
    }

    [TestFixture]
    public class Register
    {
        [Test, Category("Daily Weight")]
        public void RegisterUser()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            #endregion


        }

    }

    [TestFixture]
    public class Progress
    {

        [Test, Category("Weekly Progress")]
        public void AddWeeklyProgress()
        {
            string email = "qatester91311@gmail.com";
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            const int conversionSystem = ConversionSystem.METRIC;

            #region Add Progress as User

            for (int i = 0; i < 8; i++)
            {
                ProgressRequest.AddProgress(responseLoginUser, conversionSystem);
                AppDbContext.Progress.UpdateUserProgressDate(userId);
            }
            #endregion
        }

        [Test, Category("Daily Weight")]
        public void AddWeight()
        {
            #region Register New User
            //string email = RandomHelper.RandomEmail();
            string email = "qatester91311@gmail.com";
            //ignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add Weight daily
            const int recordCount = 30;
            const int conversionSystem = ConversionSystem.METRIC;

            Enumerable.Range(0, recordCount)
          .ToList()
          .ForEach(_ =>
          {
              var weight = RandomHelper.RandomProgressData(ProgressBodyPart.WEIGHT);
              var date = DateTime.Now.ToString("yyyy-MM-dd");
              WeightTracker.AddWeight(responseLoginUser, weight, date, conversionSystem, false);
              AppDbContext.Progress.UpdateUserDailyProgressDate(userId);
          });
            WeightTracker.VerifyAddedWeight(responseLoginUser, conversionSystem, userId);
            WeightTracker.VerifyAverageOfWeightTracking(responseLoginUser, conversionSystem, userId);


            #endregion

        }

        [Test, Category("Daily Weight")]
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
            const int conversionSystem = ConversionSystem.METRIC;
            for (int i = 0; i < countOfRecods; i++)
            {
                WeightTracker.AddWeight(responseLoginUser, RandomHelper.RandomProgressData(ProgressBodyPart.WEIGHT), RandomHelper.RandomDateInThePast(), 0, false);
            }
            WeightTracker.VerifyAddedWeight(responseLoginUser, conversionSystem, userId);

            #endregion

            #region Edit Weight daily
            var weightList = WeightTracker.GetWeightList(responseLoginUser, conversionSystem, 0, countOfRecods);
            foreach (var weight in weightList)
            {
                WeightTracker.EditWeight(responseLoginUser, RandomHelper.RandomProgressData(ProgressBodyPart.WEIGHT), ConversionSystem.METRIC, weight.id);
            }
            WeightTracker.VerifyAddedWeight(responseLoginUser, conversionSystem, userId);

            #endregion
        }

        [Test, Category("Daily Weight")]
        public void DeleteWeight()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            var userData = AppDbContext.User.GetUserData(email);
            #endregion

            #region Add Weight daily
            int countOfRecods = 5;
            var conversionSystem = ConversionSystem.METRIC;
            for (int i = 0; i < countOfRecods; i++)
            {
                WeightTracker.AddWeight(responseLoginUser, RandomHelper.RandomProgressData(ProgressBodyPart.WEIGHT), RandomHelper.RandomDateInThePast(), conversionSystem, false);
            }
            
            
            WeightTracker.VerifyAddedWeight(responseLoginUser, conversionSystem, userData.Id);

            #endregion

            #region Delete Weight daily
            var weightList = WeightTracker.GetWeightList(responseLoginUser, conversionSystem, 0, countOfRecods);
            foreach (var weight in weightList)
            {
                WeightTracker.DeleteWeight(responseLoginUser, weight.id);
            }
            WeightTracker.VerifyIsDeletedProgressDaily(responseLoginUser, userData.Id);

            #endregion
        }

        [Test, Category("Daily Weight")]
        public void GetDailyProgressByEmail()
        {
            #region Register New User

            var userData = AppDbContext.User.GetUserData("qatester2023-03-6-09-38-42@xitroo.com");
            var responseLoginUser = SignInRequest.MakeSignIn(userData.Email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);

            #endregion

            #region Add Weight daily
            const int recordCount = 60;
            const int conversionSystem = ConversionSystem.METRIC;

            Enumerable.Range(0, recordCount)
          .ToList()
          .ForEach(_ =>
          {
              var weight = RandomHelper.RandomProgressData(ProgressBodyPart.WEIGHT);
              var date = RandomHelper.RandomDateInThePast();
              WeightTracker.AddWeight(responseLoginUser, weight, date, conversionSystem, false);
          });
            WeightTracker.VerifyAddedWeight(responseLoginUser, conversionSystem, userData.Id);
            WeightTracker.VerifyAverageOfWeightTracking(responseLoginUser, conversionSystem, userData.Id);
            WeightTracker.VerifyChangedWeekWeight(responseLoginUser, conversionSystem, userData.Id);

            #endregion


        }

        [Test, Category("Daily Weight")]
        public void VerifyChangedWeekWeightByEmail()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
            string userId = AppDbContext.User.GetUserData(email).Id;
            #endregion

            #region Add Weight daily
            const int recordCount = 30;
            const int conversionSystem = ConversionSystem.METRIC;

            Enumerable.Range(0, recordCount)
          .ToList()
          .ForEach(_ =>
          {
              var weight = RandomHelper.RandomProgressData(ProgressBodyPart.WEIGHT);
              var date = RandomHelper.RandomDateInThePast();
              WeightTracker.AddWeight(responseLoginUser, weight, date, conversionSystem, false);
          });

            #endregion

            #region Verify Changed Week Weight daily

            WeightTracker.VerifyChangedWeekWeight(responseLoginUser, conversionSystem, userId);

            #endregion
        }
    }

    [TestFixture]
    public class Memberships
    {
        [Test, Category("Memberships")]
        public void CompleteSuballMemberships()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            var user = AppDbContext.User.GetUserData(email);
            #endregion

            #region Add and Activate membership to User

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var listOfMemberships = AppDbContext.Memberships.GetAllMemberships()
                .Where(x => x.SKU != null && !x.SKU.StartsWith("CH")
                                          && !x.Name.Contains("test", StringComparison.OrdinalIgnoreCase)
                                          && !x.Name.Contains("jenna", StringComparison.OrdinalIgnoreCase)
                                          && !x.Name.Contains("lorem", StringComparison.OrdinalIgnoreCase)
                                          && !x.Name.Contains("phoenix", StringComparison.OrdinalIgnoreCase))
                .ToList();
            MembershipRequest.CreateSubAllMembership(responseLoginAdmin, MembershipsSKU.SKU_SUBALL_MEMBER, listOfMemberships, 6);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, user.Id);

            #endregion

            #region Steps to Complete memberships

            AppDbContext.UserMemberships.GetAllUsermembershipByUserId(user, out List<DB.UserMemberships> userMemberships);
            foreach (var userMember in userMemberships)
            {
                MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMember.Id, user.Id);
                ClientMembershipRequest.GetActiveMembershipForAllPhases(responseLoginUser);
            }

            #endregion
        }

        [Test, Category("Memberships")]
        public void CompleteProductMembership()
        {
            #region Register New User
            string email = RandomHelper.RandomEmail();
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            var user = AppDbContext.User.GetUserData(email);
            #endregion

            #region Add and Activate membership to User

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            const int programCount = 3;
            MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out List<DB.Programs> programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, user.Id);
            AppDbContext.UserMemberships.GetLastUsermembershipByUserId(user, out DB.UserMemberships userMembership);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembership.Id, user.Id);

            #endregion

            #region UserSteps

            ClientMembershipRequest.GetActiveMembershipForAllPhases(responseLoginUser);

            #endregion
        }
    }

        [TestFixture]
    public class Demo 
    { 

        [Test]

        public void DemoS()
        {
            #region Register New User
            string email = "qatester91311@gmail.com";
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            var user = AppDbContext.User.GetUserData(email);
            #endregion

            #region Add and Activate membership to User

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var listOfMemberships = AppDbContext.Memberships.GetAllMemberships()
                .Where(x => x.SKU != null && !x.SKU.StartsWith("CH") 
                    && !x.Name.Contains("test", StringComparison.OrdinalIgnoreCase) 
                    && !x.Name.Contains("jenna", StringComparison.OrdinalIgnoreCase) 
                    && !x.Name.Contains("lorem", StringComparison.OrdinalIgnoreCase)
                    && !x.Name.Contains("phoenix", StringComparison.OrdinalIgnoreCase)
                    && !x.Name.Contains("Building The Bikini Body Home Part 1", StringComparison.OrdinalIgnoreCase))
                .ToList();
            MembershipRequest.CreateSubAllMembership(responseLoginAdmin, MembershipsSKU.SKU_SUBALL_MEMBER, listOfMemberships, 6);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, user.Id);
            //MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            //const int programCount = 3;
            //MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out List<DB.Programs> programs);
            //MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
            //MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);
            //AppDbContext.Memberships.GetLastMembership(out membershipData);
            //MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, user.Id);
            AppDbContext.UserMemberships.GetAllUsermembershipByUserId(user, out List<DB.UserMemberships> userMemberships);
            foreach ( var userMember in userMemberships)
            {
                MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMember.Id, user.Id);
                ClientMembershipRequest.GetActiveMembershipForAllPhases(responseLoginUser);
            }

            #endregion

            #region UserSteps

            //ClientMembershipRequest.GetUserMemberships(responseLoginUser, out List<MembershipModel.GetMembership> getUserMemberships);
            //ClientMembershipRequest.GetActiveMembershipForRandomPhase(responseLoginUser, out MembershipModel.GetActiveMembership getActiveMembership);
            //ClientMembershipRequest.GetWorkoutsListForFirstWeek(responseLoginUser, getActiveMembership, -14, out List<MembershipModel.GetListByProgramWeekResponse> getWorkoutsListByProgramWeek);
            //ClientMembershipRequest.GetUserWorkoutExercisesForFirstWeek(responseLoginUser, getActiveMembership, getWorkoutsListByProgramWeek, out MembershipModel.GetUserWorkoutExercisesResponse getUserWorkoutExercises);
            //ClientMembershipRequest.SaveCompletedWorkoutForFirstWeek(responseLoginUser, getActiveMembership, getWorkoutsListByProgramWeek, getUserWorkoutExercises);

            //ClientMembershipRequest.GetActiveMembershipForAllPhases(responseLoginUser);

            #endregion
        }
        public List<UserMember> DemoTest()
        {
            var listUsers = new List<UserMember>();
            DateTime start = DateTime.Now.AddDays(-2).Date;
            DateTime end = DateTime.Now;
            var listUserMem = AppDbContext.UserMemberships.GetAllUsermembershipInRange(start, end);
            for (int i = 0; i < 1; i++)
            {
                foreach (var item in listUserMem)
                {
                    var userMemberList = AppDbContext.UserMemberships.GetAllUsermembershipByUserId(item, start, end);
                    var listJsonExercises = AppDbContext.UserMemberships.GetUnicUserIdFromUserMembershipsInRange(item, start, end);
                    try
                    {
                        var check = userMemberList.SequenceEqual(listJsonExercises, new CustomClassEqualityComparer());
                    }
                    catch(Exception ex)
                    {
                        var row = new UserMember()
                        {
                            UserId = item.UserId,
                            UsermembershipId = item.Id,
                            MembershipId = item.MembershipId,
                            Error = ex.Message
                        };

                        listUsers.Add(row);
                    }

                    

                }
            }
            foreach (var listUser in listUsers)
            {
                Console.WriteLine("UserId: {0}\nUsermembershipId: {1}\nMembershipId: {2}\n\n\n", listUser.UserId, listUser.UsermembershipId, listUser.MembershipId);
            }
            return listUsers;
        }

        public class UserMember
        {
            public string UserId { get; set; }
            public int? UsermembershipId { get; set; }
            public int? MembershipId { get; set; }
            public string? Error { get; set; }
        }

        class CustomClassEqualityComparer : IEqualityComparer<DB.JsonUserExOneField>
        {
            public bool Equals(DB.JsonUserExOneField x, DB.JsonUserExOneField y)
            {
                //return x.UserMembershipId == y.UserMembershipId;
                if (x == null || y == null)
                    return false;

                if (x.UserMembershipId != y.UserMembershipId)
                    throw new Exception($"CustomClass values do not match: {x.UserMembershipId} != {y.UserMembershipId}");

                return true;
            }

            public int GetHashCode(DB.JsonUserExOneField obj)
            {
                return obj.UserMembershipId.GetHashCode();
            }
        }

    }
}
