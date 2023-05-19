namespace Projet.Models.DAL.Interfaces
{
    public interface ICarsharingRequestDAL
    {
        public List<CarsharingRequest> GetAllRequest(ICityDAL cityDAL, IUserDAL userDAL);
		public bool InsertInto();
		public bool InsertInto(Carsharing carsharing, int? car_id);
		public int TakeCarsharingIdByCityIdAndType(int? cityId, string type);
	}
}
