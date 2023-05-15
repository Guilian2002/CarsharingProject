namespace Projet.Models
{
    public class CarsharingOffer : Carsharing
    {
        private List<City> citys;
        private List<User> passengers;
        private User driver;
        private Car car;
        public List<City> Citys
        {
            get { return citys; }
            set { citys = value; }
        }
        public List<User> Passengers
        {
            get { return passengers; }
            set { passengers = value; }
        }
        public User Driver
        {
            get { return driver; }
            set { driver = value; }
        }
        public Car Car
        {
            get { return car; }
            set { car = value; }
        }
        public CarsharingOffer(City departure,City arrival,User driver,Car car) : base(departure,arrival)
        {
            this.driver = driver;
            this.car = car;
            citys = new List<City>();
            passengers = new List<User>();
        }
        //public static List<CarsharingOffer> DisplayOffer()
        //{
        //    new List<CarsharingOffer> offers;
        //}
        //public void BookOffer()
        //{

        //}
        //public double GetPrice()
        //{

        //}
        //public void AddCar()
        //{

        //}
        //public void AddCity()
        //{

        //}
        //public int GetPassengerNumber()
        //{

        //}
        //public void LoadOffer()
        //{

        //}
    }
}
