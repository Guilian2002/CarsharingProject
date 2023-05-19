using Microsoft.AspNetCore.Mvc;
using Projet.Models;
using Projet.Models.DAL.Interfaces;

namespace Projet.Controllers
{
	public class ToBookACarsharingOfferController : Controller
	{
		private readonly ICarDAL carDAL;
		private readonly IUserDAL userDAL;
		private readonly ICityDAL cityDAL;

		public ToBookACarsharingOfferController(ICarDAL carDAL, IUserDAL userDAL, ICityDAL cityDAL)
		{
			this.carDAL = carDAL;
			this.userDAL = userDAL;
			this.cityDAL = cityDAL;
		}

		public IActionResult BookACarsharingOffer()
		{
			List<CarsharingOffer> allOfferList = new List<CarsharingOffer>();
			CarsharingOffer offer = new CarsharingOffer();
			allOfferList = offer.LoadOffer(carDAL,userDAL,cityDAL);
			return View(allOfferList);
		}
		public IActionResult BookAnOffer(CarsharingOffer offer)
		{
			double price = 0;
			try
			{
				offer.BookOffer();
				price = offer.GetPrice();
				TempData["messageOffre"] = "The offer has been perfectly taken and your account has been debited for a price of " + price + " dollars.";
				return RedirectToAction("BookACarsharingOffer");
			}
			catch(Exception ex) 
			{
				TempData["messageOffre"] = "The offer is full";
				return RedirectToAction("BookACarsharingOffer");
			}
		}
	}
}
