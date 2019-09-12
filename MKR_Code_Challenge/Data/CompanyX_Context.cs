using Microsoft.EntityFrameworkCore;
using MKR_Code_Challenge.Models.ContosoUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKR_Code_Challenge.Models
{
    public class CompanyX_Context : DbContext
    {
        public CompanyX_Context(DbContextOptions<CompanyX_Context> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }


        //I am doing the overriding of the table names above to show that I can name the tables anything I want as shown below.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employee");  //Took out the "s" of Employee and Deptartment.
            modelBuilder.Entity<Department>().ToTable("Department");
        }
    }
}
