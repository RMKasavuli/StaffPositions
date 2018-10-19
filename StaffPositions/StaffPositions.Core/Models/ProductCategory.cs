using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffPositions.Core.Models
{
    public class ProductCategory:BaseEntity
    {
        //public string Id { get; set; }, base entity already has an Id
        public string Category { get; set; }

    }
}
