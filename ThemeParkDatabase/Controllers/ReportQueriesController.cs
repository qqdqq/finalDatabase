using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThemeParkDatabase.Models;
using ThemeParkDatabase.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ThemeParkDatabase.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReportQueriesController : Controller
    {
        private readonly ThemeParkDatabaseContext _context;

        public ReportQueriesController(ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public PartialViewResult ReportQuery(IFormCollection collection)
        {
            var query = new ReportQuery()
            {
                QueryObject = collection["query-object"],
                AttractionName = collection["attractionName"],
                VendorName = collection["vendorName"]
            };

            if (collection["start-date"] != "")
            {
                query.StartDate = DateTime.Parse(collection["start-date"]);
            }
            else
            {
                query.StartDate = new DateTime(2000, 1, 1);
            }
            if (collection["end-date"] != "")
            {
                query.EndDate = DateTime.Parse(collection["end-date"]);
            }
            else
            {
                query.EndDate = DateTime.Now;
            }
            if (collection["locationId"] != "All")
            {
                query.LocationId = Convert.ToInt32(collection["locationId"]);
            }
            else
            {
                query.LocationId = -1;
            }
            if (collection["attractionTypeId"] != "All")
            {
                query.AttractionTypeId = Convert.ToInt32(collection["attractionTypeId"]);
            }
            else
            {
                query.AttractionTypeId = -1;
            }
            if (collection["vendorTypeId"] != "All")
            {
                query.VendorTypeId = Convert.ToInt32(collection["attractionTypeId"]);
            }
            else
            {
                query.VendorTypeId = -1;
            }

            if (query.QueryObject == "AttractionsQuery")
            {
                var attractionsQuery = new AttractionsQuery();
                attractionsQuery.ReportQuery = query;
                attractionsQuery.Attractions = _context.Attraction
                    .Include(a => a.Location)
                    .Include(a => a.AttractionType)
                    .Include(a => a.AttractionVisit)
                    .Include(a => a.MaintenanceRequest).ToList();

                return PartialView("/Pages/ReportQueries/_AttractionsQuery.cshtml", attractionsQuery);
            }
            else if (query.QueryObject == "VendorsQuery")
            {
                var vendorsQuery = new VendorsQuery();
                vendorsQuery.ReportQuery = query;
                vendorsQuery.Vendors = _context.Vendor
                        .Include(v => v.VendorSalesReport)
                        .Include(v => v.VendorType)
                        .Include(v => v.Location).ToList();
                vendorsQuery.VendorTypes = _context.VendorType.ToList();

                return PartialView("/Pages/ReportQueries/_VendorsQuery.cshtml", vendorsQuery);
            }
            else if (query.QueryObject == "VisitorsQuery")
            {
                var visitorsQuery = new VisitorsQuery();
                visitorsQuery.ReportQuery = query;
                visitorsQuery.Visitors = _context.Visitor
                    .Include(v => v.Ticket).ToList();
                visitorsQuery.DailyParkReports = _context.DailyParkReport.ToList();

                return PartialView("/Pages/ReportQueries/_VisitorsQuery.cshtml", visitorsQuery);
            }

            System.Diagnostics.Debug.WriteLine("Invalid query object.");
            return PartialView();
        }
    }
}