using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThemeParkDatabase.Models;
using Microsoft.AspNetCore.Authorization;

namespace ThemeParkDatabase.Pages.ParkInfo
{
    [Authorize(Roles = "Admin, Manager")]
    public class CreateModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public CreateModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ParkInfomation ParkInfomation { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ParkInfomation.Add(ParkInfomation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}