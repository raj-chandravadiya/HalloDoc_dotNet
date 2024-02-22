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
        public IActionResult create_patient_request()
        {

            return View();
        }
        public IActionResult create_familyFriend_request()
        {
            return View();
        }
        public IActionResult Submit_request_screen()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    Requesttypeid = 2,
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



                if (obj.uploadFile != null)
                {
                    Guid myuuid = Guid.NewGuid();
                    var filename = Path.GetFileName(obj.uploadFile.FileName);
                    var FinalFileName = myuuid.ToString() + filename;

                    //path

                    var filepath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "uploads", FinalFileName);

                    //copy in stream

                    using (var str = new FileStream(filepath, FileMode.Create))
                    {
                        //copy file
                        obj.uploadFile.CopyTo(str);
                    }

                    //STORE DATA IN TABLE
                    var fileupload = new Requestwisefile()
                    {

                        Requestid = request.Requestid,
                        Filename = FinalFileName,
                        Createddate = DateTime.Now,
                    };

                    _context.Requestwisefiles.Add(fileupload);
                    _context.SaveChanges();
                }

                return RedirectToAction("patientDashboard", "patientDash");
            }
            else
            {
                return RedirectToAction("create_patient_request", "create_request");
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult create_familyFriend_request(FamilyFriendRequestViewModel fmfr)
        {
            if (ModelState.IsValid)
            {
                Request r = new()
                {
                    Requesttypeid = 3,
                    Firstname = fmfr.FriendFirstName,
                    Lastname = fmfr.FriendLastName,
                    Phonenumber = fmfr.FriendPhoneNo,
                    Email = fmfr.FriendEmail,
                    Status = 1,
                    Createddate = DateTime.Now
                };
                _context.Requests.Add(r);
                _context.SaveChanges();

                Requestclient rcl = new()
                {
                    Requestid = r.Requestid,
                    Firstname = fmfr.PatientRequest.FirstName,
                    Lastname = fmfr.PatientRequest.LastName,
                    Phonenumber = fmfr.PatientRequest.PhoneNumber,
                    Email = fmfr.PatientRequest.Email,
                    Location = fmfr.PatientRequest.City + fmfr.PatientRequest.State,
                    City = fmfr.PatientRequest.City,
                    State = fmfr.PatientRequest.State,
                    Zipcode = fmfr.PatientRequest.Zipcode
                };

                _context.Requestclients.Add(rcl);
                _context.SaveChanges();

                return RedirectToAction("login_page", "login");
            }
            else
            {
                return RedirectToAction("create_patient_request", "create_request");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create_concierge_request(ConciergeRequestViewModel crvm)
        {
            if (ModelState.IsValid)
            {
                Request r = new()
                {
                    Requesttypeid = 4,
                    Firstname = crvm.ConciergeFirstName,
                    Lastname = crvm.ConciergeLastName,
                    Phonenumber = crvm.ConciergePhone,
                    Email = crvm.ConciergeEmail,
                    Status = 1,
                    Createddate = DateTime.Now
                };
                _context.Requests.Add(r);
                _context.SaveChanges();

                Requestclient rcl = new()
                {
                    Requestid = r.Requestid,
                    Firstname = crvm.PatientRequest.FirstName,
                    Lastname = crvm.PatientRequest.LastName,
                    Phonenumber = crvm.PatientRequest.PhoneNumber,
                    Email = crvm.PatientRequest.Email,
                    Location = crvm.PatientRequest.City + crvm.PatientRequest.State,
                    City = crvm.PatientRequest.City,
                    State = crvm.PatientRequest.State,
                    Zipcode = crvm.PatientRequest.Zipcode
                };
                _context.Requestclients.Add(rcl);
                _context.SaveChanges();

                //Concierge concierge = new()
                //{
                //    Conciergename = crvm.ConciergeFirstName + " " + crvm.ConciergeLastName,
                    

                //};
                Concierge concierge = new()
                {
                    Conciergename = crvm.ConciergeFirstName,
                    Address = crvm.ConciergeProperty,
                    Street = crvm.ConciergeStreet,
                    City = crvm.ConciergeCity,
                    State = crvm.ConciergeState,
                    Zipcode = crvm.ConciergeZip,
                    Createddate = DateTime.Now,
                    
                };
                _context.Concierges.Add(concierge);
                _context.SaveChanges();

                Requestconcierge rc = new()
                {
                    Requestid = r.Requestid,
                    Conciergeid = concierge.Conciergeid,
                };
                _context.Requestconcierges.Add(rc);
                _context.SaveChanges();

                return RedirectToAction("login_page", "login");
            }
            else
            {
                return RedirectToAction("create_patient_request", "create_request");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create_businessPartner_request(BusinessRequestViewModel cbpr)
        {
            if (ModelState.IsValid)
            {
                Request r = new()
                {
                    Requesttypeid = 1,
                    Firstname = cbpr.BusinessFirstName,
                    Lastname = cbpr.BusinessLastName,
                    Phonenumber = cbpr.BusinessPhone,
                    Email = cbpr.BusinessEmail,
                    Status = 1,
                    Createddate = DateTime.Now
                };
                _context.Requests.Add(r);
                _context.SaveChanges();

                Requestclient requestclient = new()
                {
                    Requestid = r.Requestid,
                    Firstname = cbpr.PatientRequest.FirstName,
                    Lastname = cbpr.PatientRequest.LastName,
                    Phonenumber = cbpr.PatientRequest.PhoneNumber,
                    Email = cbpr.PatientRequest.Email,
                    Location = cbpr.PatientRequest.City + cbpr.PatientRequest.State,
                    City = cbpr.PatientRequest.City,
                    State = cbpr.PatientRequest.State,
                    Zipcode = cbpr.PatientRequest.Zipcode
                };
                _context.Requestclients.Add(requestclient);
                _context.SaveChanges();

                Business business= new()
                {
                    Name= cbpr.BusinessFirstName + " " + cbpr.BusinessLastName,
                    Address1 = cbpr.BusinessPropertyName,
                    Createddate = DateTime.Now,
                };
                _context.Businesses.Add(business);
                _context.SaveChanges();

                Requestbusiness rb = new()
                {
                    Requestid = r.Requestid,
                    Businessid= business.Id
                };
                _context.Requestbusinesses.Add(rb);
                _context.SaveChanges(); 

                return RedirectToAction("login_page", "login");
            }
            else
            {
                return RedirectToAction("create_patient_request", "create_request");
            }
        }
    }

}



