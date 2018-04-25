using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.Pages.Attractions
{
    public class IndexModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public IndexModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        public IList<Attraction> Attraction { get;set; }

        public async Task OnGetAsync()
        {
            Attraction = await _context.Attraction
                .Include(a => a.AttractionType)
                .Include(a => a.Location).ToListAsync();
        }

        public ActionResult OnGetAttractionPopularityGraph()
        {
            /* var result = from av in _context.Attraction */
            /*     orderby av.AttractionVisit.Count() descending */
            /*     select new { attraction_id = av.Id, visit_count = av.AttractionVisit.Count() }; */

            /* var result = from av in _context.AttractionVisit */
            /*     group av by av.Time.Month into g */
            /*     select new { time = g.Key, items = g }; */

            var av = _context.AttractionVisit;

            var result = av.GroupBy(record => new {record.Time.Month, record.AttractionId }).ToList();


            /* var result = from r in result */
            /*     orderby av.AttractionVisit.Count() descending */
            /*     select new { attraction_id = av.Id, visit_count = av.AttractionVisit.Count() }; */

            /* var top_three = result.Take(3); */

            /* serialize result into json */ 
            return new ContentResult { Content = JsonConvert.SerializeObject(result, Formatting.Indented), ContentType = "application/json" };
        }
    }
}
