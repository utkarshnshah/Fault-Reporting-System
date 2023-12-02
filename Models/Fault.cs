using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaultReportingSystem.Models
{
    public enum FaultType
    {
        Documentation,
        New_Feature_Specification,
        Genuine_Fault
    }
    public enum PriorityLevel
    {
        Undecided,
        Minor,
        Major
    }
    public enum Status
    {
        New,
        Open,
        Work_In_Progress,
        Solved
    }
    public enum ReportMethod
    {
        Phone,
        Email,
        Website,
        Other
    }
    public class Fault
    {
        [Key]
        [DisplayName("Fault Id")]
        public int FaultId { get; set; }

        [DisplayName("Reported On")]
        public DateTime DateReported { get; set; }

        [DisplayName("Priority")]
        [StringLength(50)]
        public string Priority { get; set; }

        [DisplayName("Problem")]
        [StringLength(250)]
        public string Problem { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [DisplayName("Closed On")]
        public DateTime? ClosedOn { get; set; }

        [DisplayName("Fault Type")]
        [StringLength(50)]
        public string FaultType{ get; set; }

        [DisplayName("Report Method")]
        [StringLength(25)]
        public string ReportMethod { get; set; }

        [DisplayName("Customer")]
        public int? CustomerId { get; set; }
        [DisplayName("Customer")]
        public Customer? Customer { get; set; }

        [DisplayName("Assigned Developer")]
        public int? DeveloperId { get; set; }
        [DisplayName("Assigned Developer")]
        public Developer? Developer { get; set; }

        [DisplayName("Help Desk Staff")]
        public int? HelpDeskId { get; set; }
        [DisplayName("Help Desk Staff")]
        public HelpDesk? HelpDesk { get; set; }

        [DisplayName("Software Product")]
        public int? SoftwareProductId { get; set; }
        [DisplayName("Software Product")]
        public SoftwareProduct? SoftwareProduct { get; set; }

        public ICollection<FaultLog>? FaultLogs { get; set; }
    }
}
