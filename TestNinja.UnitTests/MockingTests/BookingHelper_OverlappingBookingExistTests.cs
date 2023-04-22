using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking.Booking;
using TestNinja.Mocking.Interfaces;

namespace TestNinja.UnitTests.MockingTests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class BookingHelper_OverlappingBookingExistTests
    {
        [SetUp]
        public void SetUp()
        {
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2020, 1, 15),
                DepartureDate = DepartOn(2020, 1, 20),
                Reference = "a"
            };

            _repository = new Mock<IBookingRepository>();
            _repository.Setup(repo => repo
                    .GetActiveBookings(1))
                .Returns(new List<Booking>
                    {
                        _existingBooking
                    }
                    .AsQueryable());
        }

        private Booking _existingBooking;
        private Mock<IBookingRepository> _repository;

        [Test]
        public void BookingStartsAndFinishesBeforeExistingBooking__ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate, 2),
                    DepartureDate = Before(_existingBooking.ArrivalDate)
                },
                _repository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInMiddleOfExistingBooking__ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.ArrivalDate)
                },
                _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInMiddleOfExistingBooking__ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = Before(_existingBooking.DepartureDate)
                },
                _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsInMiddleAndFinishesAfterExistingBooking__ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate)
                },
                _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterExistingBooking__ReturnExistingBookingReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate)
                },
                _repository.Object);

            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesAfterExistingBooking__ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.DepartureDate),
                    DepartureDate = After(_existingBooking.DepartureDate, 2)
                },
                _repository.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingIsCancelledAndOverlaps__ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = Before(_existingBooking.DepartureDate),
                    Status = "Cancelled"
                },
                _repository.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}