using Microsoft.AspNetCore.Mvc;
using Projet.Models;
using Projet.Models.DAL.Interfaces;

namespace Projet.Controllers
{
	public class ToLookForACarsharingOfferController : Controller
	{
		private readonly ICarsharingOfferDAL _carsharingOfferDAL;

		public ToLookForACarsharingOfferController(ICarsharingOfferDAL carsharingOfferDAL)
		{
			_carsharingOfferDAL = carsharingOfferDAL;
		}
		public IActionResult LookForACarsharingOffer()
		{
			List<CarsharingOffer> offerList = new List<CarsharingOffer>();
			CarsharingOffer offer = new CarsharingOffer();
			offerList = offer.DisplayOffer(_carsharingOfferDAL);
			return View(offerList);
		}
	}
}
