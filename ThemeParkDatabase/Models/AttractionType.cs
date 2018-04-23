using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class AttractionType
    {
        public AttractionType()
        {
            Attraction = new HashSet<Attraction>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public  ICollection<Attraction> Attraction { get; set; }
    }
}
