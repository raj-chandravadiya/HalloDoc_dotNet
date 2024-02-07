using Microsoft.AspNetCore.Mvc;

namespace HalloDocMVC.Controllers
{
    public class create_request : Controller
    {
        public IActionResult create_businessPartner_request()
        {
            return View();
        }
        public IActionResult create_concierge_request()
        {
            return View();
        }
        public IActionResult create_familyFriend_request()
        {
            return View();
        }
        public IActionResult create_patient_request()
        {
            return View();
        }
        public IActionResult submit_request_screen()
        {
            return View();
        }
    }
}
