using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaultReportingSystem.Models
{
    public class Developer
    {
        [Key]
        public int DeveloperId { get; set; }
       
        [DisplayName("First Name")]
        [StringLength(50)]
        public string? DeveloperFirstName { get; set; }
       
        [DisplayName("Last Name")]
        [StringLength(50)]
        public string? DeveloperLastName { get; set; }
        
        [DisplayName("Email")]
        [StringLength(250)]
        public string DeveloperEmail { get; set; }
        
        [DisplayName("Password")]
        [StringLength(25)]
        public string DeveloperPassword { get; set; }
       
        [DisplayName("Contact Number")]
        [StringLength(25)]
        public string? DeveloperContactNumber { get; set; }

        [DisplayName("Manager")]
        public int? ManagerId { get; set; }
        [DisplayName("Manager")]
        public Manager? Manager { get; set; }

        [NotMapped]
        [DisplayName("Developer")]
        public string DeveloperFullNameWithEmail => $"{DeveloperFirstName} {DeveloperLastName} ({DeveloperEmail})";

    }
}
