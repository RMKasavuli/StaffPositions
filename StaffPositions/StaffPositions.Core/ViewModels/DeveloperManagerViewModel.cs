using StaffPositions.Core.Models;
using System;
using System.Collections.Generic;
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
    }
}
