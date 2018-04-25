using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class Visitor
    {
        public Visitor()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public  ICollection<Ticket> Ticket { get; set; }
    }
}
