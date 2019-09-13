using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MKR_Code_Challenge.Models;
using MKR_Code_Challenge.Models.ContosoUniversity.Models;

namespace MKR_Code_Challenge.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CompanyX_Context _context;

        public EmployeesController(CompanyX_Context context)
        {
            _context = context;
        }

        //The advantage of Eager Loading is that you have all of the related Entities at your disposal immeidately
        // especially when there is a constant need for access to data in any and all related tables.

        //The disadvantage of Eager Loading is the Entity Framework creates complex joins.

        //The advantage of Lazy Loading is that you only work with the entities you need.
        
        //The disadvantage of Lazy Loading is that you have multiple trips back and forth to the database and it takes
        //longer to execute any data retrievals..

        // GET: Employees
        public async Task<IActionResult> Index()
        {

            //This is an example of Eager Loading.  By "Including the Department mode" in this LINQ statement,
            //I am loading the Department immediately upon the initial database request.
            var employee = await _context.Employees
                                .Include(e => e.Departments)                        
                                .AsNoTracking()
                                .ToListAsync();

            return View(employee);
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees                       
                                .AsNoTracking()
                                .FirstOrDefaultAsync(m => m.ID == id);

            //This is an example of Lazy Loading.  I am loading the related entity, Department, in the navigation property of Employee
            //entity.  It is a delayed loading of the Department entity.
            employee.Departments = _context.Departments.Where(x => x.DepartmentID == employee.DepartmentID).SingleOrDefault();

            if (employee == null) return NotFound();

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,PhoneNumber,DepartmentID")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateDepartmentsDropDownList(employee.Departments.DepartmentID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees
                                .Include(e => e.Departments)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(m => m.ID == id);

            if (employee == null) return NotFound();

            PopulateDepartmentsDropDownList(employee.Departments.DepartmentID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ID,FirstName,LastName,PhoneNumber,DepartmentID")] Employee employeeInput)
        {
             var employee = await _context.Employees
                                .AsNoTracking()
                                .FirstOrDefaultAsync(m => m.ID == employeeInput.ID);

            if (employee != null)
            {
                _context.Remove(employee);
                await _context.SaveChangesAsync();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employeeInput.ID = 0;
                    _context.Add(employeeInput);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateDepartmentsDropDownList(employee.Departments.DepartmentID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.ID == id);

            if (employee == null) return NotFound();

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //Helper
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        { 
            var departmentsQuery = _context.Departments.GroupBy(x => x.DepartmentName).Select(x => x.FirstOrDefault());

            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "DepartmentName", selectedDepartment);
        }
    }
}
