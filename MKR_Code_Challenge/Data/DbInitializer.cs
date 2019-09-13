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

            var departments = new List<Department>
            {
            new Department{DepartmentName = "Accounting", Employee = new List<Employee>()},
            new Department{DepartmentName = "HR", Employee = new List<Employee>()},
            new Department{DepartmentName = "Engineering", Employee = new List<Employee>()},
            };

            departments.ForEach(d => context.Departments.Add(d));
            context.SaveChanges();


            // Look for any students.
            if (context.Employees.Any()) return;   // DB has been seeded

            var employees = new List<Employee>
            {
            new Employee{FirstName="Carson",LastName="Alexander",PhoneNumber="111-222-3333", DepartmentID = 1, Departments = departments.FirstOrDefault(d => d.DepartmentID == 1)},
            new Employee{FirstName="Meredith",LastName="Alonso",PhoneNumber="222-333-5555", DepartmentID = 2, Departments = departments.FirstOrDefault(d => d.DepartmentID == 2)},
            new Employee{FirstName="Arturo",LastName="Anand",PhoneNumber="999-888-7777",DepartmentID = 3, Departments = departments.FirstOrDefault(d => d.DepartmentID == 3)},
            new Employee{FirstName="Gytis",LastName="Barzdukas",PhoneNumber="444-555-6666",DepartmentID = 1, Departments = departments.FirstOrDefault(d => d.DepartmentID == 1)},
            new Employee{FirstName="Yan",LastName="Li",PhoneNumber="666-777-8888",DepartmentID = 2, Departments = departments.FirstOrDefault(d => d.DepartmentID == 2)},
            new Employee{FirstName="Peggy",LastName="Justice",PhoneNumber="444-333-2222",DepartmentID = 3, Departments = departments.FirstOrDefault(d => d.DepartmentID == 3)},

            };

            employees.ForEach(e => context.Employees.Add(e));
            context.SaveChanges();

            departments[0].Employee.Add(employees[0]); // in list departments at index 0 add employees from list employees at index 0
            departments[1].Employee.Add(employees[1]); // in list departments at index 1 add employees from list employees at index 1
            departments[2].Employee.Add(employees[2]); // in list departments at index 2 add employees from list employees at index 2
            departments[0].Employee.Add(employees[3]); // in list departments at index 0 add employees from list employees at index 3
            departments[1].Employee.Add(employees[4]); // in list departments at index 1 add employees from list employees at index 4
            departments[2].Employee.Add(employees[5]); // in list departments at index 2 add employees from list employees at index 5

            context.SaveChanges();
        }

    }
}
