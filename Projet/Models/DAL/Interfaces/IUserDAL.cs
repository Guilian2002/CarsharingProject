    namespace Projet.Models.DAL.Interfaces
{
    public interface IUserDAL
    {
        public bool InsertInto(User user);
        public int TakeUserId(User user);
        public int VerifyIdentifiers(User user);
        public User GetUser(int user_id);
		public User TakeUser(int? user_id);
        public List<User> GetPassengersByDriverId(int? driverId);
        public User GetDriver(int? driverId);
	}
}
