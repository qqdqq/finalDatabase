using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class AttractionVisit
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int AttractionId { get; set; }

        public  Attraction Attraction { get; set; }
    }
}
