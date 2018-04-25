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

namespace ThemeParkDatabase.Pages.Visitors
{
    public class IndexModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public IndexModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        public IList<Visitor> Visitor { get;set; }

        public async Task OnGetAsync()
        {
            Visitor = await _context.Visitor.ToListAsync();
        }

        public ActionResult OnGetTicketGraph()
        {
            /* var visitors = _context.Visitor.Include(v => v.Ticket).ToList(); */
            /* var ticketTypes = _context.TicketType.ToList(); */

            /* QUERY: select all tickets and group by ticket type */
            /* var result = from entity in _context.Ticket */
            /*     group entity by entity.TicketTypeId into e */
            /*     select new { ticket_type = e.Key, tickets = e }; */

            /* QUERY: select all ticket types and the number of tickets of each ticket type */
            var result = from tt in _context.TicketType
                select new { ticket_type = tt.Name, ticket_count = tt.Ticket.Count() };


            /* serialize result into json */ 
            return new ContentResult { Content = JsonConvert.SerializeObject(result, Formatting.Indented), ContentType = "application/json" };
            /* return new ContentResult { Content = JsonConvert.SerializeObject(results, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), ContentType = "application/json" }; */
        }
    }
}
