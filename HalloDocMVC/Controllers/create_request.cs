using HalloDocDAL.DataContext;
using HalloDocDAL.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalloDocMVC.Controllers
{
    public class create_request : Controller
    {

        private readonly ApplicationDbContext _context;
        
        public create_request(ApplicationDbContext context)
        {
            _context = context;
        }


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
        [HttpPost]
        public IActionResult create_patient_request(Request obj)
        {
            obj.Requestid = 34;
            _context.Requests.Add(obj);
            _context.SaveChanges();

            return View();
        }


        public IActionResult submit_request_screen()
        {
            return View();
        }
    }
}
