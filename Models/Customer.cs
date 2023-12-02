using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaultReportingSystem.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [DisplayName("First Name")]
        [StringLength(50)]
        public string? CustomerFirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50)]
        public string? CustomerLastName { get; set; }
        
        [DisplayName("Email")]
        [StringLength(250)]
        public string CustomerEmail { get; set; }

        [DisplayName("Password")]
        [StringLength(50)]
        public string CustomerPassword { get; set; }
        
        [DisplayName("Contact Number")]
        [StringLength(25)]
        public string? CustomerContactNumber { get; set; }

        [NotMapped]
        public string CustomerFullNameWithEmail => $"{CustomerFirstName} {CustomerLastName} ({CustomerEmail})";

    }
}