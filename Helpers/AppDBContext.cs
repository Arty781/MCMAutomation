using MCMAutomation.PageObjects;
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

        public static string[] GetUsersData()
        {
            var list = new List<string>();
            using (SqlConnection db = new(DB.GetConnectionString))
            {
                SqlCommand command = new("SELECT TOP(1) *" +
                                         "FROM [AspNetUsers] where email like 'qatester%@xitroo.com' " +
                                         "ORDER BY DateTime DESC", db);
                db.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetValue(3).ToString());
                    }
                }

            }
            string[] data = list.ToArray();

            return data;
        }

        public static string[] GetExercisesData()
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

            string[] exercise = list.ToArray();
            return exercise;
        }

        public static string[] GetLastMembership()
        {
            
            WaitUntil.CustomElevemtIsVisible(Pages.MembershipAdmin.membershipTitleElem, 90);
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
                        string str = reader.GetString(2);

                        list.Add(str);

                    }
                }

            }

            string[] exercise = list.ToArray();
            return exercise;
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

        public static string[] GetActiveMembershipsByEmail(string email)
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

            string[] nameMembership = list.ToArray();
            return nameMembership;
        }

        public static string[] GetActiveMembershipsBySKU(string SKU)
        {

            var list = new List<string>();

            using (SqlConnection db = new(DB.GetConnectionString))
            {
                SqlCommand command = new("SELECT TOP(1) Name, SKU " +
                                         "FROM Memberships WHERE SKU LIKE @SKU", db);
                command.Parameters.AddWithValue("@SKU", DbType.String).Value = SKU + "%";
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

            string[] nameMembership = list.ToArray();
            return nameMembership;
        }

        public static string[] GetUserData(string userEmail)
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
            string[] data = list.ToArray();

            return data;
        }
    }
}
