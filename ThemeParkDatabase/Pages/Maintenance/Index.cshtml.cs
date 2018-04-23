using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThemeParkDatabase.Models;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace ThemeParkDatabase.Pages.Maintenance
{
    public class IndexModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public IndexModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        public IList<MaintenanceRequest> MaintenanceRequest { get;set; }

        public async Task OnGetAsync()
        {
            MaintenanceRequest = await _context.MaintenanceRequest
                .Include(m => m.Attraction).ToListAsync();
        }

        public ActionResult OnGetMaintenanceCostGraph()
        {
            var attractions = _context.Attraction.Include(a => a.MaintenanceRequest).ToList();
        
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter w = new JsonTextWriter(sw))
            {
                w.Formatting = Formatting.Indented;

                w.WriteStartArray();
                w.WriteStartArray();
                w.WriteValue("Name");
                w.WriteValue("Cost");
                w.WriteStartObject();
                w.WritePropertyName("role");
                w.WriteValue("style");
                w.WriteEndObject();
                w.WriteEndArray();

                double cost = 0;
                foreach (var attraction in attractions)
                {
                    w.WriteStartArray();
                    w.WriteValue(attraction.Name);
                    foreach (var request in attraction.MaintenanceRequest)
                    {
                        cost += (double)request.EstimatedCost;
                    }
                    w.WriteValue(cost);
                    w.WriteValue("");
                    w.WriteEndArray();
                    cost = 0;
                }
            }

            return new ContentResult { Content = sb.ToString(), ContentType = "application/json" };
        }

        public ActionResult OnGetCostNumberGraph()
        {
            var attractions = _context.Attraction.Include(a => a.MaintenanceRequest).ToList();
            double totalCost = 0;
            int numOfRequests = 0;

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter w = new JsonTextWriter(sw))
            {
                w.Formatting = Formatting.Indented;

                w.WriteStartArray();

                w.WriteStartArray();
                w.WriteValue("Name");
                w.WriteValue("Total Cost");
                w.WriteValue("Number of Requests");
                w.WriteEndArray();

                foreach (var attraction in attractions)
                {
                    w.WriteStartArray();
                    w.WriteValue(attraction.Name);
                    foreach (var report in attraction.MaintenanceRequest)
                    {
                        totalCost += (double)report.EstimatedCost;
                        numOfRequests++;
                    }
                    w.WriteValue(totalCost);
                    w.WriteValue(numOfRequests);
                    w.WriteEndArray();        
                    totalCost = 0;
                    numOfRequests = 0;
                }

                w.WriteEndArray();

                return new ContentResult { Content = sb.ToString(), ContentType = "application/json" };
            }
        }
    }
}
