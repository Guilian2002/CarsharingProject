using Projet.Models.DAL.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Projet.Models.DAL
{
    public class CarDAL : ICarDAL
    {
        private string connectionstring;
        public CarDAL(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }
        public bool InsertInto(Car car, int user_id)
        {
            bool success;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Car(FK_user_id,model_name,number_seat) VALUES(@user_id,@model_name,@number_seats)", connection);
                cmd.Parameters.AddWithValue("user_id", user_id);
                cmd.Parameters.AddWithValue("model_name", car.ModelName);
                cmd.Parameters.AddWithValue("number_seats", car.NbrPlaces);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                success = res > 0;
            }
            return success;
        }
        public List<Car> GetUserCar(int? user_id)
        {
            List<Car> cars = new List<Car>();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                
                SqlCommand command = new SqlCommand("SELECT model_name,number_seat FROM dbo.Car WHERE FK_user_id=@id", connection);
                command.Parameters.AddWithValue("id", user_id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cars.Add(new(reader.GetString("model_name"), reader.GetInt32("number_seat")));
                    }
                }
            }
            return cars;
        }
        public int GetCarId(Car car)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {

                SqlCommand command = new SqlCommand("SELECT car_id FROM dbo.Car WHERE model_name=@model and number_seat=@seats", connection);
                command.Parameters.AddWithValue("model", car.ModelName);
                command.Parameters.AddWithValue("seats", car.NbrPlaces);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32("car_id");
                    }

                }
            }
            return 0;
        }
        public List<int?> GetAllDriverId()
        {
            List<int?> result = new List<int?>();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {

                SqlCommand command = new SqlCommand("SELECT u.user_id FROM dbo.Users u INNER JOIN dbo.Car c ON u.user_id = c.FK_user_id " +
                    "WHERE c.FK_user_id IS NOT NULL", connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            return result;
        }
    }
}
