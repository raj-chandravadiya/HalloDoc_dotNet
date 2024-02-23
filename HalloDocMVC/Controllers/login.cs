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
            string passhash = create_request.GenerateSHA256(loginUser.Passwordhash);

            Aspnetuser aspnetuser = _context.Aspnetusers.FirstOrDefault(u => u.Username == loginUser.Username && u.Passwordhash == passhash);

            if (aspnetuser != null)
            {
                User user = _context.Users.FirstOrDefault(u => u.Aspnetuserid == aspnetuser.Id);
                HttpContext.Session.SetInt32("userId", user.Userid);
                return RedirectToAction("patientDashboard", "patientDash");
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
