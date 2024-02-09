using HalloDocDAL.DataContext;
using HalloDocDAL.DataModels;
//using HalloDocDAL.Models;
using HalloDocDAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections;

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
        public IActionResult create_patient_request(PatientRequestViewModel obj)
        {
                if (ModelState.IsValid)
                {
                    var newaspNetUser = new Aspnetuser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Username = obj.Email,
                        Email = obj.Email,
                        Createddate = DateTime.Now,
                        Phonenumber = obj.PhoneNumber,
                    };
                    _context.Aspnetusers.Add(newaspNetUser);
                    _context.SaveChanges();

                    var user = new User()
                    {
                        Aspnetuserid = newaspNetUser.Id,
                        Firstname = obj.FirstName,
                        Lastname = obj.LastName,
                        Email = obj.Email,
                        Mobile = obj.PhoneNumber,
                        Createdby = newaspNetUser.Id,
                        Createddate = DateTime.Now,
                    };
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    var request = new Request
                    {
                        Requesttypeid = 1,
                        Userid = user.Userid,
                        Firstname = obj.FirstName,
                        Lastname = obj.LastName,
                        Phonenumber = obj.PhoneNumber,
                        Email = obj.Email,
                        Status = 1,
                        Createddate = DateTime.Now,
                        //Isurgentemailsent = new BitArray(1),
                    };
                    _context.Requests.Add(request);
                    _context.SaveChanges();

                    var requestClient = new Requestclient
                    {
                        Requestid = request.Requestid,
                        Firstname = obj.FirstName,
                        Lastname = obj.LastName,
                        Phonenumber = obj.PhoneNumber,
                    };
                    _context.Requestclients.Add(requestClient);
                    _context.SaveChanges();
                
                
                    return RedirectToAction("login_page", "login");
                }
                else
                {
                    return RedirectToAction("create_patient_request", "create_request");
            }
        }
        public IActionResult Submit_request_screen()
        {
            return View();
        }

    }



    
}
