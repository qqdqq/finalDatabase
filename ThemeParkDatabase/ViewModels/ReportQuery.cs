using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThemeParkDatabase.ViewModels
{
    public class ReportQuery
    {
        public string QueryObject { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LocationId { get; set; }
        public int AttractionTypeId { get; set; }
        public string AttractionName { get; set; }
        public int VendorTypeId { get; set; }
        public string VendorName { get; set; }
    }
}
