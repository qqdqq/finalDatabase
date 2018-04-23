using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.Pages.VendorTypes
{
    public class DeleteModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public DeleteModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public VendorType VendorType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VendorType = await _context.VendorType.SingleOrDefaultAsync(m => m.Id == id);

            if (VendorType == null)
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

            VendorType = await _context.VendorType.FindAsync(id);

            if (VendorType != null)
            {
                _context.VendorType.Remove(VendorType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
