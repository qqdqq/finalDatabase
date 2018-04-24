using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class DailyParkReport
    {
        public int Id { get; set; }
        public int NumVisitors { get; set; }
        public bool Rainout { get; set; }
        public double Temperature { get; set; }
        public double InchesPercipitation { get; set; }
        public DateTime Date { get; set; }
    }
}
