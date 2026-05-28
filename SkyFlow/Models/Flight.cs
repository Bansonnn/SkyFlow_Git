using System;

namespace SkyFlow.Models
{
    public class Flight
    {
        public int FlightID { get; set; }
        public string FlightNumber { get; set; } = "";
        public string Origin { get; set; } = "";
        public string Destination { get; set; } = "";
        public DateTime DepartureTime { get; set; }
        public int AircraftCapacity { get; set; }
        public int AvailableSeats { get; set; }
        public string Status { get; set; } = "Scheduled";

        public bool DepartFlight()
        {
            if (Status == "Departed")
            {
                Console.WriteLine("✗ Flight has already departed!");
                return false;
            }
            Status = "Departed";
            Console.WriteLine("✓ Flight status updated to Departed");
            return true;
        }

        public bool StartBoarding()
        {
            if (Status == "Departed")
            {
                Console.WriteLine("✗ Cannot board - flight has already departed!");
                return false;
            }
            if (Status == "Boarding")
            {
                Console.WriteLine("⚠ Boarding is already in progress!");
                return false;
            }
            Status = "Boarding";
            Console.WriteLine("✓ Boarding started!");
            return true;
        }

        public bool IsFull() => AvailableSeats <= 0;
    }
}