using TestNinja.Mocking.Interfaces;

namespace TestNinja.Mocking.Employee
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
}