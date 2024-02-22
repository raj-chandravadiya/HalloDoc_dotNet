
using HalloDocDAL.DataContext;
using HalloDocDAL.DataModels;
using HalloDocDAL.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HalloDocMVC.Controllers
{
    public class patientDashController : Controller
    {
        private readonly ApplicationDbContext _context;

        public patientDashController (ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult patientDashboard()
        {

            //query
            var dashTable = from req in _context.Requests
                            join file in _context.Requestwisefiles
                            on req.Requestid equals file.Requestid
                            select new dashTableViewModel()
                            {
                                date = req.Createddate,
                                status = "unassigned",
                            };



            return View(dashTable);
        }
        public IActionResult patientInformationByMe( int userId) 
        {
           var link = _context.Users.Where(x=> x.Userid == userId).FirstOrDefault();

            if(link == null)
            {
                return View();
            }
            var models = new patientInformationMe()
            {
                Email = link.Email,
                FirstName=link.Firstname

            };
            return View(models);
        }
        public IActionResult patientInformationBySomeone()
        {
            return View();
        }

        public IActionResult viewDocuments()
        {
            return View();  
        }
        public IActionResult patientProfile() 
        {
            return View();
        }
    }
}
