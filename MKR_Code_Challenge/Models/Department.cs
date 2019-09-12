using MKR_Code_Challenge.Models.ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKR_Code_Challenge.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
