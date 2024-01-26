using AngleSharp.Dom;
using CsvHelper;
using MCMAutomation.APIHelpers;
using MCMAutomation.APIHelpers.Admin.PagesEducationPage;
using MCMAutomation.APIHelpers.Admin.VideosPage;
using MCMAutomation.APIHelpers.Client.AddProgress;
using MCMAutomation.APIHelpers.Client.EditUser;
using MCMAutomation.APIHelpers.Client.Membership;
using MCMAutomation.APIHelpers.Client.SignUp;
using MCMAutomation.APIHelpers.Client.WeightTracker;
using MCMAutomation.APIHelpers.SignInPage;
using MCMAutomation.Helpers;
using NUnit.Framework;
using RimuTec.Faker;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using Telegram.Bot.Types;
using static MCMAutomation.APIHelpers.Admin.VideosPage.Videos;

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
            #region Create Membership

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            const int programCount = 6;
            MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out List<DB.Programs> programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);

            #endregion

            #region Register New User
            //string email = RandomHelper.RandomEmail();
            //SignUpRequest.RegisterNewUser(email);
            //var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            //EditUserRequest.EditUser(responseLoginUser);
            string userId = AppDbContext.User.GetUserData("annfall1111@gmail.com").Id;
            #endregion

            #region Add and Activate membership to User
            
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, userId);
            int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId("annfall1111@gmail.com");
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            #endregion
        }

        [Test, Category("Memberships")]
        public void AddMembershipToOneThousandUsers()
        {
            #region Create Membership

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            const int programCount = 3;
            MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out List<DB.Programs> programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);

            #endregion

            for (int i = 0; i < 1000; i++)
            {
                #region Register New User

                string email = RandomHelper.RandomEmail();
                SignUpRequest.RegisterNewUser(email);
                var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
                EditUserRequest.EditUser(responseLoginUser);
                string userId = AppDbContext.User.GetUserData(email).Id;

                #endregion

                #region Add and Activate membership to User

                MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, userId);
                int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
                MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

                #endregion
            }

        }

        [Test, Category("Memberships")]
        public void CreateProductMembership()
        {
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membership);
            var exercises = AppDbContext.Exercises.GetExercisesData();
            int programCount = 5;
            MembershipRequest.CreatePrograms(responseLoginAdmin, membership.Id, programCount, out List<DB.Programs> programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
            MembershipRequest.AddExercisesToMembership(responseLoginAdmin, workouts, exercises);
                

            
        }

        [Test, Category("Memberships")]
        public void CreateSubAllMembership()
        {
            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var listOfMemberships = AppDbContext.Memberships.GetAllMemberships().Where(x=> x.SKU != null && x.AccessWeekLength != 0).ToList();
            CreateProductMembership();
            MembershipRequest.CreateSubAllMembership(responseLoginAdmin, MembershipsSKU.SKU_SUBALL_MEMBER, listOfMemberships, 5);

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
        [Repeat(100)]
        public void RegisterUser()
        {
            #region Register New User
            string email = "qatester" + DateTime.Now.ToString("dd-MM-dd-hh-mm-ss-fffff")+ "@putsbox.com";
            SignUpRequest.RegisterNewUser(email);
            //var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            //EditUserRequest.EditUser(responseLoginUser, 15, UserAccount.MALE);
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
            string email = "qatester91311@gmail.com";
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            var user = AppDbContext.User.GetUserData(email);
            #endregion

            #region Add and Activate membership to User

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            var listOfMemberships = AppDbContext.Memberships.GetAllMemberships()
                                                                .Where(x => x.SKU != null
                                                                    && !x.SKU.StartsWith("CH")
                                                                    && !x.Name.Contains("test", StringComparison.OrdinalIgnoreCase)
                                                                    && !x.Name.Contains("jenna", StringComparison.OrdinalIgnoreCase)
                                                                    && !x.Name.Contains("lorem", StringComparison.OrdinalIgnoreCase)
                                                                    && !x.Name.Contains("phoenix", StringComparison.OrdinalIgnoreCase)
                                                                    && !x.Name.Contains("Building The Bikini Body Free Trial", StringComparison.OrdinalIgnoreCase)
                                                                    && x.StartDate == DateTime.Parse("1/1/0001 12:00:00 AM"))
                                                                .ToList();
            MembershipRequest.CreateSubAllMembership(responseLoginAdmin, MembershipsSKU.SKU_SUBALL_MEMBER, listOfMemberships, 3);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, user.Id);

            #endregion

            #region Steps to Complete memberships

            AppDbContext.UserMemberships.GetAllUsermembershipByUserId(user, out List<DB.UserMemberships> userMemberships);
            //foreach (var userMember in userMemberships)
            //{
            //    MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMember.Id, user.Id);
            //    ClientMembershipRequest.GetActiveMembershipForAllPhases(responseLoginUser, -15);
            //}

            #endregion
        }

        [Test, Category("Memberships")]
        public void CompleteProductMembership()
        {
            #region Register New User
            string email = "annfall1111@gmail.com";
            SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            var user = AppDbContext.User.GetUserData(email);
            #endregion

            #region Add and Activate membership to User

            var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            const int programCount = 4;
            MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out List<DB.Programs> programs);
            MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
            MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);
            MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, user.Id);
            AppDbContext.UserMemberships.GetLastUsermembershipByUserId(user, out DB.UserMemberships userMembership);
            MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembership.Id, user.Id);

            #endregion

            #region UserSteps

            ClientMembershipRequest.GetActiveMembershipForAllPhases(responseLoginUser, -72);

            #endregion
        }
    }

        [TestFixture]
    public class Demo 
    { 

        [Test]

        public void DemoS()
        {
            //#region Create Membership

            //var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            //MembershipRequest.CreateProductMembership(responseLoginAdmin, MembershipsSKU.SKU_PRODUCT);
            //AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipData);
            //const int programCount = 5;
            //MembershipRequest.CreatePrograms(responseLoginAdmin, membershipData.Id, programCount, out List<DB.Programs> programs);
            //MembershipRequest.CreateWorkouts(responseLoginAdmin, programs, programCount, out List<DB.Workouts> workouts);
            //MembershipRequest.AddExercisesToWorkouts(responseLoginAdmin, workouts);

            //#endregion
            //for(int i = 0; i < 1000; i++)
            //{
            //    #region Register New User
            //    string email = RandomHelper.RandomEmail();
            //    SignUpRequest.RegisterNewUser(email);
            //    var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            //    EditUserRequest.EditUser(responseLoginUser);
            //    string userId = AppDbContext.User.GetUserData(email).Id;
            //    #endregion

            //    #region Add and Activate membership to User

            //    MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipData.Id, userId);
            //    int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            //    MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);

            //    #endregion
            //}

            List<string> newApp = new List<string>()
            {
                "2 eggs & apple",
                "Acai Bowl with Blueberries",
                "Almond Butter & Berry Yogurt Bowl",
                "Almond Butter Rice Cakes",
                "Almond Butter Yogurt Snack Bowl",
                "Almond Vanilla Smoothie",
                "Almond Yogurt with Chia and Oats",
                "Almonds With Eggs",
                "apple (182g)",
                "Apple & Almonds",
                "Apple & Coconut Bircher Muesli",
                "Apple & Creamy Peanut Butter Dip",
                "Apple & Ham Wrap",
                "Apple & Peanut Butter",
                "Apple and Peanut Butter",
                "Apple Cinnamon Smoothie",
                "Apple Crumble Overnight Oats",
                "Apple Crumble Yoghurt Bowl",
                "Apple Kiwi Smoothie",
                "Apple Pancakes",
                "Apple Pie Baked Oats",
                "Apple Pie Smoothie",
                "Apple Pie Snack",
                "Apple Pie Toast",
                "Apple Pie Yogurt Bowl",
                "Asparagus Avocado Fettuccine",
                "Asparagus Feta Salad",
                "Avocado & Tomato Rice Cakes",
                "Avocado Arugula Omelette",
                "Avocado Blueberry Smoothie",
                "Avocado Toast",
                "Bacon & Egg Fried Rice",
                "Bacon & Egg Muffin",
                "Bacon & Egg Muffin with Hash Brown",
                "Bacon, Egg & Avocado Bagel",
                "Baked Avocado with Eggs",
                "Baked Bean & Cheese Toastie",
                "Baked Bean Jacket Potato",
                "Baked Chicken And Broccoli",
                "Baked Chicken And Sweet Potato With Broccoli",
                "Baked Chicken With Brown Rice and Broccoli",
                "Baked Feta Risotto",
                "Baked Pesto Chicken",
                "Baked Salmon with Broccoli",
                "banana (118g)",
                "Banana & Cacao Smoothie Bowl",
                "Banana & Dark Chocolate",
                "Banana Bowl With Yogurt",
                "Banana Bread Baked Oats",
                "Banana Chocolate Milkshake",
                "Banana Chocolate Smoothie",
                "Banana Oatmeal Smoothie",
                "Banana PB Toast & Protein Shake",
                "Banana Protein Oats",
                "Banana Protein Oats (lower calorie)",
                "Banana Protein Smoothie",
                "Banana Split",
                "Bangers & Mash",
                "BBQ Chicken Coleslaw Roll",
                "Bean and Lentil Salad with Pecans",
                "Bean Nachos",
                "Beef Bolognese",
                "Beef Bulgogi",
                "Beef Burrito",
                "Beef Burrito Bowl",
                "Beef Burrito with Vegetables",
                "Beef Chili",
                "Beef Enchiladas",
                "Beef Fajita Rice Bowl",
                "Beef Gyros",
                "Beef Lasagne",
                "Beef Mince Wraps",
                "Beef Picadillo",
                "Beef Pita Wraps with Veggies",
                "Beef Ravioli",
                "Beef Ravioli (Lower Calorie)",
                "Beef Rissoles With Mash",
                "Beef Shepherd's Pie",
                "Beef Stir Fry",
                "Beef Stuffed Peppers",
                "Beef Taco Bowl",
                "Beef Tacos",
                "Berries & Yogurt",
                "Berry And Yoghurt Protein Smoothie",
                "Berry Nut Butter Yogurt Snack",
                "Big English Breakfast",
                "Big Mac Burger",
                "Biscoff & Blueberry Protein Mug Cake",
                "Biscoff & Dark Choc Oats",
                "Biscoff & Strawberry Ice-Cream Pop",
                "Biscoff Chessecake",
                "Biscoff Cottage Cheese Ice-Cream",
                "Biscoff French Toast",
                "Biscoff Glazed Overnight Oats",
                "Biscoff Ice-Cream Waffle Cone",
                "Biscoff Lava Cake",
                "Biscoff Overnight Weetbix",
                "Biscoff Protein Oats",
                "Biscoff Ricotta Crumpets",
                "Biscoff Scrambled Pancakes",
                "Biscoff Strawberry Rolls",
                "Black Bean Burrito Bowl",
                "Black Bean Stir Fry",
                "Black Bean Taco Bowl",
                "Blessed Bar Waffle Stack",
                "Blessed Bar Yogurt Bowl",
                "Blessed Protein Bar (Salted Caramel)",
                "Blessed Protein Bar & Apple",
                "Blessed Protein Bar, Popcorn & Apple",
                "BLT Pasta Salad",
                "Blueberry and Oats Protein Parfait",
                "Blueberry Banana Smoothie",
                "Blueberry Cottage Cheese Pancakes",
                "Blueberry Loaded Oatmeal",
                "Blueberry Milkshake",
                "Blueberry Parfait",
                "Blueberry Vegan Pancakes",
                "Boiled Eggs with Herb Sauce",
                "Boiled Eggs With Roasted Chickpeas And Arugula",
                "Breakfast Cookie",
                "Breakfast Hashbrown Toast",
                "Breakfast Pizza",
                "Breakfast Potato Bean Salad",
                "Breakfast Pumpkin Waffles",
                "Breakfast Sandwich",
                "Breakfast Tacos",
                "Breakfast Wrap",
                "Broccoli & Chicken Stir-Fry",
                "Brown Rice Buddha Bowl",
                "Brown Rice Mushroom Risotto",
                "Bruschetta with Eggs And Tomatoes",
                "Buckwheat Pancakes",
                "Buddha Bowl with Spinach, Avocado, and Radishes",
                "Buddha Bowl With Turkey And Avocado",
                "Bueno Overnight Oats",
                "Buffalo Chicken Drumsticks",
                "Bunless Beef Burger & Parmesan Chips",
                "Butter Chicken & Naan",
                "Butternut Squash & Feta Chicken Salad",
                "Cajun Beef and Veggie Rice",
                "Cajun Chicken",
                "Cajun Chicken Couscous Salad",
                "Cajun Chicken Rice Bowl",
                "Caprese Chicken Salad",
                "Caprese Toast",
                "Caprese Tuna Salad",
                "Caramel Banana Smoothie Bowl",
                "Carrot Cake Muffin",
                "Carrots & Almonds",
                "Carrots & Turkey",
                "Cheese & Olive Melt",
                "Cheese & Tomato Rice Cakes",
                "Cheese And Tomato Pita Breads",
                "Cheese Omelette With Vegetables",
                "Cheeseburger Fries",
                "Cheeseburger Salad",
                "Cheesy Eggs With Veg",
                "Cheesy Pasta Bake",
                "Chia Overnight Oats with Cranberries",
                "Chia Seeds Pudding",
                "Chicken & Feta Baked Potato",
                "Chicken & Spinach Gnocchi",
                "Chicken & Sriracha Mayo Rice Bowl",
                "Chicken & Veg Pasta",
                "Chicken and Broccoli",
                "Chicken and Egg Salad",
                "Chicken And Quinoa Salad",
                "Chicken Asparagus Avocado Salad",
                "Chicken Breast in Tomato Sauce",
                "Chicken Burger & Chips",
                "Chicken Burrito",
                "Chicken Burrito Bowl",
                "Chicken Caesar Salad",
                "Chicken Carbonara",
                "Chicken Cottage Cheese Frittata",
                "Chicken Enchiladas",
                "Chicken Fajita Enchiladas",
                "Chicken Fajita Rice Bowl",
                "Chicken Fajitas",
                "Chicken Farfalle",
                "Chicken Fingers",
                "Chicken Mushroom Risotto",
                "Chicken Nachos",
                "Chicken Napolitana Pasta",
                "Chicken Pad Thai",
                "Chicken Pearl Couscous Salad",
                "Chicken Pesto Pasta",
                "Chicken Pesto Pasta Salad",
                "Chicken Pesto Pizza",
                "Chicken Rice Noodle Bowl",
                "Chicken Sandwiches With Chilli",
                "Chicken Schnitzel Salad",
                "Chicken Schnitzel Wrap",
                "Chicken Shawarma",
                "Chicken Tenders and Fries",
                "Chicken Veggie Sweet Potato Bowl",
                "Chicken Vermicelli Noodles & Spicy Mayo",
                "Chicken, Broccoli & Brown Rice",
                "Chicken, Broccoli & Sweet Potato",
                "Chicken, Mash & Broccoli",
                "Chickpea & Sweet Potato Wrap",
                "Chickpea Curry",
                "Chickpea Gyros",
                "Chickpea Snack",
                "Chickpea Stuffed Peppers",
                "Chili Con Carne",
                "Chili Ground Beef",
                "Chili T-Bone Steak",
                "Chinese Chicken Salad",
                "Chipotle Beef & Cauliflower Rice",
                "Chipotle Chicken & Cauliflower Rice",
                "Chipotle Chicken Nachos",
                "Chipotle Chicken Quesadilla",
                "Choc Banana Protein Oats",
                "Choc Caramel Smoothie Bowl",
                "Choc Chip Baked Oats",
                "Choc Chip Cookie Dough Balls",
                "Choc Chip Oatmeal Cookie",
                "Choc Hazelnut Protein Ice-Cream",
                "Choc Milkshake With Bananas",
                "Choc Oreo Donuts",
                "Choc PB Rice Cakes",
                "Choc Peanut Butter Mug Cake",
                "Choc Peanut Butter Yogurt Bark",
                "Choc Peanut-Butter Oats",
                "Choc Protein Ice Cream",
                "Choc Protein Milkshake",
                "Choc Protein Popcorn",
                "Choc-Banana Bread Mug Cake",
                "Choc-Hazelnut Protein Shake",
                "Choco Banana Smoothie",
                "Chocolate Chip Pancakes",
                "Chocolate Milkshake",
                "Chocolate Milkshake With Hazelnuts",
                "Chocolate Protein Mug Cake",
                "Chorizo & Veggie Pizza",
                "Chorizo Mushroom Pasta",
                "Cinnamon & Blueberry Yogurt",
                "Cinnamon Mocha Protein Shake",
                "Cinnamon Oats with Pecans",
                "Cinnamon Porridge With Blueberries",
                "Coconut Oatmeal",
                "Cookie Dough Oatmeal",
                "Cookies & Cream Overnight Weetbix",
                "Cookies & Cream Yogurt",
                "Corn Chips & Guac",
                "Corn Chips with Guacamole",
                "Cottage Cheese & Bacon Alfredo",
                "Cottage Cheese Scramble Tortilla",
                "Cottage Cheese with Cucumber & Carrot Sticks",
                "Coyo Breakfast Bowl",
                "Cream Cheese & Tomato Rice Cakes",
                "Creamy Avo And Egg Sandwich",
                "Creamy Cajun Chicken Pasta",
                "Creamy Cajun Prawn Pasta",
                "Creamy Cajun Salmon Pasta",
                "Creamy Chicken Gnocchi",
                "Creamy Potato & Salmon Tray Bake",
                "Creamy Satay Chicken and Rice",
                "Creamy Tofu Veg Sandwich",
                "Creamy Tuna Salad & Toast",
                "Creamy Tuna Salad on Toast",
                "Creamy Tuscan Chicken Pasta",
                "Creamy Veg Chicken Pasta",
                "Crispy Fish Tacos",
                "Crispy Tofu Cauliflower Rice",
                "Crispy Tofu with Cauliflower Rice",
                "Crumbed Fish Burger",
                "Crumpet with Berries & Ricotta",
                "Crunchy Chickpeas",
                "Crunchy Nut Smoothie",
                "Cucumber & Turkey",
                "Cucumber Almond And Cheese Snack",
                "Cucumber Salmon Sushi",
                "Cucumber, Almonds & Mozzarella",
                "Cucumbers With Eggs",
                "Custard Strawberry Toast",
                "Dark Choc Overnight Oats",
                "Dark Chocolate",
                "Deconstructed Pesto Pasta",
                "Double Beef Burger & Chips",
                "Easy Banana Pancakes",
                "Easy Protein Balls",
                "Egg & Almonds",
                "Egg & Carrots",
                "Egg & Cucumber",
                "Egg Bruschetta with Tomatoes",
                "Egg Salad",
                "Egg Salad Sandwich",
                "Egg Salad With Tuna And Veg",
                "Egg White & Turkey Bacon Wrap",
                "Egg White Scramble On English Muffin",
                "Egg White Scramble On English Muffins",
                "Egg White, Spinach & Feta Wrap",
                "Egg, Avo & Turkey Bacon Muffin",
                "Eggs On Toast With Veggies",
                "Eggs with Hummus & Toast",
                "Eggs With Rice And Greens",
                "Eggs, Beans & Vegetables",
                "Eggs, Broccoli & Chickpeas",
                "Eggs, Brown Rice & Broccoli",
                "English Muffin with Ricotta and Tomato",
                "Fajita Beef Rice Bowl",
                "Fajita Chicken Rice Bowl",
                "Falafel Pita",
                "Feta Scrambled Eggs On Toast",
                "Fish n' Chips",
                "Fish With Greens And Rice",
                "Fish, Rice & Mango Salsa",
                "Flatbread Lamb Kebab",
                "Freezer Cookie",
                "Fried Egg & Avo Toast",
                "Fried Egg and Mushroom Sandwich",
                "Fried Egg Avo Sandwich",
                "Fried Egg, Bacon & Avo Toast",
                "Fried Eggs with Asparagus and Tomatoes",
                "Fried Eggs With Toast And Vegetables",
                "Fried Noodle Chicken Salad",
                "Fried Salmon With Spinach",
                "Fried Tofu with Green Beans",
                "Fried Vegetable Sandwich",
                "Frozen Grapes",
                "Fruit & Nut Yogurt Bowl",
                "Fruity Vanilla Oats",
                "Gnocchi Sausage Bake",
                "Granola Smoothie Bowl",
                "Grapes, Cheese & Crakers",
                "Greek Chicken Pita Salad",
                "Greek Chicken Rice Bowl",
                "Greek Chicken Salad",
                "Greek Chicken Wedges",
                "Greek Chickpea Pasta Salad",
                "Greek Inspired Lamb Salad",
                "Greek Style Lamb Pasta",
                "Greek Yogurt & Banana",
                "Greek Yogurt & Peanut Butter",
                "Greek Yogurt with Strawberries",
                "Greek-Inspired Pasta Salad",
                "Green Goddess Chicken Salad",
                "Green Pesto Buddha Bowl",
                "Grilled Halloumi and Lentil Salad",
                "Grilled Halloumi Sandwich",
                "Grilled Pineapple With Yoghurt And Berries",
                "Grilled Salmon With Quinoa",
                "Grilled Salmon with Vegetables and Rice",
                "Grilled Steak Mexican Salad",
                "Grilled Tofu Steak with Cucumber and Rice",
                "Ground Beef & Green Beans",
                "Ground Beef Wraps",
                "Ham & Cheese Toasted Sandwich",
                "Ham & Egg Muffin With Protein Shake",
                "Ham & Pineapple Pizza Muffins",
                "Ham Pizza",
                "Ham Salad Bagel",
                "Ham Salad Bagel & Protein Shake",
                "Ham Salad Sandwich",
                "Ham, Cheese & Cucumber Roll Ups",
                "Ham, Cheese & Egg Muffin",
                "Hard Boiled Eggs",
                "Hawaiian Chicken Parmesan",
                "Hawaiian Pizza Pocket",
                "Hawaiian Pizza Toast",
                "Hawaiian Shrimp Poke Bowl",
                "Herby Scrambled Eggs With Veggies",
                "Hoisin Chicken Rice Paper Rolls",
                "Honey Garlic Prawns",
                "Honey Prawn & Halloumi Stir-Fry",
                "Honey Soy Chicken",
                "Honey Soy Chicken Noodle Stir-Fry",
                "Honey Soy Chicken Stir-Fry",
                "Honey Soy Salmon Rice Bowl",
                "Honey Soy Tofu Stir Fry",
                "Honey Soy Tofu Stir Fry (lower calorie)",
                "Honey Sriracha Egg & Avo Toast",
                "Honey Sriracha Eggs & Avo Toast",
                "Honey Sriracha Eggs on Toast",
                "Hotcakes & Bacon",
                "Hummus Buddha Bowl",
                "Hummus Rice Cakes",
                "Hummus, Crackers and Veggie Sticks",
                "Jalapeno Poppers",
                "Jam & Ricotta English Muffin",
                "Kidney Bean Risotto",
                "Kiwi, Orange & Berry Fruit Salad",
                "Lamb Shepherd's Pie",
                "Lemon Shrimp Salad",
                "Lentil Pasta Salad",
                "Lentil Pasta With Mushrooms",
                "Lentil Quinoa Salad",
                "Lentil Tacos",
                "Lettuce Cups With Tempeh",
                "Loaded Smores Smoothie",
                "Low Carb Protein Waffles",
                "Macadamias & Dark Chocolate",
                "Margherita Toastie",
                "Mars Bar Weetbix Cheesecake",
                "Marshmallow Trail Mix",
                "Massaman Curry",
                "Mayo & Sweetcorn Tuna Pasta Salad",
                "Meatball Sub",
                "Meatballs, Mash & Broccoli",
                "Mexi Beef and Veg",
                "Mexi Fish Rice Bowl",
                "Mexican Avocado Toast",
                "Mexican Beef Loaded Fries",
                "Mexican Chicken Pasta Salad",
                "Mexican Loaded Wedges",
                "Mexican Picadillo",
                "Mexican Tacos",
                "Mexican tofu, avocado and broccoli",
                "Mini Banana Pancake Bites",
                "Mini Biscoff Cheesecake",
                "Mini Nutella Cheesecake",
                "Mini Sweet Chilli Chicken Pizzas",
                "Miso Chicken Rice Bowl",
                "Miso Tofu Poke Bowl",
                "Mixed Berry PB Overnight Oats",
                "Mixed Berry Protein Shake",
                "Mixed Berry Protein Smoothie",
                "Mocha Protein Shake",
                "Moroccan Soup with Chickpeas",
                "Mozzarella Stuffed Chicken Breast",
                "Mushroom & Spinach Breakfast Burrito",
                "Mushroom Omelette",
                "Naked Chicken Burrito Bowl",
                "No Bake Protein Balls",
                "Nut Mix & Yogurt",
                "Nutella and Banana Bagel",
                "Nutella Crumpets",
                "Nutella Donuts",
                "Nutella Fondant & Ice-Cream",
                "Nutella French Toast",
                "Nutella Overnight Weetbix",
                "Nutella Protein Crepes",
                "Nutella Protein Mug Cake",
                "Nutella Protein Oats",
                "Nutella Ricotta Crumpets",
                "Oatmeal Berry Porridge",
                "Oatmeal Choc Chip Cookie",
                "Oats With Almond Yoghurt",
                "Oats with Almond Yogurt and Raspberries",
                "Omelette Salad",
                "Omelette With Tomatoes And Feta",
                "One-Pan Gnocchi Bolognese",
                "One-Pan Pesto Chicken",
                "One-Pot Beef & Sweet Potato Chili",
                "Open Tuna Sandwich",
                "Oreo Cottage Cheese Ice-Cream",
                "Oreo Protein Crepes",
                "Oreo Protein Ice-Cream",
                "Oreo Strawberry Cheesecake",
                "Oven Baked Risotto With Chorizo And Vegetables",
                "Overnight PB And Raspberry Oats",
                "OxyWhey or Blessed Protein Shake",
                "Pad See Ew",
                "Panzanella",
                "Parmesan Chicken Schnitzel & Salad",
                "Pasta with Vegetables",
                "PB & Banana with Crushed Peanuts",
                "PB Banana Rice Cakes & Protein Shake",
                "PB banana toast",
                "PB Banana Toast & Protein Shake",
                "PB Berry Overnight Oats",
                "PB Sandwich With Apple",
                "PB, Banana & Honey Rice Cakes",
                "Peach Vanilla Smoothie Bowl",
                "Peanut Butter & Banana Yogurt Bowl",
                "Peanut Butter & Celery",
                "Peanut Butter & Jelly Oats",
                "Peanut Butter Acai Bowl",
                "Peanut Butter Apple Sandwich",
                "Peanut Butter Banana Smoothie",
                "Peanut Butter Berry Protein Oats",
                "Peanut Butter Ice-Cream",
                "Peanut Butter Popcorn",
                "Peanut Butter Toast",
                "Pepperoni Cheesy Crust Pizza",
                "Pepperoni Pizza",
                "Pepperoni Rice Cakes",
                "Peri Peri Chicken Pasta",
                "Peri Peri Chicken Rice",
                "Peri Peri Pizza",
                "Pesto & Tomato Rice Cakes",
                "Pesto Avocado Toast",
                "Pesto Cheese Toastie",
                "Pesto Eggplant Salad",
                "Pesto Eggs on Avo Toast",
                "Pesto Fried Eggs",
                "Pesto Pasta with Chicken",
                "Pesto Prawn Pizza",
                "Pesto Salmon Tray Bake",
                "Pesto Tuna Rice Cakes",
                "Pina Colada Smoothie",
                "Pineapple Parfait",
                "Pita Bread with Spinach and Mozzarella",
                "Pita With Falafel",
                "Pizza Bagel",
                "Plant Based Pancakes",
                "Poached Egg And Avocado Sandwich",
                "Poached Egg Salad",
                "Poached Egg Toast with Veggies",
                "Poached Egg with Leeks",
                "Popcorn & Apple",
                "Popcorn and Almonds",
                "Popcorn Trail Mix",
                "Pork Dumpling Stir-Fry",
                "Potato Carrot Patties",
                "Potato Mushroom Stir-Fry",
                "Prawn & Chorizo Paella",
                "Prawn And Legume Spicy Bowl",
                "Prawn Salad with Greens",
                "Prawns, Brown Rice & Sweet Potato",
                "Prawns, Chicken & Broccoli",
                "Protein Banana Smoothie",
                "Protein Berry Pancakes",
                "Protein Chia Pudding With Berries",
                "Protein Crumpets",
                "Protein Iced Coffee",
                "Protein Yogurt Bowl",
                "Pulled Pork Rice Bowl",
                "Pulled Pork Wrap",
                "Pumpkin Oatmeal with Apples",
                "Pumpkin Pie Smoothie Bowl",
                "Quick Chicken Salad",
                "Quick Chicken Stir Fry",
                "Quick Sweet Chili Chicken Noodles",
                "Quick Teriyaki Chicken",
                "Quinoa and Chickpea Salad",
                "Quinoa and Corn Burritos",
                "Quinoa and Kidney Beans Buddha Bowl",
                "Quinoa BBQ Chicken and Veg",
                "Quinoa Broccoli with Tuna",
                "Raspberry Almond Smoothie Bowl",
                "Raspberry Ripple Protein Oats",
                "Raspberry Smoothie",
                "Raspberry White Choc Yogurt Bowl",
                "Recovery Boosting Strawberry Slushie",
                "Rice Cake Salmon Caprese",
                "Rice Cakes Two Ways",
                "Rice Paper Rolls With Tofu",
                "Risotto With Chicken And Mushrooms",
                "Roasted Beef Sandwich",
                "Roasted Butternut Squash",
                "Rocky Road Yogurt Snack",
                "Rocky Road Yogurt Treat",
                "S'mores Smoothie Bowl",
                "Salad Bagel & Protein Shake",
                "Salmon & Broccolini",
                "Salmon & Cauliflower Rice",
                "Salmon & Feta Quinoa Salad",
                "Salmon & Roast Veg Quinoa Salad",
                "Salmon And Arugula Salad",
                "Salmon And Cream Cheese Bagel",
                "Salmon Avocado Bruschetta",
                "Salmon Bites with Sriracha",
                "Salmon Bruschetta With Avo",
                "Salmon Rice Paper Rolls",
                "Salmon Sushi Rice Bowl",
                "Salmon With Broccolini",
                "Salmon with Steamed Vegetables",
                "Salmon, Leek & Potato Tray Bake",
                "Salmon, Mash & Broccoli",
                "Salmon, Rice & Cucumber Pico De Gallo",
                "Salt & Vinegar Chips",
                "Salted Caramel Oatmeal",
                "Salted Caramel Yogurt",
                "Satay Beef",
                "Satay Chicken Skewers and Rice",
                "Satay Tofu Noodles",
                "Satay Tofu, Brown Rice & Vegetables",
                "Sausage Breakfast Muffin",
                "Sausage Risotto",
                "Scrambled Eggs & Turkey Bacon",
                "Scrambled Eggs & Turkey Bacon on Avo Toast",
                "Scrambled Eggs On Toast",
                "Scrambled Eggs With Smoked Turkey",
                "Scrambled Eggs With Tomatoes And Peppers",
                "Scrambled Eggs With Vegetables",
                "Sesame Tofu Noodles",
                "Shakshuka",
                "Shiitake Spaghetti",
                "Shredded Beef Nachos",
                "Shredded Beef Quesadilla",
                "Shredded Chicken Nachos",
                "Shredded Chicken Pizza",
                "Shrimp & Avocado Ceviche",
                "Shrimp Avocado Salad",
                "Shrimp Ceviche",
                "Shrimp Rice Bowl With Veggies",
                "Shrimp, Rice & Veggies",
                "Simple Berry Protein Oats",
                "Simple Chicken Fried Rice",
                "Simple Chickpea and Tahini Salad",
                "Simple Pesto Pasta",
                "Simple Spag Bowl",
                "Simple Tofu Fried Rice",
                "Simple Tuna Mayo Salad",
                "Simple Tuna Mayo Salad On Toast",
                "Slow Cooker Massaman Curry",
                "Smashed Avocado Toast",
                "Smoked Salmon Sandwich",
                "Smoked Tofu and Avocado Tortillas",
                "Smoked Turkey Salad With Avocado And Orange",
                "Snickers Overnight Oats",
                "Southwest Chicken Salad",
                "Spaghetti & Meatballs",
                "Spicy Beef Wraps",
                "Spicy Chicken Breast And Tomato Sauce",
                "Spicy Chicken Sandwich",
                "Spicy Shrimp Tacos",
                "Spicy Soba Noodle Chicken Salad",
                "Spicy Tofu With Veggies",
                "Spinach Almond Salad",
                "Spinach Baked Chicken",
                "Spinach Goats Cheese Stir Fry",
                "Spinach Omelette",
                "Spinach, Feta, & Egg White Wrap",
                "Sriracha eggs and avo toast",
                "Steak and Chips",
                "Steak Diane",
                "Steak Fajitas",
                "Steak Skewers",
                "Steak with Mash Potato and Broccoli",
                "Steak, Mash & Broccoli",
                "Steak, Mash & Gravy",
                "Stir Fry Turkey Bowl",
                "Strawberries & Cream Overnight Oats",
                "Strawberry & Almond Butter Yogurt Bowl",
                "Strawberry & Peach Yogurt Parfait",
                "Strawberry & White Choc Oats",
                "Strawberry Banana Milkshake",
                "Strawberry Chia Pudding",
                "Strawberry Cream Cheese Bagel",
                "Strawberry Protein Smoothie",
                "String Cheese & Apple",
                "Stuffed Eggplants",
                "Summer Smoothie",
                "Sunset Chicken Burger",
                "Sunset Chicken Burger (higher calorie)",
                "Sunset Fish Burger",
                "Sunset Fish Burger (lower calorie)",
                "Supreme Pizza Tortilla",
                "Swedish Meatballs & Mash",
                "Sweet Berry Protein Milkshake",
                "Sweet Chili Chicken & Cheese Burger",
                "Sweet Chili Chicken Noodle Salad",
                "Sweet Chili Peanut Tofu Noodles",
                "Sweet Chili Prawn Noodles",
                "Sweet Chili Prawn Rice Paper Rolls",
                "Sweet Chili Salmon",
                "Sweet Chili Tofu Noodle Salad",
                "Sweet Chili Tuna Rice Bowl",
                "Sweet Pancake Tacos",
                "Sweet Potato And Lentil Salad",
                "Sweet Potato Nachos (Beef)",
                "Sweet Potato Nachos (Vegetarian)",
                "Sweet Tuna Salad",
                "Sweetcorn & Mayo Tuna Pasta Salad",
                "Taco Salad",
                "Tandoori Chicken Wrap",
                "Tempeh Lettuce Cups",
                "Tempeh Pad Thai",
                "Teriyaki Beef Bowls",
                "Teriyaki Chicken Salad",
                "Teriyaki Chicken Tray Bake",
                "Teriyaki Salmon Soba Bowl",
                "Teriyaki Salmon, Mash & Broccoli",
                "Teriyaki Tofu Stir Fry",
                "Thai Green Curry With Chicken",
                "Three Cheese Pasta",
                "Tilapia Veracruz",
                "Tilapia Veracruz With Potatoes",
                "Tilapia Veracruz With Rice",
                "Toast With Cheese And Tomatoes",
                "Tofu & Broccoli",
                "Tofu And Vegetable Sandwich",
                "Tofu And Vegetable Skewers",
                "Tofu Enchiladas",
                "Tofu Fried Rice",
                "Tofu Rice Paper Rolls",
                "Tofu Scramble with Toast",
                "Tofu Teriyaki Tray Bake",
                "Tofu With Greens And Rice",
                "Tofu, Avocado & Broccoli",
                "Tofu, Broccoli & Brown Rice",
                "Tofu, Carrots, Beans & Broccoli",
                "Tofu, Egg & Asparagus",
                "Tomato & Bocconcini Pizza",
                "Tomato & Pesto Rice Cake",
                "Tomato and Tofu Couscous",
                "Tomato Feta Omelette",
                "Tropical Fruit Yogurt Bowl",
                "Tropical Overnight Oats",
                "Tropical Vegan Yogurt Bowl",
                "Tuna & Beetroot Rice Cakes",
                "Tuna Avocado Tortillas With Veg",
                "Tuna Egg Salad with Vegetables",
                "Tuna Jacket Potato",
                "Tuna Orzo Pasta Salad",
                "Tuna Pasta Bake",
                "Tuna Patties",
                "Tuna Rice Paper Rolls",
                "Tuna Salad",
                "Tuna Sushi Bowl",
                "Tuna Tacos",
                "Tuna Vegetable Tortilla",
                "Tuna With Lemon And Greens",
                "Tuna Wrap",
                "Tuna Wraps",
                "Turkey Bacon & Egg Bagel",
                "Turkey Bacon & Egg Muffin",
                "Turkey Bacon & Egg Muffin With Protein Shake",
                "Turkey Bacon & Scrambled Eggs",
                "Turkey Bacon Scramble on Toast",
                "Turkey Bacon Scrambled Egg Muffin",
                "Turkey Bacon, Eggs & Avo on Sourdough",
                "Turkey Bolognese",
                "Turkey Breast And Mustard Sandwich",
                "Turkey Burrito",
                "Turkey Enchiladas",
                "Turkey Picadillo",
                "Turkey Pizza",
                "Turkey Sandwich",
                "Turkey Shish Kebab",
                "Turkey Stuffed Peppers",
                "Turkey Zoodle Bolognese",
                "Turkey, Rice & Asparagus",
                "Turkish Eggs",
                "Vanilla Peach Smoothie",
                "Vegan Avo Toast",
                "Vegan Beetroot Burgers",
                "Vegan Bolognese",
                "Vegan Breakfast Bowl",
                "Vegan Burger & Fries",
                "Vegan Burrito",
                "Vegan Burrito Bowl",
                "Vegan Carbonara",
                "Vegan Creamy Veg Pasta",
                "Vegan Crunch Wrap",
                "Vegan Hearty Pasta",
                "Vegan Lentil Pasta",
                "Vegan Lentil Tacos",
                "Vegan Overnight Protein Oats",
                "Vegan Pearl Couscous Salad",
                "Vegan Pesto Pizza",
                "Vegan Ramen",
                "Vegan Southwest Salad",
                "Vegan Stuffed Peppers",
                "Vegan Taco Salad",
                "Vegan Wrap",
                "Vegetable Couscous",
                "Vegetarian Greek Style Wrap",
                "Vegetarian Lasagne",
                "Vegetarian Nachos",
                "Veggie Cottage Cheese Frittata",
                "Veggie Pasta",
                "Veggie Pesto Pizza",
                "Veggie Tortilla Pizza",
                "Vietnamese Chicken Salad",
                "Waffles With Pumpin And Spice",
                "Watermelon",
                "White Bean and Broccoli Pasta",
                "White Choc & Blueberry Yogurt Pop",
                "White Choc & Raspberry Protein Ice-Cream",
                "White Choc Caramel Mug Cake",
                "Whitefish, Asparagus & Brown Rice",
                "Whitefish, Asparagus & Chickpeas",
                "Zucchini Caprese",
                "Zucchini Chicken Enchiladas",
                "Zucchini Noodles with Tofu",
                "Zucchini Patties"
            };
            List<string> oldApp = new List<string>()
            {
                "2 eggs & apple",
                "Acai Bowl with Blueberries",
                "Almond Energy Balls",
                "Almond Vanilla Smoothie",
                "Almond Yogurt with Chia and Oats",
                "apple (182g)",
                "Apple & Almonds",
                "Apple & Cinnamon Oats",
                "Apple & Creamy Peanut Butter Dip",
                "Apple & Ham Swiss Wrap",
                "Apple & Peanut Butter",
                "Apple and Peanut Butter",
                "Apple Cinnamon Smoothie",
                "Apple Crumble Overnight Oats",
                "Apple Kiwi Smoothie",
                "Apple Pancakes",
                "Apple Pie Dessert",
                "Apple Pie Oatmeal",
                "Apple Pie Smoothie",
                "Apple Pie Toast",
                "Apple with Almond Butter & Granola",
                "Arugula and Tofu Salad",
                "Asian eggs, broccoli and brown rice",
                "Asian Greens Bowl",
                "Asian Prawn Salad",
                "Asian tofu, broccoli & brown rice",
                "Asparagus & Mushroom Risotto",
                "Asparagus Avocado Fettuccine",
                "Asparagus Cheese Salad",
                "Avocado and Tuna Salad",
                "Avocado Arugula Omelette",
                "Avocado Blueberry Smoothie",
                "Avocado Canape with Walnuts",
                "Avocado Sandwich with Fried Egg",
                "Avocado Sandwich with Pomegranate Seeds",
                "Avocado Smoothie",
                "Avocado Stuffed with Tuna",
                "Avocado Toast",
                "Bacon & Egg Bagel",
                "Bacon & Egg Fried Rice",
                "Bacon & Egg Muffin",
                "Baked Avocado Egg with Tuna Salad",
                "Baked Avocado with Eggs",
                "Baked Chicken Salad",
                "Baked Chicken with Quinoa and Vegetables",
                "Baked Eggplant",
                "Baked Eggplant with Mozzarella",
                "Baked Eggs with Tomatoes and Broccoli",
                "Baked Feta Risotto",
                "Baked Frittata with Mushrooms and Spinach",
                "Baked Green Beans with Eggs",
                "Baked Miso Salmon",
                "Baked Pesto Chicken",
                "Baked Rainbow Trout",
                "Baked Salmon with Broccoli",
                "Baked Sweet Potato and Garlic Salmon",
                "Baked Turkey Patties in Marinara Sauce",
                "Balsamic Turkey Breast",
                "banana (118g)",
                "Banana & Cacao Smoothie Bowl",
                "Banana & Peanut Butter",
                "Banana Almond Caramel Bowl",
                "Banana and Walnut Muffins",
                "Banana Berry Smoothie Bowl",
                "Banana Bowl With Yogurt",
                "Banana Bread Baked Oats",
                "Banana Bread Oatmeal",
                "Banana Chocolate Milkshake",
                "Banana Chocolate Smoothie",
                "Banana Cinnamon Smoothie",
                "Banana Oatmeal Smoothie",
                "Banana Protein Oats",
                "Banana Rice Cakes",
                "Banana Split",
                "Barbecue Chicken with Green Beans and Cauliflower",
                "Barbecue Chicken with Green Beans and Mash",
                "Basic Banana Proats",
                "Basic Choc Blueberry Proats",
                "Basic Choc Blueberry Proats (VEGAN)",
                "Basil tofu and broccoli",
                "BBQ and Chili Chicken Quinoa and Broccoli",
                "BBQ Chicken Coleslaw Roll",
                "BBQ Chicken Tortilla Pizza",
                "BBQ Jackfruit and Beans",
                "Bean and Lentil Salad with Pecans",
                "Bean Enchiladas",
                "Beans Corn and Prawn Bowl",
                "Beef Bolognese",
                "Beef Bulgogi",
                "Beef Burrito",
                "Beef Burrito Bowl",
                "Beef Burrito with Vegetables",
                "Beef Chili",
                "Beef Enchiladas",
                "Beef Lasagne",
                "Beef Pita Wraps with Veggies",
                "Beef Ramen Noodles",
                "Beef Ravioli",
                "Beef Ravioli (Lower Calorie)",
                "Beef Sausage Rolls",
                "Beef Shepherd's Pie",
                "Beef Soba Noodle Stir Fry",
                "Beef Stir Fry",
                "Beef Tacos",
                "Beef Tacos with Tofu",
                "Beef with Asian Greens",
                "Beef Wraps with Tomatoes",
                "Beef zoodle bolognese",
                "Beet Burgers",
                "Beet Smoothie",
                "Beetroot Corn Cakes",
                "Beetroot Rice Cakes",
                "Berries & Almonds",
                "Berries & Yogurt",
                "Berry and Almond Chia Pudding",
                "Berry Apple Smoothie",
                "Berry Nut Butter Yogurt Snack",
                "Berry Vanilla Smoothie Bowl",
                "Big English Breakfast",
                "Big Mac Burger & Chips",
                "Biscoff & Blueberry Protein Mug Cake",
                "Biscoff & Dark Choc Oats",
                "Biscoff & Strawberry Ice-Cream Pop",
                "Biscoff Baked Oats",
                "Biscoff Cottage Chesse Ice-Cream",
                "Biscoff French Toast",
                "Biscoff Glazed Overnight Oats",
                "Biscoff Ice-Cream Waffle Cone",
                "Biscoff Overnight Weetbix",
                "Biscoff Protein Oats",
                "Biscoff Ricotta Crumpets",
                "Biscoff Scrambled Pancakes",
                "Biscoff Strawberry Rolls",
                "Black Bean and Pepper Stir Fry",
                "Black Bean Burrito Bowl",
                "Black Bean Stir Fry",
                "Blackberry and Banana Protein Smoothie Bowl",
                "Blackberry And Strawberry Smoothie",
                "Blackberry Smoothie Bowl",
                "Blessed Bar Waffle Stack",
                "Blessed Bar Yogurt Bowl",
                "Blessed Protein Bar",
                "Blessed Protein Bar & Apple",
                "Blessed Protein Bar, Popcorn & Apple",
                "BLT Pasta Salad",
                "Blueberry Banana Smoothie",
                "Blueberry Milkshake",
                "Blueberry Parfait",
                "Blueberry Smoothie Bowl",
                "Blueberry Vegan Pancakes",
                "Blueberry Yogurt With Oats",
                "Boiled Egg with Spinach & Nuts",
                "Boiled Eggs with Herb Sauce",
                "Boiled Eggs With Roasted Chickpeas And Arugula",
                "Breakfast Cheese Tortilla",
                "Breakfast Cookie",
                "Breakfast Egg Muffin",
                "Breakfast Hashbrown Toast",
                "Breakfast Pizza",
                "Breakfast Potato Bean Salad",
                "Breakfast Pumpkin Waffles",
                "Breakfast Sandwich",
                "Breakfast Tacos",
                "Breakfast Tiramisu",
                "Breakfast Wrap",
                "Broccoli & Chicken Stir-Fry",
                "Broccoli Casserole",
                "Broccoli Herbed Chicken",
                "Broccoli Salad with Bacon",
                "Brown Rice Buddha Bowl",
                "Brown Rice Mushroom Risotto",
                "Buddha Bowl with Spinach, Avocado, and Radishes",
                "Buddha Bowl With Turkey And Avocado",
                "Bueno Overnight Oats",
                "Buffalo Chicken Drumsticks",
                "Bunless Beef Burger & Parmesan Chips",
                "Busy Gal Breakfast",
                "Busy Girl Breakfast",
                "Butter Chicken & Naan",
                "Cabbage Broccoli And Chicken With Herbs",
                "Cajun Beef and Veggie Rice",
                "Cajun Chicken Couscous Salad",
                "Candy Cane Protein Shake",
                "Caprese Chicken Salad",
                "Caprese Rice Cakes",
                "Caprese Toast",
                "Caprese Tuna Salad",
                "Caramel Banana Smoothie Bowl",
                "Caramel Protein Balls",
                "Caramel Quinoa Bowl",
                "Caramel White Choc Mug Cake",
                "Carrot Cake Muffin",
                "Carrots & Almonds",
                "Carrots & Turkey",
                "Cauliflower Crust Pizza",
                "Cauliflower Fried Rice",
                "Cheese & Olive Melt",
                "Cheese & Tomato Toastie",
                "Cheese Burger",
                "Cheese Omelette With Vegetables",
                "Cheeseburger Salad",
                "Cheesy Pasta Bake",
                "Chia Blueberry Smoothie",
                "Chia Overnight Oats with Cranberries",
                "Chia Seeds Pudding",
                "Chicken & Spinach Gnocchi",
                "Chicken & Sriracha Mayo Rice Bowl",
                "Chicken 3-color Quinoa and Vegetables",
                "Chicken and Broccoli",
                "Chicken and Egg Salad",
                "Chicken And Quinoa Salad",
                "Chicken and Tomato Skewers with Vegetables",
                "Chicken and Vegetable Soup",
                "Chicken Asparagus Avocado Salad",
                "Chicken Breast in Tomato Sauce",
                "Chicken Breast in Tomato Sauce with Quinoa and Broccoli",
                "Chicken Breast Salad",
                "Chicken Breast With Cabbage And Broccoli",
                "Chicken Burger & Chips",
                "Chicken Burrito",
                "Chicken Caesar Salad",
                "Chicken Carbonara",
                "Chicken Fajitas",
                "Chicken Farfalle",
                "Chicken Fettuccine",
                "Chicken Fingers",
                "Chicken Fried Rice",
                "Chicken Green Curry",
                "Chicken Mushroom Risotto",
                "Chicken Mushroom Wraps",
                "Chicken Napolitana Pasta",
                "Chicken Pad Thai",
                "Chicken Pasta Salad",
                "Chicken Pearl Couscous Salad",
                "Chicken Pesto Pasta",
                "Chicken Pesto Pasta Salad",
                "Chicken Pesto Pizza",
                "Chicken Pesto Wrap",
                "Chicken Pineapple Quinoa and Veg Bowl",
                "Chicken Quesadilla",
                "Chicken Quinoa and Broccoli with chili",
                "Chicken Ramen Bowl",
                "Chicken Ricotta Toastie",
                "Chicken Risotto",
                "Chicken Salad to go",
                "Chicken Salad With Roasted Vegetables",
                "Chicken Schnitzel Salad",
                "Chicken Schnitzel Wrap",
                "Chicken Shawarma",
                "Chicken Skewers with Eggs and Fresh Vegetable Salad",
                "Chicken Snack Box",
                "Chicken Stew with Peppers",
                "Chicken Vermicelli Noodles & Spicy Mayo",
                "Chicken, Broccoli & Brown Rice",
                "Chicken, Broccoli & Sweet Potato",
                "Chicken, Mash & Broccoli",
                "Chickpea & Sweet Potato Wrap",
                "Chickpea and Tahini Salad",
                "Chickpea Bruschetta",
                "Chickpea Green Curry",
                "Chickpea Quesadilla",
                "Chickpea Scramble",
                "Chickpea Snack",
                "Chickpea Stuffed Peppers",
                "Chili Chicken & Veggies",
                "Chili Con Carne",
                "Chili Ground Beef",
                "Chili Risotto With Egg",
                "Chili T-Bone Steak",
                "Chinese Chicken Salad",
                "Choc Banana Bites",
                "Choc Caramel Mug Cake",
                "Choc Caramel Protein Smoothie",
                "Choc Caramel Smoothie Bowl",
                "Choc Cherry Smoothie",
                "Choc Chip Cookie Dough Balls",
                "Choc Chip Cookies",
                "Choc Chip Filled Strawberries",
                "Choc Chip Mug Cake",
                "Choc Chip Pancakes",
                "Choc Chip Yogurt Cookie Dough",
                "Choc Coconut Smoothie",
                "Choc Oreo Donuts",
                "Choc PB Rice Cake",
                "Choc Peanut Butter Yogurt Bark",
                "Choc Protein Ice Cream",
                "Choc Protein Popcorn",
                "Choc-Peanut Protein Bar",
                "Choco Banana Smoothie",
                "Chocolate Chia Creamy Smoothie Bowl",
                "Chocolate Chia Oatmeal with Berries, Banana, and Kiwi",
                "Chocolate Chip Pancakes",
                "Chocolate Egg Nest",
                "Chocolate Milkshake",
                "Chocolate Protein Mug Cake",
                "Chocolate Protein Pudding",
                "Chorizo & Veggie Pizza",
                "Chorizo Mushroom Pasta",
                "Cinnamon & Banana Smoothie",
                "Cinnamon Mocha Protein Shake",
                "Cinnamon Oats with Pecans",
                "Cinnamon Porridge With Blueberries",
                "Citrus Salad with Feta and Pomegranate Seeds",
                "Coconut Chocolate Smoothie Bowl",
                "Coconut Curry Fish with vegetables",
                "Coconut Oatmeal",
                "Coconut Strawberry Smoothie",
                "Cookie Dough Oatmeal",
                "Cookies & Cream Yogurt Bowl",
                "Corn Cake Avo Smash with Cottage Cheese",
                "Corn Chips & Guac",
                "Corn Chips and Salsa",
                "Cottage Cheese & Bacon Alfredo",
                "Cottage Cheese Pancakes with Blueberries",
                "Crackers & Cheese",
                "Cream Cheese & Tomato Rice Cakes",
                "Creamy Apple Pie Smoothie",
                "Creamy Cajun Chicken Pasta",
                "Creamy Chocolate Protein Bowl",
                "Creamy Potato & Salmon Tray Bake",
                "Creamy Raspberry Smoothie",
                "Creamy Strawberry Milkshake",
                "Creamy Tuna Pasta Salad",
                "Creamy Tuna Sandwich",
                "Creamy Tuscan Chicken and Mash",
                "Creamy Tuscan Chicken Pasta",
                "Creamy Veg Chicken Pasta",
                "Crepes Suzette",
                "Crispy Fish Tacos",
                "Crispy Tofu with Cauliflower Rice",
                "Crumpet with Avocado & Egg",
                "Crumpet with Berries & Ricotta",
                "Crunchy Chickpeas",
                "Cucumber & Turkey",
                "Cucumber Salad",
                "Cucumber, Almonds & Mozzarella",
                "Dark Choc Overnight Oats",
                "Dark Choc PB Overnight Oats",
                "Dark Chocolate",
                "Dark Chocolate Snack",
                "Deconstructed Pesto Pasta",
                "Easter Egg Overnight Weetbix",
                "Easy Banana Pancakes",
                "Easy Beef Steak",
                "Easy Tuna Lunch Bowl",
                "Edamame Snack",
                "Egg & Almonds",
                "Egg & Carrots",
                "Egg & Cucumber",
                "Egg Bruschetta with Tomatoes",
                "Egg Fried Rice",
                "Egg Salad",
                "Egg Salad Sandwich",
                "Egg White Muffins",
                "Egg White Omelette",
                "Egg White Scramble",
                "Egg White, Spinach & Feta Wrap",
                "Eggnog French Toast",
                "Eggplant Parmigiana",
                "Eggplant Stuffed with Beef and Tomatoes",
                "Eggs with Hummus & Toast",
                "Eggs with Sweet Potato and Kale Hash",
                "Eggs, Beans & Vegetables",
                "Eggs, Broccoli & Chickpeas",
                "Eggs, Brown Rice & Broccoli",
                "English Muffin with Ricotta and Tomato",
                "Festive Cranberry Glazed Turkey Breast",
                "Festive Grazing Platter for Two",
                "Feta Salad",
                "Feta Scrambled Eggs On Toast",
                "Fish & Chips",
                "Fish Taco Bowl",
                "Flatbread Lamb Kebab",
                "Flourless Broccoli and Cauliflower Bread",
                "Fragrant Beef Stir Fry",
                "Freezer Cookie",
                "Fried Egg & Avo Toast",
                "Fried Egg and Mushroom Sandwich",
                "Fried Egg, Bacon & Avo Toast",
                "Fried Eggs with Asparagus and Tomatoes",
                "Fried Eggs With Toast And Vegetables",
                "Fried Eggs with Tuna",
                "Fried Noodle Chicken Salad",
                "Fried Salmon With Spinach",
                "Fried Tofu and Avocado Salad",
                "Fried Tofu with Broccoli and Mung Bean Sprouts",
                "Fried Tofu with Green Beans",
                "Fried Tofu with Green Beans and Chilli",
                "Fried Vegetable Sandwich",
                "Fried Zucchini with Peppers and Cashews",
                "Frozen Apple Berry Smoothie",
                "Frozen Berry Smoothie",
                "Frozen Grapes",
                "Fruit & Nut Yogurt Bowl",
                "Fruit Bruschetta",
                "Garlic & Prawn Tortilla Pizza",
                "Garlic Salmon With Beans and Sweet Potato",
                "Ginger and Sweet Chilli Salmon",
                "Gingerbread Overnight Oats",
                "Gingerbread Overnight Quinoa(GF, DF)",
                "Gooey Lust Bar",
                "Granola Parfait",
                "Granola Smoothie Bowl",
                "Grapes, Cheese & Crakers",
                "Grated Cheesy Eggs on Toast",
                "Grated Egg Toast",
                "Greek Berry Bowl",
                "Greek Chicken & Chickpea Salad",
                "Greek Chicken Pita Salad",
                "Greek Chicken Rice Bowl",
                "Greek Chicken Salad",
                "Greek Inspired Lamb Salad",
                "Greek Pasta Salad",
                "Greek Style Lamb Pasta",
                "Greek Syle Avocado Toast",
                "Greek Toast",
                "Greek Yogurt & Banana",
                "Greek Yogurt & Peanut Butter",
                "Greek Yogurt with Strawberries",
                "Green Breakfast Smoothie",
                "Green Goddess Chicken Salad",
                "Green Pesto Buddha Bowl",
                "Green Protein Smoothie",
                "Grilled Cajun Chicken",
                "Grilled Chicken Breast with Asparagus",
                "Grilled Chicken Gyros",
                "Grilled Chicken Parmigana",
                "Grilled Chicken Tacos",
                "Grilled Chicken with Tomatoes and Garlic",
                "Grilled Eggplant Salad",
                "Grilled Fish & Summer Salad",
                "Grilled Halloumi and Lentil Salad",
                "Grilled Halloumi Sandwich",
                "Grilled Mushrooms Salad",
                "Grilled Peach Salad with Blue Cheese and Walnuts",
                "Grilled Pineapple",
                "Grilled Pineapple Chicken",
                "Grilled Pork Chop Steak",
                "Grilled Prawn Skewers",
                "Grilled Prawns and Summer Veggies",
                "Grilled Salmon Fillet",
                "Grilled Salmon with Vegetables and Rice",
                "Grilled Steak Mexican Salad",
                "Grilled Steak Salad",
                "Grilled Sweet Potato With Avocado Mousse And Boiled Egg",
                "Grilled Tofu Steak with Cucumber and Rice",
                "Grilled Vegetables with Greek Yogurt",
                "Grilled Zucchini Salad",
                "Ground Beef & Green Beans",
                "Ground Beef with Asian Greens",
                "Ground Beef Wraps",
                "Guacamole & Veggies",
                "Ham & Cheese Tortilla Pizza",
                "Ham Salad Bagel",
                "Ham, Cheese & Cucumber Roll Ups",
                "Hawaii Chicken Bowl",
                "Hawaiian Pineapple Chicken Bowl",
                "Hawaiian Pizza Pockets",
                "Hawaiian Salad",
                "Hazelnut Chocolate Milkshake",
                "Herb And Veg Chicken",
                "High Protein Carrot Mug Cake",
                "Honey Glazed Turkey Breast with Wild Rice and Avocado",
                "Honey Glazed Turkey Topped With Avocado",
                "Honey Soy Chicken",
                "Honey Soy Chicken Noodle Stir - Fry",
                "Honey Soy Salmon Rice Bowl",
                "Hotcakes & Bacon",
                "Hummus Buddha Bowl",
                "Hummus Rice Cakes",
                "Hummus Toast",
                "Hummus with Fruit & Nuts",
                "Hummus, Crackers and Veggie Sticks",
                "Italian Chicken Beans And Vegetables",
                "Italian eggs, broccoli & chickpeas",
                "Italian Poached Egg Toast",
                "Italian Spiced Chicken & Veggies",
                "Italiano egg, chickpeas and vegetable",
                "Jalapeno Poppers",
                "Japanese Chicken Rice Bowl",
                "Kale Egg Muffins with Chicken",
                "Kale Salad with Buckwheat, Pine Nuts and Tofu",
                "Kidney Bean Risotto",
                "Lamb Shepherd's Pie",
                "Lauren's 'Scones' with Jam and Ricotta",
                "Lauren's 'Scones' with Jam and Vegan Yoghurt",
                "Lauren's Apple Pie Snack",
                "Lauren's Apple Pie Snack (VEGAN)",
                "Lauren's Basic Tuna Rice Cakes",
                "Lauren's Beef Nachos",
                "Lauren's Egg White Scramble",
                "Lauren's Famous Rice Paper Rolls",
                "Lauren's Famous Rice Paper Rolls (VEGAN)",
                "Lean Turkey Zoodle Bolognese",
                "Lemon Herb Chicken",
                "Lemon Shrimp Salad",
                "Lemongrass Beef Rice Paper Rolls",
                "Lemongrass Tofu Stir Fry",
                "Lentil Bolognese",
                "Lentil Burgers and Sweet Potato Salad",
                "Lentil Pasta",
                "Lentil Pasta Salad",
                "Lentil Quinoa Salad",
                "Lentil Shepherd's Pie",
                "Lentil Soup",
                "Lentil Spinach Stew",
                "Lime Drizzled Turkey And Vegetables",
                "Loaded Baked Potato",
                "Loaded Smores Smoothie",
                "Low Carb French Toast",
                "Low Carb Protein Waffles",
                "Loz's Banana Choc Muffins",
                "Loz's Chicken Nuggets",
                "Loz's Choc Caramel Quick Oats ",
                "Loz's Custard Toast ",
                "Loz's Shredded Chicken Muffin ",
                "Loz's Tuna on Rice Cakes",
                "Loz's Tuna Salad",
                "Lust Bar",
                "Lust Bar with Apple",
                "Macadamias & Dark Chocolate",
                "Macro Friendly Chicken Schnitzel",
                "Mango Peach Smoothie",
                "Margherita Toastie",
                "Mars Bar Weetbix Cheesecake",
                "McChicken Burger",
                "Meatballs Wild Rice and Carrots With Spicy Sauce",
                "Meatballs, Mash & Broccoli",
                "Mediterranean Omelet",
                "Mediterranean Prawn Spaghetti",
                "Mexi Beef and Veg",
                "Mexican Avocado Toast",
                "Mexican Beef Loaded Fries",
                "Mexican Chicken Pasta Salad",
                "Mexican Ground Beef with Vegetables",
                "Mexican Tacos",
                "Mexican tofu, avocado and broccoli",
                "Minestrone Soup",
                "Mini Banana Pancake Bites",
                "Mini Biscoff Cheesecake",
                "Mini Eggplant Pizza",
                "Mini Hawaiian Pizzas",
                "Mini Nutella Cheesecake",
                "Mini Sweet Chilli Chicken Pizzas",
                "Mini Zucchini Casserole",
                "Miso Chicken Rice Bowl",
                "Mixed Berry & Granola Yogurt Bowl",
                "Mixed Berry and Almond Smoothie Bowl",
                "Mixed Berry Protein Smoothie",
                "Mixed Berry Smoothie",
                "Mixed Blueberry Smoothie",
                "Mocha Cocoa Energy Balls",
                "Mocha Nice Cream",
                "Mocha Protein Balls",
                "Moroccan Soup with Chickpeas",
                "Mushroom & Halloumi Pita",
                "Mushroom and Blue Cheese Sandwich",
                "Mushroom Omelette",
                "Mushroom Rice Cakes",
                "Nectarine(129g)",
                "No Bake Peanut Butter Cheesecakes",
                "No Bake Protein Balls",
                "No Bake Protein Bars",
                "No Bake Raspberry Cheesecake",
                "Nutella & Banana Bagel",
                "Nutella Fondant & Ice - Cream",
                "Nutella French Toast",
                "Nutella Overnight Oats",
                "Nutella Overnight Weetbix",
                "Nutella Protein Oats",
                "Nutella Ricotta Crumpets",
                "Oat Protein Balls",
                "Oatmeal & Berries",
                "Oatmeal Berry Porridge",
                "Oatmeal Granola Bowl",
                "Oatmeal with Fresh Vegetables",
                "Oats with Almond Yogurt and Raspberries",
                "Omelette Salad",
                "One - Pan Pesto Chicken",
                "Open Tuna Sandwich",
                "Orange Chicken",
                "Oreo Cottage Cheese Ice - Cream",
                "Oven Baked Risotto With Chorizo And Vegetables",
                "Overnight Chocolate Oats",
                "Overnight Chocolate Oats With Dark Choc and PB",
                "OxyWhey or Blessed Protein Shake",
                "Pad See Ew",
                "Panzanella",
                "Pasta with Vegetables",
                "PB & Choc Cheesecakes",
                "PB banana toast",
                "Peach(147g)",
                "Peach Vanilla Smoothie Bowl",
                "Peanut Butter & Celery",
                "Peanut Butter & Jelly Oats",
                "Peanut Butter Acai Bowl",
                "Peanut Butter and Swiss Chocolate Overnight Oats",
                "Peanut Butter Apple Sandwich",
                "Peanut Butter Banana Smoothie",
                "Peanut Butter Banana Yogurt Bowl",
                "Peanut Butter Dates",
                "Peanut Butter Fudge",
                "Peanut Butter Popcorn",
                "Peanut Butter Rice Cakes",
                "Pepperoni Cheesy Crust Pizza",
                "Pepperoni Pizza Tortilla",
                "Peri Peri Chicken Burger",
                "Peri Peri Chicken Pasta",
                "Peri Peri Chicken Rice",
                "Peri Peri Pizza",
                "Pesto Avocado Toast",
                "Pesto Chicken Pasta Salad",
                "Pesto Eggs on Avo Toast",
                "Pesto Fried Eggs",
                "Pesto Grilled Cheese Toastie",
                "Pesto Prawn Pizza",
                "Pesto Salmon Tray Bake",
                "Pesto Tofu Zoodles",
                "Pesto Veggie Rice Bowl",
                "Pina Colada Smoothie",
                "Pineapple Chicken Curry Bowl",
                "Pineapple Parfait",
                "Pita Bread with Spinach and Mozzarella",
                "Pizza Bagel",
                "Pizza Tortilla",
                "Plum Vanilla Smoothie",
                "Poached Egg And Avocado Sandwich",
                "Poached Egg Salad",
                "Poached Egg Toast with Veggies",
                "Poached Egg with Kale Salad",
                "Poached Egg with Leeks",
                "Poached Eggs with Spinach",
                "Poke Bowl",
                "Popcorn & Apple",
                "Popcorn and Almonds",
                "Popcorn Trail Mix",
                "Portabello Pizza",
                "Potato Carrot Patties",
                "Potato Mushroom Stir - Fry",
                "Potato Pancakes with Smoked Salmon",
                "Prawn And Legume Spicy Bowl",
                "Prawn Salad with Greens",
                "Prawn Spaghetti with Avocado",
                "Prawn Spaghetti with Avocado and Sun Dried Tomatoes",
                "Prawn Wild Rice and Veg Bowl",
                "Prawns, Brown Rice & Sweet Potato",
                "Prawns, Chicken & Broccoli",
                "Pro Yo",
                "Protein Avocado Puree Sandwich",
                "Protein Berry Smoothie Bowl",
                "Protein Bircher Muesli",
                "Protein Chia Pudding with Raspberry Sauce",
                "Protein Chocolate",
                "Protein Crumpets",
                "Protein French Toast",
                "Protein Iced Coffee",
                "Protein Pudding With Chia and Berries",
                "Protein Pumpkin Spice Latte",
                "Protein Yogurt",
                "Pulled Pork Nachos",
                "Pulled Pork Rice Bowl",
                "Pulled Pork Sliders",
                "Pulled Pork Wrap",
                "Pumpkin & Feta Chicken Salad",
                "Pumpkin Oatmeal",
                "Pumpkin Oatmeal with Apples",
                "Pumpkin Pie Smoothie",
                "Pumpkin Pie Smoothie Bowl",
                "Pumpkin Soup & Toast",
                "Quick Apple Crumble",
                "Quick Beef Stir Fry",
                "Quick Chicken Stir Fry",
                "Quick Chili Chicken Noodles",
                "Quick Teriyaki Chicken",
                "Quick Tuna Salad",
                "Quinoa and Chickpea Salad",
                "Quinoa and Corn Burritos",
                "Quinoa and Kidney Beans Buddha Bowl",
                "Quinoa BBQ Chicken and Veg",
                "Quinoa Broccoli with Tuna",
                "Quinoa Salad",
                "Quinoa Salad With Vegetables",
                "Quinoa Sushi",
                "Quinoa With Broccoli, Mushrooms and Lime",
                "Quinoa With Eggs And Vegetables",
                "Quinoa with Onions and Eggs",
                "Quinoa with Tuna and Lemon",
                "Quinoa, Avocado, and Spinach Salad",
                "Raspberry Almond Smoothie Bowl",
                "Raspberry Chia Pudding",
                "Raspberry Ripple Protein Oats",
                "Raspberry Smoothie",
                "Raspberry Vanilla Smoothie",
                "Red Quinoa Porridge",
                "Rice Cake Avo Smash",
                "Rice Cake Salmon Caprese",
                "Rice Turkey Patties",
                "Roast Chicken And Veg",
                "Roasted Beef Sandwich",
                "Roasted Butternut Squash",
                "Roasted Vegetable and Lentil Salad",
                "Rocky Road Yogurt Snack",
                "Rocky Road Yogurt Treat",
                "Salad Bagel & Protein Shake",
                "Salmon And Arugula Salad",
                "Salmon Avocado Bruschetta",
                "Salmon Fillet with Zucchini Noodles",
                "Salmon Rice Paper Rolls",
                "Salmon Skewers with Lime",
                "Salmon Steak with Herbs",
                "Salmon Swiss Chard Salad",
                "Salmon with Steamed Vegetables",
                "Salmon, Leek & Potato Tray Bake",
                "Salted Caramel Almond Blessed Bowl",
                "Salted Caramel Banana Goji Bowl",
                "Salted Caramel Nice Cream",
                "Salted Caramel Pancakes",
                "Salted Caramel Smoothie Bowl",
                "Salted Caramel Smoothie Bowl With Banana and Almond",
                "San Choy Bau",
                "Sandwich Thin Avo Smash",
                "Satay Chicken and Rice",
                "Satay Tofu Noodles",
                "Satay Tofu, Brown Rice & Vegetables",
                "Sausage Risotto",
                "Sausage Shakshuka",
                "Savoury Muffins",
                "Scone with Jam and Cream",
                "Scrambled Eggs On Toast",
                "Scrambled Eggs With Smoked Turkey",
                "Scrambled Eggs With Vegetables",
                "Scrambled Eggs with Vegetables and Herbs",
                "Scrambled Tofu With Vegetables",
                "Sesame and Garlic Vegetables and Teriyaki Chicken",
                "Sesame Ginger Spaghetti",
                "Sesame soy tofu & veggies",
                "Shakshuka",
                "Shiitake Spaghetti",
                "Shred Friendly Egg Burrito",
                "Shredded Beef Quesadilla",
                "Shredded Chicken Nachos",
                "Shredded Chicken Pizza",
                "Shrimp Avocado Salad",
                "Simple Chicken Fried Rice",
                "Simple Chicken Wrap",
                "Simple Chickpea and Tahini Salad",
                "Simple Hamburger",
                "Simple Mac & Cheese",
                "Simple Mexican Wrap",
                "Simple Smoked Salmon Muffin",
                "Simple Spag Bowl",
                "Simple Strawberry Smoothie",
                "Simple Tofu Fried Rice",
                "Simple Vego Wrap",
                "Single Serve Blondie",
                "Single Serve Chocolate Brownie",
                "Slow Cooker Massaman Curry",
                "Smoked Salmon & Cream Cheese Bagel",
                "Smoked Salmon & Cream Cheese Toast",
                "Smoked Salmon English Muffin",
                "Smoked Salmon Sandwich",
                "Smoked Tofu and Avocado Tortillas",
                "Smoked Turkey Salad With Avocado And Orange",
                "Smores Protein Cookie",
                "Smores Toast",
                "Snickers Baked Oats",
                "Snickers Overnight Oats",
                "Snickers Smoothie",
                "Soba Noodle & Veggie Stir Fry",
                "Southern Style Chicken Wrap",
                "Southwest Chicken Salad",
                "Soy and Garlic Tempeh Stir Fry",
                "Speedy Tuna Salad",
                "Spiced Ground Beef with Vegetables",
                "Spicy Beef Wraps",
                "Spicy Chicken Sandwich",
                "Spicy Chicken with Broccoli and Rice",
                "Spicy eggs, beans and vegetables",
                "Spicy Grilled Beef Salad",
                "Spicy Lentil Stew",
                "Spicy Marinara Meatballs",
                "Spicy Meatballs Rice and Carrots",
                "Spicy Meatballs with Wild Rice",
                "Spicy Soba Noodle Chicken Salad",
                "Spicy Spaghetti with Chickpeas",
                "Spicy Tortillas With Cabbage",
                "Spicy Tuna Potato Patties",
                "Spinach & Cheese Stir Fry",
                "Spinach Almond Salad",
                "Spinach and Avocado Smoothie Bowl",
                "Spinach And Banana Smoothie Bowl",
                "Spinach and Lentil Soup",
                "Spinach Balls",
                "Spinach Chicken",
                "Spinach Muffins",
                "Spinach Omelette",
                "Spinach Protein Balls",
                "Spinach Smoothie",
                "Sriracha Bean Prawn Bowl",
                "Sriracha eggs and avo toast",
                "Sriracha Prawn Bowl",
                "Steak and Chips",
                "Steak Diane",
                "Steak Fajitas",
                "Steak Skewers",
                "Steak with Potato Chips",
                "Steak, Mash & Gravy",
                "Stir Fry Turkey Bowl",
                "Strawberries & Cream Overnight Oats",
                "Strawberry & Choc Yogurt Bowl",
                "Strawberry & Peach Yogurt Parfait",
                "Strawberry & White Choc Oats",
                "Strawberry Banana Chia Smoothie Bowl",
                "Strawberry Banana Milkshake",
                "Strawberry Cheesecake Parfait",
                "Strawberry Chia Pudding",
                "Strawberry Chia Vanilla Bowl",
                "Strawberry Milkshake Smoothie Bowl",
                "Strawberry Protein Smoothie",
                "Strawberry Smoothie Bowl",
                "Strawberry Vanilla Smoothie",
                "Stuffed Bell Peppers",
                "Stuffed Eggplants",
                "Stuffed Mushrooms",
                "Stuffed Red Bell Peppers",
                "Summer Smoothie",
                "Sun Dried Tomato Spaghetti with Prawns",
                "Supreme Tortilla Pizza",
                "Swedish Meatballs & Mash",
                "Sweet Berry Smoothie",
                "Sweet Carrot Salad",
                "Sweet Chili Chicken Noodle Salad",
                "Sweet Chili Peanut Tofu Noodles",
                "Sweet Chili Prawn Noodles",
                "Sweet Chili Prawn Rice Paper Rolls",
                "Sweet Chili Salmon",
                "Sweet Chili Tofu Noodle Salad",
                "Sweet Chili Tuna Rice Bowl",
                "Sweet Chilli Chicken Tenders & Chips",
                "Sweet English Muffins",
                "Sweet Pancake Tacos",
                "Sweet Potato And Lentil Salad",
                "Sweet Potato Nachos(Beef)",
                "Sweet Potato Nachos(Vegetarian)",
                "Sweet Potato Patties",
                "Swiss Chocolate Almond and Chia Bowl",
                "Swiss Chocolate Smoothie Bowl",
                "Taco Salad",
                "Tandoori Chicken Bake",
                "Tandoori Chicken Wrap",
                "Tempeh Pad Thai",
                "Tempeh Stir Fry",
                "Teriyaki Chicken Bowl",
                "Teriyaki Chicken Salad",
                "Teriyaki Chicken With Sesame Vegetables",
                "Teriyaki Chicken with Vegetables",
                "Teriyaki Chicken, Veg and Avocado Stir Fry",
                "Teriyaki Salmon Soba Bowl",
                "Teriyaki Tofu Stir Fry",
                "Thai Green Curry With Chicken",
                "Three Cheese Pasta",
                "Tofu & Broccoli",
                "Tofu & egg beet salad",
                "Tofu and Cucumber Skewers",
                "Tofu And Vegetable Sandwich",
                "Tofu And Vegetable Skewers",
                "Tofu chickpea rice salad",
                "Tofu Enchiladas",
                "Tofu Fried Rice",
                "Tofu Scramble with Toast",
                "Tofu Steak Sandwich",
                "Tofu Stuffed Mushrooms",
                "Tofu With Pumpkin And Rice",
                "Tofu, Avocado & Broccoli",
                "Tofu, Broccoli & Brown Rice",
                "Tofu, Carrots, Beans & Broccoli",
                "Tofu, Chickpeas & Broccoli",
                "Tofu, Egg & Asparagus",
                "Tomato & Bocconcini Pizza",
                "Tomato and Tofu Couscous",
                "Tomato Feta Omelette",
                "Tortilla Pizza",
                "Tortilla Pizza with Tomatoes and Olives",
                "Trail Mix",
                "Tropical Overnight Oats",
                "Tropical Overnight Proats",
                "Tropical Protein Smoothie",
                "Tropical Vegan Yogurt Bowl",
                "Tuna and Bocconcini Salad",
                "Tuna Cucumber and Avocado Spaghetti",
                "Tuna Egg Salad with Vegetables",
                "Tuna Orzo Pasta Salad",
                "Tuna Pasta Bake",
                "Tuna Pasta Salad",
                "Tuna Rice Paper Rolls",
                "Tuna Salad",
                "Tuna Salad Rice Cakes",
                "Tuna Salad Wrap",
                "Tuna Sandwich Thin",
                "Tuna Slaw with Brown Rice",
                "Tuna Spaghetti With Ginger Sesame Vegetables",
                "Tuna Spinach Balls",
                "Tuna Sushi Bowl",
                "Tuna Tacos",
                "Tuna Vegetable Tortilla",
                "Tuna With Lemon And Greens",
                "Tuna Wrap",
                "Tuna Wraps",
                "Turkey & Avo Sandwich",
                "Turkey Bolognese",
                "Turkey Breast With Beans and Veg",
                "Turkey Breast With Quinoa And Avocado",
                "Turkey Burger Salad",
                "Turkey Burrito",
                "Turkey Burrito Bowl",
                "Turkey Enchiladas",
                "Turkey Lasagne",
                "Turkey Lettuce Wraps",
                "Turkey Pizza",
                "Turkey Sandwich",
                "Turkey Shish Kebab",
                "Turkey Skewers with Vegetables and Quinoa",
                "Turkey Stuffed Peppers",
                "Turkey Tacos",
                "Turkey with Quinoa and Vegetables",
                "Turkey Zoodle Bolognese",
                "Turkey, Cheese & Cucumber Roll Ups",
                "Turkish Eggs",
                "Vanilla & Strawberry Protein Balls",
                "Vanilla and Raspberry Protein Chia Pudding",
                "Vanilla Oats with Kiwi",
                "Vanilla Peach Smoothie",
                "Vanilla Pomegranate Smoothie Bowl",
                "Veg Nachos",
                "Vegan Banana Pancakes",
                "Vegan Beetroot Rice Cakes",
                "Vegan Burger & Fries",
                "Vegan Burrito",
                "Vegan Burrito Bowl",
                "Vegan Creamy Veg Pasta",
                "Vegan Grilled Tofu Gyros",
                "Vegan Hearty Pasta",
                "Vegan Lentil Tacos",
                "Vegan Loaded Mexican Fries",
                "Vegan Meatballs Spaghetti",
                "Vegan Pasta Bake",
                "Vegan Pearl Couscous Salad",
                "Vegan Pesto Fried Rice",
                "Vegan Pro Yo",
                "Vegan Ramen Bowl",
                "Vegan Sheppard's Pie",
                "Vegan Southwest Salad",
                "Vegan Taco Salad",
                "Vegan Wrap",
                "Vegetable & Ricotta Omelette",
                "Vegetable and Turkey Mince Stir Fry",
                "Vegetable Couscous",
                "Vegetable Pie",
                "Vegetable Quesadilla",
                "Vegetarian Biryani with Nuts and Raisins",
                "Vegetarian Burrito",
                "Vegetarian Cauliflower Fried Rice",
                "Vegetarian Greek Style Wrap",
                "Vegetarian Lasagne",
                "Vegetarian Meatloaf with Lentils, Nuts, and Quinoa",
                "Vegetarian Nachos",
                "Veggie & cheese omelette with toast",
                "Veggie Omelette",
                "Veggie Sticks",
                "Veggie Stuffed Sweet Potato",
                "Veggie Tortilla Pizza",
                "Vietnamese Beef Salad",
                "Vietnamese Chicken Salad",
                "Weetbix PB Cookie Dough Protein Balls",
                "White Bean and Broccoli Pasta",
                "White Choc Macadamia Single Serve Cookie",
                "White Choc Raspberry Yogurt Bowl",
                "White Fish Tacos",
                "White Fish with Kale",
                "Whitefish, Asparagus & Brown Rice",
                "Whitefish, Asparagus & Chickpeas",
                "Whole Grain Pepper Pizza",
                "Work Friendly Rice Bowl",
                "Yoghurt Choc Top",
                "Yoghurt Granola Bowl",
                "Zoodle Pasta Bolognese",
                "Zucchini and Mushroom Stew",
                "Zucchini Caprese",
                "Zucchini Casserole",
                "Zucchini Enchiladas",
                "Zucchini Noodle Bolognese",
                "Zucchini Noodles with Tofu",
                "Zucchini Patties",
                "Zucchini Rolls with Cheese"
            };

            List<string> nonMatchingNames = oldApp.Except(newApp).ToList();

            // Specify the CSV file path
            string csvFilePath = "C:\\Users\\sucha\\source\\repos\\Arty781\\MCMAutomation\\nonMatchingNames.csv";

            // Write non-matching names to CSV file
            WriteToCsv(csvFilePath, nonMatchingNames);

            Console.WriteLine("Non-matching names written to CSV file.");

            static void WriteToCsv(string filePath, List<string> names)
            {
                // Create or append to the CSV file
                using StreamWriter writer = new(filePath, true);
                // Check if the file is empty and write the header if needed
                if (new FileInfo(filePath).Length == 0)
                {
                    writer.WriteLine("NonMatchingNames");
                }

                // Write each non-matching name to the CSV file
                foreach (var name in names)
                {
                    writer.WriteLine(name);
                }
            }


        }

        [Test]
        public void DemoTest()
        {

            #region Preconditions

            #region Register New User
            //string email = RandomHelper.RandomEmail();
            string email = "annfall1111@gmail.com";
            //SignUpRequest.RegisterNewUser(email);
            var responseLoginUser = SignInRequest.MakeSignIn(email, Credentials.PASSWORD);
            EditUserRequest.EditUser(responseLoginUser);
            #endregion

            #region Add and Activate membership to User
            string userId = AppDbContext.User.GetUserData(email).Id;
            //AppDbContext.Memberships.GetLastMembership(out DB.Memberships membershipId);
            //var responseLoginAdmin = SignInRequest.MakeSignIn(Credentials.LOGIN_ADMIN, Credentials.PASSWORD_ADMIN);
            //MembershipRequest.AddUsersToMembership(responseLoginAdmin, membershipId.Id, userId);
            //int userMembershipId = AppDbContext.UserMemberships.GetLastUsermembershipId(email);
            //MembershipRequest.ActivateUserMembership(responseLoginAdmin, userMembershipId, userId);
            const int conversionSystem = ConversionSystem.METRIC;
            #endregion

            #region Add Progress as User
            for (int i = 0; i < 60; i++)
            {
                ProgressRequest.AddProgress(responseLoginUser, conversionSystem);
                AppDbContext.Progress.UpdateUserProgressDate(userId);
            }
            #endregion
            #endregion
            //List<DB.WorkoutExercises> workoutExercises = AppDbContext.WorkoutExercises.GetAllWorkoutExercises();
            //var result = workoutExercises.GroupBy(entry => new { entry.WorkoutId, entry.Series });

            //List<DB.WorkoutExercises> inconsistentEntries = new List<DB.WorkoutExercises>();

            //foreach (var group in result)
            //{
            //    // Check if all GroupId values are the same within the group
            //    bool sameGroupId = group.All(entry => entry.WorkoutExerciseGroupId == group.First().WorkoutExerciseGroupId);

            //    if (!sameGroupId)
            //    {
            //        // Add the inconsistent entries to the list
            //        inconsistentEntries.AddRange(group);
            //    }
            //}

            //// Save inconsistent entries to a CSV file
            //SaveToCsv(inconsistentEntries, Path.Combine(Browser.RootPath() + "inconsistent_entries.csv"));

            //Console.WriteLine("Inconsistent entries saved to inconsistent_entries.csv");

            //static void SaveToCsv(List<DB.WorkoutExercises> data, string filePath)
            //{
            //    using StreamWriter writer = new StreamWriter(filePath);
            //    // Write header
            //    writer.WriteLine("Id,WorkoutId,Series,WeekNumber,WorkoutExerciseGroupId,CreationDate");

            //    // Write data
            //    foreach (var entry in data)
            //    {
            //        writer.WriteLine($"{entry.Id},{entry.WorkoutId},{entry.Series},{entry.Week},{entry.WorkoutExerciseGroupId},{entry.CreationDate}");
            //    }
            //}
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
