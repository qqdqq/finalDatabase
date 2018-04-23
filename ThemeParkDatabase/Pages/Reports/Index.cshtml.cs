using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ThemeParkDatabase.Models;



namespace ThemeParkDatabase.Pages.Reports
{

    [Authorize(Roles = "Admin, Manager")]
    public class IndexModel : PageModel
    {
        private ThemeParkDatabaseContext _context;
        public List<DailyParkReport> _reports { get; set; }


        public IndexModel(ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            _reports = _context.DailyParkReport.ToList();
        }

        [HttpGet("ReportDetails")]
        public JsonResult OnGetReportDetails(int id)
        {
            return new JsonResult(_context.DailyParkReport.Where(r => r.Id == id));
        }

        public ActionResult OnGetNumOfVisitorsGraph()
        {
            var reports = _context.DailyParkReport.ToList();

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter w = new JsonTextWriter(sw))
            {
                w.WriteStartArray();
                w.WriteStartArray();
                w.WriteValue("Date");
                w.WriteValue("Number of Visitors");
                w.WriteEndArray();
                foreach (var report in reports)
                {
                    w.WriteStartArray();
                    w.WriteValue(report.Date);
                    w.WriteValue(report.NumVisitors);
                    w.WriteEndArray();
                }

                w.WriteEndArray();
            }

            return new ContentResult { Content = sb.ToString(), ContentType = "application/json" };
        }

        public ActionResult OnGetNumOfRainoutsGraph()
        {
            var reports = _context.DailyParkReport.ToList();

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter w = new JsonTextWriter(sw))
            {
                w.WriteStartArray();
                w.WriteStartArray();
                w.WriteValue("Date");
                w.WriteValue("Number of Rainouts");
                w.WriteEndArray();

                foreach (var report in reports)
                {
                    w.WriteStartArray();
                    w.WriteValue(String.Format("{0:MM/dd/yyyy}", report.Date));
                    w.WriteValue(Convert.ToInt32(report.Rainout));
                    w.WriteEndArray();
                }

                w.WriteEndArray();
            }

            return new ContentResult { Content = sb.ToString(), ContentType = "application/json" };
        }
    }
}

