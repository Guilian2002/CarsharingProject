using Projet.Models.DAL.Interfaces;
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
    }
}
