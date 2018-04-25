using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class Department
    {
        public Department()
        {
            Employee = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public int? ManagerId { get; set; }

        public  ICollection<Employee> Employee { get; set; }
    }
}
