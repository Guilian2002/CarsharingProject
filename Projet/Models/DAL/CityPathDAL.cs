using Projet.Models.DAL.Interfaces;
using System.Data.SqlClient;

namespace Projet.Models.DAL
{
	public class CityPathDAL : ICityPathDAL
	{
		private string connectionstring;
		public CityPathDAL(string connectionstring)
		{
			this.connectionstring = connectionstring;
		}
		public bool InsertInto(int? carsharingId, int? cityOrder)
		{
			bool success;
			using (SqlConnection connection = new SqlConnection(connectionstring))
			{
				SqlCommand cmd = new SqlCommand("INSERT INTO dbo.City_Path(FK_carsharing_id,city_order) VALUES(@carsharing_id,@city_order)", connection);
				cmd.Parameters.AddWithValue("carsharing_id", carsharingId);
				cmd.Parameters.AddWithValue("city_order", cityOrder);
				connection.Open();
				int res = cmd.ExecuteNonQuery();
				success = res > 0;
			}
			return success;
		}
	}
}
