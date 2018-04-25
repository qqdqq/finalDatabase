using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThemeParkDatabase.Models
{
    public class WeatherAudit
    {
        public int WeatherAuditId { get; set; }
        public int Id { get; set; }
        public bool Rainout { get; set; }
        public double Temperature { get; set; }
        public double InchesPrecipitation { get; set; }
        public DateTime Date { get; set; }
    }
}
