using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.Pages.ParkInfo
{
    public class EditModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public EditModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ParkInfomation ParkInfomation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ParkInfomation = await _context.ParkInfomation.SingleOrDefaultAsync(m => m.Id == id);

            if (ParkInfomation == null)
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

            _context.Attach(ParkInfomation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParkInfomationExists(ParkInfomation.Id))
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

        private bool ParkInfomationExists(int id)
        {
            return _context.ParkInfomation.Any(e => e.Id == id);
        }
    }
}
