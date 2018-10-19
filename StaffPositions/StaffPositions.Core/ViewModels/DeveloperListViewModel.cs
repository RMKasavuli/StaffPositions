using StaffPositions.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffPositions.Core.ViewModels
{
    public class DeveloperListViewModel
    {
        //store product object

        public IEnumerable<Developer> Developers { get; set; }
        //public IEnumerable<DeveloperPosition> DeveloperPositions { get; set; }
        public List<string> DeveloperPositions = new List<string>();
      
    }
}
