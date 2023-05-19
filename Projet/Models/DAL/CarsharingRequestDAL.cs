using Projet.Models.DAL.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Projet.Models.DAL
{
    public class CarsharingRequestDAL : ICarsharingRequestDAL
    {
        private string connectionString;
        public CarsharingRequestDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<CarsharingRequest> GetAllRequest(ICityDAL cityDAL,IUserDAL userDAL)
        {
            List<CarsharingRequest> requests = new List<CarsharingRequest>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT carsharing_id,date_carsharing,FK_user_id FROM dbo.Carsharing c INNER JOIN dbo.PassengerOffer p ON c.carsharing_id=p.FK_user_id WHERE type=@type",connection);
                command.Parameters.AddWithValue("type", "request");
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        int request_id = reader.GetInt32("carsharing_id");
                        City departure = City.GetDepartureCity(cityDAL, request_id);
                        City arrival = City.GetArrivalCity(cityDAL, request_id);
                        User u = User.LoadUser(userDAL, reader.GetInt32("FK_user_id")) ;
                        CarsharingRequest request = new CarsharingRequest(departure, arrival,u);
                        requests.Add(request);
                        
                    }
                }
            }
            return requests;
        }
		public bool InsertInto()
		{
			bool success;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Carsharing(type,date_carsharing) VALUES(@carsharing_type,@date)", connection);
				cmd.Parameters.AddWithValue("carsharing_type", "request");
				cmd.Parameters.AddWithValue("date", DateTime.Now);
				connection.Open();
				int res = cmd.ExecuteNonQuery();
				success = res > 0;
			}
			return success;
		}
		public bool InsertInto(Carsharing carsharing, int? carId)
		{
			bool success;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("INSERT INTO dbo.Carsharing(FK_car_id,type,date_carsharing) VALUES(@car_id,@carsharing_type,@date)", connection);
				cmd.Parameters.AddWithValue("FK_car_id", carId);
				cmd.Parameters.AddWithValue("carsharing_type", "offer");
				cmd.Parameters.AddWithValue("date", carsharing.Date);
				connection.Open();
				int res = cmd.ExecuteNonQuery();
				success = res > 0;
			}
			return success;
		}
		public int TakeCarsharingIdByCityIdAndType(int? cityId, string type)
		{
			int carsharing_id;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("SELECT ca.carsharing_id, c.city_id FROM dbo.Carsharing ca " +
					"INNER JOIN dbo.City_Path cp ON ca.carsharing_id = cp.FK_carsharing_id " +
					"INNER JOIN dbo.City c ON cp.FK_city_id = c.city_id " +
					"WHERE c.city_id=@cityId and ca.type = @type", connection);
				cmd.Parameters.AddWithValue("cityId", cityId);
				cmd.Parameters.AddWithValue("type", type);
				connection.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						carsharing_id = reader.GetInt32("ca.carsharing_id");
						return carsharing_id;
					}
				}
			}
			return 0;
		}
	}
}
