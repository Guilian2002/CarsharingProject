using Microsoft.AspNetCore.Mvc;
using Projet.Models;
using System.Diagnostics;

namespace Projet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		public IActionResult Index()
		{
			if (HttpContext.Session.GetInt32("curr_user_id") is not null)
			{
				ViewData["connected"] = "Yes";
			}
			return View();
		}
		public IActionResult MainMenu()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}