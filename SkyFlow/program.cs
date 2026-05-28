using System;
using System.Collections.Generic;
using System.Linq;
using SkyFlow.Models;
using SkyFlow.Database;
using SkyFlow.Services;

namespace SkyFlow
{
    class Program
    {
        static UserRepository userRepo = new UserRepository();
        static FlightRepository flightRepo = new FlightRepository();
        static BookingRepository bookingRepo = new BookingRepository();
        static PassengerRepository passengerRepo = new PassengerRepository();
        static User currentUser = null;

        static void Main(string[] args)
        {
            Console.Title = "SkyFlow Airport Management System";
            Console.ForegroundColor = ConsoleColor.Cyan;

            while (true)
            {
                ShowLoginScreen();
                if (currentUser == null) continue;

                currentUser.ShowWelcomeMessage();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();

                bool running = true;
                while (running)
                {
                    currentUser.DisplayDashboard();
                    string input = Console.ReadLine();

                    if (currentUser is Admin)
                        running = HandleAdminMenu(input);
                    else if (currentUser is GateAgent)
                        running = HandleGateAgentMenu(input);
                }
            }
        }

        static void ShowLoginScreen()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("       SKYFLOW AIRPORT SYSTEM           ");
            Console.WriteLine("            LOGIN PORTAL                ");
            Console.WriteLine("========================================");
            Console.WriteLine("  Demo Credentials:                     ");
            Console.WriteLine("  Admin:   admin / admin123             ");
            Console.WriteLine("  Agent:   agent1 / agent123            ");
            Console.WriteLine("========================================\n");

            Console.Write("  Username: ");
            string username = Console.ReadLine();
            Console.Write("  Password: ");
            string password = ReadPassword();

            currentUser = userRepo.Authenticate(username, password);

            if (currentUser == null)
            {
                Console.WriteLine("\n Invalid username or password!");
                Console.WriteLine("\nPress any key to try again...");
                Console.ReadKey();
            }
        }

