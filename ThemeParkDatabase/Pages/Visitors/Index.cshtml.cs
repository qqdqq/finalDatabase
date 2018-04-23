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
            var visitors = _context.Visitor.Include(v => v.Ticket).ToList();
            var ticketTypes = _context.TicketType.ToList();
            var dictionary = new Dictionary<string, int>();

            foreach (var ticketType in ticketTypes)
            {
                dictionary.Add(ticketType.Name, 0);
            }
            foreach (var visitor in visitors)
            {
                foreach (var ticket in visitor.Ticket)
                {
                    var ticketType = ticketTypes.Where(tt => tt.Id == ticket.TicketTypeId).Single();
                    dictionary[ticketType.Name]++;
                }
            }

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter w = new JsonTextWriter(sw))
            {
                w.WriteStartArray();
                w.WriteStartArray();
                w.WriteValue("Name");
                w.WriteValue("Number");
                w.WriteEndArray();

                foreach (var ticketType in ticketTypes)
                {
                    w.WriteStartArray();
                    w.WriteValue(ticketType.Name);
                    w.WriteValue(dictionary[ticketType.Name]);
                    w.WriteEndArray();
                }

                w.WriteEndArray();
            }

            return new ContentResult { Content = sb.ToString(), ContentType = "application/json" };
        }
    }
}
