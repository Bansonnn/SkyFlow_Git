using System;
using System.Collections.Generic;
using System.Linq;
using SkyFlow.Models;

namespace SkyFlow.Services
{
    public static class TableRenderer
    {
        public static void RenderFlightTable(List<Flight> flights)
        {
            if (flights == null || flights.Count == 0)
            {
                Console.WriteLine("+========================================+");
                Console.WriteLine("|          No flights available           |");
                Console.WriteLine("+========================================+");
                return;
            }

            Console.Clear();
            Console.WriteLine("+----------+-------------+-------------+---------------------+----------+-----------+-----------------+");
            Console.WriteLine("| Flight # | Origin      | Destination | Departure Time      | Capacity | Available | Status          |");
            Console.WriteLine("+----------+-------------+-------------+---------------------+----------+-----------+-----------------+");

            foreach (var flight in flights)
            {
                string departureTime = flight.DepartureTime.ToString("yyyy-MM-dd HH:mm");
                Console.WriteLine($"| {flight.FlightNumber,-8} | {flight.Origin,-11} | {flight.Destination,-11} | {departureTime,-19} | {flight.AircraftCapacity,-8} | {flight.AvailableSeats,-9} | {flight.Status,-15} |");
            }

            Console.WriteLine("+----------+-------------+-------------+---------------------+----------+-----------+-----------------+");
        }

        public static void RenderPassengerTable(List<Passenger> passengers)
        {
            if (passengers == null || passengers.Count == 0)
            {
                Console.WriteLine("+========================================+");
                Console.WriteLine("|        No passengers available          |");
                Console.WriteLine("+========================================+");
                return;
            }

            Console.WriteLine("+------------+--------------------------+--------------------------+-----------------+");
            Console.WriteLine("| Passenger #| Full Name                | Email                    | Phone           |");
            Console.WriteLine("+------------+--------------------------+--------------------------+-----------------+");

            foreach (var p in passengers)
            {
                Console.WriteLine($"| {p.PassengerID,-10} | {p.FullName,-24} | {p.Email,-24} | {p.PhoneNumber,-15} |");
            }

            Console.WriteLine("+------------+--------------------------+--------------------------+-----------------+");
        }

        public static void RenderBookingTable(List<Booking> bookings, string flightNumber)
        {
            if (bookings == null || bookings.Count == 0)
            {
                Console.WriteLine("+================================================+");
                Console.WriteLine("|   No passengers booked on flight " + flightNumber.PadRight(10) + "   |");
                Console.WriteLine("+================================================+");
                return;
            }

            Console.WriteLine("+----------+--------------------------+------+-------------+");
            Console.WriteLine("| Booking #| Passenger Name           | Seat | Status      |");
            Console.WriteLine("+----------+--------------------------+------+-------------+");

            foreach (var b in bookings)
            {
                string name = b.PassengerName.Length > 24 ? b.PassengerName.Substring(0, 21) + "..." : b.PassengerName;
                Console.WriteLine($"| {b.BookingID,-8} | {name,-24} | {b.SeatNumber,-4} | {b.Status,-11} |");
            }

            Console.WriteLine("+----------+--------------------------+------+-------------+");
        }

        public static void RenderStaffTable(List<User> staff)
        {
            if (staff == null || staff.Count == 0)
            {
                Console.WriteLine("+========================================+");
                Console.WriteLine("|           No staff members              |");
                Console.WriteLine("+========================================+");
                return;
            }

            Console.WriteLine("+----+------------------+--------------------------------+------------+");
            Console.WriteLine("| ID | Username         | Full Name                      | Role       |");
            Console.WriteLine("+----+------------------+--------------------------------+------------+");

            foreach (var s in staff)
            {
                string fullName = s.FullName.Length > 30 ? s.FullName.Substring(0, 27) + "..." : s.FullName;
                Console.WriteLine($"| {s.UserID,-2} | {s.Username,-16} | {fullName,-30} | {s.Role,-10} |");
            }

            Console.WriteLine("+----+------------------+--------------------------------+------------+");
        }

        public static void RenderSystemOverview(List<Flight> flights)
        {
            Console.Clear();
            Console.WriteLine("+============================================================+");
            Console.WriteLine("|                     SYSTEM OVERVIEW                         |");
            Console.WriteLine("+============================================================+");

            int totalFlights = flights.Count;
            int scheduled = flights.Count(f => f.Status == "Scheduled");
            int boarding = flights.Count(f => f.Status == "Boarding");
            int departed = flights.Count(f => f.Status == "Departed");
            int totalCapacity = flights.Sum(f => f.AircraftCapacity);
            int totalBooked = flights.Sum(f => f.AircraftCapacity - f.AvailableSeats);
            double occupancyRate = totalCapacity > 0 ? (double)totalBooked / totalCapacity * 100 : 0;

            Console.WriteLine("| Total Flights: " + totalFlights.ToString().PadRight(56) + " |");
            Console.WriteLine("| Scheduled: " + scheduled.ToString().PadRight(61) + " |");
            Console.WriteLine("| Boarding: " + boarding.ToString().PadRight(61) + " |");
            Console.WriteLine("| Departed: " + departed.ToString().PadRight(61) + " |");
            Console.WriteLine("| Total Capacity: " + totalCapacity.ToString().PadRight(54) + " |");
            Console.WriteLine("| Total Booked: " + totalBooked.ToString().PadRight(56) + " |");
            Console.WriteLine("| Occupancy Rate: " + string.Format("{0:F1}", occupancyRate) + "% (" + totalBooked + "/" + totalCapacity + ")" + "".PadRight(35) + " |");
            Console.WriteLine("+============================================================+");
        }

        public static void RenderMessage(string title, string message, bool isSuccess)
        {
            string status = isSuccess ? "SUCCESS" : "ERROR";
            
            Console.WriteLine("+================================================+");
            Console.WriteLine("| " + status.PadRight(46) + " |");
            Console.WriteLine("+================================================+");
            Console.WriteLine("| " + message.PadRight(46) + " |");
            Console.WriteLine("+================================================+");
        }

        public static void RenderMenu(string title, List<string> options)
        {
            Console.Clear();
            int maxLength = title.Length;
            foreach (var opt in options)
            {
                if (opt.Length > maxLength) maxLength = opt.Length;
            }
            
            int width = maxLength + 10;
            string border = "+" + new string('-', width) + "+";
            
            Console.WriteLine(border);
            Console.WriteLine("| " + title.PadRight(width - 2) + " |");
            Console.WriteLine(border);
            
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine("| " + (i + 1).ToString() + ". " + options[i].PadRight(width - 6) + " |");
            }
            
            Console.WriteLine(border);
            Console.Write("| Select option: ");
        }
    }
}