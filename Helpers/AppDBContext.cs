using MCMAutomation.APIHelpers;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Logging;
using Npgsql;
using NUnit.Framework;
using OpenQA.Selenium;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using static Chilkat.Http;
using static MCMAutomation.APIHelpers.Client.Membership.MembershipModel;

namespace MCMAutomation.Helpers
{

    public class AppDbContext
    {
        private static T GetValueOrDefault<T>(SqlDataReader reader, int index, T defaultValue = default)
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
        private static T GetValueOrDefault<T>(NpgsqlDataReader reader, int index, T defaultValue = default)
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

        public class LsfDb
        {
            public class UserWorkoutDay
            {
                public static List<DB.LsfDbModels.ApiUserWorkoutDay> GetApiUserWorkoutDayByWorkoutIdAndDay(string workoutId, string day, bool isForGym)
                {
                    var list = new List<DB.LsfDbModels.ApiUserWorkoutDay>();
                    string query = $"Select distinct * " +
                                                 $"FROM api_userworkoutday " +
                                                 $"where workout_id = '{workoutId}' and \"day\" in ('{day}') AND for_gym = {isForGym} and user_id not in (\r\n'ade3829b-22d4-4343-be5e-2d01758baba7',\r\n'ca26375b-3e5b-4cfe-ab0b-c74dea63e9c8',\r\n'f9a24549-2433-4644-b069-7773bbb6f743') order by id;";
                    using NpgsqlConnection db = new(DB.GET_CONNECTION_STRING_LSF);

                    using NpgsqlCommand command = new(query, db);
                    try
                    {
                        db.Open();
                        using NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var row = new DB.LsfDbModels.ApiUserWorkoutDay();

                            row.Id = GetValueOrDefault<Guid>(reader, 0);
                            row.Created_at = GetValueOrDefault<DateTime>(reader, 1);
                            row.Modified_at = GetValueOrDefault<DateTime>(reader, 2);
                            row.Date = GetValueOrDefault<DateTime>(reader, 3);
                            row.Completed = GetValueOrDefault<bool>(reader, 4);
                            row.Name = GetValueOrDefault<string>(reader, 5);
                            row.Day = GetValueOrDefault<string>(reader, 6);
                            row.Approx_time = GetValueOrDefault<string>(reader, 7);
                            row.Step_target = GetValueOrDefault<int>(reader, 8);
                            row.Level = GetValueOrDefault<string>(reader, 9);
                            row.For_gym = GetValueOrDefault<bool>(reader, 10);
                            row.For_home = GetValueOrDefault<bool>(reader, 11);
                            row.Image = GetValueOrDefault<string>(reader, 12);
                            row.User_id = GetValueOrDefault<Guid>(reader, 13);
                            row.Workout_id = GetValueOrDefault<Guid>(reader, 14);
                            row.Steps_covered = GetValueOrDefault<int>(reader, 15);
                            row.Challenge_id = GetValueOrDefault<Guid>(reader, 16);
                            row.Challenge_ends_on = GetValueOrDefault<DateTime>(reader, 17);
                            row.A_o_p_id = GetValueOrDefault<Guid>(reader, 18);
                            row.Program = GetValueOrDefault<string>(reader, 19);
                            list.Add(row);
                        }

                    }
                    catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                    

                    return list;
                }

                public static List<DB.LsfDbModels.ApiUserWorkoutDay> GetApiUserWorkoutDayForFirstDay(int workout_category, int orderNum, string workoutId, string day, bool isForGym)
                {
                    var list = new List<DB.LsfDbModels.ApiUserWorkoutDay>();
                    string query = $"Select distinct *  " +
                                   $"FROM api_userworkoutday  " +
                                   $"where id in " +
                                   $"(Select workout_day_id from api_userworkoutdayexerciseset where workout_category = '{workout_category}' and \"order\"= '{orderNum}' and workout_day_id in " +
                                   $"(Select id FROM api_userworkoutday where workout_id = '{workoutId}' and \"day\" in ('{day}') AND for_gym = {isForGym} and user_id not in ('ade3829b-22d4-4343-be5e-2d01758baba7','ca26375b-3e5b-4cfe-ab0b-c74dea63e9c8','f9a24549-2433-4644-b069-7773bbb6f743'))) order by id;";
                    using NpgsqlConnection db = new(DB.GET_CONNECTION_STRING_LSF);

                    using NpgsqlCommand command = new(query, db);
                    try
                    {
                        db.Open();
                        using NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var row = new DB.LsfDbModels.ApiUserWorkoutDay();

                            row.Id = GetValueOrDefault<Guid>(reader, 0);
                            row.Created_at = GetValueOrDefault<DateTime>(reader, 1);
                            row.Modified_at = GetValueOrDefault<DateTime>(reader, 2);
                            row.Date = GetValueOrDefault<DateTime>(reader, 3);
                            row.Completed = GetValueOrDefault<bool>(reader, 4);
                            row.Name = GetValueOrDefault<string>(reader, 5);
                            row.Day = GetValueOrDefault<string>(reader, 6);
                            row.Approx_time = GetValueOrDefault<string>(reader, 7);
                            row.Step_target = GetValueOrDefault<int>(reader, 8);
                            row.Level = GetValueOrDefault<string>(reader, 9);
                            row.For_gym = GetValueOrDefault<bool>(reader, 10);
                            row.For_home = GetValueOrDefault<bool>(reader, 11);
                            row.Image = GetValueOrDefault<string>(reader, 12);
                            row.User_id = GetValueOrDefault<Guid>(reader, 13);
                            row.Workout_id = GetValueOrDefault<Guid>(reader, 14);
                            row.Steps_covered = GetValueOrDefault<int>(reader, 15);
                            row.Challenge_id = GetValueOrDefault<Guid>(reader, 16);
                            row.Challenge_ends_on = GetValueOrDefault<DateTime>(reader, 17);
                            row.A_o_p_id = GetValueOrDefault<Guid>(reader, 18);
                            row.Program = GetValueOrDefault<string>(reader, 19);
                            list.Add(row);
                        }

                    }
                    catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                    

                    return list;
                }

