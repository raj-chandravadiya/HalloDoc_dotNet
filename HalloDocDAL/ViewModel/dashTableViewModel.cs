using HalloDocDAL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocDAL.ViewModel
{
    public  class dashTableViewModel
    {
        public string? UserName { get; set; } 
        public List<Request> requests { get; set; }
        public List<int> fileCount { get; set; }
    }
}
