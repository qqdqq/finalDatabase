using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThemeParkDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace ThemeParkDatabase.Pages.ReportQueries
{
    public class IndexModel : PageModel
    {
        private readonly ThemeParkDatabaseContext _context;
        public IEnumerable<Location> _locations;
        public IEnumerable<AttractionType> _attractionTypes;
        public IEnumerable<VendorType> _vendorTypes { get; set; }
        public IEnumerable<Attraction> _attractions { get; set; }
        public IEnumerable<Vendor> _vendors { get; set; }

        public IndexModel(ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            _locations = _context.Location.ToList();
            _attractionTypes = _context.AttractionType.ToList();
            _vendorTypes = _context.VendorType.ToList();
            _attractions = _context.Attraction.Include(a => a.AttractionType).ToList();
            _vendors = _context.Vendor.ToList();
        }
    }
}