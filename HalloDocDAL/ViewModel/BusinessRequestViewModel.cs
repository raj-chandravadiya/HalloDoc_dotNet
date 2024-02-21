using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocDAL.ViewModel
{
    public class BusinessRequestViewModel
    {
        public PatientRequestViewModel PatientRequest { get; set; }
        public string? BusinessFirstName { get; set; }

        public string? BusinessLastName { get; set;}

        public string? BusinessPhone { get; set;}

        public string? BusinessEmail { get; set;}

        public string? BusinessPropertyName { get; set;}

        public string BusinessCaseNumber{ get; set;}
    }
}
