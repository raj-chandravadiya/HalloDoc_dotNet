using Microsoft.AspNetCore.Mvc;

namespace HalloDocMVC.Controllers
{
    public class patientDashController : Controller
    {
        public IActionResult patientDashboard()
        {
            return View();
        }
    }
}
