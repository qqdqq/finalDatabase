using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.Pages.Attractions
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
        ViewData["AttractionTypeId"] = new SelectList(_context.AttractionType, "Id", "Name");
        ViewData["LocationId"] = new SelectList(_context.Location, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Attraction Attraction { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attraction.Add(Attraction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}