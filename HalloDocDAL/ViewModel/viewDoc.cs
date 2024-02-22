using HalloDocDAL.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocDAL.ViewModel
{
    public class viewDoc
    {
        public string Username { get; set; }

        public string ConfirmationNo { get; set; }

        public int RequestID { get; set; }

        public List<Requestwisefile> Requestwisefiles { get; set; }

        public IFormFile? File { get; set; }
    }
}
