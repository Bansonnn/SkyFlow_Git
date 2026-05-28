CREATE DATABASE SkyFlowDB;
GO

USE SkyFlowDB;
GO

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Flights (
    FlightID INT PRIMARY KEY IDENTITY(1,1),
    FlightNumber NVARCHAR(10) UNIQUE NOT NULL,
    Origin NVARCHAR(50) NOT NULL,
    Destination NVARCHAR(50) NOT NULL,
    DepartureTime DATETIME NOT NULL,
    AircraftCapacity INT NOT NULL,
    AvailableSeats INT NOT NULL,
    Status NVARCHAR(20) DEFAULT 'Scheduled'
);

CREATE TABLE Passengers (
    PassengerID INT PRIMARY KEY IDENTITY(1,1),
    PassportNumber NVARCHAR(20) UNIQUE NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(15)
);

CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    FlightID INT,
    PassengerID INT,
    SeatNumber NVARCHAR(5) NOT NULL,
    Status NVARCHAR(20) DEFAULT 'Confirmed',
    BookingDate DATETIME DEFAULT GETDATE()
);

INSERT INTO Users (Username, PasswordHash, FullName, Role) VALUES 
('admin', 'admin123', 'System Administrator', 'Admin'),
('agent1', 'agent123', 'John Smith', 'GateAgent'),
('agent2', 'agent123', 'Sarah Johnson', 'GateAgent');

INSERT INTO Flights (FlightNumber, Origin, Destination, DepartureTime, AircraftCapacity, AvailableSeats, Status) VALUES 
('SF102', 'CPT', 'JHB', '2026-05-28 08:30:00', 150, 150, 'Scheduled'),
('SF221', 'DBN', 'CPT', '2026-05-28 10:15:00', 120, 120, 'Scheduled'),
('SF305', 'JHB', 'PLZ', '2026-05-28 13:45:00', 100, 100, 'Boarding');

INSERT INTO Passengers (PassportNumber, FullName, Email, PhoneNumber) VALUES 
('PASSPORT12345', 'Neroshen Govender', 'neroshen@example.com', '0712345678'),
('PASSPORT67890', 'Thabo Mbeki', 'thabo@example.com', '0823456789'),
('PASSPORT11111', 'Jane Doe', 'jane@example.com', '0734567890');

INSERT INTO Bookings (FlightID, PassengerID, SeatNumber, Status) VALUES 
(1, 1, '12A', 'CheckedIn'),
(1, 2, '14B', 'Confirmed'),
(2, 3, '05C', 'Confirmed');