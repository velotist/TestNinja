using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        // To use DI via constructor you would have to break signature of the class,
        // => namely to be non-static
        // Here is a discrepancy: often DI frameworks do not have the possibility to inject via parameter
        // In our case we assume that the DI framework has the possibility and therefore
        // => we use the way to inject via parameter
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository repository)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = repository.GetActiveBookings(booking.Id);
            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate < b.DepartureDate
                        && b.ArrivalDate < booking.DepartureDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }
}