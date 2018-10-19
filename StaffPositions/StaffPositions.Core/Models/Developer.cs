using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffPositions.Core.Models
{
    public class Developer: BaseEntity
    {

        [Required]
        [StringLength(20)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        public string Position { get; set; }

        [DisplayName("Profile Picture")]
        public string Photo { get; set; }

        #region Properties

        [Key]
        public int DeveloperId { get; set; }
        public int? TeamLeadID { get; set; } 
        public Developer TeamLead { get; set; }

        public int? ManagerID { get; set; }
        public Developer Manager { get; set; }
        #endregion
    }

  
}
