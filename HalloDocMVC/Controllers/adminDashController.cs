using HalloDocDAL.DataContext;
using HalloDocDAL.DataModels;
using HalloDocDAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalloDocMVC.Controllers
{

    public enum RequestStatus
    {
        Unassigned = 1,
        Accepted = 2,
        Cancelled = 3,
        MDEnRoute = 4,
        MDOnSite = 5,
        Conclude = 6,
        CancelledByPatient = 7,
        Closed = 8,
        Unpaid = 9,
        Clear = 10,
    }

    public enum DashboardStatus
    {
        New = 1,
        Pending = 2,
        Active = 3,
        Conclude = 4,
        ToClose = 5,
        Unpaid = 6,
    }

    public enum RequestType
    {
        Business = 1,
        Patient = 2,
        Family = 3,
        Concierge = 4
    }

    public class adminDashController : Controller
    {
        private readonly ApplicationDbContext _context;

        public adminDashController(ApplicationDbContext context)
        {
            _context = context;
        }


        public static string GetPatientDOB(Requestclient u)
        {
            string udb = u.Intyear + "-" + u.Strmonth + "-" + u.Intdate;
            if (u.Intyear == null || u.Strmonth == null || u.Intdate == null)
            {
                return "";
            }

            DateTime dobDate = DateTime.Parse(udb);
            string dob = dobDate.ToString("MMM dd, yyyy");
            var today = DateTime.Today;
            var age = today.Year - dobDate.Year;
            if (dobDate.Date > today.AddYears(-age)) age--;

            string dobString = dob + " (" + age + ")";

            return dobString;
        }

        public static string GetRequestType(Request request)
        {
            switch (request.Requesttypeid)
            {
                case (int)RequestType.Business: return "Business";
                case (int)RequestType.Patient: return "Patient";
                case (int)RequestType.Concierge: return "Concierge";
                case (int)RequestType.Family: return "Relative/Family";
            }

            return null;
        }

        public ActionResult PartialTable(int status)
        {
            List<AdminRequest> adminRequests = new List<AdminRequest>();

            if (status == (int)DashboardStatus.New)
            {

                adminRequests = (from r in _context.Requests
                                 join rc in _context.Requestclients on r.Requestid equals rc.Requestid
                                 where (r.Status == (short)RequestStatus.Unassigned)
                                 select new AdminRequest
                                 {
                                     RequestId = r.Requestid,
                                     PatientName = rc.Firstname + " " + rc.Lastname,
                                     DateOfBirth = GetPatientDOB(rc),
                                     RequestType = r.Requesttypeid,
                                     Requestor = GetRequestType(r) + " " + r.Firstname + " " + r.Lastname,
                                     RequestDate = r.Createddate.ToString("MMM dd, yyyy"),
                                     PatientPhone = rc.Phonenumber,
                                     Phone = r.Phonenumber,
                                     Address = rc.Address,
                                     Notes = rc.Notes,
                                 }).ToList();
            }
            else if (status == (int)DashboardStatus.Pending)
            {
                adminRequests = (from r in _context.Requests
                                 join rc in _context.Requestclients on r.Requestid equals rc.Requestid
                                 where (r.Status == (short)RequestStatus.Accepted)
                                 select new AdminRequest
                                 {
                                     RequestId = r.Requestid,
                                     PatientName = rc.Firstname + " " + rc.Lastname,
                                     DateOfBirth = GetPatientDOB(rc),
                                     RequestType = r.Requesttypeid,
                                     Requestor = GetRequestType(r) + " " + r.Firstname + " " + r.Lastname,
                                     RequestDate = r.Createddate.ToString("MMM dd, yyyy"),
                                     PatientPhone = rc.Phonenumber,
                                     Phone = r.Phonenumber,
                                     Address = rc.Address,
                                     Notes = rc.Notes,
                                 }).ToList();
            }
            else if (status == (int)DashboardStatus.Active)
            {
                adminRequests = (from r in _context.Requests
                                 join rc in _context.Requestclients on r.Requestid equals rc.Requestid
                                 where (r.Status == (short)RequestStatus.MDEnRoute) || (r.Status == (short)RequestStatus.MDOnSite)
                                 select new AdminRequest
                                 {
                                     PatientName = rc.Firstname + " " + rc.Lastname,
                                     DateOfBirth = GetPatientDOB(rc),
                                     RequestType = r.Requesttypeid,
                                     Requestor = GetRequestType(r) + " " + r.Firstname + " " + r.Lastname,
                                     RequestDate = r.Createddate.ToString("MMM dd, yyyy"),
                                     PatientPhone = rc.Phonenumber,
                                     Phone = r.Phonenumber,
                                     Address = rc.Address,
                                     Notes = rc.Notes,
                                 }).ToList();
            }
            else if (status == (int)DashboardStatus.Conclude)
            {
                adminRequests = (from r in _context.Requests
                                 join rc in _context.Requestclients on r.Requestid equals rc.Requestid
                                 where (r.Status == (short)RequestStatus.Conclude)
                                 select new AdminRequest
                                 {
                                     PatientName = rc.Firstname + " " + rc.Lastname,
                                     DateOfBirth = GetPatientDOB(rc),
                                     RequestType = r.Requesttypeid,
                                     Requestor = GetRequestType(r) + " " + r.Firstname + " " + r.Lastname,
                                     RequestDate = r.Createddate.ToString("MMM dd, yyyy"),
                                     PatientPhone = rc.Phonenumber,
                                     Phone = r.Phonenumber,
                                     Address = rc.Address,
                                     Notes = rc.Notes,
                                 }).ToList();
            }
            else if (status == (int)DashboardStatus.ToClose)
            {
                adminRequests = (from r in _context.Requests
                                 join rc in _context.Requestclients on r.Requestid equals rc.Requestid
                                 where (r.Status == (short)RequestStatus.Cancelled) || (r.Status == (short)RequestStatus.CancelledByPatient) || (r.Status == (short)RequestStatus.Closed)
                                 select new AdminRequest
                                 {
                                     PatientName = rc.Firstname + " " + rc.Lastname,
                                     DateOfBirth = GetPatientDOB(rc),
                                     RequestType = r.Requesttypeid,
                                     Requestor = GetRequestType(r) + " " + r.Firstname + " " + r.Lastname,
                                     RequestDate = r.Createddate.ToString("MMM dd, yyyy"),
                                     PatientPhone = rc.Phonenumber,
                                     Phone = r.Phonenumber,
                                     Address = rc.Address,
                                     Notes = rc.Notes,
                                 }).ToList();
            }
            else if (status == (int)DashboardStatus.Unpaid)
            {
                adminRequests = (from r in _context.Requests
                                 join rc in _context.Requestclients on r.Requestid equals rc.Requestid
                                 where (r.Status == (short)RequestStatus.Unpaid)
                                 select new AdminRequest
                                 {
                                     PatientName = rc.Firstname + " " + rc.Lastname,
                                     DateOfBirth = GetPatientDOB(rc),
                                     RequestType = r.Requesttypeid,
                                     Requestor = GetRequestType(r) + " " + r.Firstname + " " + r.Lastname,
                                     RequestDate = r.Createddate.ToString("MMM dd, yyyy"),
                                     PatientPhone = rc.Phonenumber,
                                     Phone = r.Phonenumber,
                                     Address = rc.Address,
                                     Notes = rc.Notes,
                                 }).ToList();
            }

            AdminDashboardViewModel model = new AdminDashboardViewModel();
            model.adminRequests = adminRequests;
            model.DashboardStatus = status;
            return PartialView("partialTable", model);
        }

        public IActionResult adminDashboard()
        {
            AdminDashboardViewModel model = new AdminDashboardViewModel();

            var data = (from r in _context.Requests
                        join rc in _context.Requestclients on r.Requestid equals rc.Requestid
                        where r.Status == (short)RequestStatus.Unassigned
                        select new AdminRequest
                        {
                            RequestId = r.Requestid,
                            PatientName = rc.Firstname + " " + rc.Lastname,
                            DateOfBirth = GetPatientDOB(rc),
                            RequestType = r.Requesttypeid,
                            Requestor = GetRequestType(r) + " " + r.Firstname + " " + r.Lastname,
                            RequestDate = r.Createddate.ToString("MMM dd, yyyy"),
                            PatientPhone = rc.Phonenumber,
                            Phone = r.Phonenumber,
                            Address = rc.Address,
                            Notes = rc.Notes,
                        }).ToList();

            model.adminRequests = data;
            model.newReqCount = _context.Requests.Count(r => r.Status == (short)RequestStatus.Unassigned);
            model.pendingReqCount = _context.Requests.Count(r => r.Status == (short)RequestStatus.Accepted);
            model.activeReqCount = _context.Requests.Count(r => (r.Status == (short)RequestStatus.MDEnRoute) || (r.Status == (short)RequestStatus.MDOnSite));
            model.concludeReqCount = _context.Requests.Count(r => r.Status == (short)RequestStatus.Conclude);
            model.toCloseReqCount = _context.Requests.Count(r => (r.Status == (short)RequestStatus.Cancelled) || (r.Status == (short)RequestStatus.CancelledByPatient) || (r.Status == (short)RequestStatus.Closed));
            model.unpaidReqCount = _context.Requests.Count(r => r.Status == (short)RequestStatus.Unpaid);

            return View(model);
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
