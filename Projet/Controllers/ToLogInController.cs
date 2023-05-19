using Microsoft.AspNetCore.Mvc;
using Projet.Models;
using Projet.Models.DAL.Interfaces;

namespace Projet.Controllers
{
    public class ToLogInController : Controller
    {
        private readonly IUserDAL _IUserDal;
        public ToLogInController(IUserDAL IUserDal)
        { 
            _IUserDal= IUserDal;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpGet]
        public IActionResult LogIn(User currUser)
        { 
            if(currUser.Email is string&& currUser.Email is not null&& currUser.Password is string && currUser.Password is not null)
            {
                try
                {
                    HttpContext.Session.SetInt32("curr_user_id", currUser.VerifyIdentifiers(_IUserDal));
                    return RedirectToAction("Index","Home");
                }
                catch(Exception ex)
                {
                    ViewBag.message = ex.Message;
                    return RedirectToAction((nameof(LogIn)));
                }
            }
            return View(currUser);
        }
    }
}
