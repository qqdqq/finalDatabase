using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class ParkInfomation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
    }
}
