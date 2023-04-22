using System.Data.Entity;

namespace TestNinja.Mocking.Employee
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public override int SaveChanges()
        {
            const int employeeId = 1;

            return employeeId;
        }
    }
}