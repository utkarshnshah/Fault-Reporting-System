using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaultReportingSystem.Models
{
    public class SoftwareProduct
    {
        [Key]
        public int SoftwareProductId { get; set; }

        [DisplayName("Product Name")]
        [StringLength(50)]
        public string ProductName { get; set; }
       
        [DisplayName("Product Description")]
        [StringLength(250)]
        public string ProductDescription { get; set; }

        [NotMapped]
        [DisplayName("Software Product")]
        public string SoftwareProductIdWithName => $"{ProductName} (Product Id: {SoftwareProductId})";

    }
}