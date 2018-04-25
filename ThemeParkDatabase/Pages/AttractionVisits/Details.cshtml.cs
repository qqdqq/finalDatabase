using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.Pages.AttractionVisits
{
    public class DetailsModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public DetailsModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        public AttractionVisit AttractionVisit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AttractionVisit = await _context.AttractionVisit
                .Include(a => a.Attraction).SingleOrDefaultAsync(m => m.Id == id);

            if (AttractionVisit == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
