using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThemeParkDatabase.Models
{
    public class MaintenanceAudit
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateResolved { get; set; }
        public string CurrentStatus { get; set; }
        public decimal EstimatedCost { get; set; }
        public int AttractionId { get; set; }
        public DateTime UpdatedOne { get; set; }
    }
}