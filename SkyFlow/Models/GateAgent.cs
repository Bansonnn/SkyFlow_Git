using System;

namespace SkyFlow.Models
{
    public class GateAgent : User
    {
        public override void DisplayDashboard()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║       GATE AGENT DASHBOARD             ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine("║  1. View Flight Manifest               ║");
            Console.WriteLine("║  2. Passenger Check-in                 ║");
            Console.WriteLine("║  3. Boarding Gate                      ║");
            Console.WriteLine("║  4. Logout                             ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.Write("\nSelect option: ");
        }
    }
}