namespace SkyFlow.Models
{
    public class Passenger
    {
        public int PassengerID { get; set; }
        public string PassportNumber { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
    }
}