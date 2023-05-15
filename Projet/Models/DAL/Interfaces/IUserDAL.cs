namespace Projet.Models.DAL.Interfaces
{
    public interface IUserDAL
    {
        public bool InsertInto(User user);
        public int TakeUserId(User user);
        public int VerifyIdentifiers(User user);
    }
}
