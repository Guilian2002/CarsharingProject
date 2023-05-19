using Microsoft.AspNetCore.Mvc;
using Projet.Models.DAL.Interfaces;
using Projet.Models;
using Projet.Models.DAL;

namespace Projet.Controllers
{
	public class ToRequestACarsharingController : Controller
	{
		private readonly IUserDAL _userDAL;
		private readonly ICarsharingRequestDAL _carsharingRequestDAL;
		private readonly ICityDAL _cityDAL;
		private readonly ICityPathDAL _cityPathDAL;
		public ToRequestACarsharingController(IUserDAL userDAL, ICarsharingRequestDAL carsharingDAL, ICityDAL cityDAL, ICityPathDAL cityPathDAL)
		{
			_userDAL = userDAL;
			_carsharingRequestDAL = carsharingDAL;
			_cityDAL = cityDAL;
			_cityPathDAL = cityPathDAL;
		}
		public IActionResult RequestACarsharing()
		{
			return View();
		}
		[HttpPost]
		public IActionResult RequestACarsharing(CarsharingRequest carsharing)
		{
			int cityId = 0;
			int carsharingId = 0;
			int? currUserId = HttpContext.Session.GetInt32("curr_user_id");
			carsharing.Passenger = _userDAL.TakeUser(currUserId);
			if (carsharing.Departure is null || carsharing.Arrival is null)
			{
				Console.WriteLine("city");
				return View(carsharing);
			}
			else if (!ModelState.IsValid)
			{
				if (carsharing.Departure is null || carsharing.Arrival is null)
				{
					Console.WriteLine("city1");
				}
				TempData["message"] = "Request successfully registered!";
				carsharing.Save(_carsharingRequestDAL);
				carsharing.Departure.Save(_cityDAL);
				cityId = carsharing.Departure.Get(_cityDAL);
				carsharingId = carsharing.Get(_carsharingRequestDAL, cityId);
				carsharing.SavePath(_cityPathDAL, carsharingId, 1);
				carsharing.Arrival.Save(_cityDAL);
				cityId = carsharing.Arrival.Get(_cityDAL);
				carsharing.SavePath(_cityPathDAL, carsharingId, 2);
				return View();
			}
			else
				return View(carsharing);
		}
	}
}
