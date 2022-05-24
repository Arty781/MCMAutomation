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

        public static List<User> GetUserData()
        {
            var list = new List<User>();
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
                        var row = new User();
                        row.Email = reader.GetValue(3);


                        list.Add(row);
                    }
                }

            }
            return list;
        }


    }
}
