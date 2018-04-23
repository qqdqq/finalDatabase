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

            for (int i = lowest.Year; i <= highest.Year; i++) // Fill up the dictionary with keys between the ranges
            {
                for (int j = 1; j <= 12; j++)
                {
                    dictionary.Add(new DateTime(i, j, 1), new AttractionStruct());
                }
            }

            for (int i = 1; i < lowest.Month; i++) // Remove unecessary keys
            {
                dictionary.Remove(new DateTime(lowest.Year, i, 1));
            }
            for (int i = highest.Month + 1; i <= 12; i++)
            {
                dictionary.Remove(new DateTime(highest.Year, i, 1));
            }

            var dictionaryCopy = new Dictionary<DateTime, AttractionStruct>();
            var attractionPerMonth = new List<AttractionStruct>();
            foreach (var key in dictionary.Keys)
            {
                System.Diagnostics.Debug.WriteLine("\nDate: " + key + "\n");
                foreach (var attraction in attractions)
                {
                    var visits = attraction.AttractionVisit.Where(v => (v.Time.Month == key.Month) && (v.Time.Year == key.Year));
                    var numOfVisits = visits.Count();
                    System.Diagnostics.Debug.WriteLine("\tName: " + attraction.Name + "\n\tVisits: " + numOfVisits + "\n");
                   attractionPerMonth.Add(new AttractionStruct() { Name = attraction.Name, NumVisits = numOfVisits });
                }
                var highestNumVisits = attractionPerMonth.OrderByDescending(a => a.NumVisits);
                System.Diagnostics.Debug.WriteLine(highestNumVisits.First().Name);
              //System.Diagnostics.Debug.WriteLine("Date: " + key + "\nName: " + highestNumVisits.Name + "\nVisits: " + highestNumVisits.NumVisits + "\n");
              //dictionaryCopy.Add(key, highestNumVisits);
            }

            foreach (var key in dictionaryCopy.Keys)
            {
                //System.Diagnostics.Debug.WriteLine("Name: " + dictionaryCopy[key].Name + "\nNumber of visits: " + dictionaryCopy[key].NumVisits);
            }

            //foreach (var attraction in attractions) // Increment visit counts
            //{
            //    foreach (var visit in attraction.AttractionVisit)
            //    {
            //        var key = new DateTime(visit.Time.Year, visit.Time.Month, 1);
            //        dictionary[key]++;
            //    }
            //}

            return new EmptyResult();

            //StringBuilder sb = new StringBuilder();
            //StringWriter sw = new StringWriter(sb);
            //using (JsonWriter w = new JsonTextWriter(sw))
            //{
            //    w.WriteStartArray();
            //    w.WriteStartArray();
            //    w.WriteValue("Date");
            //    w.WriteValue("Number of visitors");
            //    w.WriteEndArray();

            //    foreach (var key in dictionary.Keys)
            //    {
            //        w.WriteStartArray();
            //        w.WriteValue(String.Format("{0:MMMM yyyy}", key));
            //        w.WriteValue(dictionary[key]);
            //        w.WriteEndArray();
            //    }

            //    w.WriteEndArray();
            //}

            //return new ContentResult { Content = sb.ToString(), ContentType = "application/json" };
        }
    }

    public class AttractionStruct
    {
        public string Name { get; set; }
        public int NumVisits { get; set; }
    }
}
