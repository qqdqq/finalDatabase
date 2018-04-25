using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.Pages.AttractionVisits
{
    public class CreateModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public CreateModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AttractionId"] = new SelectList(_context.Attraction, "Id", "Description");
            return Page();
        }

        [BindProperty]
        public AttractionVisit AttractionVisit { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AttractionVisit.Add(AttractionVisit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}