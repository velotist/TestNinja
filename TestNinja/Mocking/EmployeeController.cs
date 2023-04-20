using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class EmployeeController
    {
        private readonly IEmployeeStorage _employeeStorage;

        public EmployeeController(IEmployeeStorage employeeStorage)
        {
            _employeeStorage = employeeStorage;
        }

        // Test cases:
        // a) method returns right result
        // b) ensure that the method deletes the given employee
        public ActionResult DeleteEmployee(int id)
        {
            _employeeStorage.DeleteEmployee(id);

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
            const int employeeId = 1;

            return employeeId;
        }
    }

    public class Employee
    {
    }
}