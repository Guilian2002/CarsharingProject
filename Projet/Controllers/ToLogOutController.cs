using Microsoft.AspNetCore.Mvc;

namespace Projet.Controllers
{
	public class ToLogOutController : Controller
	{
		public void DestroySession()
		{
			HttpContext.Session.Clear();
		}
		public IActionResult LogOut()
		{
			DestroySession();
			return RedirectToAction("LogIn", "ToLogIn");
		}
	}
}
