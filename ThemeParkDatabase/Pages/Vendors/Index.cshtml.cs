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

namespace ThemeParkDatabase.Pages.Vendors
{

    [Authorize(Roles= "Admin, Manager")]
    public class IndexModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public IndexModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        public IList<Vendor> Vendor { get;set; }

        public async Task OnGetAsync()
        {
            Vendor = await _context.Vendor
                .Include(v => v.Location)
                .Include(v => v.VendorType).ToListAsync();
        }

        public ActionResult OnGetSalesGraph()
        {
            var vendors = _context.Vendor.Include(v => v.VendorSalesReport).ToList();
            var salesReports = new List<VendorSalesReport>();
            var dictionary = new Dictionary<DateTime, decimal[]>(); // [0] is total, [1] is goal

            foreach (var vendor in vendors)
            {
                foreach (var salesReport in vendor.VendorSalesReport)
                {
                    salesReports.Add(salesReport);
                }
            }

            foreach (var report in salesReports)
            {
                if (dictionary.ContainsKey(report.Date))
                {
                    dictionary[report.Date][0] += report.TotalSales;
                    dictionary[report.Date][1] += report.SalesGoal;
                }
                else
                {
                    dictionary.Add(report.Date, new decimal[] { report.TotalSales, report.SalesGoal});
                }
            }

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter w = new JsonTextWriter(sw))
            {
                w.WriteStartArray();
                w.WriteStartArray();
                w.WriteValue("Date");
                w.WriteValue("Sales");
                w.WriteValue("Sales Goal");
                w.WriteEndArray();

                foreach (var key in dictionary.Keys)
                {
                    w.WriteStartArray();
                    w.WriteValue(String.Format("{0:MM/dd/yyyy}", key));
                    w.WriteValue(dictionary[key][0]);
                    w.WriteValue(dictionary[key][1]);
                    w.WriteEndArray();
                }

                w.WriteEndArray();
            }

            return new ContentResult { Content = sb.ToString(), ContentType = "application/json" };

        }
    }
}
