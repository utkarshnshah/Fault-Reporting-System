using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaultReportingSystem.Models
{
    public class HelpDesk
    {
        [Key]
        public int HelpDeskId { get; set; }

        [DisplayName("First Name")]
        [StringLength(50)]
        public string? HelpDeskFirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50)]
        public string? HelpDeskLastName { get; set; }

        [DisplayName("Email")]
        [StringLength(250)]
        public string HelpDeskEmail { get; set; }

        [DisplayName("Password")]
        [StringLength(25)]
        public string HelpDeskPassword { get; set; }

        [StringLength(50)]
        [DisplayName("Contact Number")]
        public string? HelpDeskContactNumber { get; set; }

        [NotMapped]
        public string HelpDeskFullNameWithEmail => $"{HelpDeskFirstName} {HelpDeskLastName} ({HelpDeskEmail})";

    }
}