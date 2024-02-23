
using HalloDocDAL.DataContext;
using HalloDocDAL.DataModels;
using HalloDocDAL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;

namespace HalloDocMVC.Controllers
{
    public class patientDashController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public patientDashController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult patientDashboard()
        {
            int userId = (int)HttpContext.Session.GetInt32("userId");
            User user = _context.Users.FirstOrDefault(u => u.Userid == userId);
            //query
            //var dashTable = from req in _context.Requests
            //                join file in _context.Requestwisefiles
            //                on req.Requestid equals file.Requestid
            //                select new dashTableViewModel()
            //                {
            //                    date = req.Createddate,
            //                    status = "unassigned",
            //                };

            List<Request> reqList = _context.Requests.Where(req => req.Userid == userId).ToList();

            List<int> filecount = new List<int>();
            for (int i = 0; i < reqList.Count; i++)
            {
                int count = _context.Requestwisefiles.Count(file => file.Requestid == reqList[i].Requestid);
                filecount.Add(count);
            }

            dashTableViewModel dashtable = new dashTableViewModel()
            {
                UserName = user.Firstname + " " + user.Lastname,
                requests = reqList,
                fileCount = filecount,
            };

            return View(dashtable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult viewDocuments(viewDoc model)
        {
            if (model.File != null)
            {
                Guid myuuid = Guid.NewGuid();
                var filename = Path.GetFileName(model.File.FileName);
                var FinalFileName = myuuid.ToString() + filename;

                //path

                var filepath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "uploads", FinalFileName);

                //copy in stream

                using (var str = new FileStream(filepath, FileMode.Create))
                {
                    //copy file
                    model.File.CopyTo(str);
                }

                //STORE DATA IN TABLE
                var fileupload = new Requestwisefile()
                {
                    Requestid = model.RequestID,
                    Filename = FinalFileName,
                    Createddate = DateTime.Now,
                };

                _context.Requestwisefiles.Add(fileupload);
                _context.SaveChanges();

            }
            return viewDocuments(model.RequestID);

        }

        //For Download All

        //public async Task<IActionResult> DownloadAllFiles(int requestId)
        //{
        //    try
        //    {
        //        // Fetch all document details for the given request:
        //        var documentDetails = _context.Requestwisefiles.Where(m => m.Requestid == requestId).ToList();

        //        if (documentDetails == null || documentDetails.Count == 0)
        //        {
        //            return NotFound("No documents found for download");
        //        }

        //        // Create a unique zip file name
        //        var zipFileName = $"Documents_{DateTime.Now:yyyyMMddHHmmss}.zip";
        //        var zipFilePath = Path.Combine(_environment.WebRootPath, "DownloadableZips", zipFileName);

        //        // Create the directory if it doesn't exist
        //        var zipDirectory = Path.GetDirectoryName(zipFilePath);
        //        if (!Directory.Exists(zipDirectory))
        //        {
        //            Directory.CreateDirectory(zipDirectory);
        //        }

        //        // Create a new zip archive
        //        using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
        //        {
        //            // Add each document to the zip archive
        //            foreach (var document in documentDetails)
        //            {
        //                var documentPath = Path.Combine(_environment.WebRootPath, "uploads", document.Filename);
        //                zipArchive.CreateEntryFromFile(documentPath, document.Filename);
        //            }
        //        }

        //        // Return the zip file for download
        //        var zipFileBytes = await System.IO.File.ReadAllBytesAsync(zipFilePath);
        //        return File(zipFileBytes, "application/zip", zipFileName);
        //    }
        //    catch
        //    {
        //        return BadRequest("Error downloading files");
        //    }
        //}

        //public IActionResult DownLoadAll(int requestid)
        //{
        //    var zipName = $"TestFiles-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        //required: using System.IO.Compression;  
        //        using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
        //        {
        //            //QUery the Products table and get all image content  
        //            _context.Requests.ToList().ForEach(file =>
        //            {
        //                //var entry = zip.CreateEntry(file.);
        //                //using (var fileStream = new MemoryStream(file.ProImageContent))
        //                //using (var entryStream = entry.Open())
        //                //{
        //                //    fileStream.CopyTo(entryStream);
        //                //}
        //            });
        //        }
        //        return File(ms.ToArray(), "application/zip", zipName);
        //    }
        //}

        public IActionResult viewDocuments(int requestid)
        {

            int? userId = (int)HttpContext.Session.GetInt32("userId");
            User user = _context.Users.FirstOrDefault(u => u.Userid == userId);
            List<Requestwisefile> listfiles = _context.Requestwisefiles.Where(reqfile => reqfile.Requestid == requestid).ToList();
            viewDoc userName = new()
            {
                Username = user.Firstname + " " + user.Lastname,
                Requestwisefiles = listfiles,

            };

            return View(userName);
        }

        public IActionResult patientInformationByMe(int userId)
        {
            var link = _context.Users.Where(x => x.Userid == userId).FirstOrDefault();

            if (link == null)
            {
                return View();
            }
            var models = new patientInformationMe()
            {
                Email = link.Email,
                FirstName = link.Firstname

            };
            return View(models);
        }

        
        public IActionResult patientInformationBySomeone()
        {
            return View();   
        }

        public IActionResult patientProfile()
        {
            return View();
        }
    }
}






