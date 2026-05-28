  SkyFlow - Airport & Airline Management System

A console-based airport management system developed in C# with SQL Server database. The system demonstrates advanced Object-Oriented Programming principles and persistent data management.

  Project Overview

SkyFlow is a terminal-based application that allows airport staff to manage flights, handle passenger check-ins, and oversee daily airport operations. The system supports two user roles: **Administrator** and **Gate Agent**, each with specific dashboards and permissions.

  Features
  Authentication
- Secure login system checking credentials against SQL Server database
- Role-based access control (Administrator and Gate Agent)

 Administrator Functions
- **Manage Flights**: Add, view, update, and delete flight schedules
- **System Overview**: View master table of all flights with occupancy status
- **Staff Management**: Add and remove gate agent staff members

  Gate Agent Functions
- **Flight Manifest**: View all registered passengers for a selected flight
- **Passenger Check-in**: Search passengers by ID or passport number and update status
- **Boarding Gate**: Finalize flights with validation (prevents boarding if flight is full or already departed)

Data Visualization
- Clean tabular display format with ASCII borders (|, -, +)
- Aligned columns with clear headers
- Professional console-based user interface

 
 Object-Oriented Programming Principles Demonstrated

 Inheritance
- Base `User` class with derived `Admin` and `GateAgent` classes
- Shared properties and methods inherited by both roles

 Encapsulation
- Private fields with public properties (getters/setters)
- Flight status only changeable via dedicated methods, not direct access

Polymorphism
- Abstract `DisplayDashboard()` method in `User` class
- Different dashboard menus shown based on user role at runtime

 Abstraction
- `IDataRepository<T>` interface separating database logic from business logic
- Clean separation of concerns

Database Schema

### Entity Relationships
