using Projet.Models.DAL.Interfaces;

namespace Projet.Models
{
    public class CarsharingRequest : Carsharing
    {
        private User passenger;
        public User Passenger
        {
            get { return passenger; }
            set{ passenger = value;}
        }
        public CarsharingRequest(City departure,City arrival,User passenger) : base(departure, arrival)
        {
            this.passenger = passenger;
        }
        public static List<CarsharingRequest> GetAllRequests(ICarsharingRequestDAL CarsharingRequestDAL,ICityDAL cityDAL,IUserDAL userDAL)
        {
            return CarsharingRequestDAL.GetAllRequest(cityDAL,userDAL);
        }
		public int Get(ICarsharingRequestDAL carsharingRequestDAL, int? cityId)
		{
			return carsharingRequestDAL.TakeCarsharingIdByCityIdAndType(cityId, "request");
		}
		public void Save(ICarsharingRequestDAL carsharingRequestDAL)
		{
			carsharingRequestDAL.InsertInto();
		}
		public void SavePath(ICityPathDAL cityPathDAL, int? carsharingId, int? order)
		{
			cityPathDAL.InsertInto(carsharingId, order);
		}
	}
}
