using Microsoft.EntityFrameworkCore;
using MKR_Code_Challenge.Models;
using MKR_Code_Challenge.Models.ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKR_Code_Challenge.Repository
{
    public class CompanyX_Repository
    {
        readonly CompanyX_Context _companyX_Context;

        public CompanyX_Repository(CompanyX_Context companyX_Context)
        {
            _companyX_Context = companyX_Context;
        }


        public async Task<List<Employee>> GetEmployeesWithDepartments()
        {
            return await _companyX_Context.Employees
                                            .Include(e => e.Departments)
                                            .AsNoTracking()
                                            .ToListAsync();
        }
    }
}
