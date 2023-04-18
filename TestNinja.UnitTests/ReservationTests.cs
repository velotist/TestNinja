using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
        {
            var reservation = new Reservation();

            var result = reservation.CanBeCancelledBy(new User
            {
                IsAdmin = true
            });

            Assert.IsTrue(result);
        }
        [TestMethod]
        public void CanBeCancelledBy_UserIsMadeBy_ReturnsTrue()
        {
        }
        [TestMethod]
        public void CanBeCancelledBy_AnotherUser_ReturnsFalse()
        {
        }
    }
}
