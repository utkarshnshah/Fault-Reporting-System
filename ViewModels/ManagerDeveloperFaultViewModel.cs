using System.ComponentModel;

namespace FaultReportingSystem.ViewModels
{
    public class ManagerDeveloperFaultViewModel
    {
        [DisplayName("Manager Id")]
        public int ManagerId { get; set; }
        [DisplayName("Manager")]
        public string ManagerNameWithEmail { get; set; }
        [DisplayName("Developers")]
        public List<DeveloperFaultViewModel> Developers { get; set; }

    }
}
