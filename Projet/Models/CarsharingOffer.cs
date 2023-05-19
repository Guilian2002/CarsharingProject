using Projet.Models.DAL.Interfaces;

namespace Projet.Models
{
    public class CarsharingOffer : Carsharing
    {
        private List<City>? citys;
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
        public CarsharingOffer(){}
		public List<CarsharingOffer> DisplayOffer(ICarsharingOfferDAL _carsharingOfferDAL) => _carsharingOfferDAL.TakeOffers();
        public void BookOffer()
        {
            if(this.Car.NbrPlaces > this.Passengers.Count) 
            {
                this.GetPrice();
            }
            else
            {
                throw new Exception(null);
            }
        }
        public double GetPrice() => this.Citys.Count * 5;
        //public void AddCar()
        //{

        //}
        public void AddCity(City city)
        {
            if (citys is null)
            {
                citys = new List<City>();
            }
            citys.Add(city);
        }
        //public int GetPassengerNumber()
        //{

        //}
        public List<CarsharingOffer> LoadOffer(ICarDAL carDAL, IUserDAL userDAL, ICityDAL cityDAL)
        {
            CarsharingOffer offer = new CarsharingOffer();
            List<CarsharingOffer> allOfferList = new List<CarsharingOffer>();
            List<int?> driverIdList = new List<int?>();
            List<Car> cars = new List<Car>();
            List<User> users = new List<User>();
            List<City> cities = new List<City>();
            User driver = new User();
            driverIdList = carDAL.GetAllDriverId();
            foreach (var driverId in driverIdList)
            {
                cars = carDAL.GetUserCar(driverId);
                driver = userDAL.GetDriver(driverId);
                users = userDAL.GetPassengersByDriverId(driverId);
                cities = cityDAL.GetAllCitiesByDriverId(driverId);
                offer.Car = cars[0];
                offer.Driver = driver;
                offer.Citys = cities;
                offer.Passengers = users;
                allOfferList.Add(offer);
            }
            return allOfferList;
        }
        public void Save(ICarsharingOfferDAL offerDAL,ICityDAL cityDAL,ICarDAL carDAL)
        {
            int car_id=this.Car.GetCarId(carDAL);
            int offer_id = offerDAL.Save(this,car_id);
            int city_id= this.Departure.Save(cityDAL);
            SavePath(offerDAL, offer_id, city_id,1);
            int cpt = 2;
            if (citys is not null)
            {
                foreach (City c in citys)
                {
                    offer_id = offerDAL.Save(this, car_id);
                    city_id = c.Save(cityDAL);
                    SavePath(offerDAL, offer_id, city_id, cpt);
                    cpt++;
                }
            }
            offer_id = offerDAL.Save(this, car_id);
            city_id = this.Arrival.Save(cityDAL);
            SavePath(offerDAL, offer_id, city_id, cpt);
        }
        public void SavePath(ICarsharingOfferDAL offerDAL, int offer_id, int city_id,int order)
        {
            offerDAL.SavePath(offer_id, city_id, order);
        }
    }
}
