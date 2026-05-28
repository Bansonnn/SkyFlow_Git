using System;
using Microsoft.Data.SqlClient;

namespace SkyFlow.Database
{
    public static class DatabaseConnection
    {
        // For LocalDB (comes with Visual Studio)
        private static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;";

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("✓ Database connected!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Database error: {ex.Message}");
                return false;
            }
        }
    }
}