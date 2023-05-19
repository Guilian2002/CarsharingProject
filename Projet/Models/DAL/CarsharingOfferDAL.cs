using Projet.Models.DAL.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Projet.Models.DAL
{
    public class CarsharingOfferDAL : ICarsharingOfferDAL
    {
        private string connectionString;
        public CarsharingOfferDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public int Save(CarsharingOffer offer,int car_id)
        {
            
           using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO dbo.Carsharing(date_carsharing,type,FK_car_id) VALUES(@date,@type,@car_id)",connection);
                command.Parameters.AddWithValue("date", offer.Date);
                command.Parameters.AddWithValue("type", "offer");
                command.Parameters.AddWithValue("car_id", car_id);
                connection.Open();
                connection.Close();
                SqlCommand command2 = new SqlCommand("SELECT carsharing_id FROM dbo.Carsharing WHERE date_carsharing=@date and type=@type and FK_car_id=@car_id", connection);
                command2.Parameters.AddWithValue("date", offer.Date);
                command2.Parameters.AddWithValue("type", "offer");
                command2.Parameters.AddWithValue("car_id", car_id);
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
        public void SavePath( int offer_id, int city_id, int order)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("INSERT INTO dbo.City_Path(FK_carsharing_id,FK_city_id,city_order) VALUES(@carsharing_id,@city_id,@order)", connection);
                command.Parameters.AddWithValue("carsharing_id", offer_id);
                command.Parameters.AddWithValue("city_id", city_id);
                command.Parameters.AddWithValue("order", order);
                connection.Open();
            }
        }
		public List<CarsharingOffer> TakeOffers()
		{
			int i = 0;
			List<CarsharingOffer> offers = new List<CarsharingOffer>();
			List<int> carsharing_id = new List<int>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("SELECT carsharing_id FROM dbo.Carsharing WHERE type = @type ", connection);
				cmd.Parameters.AddWithValue("type", "offer");
				connection.Open();

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							carsharing_id.Add(reader.GetInt32(0));
						}
					}
				}
			}
			foreach (var id in carsharing_id)
			{
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					SqlCommand cmd = new SqlCommand("SELECT c.city_name, c.passage_hour FROM dbo.City c " +
						"INNER JOIN dbo.City_Path cp ON c.city_id = cp.FK_city_id" +
						"INNER JOIN Carsharing ca ON cp.FK_carsharing_id = ca.carsharing_id  " +
						"WHERE ca.carsharing_id = @carsharingId ORDER BY city_order", connection);
					cmd.Parameters.AddWithValue("carsharingId", id);
					connection.Open();

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								offers[id - 1].Citys[i].Name = reader.GetString(0);
								offers[id - 1].Citys[i].PassageHour = reader.GetDateTime(0);
								i++;
							}
						}
					}
				}
			}
			return offers;
		}
	}
}
