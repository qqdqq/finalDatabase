using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.ViewModels
{
    public class VendorsQuery
    {
        public ReportQuery ReportQuery { get; set; }
        public List<Vendor> Vendors { get; set; }
        public List<VendorType> VendorTypes { get; set; }
    }
}
