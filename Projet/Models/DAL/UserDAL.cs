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
        public User GetUser(int user_id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Users WHERE user_id=@id",connection);
                command.Parameters.AddWithValue("id",user_id);
                connection.Open ();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(reader.GetString("Lastname"), reader.GetString("Firstname"), reader.GetString("street_name"), reader.GetString("Postal_code"), reader.GetString("city"), reader.GetInt32("House_number"), reader.GetString("email"), "crypted");
                    }else
                    {
                        return null;
                    }
                }
            }
        }
		public User TakeUser(int? user_id)
		{
			User user = new User();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("SELECT lastname, firstname, street_name, postal_code," +
					" city, house_number, email, password" +
					" FROM dbo.Users WHERE user_id=@userId", connection);
				cmd.Parameters.AddWithValue("userId", user_id);
				connection.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						user.Firstname = reader.GetString("lastname");
						user.Lastname = reader.GetString("firstname");
						user.Street_name = reader.GetString("street_name");
						user.Postal_code = reader.GetString("postal_code");
						user.City = reader.GetString("city");
						user.House_number = reader.GetInt32("house_number");
						user.Email = reader.GetString("email");
						user.Password = reader.GetString("password");
						return user;
					}
				}
			}
			return null;
		}
        public List<User> GetPassengersByDriverId(int? driverId)
        {
            int i = 0;
            List<User> result = new List<User>();
            User user = new User();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand("SELECT u.lastname, u.firstname FROM dbo.Users u INNER JOIN dbo.Car c ON u.user_id = c.FK_user_id " +
                    "INNER JOIN dbo.Carsharing ca ON c.car_id = ca.FK_car_id INNER JOIN dbo.PassengerOffer p ON ca.carsharing_id = p.FK_carsharing_id " +
                    "WHERE type = @type and c.FK_user_id = @driverId and u.user_id != @driver_id", connection);
                command.Parameters.AddWithValue("type", "offer");
                command.Parameters.AddWithValue("driverId", driverId);
                command.Parameters.AddWithValue("driver_id", driverId);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user.Lastname = reader.GetString(0);
                            user.Firstname = reader.GetString(1);
                            result.Add(user);
                            i++;
                        }
                    }
                }
            }
            return result;
        }
		public User GetDriver(int? driverId)
        {
			User driver = new User();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("SELECT lastname, firstname FROM dbo.Users WHERE user_id=@driverId", connection);
				cmd.Parameters.AddWithValue("driverId", driverId);
				connection.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						driver.Firstname = reader.GetString(0);
						driver.Lastname = reader.GetString(1);
						return driver;
					}
				}
			}
			return null;
		}
	}
}
