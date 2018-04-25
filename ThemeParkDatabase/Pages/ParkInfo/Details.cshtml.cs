using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThemeParkDatabase.Models;
using Microsoft.AspNetCore.Authorization;

namespace ThemeParkDatabase.Pages.ParkInfo
{
    public class DetailsModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public DetailsModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

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
    }
}
