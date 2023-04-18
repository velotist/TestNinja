using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            const int id = 0;
            var controller = new CustomerController();

            var result = controller.GetCustomer(id);

            // Assert that object is of type NotFound or of one of its derivatives,
            // in this case also of type ActionResult
            Assert.That(result, Is.InstanceOf<NotFound>());

            // Assert that object is of type NotFound
            Assert.That(result, Is.TypeOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            const int id = 10;
            var controller = new CustomerController();

            var result = controller.GetCustomer(id);

            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
