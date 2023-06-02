using MCMAutomation.APIHelpers;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using NUnit.Framework;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using static Chilkat.Http;

namespace MCMAutomation.Helpers
{

    public class AppDbContext
    {
        private static T GetValueOrDefault<T>(SqlDataReader reader, int index, T defaultValue = default(T))
        {
            if (!reader.IsDBNull(index))
            {
                return (T)reader.GetValue(index);
            }
            else
            {
                return defaultValue;
            }
        }
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
                        row.Id = GetValueOrDefault<object>(reader, 0);
                        row.SetDescription = GetValueOrDefault<string>(reader, 1);
                        row.WorkoutExerciseId = GetValueOrDefault<long>(reader, 2);
                        row.UserId = GetValueOrDefault<string>(reader, 3);
                        row.IsDone = GetValueOrDefault<bool>(reader, 4);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 6);
                        row.UpdatedDate = GetValueOrDefault<DateTime>(reader, 7);


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
                        row.Id = GetValueOrDefault<int>(reader, 0);
                        row.Name = GetValueOrDefault<string>(reader, 1);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 2);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 3);
                        row.VideoURL = GetValueOrDefault<string>(reader, 4);
                        row.TempoBold = GetValueOrDefault<int>(reader, 5);
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

            public static DB.Exercises GetLastExerciseData()
            {

                var row = new DB.Exercises();
                string query = "SELECT TOP(1) * " +
                               "FROM [Exercises] WHERE IsDeleted=0 " +
                               "ORDER BY CreationDate DESC";

                try
                {
                    using SqlConnection connection = new(DB.GET_CONNECTION_STRING);
                    using SqlCommand command = new(query, connection);
                    connection.Open();

                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        row.Id = GetValueOrDefault<int>(reader, 0);
                        row.Name = GetValueOrDefault<string>(reader, 1);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 2);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 3);
                        row.VideoURL = GetValueOrDefault<string>(reader, 4);
                        row.TempoBold = GetValueOrDefault<int>(reader, 5);
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
                        var str = GetValueOrDefault<string>(reader, 3);
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
            public static List<DB.Workouts> GetLastWorkoutsData(List<DB.Programs> programs)
            {
                WaitUntil.WaitSomeInterval(5000);
                var list = new List<DB.Workouts>();
                string query = $"SELECT * " +
                               $"FROM [Workouts] WHERE IsDeleted=0 and ProgramId BETWEEN {programs.LastOrDefault().Id} and {programs.FirstOrDefault().Id} " +
                               "ORDER BY CreationDate DESC";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    command.Parameters.AddWithValue("@count", DbType.Int32).Value = programs.Count;
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var row = new DB.Workouts();
                            row.Id = GetValueOrDefault<int>(reader, 0);
                            row.Name = GetValueOrDefault<string>(reader, 1);
                            row.WeekDay = GetValueOrDefault<int>(reader, 2);
                            row.ProgramId = GetValueOrDefault<int>(reader, 3);
                            row.CreationDate = GetValueOrDefault<DateTime>(reader, 4);
                            row.IsDeleted = GetValueOrDefault<bool>(reader, 5);
                            row.Type = GetValueOrDefault<int>(reader, 6);

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
                            row.MembershipName = GetValueOrDefault<string>(reader, 0);
                            row.ProgramName = GetValueOrDefault<string>(reader, 1);
                            row.WorkoutName = GetValueOrDefault<string>(reader, 2);

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
                WaitUntil.WaitSomeInterval(5000);
                var row = new DB.Memberships();
                string query = "SELECT TOP(1)*" +
                                             "FROM [Memberships] " +
                                             "ORDER BY CreationDate DESC";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        row.Id = GetValueOrDefault<Int32>(reader, 0);
                        row.SKU = GetValueOrDefault<string>(reader, 1);
                        row.Name = GetValueOrDefault<string>(reader, 2);
                        row.Description = GetValueOrDefault<string>(reader, 3);
                        row.StartDate = GetValueOrDefault<DateTime>(reader, 4);
                        row.EndDate = GetValueOrDefault<DateTime>(reader, 5);
                        row.URL = GetValueOrDefault<string>(reader, 6);
                        row.Price = GetValueOrDefault<decimal>(reader, 7);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 8);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 9);
                        row.IsCustom = GetValueOrDefault<bool>(reader, 10);
                        row.ForPurchase = GetValueOrDefault<bool>(reader, 11);
                        row.AccessWeekLength = GetValueOrDefault<int>(reader, 12);
                        row.RelatedMembershipGroupId = GetValueOrDefault<int>(reader, 13);
                        row.Gender = GetValueOrDefault<int>(reader, 14);
                        row.PromotionalPopupId = GetValueOrDefault<int>(reader, 15);
                        row.Type = GetValueOrDefault<int>(reader, 16);
                        
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
                string query = "SELECT *" +
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
                            Id = GetValueOrDefault<int>(reader, 0),
                            SKU = GetValueOrDefault<string>(reader, 1),
                            Name = GetValueOrDefault<string>(reader, 2),
                            Description = GetValueOrDefault<string>(reader, 3),
                            StartDate = GetValueOrDefault<DateTime>(reader, 4),
                            EndDate = GetValueOrDefault<DateTime>(reader, 5),
                            URL = GetValueOrDefault<string>(reader, 6),
                            Price = GetValueOrDefault<decimal>(reader, 7),
                            CreationDate = GetValueOrDefault<DateTime>(reader, 8),
                            IsDeleted = GetValueOrDefault<bool>(reader, 9),
                            IsCustom = GetValueOrDefault<bool>(reader, 10),
                            ForPurchase = GetValueOrDefault<bool>(reader, 11),
                            AccessWeekLength = GetValueOrDefault<int>(reader, 12),
                            RelatedMembershipGroupId = GetValueOrDefault<int>(reader, 13),
                            Gender = GetValueOrDefault<int>(reader, 14),
                            PromotionalPopupId = GetValueOrDefault<int>(reader, 15),
                            Type = GetValueOrDefault<int>(reader, 16)
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
                WaitUntil.WaitSomeInterval();
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
                        membership.Id = GetValueOrDefault<int>(reader, 0);
                        membership.SKU = GetValueOrDefault<string>(reader, 1);
                        membership.Name = GetValueOrDefault<string>(reader, 2);

                        membership.Description = GetValueOrDefault<string>(reader,3);
                        membership.StartDate = GetValueOrDefault<DateTime>(reader,4);
                        membership.EndDate = GetValueOrDefault<DateTime>(reader,5);
                        membership.URL = GetValueOrDefault<string>(reader, 6);
                        membership.Price = GetValueOrDefault<decimal>(reader, 7);
                        membership.CreationDate = GetValueOrDefault<DateTime>(reader, 8);
                        membership.IsDeleted = GetValueOrDefault<bool>(reader, 9);
                        membership.IsCustom = GetValueOrDefault<bool>(reader, 10);
                        membership.ForPurchase = GetValueOrDefault<bool>(reader, 11);
                        membership.AccessWeekLength = GetValueOrDefault<int>(reader, 12);
                        membership.RelatedMembershipGroupId = GetValueOrDefault<int>(reader,13);
                        membership.Gender = GetValueOrDefault<int>(reader, 14);
                        membership.PromotionalPopupId = GetValueOrDefault<int>(reader,15);
                        membership.Type = GetValueOrDefault<int>(reader, 16);
                        
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
                            row.Name = reader.GetString(1);
                            row.Description = reader.GetString(2);
                            row.StartDate = null;
                            row.EndDate = null;
                            row.URL = reader.GetString(5);
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
                        row.Name = reader.GetString(1);
                        row.Description = reader.GetString(2);
                        row.StartDate = null;
                        row.EndDate = null;
                        row.URL = reader.GetString(5);
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
                        row.SKU = reader.GetString(16);
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
                                             "Delete from PushNotifications\r\n  " +
                                                "where Id in (Select Id from PushNotificationInMemberships\r\n  " +
                                                "where MembershipId in (Select Id From Memberships where Name like @membership));\r\n\r\n" +
                                             "Delete from PushNotificationInMemberships\r\n  " +
                                                "where MembershipId in (Select Id From Memberships where Name like @membership);" +
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
                                             //"delete from MultiLevelMembershipGroups\r\nwhere ParentMembershipId in " +
                                             //   "(select id From Memberships\r\n  where Name like @membership)\r\n\r\n" +
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
                    WaitUntil.WaitSomeInterval(5000);
                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

            }

            public class Insert
            {
                public static void InsertMembership(int lastMemberId, string membershipSKU, bool eightWeeks)
                {
                    string query;
                    if(eightWeeks == true)
                    {
                        query = "SET IDENTITY_INSERT [dbo].[Memberships] ON\r\n" +
                        "INSERT [Memberships] (Id, SKU, Name, Description, StartDate, EndDate, URL, Price, CreationDate, IsDeleted, IsCustom, ForPurchase, AccessWeekLength, RelatedMembershipGroupId, Gender, PromotionalPopupId, Type)\r\n" +
                        $"VALUES (\'{lastMemberId + 1}\', \'{membershipSKU}\', \'{"00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh-mm-ss")}\', \'{Lorem.ParagraphByChars(300)}\', \'{DateTime.Now.Date}\', \'{DateTime.Now.AddHours(1644).Date}\', \'{$"https://mcmstaging-ui.azurewebsites.net/programs/all"}\', \'{100}\', \'{DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")}\', \'{false}\', \'{false}\', \'{true}\', \'{0}\', {"null"}, \'{0}\', {"null"}, \'{0}\')\r\n" +
                        "SET IDENTITY_INSERT [dbo].[Memberships] OFF\r\n";
                    }
                    else
                    {
                        query = "SET IDENTITY_INSERT [dbo].[Memberships] ON\r\n" +
                                       "INSERT [Memberships] (Id, SKU, Name, Description, StartDate, EndDate, URL, Price, CreationDate, IsDeleted, IsCustom, ForPurchase, AccessWeekLength, RelatedMembershipGroupId, Gender, PromotionalPopupId, Type)\r\n" +
                                       $"VALUES (\'{lastMemberId + 1}\', \'{membershipSKU}\', \'{"00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh-mm-ss")}\', \'{Lorem.ParagraphByChars(300)}\', {"null"}, {"null"}, \'{$"https://mcmstaging-ui.azurewebsites.net/programs/all"}\', \'{100}\', \'{DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")}\', \'{false}\', \'{false}\', \'{true}\', \'{12}\', {"null"}, \'{0}\', {"null"}, \'{0}\')\r\n" +
                                       "SET IDENTITY_INSERT [dbo].[Memberships] OFF\r\n";
                    }
                    
                    try
                    {
                        SqlConnection db = new(DB.GET_CONNECTION_STRING);
                        SqlCommand command = new(query, db);

                        db.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            continue;
                        }
                        var rowsAffected = command.ExecuteNonQueryAsync();
                        Console.WriteLine(rowsAffected);
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

                public static void InsertCustomMembership(int lastMemberId, string membershipSKU)
                {
                    string query = "SET IDENTITY_INSERT [dbo].[Memberships] ON\r\n" +
                        "INSERT [Memberships] (Id, SKU, Name, Description, StartDate, EndDate, URL, Price, CreationDate, IsDeleted, IsCustom, ForPurchase, AccessWeekLength, RelatedMembershipGroupId, Gender, PromotionalPopupId, Type)\r\n" +
                        $"VALUES (\'{lastMemberId + 1}\', \'{membershipSKU}\', \'{"00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh-mm-ss")}\', \'{Lorem.ParagraphByChars(300)}\', {"null"}, {"null"}, \'{$"https://mcmstaging-ui.azurewebsites.net/programs/all"}\', \'{100}\', \'{DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")}\', \'{false}\', \'{false}\', \'{true}\', \'{16}\', '{null}', \'{0}\', {"null"}, \'{0}\')\r\n" +
                        "SET IDENTITY_INSERT [dbo].[Memberships] OFF\r\n";
                    try
                    {
                        SqlConnection db = new(DB.GET_CONNECTION_STRING);
                        SqlCommand command = new(query, db);

                        db.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            continue;
                        }
                        var rowsAffected = command.ExecuteNonQueryAsync();
                        Console.WriteLine(rowsAffected);
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

            public static List<DB.UserMemberships> GetAllUsermembershipInRange(DateTime start, DateTime end)
            {
                var list = new List<DB.UserMemberships>();
                string query = "Select *\r\n  " +
                               "From UserMemberships \r\n  " +
                               $"where IsDeleted = 0 and CreationDate between '{start}' and '{end}'";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.UserMemberships();
                        row.Id = GetValueOrDefault<int>(reader, 0);
                        row.MembershipId = GetValueOrDefault<int>(reader, 1);
                        row.UserId = GetValueOrDefault<string>(reader, 2);
                        row.StartOn = GetValueOrDefault<DateTime>(reader, 3);
                        row.Active = GetValueOrDefault<bool>(reader, 4);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 6);
                        row.OnPause = GetValueOrDefault<bool>(reader, 7);
                        row.PauseEnd = GetValueOrDefault<DateTime>(reader, 8);
                        row.PauseStart = GetValueOrDefault<DateTime>(reader, 9);
                        row.DisplayedPromotionalPopupId = GetValueOrDefault<bool>(reader, 10);
                        row.ExpirationDate = GetValueOrDefault<DateTime>(reader, 11);

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

            public static List<DB.JsonUserExOneField> GetAllUsermembershipByUserId(DB.UserMemberships userMember, DateTime start, DateTime end)
            {
                var list = new List<DB.JsonUserExOneField>();
                string query = "Select distinct Id \r\n  " +
                               "From UserMemberships \r\n  " +
                               $"where UserId = '{userMember.UserId}';";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.JsonUserExOneField();
                        row.UserMembershipId = GetValueOrDefault<int>(reader, 0);

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

            public static List<DB.JsonUserExOneField> GetUnicUserIdFromUserMembershipsInRange(DB.UserMemberships userMember, DateTime start, DateTime end)
            {
                var list = new List<DB.JsonUserExOneField>();
                string query = "select distinct jue.UserMembershipId from JsonUserExercises jue\r\n  " +
                               "inner join WorkoutExercises we on we.Id = jue.WorkoutExerciseId\r\n  " +
                               "inner join Workouts w on w.Id = we.WorkoutId\r\n  " +
                               "inner join Programs p on p.Id = w.ProgramId\r\n  " +
                               "inner join UserMemberships um on um.MembershipId = p.MembershipId\r\n  " +
                               $"where jue.UserId = '{userMember.UserId}'" +
                               "order by jue.UserMembershipId;";

                if (userMember.UserId != "44745389-8adc-4e8d-95b4-eac1d0dfa1db" || userMember.UserId != "d30d9f54-f658-4d7b-910c-cba8887184fb")
                {
                    try
                    {
                        SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
                        SqlCommand command = new(query, db);
                        db.Open();

                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var row = new DB.JsonUserExOneField();
                                //row.Id = GetValueOrDefault<int>(reader, 0);
                                //row.MembershipId = GetValueOrDefault<int>(reader, 1);
                                //row.UserId = GetValueOrDefault<string>(reader, 2);
                                //row.StartOn = GetValueOrDefault<DateTime>(reader, 3);
                                //row.Active = GetValueOrDefault<bool>(reader, 4);
                                //row.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
                                //row.IsDeleted = GetValueOrDefault<bool>(reader, 6);
                                //row.OnPause = GetValueOrDefault<bool>(reader, 7);
                                //row.PauseEnd = GetValueOrDefault<DateTime>(reader, 8);
                                //row.PauseStart = GetValueOrDefault<DateTime>(reader, 9);
                                //row.DisplayedPromotionalPopupId = GetValueOrDefault<bool>(reader, 10);
                                //row.ExpirationDate = GetValueOrDefault<DateTime>(reader, 11);
                                //row.Idjue = GetValueOrDefault<int>(reader, 12);
                                //row.SetDescription = GetValueOrDefault<string>(reader, 13);
                                //row.WorkoutExerciseId = GetValueOrDefault<int>(reader, 14);
                                //row.UserIdjue = GetValueOrDefault<string>(reader, 15);
                                //row.IsDone = GetValueOrDefault<bool>(reader, 16);
                                //row.CreationDatejue = GetValueOrDefault<DateTime>(reader, 17);
                                //row.IsDeletedjue = GetValueOrDefault<bool>(reader, 18);
                                //row.UpdateDate = GetValueOrDefault<DateTime>(reader, 19);
                                row.UserMembershipId = GetValueOrDefault<int>(reader, 0);

                                list.Add(row);
                            }
                        }
                        else if (!reader.HasRows)
                        {
                            var row = new DB.JsonUserExOneField();
                            //row.Id = null;
                            //row.MembershipId = null;
                            //row.UserId = null;
                            //row.StartOn = null;
                            //row.Active = null;
                            //row.CreationDate = null;
                            //row.IsDeleted = null;
                            //row.OnPause = null;
                            //row.PauseEnd = null;
                            //row.PauseStart = null;
                            //row.DisplayedPromotionalPopupId = null;
                            //row.ExpirationDate = null;
                            //row.Idjue = null;
                            //row.SetDescription = null;
                            //row.WorkoutExerciseId = null;
                            //row.UserIdjue = null;
                            //row.IsDone = null;
                            //row.CreationDatejue = null;
                            //row.IsDeletedjue = null;
                            //row.UpdateDate = null;
                            row.UserMembershipId = null;

                            list.Add(row);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Помилка у юзера з Ид: {0}\r\n{1}", userMember.UserId, ex.StackTrace);
                    }
                    finally
                    {

                        // Забезпечуємо вивільнення ресурсів
                        SqlConnection.ClearAllPools();
                    }
                }
                return list;
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
                        user.Bodyfat = reader.GetDecimal(10);
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
                        user.Bodyfat = reader.GetDecimal(10);
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
                               "where UserId = @UserId and IsDeleted = 0" +
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

            public static List<DB.ProgressDaily> GetAllProgressDailyByUserId(string id)
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

        public class JsonUserExercisesReq
        {
            public static List<DB.JsonUserExercises> GetJsonUserExercisesByUserId(DB.UserMemberships user)
            {
                var list = new List<DB.JsonUserExercises>();
                string query = $"select distinct jue.* " +
                               $"from JsonUserExercises jue\r\n  " +
                               $"inner join WorkoutExercises we on we.Id = jue.WorkoutExerciseId\r\n  " +
                               $"inner join Workouts w on w.Id = we.WorkoutId\r\n  " +
                               $"inner join Programs p on p.Id = w.ProgramId\r\n  " +
                               $"inner join UserMemberships um on um.MembershipId = p.MembershipId\r\n  " +
                               $"where jue.UserId = '{user.UserId}' and jue.UserMembershipId = '{user.Id}'";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var row = new DB.JsonUserExercises();
                            row.Id = GetValueOrDefault<int>(reader, 0);
                            row.SetDescription = GetValueOrDefault<string>(reader, 1);
                            row.WorkoutExerciseId = GetValueOrDefault<int>(reader, 2);
                            row.UserId = GetValueOrDefault<string>(reader, 3);
                            row.IsDone = GetValueOrDefault<bool>(reader, 4);
                            row.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
                            row.IsDeleted = GetValueOrDefault<bool>(reader, 6);
                            row.UpdateDate = GetValueOrDefault<DateTime>(reader, 7);
                            row.UserMembershipId = GetValueOrDefault<int>(reader, 8);

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

    }
}
