using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class Attraction
    {
        public Attraction()
        {
            
            AttractionVisit = new HashSet<AttractionVisit>();
            MaintenanceRequest = new HashSet<MaintenanceRequest>();
            
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public int AttractionTypeId { get; set; }

        public  AttractionType AttractionType { get; set; }
        public  Location Location { get; set; }
        public  ICollection<AttractionVisit> AttractionVisit { get; set; }
        public  ICollection<MaintenanceRequest> MaintenanceRequest { get; set; }
    }
}
