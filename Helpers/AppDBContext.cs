using MCMAutomation.APIHelpers;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using NUnit.Framework;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using static Chilkat.Http;

namespace MCMAutomation.Helpers
{

    public class AppDbContext
    {
        public class Exercises
        {
            public static List<JsonUserExercises> GetUserExercisesList(string userEmail, string membershipName)
            {
                var list = new List<JsonUserExercises>();

                // SQL запит для вибірки даних
                string query = "SELECT *" +
                               "FROM [JsonUserExercises] WHERE UserId in " +
                                     "(SELECT id FROM[dbo].[AspNetUsers] WHERE email = @userEmail) and WorkoutExerciseId in" +
                                        "(SELECT Id FROM WorkoutExercises WHERE WorkoutId in " +
                                            "(SELECT Id FROM Workouts WHERE ProgramId in " +
                                                "(SELECT Id FROM Programs WHERE MembershipId in " +
                                                    "(SELECT Id FROM Memberships WHERE Name = @membershipName)" +
                                                ")" +
                                            ")" +
                                        ")";
                try
                {
                    using SqlConnection connection = new(DB.GET_CONNECTION_STRING);
                    using SqlCommand command = new(query, connection);
                    connection.Open();

                    // Параметризований запит з одним параметром
                    command.Parameters.AddWithValue("@userEmail", DbType.String).Value = userEmail;
                    command.Parameters.AddWithValue("@membershipName", DbType.String).Value = membershipName;

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new JsonUserExercises();
                        row.Id = reader.GetValue(0);
                        row.SetDescription = reader.GetValue(1);
                        row.WorkoutExerciseId = reader.GetValue(2);
                        row.UserId = reader.GetValue(3);
                        row.IsDone = reader.GetValue(4);
                        row.CreationDate = reader.GetValue(5);
                        row.IsDeleted = reader.GetValue(6);
                        row.UpdatedDate = reader.GetValue(7);


                        list.Add(row);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                return list;
            }
            public static List<DB.Exercises> GetExercisesData()
            {
                var list = new List<DB.Exercises>();
                string query = "SELECT * " +
                               "FROM [Exercises] WHERE IsDeleted=0";

                try
                {
                    using SqlConnection connection = new(DB.GET_CONNECTION_STRING);
                    using SqlCommand command = new(query, connection);
                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.Exercises();
                        row.Id = reader.GetInt32(0);
                        row.Name = reader.GetString(1);
                        row.CreationDate = reader.GetDateTime(2);
                        row.IsDeleted = reader.GetBoolean(3);
                        row.VideoURL = reader.GetString(4);
                        row.TempoBold = reader.GetInt32(5);
                        list.Add(row);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                return list;
            }
            public static List<string> GetExerciseStatus()
            {
                var list = new List<string>();
                string query = "SELECT TOP(1)*" +
                               "FROM [Exercises] " +
                               "ORDER BY CreationDate DESC";
                try
                {
                    using SqlConnection connection = new(DB.GET_CONNECTION_STRING);
                    using SqlCommand command = new(query, connection);
                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var str = reader.GetValue(3).ToString();
                        list.Add(str);

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {
                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                return list;
            }
            public static void DeleteExercises(string exercise)
            {
                string query = "delete from DeletedUserExercises where WorkoutExerciseId in " +
                                                "(select id from WorkoutExercises where ExerciseId in " +
                                                "(select id from exercises where name = @exercise));\r\n\r\n" +
                               "delete from WorkoutExercises where ExerciseId in " +
                                                "(select id from exercises where name = @exercise);\r\n\r\n" +
                               "delete from ExerciseInRelatedGroups where ExerciseId in " +
                                                "(select id from exercises where name = @exercise);\r\n\r\n" +
                               "delete from exercises where name = @exercise;";
                try
                {
                    using SqlConnection connection = new(DB.GET_CONNECTION_STRING);
                    using SqlCommand command = new(query, connection);
                    connection.Open();
                    command.Parameters.AddWithValue("@exercise", DbType.String).Value = exercise;

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public class Workouts
        {
            public static List<DB.Workouts> GetLastWorkoutsData(int workoutCount)
            {
                var list = new List<DB.Workouts>();
                string query = $"SELECT TOP(@count) *" +
                                "FROM [Workouts] WHERE IsDeleted=0" +
                                "ORDER BY CreationDate DESC";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    command.Parameters.AddWithValue("@count", DbType.Int32).Value = workoutCount;
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var row = new DB.Workouts();
                            row.Id = reader.GetInt32(0);
                            row.Name = reader.GetString(1);
                            row.WeekDay = reader.GetInt32(2);
                            row.ProgramId = reader.GetInt32(3);
                            row.CreationDate = reader.GetDateTime(4);
                            row.IsDeleted = reader.GetBoolean(5);
                            row.Type = reader.GetInt32(6);

                            list.Add(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return list;
            }
            public static List<DB.CopyMembershipPrograms> GetMembershipProgramWorkoutData()
            {
                var list = new List<DB.CopyMembershipPrograms>();
                string query = "Select m.Name, p.Name, w.Name from Programs p " +
                               "inner join Memberships m on m.Id = p.MembershipId " +
                               "inner join Workouts w on w.ProgramId = p.Id " +
                               "inner join WorkoutExercises we on we.WorkoutId = w.Id " +
                               "where p.NumberOfWeeks = 4 " +
                               "and p.IsDeleted = 0 " +
                               "and m.IsDeleted = 0 " +
                               "and w.IsDeleted = 0 " +
                               "Group by m.Name, p.Name, w.Name " +
                               "Having count(we.Id)>0;";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var row = new DB.CopyMembershipPrograms();
                            row.MembershipName = reader.GetString(0);
                            row.ProgramName = reader.GetString(1);
                            row.WorkoutName = reader.GetString(2);

                            list.Add(row);

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                return list;
            }
        }

        public class Memberships
        {
            public static DB.Memberships? GetLastMembership()
            {
                var row = new DB.Memberships();
                string query = "SELECT TOP(1)*" +
                                             "FROM [Memberships] WHERE IsDeleted=0" +
                                             "ORDER BY CreationDate DESC";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        row.Id = reader.GetInt32(0);
                        row.Name = reader.GetString(1);
                        row.Description = null;
                        row.StartDate = null;
                        row.EndDate = null;
                        row.URL = reader.GetString(5);
                        row.Price = reader.GetDecimal(6);
                        row.CreationDate = reader.GetDateTime(7);
                        row.IsDeleted = reader.GetBoolean(8);
                        row.IsCustom = reader.GetBoolean(9);
                        row.ForPurchase = reader.GetBoolean(10);
                        row.AccessWeekLength = null;
                        row.RelatedMembershipGroupId = null;
                        row.Gender = reader.GetInt32(13);
                        row.PromotionalPopupId = null;
                        row.Type = reader.GetInt32(15);
                        row.SKU = reader.GetString(16);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return row;
            }
            public static DB.Memberships GetActiveMembershipsNameAndSkuByEmail(string email)
            {

                var list = new DB.Memberships();
                string query = "SELECT Name, SKU " +
                               "FROM Memberships WHERE Id in (Select MembershipId from UserMemberships " +
                                                              "WHERE UserId in " +
                                                              "(select Id from [AspNetUsers] " +
                                                              "where Email like @email) and isDeleted = 0 and active = 1)";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    command.Parameters.AddWithValue("@email", DbType.String).Value = email;
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list = new DB.Memberships()
                        {
                            Name = reader.GetString(0),
                            SKU = reader.GetString(1)
                        };
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                return list;
            }
            public static DB.Memberships GetActiveMembershipNameBySKU(string SKU)
            {

                var membership = new DB.Memberships();
                string query = "SELECT TOP(1) * " +
                               "FROM Memberships " +
                               "WHERE SKU LIKE @SKU AND IsDeleted = 0 " +
                               "ORDER BY CreationDate DESC";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    command.Parameters.AddWithValue("@SKU", DbType.String).Value = String.Concat(SKU + "%");
                    db.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        membership = new DB.Memberships();
                        membership.Id = reader.GetInt32(0);
                        membership.Name = reader.GetString(1);
                        membership.Description = null;
                        membership.StartDate = null;
                        membership.EndDate = null;
                        membership.URL = reader.GetString(5);
                        membership.Price = reader.GetDecimal(6);
                        membership.CreationDate = reader.GetDateTime(7);
                        membership.IsDeleted = reader.GetBoolean(8);
                        membership.IsCustom = reader.GetBoolean(9);
                        membership.ForPurchase = reader.GetBoolean(10);
                        membership.AccessWeekLength = reader.GetInt32(11);
                        membership.RelatedMembershipGroupId = null;
                        membership.Gender = reader.GetInt32(13);
                        membership.PromotionalPopupId = null;
                        membership.Type = reader.GetInt32(15);
                        membership.SKU = reader.GetString(16);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                return membership;
            }
            public static DB.Memberships GetLastMembershipByType(string type)
            {
                var row = new DB.Memberships();
                if (type.ToLower() == MembershipType.PRODUCT.ToLower())
                {
                    try
                    {
                        SqlConnection db = new(DB.GET_CONNECTION_STRING);
                        SqlCommand command = new(String.Concat("Select top(1) * from memberships " +
                                                                "where Type = 0 And Isdeleted = 0 " +
                                                                "order by creationDate desc"), db);
                        db.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            row.Id = reader.GetInt32(0);
                            row.SKU = reader.GetString(1);
                            row.Name = reader.GetString(2);
                            row.Description = reader.GetString(3);
                            row.StartDate = null;
                            row.EndDate = null;
                            row.URL = reader.GetString(6);
                            row.Price = reader.GetDecimal(7);
                            row.CreationDate = reader.GetDateTime(8);
                            row.IsDeleted = reader.GetBoolean(9);
                            row.IsCustom = reader.GetBoolean(10);
                            row.ForPurchase = reader.GetBoolean(11);
                            row.AccessWeekLength = reader.GetInt32(12);
                            row.RelatedMembershipGroupId = null;
                            row.Gender = reader.GetInt32(14);
                            row.PromotionalPopupId = null;
                            row.Type = reader.GetInt32(16);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                    }
                    finally
                    {

                        // Забезпечуємо вивільнення ресурсів
                        SqlConnection.ClearAllPools();
                    }
                }
                else if (type.ToLower() == MembershipType.SUBSCRIPTION.ToLower())
                {
                    try
                    {

                        SqlConnection db = new(DB.GET_CONNECTION_STRING);
                        SqlCommand command = new(String.Concat("Select top(1) * from memberships " +
                                                                "where Type = 1 And Isdeleted = 0 " +
                                                                "order by creationDate desc"), db);
                        db.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            row.Id = reader.GetInt32(0);
                            row.SKU = reader.GetString(1);
                            row.Name = reader.GetString(2);
                            row.Description = null;
                            row.StartDate = null;
                            row.EndDate = null;
                            row.URL = reader.GetString(6);
                            row.Price = reader.GetDecimal(7);
                            row.CreationDate = reader.GetDateTime(8);
                            row.IsDeleted = reader.GetBoolean(9);
                            row.IsCustom = reader.GetBoolean(10);
                            row.ForPurchase = reader.GetBoolean(11);
                            row.AccessWeekLength = null;
                            row.RelatedMembershipGroupId = null;
                            row.Gender = reader.GetInt32(14);
                            row.PromotionalPopupId = null;
                            row.Type = reader.GetInt32(16);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                    }
                    finally
                    {

                        // Забезпечуємо вивільнення ресурсів
                        SqlConnection.ClearAllPools();
                    }
                }
                else if (type.ToLower() == MembershipType.CUSTOM.ToLower())
                {
                    try
                    {
                        SqlConnection db = new(DB.GET_CONNECTION_STRING);
                        SqlCommand command = new(String.Concat("Select top(1) * from memberships " +
                                                               "where Type = 0 and SKU is null And Isdeleted = 0 " +
                                                               "order by creationDate desc"), db);
                        db.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            row.Id = reader.GetInt32(0);
                            row.Name = reader.GetString(1);
                            row.Description = null;
                            row.StartDate = null;
                            row.EndDate = null;
                            row.URL = null;
                            row.Price = reader.GetDecimal(6);
                            row.CreationDate = reader.GetDateTime(7);
                            row.IsDeleted = reader.GetBoolean(8);
                            row.IsCustom = reader.GetBoolean(9);
                            row.ForPurchase = reader.GetBoolean(10);
                            row.AccessWeekLength = reader.GetInt32(11);
                            row.RelatedMembershipGroupId = null;
                            row.Gender = reader.GetInt32(13);
                            row.PromotionalPopupId = null;
                            row.Type = reader.GetInt32(15);
                            row.SKU = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                    }
                    finally
                    {

                        // Забезпечуємо вивільнення ресурсів
                        SqlConnection.ClearAllPools();
                    }
                }
                return row;
            }
            public static List<DB.Memberships> GetListOfLastMembershipsByType()
            {
                var list = new List<DB.Memberships>();
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(String.Concat("Select top(1) * from memberships " +
                                                          "where Type = 0 and SKU is not null And Isdeleted = 0 " +
                                                          "order by creationDate desc"), db);
                    db.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.Memberships();
                        row.Id = reader.GetInt32(0);
                        row.SKU = reader.GetString(1);
                        row.Name = reader.GetString(2);
                        row.Description = reader.GetString(3);
                        row.StartDate = null;
                        row.EndDate = null;
                        row.URL = reader.GetString(6);
                        row.Price = reader.GetDecimal(7);
                        row.CreationDate = reader.GetDateTime(8);
                        row.IsDeleted = reader.GetBoolean(9);
                        row.IsCustom = reader.GetBoolean(10);
                        row.ForPurchase = reader.GetBoolean(11);
                        row.AccessWeekLength = reader.GetInt32(12);
                        row.RelatedMembershipGroupId = null;
                        row.Gender = reader.GetInt32(14);
                        row.PromotionalPopupId = null;
                        row.Type = reader.GetInt32(16);
                        list.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {
                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(String.Concat("Select top(1) * from memberships " +
                                                            "where Type = 1 And Isdeleted = 0 " +
                                                            "order by creationDate desc"), db);
                    db.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.Memberships();
                        row.Id = reader.GetInt32(0);
                        row.SKU = reader.GetString(1);
                        row.Name = reader.GetString(2);
                        row.Description = null;
                        row.StartDate = null;
                        row.EndDate = null;
                        row.URL = reader.GetString(6);
                        row.Price = reader.GetDecimal(7);
                        row.CreationDate = reader.GetDateTime(8);
                        row.IsDeleted = reader.GetBoolean(9);
                        row.IsCustom = reader.GetBoolean(10);
                        row.ForPurchase = reader.GetBoolean(11);
                        row.AccessWeekLength = null;
                        row.RelatedMembershipGroupId = null;
                        row.Gender = reader.GetInt32(14);
                        row.PromotionalPopupId = null;
                        row.Type = reader.GetInt32(16);
                        list.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(String.Concat("Select top(1) * from memberships " +
                                                           "where Type = 0 and SKU is null And Isdeleted = 0 " +
                                                           "order by creationDate desc"), db);
                    db.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.Memberships();
                        row.Id = reader.GetInt32(0);
                        row.SKU = null;
                        row.Name = reader.GetString(2);
                        row.Description = null;
                        row.StartDate = null;
                        row.EndDate = null;
                        row.URL = null;
                        row.Price = reader.GetDecimal(7);
                        row.CreationDate = reader.GetDateTime(8);
                        row.IsDeleted = reader.GetBoolean(9);
                        row.IsCustom = reader.GetBoolean(10);
                        row.ForPurchase = reader.GetBoolean(11);
                        row.AccessWeekLength = reader.GetInt32(12);
                        row.RelatedMembershipGroupId = null;
                        row.Gender = reader.GetInt32(14);
                        row.PromotionalPopupId = null;
                        row.Type = reader.GetInt32(16);
                        list.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return list;
            }
            public static void DeleteMembership(string membership)
            {
                string query = "delete from [JsonUserExercises]\r\n  where WorkoutExerciseId in \r\n\t\t\t\t\t" +
                                                "(select Id from WorkoutExercises where WorkoutId in \r\n\t\t\t\t\t\t" +
                                                "(select Id from Workouts where ProgramId in \r\n\t\t\t\t\t\t\t" +
                                                "(select Id from Programs where MembershipId in " +
                                                "(Select Id From Memberships where Name like @membership))))\r\n\r\n" +
                                             "Delete from UserRelatedExercises\r\nwhere WorkoutExerciseId in \r\n\t\t\t\t\t" +
                                                "(select Id from WorkoutExercises where WorkoutId in \r\n\t\t\t\t\t\t" +
                                                "(select Id from Workouts where ProgramId in \r\n\t\t\t\t\t\t\t" +
                                                "(select Id from Programs where MembershipId in " +
                                                "(Select Id From Memberships where Name like @membership))))\r\n\r\n" +
                                             "Delete from DeletedUserExercises\r\nwhere WorkoutExerciseId in \r\n\t\t\t\t\t" +
                                                "(select Id from WorkoutExercises where WorkoutId in \r\n\t\t\t\t\t\t" +
                                                "(select Id from Workouts where ProgramId in \r\n\t\t\t\t\t\t\t" +
                                                "(select Id from Programs where MembershipId in " +
                                                "(Select Id From Memberships where Name like @membership))))\r\n\r\n" +
                                             "Delete from WorkoutExercises\r\n  where WorkoutId in " +
                                                "(select Id from Workouts where ProgramId in " +
                                                "(select Id from Programs where MembershipId in " +
                                                "(Select Id From Memberships where Name like @membership)))\r\n\r\n" +
                                             "Delete from CompletedWorkouts\r\nwhere workoutid in " +
                                                "(select id from Workouts\r\n  where ProgramId in " +
                                                "(select Id from Programs where MembershipId in " +
                                                "(Select Id From Memberships where Name like @membership)))\r\n\r\n" +
                                             "delete from UserWorkouts\r\n where ProgramId in " +
                                                "(select Id from Programs where MembershipId in \r\n\t\t\t\t\t\t" +
                                                "(Select Id From Memberships where Name like @membership))" +
                                             "Delete from Workouts\r\n  where ProgramId in " +
                                                "(select Id from Programs where MembershipId in " +
                                                "(Select Id From Memberships where Name like @membership))\r\n  \r\n" +
                                             "delete from Media\r\nwhere ProgramId in " +
                                                "(select id from Programs\r\n where MembershipId in " +
                                                "(Select Id From Memberships where Name like @membership))\r\n\r\n" +
                                             "delete from Programs\r\n where MembershipId in " +
                                                "(Select Id From Memberships where Name like @membership)\r\n\r\n" +
                                             "Update UserMemberships set MembershipId = 66, UserId = Null \r\n  where MembershipId in " +
                                                "(Select Id From Memberships where Name like @membership)\r\n\r\n" +
                                             "delete from Media\r\n where MembershipId in " +
                                                "(Select Id From Memberships where Name like @membership)\r\n\r\n" +
                                             "Delete from RelatedMembershipGroups\r\n where ParentMembershipId in " +
                                                "(select id From Memberships\r\n  where Name like @membership)\r\n\r\n" +
                                             "delete from MultiLevelMembershipGroups\r\nwhere ParentMembershipId in " +
                                                "(select id From Memberships\r\n  where Name like @membership)\r\n\r\n" +
                                             "delete from PagesInMemberships\r\n where MembershipId in " +
                                                "(Select Id From Memberships where Name like @membership)\r\n\r\n" +
                                             "delete From Memberships\r\n  where Name like @membership";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    command.Parameters.AddWithValue("@membership", DbType.String).Value = membership;
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

            }
        }

        public class Programs
        {
            public static List<DB.Programs> GetLastPrograms(int programsCount)
            {
                var list = new List<DB.Programs>();
                
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new($"SELECT TOP({programsCount}) * " +
                                             "FROM [Programs] WHERE IsDeleted=0 " +
                                             "ORDER BY CreationDate DESC", db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.Programs();
                        row.Id = reader.GetInt32(0);
                        row.MembershipId = reader.GetInt32(1);
                        row.Name = reader.GetString(2);
                        row.NumberOfWeeks = reader.GetInt32(3);
                        row.CreationDate = reader.GetDateTime(4);
                        row.IsDeleted = reader.GetBoolean(5);
                        row.Steps = reader.GetString(6);
                        row.AvailableDate = null;
                        row.NextProgramId = null;
                        row.ExpirationDate = null;
                        row.Type = reader.GetInt32(10);
                        list.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return list;
            }
        }

        public class UserMemberships
        {
            public static int GetLastUsermembershipId(string userEmail)
            {
                int str = 0;
                string query = "Select top(1) id from UserMemberships\r\n" +
                                             "where UserId in (" +
                                             "Select id FROM AspNetUsers where Email = @email)\r\n" +
                                             "order by CreationDate desc";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    command.Parameters.AddWithValue("@email", DbType.String).Value = userEmail;
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        str = reader.GetInt32(0);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return str;
            }

            public static List<int> GetTop2UsermembershipId(string userEmail)
            {
                List<int> str = new List<int>();
                string query = "Select top(2) id from UserMemberships\r\n" +
                               "where UserId in (Select id FROM AspNetUsers where Email = @email)\r\n" +
                               "order by CreationDate ASC";
                try 
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    command.Parameters.AddWithValue("@email", DbType.String).Value = userEmail;
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                        {
                            str.Add(reader.GetInt32(0));
                        }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return str;
            }

            public static void UpdateTop2UsermembershipToExpire(int usermembershipId)
            {
                List<int> str = new List<int>();
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new("Update UserMemberships set " +
                                             "StartOn = DateAdd(MM, -3, StartOn) " +
                                             "where Id = @usermembershipId and isDeleted = 0", db);
                    command.Parameters.AddWithValue("@usermembershipId", DbType.Int32).Value = usermembershipId;
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
            }

            public static void UpdateTop2UsermembershipToComingSoon(int usermembershipId)
            {
                List<int> str = new List<int>();
                string query = "Update UserMemberships set StartOn = DateAdd(MM, 5, StartOn) " +
                               "where Id = @usermembershipId and isDeleted = 0";
                try 
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    command.Parameters.AddWithValue("@usermembershipId", DbType.Int32).Value = usermembershipId;
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                        {
                            continue;
                        }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public class Progress
        {
            public static void UpdateUserProgressDate(string userId)
            {
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new("UPDATE [Progress] set " +
                                             "CreationDate = DateAdd(DD, -7, CreationDate) where UserId = @userId", db);
                    command.Parameters.AddWithValue("@userId", DbType.String).Value = userId;
                    db.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
            }
        }

        public class User
        {
            public static DB.AspNetUsers GetUserData(string userEmail)
            {
                var user = new DB.AspNetUsers();
                string query = "SELECT TOP(1) *" +
                               "FROM [AspNetUsers] where email like @userEmail " +
                               "ORDER BY DateTime DESC";

                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    command.Parameters.AddWithValue("@userEmail", DbType.String).Value = userEmail;
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        user.Id = reader.GetString(0);
                        user.FirstName = reader.GetString(1);
                        user.LastName = reader.GetString(2);
                        user.Email = reader.GetString(3);
                        user.ConversionSystem = reader.GetInt32(4);
                        user.Gender = reader.GetInt32(5);
                        user.Birthdate = reader.GetDateTime(6);
                        user.Weight = reader.GetDecimal(7);
                        user.Height = reader.GetInt32(8);
                        user.ActivityLevel = reader.GetInt32(9);
                        user.Bodyfat = reader.GetInt32(10);
                        user.Calories = reader.GetInt32(11);
                        user.Active = reader.GetBoolean(12);
                        user.DateTime = reader.GetDateTime(13);
                        user.UserName = reader.GetString(14);
                        user.NormalizedUserName = reader.GetString(15);
                        user.NormalizedEmail = reader.GetString(16);
                        user.EmailConfirmed = reader.GetBoolean(17);
                        user.PasswordHash = reader.GetString(18);
                        user.SecurityStamp = reader.GetString(19);
                        user.ConcurrencyStamp = reader.GetString(20);
                        user.PhoneNumber = null;
                        user.PhoneNumberConfirmed = reader.GetBoolean(22);
                        user.TwoFactorEnabled = reader.GetBoolean(23);
                        user.LockoutEnd = null;
                        user.LockoutEnabled = reader.GetBoolean(25);
                        user.AccessFailedCount = reader.GetInt32(26);
                        user.IsDeleted = reader.GetBoolean(27);
                        user.IsMainAdmin = reader.GetBoolean(28);
                        user.LastGeneratedIdentityToken = null;
                        user.Carbs = reader.GetInt32(30);
                        user.Fats = reader.GetInt32(31);
                        user.MaintenanceCalories = reader.GetInt32(32);
                        user.Protein = reader.GetInt32(33);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return user;
            }

            public static DB.AspNetUsers GetLastUser()
            {
                var user = new DB.AspNetUsers();
                string query = "SELECT TOP(1) *" +
                               "FROM [AspNetUsers] " +
                               "ORDER BY DateTime DESC";

                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        user.Id = reader.GetString(0);
                        user.FirstName = reader.GetString(1);
                        user.LastName = reader.GetString(2);
                        user.Email = reader.GetString(3);
                        user.ConversionSystem = reader.GetInt32(4);
                        user.Gender = reader.GetInt32(5);
                        user.Birthdate = reader.GetDateTime(6);
                        user.Weight = reader.GetDecimal(7);
                        user.Height = reader.GetInt32(8);
                        user.ActivityLevel = reader.GetInt32(9);
                        user.Bodyfat = reader.GetInt32(10);
                        user.Calories = reader.GetInt32(11);
                        user.Active = reader.GetBoolean(12);
                        user.DateTime = reader.GetDateTime(13);
                        user.UserName = reader.GetString(14);
                        user.NormalizedUserName = reader.GetString(15);
                        user.NormalizedEmail = reader.GetString(16);
                        user.EmailConfirmed = reader.GetBoolean(17);
                        user.PasswordHash = reader.GetString(18);
                        user.SecurityStamp = reader.GetString(19);
                        user.ConcurrencyStamp = reader.GetString(20);
                        user.PhoneNumber = null;
                        user.PhoneNumberConfirmed = reader.GetBoolean(22);
                        user.TwoFactorEnabled = reader.GetBoolean(23);
                        user.LockoutEnd = null;
                        user.LockoutEnabled = reader.GetBoolean(25);
                        user.AccessFailedCount = reader.GetInt32(26);
                        user.IsDeleted = reader.GetBoolean(27);
                        user.IsMainAdmin = reader.GetBoolean(28);
                        user.LastGeneratedIdentityToken = null;
                        user.Carbs = reader.GetInt32(30);
                        user.Fats = reader.GetInt32(31);
                        user.MaintenanceCalories = reader.GetInt32(32);
                        user.Protein = reader.GetInt32(33);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return user;
            }

            public static void DeleteUser(string email)
            {
                string query = String.Concat("delete from DeletedUserExercises where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from UserRelatedExercises where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from CompletedWorkouts where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from WorkoutUserNotes where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from [JsonUserExercises] where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "Update UserMemberships set isdeleted = 1, UserId=null where UserId in (" +
                                                                        "SELECT Id FROM [AspNetUsers] where Email like @email)\r\n" +
                                             "delete from Media where ProgressId in (" +
                                                                        "select id from Progress where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email))\r\n" +
                                             "delete from Media where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from Progress where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from UserExercises where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from GlobalDisplayedPopups where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from Logins where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from AspNetUsers where email like @email");
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    command.Parameters.AddWithValue("@email", DbType.String).Value = email;
                    command.Parameters.AddWithValue("@userId", DbType.String).Value = "c5c91cc8-dd78-4bba-aab6-471830c3d28e";
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {
                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
            }

            public static List<DB.ProgressDaily> GetProgressDailyByUserId(string id)
            {
                var list = new List<DB.ProgressDaily>();
                string query = "SELECT *\r\n  " +
                               "FROM [dbo].[DailyProgress]\r\n  " +
                               "where UserId = @UserId " +
                               "order by Date";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    command.Parameters.AddWithValue("@UserId", DbType.String).Value = id;
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var progressRow = new DB.ProgressDaily();
                        progressRow.Id = reader.GetInt32(0);
                        progressRow.Date = reader.GetDateTime(1);
                        progressRow.Weight = reader.GetDecimal(2);
                        progressRow.UserId = reader.GetString(3);
                        progressRow.CreationDate = reader.GetDateTime(4);
                        progressRow.IsDeleted = reader.GetBoolean(5);
                        list.Add(progressRow);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Помилка: {0}\r\n{1}", ex.Message, ex.StackTrace);
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                return list;
            }
        }

    }
}
