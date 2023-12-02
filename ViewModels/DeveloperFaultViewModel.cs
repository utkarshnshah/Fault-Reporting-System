using System.ComponentModel;

namespace FaultReportingSystem.ViewModels
{
    public class DeveloperFaultViewModel
    {
        [DisplayName("Developer Id")]
        public int DeveloperId { get; set; }
        [DisplayName("Developer")]
        public string DeveloperNameWithEmail { get; set; }
        [DisplayName("Open Fault Count")]
        public int OpenFaultCount { get; set; }
    }
}