        static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }

        static bool HandleAdminMenu(string option)
        {
            switch (option)
            {
                case "1":
                    ManageFlights();
                    break;
                case "2":
                    ViewSystemOverview();
                    break;
                case "3":
                    ManageStaff();
                    break;
                case "4":
                    Console.WriteLine("\nLogging out...");
                    currentUser = null;
                    return false;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
            return true;
        }

        static void ManageFlights()
        {
            while (true)
            {
                Console.Clear();
                var flights = flightRepo.GetAll().ToList();
                RenderFlightTable(flights);

                Console.WriteLine("\n========================================");
                Console.WriteLine("  Flight Management                     ");
                Console.WriteLine("========================================");
                Console.WriteLine("  1. Add New Flight                     ");
                Console.WriteLine("  2. Update Flight Status               ");
                Console.WriteLine("  3. Delete Flight                      ");
                Console.WriteLine("  4. Back to Main Menu                  ");
                Console.WriteLine("========================================");
                Console.Write("\nSelect option: ");

                string choice = Console.ReadLine();
                if (choice == "4") break;

                switch (choice)
                {
                    case "1":
                        AddNewFlight();
                        break;
                    case "2":
                        UpdateFlightStatus();
                        break;
                    case "3":
                        DeleteFlight();
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void RenderFlightTable(List<Flight> flights)
        {
            Console.Clear();
            Console.WriteLine("\n========================================");
            Console.WriteLine("         SKYFLOW FLIGHT SCHEDULE          ");
            Console.WriteLine("========================================\n");

            Console.WriteLine($"{"Flight #",-10} {"Origin",-12} {"Destination",-12} {"Departure Time",-20} {"Capacity",-10} {"Available",-10} {"Status",-15}");
            Console.WriteLine(new string('-', 90));

            foreach (var flight in flights)
            {
                string departureTime = flight.DepartureTime.ToString("yyyy-MM-dd HH:mm");
                Console.WriteLine($"{flight.FlightNumber,-10} {flight.Origin,-12} {flight.Destination,-12} {departureTime,-20} {flight.AircraftCapacity,-10} {flight.AvailableSeats,-10} {flight.Status,-15}");
            }

            Console.WriteLine("\n========================================");
        }

        static void AddNewFlight()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("         ADD NEW FLIGHT                 ");
            Console.WriteLine("========================================\n");

            try
            {
                Flight newFlight = new Flight();

                Console.Write("Flight Number (e.g., SF999): ");
                newFlight.FlightNumber = Console.ReadLine()?.ToUpper();

                Console.Write("Origin (e.g., JHB, CPT, DBN): ");
                newFlight.Origin = Console.ReadLine()?.ToUpper();

                Console.Write("Destination: ");
                newFlight.Destination = Console.ReadLine()?.ToUpper();

                Console.Write("Departure Date (yyyy-mm-dd): ");
                string date = Console.ReadLine();
                Console.Write("Departure Time (HH:MM): ");
                string time = Console.ReadLine();
                newFlight.DepartureTime = DateTime.Parse($"{date} {time}");

                Console.Write("Aircraft Capacity: ");
                newFlight.AircraftCapacity = int.Parse(Console.ReadLine());
                newFlight.AvailableSeats = newFlight.AircraftCapacity;
                newFlight.Status = "Scheduled";

                flightRepo.Add(newFlight);
                Console.WriteLine("\n Flight added successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void UpdateFlightStatus()
        {
            var flights = flightRepo.GetAll().ToList();
            RenderFlightTable(flights);

            Console.Write("\nEnter Flight Number to update: ");
            string flightNum = Console.ReadLine()?.ToUpper();

            var flight = flights.FirstOrDefault(f => f.FlightNumber == flightNum);
            if (flight == null)
            {
                Console.WriteLine(" Flight not found!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nSelect new status:");
            Console.WriteLine("1. Scheduled");
            Console.WriteLine("2. Boarding");
            Console.WriteLine("3. Departed");
            Console.Write("Choice: ");

            string status = Console.ReadLine() switch
            {
                "1" => "Scheduled",
                "2" => "Boarding",
                "3" => "Departed",
                _ => flight.Status
            };

            flight.Status = status;
            flightRepo.Update(flight);
            Console.WriteLine($"\n Flight {flight.FlightNumber} status updated to {status}");
            Console.ReadKey();
        }

        static void DeleteFlight()
        {
            var flights = flightRepo.GetAll().ToList();
            RenderFlightTable(flights);

            Console.Write("\nEnter Flight Number to delete: ");
            string flightNum = Console.ReadLine()?.ToUpper();

            var flight = flights.FirstOrDefault(f => f.FlightNumber == flightNum);
            if (flight == null)
            {
                Console.WriteLine(" Flight not found!");
                Console.ReadKey();
                return;
            }

            Console.Write($"Are you sure you want to delete flight {flightNum}? (Y/N): ");
            if (Console.ReadLine()?.ToUpper() == "Y")
            {
                flightRepo.Delete(flight.FlightID);
                Console.WriteLine(" Flight deleted!");
            }
            Console.ReadKey();
        }

        static void ViewSystemOverview()
        {
            Console.Clear();
            var flights = flightRepo.GetAll().ToList();
            var bookings = bookingRepo.GetAll().ToList();

            Console.WriteLine("========================================");
            Console.WriteLine("              SYSTEM OVERVIEW           ");
            Console.WriteLine("========================================\n");

            Console.WriteLine($"Total Flights: {flights.Count}");
            Console.WriteLine($"Total Bookings: {bookings.Count()}");
            Console.WriteLine($"\nFlights by Status:");
            Console.WriteLine($"  - Scheduled: {flights.Count(f => f.Status == "Scheduled")}");
            Console.WriteLine($"  - Boarding:  {flights.Count(f => f.Status == "Boarding")}");
            Console.WriteLine($"  - Departed:  {flights.Count(f => f.Status == "Departed")}");

            int totalCapacity = flights.Sum(f => f.AircraftCapacity);
            int totalBooked = flights.Sum(f => f.AircraftCapacity - f.AvailableSeats);
            double occupancyRate = totalCapacity > 0 ? (double)totalBooked / totalCapacity * 100 : 0;

            Console.WriteLine($"\nOverall Occupancy Rate: {occupancyRate:F1}%");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void ManageStaff()
        {
            while (true)
            {
                Console.Clear();
                var users = userRepo.GetAll().ToList();
                
                Console.WriteLine("\n========================================");
                Console.WriteLine("              STAFF DIRECTORY           ");
                Console.WriteLine("========================================\n");
                Console.WriteLine($"{"ID",-5} {"Username",-15} {"Full Name",-25} {"Role",-12}");
                Console.WriteLine(new string('-', 60));
                
                foreach (var u in users)
                {
                    Console.WriteLine($"{u.UserID,-5} {u.Username,-15} {u.FullName,-25} {u.Role,-12}");
                }

                Console.WriteLine("\n========================================");
                Console.WriteLine("  Staff Management                      ");
                Console.WriteLine("========================================");
                Console.WriteLine("  1. Add New Staff Member               ");
                Console.WriteLine("  2. Back to Main Menu                  ");
                Console.WriteLine("========================================");
                Console.Write("\nSelect option: ");

                string choice = Console.ReadLine();
                if (choice == "2") break;

                if (choice == "1")
                {
                    Console.Clear();
                    Console.WriteLine("========================================");
                    Console.WriteLine("         ADD NEW STAFF MEMBER           ");
                    Console.WriteLine("========================================\n");

                    Console.Write("Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Full Name: ");
                    string fullname = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();
                    Console.Write("Role (Admin/GateAgent): ");
                    string role = Console.ReadLine();

                    User newUser = role.ToLower() == "admin" ? new Admin() : new GateAgent();
                    newUser.Username = username;
                    newUser.FullName = fullname;
                    newUser.PasswordHash = password;
                    newUser.Role = role;

                    userRepo.Add(newUser);
                    Console.WriteLine("\n Staff member added!");
                    Console.ReadKey();
                }
            }
        }

        static bool HandleGateAgentMenu(string option)
        {
            switch (option)
            {
                case "1":
                    ViewFlightManifest();
                    break;
                case "2":
                    PassengerCheckIn();
                    break;
                case "3":
                    BoardingGate();
                    break;
                case "4":
                    Console.WriteLine("\nLogging out...");
                    currentUser = null;
                    return false;
                default:
                    Console.WriteLine("Invalid option.");
                    Console.ReadKey();
                    break;
            }
            return true;
        }

        static void ViewFlightManifest()
        {
            Console.Clear();
            var flights = flightRepo.GetAll().ToList();
            RenderFlightTable(flights);

            Console.Write("\nEnter Flight Number to view manifest: ");
            string flightNum = Console.ReadLine()?.ToUpper();

            var flight = flights.FirstOrDefault(f => f.FlightNumber == flightNum);
            if (flight == null)
            {
                Console.WriteLine(" Flight not found!");
                Console.ReadKey();
                return;
            }

            var bookings = bookingRepo.GetBookingsByFlight(flight.FlightID).ToList();

            if (!bookings.Any())
            {
                Console.WriteLine($"\nNo passengers booked on flight {flightNum}");
            }
            else
            {
                Console.WriteLine($"\n========== FLIGHT MANIFEST - {flightNum} ({flight.Origin} to {flight.Destination}) ==========\n");
                Console.WriteLine($"{"Booking ID",-12} {"Passenger Name",-25} {"Seat",-8} {"Status",-12}");
                Console.WriteLine(new string('-', 60));
                
                foreach (var b in bookings)
                {
                    Console.WriteLine($"{b.BookingID,-12} {b.PassengerName,-25} {b.SeatNumber,-8} {b.Status,-12}");
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void PassengerCheckIn()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("         PASSENGER CHECK-IN             ");
            Console.WriteLine("========================================\n");

            Console.Write("Enter Passenger ID or Passport Number: ");
            string search = Console.ReadLine();

            Passenger passenger = null;

            if (int.TryParse(search, out int id))
            {
                passenger = passengerRepo.GetById(id);
            }
            else
            {
                passenger = passengerRepo.GetByPassport(search);
            }

            if (passenger == null)
            {
                Console.WriteLine(" Passenger not found!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\n Passenger found: {passenger.FullName}");
            Console.WriteLine($"  Passport: {passenger.PassportNumber}");
            Console.WriteLine($"  Email: {passenger.Email}");
            Console.WriteLine($"  Phone: {passenger.PhoneNumber}");

            var allBookings = bookingRepo.GetAll().Where(b => b.PassengerID == passenger.PassengerID).ToList();

            if (!allBookings.Any())
            {
                Console.WriteLine("\nNo bookings found for this passenger.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\n========== PASSENGER BOOKINGS ==========\n");
            Console.WriteLine($"{"Booking ID",-12} {"Flight",-10} {"Seat",-8} {"Status",-12}");
            Console.WriteLine(new string('-', 45));
            
            foreach (var b in allBookings)
            {
                Console.WriteLine($"{b.BookingID,-12} {b.FlightNumber,-10} {b.SeatNumber,-8} {b.Status,-12}");
            }

            Console.Write("\nEnter Booking ID to check in: ");
            if (int.TryParse(Console.ReadLine(), out int bookingId))
            {
                var booking = allBookings.FirstOrDefault(b => b.BookingID == bookingId);
                if (booking != null && booking.Status != "CheckedIn" && booking.Status != "Boarded")
                {
                    bookingRepo.UpdateBookingStatus(bookingId, "CheckedIn");
                    Console.WriteLine($"\n Passenger {passenger.FullName} checked in successfully!");
                    Console.WriteLine($"  Seat: {booking.SeatNumber}");

                    var flight = flightRepo.GetById(booking.FlightID);
                    if (flight != null && flight.AvailableSeats > 0)
                    {
                        flight.AvailableSeats--;
                        flightRepo.Update(flight);
                    }
                }
                else if (booking != null && (booking.Status == "CheckedIn" || booking.Status == "Boarded"))
                {
                    Console.WriteLine($"\n Passenger already {booking.Status}!");
                }
                else
                {
                    Console.WriteLine("\n Invalid booking!");
                }
            }

            Console.ReadKey();
        }

        static void BoardingGate()
        {
            Console.Clear();
            var flights = flightRepo.GetAll().Where(f => f.Status == "Boarding" || f.Status == "Scheduled").ToList();

            if (!flights.Any())
            {
                Console.WriteLine("No flights available for boarding.");
                Console.ReadKey();
                return;
            }

            RenderFlightTable(flights);

            Console.Write("\nEnter Flight Number for boarding: ");
            string flightNum = Console.ReadLine()?.ToUpper();

            var flight = flights.FirstOrDefault(f => f.FlightNumber == flightNum);
            if (flight == null)
            {
                Console.WriteLine(" Flight not found!");
                Console.ReadKey();
                return;
            }

            if (flight.Status != "Boarding")
            {
                Console.Write($"\nFlight {flightNum} is not in boarding status. Start boarding? (Y/N): ");
                if (Console.ReadLine()?.ToUpper() == "Y")
                {
                    flight.Status = "Boarding";
                    flightRepo.Update(flight);
                    Console.WriteLine($"\n Boarding started for flight {flightNum}");
                }
                else
                {
                    return;
                }
            }

            var checkedInPassengers = bookingRepo.GetBookingsByFlight(flight.FlightID)
                .Where(b => b.Status == "CheckedIn").ToList();

            if (!checkedInPassengers.Any())
            {
                Console.WriteLine("\nNo checked-in passengers for this flight.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nFound {checkedInPassengers.Count} checked-in passenger(s).\n");
            Console.WriteLine("========== READY FOR BOARDING ==========\n");
            Console.WriteLine($"{"Passenger Name",-25} {"Seat",-8} {"Status",-12}");
            Console.WriteLine(new string('-', 50));
            
            foreach (var p in checkedInPassengers)
            {
                Console.WriteLine($"{p.PassengerName,-25} {p.SeatNumber,-8} {p.Status,-12}");
            }

            Console.Write("\nBoard all checked-in passengers? (Y/N): ");
            if (Console.ReadLine()?.ToUpper() == "Y")
            {
                foreach (var passenger in checkedInPassengers)
                {
                    bookingRepo.UpdateBookingStatus(passenger.BookingID, "Boarded");
                    Console.WriteLine($"  Boarded: {passenger.PassengerName} (Seat {passenger.SeatNumber})");
                }

                flight.Status = "Departed";
                flightRepo.Update(flight);

                Console.WriteLine($"\n All passengers boarded! Flight {flightNum} has departed.");
            }

            Console.ReadKey();
        }
    }
}