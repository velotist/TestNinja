using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    public class ProductTests
    {
        [Test]
        public void GetPrice_IsGoldCustomer_ReturnPrice()
        {
            var product = new Product
            {
                ListPrice = 10
            };
            var customer = new Customer
            {
                IsGold = true
            };

            var price = product.GetPrice(customer);

            Assert.That(price, Is.EqualTo(7));
        }
    }
}
