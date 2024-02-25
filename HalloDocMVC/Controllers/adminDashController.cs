using Microsoft.AspNetCore.Mvc;

namespace HalloDocMVC.Controllers
{
    public class adminDashController : Controller
    {
        public IActionResult adminDashboard()
        {
            return View();
        }
        public IActionResult providerLocation()
        {
            return View();
        }
        public IActionResult myProfile()
        {
            return View();
        }
        public IActionResult providers()
        {
            return View();
        }
        public IActionResult partners()
        {
            return View();
        }
        public IActionResult access()
        {
            return View();
        }
        public IActionResult records()
        {
            return View();
        }
    }
}
