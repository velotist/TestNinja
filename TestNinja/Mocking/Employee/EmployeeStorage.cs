using TestNinja.Mocking.Interfaces;

namespace TestNinja.Mocking.Employee
{
    public class EmployeeStorage : IEmployeeStorage
    {
        private readonly EmployeeContext _dbContext;

        public EmployeeStorage()
        {
            _dbContext = new EmployeeContext();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _dbContext.Employees.Find(id);

            if (employee == null)
                return;

            _dbContext.Employees.Remove(employee);

            _dbContext.SaveChanges();
        }
    }
}