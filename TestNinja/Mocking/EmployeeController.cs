﻿using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private readonly EmployeeContext _db;

        public EmployeeController()
        {
            _db = new EmployeeContext();
        }

        public ActionResult DeleteEmployee(int id)
        {
            var employee = _db.Employees.Find(id);
            if (employee != null)
                _db.Employees.Remove(employee);
            _db.SaveChanges();

            return RedirectToAction("Employees");
        }

        private ActionResult RedirectToAction(string employees)
        {
            return new RedirectResult();
        }
    }

    public class ActionResult { }
 
    public class RedirectResult : ActionResult { }
    
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public override int SaveChanges()
        {
            return 1;
        }
    }

    public class Employee
    {
    }
}