using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocDAL.ViewModel
{
    public class PatientRequestViewModel
    {
        public IFormFile? uploadFile { get; set; }   
        public string? symptoms { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? DOB { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Street { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Zipcode { get; set; }

        public string? Room { get; set; }
    }
}
