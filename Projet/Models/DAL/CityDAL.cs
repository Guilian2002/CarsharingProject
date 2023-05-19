using Projet.Models.DAL.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Projet.Models.DAL
{
    public class CityDAL:ICityDAL
    {
        private string connectionstring;
        public CityDAL(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }
        public City GetArrivalCity(int carsharing_id)
        {
            using ( SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand("SELECT city_name,passage_hour,MAX(city_order) FROM dbo.City_Path p INNER  JOIN dbo.City c ON c.city=p.city_id WHERE carsharing_id=@carsharing_id MIN(city_order)");
                command.Parameters.AddWithValue("carsharing_id", carsharing_id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new City(reader.GetString("city_name"), reader.GetDateTime("passage_hour"));
                    }
                    return null;
                }
            }
        }
        public City GetDepartureCity(int carsharing_id)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand("SELECT city_name,passage_hour,MIN(city_order) FROM dbo.City_Path p INNER  JOIN dbo.City c ON c.city_id=p.FK_city_id WHERE FK_carsharing_id=@carsharing_id GROUP BY city_name,passage_hour",connection) ;
                command.Parameters.AddWithValue("carsharing_id", carsharing_id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new City(reader.GetString("city_name"));
                    }
                    return null;
                }
            }
        }
        public int Save(City city)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                SqlCommand command = new SqlCommand("INSERT INTO dbo.City(city_name,passage_hour) VALUES(@name,@hour)", connection);
                command.Parameters.AddWithValue("name", city.Name);
                command.Parameters.AddWithValue("hour", city.PassageHour);
                connection.Open();
                connection.Close();
                SqlCommand command2 = new SqlCommand("SELECT city_id FROM dbo.City WHERE city_name=@name and passage_hour=@hour", connection);
                command2.Parameters.AddWithValue("name", city.Name);
                command2.Parameters.AddWithValue("hour", city.PassageHour);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetInt32("carsharing_id");
                    }
                }
            }
            return 0;
        }
		public bool InsertInto(City city)
		{
			bool success;
			using (SqlConnection connection = new SqlConnection(connectionstring))
			{
				SqlCommand cmd = new SqlCommand("INSERT INTO dbo.City(passage_hour,city_name) VALUES(@passage_hour,@city_name)", connection);
				cmd.Parameters.AddWithValue("passage_hour", DateTime.Now);
				cmd.Parameters.AddWithValue("city_name", city.Name);
				connection.Open();
				int res = cmd.ExecuteNonQuery();
				success = res > 0;
			}
			return success;
		}
		public int TakeCityId(City city)
		{
			int city_id;
			using (SqlConnection connection = new SqlConnection(connectionstring))
			{
				SqlCommand cmd = new SqlCommand("SELECT city_id FROM dbo.City WHERE city_name=@cityName", connection);
				cmd.Parameters.AddWithValue("cityName", city.Name);
				connection.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						city_id = reader.GetInt32("city_id");
						return city_id;
					}
				}
			}
			return 0;
		}
        public List<City> GetAllCitiesByDriverId(int? driverId)
        {
            List<City> result = new List<City>();
            City city = new City();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {

                SqlCommand command = new SqlCommand("SELECT c.city_name FROM dbo.City c INNER JOIN dbo.City_Path cp ON c.city_id = cp.FK_city_id " +
                    "INNER JOIN dbo.Carsharing ca ON cp.FK_carsharing_id = ca.carsharing_id " +
                    "INNER JOIN dbo.Car car ON ca.FK_car_id = car.car_id INNER JOIN dbo.Users u ON car.FK_user_id = u.user_id " +
                    "WHERE ca.type = @type and car.FK_user_id = @driverId and u.user_id != @driver_id", connection);
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
                            city.Name = reader.GetString(0);
                            result.Add(city);
                        }
                    }
                }
            }
            return result;
        }
    }
}
