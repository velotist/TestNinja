using System.Linq;

namespace TestNinja.Mocking.Interfaces
{
    public interface IBookingRepository
    {
        IQueryable<Booking.Booking> GetActiveBookings(int? excludedBookingId = null);
    }
}