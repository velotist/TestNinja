using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void CanBeCancelledBy_UserIsAdmin_ReturnTrue()
        {
            var reservation = new Reservation();

            var result = reservation.CanBeCancelledBy(new User
            {
                IsAdmin = true
            });

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_UserIsNotAdmin_ReturnFalse()
        {
            var reservation = new Reservation();
            // default boolean value is false,
            // so property IsAdmin of object User is false in this scenario
            var user = new User();

            var result = reservation.CanBeCancelledBy(user);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_SameUser_ReturnTrue()
        {
            var user = new User();
            var reservation = new Reservation
            {
                MadeBy = user
            };
            
            var result = reservation.CanBeCancelledBy(user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_DifferentUser_ReturnFalse()
        {
            var user = new User();
            var reservation = new Reservation
            {
                MadeBy = user
            };
            var anotherUser = new User();

            var result = reservation.CanBeCancelledBy(anotherUser);

            Assert.IsFalse(result);
        }
    }
}
