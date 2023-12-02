using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaultReportingSystem.Models
{
    public class Manager
    {
        [Key]
        [DisplayName("Manager")]
        public int ManagerId { get; set; }

        [DisplayName("First Name")]
        [StringLength(50)] 
        public string? ManagerFirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50)] 
        public string? ManagerLastName { get; set; }

        [DisplayName("Email")]
        [StringLength(250)] 
        public string ManagerEmail { get; set; }

        [DisplayName("Password")]
        [StringLength(25)] 
        public string ManagerPassword { get; set; }
       
        [DisplayName("Contact Number")]
        [StringLength(25)]
        public string? ManagerContactNumber { get; set; }

        [NotMapped]
        [DisplayName("Manager")]
        public string ManagerFullNameWithEmail => $"{ManagerFirstName} {ManagerLastName} ({ManagerEmail})";
    }
}
