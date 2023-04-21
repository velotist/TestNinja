using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class BookingHelper_OverlappingBookingExistTests
    {
        [Test]
        public void BookingStartsAndFinishesBeforeExistingBooking__ReturnEmptyString()
        {
            var repository = new Mock<IBookingRepository>();
            repository.Setup(repo => repo
                    .GetActiveBookings(1))
                .Returns(new List<Booking>
            {
                new Booking
                {
                    Id = 2,
                    ArrivalDate = new DateTime(2020, 1, 15, 14, 0, 0),
                    DepartureDate = new DateTime(2020,1,20, 10,0 , 0),
                    Reference = "a"
                }
            }
                    .AsQueryable());

            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = new DateTime(2020, 1, 10, 14, 0, 0),
                DepartureDate = new DateTime(2020, 1, 14, 10, 0, 0)
            },
                repository.Object);

            Assert.That(result, Is.Empty);
        }
    }
}
