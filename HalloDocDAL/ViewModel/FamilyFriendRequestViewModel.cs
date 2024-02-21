using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocDAL.ViewModel
{
    public class FamilyFriendRequestViewModel
    {
        public PatientRequestViewModel PatientRequest { get; set; }
        public string? FriendFirstName { get; set; }

        public string? FriendLastName { get; set; }

        public string? FriendPhoneNo { get; set; }

        public string? FriendEmail { get; set; }

        public string? FriendRelation { get; set; }

    }
}
