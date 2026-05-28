using System;
using System.Collections.Generic;
using System.Linq;
using SkyFlow.Models;

namespace SkyFlow.Database
{
    public class BookingRepository
    {
        private static List<Booking> bookings = new List<Booking>();

        public BookingRepository()
        {
            if (bookings.Count == 0)
            {
                bookings.Add(new Booking
                {
                    BookingID = 1,
                    FlightID = 1,
                    PassengerID = 1,
                    SeatNumber = "12A",
                    Status = "CheckedIn",
                    BookingDate = DateTime.Now.AddDays(-1),
                    PassengerName = "Neroshen Govender",
                    FlightNumber = "SF102"
                });
                bookings.Add(new Booking
                {
                    BookingID = 2,
                    FlightID = 1,
                    PassengerID = 2,
                    SeatNumber = "14B",
                    Status = "Confirmed",
                    BookingDate = DateTime.Now.AddDays(-1),
                    PassengerName = "Thabo Mbeki",
                    FlightNumber = "SF102"
                });
                bookings.Add(new Booking
                {
                    BookingID = 3,
                    FlightID = 2,
                    PassengerID = 3,
                    SeatNumber = "05C",
                    Status = "Confirmed",
                    BookingDate = DateTime.Now.AddDays(-1),
                    PassengerName = "Jane Doe",
                    FlightNumber = "SF221"
                });
            }
        }

        public List<Booking> GetAll()
        {
            return bookings;
        }

        public Booking GetById(int id)
        {
            return bookings.FirstOrDefault(b => b.BookingID == id);
        }

        public List<Booking> GetBookingsByFlight(int flightId)
        {
            return bookings.Where(b => b.FlightID == flightId).ToList();
        }

        public void UpdateBookingStatus(int bookingId, string status)
        {
            var booking = bookings.FirstOrDefault(b => b.BookingID == bookingId);
            if (booking != null)
            {
                booking.Status = status;
            }
        }

        public void Add(Booking booking)
        {
            booking.BookingID = bookings.Count + 1;
            bookings.Add(booking);
        }

        public void Update(Booking booking)
        {
            var existing = bookings.FirstOrDefault(b => b.BookingID == booking.BookingID);
            if (existing != null)
            {
                existing.Status = booking.Status;
            }
        }

        public void Delete(int id)
        {
            var booking = bookings.FirstOrDefault(b => b.BookingID == id);
            if (booking != null)
                bookings.Remove(booking);
        }
    }
}