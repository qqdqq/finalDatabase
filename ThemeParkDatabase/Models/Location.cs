using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class Location
    {
        public Location()
        {
            Attraction = new HashSet<Attraction>();
            Vendor = new HashSet<Vendor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public  ICollection<Attraction> Attraction { get; set; }
        public  ICollection<Vendor> Vendor { get; set; }
    }
}
