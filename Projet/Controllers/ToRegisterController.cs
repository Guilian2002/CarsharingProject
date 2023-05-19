using Microsoft.AspNetCore.Mvc;
using Projet.Models;
using Projet.Models.DAL;
using Projet.Models.DAL.Interfaces;
using System.Diagnostics;

namespace Projet.Controllers
{
    public class ToRegisterController : Controller
    {
        private readonly IUserDAL _userDAL;
        private readonly ICarDAL _carDAL;
        private static User user;
        public ToRegisterController(IUserDAL userDAL,ICarDAL carDAL)
        {
            _carDAL = carDAL;
            _userDAL = userDAL;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User u)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = "Register successfull!";
                user = u;
                return RedirectToAction(nameof(AddCar));
            }
            else
            {
                TempData["message"] = "test";
                return View(u);
            }
        }
        public IActionResult AddCar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            if (user is null)
            {
                Console.WriteLine("user");
            }
            if (ModelState.IsValid)
            {
                if (user is null)
                {
                    Console.WriteLine("user1");
                }
                TempData["message"] = "Car successfully registered!";
                user.AddCar(car);
                
                return View();
            }else
            return View(car);
        }
        public IActionResult Save()
        {
            if(user is null)
            {
                Console.WriteLine("user2");
            }
            int user_id = user.Save(_userDAL, _carDAL);
            HttpContext.Session.SetInt32("curr_user_id", user_id);
            return RedirectToAction("Index", "Home");
        }
    }
}
