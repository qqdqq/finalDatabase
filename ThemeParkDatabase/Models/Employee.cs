using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThemeParkDatabase.Models
{
    public partial class Employee
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Initial")]
        public string MiddleInitial { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Title { get; set; }
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }

        public  Department Department { get; set; }
    }
}
