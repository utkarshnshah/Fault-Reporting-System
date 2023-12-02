using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaultReportingSystem.Models
{
    public class FaultLog
    {
        [Key]
        [DisplayName("Fault Log Id")]
        public int LogId { get; set; }

        [DisplayName("Date & Time")]
        public DateTime DateTime { get; set; }

        [StringLength(500)]
        [DisplayName("Action Performed")]
        public string Action { get; set; }

        [DisplayName("Developer Id")]
        public int? DeveloperId { get; set; }
        public Developer? Developer { get; set; }

        [DisplayName("Fault Id")]
        public int? FaultId { get; set; }
        public Fault? Fault { get; set; }
    }
}
