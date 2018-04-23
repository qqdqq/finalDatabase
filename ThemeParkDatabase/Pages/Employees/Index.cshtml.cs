using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThemeParkDatabase.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Mime;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace ThemeParkDatabase.Pages.Employees
{
    [Authorize(Roles = "Admin, Manager")]
    public class IndexModel : PageModel
    {
        private readonly ThemeParkDatabase.Models.ThemeParkDatabaseContext _context;

        public IndexModel(ThemeParkDatabase.Models.ThemeParkDatabaseContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get; set; }

        public async Task OnGetAsync()
        {
            Employee = await _context.Employee
                .Include(e => e.Department).ToListAsync();
        }

        public ActionResult OnGetPieChart()
        {
            var departments = _context.Department.Include(d => d.Employee).ToList();

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (JsonWriter w = new JsonTextWriter(sw))
            {
                w.WriteStartArray();
                w.WriteStartArray();
                w.WriteValue("Department Name");
                w.WriteValue("Budget");
                w.WriteEndArray();

                double budget = 0;
                foreach (var department in departments)
                {
                    w.WriteStartArray();
                    w.WriteValue(department.Name);
                    foreach (var employee in department.Employee)
                    {
                        budget += (double)employee.Salary;
                    }
                    w.WriteValue(budget);
                    w.WriteEndArray();
                    budget = 0;
                }

                w.WriteEndArray();
            }

            return new ContentResult { Content = sb.ToString(), ContentType = "application/json" };
        }

        [HttpGet("EmployeeDetails")]
        public JsonResult OnGetEmployeeDetails(int id)
        {
            //var employee = _context.Employee.Where(e => e.Id == id);
            //var employeeJson = JsonConvert.SerializeObject(employee);
            //System.Diagnostics.Debug.WriteLine(employeeJson);

            return new JsonResult(_context.Employee.Where(e => e.Id == id));
        }
    }
}
