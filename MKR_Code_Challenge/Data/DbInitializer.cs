using MKR_Code_Challenge.Models;
using MKR_Code_Challenge.Models.ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKR_Code_Challenge.Data
{
    public class DbInitializer
    {
        public static void Initialize(CompanyX_Context context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Employees.Any()) return;   // DB has been seeded

            var employees = new Employee[]
            {
            new Employee{FirstName="Carson",LastName="Alexander",PhoneNumber="111-222-3333"},
            new Employee{FirstName="Meredith",LastName="Alonso",PhoneNumber="222-333-5555"},
            new Employee{FirstName="Arturo",LastName="Anand",PhoneNumber="999-888-7777"},
            new Employee{FirstName="Gytis",LastName="Barzdukas",PhoneNumber="444-555-6666"},
            new Employee{FirstName="Yan",LastName="Li",PhoneNumber="666-777-8888"},
            new Employee{FirstName="Peggy",LastName="Justice",PhoneNumber="444-333-2222"},

            };
            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();
      
            var departments = new Department[]
            {
            new Department{EmployeeID=1,DepartmentName = "Accounting"},
            new Department{EmployeeID=2,DepartmentName = "HR"},
            new Department{EmployeeID=3,DepartmentName = "Engineering"},
            new Department{EmployeeID=4,DepartmentName = "Accounting"},
            new Department{EmployeeID=5,DepartmentName = "HR"},
            new Department{EmployeeID=6,DepartmentName = "Engineering"},

            };
            foreach (Department d in departments)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();
        }

    }
}
