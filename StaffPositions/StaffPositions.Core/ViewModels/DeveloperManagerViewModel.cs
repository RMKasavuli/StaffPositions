using StaffPositions.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffPositions.Core.ViewModels
{
    public class DeveloperManagerViewModel
    {
        //store developer object

        public Developer Developer { get; set; }
        public IEnumerable<DeveloperPosition> DeveloperPositions { get; set; }
        
        //public List<Developer> PotentialSuperiors { get; set; }
        public IEnumerable<Developer> PotentialSuperiors { get; set; }
    }
}
