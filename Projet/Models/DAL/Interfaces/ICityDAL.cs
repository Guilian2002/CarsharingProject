namespace Projet.Models.DAL.Interfaces
{
    public interface ICityDAL
    {
        public City GetArrivalCity(int carsharing_id);
        public City GetDepartureCity(int carsharing_id);
        public int Save(City city);
		public bool InsertInto(City city);
		public int TakeCityId(City city);
        public List<City> GetAllCitiesByDriverId(int? driverId);
	}
}
