using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocDAL.ViewModel
{
    public class ConciergeRequestViewModel
    {
        public PatientRequestViewModel PatientRequest { get; set; }
        public string? ConciergeFirstName { get; set; }

        public string? ConciergeLastName { get; set; }

        public string? ConciergePhone { get; set; }

        public string? ConciergeEmail { get; set; }
        
        public string? ConciergeProperty { get; set; }

        public string? ConciergeStreet{ get; set; }

        public string? ConciergeCity { get; set; }

        public string? ConciergeState { get; set; }

        public string? ConciergeZip { get; set; }



    }
}
