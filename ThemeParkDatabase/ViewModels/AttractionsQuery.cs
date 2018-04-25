using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.ViewModels
{
    public class AttractionsQuery
    {
        public ReportQuery ReportQuery { get; set; }
        public List<Attraction> Attractions { get; set; }
    }
}
