﻿using MCMAutomation.APIHelpers;
using MCMAutomation.Helpers;
using MCMAutomation.PageObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{

    public class AppDbContext
    {
       
        public static List<JsonUserExercises> GetUserExercisesList(string userEmail, string membershipName)
        {
            var list = new List<JsonUserExercises>();
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("SELECT *" +
                                         "FROM [JsonUserExercises] WHERE UserId in " +
                                         "(SELECT id FROM[dbo].[AspNetUsers] WHERE email = @userEmail) and WorkoutExerciseId in" +
                                            "(SELECT Id FROM WorkoutExercises WHERE WorkoutId in " +
                                                "(SELECT Id FROM Workouts WHERE ProgramId in " +
                                                    "(SELECT Id FROM Programs WHERE MembershipId in " +
                                                        "(SELECT Id FROM Memberships WHERE Name = @membershipName)" +
                                                    ")" +
                                                ")" +
                                            ")", db);
                command.Parameters.AddWithValue("@userEmail", DbType.String).Value = userEmail;
                command.Parameters.AddWithValue("@membershipName", DbType.String).Value = membershipName;
                db.Open();
                
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
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

            }
            return list;
        }

        public static List<DB.Exercises> GetExercisesData()
        {
            var list = new List<DB.Exercises>();

            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("SELECT *" +
                                         "FROM [Exercises] WHERE IsDeleted=0", db);
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var row = new DB.Exercises()
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            CreationDate = reader.GetDateTime(2),
                            IsDeleted = reader.GetBoolean(3),
                            VideoURL = reader.GetString(4),
                            TempoBold = reader.GetInt32(5)
                        };
                        list.Add(row);
                    }
                }

            }
            return list;
        }

        public static List<DB.Workouts> GetLastWorkoutsData()
        {
            var list = new List<DB.Workouts>();

            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("SELECT TOP(7)*" +
                                         "FROM [Workouts] WHERE IsDeleted=0" +
                                         "ORDER BY CreationDate DESC", db);
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
            return list;
        }


        public static List<string> GetMembershipProgramWorkoutData()
        {
            var list = new List<string>();

            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("Select top(1) m.Name, p.Name, w.Name from Programs p " + 
                    "inner join Memberships m on m.Id = p.MembershipId " +
                    "inner join Workouts w on w.ProgramId = p.Id " +
                    "where p.NumberOfWeeks = 4 " +
                    "and m.name like 'The Challenge%' " +
                    "and p.IsDeleted = 0 " +
                    "and m.IsDeleted = 0 " +
                    "and w.IsDeleted = 0;", db);
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                        list.Add(reader.GetString(1));
                        list.Add(reader.GetString(2));

                    }
                }

            }

            return list;
        }

        public static List<DB.Memberships>? GetLastMembership()
        {
            var list = new List<DB.Memberships?>();
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("SELECT TOP(1)*" +
                                         "FROM [Memberships] WHERE IsDeleted=0" +
                                         "ORDER BY CreationDate DESC", db);
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       var row = new DB.Memberships()
                        {
                            Id = reader.GetInt32(0),
                            SKU = reader.GetString(1),
                            Name = reader.GetString(2),
                            Description = reader.GetString(3),
                            StartDate = null,
                            EndDate = null,
                            URL = reader.GetString(6),
                            Price = reader.GetDecimal(7),
                            CreationDate = reader.GetDateTime(8),
                            IsDeleted = reader.GetBoolean(9),
                            IsCustom = reader.GetBoolean(10),
                            ForPurchase = reader.GetBoolean(11),
                            AccessWeekLength = reader.GetInt32(12),
                            RelatedMembershipGroupId = null,
                            Gender = reader.GetInt32(14),
                            PromotionalPopupId = null,
                            Type = reader.GetInt32(16)
                        };
                        list.Add(row);

                    }
                }
            }

            return list;
        }

        public static List<DB.Programs> GetLastPrograms()
        {
            var list = new List<DB.Programs>();
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("SELECT TOP(5) * " +
                                         "FROM [Programs] WHERE IsDeleted=0 " +
                                         "ORDER BY CreationDate DESC", db);
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var row = new DB.Programs()
                        {
                            Id = reader.GetInt32(0),
                            MembershipId = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            NumberOfWeeks = reader.GetInt32(3),
                            CreationDate = reader.GetDateTime(4),
                            IsDeleted = reader.GetBoolean(5),
                            Steps = reader.GetString(6),
                            AvailableDate = null,
                            NextProgramId = null,
                            ExpirationDate = null,
                            Type = reader.GetInt32(10)
                        };
                        list.Add(row);
                    }
                }
            }

            return list;
        }

        public static string[] GetExerciseStatus()
        {

            var list = new List<string>();

            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("SELECT TOP(1)*" +
                                         "FROM [Exercises] " +
                                         "ORDER BY CreationDate DESC", db);
                
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var str = reader.GetValue(3).ToString();

                        list.Add(str);

                    }
                }

            }
            string[] status = list.ToArray();

            return status;
        }

        public static int GetLastUsermembershipId(string userEmail)
        {
            int str = 0;
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("Select top(1) id from UserMemberships\r\n" +
                                         "where UserId in (" +
                                         "Select id FROM AspNetUsers where Email = @email)\r\n" +
                                         "order by CreationDate desc", db);
                command.Parameters.AddWithValue("@email", DbType.String).Value = userEmail;
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        str = reader.GetInt32(0);
                    }
                }

            }

            return str;
        }

        public static List<int> GetTop2UsermembershipId(string userEmail)
        {
            List<int> str = new List<int>();
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("Select top(2) id from UserMemberships\r\n" +
                                         "where UserId in (" +
                                         "Select id FROM AspNetUsers where Email = @email)\r\n" +
                                         "order by CreationDate ASC", db);
                command.Parameters.AddWithValue("@email", DbType.String).Value = userEmail;
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        str.Add(reader.GetInt32(0));
                    }
                }

            }

            return str;
        }

        public static void UpdateTop2UsermembershipToExpire(int usermembershipId)
        {
            List<int> str = new List<int>();
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("Update UserMemberships set " +
                                         "StartOn = DateAdd(MM, -3, StartOn) " +
                                         "where Id = @usermembershipId and isDeleted = 0", db);
                command.Parameters.AddWithValue("@usermembershipId", DbType.Int32).Value = usermembershipId;
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        continue;
                    }
                }

            }
        }

        public static void UpdateTop2UsermembershipToComingSoon(int usermembershipId)
        {
            List<int> str = new List<int>();
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("Update UserMemberships set " +
                                         "StartOn = DateAdd(MM, 5, StartOn) " +
                                         "where Id = @usermembershipId and isDeleted = 0", db);
                command.Parameters.AddWithValue("@usermembershipId", DbType.Int32).Value = usermembershipId;
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        continue;
                    }
                }

            }
        }

        public static List<string> GetActiveMembershipsByEmail(string email)
        {
            
            var list = new List<string>();

            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("SELECT Name, SKU " +
                                         "FROM Memberships WHERE Id in (Select MembershipId from UserMemberships " +
                                         "WHERE UserId in (select Id from[AspNetUsers] where Email like @email) and isDeleted = 0 and active = 1)", db);
                command.Parameters.AddWithValue("@email", DbType.String).Value = email;
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                        list.Add(reader.GetString(1));
                    }
                }

            }
            return list;
        }

        public static string GetActiveMembershipsBySKU(string SKU)
        {

            string nameMembership = null;

            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("SELECT TOP(1) Name, SKU " +
                                         "FROM Memberships WHERE SKU LIKE @SKU AND IsDeleted = 0 ORDER BY CreationDate DESC", db);
                command.Parameters.AddWithValue("@SKU", DbType.String).Value = String.Concat(SKU + "%");
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        nameMembership = reader.GetString(0).ToString();
                    }
                }

            }

            return nameMembership;
        }

        public static List<string> GetSubProdAndCustomMemberships()
        {
            List<string> list = new List<string>();

            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new(String.Concat("Select top(1) Name from memberships " +
                                                        "where Type = 0 And Isdeleted = 0 " +
                                                        "order by creationDate desc"), db);
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        list.Add(reader.GetString(0));
                    }
                }

            }
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new(String.Concat("Select top(1) Name from memberships " +
                                                        "where Type = 1 And Isdeleted = 0 " +
                                                        "order by creationDate desc"), db);
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        list.Add(reader.GetString(0));
                    }
                }

            }

            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new(String.Concat("Select top(1) Name from memberships " +
                                                       "where Type = 0 and SKU is null And Isdeleted = 0 " +
                                                       "order by creationDate desc"), db);
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        list.Add(reader.GetString(0));
                    }
                }

            }

            return list;
        }

        public static void UpdateUserProgressDate(string userId)
        {
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("UPDATE [Progress] set " +
                                         "CreationDate = DateAdd(DD, -7, CreationDate) where UserId = @userId", db);
                command.Parameters.AddWithValue("@userId", DbType.String).Value = userId;
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        continue;
                    }
                }
            }
        }

        #region UserOptions

        public static List<string> GetUserData(string userEmail)
        {
            var list = new List<string>();
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("SELECT TOP(1) *" +
                                         "FROM [AspNetUsers] where email like @userEmail " +
                                         "ORDER BY DateTime DESC", db);
                command.Parameters.AddWithValue("@userEmail", DbType.String).Value = userEmail;
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        list.Add(reader.GetValue(6).ToString());
                        list.Add(reader.GetValue(7).ToString());
                        list.Add(reader.GetValue(8).ToString());
                        list.Add(reader.GetValue(10).ToString());
                        list.Add(reader.GetValue(3).ToString());
                    }
                }

            }

            return list;
        }

        public static string GetUserEmail()
        {
            string data = null;
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("SELECT TOP(1) *" +
                                         "FROM [AspNetUsers] where email like '%qatester2022%@xitroo.com' AND IsDeleted = 0" +
                                         "ORDER BY DateTime DESC", db);
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data = reader.GetValue(3).ToString();
                    }
                }
            }
            return data;
        }

        public static string GetUserId(string email)
        {
            string data = null;
            using (SqlConnection db = new(DB.GET_CONNECTION_STRING))
            {
                SqlCommand command = new("SELECT TOP(1) *" +
                                         "FROM [AspNetUsers] where email = @email  AND IsDeleted = 0" +
                                         "ORDER BY DateTime DESC", db);
                command.Parameters.AddWithValue("@email", DbType.String).Value = email;
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data = reader.GetValue(0).ToString();
                    }
                }

            }

            return data;
        }

        public static void DeleteUser (string email)
        {
            using SqlConnection db = new(DB.GET_CONNECTION_STRING);
            SqlCommand command = new(String.Concat(
                "delete from DeletedUserExercises where UserId in (" +
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
                "delete from AspNetUsers where email like @email"), db);
            command.Parameters.AddWithValue("@email", DbType.String).Value = email;
            command.Parameters.AddWithValue("@userId", DbType.String).Value = "c5c91cc8-dd78-4bba-aab6-471830c3d28e";
            db.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    continue;
                }
            }
        }
        #endregion
    }
}
