using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ThemeParkDatabase.Models;

namespace ThemeParkDatabase.Pages.Attractions
{

    [Authorize(Roles = "Admin, Manager, Employee")]
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

        [HttpGet("AttractionDetails")]
        public JsonResult OnGetAttractionDetails(int id)
        {
            var attraction = _context.Attraction.Where(e => e.Id == id).First();
            var location = _context.Location.Where(l => l.Id == id).First();

            return new JsonResult(new
            {
                Id = id,
                name = attraction.Name,
                description = attraction.Description,
                loc = location.Name
            });
        }


        public ActionResult OnGetAttractionPopularityGraph()
        {
            var attractions = _context.Attraction.Include(a => a.AttractionVisit).ToList();
         
            var dictionary = new Dictionary<DateTime, AttractionStruct>();
            DateTime lowest = attractions.First().AttractionVisit.First().Time;
            DateTime highest = lowest;
                //= new DateTime();


            foreach (var attraction in attractions) // Get start and end range
            {
                foreach (var visit in attraction.AttractionVisit)
                {
                    if (visit.Time.CompareTo(lowest) < 0)
                    {
                        lowest = visit.Time;
                    }
                    else if (visit.Time.CompareTo(highest) > 0)
                    {
                        highest = visit.Time;
                    }
                }
            }

            for (int i = lowest.Year; i <= highest.Year; i++)
            {
                for (int j = 1; j <= 12; j++) // Now going through all possible keys
                {
                    var visitsPerMonth = new List<AttractionStruct>();
                    foreach (var attraction in attractions) // Each attraction in the given month
                    {
                        var numVisits = attraction.AttractionVisit.Where(a => (a.Time.Year == i) && (a.Time.Month == j)).Count();
                        visitsPerMonth.Add(new AttractionStruct() { Name = attraction.Name, NumVisits = numVisits });
                    }
                    var top = visitsPerMonth.OrderByDescending(v => v.NumVisits).First();
                    if (top.NumVisits == 0) top.Name = "";

                    dictionary.Add(new DateTime(i, j, 1), top);
                }
            }

            for (int i = 1; i < lowest.Month; i++)
            {
                dictionary.Remove(new DateTime(lowest.Year, i, 1));
            }
            for (int i = highest.Month + 1; i <= 12; i++)
            {
                dictionary.Remove(new DateTime(highest.Year, i, 1));
            }

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter w = new JsonTextWriter(sw))
            {
                w.WriteStartArray();
                w.WriteStartArray();
                w.WriteValue("Date");
                w.WriteValue("Number of visitors");
                w.WriteStartObject();
                w.WritePropertyName("role");
                w.WriteValue("tooltip");
                w.WriteEndObject();
                w.WriteEndArray();

                foreach (var key in dictionary.Keys)
                {
                    w.WriteStartArray();
                    w.WriteValue(String.Format("{0:MMMM yyyy}", key));
                    w.WriteValue(dictionary[key].NumVisits);
                    w.WriteValue(dictionary[key].Name);
                    w.WriteEndArray();
                }

                w.WriteEndArray();
            }

            return new ContentResult { Content = sb.ToString(), ContentType = "application/json" };
        }
    }

    public class AttractionStruct
    {
        public string Name { get; set; }
        public int NumVisits { get; set; }
    }
}
