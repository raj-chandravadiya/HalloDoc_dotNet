using HalloDocDAL.DataContext;
using HalloDocDAL.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace HalloDocMVC.Controllers
{
    public class login : Controller
    {
        private readonly ApplicationDbContext _context;

        public login(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult login_page()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login_page(Aspnetuser loginUser)
        {
            var obj = _context.Aspnetusers.ToList();
            

            foreach (var item in obj)
            {
                if(loginUser.Username == item.Username && loginUser.Passwordhash == item.Passwordhash)
                {
                    return RedirectToAction("patientDashboard", "patientDash");
                }
            }
            //ModelState.AddModelError("wrong creential", "Invalide");

            return View();
        }
        public IActionResult forgot_password()
        {
            return View();
        }

    }
}
