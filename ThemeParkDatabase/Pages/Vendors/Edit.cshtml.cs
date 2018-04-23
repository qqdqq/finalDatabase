using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.Pages.Vendors
{
    public class EditModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public EditModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vendor Vendor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vendor = await _context.Vendor
                .Include(v => v.Location)
                .Include(v => v.VendorType).SingleOrDefaultAsync(m => m.Id == id);

            if (Vendor == null)
            {
                return NotFound();
            }
           ViewData["LocationId"] = new SelectList(_context.Location, "Id", "Name");
           ViewData["VendorTypeId"] = new SelectList(_context.VendorType, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("!ModelState.IsValid");
                return Page();
            }

            System.Diagnostics.Debug.WriteLine("EntityState.Modified");
            _context.Attach(Vendor).State = EntityState.Modified;

            try
            {
                System.Diagnostics.Debug.WriteLine("SaveChangesAsync");
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {;
                if (!VendorExists(Vendor.Id))
                {
                    System.Diagnostics.Debug.WriteLine("NotFound");
                    return NotFound();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Throw");
                    throw;
                }
            }

            System.Diagnostics.Debug.WriteLine("Redirect");
            return RedirectToPage("./Index");
        }

        private bool VendorExists(int id)
        {
            return _context.Vendor.Any(e => e.Id == id);
        }
    }
}
