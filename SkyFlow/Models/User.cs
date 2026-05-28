using System;

namespace SkyFlow.Models
{
    public abstract class User
    {
        public int UserID { get; set; }
        public string Username { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Role { get; set; } = "";

        public abstract void DisplayDashboard();

        public virtual void ShowWelcomeMessage()
        {
            Console.WriteLine($"\n╔════════════════════════════════════════╗");
            Console.WriteLine($"║  Welcome back, {FullName,-30} ║");
            Console.WriteLine($"╚════════════════════════════════════════╝");
        }
    }
}