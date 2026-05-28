using System;

namespace SkyFlow.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int FlightID { get; set; }
        public int PassengerID { get; set; }
        public string SeatNumber { get; set; } = "";
        public string Status { get; set; } = "Confirmed";
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public string PassengerName { get; set; } = "";
        public string FlightNumber { get; set; } = "";
    }
}