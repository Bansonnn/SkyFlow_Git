using System.Collections.Generic;
using System.Linq;
using SkyFlow.Models;

namespace SkyFlow.Database
{
    public class PassengerRepository
    {
        private static List<Passenger> passengers = new List<Passenger>();

        public PassengerRepository()
        {
            if (passengers.Count == 0)
            {
                passengers.Add(new Passenger
                {
                    PassengerID = 1,
                    PassportNumber = "PASSPORT12345",
                    FullName = "Neroshen Govender",
                    Email = "neroshen@example.com",
                    PhoneNumber = "0712345678"
                });
                passengers.Add(new Passenger
                {
                    PassengerID = 2,
                    PassportNumber = "PASSPORT67890",
                    FullName = "Thabo Mbeki",
                    Email = "thabo@example.com",
                    PhoneNumber = "0823456789"
                });
                passengers.Add(new Passenger
                {
                    PassengerID = 3,
                    PassportNumber = "PASSPORT11111",
                    FullName = "Jane Doe",
                    Email = "jane@example.com",
                    PhoneNumber = "0734567890"
                });
            }
        }

        public List<Passenger> GetAll()
        {
            return passengers;
        }

        public Passenger GetById(int id)
        {
            return passengers.FirstOrDefault(p => p.PassengerID == id);
        }

        public Passenger GetByPassport(string passportNumber)
        {
            return passengers.FirstOrDefault(p => p.PassportNumber == passportNumber);
        }

        public void Add(Passenger passenger)
        {
            passenger.PassengerID = passengers.Count + 1;
            passengers.Add(passenger);
        }

        public void Update(Passenger passenger)
        {
            var existing = passengers.FirstOrDefault(p => p.PassengerID == passenger.PassengerID);
            if (existing != null)
            {
                existing.PassportNumber = passenger.PassportNumber;
                existing.FullName = passenger.FullName;
                existing.Email = passenger.Email;
                existing.PhoneNumber = passenger.PhoneNumber;
            }
        }

        public void Delete(int id)
        {
            var passenger = passengers.FirstOrDefault(p => p.PassengerID == id);
            if (passenger != null)
                passengers.Remove(passenger);
        }
    }
}