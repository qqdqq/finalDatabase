using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class VendorType
    {
        public VendorType()
        {
            Vendor = new HashSet<Vendor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public  ICollection<Vendor> Vendor { get; set; }
    }
}
