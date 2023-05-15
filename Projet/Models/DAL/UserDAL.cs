using Projet.Models.DAL.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Projet.Models.DAL
{
    public class UserDAL : IUserDAL
    {
        private string connectionString;
        public UserDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public bool InsertInto(User user)
        {
            bool success;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Users(lastname,firstname,street_name,postal_code,city,house_number,email,password) VALUES(@Lastname,@Firstname,@street_name,@Postal_code,@City,@House_number,@email,@pass)",connection);
                cmd.Parameters.AddWithValue("Lastname", user.Lastname);
                cmd.Parameters.AddWithValue("Firstname", user.Firstname);
                cmd.Parameters.AddWithValue("street_name", user.Street_name);
                cmd.Parameters.AddWithValue("Postal_code", user.Postal_code);
                cmd.Parameters.AddWithValue("city", user.City);
                cmd.Parameters.AddWithValue("House_number", user.House_number);
                cmd.Parameters.AddWithValue("email", user.Email);
                cmd.Parameters.AddWithValue("pass", user.Password);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                 success = res > 0;
            }
            return success;
        }
        public int TakeUserId(User user)
        {
            int user_id;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT user_id FROM dbo.Users WHERE lastname=@lastname and firstname=@firstname", connection);
                cmd.Parameters.AddWithValue("lastname", user.Lastname);
                cmd.Parameters.AddWithValue("firstname", user.Firstname);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user_id = reader.GetInt32("user_id");
                        return user_id;
                    }
                    
                }
            }
            return 0;
        }
        public int VerifyIdentifiers(User currUser)
        {
            
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT password,user_id FROM dbo.Users WHERE email=@email", connection);
                cmd.Parameters.AddWithValue("email",currUser.Email);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader.GetString("password") == currUser.Password)
                        {
                            Console.WriteLine("good");
                            return reader.GetInt32("user_id");

                        }
                        else
                        {
                            Console.WriteLine("pass");
                            throw new Exception("Unknow password");
                        }
                    }

                    else 
                    {
                        Console.WriteLine("email");
                        throw new Exception("Unknow user");
                        
                    }

                }
            }
        }
    }
}
