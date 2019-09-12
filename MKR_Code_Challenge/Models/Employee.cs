using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKR_Code_Challenge.Models
{
    namespace ContosoUniversity.Models
    {
        public class Employee
        {
            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }          
            public string PhoneNumber { get; set; }

            public ICollection<Department> Departments { get; set; }
        }
    }
}
