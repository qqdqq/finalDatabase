using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.Pages.AttractionTypes
{
    public class EditModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public EditModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AttractionType AttractionType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AttractionType = await _context.AttractionType.SingleOrDefaultAsync(m => m.Id == id);

            if (AttractionType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AttractionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttractionTypeExists(AttractionType.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AttractionTypeExists(int id)
        {
            return _context.AttractionType.Any(e => e.Id == id);
        }
    }
}