                public static void UpdateApiUserWorkoutDayExerciseSetById(int workout_category, int orderNum, Guid workout_day_id, Guid new_workout_day_id)
                {
                    var list = new List<DB.LsfDbModels.ApiUserWorkoutDay>();
                    string query = $"Update api_userworkoutdayexerciseset " +
                                   $"set workout_day_id = '{new_workout_day_id}' " +
                                   $"where workout_category = '{workout_category}' " +
                                   $"AND \"order\" = '{orderNum}' and workout_day_id = '{workout_day_id}';";
                    using NpgsqlConnection db = new(DB.GET_CONNECTION_STRING_LSF);
                    using NpgsqlCommand command = new(query, db);
                    try
                    {
                        db.Open();
                        using NpgsqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            continue;
                        }
                    }
                    catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                    
                }
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                return list;
            }

            public static List<DB.ExercisesNewApp> GetExercisesDataNewApp()
            {
                var list = new List<DB.ExercisesNewApp>();
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
                        var row = new DB.ExercisesNewApp();
                        row.Id = GetValueOrDefault<Guid>(reader, 0);
                        row.Name = GetValueOrDefault<string>(reader, 1);
                        row.VideoURL = GetValueOrDefault<string>(reader, 2);
                        row.TempoBold = GetValueOrDefault<int>(reader, 3);
                        row.CreatedAt = GetValueOrDefault<DateTime>(reader, 4);
                        row.ModifiedAt = GetValueOrDefault<DateTime>(reader, 5);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 6);

                        list.Add(row);
                    }

                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return list;
            }

            public static List<DB.WorkoutsNewApp> GetLastWorkoutsDataNewApp(int numWorkouts)
            {
                WaitUntil.WaitSomeInterval(5000);
                var list = new List<DB.WorkoutsNewApp>();
                string query = $"SELECT TOP({numWorkouts}) * " +
                               $"FROM [Workouts] " +
                               "ORDER BY CreatedAt DESC";
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
                            var row = new DB.WorkoutsNewApp();
                            row.Id = GetValueOrDefault<Guid>(reader, 0);
                            row.Name = GetValueOrDefault<string>(reader, 1);
                            row.WeekDay = GetValueOrDefault<int>(reader, 2);
                            row.ProgramId = GetValueOrDefault<Guid>(reader, 3);
                            row.CreatedAt = GetValueOrDefault<DateTime>(reader, 4);
                            row.ModifiedAt = GetValueOrDefault<DateTime>(reader, 5);
                            row.IsDeleted = GetValueOrDefault<bool>(reader, 6);

                            list.Add(row);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                return list;
            }

            public static List<DB.Workouts> GetWorkoutsByProgramId(List<DB.Programs> programs)
            {
                WaitUntil.WaitSomeInterval(5000);
                var list = new List<DB.Workouts>();
                foreach (var program in programs)
                {
                    string query = $"SELECT * " +
                               $"FROM [Workouts] WHERE ProgramId = {program.Id};";
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
                    catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                    finally { SqlConnection.ClearAllPools(); }
                }

                return list;
            }

            public class Insert
            {
                public static void InsertWorkoutNewApp(List<DB.ProgramsNewApp> programs, int daysCount)
                {
                    string query;
                    query = //"SET IDENTITY_INSERT [dbo].[Memberships] ON\r\n" +
                        "INSERT INTO [dbo].[Workouts] ([Id], [Name], [WeekDay], [ProgramId], [CreatedAt], [ModifiedAt], [IsDeleted]) \r\n" +
                        $"VALUES {WorkoutHandler(programs, daysCount)}";
                    //"SET IDENTITY_INSERT [dbo].[Memberships] OFF\r\n";


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
                        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                    }
                    finally
                    {
                        // Забезпечуємо вивільнення ресурсів
                        SqlConnection.ClearAllPools();
                    }

                }

                static string WorkoutHandler(List<DB.ProgramsNewApp> programs, int daysCount)
                {
                    string query = string.Empty;
                    bool isFirstRow = true;

                    foreach (var program in programs)
                    {
                        for (int i = 0; i <= daysCount; i++)
                        {
                            if (i < daysCount || (i == daysCount && program.Id != programs.LastOrDefault().Id))
                            {
                                query += string.Format("('{0}', \'Workout Test {1}\', \'{2}\', '{3}', \'{4}\', \'{5}\', {6}),\r\n",
                                    System.Guid.NewGuid(), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), i, program.Id, DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), 0);
                            }
                            else
                            {
                                // Only remove comma for the last row of the current program
                                if (isFirstRow)
                                {
                                    query += string.Format("('{0}', \'Workout Test {1}\', \'{2}\', '{3}', \'{4}\', \'{5}\', {6})\r\n",
                                        System.Guid.NewGuid(), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), i, program.Id, DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), 0);
                                    isFirstRow = false;
                                }
                                else
                                {
                                    query += string.Format("('{0}', \'Workout Test {1}\', \'{2}\', '{3}', \'{4}\', \'{5}\', {6}),\r\n",
                                        System.Guid.NewGuid(), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), i, program.Id, DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), 0);
                                }
                            }
                        }
                        isFirstRow = true; // Reset for the next program
                    }

                    return query;
                }

            }
        }

        public class WorkoutExercises
        {
            public class Insert
            {
                public static void InsertWorkoutExercises(int? lastWorkoutExerciseId, List<DB.WorkoutExercises> workoutExercises)
                {
                    string query = "SET IDENTITY_INSERT WorkoutExercises ON" +
                                   "\r\n\r\nInsert WorkoutExercises " +
                                   "(Id,\tWorkoutId,\tExerciseId,\tSets,\tReps,\tTempo,\tRest,\tCreationDate,\tIsDeleted,\tSeries,\tNotes,\tWeek,\tSimultaneouslyСreatedIds,\tWorkoutExerciseGroupId)" +
                                   "\r\nValues\r\n" +
                                   $"{ListHandler(lastWorkoutExerciseId, workoutExercises)}" +
                                   "\r\n\r\nSET IDENTITY_INSERT WorkoutExercises OFF";
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
                        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                    }
                    finally
                    {

                        // Забезпечуємо вивільнення ресурсів
                        SqlConnection.ClearAllPools();
                    }
                }

                private static string ListHandler(int? lastWorkoutExerciseId, List<DB.WorkoutExercises> list)
                {
                    string row = string.Empty;
                    int i = 0;
                    Dictionary<int, int> valueMap = new Dictionary<int, int>
                    {

                        { 2662, 2662 },
                        { 2668, 2668 },
                        { 2669, 2669 },
                        { 2670, 2670 },
                        { 2671, 2671 },
                        { 2672, 2672 },
                        { 2673, 2673 },
                        { 2674, 2674 },
                        { 2675, 2675 },
                        { 2676, 2676 }

                    };


                    foreach (var item in list)
                    {
                        ++i;
                        int result = valueMap.ContainsKey(item.WorkoutId.Value)
                        ? valueMap[item.WorkoutId.Value]
                        : item.WorkoutId.Value;
                        if (item.Id != list.Last().Id)
                        {
                            row += string.Concat("(", lastWorkoutExerciseId + i, ",\t", valueMap.ContainsKey(item.WorkoutId.Value) ? valueMap[item.WorkoutId.Value] : default, ",\t", item.ExerciseId, ",\t", item.Sets, ",\t", "\'", item.Reps, "\'", ",\t", "\'", item.Tempo, "\'", ",\t", item.Rest, ",\t", "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\'", ",\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t", "\'", item.Series, "\'", ",\t", "\'", item.Notes, "\'", ",\t", item.Week, ",\t", "\'", item.SimultaneouslyCreatedIds, "\'", ",\t", item.WorkoutExerciseGroupId, "),", "\r\n");
                        }
                        else
                        {
                            row += string.Concat("(", lastWorkoutExerciseId + i, ",\t", valueMap.ContainsKey(item.WorkoutId.Value) ? valueMap[item.WorkoutId.Value] : default, ",\t", item.ExerciseId, ",\t", item.Sets, ",\t", "\'", item.Reps, "\'", ",\t", "\'", item.Tempo, "\'", ",\t", item.Rest, ",\t", "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\'", ",\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t", "\'", item.Series, "\'", ",\t", "\'", item.Notes, "\'", ",\t", item.Week, ",\t", "\'", item.SimultaneouslyCreatedIds, "\'", ",\t", item.WorkoutExerciseGroupId, ")");
                        }


                    }
                    return row;
                }

                public static void InsertWorkoutExercisesNewApp(int maxCount, int programWeekNumber, List<DB.WorkoutsNewApp> workouts, List<DB.ExercisesNewApp> exercises)
                {
                    int i = 0;
                    string query;
                    query = //"SET IDENTITY_INSERT [dbo].[Memberships] ON\r\n" +
                        "INSERT INTO [dbo].[WorkoutExercises] ([Id] ,[WorkoutId] ,[ExerciseId] ,[WeekNumber] ,[Series] ,[Sets] ,[Reps] ,[Tempo] ,[Rest] ,[Notes] ,[CreatedAt] ,[ModifiedAt] ,[IsDeleted])\r\n " +
                        $"VALUES {WorkoutExercisesHandler(maxCount, programWeekNumber, workouts, exercises)}";
                    //"SET IDENTITY_INSERT [dbo].[Memberships] OFF\r\n";


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
                        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                    }
                    finally
                    {
                        // Забезпечуємо вивільнення ресурсів
                        SqlConnection.ClearAllPools();
                    }

                }

                static string WorkoutExercisesHandler(int maxCount, int? weekNumber, List<DB.WorkoutsNewApp> workouts, List<DB.ExercisesNewApp> exercises)
                {
                    string query = string.Empty;
                    bool isFirstRow = true;
                    int seriesNum = 0;

                    foreach (var workout in workouts)
                    {
                        for (int i = 1; i <= maxCount; i++)
                        {
                            if (i < weekNumber)
                            {
                                List<DB.WorkoutExercisesNewApp> squery = Enumerable.Range(1, 4).Select(str => new DB.WorkoutExercisesNewApp
                                {
                                    Id = Guid.NewGuid(),
                                    WorkoutId = workout.Id,
                                    ExerciseId = exercises[RandomHelper.RandomExercise(exercises.Count)].Id,
                                    WeekNumber = i,
                                    Series = SeriesHandler(seriesNum),
                                    Sets = 3,
                                    Reps = "10",
                                    Tempo = "2010",
                                    Rest = int.Parse(RandomHelper.RandomNumber(120)),
                                    Notes = Lorem.ParagraphByChars(15),
                                    CreatedAt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")),
                                    ModifiedAt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")),
                                    IsDeleted = false
                                    //string.Format("(\'{0}\' , \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\', \'{6}\', \'{7}\', \'{8}\', \'{9}\', \'{10}\', '{11}', {12}),\r\n",
                                    //    Guid.NewGuid(), workout.Id, exercises[RandomHelper.RandomExercise(exercises.Count)].Id, i, SeriesHandler(seriesNum), 3, 10, 2010, RandomHelper.RandomNumber(120), Lorem.ParagraphByChars(15), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), 0)
                                }).ToList();
                            }
                            else
                            {
                                // Only remove comma for the last row of the current workout
                                if (isFirstRow)
                                {
                                    query += Enumerable.Range(1, 4).Select(str => new String[]
                                    {
                                        string.Format("(\'{0}\' , \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\', \'{6}\', \'{7}\', \'{8}\', \'{9}\', \'{10}\', '{11}', {12})\r\n",
                                        Guid.NewGuid(), workout.Id, exercises[RandomHelper.RandomExercise(exercises.Count)].Id, i, SeriesHandler(seriesNum), 3, 10, 2010, RandomHelper.RandomNumber(120), Lorem.ParagraphByChars(15), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), 0)
                                    }).ToList();
                                    isFirstRow = false;
                                    seriesNum++;
                                }
                                else
                                {
                                    query += Enumerable.Range(1, 4).Select(str => new String[]
                                    {
                                        string.Format("(\'{0}\' , \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\', \'{6}\', \'{7}\', \'{8}\', \'{9}\', \'{10}\', '{11}', {12})\r\n",
                                        Guid.NewGuid(), workout.Id, exercises[RandomHelper.RandomExercise(exercises.Count)].Id, i, SeriesHandler(seriesNum), 3, 10, 2010, RandomHelper.RandomNumber(120), Lorem.ParagraphByChars(15), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff"), 0)

                                    }).ToList();

                                }
                            }
                        }




                        isFirstRow = true; // Reset for the next workout
                        seriesNum = 0;
                    }

                    return query;
                }



                static string SeriesHandler(int seriesNum)
                {
                    string serie = string.Empty;
                    switch (seriesNum)
                    {
                        case 0:
                            serie = "A";
                            break;

                        case 1:
                            serie = "B";
                            break;
                        case 2:
                            serie = "C";
                            break;
                        case 3:
                            serie = "D";
                            break;
                        case 4:
                            serie = "E";
                            break;
                        default: return "t";
                    }
                    return serie;
                }
            }

            public static List<DB.WorkoutExercises> GetLastWorkoutExercise()
            {
                var list = new List<DB.WorkoutExercises>();
                string query = "SELECT TOP(1) *\r\n" +
                               "FROM WorkoutExercises \r\n" +
                               "ORDER BY id desc;";
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
                            var row = new DB.WorkoutExercises();
                            row.Id = GetValueOrDefault<int>(reader, 0);
                            row.WorkoutId = GetValueOrDefault<int>(reader, 1);
                            row.ExerciseId = GetValueOrDefault<int>(reader, 2);
                            row.Sets = GetValueOrDefault<int>(reader, 3);
                            row.Reps = GetValueOrDefault<string>(reader, 4);
                            row.Tempo = GetValueOrDefault<string>(reader, 5);
                            row.Rest = GetValueOrDefault<int>(reader, 6);
                            row.CreationDate = GetValueOrDefault<DateTime?>(reader, 7);
                            row.IsDeleted = GetValueOrDefault<bool>(reader, 8);
                            row.Series = GetValueOrDefault<string>(reader, 9);
                            row.Notes = GetValueOrDefault<string>(reader, 10);
                            row.Week = GetValueOrDefault<int>(reader, 11);
                            row.SimultaneouslyCreatedIds = GetValueOrDefault<string>(reader, 12);
                            row.WorkoutExerciseGroupId = GetValueOrDefault<int>(reader, 13);

                            list.Add(row);

                        }
                    }
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }
                return list;
            }

            public static List<DB.CombinedWorkoutExercisesData> GetCombinedDataOfWorkoutExercises(int? workoutId, string series)
            {
                var list = new List<DB.CombinedWorkoutExercisesData>();
                string query = "SELECT we.Id, we.WorkoutId, we.ExerciseId, we.IsDeleted,we.Series, we.WorkoutExerciseGroupId, we.CreationDate WorkoutExerciseCreationDate, " +
                               "m.Id MembershipId, m.StartDate MembershipStartDate, m.EndDate MembershipEndDate, m.AccessWeekLength MembershipAccessWeekLength, " +
                               "p.Id ProgramId,  p.AvailableDate ProgramStartDate, p.ExpirationDate ProgramEndDate \r\n" +
                               "FROM WorkoutExercises we\r\n " +
                               "LEFT JOIN Exercises e ON e.id = we.ExerciseId\r\n " +
                               "LEFT JOIN Workouts w ON w.id = we.WorkoutId\r\n " +
                               "LEFT JOIN Programs p ON p.Id = w.ProgramId\r\n " +
                               "LEFT JOIN Memberships m ON m.Id = p.MembershipId\r\n " +
                               $"WHERE we.WorkoutId = {workoutId} and we.Series = '{series}';";
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
                            var row = new DB.CombinedWorkoutExercisesData();
                            row.WorkoutExerciseId = GetValueOrDefault<int>(reader, 0);
                            row.WorkoutId = GetValueOrDefault<int>(reader, 1);
                            row.ExerciseId = GetValueOrDefault<int>(reader, 2);
                            row.IsDeleted = GetValueOrDefault<bool>(reader, 3);
                            row.Series = GetValueOrDefault<string>(reader, 4);
                            row.WorkoutExerciseGroupId = GetValueOrDefault<int>(reader, 5);
                            row.WorkoutExercisesCreationDate = GetValueOrDefault<DateTime?>(reader, 6);
                            row.MembershipId = GetValueOrDefault<int>(reader, 7);
                            row.MembershipStartDate = GetValueOrDefault<DateTime?>(reader, 8);
                            row.MembershipEndDate = GetValueOrDefault<DateTime?>(reader, 9);
                            row.MembershipAccessWeekLength = GetValueOrDefault<int>(reader, 10);
                            row.ProgramId = GetValueOrDefault<int>(reader, 11);
                            row.ProgramStartDate = GetValueOrDefault<DateTime?>(reader, 12);
                            row.ProgramEndDate = GetValueOrDefault<DateTime?>(reader, 13);

                            list.Add(row);

                        }
                    }
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }
                return list;
            }

            public static List<DB.WorkoutExercises> GetWorkoutExercisesByWorkoutIds(List<DB.Workouts> workouts)
            {
                var list = new List<DB.WorkoutExercises>();
                foreach (var workout in workouts)
                {
                    string query = "SELECT * FROM WorkoutExercises" +
                        $"\r\nwhere WorkoutId = {workout.Id};";
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
                                var row = new DB.WorkoutExercises();
                                row.Id = GetValueOrDefault<int>(reader, 0);
                                row.WorkoutId = GetValueOrDefault<int>(reader, 1);
                                row.ExerciseId = GetValueOrDefault<int>(reader, 2);
                                row.Sets = GetValueOrDefault<int>(reader, 3);
                                row.Reps = GetValueOrDefault<string>(reader, 4);
                                row.Tempo = GetValueOrDefault<string>(reader, 5);
                                row.Rest = GetValueOrDefault<int>(reader, 6);
                                row.CreationDate = GetValueOrDefault<DateTime?>(reader, 7);
                                row.IsDeleted = GetValueOrDefault<bool>(reader, 8);
                                row.Series = GetValueOrDefault<string>(reader, 9);
                                row.Notes = GetValueOrDefault<string>(reader, 10);
                                row.Week = GetValueOrDefault<int>(reader, 11);
                                row.SimultaneouslyCreatedIds = GetValueOrDefault<string>(reader, 12);
                                row.WorkoutExerciseGroupId = GetValueOrDefault<int>(reader, 13);

                                list.Add(row);

                            }
                        }
                    }
                    catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                    finally { SqlConnection.ClearAllPools(); }
                }

                return list;
            }

            //public static List<DB.WorkoutExercises> GetWorkoutExercisesByWorkoutIdsFromLive()
            //{
            //    var list = new List<DB.WorkoutExercises>();
            //      string query = "select * " +
            //                       "from WorkoutExercises" +
            //                       " where WorkoutId in " +
            //                       "(select id from Workouts where ProgramId in " +
            //                        "(select id from Programs where MembershipId = 178 and id in (691, 696)))";
            //        try
            //        {
            //            SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
            //            SqlCommand command = new(query, db);
            //            db.Open();

            //            SqlDataReader reader = command.ExecuteReader();
            //            if (reader.HasRows)
            //            {
            //                while (reader.Read())
            //                {
            //                    var row = new DB.WorkoutExercises();
            //                    row.Id = GetValueOrDefault<int>(reader, 0);
            //                    row.WorkoutId = GetValueOrDefault<int>(reader, 1);
            //                    row.ExerciseId = GetValueOrDefault<int>(reader, 2);
            //                    row.Sets = GetValueOrDefault<int>(reader, 3);
            //                    row.Reps = GetValueOrDefault<string>(reader, 4);
            //                    row.Tempo = GetValueOrDefault<string>(reader, 5);
            //                    row.Rest = GetValueOrDefault<int>(reader, 6);
            //                    row.CreationDate = GetValueOrDefault<DateTime?>(reader, 7);
            //                    row.IsDeleted = GetValueOrDefault<bool>(reader, 8);
            //                    row.Series = GetValueOrDefault<string>(reader, 9);
            //                    row.Notes = GetValueOrDefault<string>(reader, 10);
            //                    row.Week = GetValueOrDefault<int>(reader, 11);
            //                    row.SimultaneouslyCreatedIds = GetValueOrDefault<string>(reader, 12);
            //                    row.WorkoutExerciseGroupId = GetValueOrDefault<int>(reader, 13);

            //                    list.Add(row);

            //                }
            //            }
            //        }
            //        catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
            //        finally { SqlConnection.ClearAllPools(); }


            //    return list;
            //}

            public static List<DB.WorkoutExercises> GetWorkoutExercisesByProgramId()
            {
                var list = new List<DB.WorkoutExercises>();
                string query = "select * " +
                               "from WorkoutExercises " +
                               "where WorkoutId in " +
                               "(select id from Workouts where ProgramId in " +
                               "(select id from Programs where MembershipId = 178))";
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
                            var row = new DB.WorkoutExercises();
                            row.Id = GetValueOrDefault<int>(reader, 0);
                            row.WorkoutId = GetValueOrDefault<int>(reader, 1);
                            row.ExerciseId = GetValueOrDefault<int>(reader, 2);
                            row.Sets = GetValueOrDefault<int>(reader, 3);
                            row.Reps = GetValueOrDefault<string>(reader, 4);
                            row.Tempo = GetValueOrDefault<string>(reader, 5);
                            row.Rest = GetValueOrDefault<int>(reader, 6);
                            row.CreationDate = GetValueOrDefault<DateTime?>(reader, 7);
                            row.IsDeleted = GetValueOrDefault<bool>(reader, 8);
                            row.Series = GetValueOrDefault<string>(reader, 9);
                            row.Notes = GetValueOrDefault<string>(reader, 10);
                            row.Week = GetValueOrDefault<int>(reader, 11);
                            row.SimultaneouslyCreatedIds = GetValueOrDefault<string>(reader, 12);
                            row.WorkoutExerciseGroupId = GetValueOrDefault<int>(reader, 13);

                            list.Add(row);

                        }
                    }
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }


                return list;
            }

            public static List<DB.WorkoutExercises> GetAllWorkoutExercises()
            {
                var list = new List<DB.WorkoutExercises>();
                string query = "SELECT *\r\n" +
                               "FROM WorkoutExercises \r\n" +
                               "WHERE IsDeleted = 0 \r\n";
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
                            var row = new DB.WorkoutExercises();
                            row.Id = GetValueOrDefault<int>(reader, 0);
                            row.WorkoutId = GetValueOrDefault<int>(reader, 1);
                            row.ExerciseId = GetValueOrDefault<int>(reader, 2);
                            row.Sets = GetValueOrDefault<int>(reader, 3);
                            row.Reps = GetValueOrDefault<string>(reader, 4);
                            row.Tempo = GetValueOrDefault<string>(reader, 5);
                            row.Rest = GetValueOrDefault<int>(reader, 6);
                            row.CreationDate = GetValueOrDefault<DateTime?>(reader, 7);
                            row.IsDeleted = GetValueOrDefault<bool>(reader, 8);
                            row.Series = GetValueOrDefault<string>(reader, 9);
                            row.Notes = GetValueOrDefault<string>(reader, 10);
                            row.Week = GetValueOrDefault<int>(reader, 11);
                            row.SimultaneouslyCreatedIds = GetValueOrDefault<string>(reader, 12);
                            row.WorkoutExerciseGroupId = GetValueOrDefault<int>(reader, 13);

                            list.Add(row);

                        }
                    }
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }
                return list;
            }

            public static int GetCountWorkoutExercises(DB.UserMemberships userMemebrship)
            {
                var row = new int();
                string query = "select count(id) \r\n " +
                               "from WorkoutExercises \r\n " +
                               "where WorkoutId in \r\n " +
                               "(select id from Workouts \r\n " +
                               "where ProgramId in \r\n " +
                               "(select id from Programs \r\n " +
                               $"where membershipid = {userMemebrship.MembershipId} and IsDeleted = 0)) and isdeleted = 0";
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

                            row = GetValueOrDefault<int>(reader, 0);
                        }
                    }
                    db.Close();
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }
                return row;
            }

        }

        public class Memberships
        {
            public static void GetLastMembership(string sku, out DB.Memberships membership)
            {
                WaitUntil.WaitSomeInterval(5000);
                membership = new();
                string query = "SELECT TOP(1)*" +
                                             "FROM [Memberships] " +
                                             $"WHERE isDeleted = 0 AND SKU = {sku}" +
                                             "ORDER BY Id DESC";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        membership.Id = GetValueOrDefault<Int32>(reader, 0);
                        membership.SKU = GetValueOrDefault<string>(reader, 1);
                        membership.Name = GetValueOrDefault<string>(reader, 2);
                        membership.Description = GetValueOrDefault<string>(reader, 3);
                        membership.StartDate = GetValueOrDefault<DateTime>(reader, 4);
                        membership.EndDate = GetValueOrDefault<DateTime>(reader, 5);
                        membership.URL = GetValueOrDefault<string>(reader, 6);
                        membership.Price = GetValueOrDefault<decimal>(reader, 7);
                        membership.CreationDate = GetValueOrDefault<DateTime>(reader, 8);
                        membership.IsDeleted = GetValueOrDefault<bool>(reader, 9);
                        membership.IsCustom = GetValueOrDefault<bool>(reader, 10);
                        membership.ForPurchase = GetValueOrDefault<bool>(reader, 11);
                        membership.AccessWeekLength = GetValueOrDefault<int>(reader, 12);
                        membership.RelatedMembershipGroupId = GetValueOrDefault<int>(reader, 13);
                        membership.Gender = GetValueOrDefault<int>(reader, 14);
                        membership.PromotionalPopupId = GetValueOrDefault<int>(reader, 15);
                        membership.Type = GetValueOrDefault<int>(reader, 16);

                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
                    SqlConnection.ClearAllPools();
                }

            }

            public static List<DB.Memberships>? GetAllMemberships()
            {
                WaitUntil.WaitSomeInterval(5000);
                List<DB.Memberships>? list = new();

                string query = "SELECT *" +
                                             "FROM [Memberships] " +
                                             "WHERE isDeleted = 0 " +
                                             "ORDER BY CreationDate DESC";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.Memberships();
                        row.Id = GetValueOrDefault<int>(reader, 0);
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
                        list.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return list;
            }

            public static List<DB.MembershipsNewApp>? GetAllMembershipsNewApp()
            {
                WaitUntil.WaitSomeInterval(5000);
                List<DB.MembershipsNewApp>? list = new();

                string query = "SELECT *" +
                               "FROM [Memberships] " +
                               "WHERE isDeleted = 0 " +
                               "ORDER BY CreatedAt ASC";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.MembershipsNewApp();
                        row.Id = GetValueOrDefault<Guid>(reader, 0);
                        row.SKU = GetValueOrDefault<string>(reader, 1);
                        row.Name = GetValueOrDefault<string>(reader, 2);
                        row.Type = GetValueOrDefault<int>(reader, 3);
                        row.AccessWeekLength = GetValueOrDefault<int>(reader, 4);
                        row.StartDate = GetValueOrDefault<DateTime>(reader, 5);
                        row.EndDate = GetValueOrDefault<DateTime>(reader, 6);
                        row.Description = GetValueOrDefault<string>(reader, 7);
                        row.URL = GetValueOrDefault<string>(reader, 8);
                        row.Price = GetValueOrDefault<decimal>(reader, 9);
                        row.IsCustom = GetValueOrDefault<bool>(reader, 10);
                        row.ForPurchase = GetValueOrDefault<bool>(reader, 11);
                        row.Gender = GetValueOrDefault<int>(reader, 12);
                        row.CreatedAt = GetValueOrDefault<DateTime>(reader, 13);
                        row.ModifiedAt = GetValueOrDefault<DateTime>(reader, 14);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 15);
                        row.MediaId = GetValueOrDefault<Guid>(reader, 16);

                        list.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return list;
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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

                        membership.Description = GetValueOrDefault<string>(reader, 3);
                        membership.StartDate = GetValueOrDefault<DateTime>(reader, 4);
                        membership.EndDate = GetValueOrDefault<DateTime>(reader, 5);
                        membership.URL = GetValueOrDefault<string>(reader, 6);
                        membership.Price = GetValueOrDefault<decimal>(reader, 7);
                        membership.CreationDate = GetValueOrDefault<DateTime>(reader, 8);
                        membership.IsDeleted = GetValueOrDefault<bool>(reader, 9);
                        membership.IsCustom = GetValueOrDefault<bool>(reader, 10);
                        membership.ForPurchase = GetValueOrDefault<bool>(reader, 11);
                        membership.AccessWeekLength = GetValueOrDefault<int>(reader, 12);
                        membership.RelatedMembershipGroupId = GetValueOrDefault<int>(reader, 13);
                        membership.Gender = GetValueOrDefault<int>(reader, 14);
                        membership.PromotionalPopupId = GetValueOrDefault<int>(reader, 15);
                        membership.Type = GetValueOrDefault<int>(reader, 16);

                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                        row.Id = GetValueOrDefault<int>(reader, 0);
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

                        list.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                        row.Id = GetValueOrDefault<int>(reader, 0);
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

                        list.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                        row.Id = GetValueOrDefault<int>(reader, 0);
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

                        list.Add(row);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    if (eightWeeks == true)
                    {
                        query = "SET IDENTITY_INSERT [dbo].[Memberships] ON\r\n" +
                        "INSERT [Memberships] (Id, SKU, Name, Description, StartDate, EndDate, URL, Price, CreationDate, IsDeleted, IsCustom, ForPurchase, AccessWeekLength, RelatedMembershipGroupId, Gender, PromotionalPopupId, Type)\r\n" +
                        $"VALUES (\'{lastMemberId + 1}\', \'{membershipSKU}\', \'{"00Created New Membership " + DateTime.Now.ToString("yyyy-MM-d hh-mm-ss")}\', \'{Lorem.ParagraphByChars(300)}\', \'{DateTime.Now.Date}\', \'{DateTime.Now.AddHours(720).Date}\', \'{$"https://mcmstaging-ui.azurewebsites.net/programs/all"}\', \'{100}\', \'{DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")}\', \'{false}\', \'{false}\', \'{true}\', \'{0}\', {"null"}, \'{0}\', {"null"}, \'{0}\')\r\n" +
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
                        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                    }
                    finally
                    {

                        // Забезпечуємо вивільнення ресурсів
                        SqlConnection.ClearAllPools();
                    }

                }

                public static void InsertMembershipNewApp(string membershipSKU)
                {
                    string query;
                    query = //"SET IDENTITY_INSERT [dbo].[Memberships] ON\r\n" +
                        "INSERT [Memberships] ([Id] ,[SKU] ,[Name] ,[Type] ,[AccessWeekLength] ,[StartDate] ,[EndDate] ,[Description] ,[URL] ,[Price] ,[IsCustom] ,[ForPurchase] ,[Gender] ,[CreatedAt] ,[ModifiedAt] ,[IsDeleted] ,[MediaId])\r\n" +
                        $"VALUES (\'{System.Guid.NewGuid()}\', \'{membershipSKU}\', \'{"0000Created New Membership " + "product " + RandomHelper.RandomNumber(25)}\', {0}, {16}, {"null"}, {"null"}, \'{Lorem.ParagraphByChars(300)}\', '{$"https://mcmstaging-ui.azurewebsites.net/programs/all"}', '{100}', '{false}', '{false}', '{0}', '{DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")}', '{DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")}', '{0}', {"null"})\r\n";
                    //"SET IDENTITY_INSERT [dbo].[Memberships] OFF\r\n";


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
                        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                        row.Id = GetValueOrDefault<int>(reader, 0);
                        row.MembershipId = GetValueOrDefault<int>(reader, 1);
                        row.Name = GetValueOrDefault<string>(reader, 2);
                        row.NumberOfWeeks = GetValueOrDefault<int>(reader, 3);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 4);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 5);
                        row.Steps = GetValueOrDefault<string>(reader, 6);
                        row.AvailableDate = GetValueOrDefault<DateTime>(reader, 7);
                        row.NextProgramId = GetValueOrDefault<int>(reader, 8);
                        row.ExpirationDate = GetValueOrDefault<DateTime>(reader, 9);
                        row.Type = GetValueOrDefault<int>(reader, 10);
                        list.Add(row);
                    }
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }

                return list;
            }

            public static List<DB.ProgramsNewApp> GetLastProgramsNewApp(int programsCount)
            {
                var list = new List<DB.ProgramsNewApp>();

                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new($"SELECT TOP({programsCount}) * " +
                                             "FROM [Programs] WHERE IsDeleted=0 " +
                                             "ORDER BY CreatedAt DESC", db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.ProgramsNewApp();
                        row.Id = GetValueOrDefault<Guid>(reader, 0);
                        row.MembershipId = GetValueOrDefault<Guid>(reader, 1);
                        row.Name = GetValueOrDefault<string>(reader, 2);
                        row.NumberOfWeeks = GetValueOrDefault<int>(reader, 3);
                        row.NextProgramId = GetValueOrDefault<Guid>(reader, 4);
                        row.Type = GetValueOrDefault<int>(reader, 5);
                        row.Steps = GetValueOrDefault<string>(reader, 6);
                        row.AvailableDate = GetValueOrDefault<DateTime>(reader, 7);
                        row.ExpirationDate = GetValueOrDefault<DateTime>(reader, 8);
                        row.CreatedAt = GetValueOrDefault<DateTime>(reader, 9);
                        row.ModifiedAt = GetValueOrDefault<DateTime>(reader, 10);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 11);
                        row.MediaId = GetValueOrDefault<Guid>(reader, 12);

                        list.Add(row);
                    }
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }

                return list;
            }

            public static DB.Programs GetLastProgram()
            {
                var row = new DB.Programs();
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new($"SELECT TOP(1) * " +
                                             "FROM [Programs] WHERE IsDeleted=0 " +
                                             "ORDER BY CreationDate DESC", db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        row.Id = GetValueOrDefault<int>(reader, 0);
                        row.MembershipId = GetValueOrDefault<int>(reader, 1);
                        row.Name = GetValueOrDefault<string>(reader, 2);
                        row.NumberOfWeeks = GetValueOrDefault<int>(reader, 3);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 4);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 5);
                        row.Steps = GetValueOrDefault<string>(reader, 6);
                        row.AvailableDate = GetValueOrDefault<DateTime>(reader, 7);
                        row.NextProgramId = GetValueOrDefault<int>(reader, 8);
                        row.ExpirationDate = GetValueOrDefault<DateTime>(reader, 9);
                        row.Type = GetValueOrDefault<int>(reader, 10);
                    }
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }

                return row;
            }

            public static List<DB.Programs> GetProgramsByMembershipId(int membershipId)
            {
                var list = new List<DB.Programs>();

                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new($"SELECT * " +
                                             $"FROM [Programs] WHERE MembershipId = {membershipId} ", db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.Programs();
                        row.Id = GetValueOrDefault<int>(reader, 0);
                        row.MembershipId = GetValueOrDefault<int>(reader, 1);
                        row.Name = GetValueOrDefault<string>(reader, 2);
                        row.NumberOfWeeks = GetValueOrDefault<int>(reader, 3);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 4);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 5);
                        row.Steps = GetValueOrDefault<string>(reader, 6);
                        row.AvailableDate = GetValueOrDefault<DateTime>(reader, 7);
                        row.NextProgramId = GetValueOrDefault<int>(reader, 8);
                        row.ExpirationDate = GetValueOrDefault<DateTime>(reader, 9);
                        row.Type = GetValueOrDefault<int>(reader, 10);
                        list.Add(row);
                    }
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }

                return list;
            }

            public class Insert
            {
                public static void InsertProgramsNewApp(Guid? memberGuid, int weeksNumber, int maxCount)
                {
                    string query;
                    query = //"SET IDENTITY_INSERT [dbo].[Memberships] ON\r\n" +
                        "INSERT INTO [dbo].[Programs]\r\n ([Id], [MembershipId], [Name], [NumberOfWeeks], [NextProgramId], [Type], [Steps], [AvailableDate], [ExpirationDate], [CreatedAt], [ModifiedAt], [IsDeleted], [MediaId])\r\n" +
                        $"VALUES {ProgramsHandler(maxCount, memberGuid, weeksNumber)}";
                    /*"SET IDENTITY_INSERT [dbo].[Memberships] OFF\r\n"*/


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
                        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                    }
                    finally
                    {
                        // Забезпечуємо вивільнення ресурсів
                        SqlConnection.ClearAllPools();
                    }

                }

                static string ProgramsHandler(int maxCount, Guid? memberGuid, int weeksNumber)
                {
                    string query = string.Empty;
                    for (int i = 0; i < maxCount; i++)
                    {

                        if (i < maxCount - 1)
                        {
                            query += string.Concat($"(\'{System.Guid.NewGuid()}\', \'{memberGuid}\',\'ProgramName{DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")}\',\'{weeksNumber}\', {"null"}, 1,\'{Lorem.ParagraphByChars(50)}\', {"null"}, {"null"}, \'{DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")}\',{"null"}, {0}, {"null"}),\r\n");
                        }
                        else
                        {
                            query += string.Concat($"(\'{System.Guid.NewGuid()}\', \'{memberGuid}\',\'ProgramName{DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")}\',\'{weeksNumber}\', {"null"}, 1,\'{Lorem.ParagraphByChars(50)}\', {"null"}, {"null"}, \'{DateTime.Now.ToString("yyyy-MM-d hh:mm:ss.fffffff")}\',{"null"}, {0}, {"null"})\r\n");
                        }
                    }

                    return query;
                }
            }
        }

        public class UserMemberships
        {
            public static int GetLastUsermembershipId(string userEmail)
            {
                int str = 0;
                string query = "Select top(1) id from UserMemberships\r\n" +
                                             "where UserId in (Select id FROM AspNetUsers where Email = @email)\r\n" +
                                             "order by Id desc";
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
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
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                        SqlConnection db = new(DB.GET_CONNECTION_STRING);
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
                        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                    }
                    finally
                    {

                        // Забезпечуємо вивільнення ресурсів
                        SqlConnection.ClearAllPools();
                    }
                }
                return list;
            }

            public static void GetAllUsermembershipByUserId(DB.AspNetUsers user, out List<DB.UserMemberships> userMemberships)
            {
                WaitUntil.WaitSomeInterval(5000);
                userMemberships = new List<DB.UserMemberships>();
                string query = "SELECT * \r\n" +
                               "FROM [dbo].[UserMemberships] \r\n" +
                               $"where UserId = '{user.Id}'";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
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
                        row.ParentSubAllUserMembershipId = GetValueOrDefault<int>(reader, 12);

                        userMemberships.Add(row);
                    }
                    var nonNullParentIds = userMemberships
                        .Where(x => x.ParentSubAllUserMembershipId != null && x.ParentSubAllUserMembershipId != 0)
                        .Select(x => x.ParentSubAllUserMembershipId.Value)
                        .ToList();

                    var filteredMemberships = userMemberships
                        .Where(x => x.ParentSubAllUserMembershipId == null || !nonNullParentIds.Contains((int)x.Id))
                        .ToList();

                    userMemberships = filteredMemberships;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
                    SqlConnection.ClearAllPools();
                }

            }

            public static void GetLastUsermembershipByUserIdForSubAll(DB.AspNetUsers user, out DB.UserMemberships userMembership)
            {
                WaitUntil.WaitSomeInterval(5000);
                userMembership = new();
                string query = "SELECT TOP(1) * \r\n" +
                               "FROM [dbo].[UserMemberships] \r\n" +
                               $"where UserId = '{user.Id}' or (UserId = '{user.Id}' and ParentSubAllUserMembershipId is not null)";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        userMembership.Id = GetValueOrDefault<int>(reader, 0);
                        userMembership.MembershipId = GetValueOrDefault<int>(reader, 1);
                        userMembership.UserId = GetValueOrDefault<string>(reader, 2);
                        userMembership.StartOn = GetValueOrDefault<DateTime>(reader, 3);
                        userMembership.Active = GetValueOrDefault<bool>(reader, 4);
                        userMembership.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
                        userMembership.IsDeleted = GetValueOrDefault<bool>(reader, 6);
                        userMembership.OnPause = GetValueOrDefault<bool>(reader, 7);
                        userMembership.PauseEnd = GetValueOrDefault<DateTime>(reader, 8);
                        userMembership.PauseStart = GetValueOrDefault<DateTime>(reader, 9);
                        userMembership.DisplayedPromotionalPopupId = GetValueOrDefault<bool>(reader, 10);
                        userMembership.ExpirationDate = GetValueOrDefault<DateTime>(reader, 11);
                        userMembership.ParentSubAllUserMembershipId = GetValueOrDefault<int>(reader, 12);
                    }

                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
                    SqlConnection.ClearAllPools();
                }

            }

            public static void GetLastUsermembershipByUserId(DB.AspNetUsers user, out DB.UserMemberships userMembership)
            {
                WaitUntil.WaitSomeInterval(5000);
                userMembership = new();
                string query = "SELECT TOP(1) * \r\n" +
                               "FROM [dbo].[UserMemberships] \r\n " +
                               $"where UserId = '{user.Id}' order by Id desc";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        userMembership.Id = GetValueOrDefault<int>(reader, 0);
                        userMembership.MembershipId = GetValueOrDefault<int>(reader, 1);
                        userMembership.UserId = GetValueOrDefault<string>(reader, 2);
                        userMembership.StartOn = GetValueOrDefault<DateTime>(reader, 3);
                        userMembership.Active = GetValueOrDefault<bool>(reader, 4);
                        userMembership.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
                        userMembership.IsDeleted = GetValueOrDefault<bool>(reader, 6);
                        userMembership.OnPause = GetValueOrDefault<bool>(reader, 7);
                        userMembership.PauseEnd = GetValueOrDefault<DateTime>(reader, 8);
                        userMembership.PauseStart = GetValueOrDefault<DateTime>(reader, 9);
                        userMembership.DisplayedPromotionalPopupId = GetValueOrDefault<bool>(reader, 10);
                        userMembership.ExpirationDate = GetValueOrDefault<DateTime>(reader, 11);
                        userMembership.ParentSubAllUserMembershipId = GetValueOrDefault<int>(reader, 12);
                    }

                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
                    SqlConnection.ClearAllPools();
                }

            }

            public static void GetListUsermembershipsByDate(out List<DB.UserMemberships> userMemberships)
            {
                WaitUntil.WaitSomeInterval(5000);
                userMemberships = new();
                string query = "Select * " +
                               "from Usermemberships " +
                               "where CreationDate > '2023-08-01 00:00:00.0000000' and isdeleted = 0";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
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
                        row.ParentSubAllUserMembershipId = GetValueOrDefault<int>(reader, 12);
                        userMemberships.Add(row);
                    }

                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
                    SqlConnection.ClearAllPools();
                }
            }

            public static List<DB.UserMemberships> GetAllUsermembershipByDate()
            {
                var list = new List<DB.UserMemberships>();
                string query = "Select  *\r\n  " +
                               "From UserMemberships\r\n  " +
                               "where CreationDate > '2023-12-07 00:00:00.0000000'\r\n  " +
                               "order by userid";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
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
                    db.Close();

                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
                    SqlConnection.ClearAllPools();
                }
            }

            public static void UpdateUserDailyProgressDate(string userId)
            {
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new("UPDATE [DailyProgress] set " +
                                             "CreationDate = DateAdd(DD, -1, CreationDate), Date = DateAdd(DD, -1, Date) where UserId = @userId", db);
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
                return list;
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
                        user.Id = GetValueOrDefault<string>(reader, 0);
                        user.FirstName = GetValueOrDefault<string>(reader, 1);
                        user.LastName = GetValueOrDefault<string>(reader, 2);
                        user.Email = GetValueOrDefault<string>(reader, 3);
                        user.ConversionSystem = GetValueOrDefault<int>(reader, 4);
                        user.Gender = GetValueOrDefault<int>(reader, 5);
                        user.Birthdate = GetValueOrDefault<DateTime>(reader, 6);
                        user.Weight = GetValueOrDefault<decimal>(reader, 7);
                        user.Height = GetValueOrDefault<int>(reader, 8);
                        user.ActivityLevel = GetValueOrDefault<int>(reader, 9);
                        user.Bodyfat = GetValueOrDefault<decimal>(reader, 10);
                        user.Calories = GetValueOrDefault<int>(reader, 11);
                        user.Active = GetValueOrDefault<bool>(reader, 12);
                        user.DateTime = GetValueOrDefault<DateTime>(reader, 13);
                        user.UserName = GetValueOrDefault<string>(reader, 14);
                        user.NormalizedUserName = GetValueOrDefault<string>(reader, 15);
                        user.NormalizedEmail = GetValueOrDefault<string>(reader, 16);
                        user.EmailConfirmed = GetValueOrDefault<bool>(reader, 17);
                        user.PasswordHash = GetValueOrDefault<string>(reader, 18);
                        user.SecurityStamp = GetValueOrDefault<string>(reader, 19);
                        user.ConcurrencyStamp = GetValueOrDefault<string>(reader, 20);
                        user.PhoneNumber = GetValueOrDefault<string>(reader, 21);
                        user.PhoneNumberConfirmed = GetValueOrDefault<bool>(reader, 22);
                        user.TwoFactorEnabled = GetValueOrDefault<bool>(reader, 23);
                        user.LockoutEnd = GetValueOrDefault<DateTime?>(reader, 24);
                        user.LockoutEnabled = GetValueOrDefault<bool>(reader, 25);
                        user.AccessFailedCount = GetValueOrDefault<int>(reader, 26);
                        user.IsDeleted = GetValueOrDefault<bool>(reader, 27);
                        user.IsMainAdmin = GetValueOrDefault<bool>(reader, 28);
                        user.LastGeneratedIdentityToken = GetValueOrDefault<string>(reader, 29);
                        user.Carbs = GetValueOrDefault<int>(reader, 30);
                        user.Fats = GetValueOrDefault<int>(reader, 31);
                        user.MaintenanceCalories = GetValueOrDefault<int>(reader, 32);
                        user.Protein = GetValueOrDefault<int>(reader, 33);
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
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
                                             "delete from DailyProgress where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from UserExercises where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from GlobalDisplayedPopups where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from UserWorkouts where UserId in (" +
                                                                        "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n" +
                                             "delete from MovedWorkouts where UserId in (" +
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
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {
                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }
            }


        }

        public class JsonUserExercisesReq
        {
            //public static List<DB.JsonUserExercises> GetLastJsonUserExercises()
            //{
            //    var list = new List<DB.JsonUserExercises>();
            //    string query = $"select top(1) jue.* " +
            //                   $"from JsonUserExercises jue\r\n  " +
            //                   $"order by Id desc";
            //    try
            //    {
            //        SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
            //        SqlCommand command = new(query, db);
            //        db.Open();

            //        SqlDataReader reader = command.ExecuteReader();
            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                var row = new DB.JsonUserExercises();
            //                row.Id = GetValueOrDefault<int>(reader, 0);
            //                row.SetDescription = GetValueOrDefault<string>(reader, 1);
            //                row.WorkoutExerciseId = GetValueOrDefault<int>(reader, 2);
            //                row.UserId = GetValueOrDefault<string>(reader, 3);
            //                row.IsDone = GetValueOrDefault<bool>(reader, 4);
            //                row.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
            //                row.IsDeleted = GetValueOrDefault<bool>(reader, 6);
            //                row.UpdateDate = GetValueOrDefault<DateTime>(reader, 7);
            //                row.UserMembershipId = GetValueOrDefault<int>(reader, 8);

            //                list.Add(row);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
            //    }
            //    finally
            //    {

            //        // Забезпечуємо вивільнення ресурсів
            //        SqlConnection.ClearAllPools();
            //    }

            //    return list;
            //}

            public static List<DB.JsonUserExercises> GetJsonUserExercisesByUserId(string userId)
            {
                var list = new List<DB.JsonUserExercises>();
                string query = $"select jue.* " +
                               $"from JsonUserExercises jue\r\n  " +
                               $"where jue.UserId = '{userId}'";
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
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }

                return list;
            }

            //public static List<DB.JsonUserExercises> GetJsonUserExercisesByUserIdLive(string userId)
            //{
            //    var list = new List<DB.JsonUserExercises>();
            //    string query = $"select jue.* " +
            //                   $"from JsonUserExercises jue\r\n  " +
            //                   $"where jue.UserId = '{userId}' and usermembershipId =59997";
            //    try
            //    {
            //        SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
            //        SqlCommand command = new(query, db);
            //        db.Open();

            //        SqlDataReader reader = command.ExecuteReader();
            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                var row = new DB.JsonUserExercises();
            //                row.Id = GetValueOrDefault<int>(reader, 0);
            //                row.SetDescription = GetValueOrDefault<string>(reader, 1);
            //                row.WorkoutExerciseId = GetValueOrDefault<int>(reader, 2);
            //                row.UserId = GetValueOrDefault<string>(reader, 3);
            //                row.IsDone = GetValueOrDefault<bool>(reader, 4);
            //                row.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
            //                row.IsDeleted = GetValueOrDefault<bool>(reader, 6);
            //                row.UpdateDate = GetValueOrDefault<DateTime>(reader, 7);
            //                row.UserMembershipId = GetValueOrDefault<int>(reader, 8);

            //                list.Add(row);
            //            }
            //        }
            //        reader.Close();
            //    }
            //    catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
            //    finally { SqlConnection.ClearAllPools(); }

            //    return list;
            //}

            public static List<DB.JsonUserExercises> GetJsonUserExercisesByWorkoutExerciseId(int? workoutExerciseId)
            {
                var list = new List<DB.JsonUserExercises>();
                string query = $"select jue.* " +
                               $"from JsonUserExercises jue\r\n  " +
                               $"where jue.WorkoutExerciseId = '{workoutExerciseId}';";
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
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }

                return list;
            }

            public static List<(int?, int?, string?)> GetUsersIfJsonUserExercisesIsNull(List<DB.UserMemberships> userMemberships)
            {
                List<(int?, int?, string?)> list = new List<(int?, int?, string?)>();
                int i = 0;
                foreach (var userMembership in userMemberships)
                {

                    if (i < 10)
                    {
                        string query = $"select count(id) " +
                                   $"from JsonUserExercises " +
                                   $"where UserId = '{userMembership.UserId}' and UsermembershipId = {userMembership.Id}";
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
                                    int count = GetValueOrDefault<int>(reader, 0);
                                    if (count == 0)
                                    {
                                        list.Add((userMembership.Id, userMembership.MembershipId, userMembership.UserId));
                                    }

                                }


                            }
                            WaitUntil.WaitSomeInterval(150);
                        }
                        catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}\r\n{userMembership.Id}"); }
                        finally { SqlConnection.ClearAllPools(); }
                        i++;
                        if (i >= 10)
                        {
                            i = 0;
                            WaitUntil.WaitSomeInterval(5000);
                        }
                    }

                }


                return list;
            }

            public static int GetCountOfJsonUserExercises(DB.UserMemberships userMembership)
            {
                var row = new int();
                string query = $" Select count(id) " +
                               $"from JsonUserExercises " +
                               $"where UserId = '{userMembership.UserId}' and UserMembershipId = {userMembership.Id}";
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
                            row = GetValueOrDefault<int>(reader, 0);
                        }
                    }
                    db.Close();
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }

                return row;
            }

            public class Insert
            {
                //public static void InsertWorkoutExercises(int? lastJSONExerciseId, List<DB.JsonUserExercises> listOriginal/*, List<DB.WorkoutExercises> listReplace*/)
                //{
                //    var str = ListHandler(lastJSONExerciseId, listOriginal/*, listReplace*/);
                //    string query = string.Empty;
                //    for (int u =0; u <3; u++)
                //    {
                //        if (u == 0) {
                //            query = "SET IDENTITY_INSERT JsonUserExercises ON" +
                //                   "\r\n\r\nInsert JsonUserExercises " +
                //                   "(Id,\tSetDescription,\tWorkoutExerciseId,\tUserId,\tIsDone,\tCreationDate,\tIsDeleted,\tUpdateDate,\tUserMembershipId)" +
                //                   "\r\nValues\r\n" +
                //                   $"{str.Item1}" +
                //                   "\r\n\r\nSET IDENTITY_INSERT JsonUserExercises OFF";
                //        }
                //        else if(u == 1) {
                //            query = "SET IDENTITY_INSERT JsonUserExercises ON" +
                //                   "\r\n\r\nInsert JsonUserExercises " +
                //                   "(Id,\tSetDescription,\tWorkoutExerciseId,\tUserId,\tIsDone,\tCreationDate,\tIsDeleted,\tUpdateDate,\tUserMembershipId)" +
                //                   "\r\nValues\r\n" +
                //                   $"{str.Item2}" +
                //                   "\r\n\r\nSET IDENTITY_INSERT JsonUserExercises OFF";
                //        }
                //        else if (u == 2)
                //        {
                //            query = "SET IDENTITY_INSERT JsonUserExercises ON" +
                //                   "\r\n\r\nInsert JsonUserExercises " +
                //                   "(Id,\tSetDescription,\tWorkoutExerciseId,\tUserId,\tIsDone,\tCreationDate,\tIsDeleted,\tUpdateDate,\tUserMembershipId)" +
                //                   "\r\nValues\r\n" +
                //                   $"{str.Item3}" +
                //                   "\r\n\r\nSET IDENTITY_INSERT JsonUserExercises OFF";
                //        }

                //        try
                //        {
                //            SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
                //            SqlCommand command = new(query, db);

                //            db.Open();

                //            SqlDataReader reader = command.ExecuteReader();
                //            while (reader.Read())
                //            {
                //                continue;
                //            }
                //            reader.Close();
                //            //var rowsAffected = command.ExecuteNonQueryAsync();
                //            //Console.WriteLine(rowsAffected.Result);



                //        }
                //        catch (Exception ex)
                //        {
                //            throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                //        }
                //        finally
                //        {

                //            // Забезпечуємо вивільнення ресурсів
                //            SqlConnection.ClearAllPools();
                //        }
                //    }

                //}

                private static (string, string, string) ListHandler(int? lastJSONExerciseId, List<DB.JsonUserExercises> listOriginal/*, List<DB.WorkoutExercises> listReplace*/)
                {
                    string row = string.Empty;
                    string row1 = string.Empty;
                    string row2 = string.Empty;
                    int i = 0;
                    //List<int?> originalValues = listOriginal.OrderBy(l=>l.WorkoutExerciseId).Select(l=>l.WorkoutExerciseId).ToList();
                    //List<int?> replacementValues = listReplace.OrderBy(l => l.Id).Select(l => l.Id).ToList();

                    //Dictionary<int?, int?> valueMap = originalValues
                    //    .Zip(replacementValues, (original, replacement) => new { Original = original, Replacement = replacement })
                    //    .ToDictionary(pair => pair.Original, pair => pair.Replacement);


                    foreach (var item in listOriginal)
                    {
                        ++i;
                        if (i <= 1000)
                        {
                            //int? result = valueMap.ContainsKey(item.WorkoutExerciseId.Value)
                            //? valueMap[item.WorkoutExerciseId.Value]
                            //: item.WorkoutExerciseId.Value;
                            if (i <= 999)
                            {
                                row += string.Concat("(", lastJSONExerciseId + i, ",\t\'", item.SetDescription, "\',\t", item.WorkoutExerciseId, ",\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.IsDone.HasValue ? Convert.ToInt32(item.IsDone.Value) : default, ",\t", "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t\'", item.UpdateDate.HasValue ? Convert.ToString(item.UpdateDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff")) : null, "\',\t", 71732, "),\r\n");
                            }
                            else
                            {
                                row += string.Concat("(", lastJSONExerciseId + i, ",\t\'", item.SetDescription, "\',\t", item.WorkoutExerciseId, ",\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.IsDone.HasValue ? Convert.ToInt32(item.IsDone.Value) : default, ",\t", "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t\'", item.UpdateDate.HasValue ? Convert.ToString(item.UpdateDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff")) : null, "\',\t", 71732, ")\r\n");
                            }
                        }
                        else if (i > 1000 && i <= 2000)
                        {
                            //int? result = valueMap.ContainsKey(item.WorkoutExerciseId.Value)
                            //? valueMap[item.WorkoutExerciseId.Value]
                            //: item.WorkoutExerciseId.Value;
                            if (i <= 1999)
                            {
                                row1 += string.Concat("(", lastJSONExerciseId + i, ",\t\'", item.SetDescription, "\',\t", item.WorkoutExerciseId, ",\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.IsDone.HasValue ? Convert.ToInt32(item.IsDone.Value) : default, ",\t", "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t\'", item.UpdateDate.HasValue ? Convert.ToString(item.UpdateDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff")) : null, "\',\t", 71732, "),\r\n");
                            }
                            else
                            {
                                row1 += string.Concat("(", lastJSONExerciseId + i, ",\t\'", item.SetDescription, "\',\t", item.WorkoutExerciseId, ",\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.IsDone.HasValue ? Convert.ToInt32(item.IsDone.Value) : default, ",\t", "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t\'", item.UpdateDate.HasValue ? Convert.ToString(item.UpdateDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff")) : null, "\',\t", 71732, ")\r\n");
                            }
                        }
                        else if (i > 2000)
                        {
                            //int? result = valueMap.ContainsKey(item.WorkoutExerciseId.Value)
                            //? valueMap[item.WorkoutExerciseId.Value]
                            //: item.WorkoutExerciseId.Value;
                            if (item.Id != listOriginal.Last().Id)
                            {
                                row2 += string.Concat("(", lastJSONExerciseId + i, ",\t\'", item.SetDescription, "\',\t", item.WorkoutExerciseId, ",\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.IsDone.HasValue ? Convert.ToInt32(item.IsDone.Value) : default, ",\t", "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t\'", item.UpdateDate.HasValue ? Convert.ToString(item.UpdateDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff")) : null, "\',\t", 71732, "),\r\n");
                            }
                            else
                            {
                                row2 += string.Concat("(", lastJSONExerciseId + i, ",\t\'", item.SetDescription, "\',\t", item.WorkoutExerciseId, ",\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.IsDone.HasValue ? Convert.ToInt32(item.IsDone.Value) : default, ",\t", "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t\'", item.UpdateDate.HasValue ? Convert.ToString(item.UpdateDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff")) : null, "\',\t", 71732, ")\r\n");
                            }
                        }



                    }
                    return (row, row1, row2);
                }
            }
        }

        public class SubAllMemberships
        {
            public static List<DB.SubAllMembershipModel>? GetSubAllMembershipsGroup(int membershipId)
            {
                WaitUntil.WaitSomeInterval(5000);
                var list = new List<DB.SubAllMembershipModel>();
                string query = "SELECT *" +
                               "FROM [SubAllMembershipGroups] " +
                               $"WHERE isDeleted = 0 AND ParentMembershipId = {membershipId}";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.SubAllMembershipModel();
                        row.Id = GetValueOrDefault<int>(reader, 0);
                        row.ParentMembershipId = GetValueOrDefault<int>(reader, 1);
                        row.SubAllMembershipId = GetValueOrDefault<int>(reader, 2);
                        row.Description = GetValueOrDefault<string>(reader, 3);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 4);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 5);
                        list.Add(row);

                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                }
                finally
                {

                    // Забезпечуємо вивільнення ресурсів
                    SqlConnection.ClearAllPools();
                }

                return list;
            }
        }

        public class PagesDb
        {
            public static void GetAllPages(out List<DB.Pages>? pages)
            {
                WaitUntil.WaitSomeInterval(5000);
                pages = new();
                string query = "SELECT *" +
                               "FROM [Pages] " +
                               "WHERE isDeleted = 0";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.Pages();
                        row.Id = GetValueOrDefault<int>(reader, 0);
                        row.Title = GetValueOrDefault<string>(reader, 1);
                        row.NavigationLabel = GetValueOrDefault<string>(reader, 2);
                        row.Content = GetValueOrDefault<string>(reader, 3);
                        row.Order = GetValueOrDefault<int>(reader, 4);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 6);
                        pages.Add(row);
                    }
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }
            }

            public static void GetPagesInMemberships(out List<DB.PagesInMemberships>? pagesInMemberships)
            {
                WaitUntil.WaitSomeInterval(5000);
                pagesInMemberships = new();
                string query = "SELECT *" +
                               "FROM [PagesInMemberships] " +
                               "WHERE isDeleted = 0";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.PagesInMemberships();
                        row.Id = GetValueOrDefault<int>(reader, 0);
                        row.PageId = GetValueOrDefault<int>(reader, 1);
                        row.MembershipId = GetValueOrDefault<int>(reader, 2);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 3);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 4);
                        pagesInMemberships.Add(row);
                    }
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }

            }

        }

        public class VideosHelper
        {
            public static void GetAllVideos(out List<DB.VideosHelper>? videos)
            {
                WaitUntil.WaitSomeInterval(5000);
                videos = new();
                string query = "SELECT *" +
                               "FROM [Videos] " +
                               "WHERE isDeleted = 0";
                try
                {
                    SqlConnection db = new(DB.GET_CONNECTION_STRING);
                    SqlCommand command = new(query, db);
                    db.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var row = new DB.VideosHelper();
                        row.Id = GetValueOrDefault<int>(reader, 0);
                        row.Name = GetValueOrDefault<string>(reader, 1);
                        row.Description = GetValueOrDefault<string>(reader, 2);
                        row.Url = GetValueOrDefault<string>(reader, 3);
                        row.IsForAllMemberships = GetValueOrDefault<bool>(reader, 4);
                        row.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
                        row.IsDeleted = GetValueOrDefault<bool>(reader, 6);
                        videos.Add(row);
                    }
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }
            }

            public static void UpdateAllVideoById(int videoId, string url)
            {
                string query = "UPDATE [Videos] " +
                               $"SET Url = '{url}'" +
                               $"WHERE Id = {videoId}";
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
                }
                catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
                finally { SqlConnection.ClearAllPools(); }

            }
        }

        public class CompletedWorkouts
        {
            //public static List<DB.JsonUserExercises> GetLastJsonUserExercises()
            //{
            //    var list = new List<DB.JsonUserExercises>();
            //    string query = $"select top(1) jue.* " +
            //                   $"from CompletedWorkouts jue\r\n  " +
            //                   $"order by Id desc";
            //    try
            //    {
            //        SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
            //        SqlCommand command = new(query, db);
            //        db.Open();

            //        SqlDataReader reader = command.ExecuteReader();
            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                var row = new DB.JsonUserExercises();
            //                row.Id = GetValueOrDefault<int>(reader, 0);
            //                row.SetDescription = GetValueOrDefault<string>(reader, 1);
            //                row.WorkoutExerciseId = GetValueOrDefault<int>(reader, 2);
            //                row.UserId = GetValueOrDefault<string>(reader, 3);
            //                row.IsDone = GetValueOrDefault<bool>(reader, 4);
            //                row.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
            //                row.IsDeleted = GetValueOrDefault<bool>(reader, 6);
            //                row.UpdateDate = GetValueOrDefault<DateTime>(reader, 7);
            //                row.UserMembershipId = GetValueOrDefault<int>(reader, 8);

            //                list.Add(row);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
            //    }
            //    finally
            //    {

            //        // Забезпечуємо вивільнення ресурсів
            //        SqlConnection.ClearAllPools();
            //    }

            //    return list;
            //}

            //public static List<DB.CompletedWorkouts> GetCompletedWorkoutsByUserIdLive(string userId)
            //{
            //    var list = new List<DB.CompletedWorkouts>();
            //    string query = $"select * " +
            //                   $"from CompletedWorkouts \r\n  " +
            //                   $"where UserId = '{userId}' and usermembershipId =59997";
            //    try
            //    {
            //        SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
            //        SqlCommand command = new(query, db);
            //        db.Open();

            //        SqlDataReader reader = command.ExecuteReader();
            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                var row = new DB.CompletedWorkouts();
            //                row.Id = GetValueOrDefault<int>(reader, 0);
            //                row.WorkoutId = GetValueOrDefault<int>(reader, 1);
            //                row.UserId = GetValueOrDefault<string>(reader, 2);
            //                row.WeekNumber = GetValueOrDefault<int>(reader, 3);
            //                row.CreatedDate = GetValueOrDefault<DateTime>(reader, 4);
            //                row.IsDeleted = GetValueOrDefault<bool>(reader, 5);
            //                row.UserMembershipId = GetValueOrDefault<int>(reader, 6);

            //                list.Add(row);
            //            }
            //        }
            //        reader.Close();
            //    }
            //    catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
            //    finally { SqlConnection.ClearAllPools(); }

            //    return list;
            //}

            public class Insert
            {
                //public static void InsertCompletedWorkouts(int? lastCompletedWorkoutId, List<DB.CompletedWorkouts> listOriginal/*, List<DB.WorkoutExercises> listReplace*/)
                //{
                //    var str = ListHandler(lastCompletedWorkoutId, listOriginal);
                //    string query = "SET IDENTITY_INSERT CompletedWorkouts ON" +
                //                   "\r\n\r\nInsert CompletedWorkouts " +
                //                   "(Id,\tWorkoutId,\tUserId,\tWeekNumber,\tCreationDate,\tIsDeleted,\tUserMembershipId)" +
                //                   "\r\nValues\r\n" +
                //                   $"{str}" +
                //                   "\r\n\r\nSET IDENTITY_INSERT CompletedWorkouts OFF";

                //    try
                //    {
                //        SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
                //        SqlCommand command = new(query, db);

                //        db.Open();

                //        SqlDataReader reader = command.ExecuteReader();
                //        while (reader.Read())
                //        {
                //            continue;
                //        }
                //        reader.Close();
                //        //var rowsAffected = command.ExecuteNonQueryAsync();
                //        //Console.WriteLine(rowsAffected.Result);



                //    }
                //    catch (Exception ex)
                //    {
                //        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                //    }
                //    finally
                //    {

                //        // Забезпечуємо вивільнення ресурсів
                //        SqlConnection.ClearAllPools();
                //    }


                //}

                private static string ListHandler(int? lastCompletedWorkoutId, List<DB.CompletedWorkouts> listOriginal)
                {
                    string row = string.Empty;
                    int i = 0;
                    //List<int?> originalValues = listOriginal.OrderBy(l=>l.WorkoutExerciseId).Select(l=>l.WorkoutExerciseId).ToList();
                    //List<int?> replacementValues = listReplace.OrderBy(l => l.Id).Select(l => l.Id).ToList();

                    //Dictionary<int?, int?> valueMap = originalValues
                    //    .Zip(replacementValues, (original, replacement) => new { Original = original, Replacement = replacement })
                    //    .ToDictionary(pair => pair.Original, pair => pair.Replacement);


                    foreach (var item in listOriginal)
                    {
                        ++i;

                        if (item.Id != listOriginal.Last().Id)
                        {
                            row += string.Concat("(", lastCompletedWorkoutId + i, ",\t", item.WorkoutId, "\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.WeekNumber, ",\t", "\'", item.CreatedDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t", 71732, "),\r\n");
                        }
                        else
                        {
                            row += string.Concat("(", lastCompletedWorkoutId + i, ",\t", item.WorkoutId, "\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.WeekNumber, ",\t", "\'", item.CreatedDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t", 71732, ")\r\n");
                        }


                    }
                    return row;
                }
            }
        }

        public class UserRelatedExercises
        {
            //public static List<DB.UserRelatedExercises> GetUserRelatedExercisesByUserIdLive(string userId)
            //{
            //    var list = new List<DB.UserRelatedExercises>();
            //    string query = $"select * " +
            //                   $"from UserRelatedExercises \r\n  " +
            //                   $"where UserId = '{userId}' and usermembershipId =59997";
            //    try
            //    {
            //        SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
            //        SqlCommand command = new(query, db);
            //        db.Open();

            //        SqlDataReader reader = command.ExecuteReader();
            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                var row = new DB.UserRelatedExercises();
            //                row.Id = GetValueOrDefault<int>(reader, 0);
            //                row.UserId = GetValueOrDefault<string>(reader, 1);
            //                row.WeekNumber = GetValueOrDefault<int>(reader, 2);
            //                row.WorkoutExerciseId = GetValueOrDefault<int>(reader, 3);
            //                row.ExerciseId = GetValueOrDefault<int>(reader, 4);
            //                row.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
            //                row.IsDeleted = GetValueOrDefault<bool>(reader, 6);
            //                row.UserMembershipId = GetValueOrDefault<int>(reader, 7);
            //                row.ExerciseType = GetValueOrDefault<bool>(reader, 8);

            //                list.Add(row);
            //            }
            //        }
            //        reader.Close();
            //    }
            //    catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
            //    finally { SqlConnection.ClearAllPools(); }

            //    return list;
            //}

            public class Insert
            {
                //public static void InsertUserRelatedExercises(int? lastUserRelatedExerciseId, List<DB.UserRelatedExercises> listOriginal/*, List<DB.WorkoutExercises> listReplace*/)
                //{
                //    var str = ListHandler(lastUserRelatedExerciseId, listOriginal);
                //    string query = "SET IDENTITY_INSERT UserRelatedExercises ON" +
                //                   "\r\n\r\nInsert UserRelatedExercises " +
                //                   "(Id,\tUserId,\tWeekNumber,\tWorkoutExerciseId,\tExerciseId,\tCreationDate,\tIsDeleted,\tUserMembershipId,\tExerciseType)" +
                //                   "\r\nValues\r\n" +
                //                   $"{str}" +
                //                   "\r\n\r\nSET IDENTITY_INSERT UserRelatedExercises OFF";

                //    try
                //    {
                //        SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
                //        SqlCommand command = new(query, db);

                //        db.Open();

                //        SqlDataReader reader = command.ExecuteReader();
                //        while (reader.Read())
                //        {
                //            continue;
                //        }
                //        reader.Close();
                //        //var rowsAffected = command.ExecuteNonQueryAsync();
                //        //Console.WriteLine(rowsAffected.Result);



                //    }
                //    catch (Exception ex)
                //    {
                //        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                //    }
                //    finally
                //    {

                //        // Забезпечуємо вивільнення ресурсів
                //        SqlConnection.ClearAllPools();
                //    }


                //}

                private static string ListHandler(int? lastUserRelatedExerciseId, List<DB.UserRelatedExercises> listOriginal)
                {
                    string row = string.Empty;
                    int i = 0;
                    //List<int?> originalValues = listOriginal.OrderBy(l=>l.WorkoutExerciseId).Select(l=>l.WorkoutExerciseId).ToList();
                    //List<int?> replacementValues = listReplace.OrderBy(l => l.Id).Select(l => l.Id).ToList();

                    //Dictionary<int?, int?> valueMap = originalValues
                    //    .Zip(replacementValues, (original, replacement) => new { Original = original, Replacement = replacement })
                    //    .ToDictionary(pair => pair.Original, pair => pair.Replacement);


                    foreach (var item in listOriginal)
                    {
                        ++i;

                        if (item.Id != listOriginal.Last().Id)
                        {
                            row += string.Concat("(", lastUserRelatedExerciseId + i, ",\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.WeekNumber, ",\t", item.WorkoutExerciseId, ",\t", item.ExerciseId, "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t", 71732, ",\t", item.ExerciseType.HasValue ? Convert.ToInt32(item.ExerciseType.Value) : default, "),\r\n");
                        }
                        else
                        {
                            row += string.Concat("(", lastUserRelatedExerciseId + i, ",\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.WeekNumber, ",\t", item.WorkoutExerciseId, ",\t", item.ExerciseId, "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t", 71732, ",\t", item.ExerciseType.HasValue ? Convert.ToInt32(item.ExerciseType.Value) : default, ")\r\n");
                        }


                    }
                    return row;
                }
            }
        }

        public class WorkoutUserNotes
        {
            //public static List<DB.WorkoutUserNotes> GetUserRelatedExercisesByUserIdLive(string userId)
            //{
            //    var list = new List<DB.WorkoutUserNotes>();
            //    string query = $"select * " +
            //                   $"from WorkoutUserNotes \r\n  " +
            //                   $"where UserId = '{userId}' and usermembershipId =59997";
            //    try
            //    {
            //        SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
            //        SqlCommand command = new(query, db);
            //        db.Open();

            //        SqlDataReader reader = command.ExecuteReader();
            //        if (reader.HasRows)
            //        {
            //            while (reader.Read())
            //            {
            //                var row = new DB.WorkoutUserNotes();
            //                row.Id = GetValueOrDefault<int>(reader, 0);
            //                row.Notes = GetValueOrDefault<string>(reader, 1);
            //                row.WorkoutExerciseId = GetValueOrDefault<int>(reader, 2);
            //                row.UserId = GetValueOrDefault<string>(reader, 3);
            //                row.WeekNumber = GetValueOrDefault<int>(reader, 4);
            //                row.CreationDate = GetValueOrDefault<DateTime>(reader, 5);
            //                row.IsDeleted = GetValueOrDefault<bool>(reader, 6);
            //                row.UserMembershipId = GetValueOrDefault<int>(reader, 7);

            //                list.Add(row);
            //            }
            //        }
            //        reader.Close();
            //    }
            //    catch (Exception ex) { throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}"); }
            //    finally { SqlConnection.ClearAllPools(); }

            //    return list;
            //}

            public class Insert
            {
                //public static void InsertUserRelatedExercises(int? lastWorkoutUserNoteId, List<DB.WorkoutUserNotes> listOriginal/*, List<DB.WorkoutExercises> listReplace*/)
                //{
                //    var str = ListHandler(lastWorkoutUserNoteId, listOriginal);
                //    string query = "SET IDENTITY_INSERT WorkoutUserNotes ON" +
                //                   "\r\n\r\nInsert WorkoutUserNotes " +
                //                   "(Id,\tNotes,\tWorkoutExerciseId,\tUserId,\tWeekNumber,\tCreationDate,\tIsDeleted,\tUserMembershipId)" +
                //                   "\r\nValues\r\n" +
                //                   $"{str}" +
                //                   "\r\n\r\nSET IDENTITY_INSERT WorkoutUserNotes OFF";

                //    try
                //    {
                //        SqlConnection db = new(DB.GET_CONNECTION_STRING_Live);
                //        SqlCommand command = new(query, db);

                //        db.Open();

                //        SqlDataReader reader = command.ExecuteReader();
                //        while (reader.Read())
                //        {
                //            continue;
                //        }
                //        reader.Close();
                //        //var rowsAffected = command.ExecuteNonQueryAsync();
                //        //Console.WriteLine(rowsAffected.Result);



                //    }
                //    catch (Exception ex)
                //    {
                //        throw new ArgumentException($"Error: {ex.Message}\r\n{ex.StackTrace}");
                //    }
                //    finally
                //    {

                //        // Забезпечуємо вивільнення ресурсів
                //        SqlConnection.ClearAllPools();
                //    }


                //}

                private static string ListHandler(int? lastWorkoutUserNoteId, List<DB.WorkoutUserNotes> listOriginal)
                {
                    string row = string.Empty;
                    int i = 0;
                    //List<int?> originalValues = listOriginal.OrderBy(l=>l.WorkoutExerciseId).Select(l=>l.WorkoutExerciseId).ToList();
                    //List<int?> replacementValues = listReplace.OrderBy(l => l.Id).Select(l => l.Id).ToList();

                    //Dictionary<int?, int?> valueMap = originalValues
                    //    .Zip(replacementValues, (original, replacement) => new { Original = original, Replacement = replacement })
                    //    .ToDictionary(pair => pair.Original, pair => pair.Replacement);


                    foreach (var item in listOriginal)
                    {
                        ++i;

                        if (item.Id != listOriginal.Last().Id)
                        {
                            row += string.Concat("(", lastWorkoutUserNoteId + i, ",\t\'", item.Notes, "\',\t", item.WorkoutExerciseId, ",\t", ",\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.WeekNumber, ",\t", "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t", 71732, "),\r\n");
                        }
                        else
                        {
                            row += string.Concat("(", lastWorkoutUserNoteId + i, ",\t\'", item.Notes, "\',\t", item.WorkoutExerciseId, ",\t", ",\t\'", "39cf9d72-62a0-4936-a5a5-0edd1246b595", "\',\t", item.WeekNumber, ",\t", "\'", item.CreationDate.Value.ToString("yyyy-MM-dd hh:mm:ss.fffffff"), "\',\t", item.IsDeleted.HasValue ? Convert.ToInt32(item.IsDeleted.Value) : default, ",\t", 71732, ")\r\n");
                        }


                    }
                    return row;
                }
            }
        }
    }
}
