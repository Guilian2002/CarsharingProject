using Microsoft.AspNetCore.Mvc;
using Projet.Models;
using Projet.Models.DAL;
using Projet.Models.DAL.Interfaces;


namespace Projet.Controllers
{
    public class ToOfferCarsharingController : Controller
    {
        private readonly IUserDAL userDAL;
        private readonly ICarDAL carDAL;
        private readonly ICarsharingOfferDAL carsharingOfferDAL;
        private readonly ICityDAL cityDAL;
        private static User u;
        private static Car usedCar;
        private static CarsharingOffer offer;
        public ToOfferCarsharingController(IUserDAL userDAL, ICarDAL carDAL,ICarsharingOfferDAL offerDAL,ICityDAL cityDAL )
        {
            this.userDAL = userDAL;
            this.carDAL = carDAL;
            this.cityDAL = cityDAL;
            carsharingOfferDAL = offerDAL;
        }

		public IActionResult DisplayCar()
		{
			if (HttpContext.Session.GetInt32("curr_user_id") is not null)
			{
				int user_id = (int)HttpContext.Session.GetInt32("curr_user_id");
				//User.LoadUser(userDAL, user_id); => apparently doesn't work here ?
				u = userDAL.GetUser(user_id);
				u.RequestUserCars(carDAL, user_id);
				return View(u.Cars);
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
		public IActionResult SaveCar(string name,int seats)
        {
            if (name is not null && seats > 0)
            {
                usedCar = new Car(name, seats);
                return RedirectToAction("MakeOffer");
            } else
            {
                return RedirectToAction("DisplayCar");
            }
        }
        public IActionResult MakeOffer()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MakeOffer(CarsharingOffer o)
        {
			if (ModelState.IsValid && o.Arrival.Name is String && o.Departure.Name is String && o.Date is DateTime && o.Departure.PassageHour is DateTime && o.Arrival.PassageHour is DateTime && DateTime.Compare(o.Departure.PassageHour, DateTime.Today) >= 1 && DateTime.Compare(o.Arrival.PassageHour, DateTime.Today) >= 1 && DateTime.Compare(o.Departure.PassageHour, new DateTime(9999, 1, 1, 0, 0, 0)) <= -1 && DateTime.Compare(o.Departure.PassageHour, new DateTime(9999, 1, 1, 0, 0, 0)) <= -1 && DateTime.Compare(o.Departure.PassageHour, o.Arrival.PassageHour) <= -1) ;
			{
				Console.WriteLine("good");
                o.Driver = u;
                o.Car = usedCar;
                offer = o;
                return RedirectToAction("AddDestination");
            }
			ViewData["Message"] = "Invalid data";
			return View(o);
            
        }
        public IActionResult AddDestination()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddDestination(City city)
        {
			//&&DateTime.Compare(city.PassageHour,offer.Departure.PassageHour)==1&& DateTime.Compare(city.PassageHour, offer.Arrival.PassageHour) == -1
			if (ModelState.IsValid)
            {
                offer.AddCity(city);
                return View();
            }
			else
			{
				ViewData["message"] = "Date has to be after the departure and before the arrival !";
				return View(city);
			}
        }
        public IActionResult Save()
        {
            offer.Save(carsharingOfferDAL,cityDAL,carDAL);
            return RedirectToAction("Home", "Index");
            
        }
    }
}
