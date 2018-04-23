using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.Pages.WeatherAudits
{
    public class DeleteModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public DeleteModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WeatherAudit WeatherAudit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeatherAudit = await _context.WeatherAudit.SingleOrDefaultAsync(m => m.Id == id);

            if (WeatherAudit == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WeatherAudit = await _context.WeatherAudit.FindAsync(id);

            if (WeatherAudit != null)
            {
                _context.WeatherAudit.Remove(WeatherAudit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
