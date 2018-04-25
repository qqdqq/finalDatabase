using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThemeParkDatabase.Models
{
    public partial class Vendor
    {
        public Vendor()
        {
            VendorSalesReport = new HashSet<VendorSalesReport>();
        }

        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int VendorTypeId { get; set; }

        public  Location Location { get; set; }
        public  VendorType VendorType { get; set; }
        public  ICollection<VendorSalesReport> VendorSalesReport { get; set; }
    }
}
