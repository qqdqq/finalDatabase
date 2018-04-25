using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime RedeemedDate { get; set; }
        public int TicketTypeId { get; set; }
        public int VisitorId { get; set; }

        public  TicketType TicketType { get; set; }
        public  Visitor Visitor { get; set; }
    }
}
