using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class VendorSalesReport
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalSales { get; set; }
        public decimal SalesGoal { get; set; }
        public int VendorId { get; set; }

        public  Vendor Vendor { get; set; }
    }
}
