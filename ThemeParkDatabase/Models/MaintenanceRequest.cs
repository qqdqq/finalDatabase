using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class MaintenanceRequest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime? DateResolved { get; set; }
        public string CurrentStatus { get; set; }
        public int EstimatedCost { get; set; }
        public int AttractionId { get; set; }

        public  Attraction Attraction { get; set; }
    }
}
