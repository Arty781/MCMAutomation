using MCMAutomation.APIHelpers;
using MCMAutomation.PageObjects;
using NUnit.Framework.Internal;
using RimuTec.Faker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{

    public class AppDbContext
    {
       
        public static List<JsonUserExercises> GetUserExercisesList(string userEmail, string membershipName)
        {
            var list = new List<JsonUserExercises>();
            using (SqlConnection db = new(DB.GetConnectionString))
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


        public static List<string> GetExercisesData()
        {
            var list = new List<string>();
            
            using (SqlConnection db = new(DB.GetConnectionString))
            {
                SqlCommand command = new("SELECT *" +
                                         "FROM [Exercises] WHERE IsDeleted=0", db);
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string str = reader.GetString(1);

                        list.Add(str);
                        
                    }
                }
                
            }
            return list;
        }

        public static List<string> GetMembershipProgramWorkoutData()
        {
            var list = new List<string>();

            using (SqlConnection db = new(DB.GetConnectionString))
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

        public static List<string> GetLastMembership()
        {
            var list = new List<string>();
            using (SqlConnection db = new(DB.GetConnectionString))
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
                        list.Add(reader.GetInt32(0).ToString());
                        list.Add(reader.GetString(2).ToString());
                    }
                }
            }

            return list;
        }

        public static List<int> GetLastPrograms()
        {
            var list = new List<int>();
            using (SqlConnection db = new(DB.GetConnectionString))
            {
                SqlCommand command = new("SELECT TOP(3) Id " +
                                         "FROM [Programs] WHERE IsDeleted=0 " +
                                         "ORDER BY CreationDate DESC", db);
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetInt32(0));
                    }
                }
            }

            return list;
        }

        public static string[] GetExerciseStatus()
        {

            var list = new List<string>();

            using (SqlConnection db = new(DB.GetConnectionString))
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

        public static List<string> GetActiveMembershipsByEmail(string email)
        {
            
            var list = new List<string>();

            using (SqlConnection db = new(DB.GetConnectionString))
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

            using (SqlConnection db = new(DB.GetConnectionString))
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

            using (SqlConnection db = new(DB.GetConnectionString))
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
            using (SqlConnection db = new(DB.GetConnectionString))
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

            using (SqlConnection db = new(DB.GetConnectionString))
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
            using (SqlConnection db = new(DB.GetConnectionString))
            {
                SqlCommand command = new("UPDATE [Progress] set " +
                                         "CreationDate = DateAdd(DD, -7, CreationDate) where UserId = @userId " +
                                         "Select * from [Progress] where UserId = @userId", db);
                command.Parameters.AddWithValue("@userId", DbType.String).Value = userId;
                db.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reader.GetValue(6).ToString();
                    }
                }
            }
        }

        #region UserOptions
        public static List<string> GetUserData(string userEmail)
        {
            var list = new List<string>();
            using (SqlConnection db = new(DB.GetConnectionString))
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
            using (SqlConnection db = new(DB.GetConnectionString))
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
            using (SqlConnection db = new(DB.GetConnectionString))
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
            using SqlConnection db = new(DB.GetConnectionString);
            SqlCommand command = new(String.Concat(
                "delete from DeletedUserExercises where UserId in (" +
                                                                    "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n\r\n" +
                "delete from UserRelatedExercises where UserId in (" +
                                                                    "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n\r\n" +
                "delete from CompletedWorkouts where UserId in (" +
                                                                    "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n\r\n" +
                "delete from WorkoutUserNotes where UserId in (" +
                                                                    "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n\r\n" +
                "delete from [JsonUserExercises] where UserId in (" +
                                                                    "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n\r\n" +
                "Update UserMemberships set isdeleted = 1, UserId=null where UserId in (" +
                                                                    "SELECT Id FROM [AspNetUsers] where Email like @email)\r\n\r\n" +
                "delete from Media where ProgressId in (" +
                                                                    "SELECT Id FROM Progress where UserId in (" +
                                                                    "SELECT id FROM [dbo].[AspNetUsers] where email like @email))\r\n\r\n" +
                "delete from Progress where UserId in (" +
                                                                    "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n\r\n" +
                "delete from UserExercises where UserId in (" +
                                                                    "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n\r\n" +
                "delete from GlobalDisplayedPopups where UserId in (" +
                                                                    "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n\r\n" +
                "delete from Logins where UserId in (" +
                                                                    "SELECT id FROM [dbo].[AspNetUsers] where email like @email)\r\n\r\n" +
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
