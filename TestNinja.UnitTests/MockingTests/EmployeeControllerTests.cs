using Moq;
using NUnit.Framework;
using TestNinja.Mocking.Employee;
using TestNinja.Mocking.Interfaces;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        [SetUp]
        public void SetUp()
        {
            _storage = new Mock<IEmployeeStorage>();
            _controller = new EmployeeController(_storage.Object);
        }

        private EmployeeController _controller;
        private Mock<IEmployeeStorage> _storage;

        [Test]
        public void DeleteEmployee_GivenEmployee_ReturnRedirect()
        {
            const int employeeId = 1;

            var result = _controller.DeleteEmployee(employeeId);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public void DeleteEmployee_GivenEmployee_EmployeeDeletedInStorage()
        {
            const int employeeId = 1;

            _controller.DeleteEmployee(employeeId);

            _storage.Verify(storage => storage.DeleteEmployee(employeeId));
        }
    }
}