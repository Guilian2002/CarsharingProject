using Microsoft.AspNetCore.Mvc;
using Projet.Models;
using Projet.Models.DAL;
using Projet.Models.DAL.Interfaces;


namespace Projet.Controllers
{
    public class ToConsultCarsharingRequestController : Controller
    {
        private readonly ICarsharingRequestDAL _ICarsharingRequestDAL;
        private readonly IUserDAL _IUserDAL;
        private readonly ICityDAL _ICityDAL;
        public ToConsultCarsharingRequestController(ICarsharingRequestDAL CarsharingRequestDAL,ICityDAL cityDAL,IUserDAL userDAL)
        {
            _ICarsharingRequestDAL = CarsharingRequestDAL;
            _ICityDAL = cityDAL;
            _IUserDAL = userDAL;
        }

        public IActionResult ConsultRequest()
        {
            List<CarsharingRequest> requests = CarsharingRequest.GetAllRequests(_ICarsharingRequestDAL, _ICityDAL, _IUserDAL);
            return View(requests);
        }
    }
}
