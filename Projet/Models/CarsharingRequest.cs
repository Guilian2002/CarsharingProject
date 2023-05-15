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
        public static List<CarsharingRequest> GetAllRequests()
        {
            return new List<CarsharingRequest>();
        }
    }
}
