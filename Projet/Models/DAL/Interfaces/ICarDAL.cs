namespace Projet.Models.DAL.Interfaces
{
    public interface ICarDAL
    {
        public bool InsertInto(Car car,int user_id);
        public List<Car> GetUserCar(int? user_id);
        public int GetCarId(Car car);
        public List<int?> GetAllDriverId();
    }
}
