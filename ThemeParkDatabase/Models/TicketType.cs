using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class TicketType
    {
        public TicketType()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public  ICollection<Ticket> Ticket { get; set; }
    }
}
