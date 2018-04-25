using System;
using System.Collections.Generic;

namespace ThemeParkDatabase.Models
{
    public partial class DeletionRequest
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int TableId { get; set; }
        public DateTime TimeRequested { get; set; }

        public int EmployeeId { get; set; }
        public  Employee Employee { get; set; }
    }
}
