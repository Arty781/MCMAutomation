using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.Helpers
{
    public class Data
    {
        public object Id { get; set; }
        public object SetDescription { get; set; } // this might be another data type
        public object WorkoutExerciseId { get; set; }
        public object UserId { get; set; }
        public object IsDone { get; set; }
        public object CreationDate { get; set; }
        public object IsDeleted { get; set; }
        public object UpdatedDate { get; set; }
    }
    public class AppDbContext
    {
       
        public static List<Data> GetUserExercisesList(string userEmail, string membershipName)
        {
            var list = new List<Data>();
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
                        var row = new Data();
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


    }
}
