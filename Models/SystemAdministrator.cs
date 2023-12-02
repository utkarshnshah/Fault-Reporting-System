using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FaultReportingSystem.Models
{
    public class SystemAdministrator
    {
        [Key]
        [DisplayName("System Administrator")]
        public int SystemAdministratorId { get; set; }

        [DisplayName("First Name")]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50)]
        public string? LastName { get; set; }

        [DisplayName("Email")]
        [StringLength(250)]
        public string Email { get; set; }

        [DisplayName("Password")]
        [StringLength(25)]
        public string Password { get; set; }

        [DisplayName("Contact Number")]
        [StringLength(25)]
        public string? ContactNumber { get; set; }
    }
}