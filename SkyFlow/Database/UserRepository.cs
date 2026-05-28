using System.Collections.Generic;
using System.Linq;
using SkyFlow.Models;

namespace SkyFlow.Database
{
    public class UserRepository
    {
        private static List<User> users = new List<User>();

        public UserRepository()
        {
            if (users.Count == 0)
            {
                Admin admin = new Admin();
                admin.UserID = 1;
                admin.Username = "admin";
                admin.PasswordHash = "admin123";
                admin.FullName = "System Administrator";
                admin.Role = "Admin";
                users.Add(admin);

                GateAgent agent1 = new GateAgent();
                agent1.UserID = 2;
                agent1.Username = "agent1";
                agent1.PasswordHash = "agent123";
                agent1.FullName = "John Smith";
                agent1.Role = "GateAgent";
                users.Add(agent1);

                GateAgent agent2 = new GateAgent();
                agent2.UserID = 3;
                agent2.Username = "agent2";
                agent2.PasswordHash = "agent123";
                agent2.FullName = "Sarah Johnson";
                agent2.Role = "GateAgent";
                users.Add(agent2);
            }
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User GetById(int id)
        {
            return users.FirstOrDefault(u => u.UserID == id);
        }

        public User Authenticate(string username, string password)
        {
            return users.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
        }

        public void Add(User user)
        {
            user.UserID = users.Count + 1;
            users.Add(user);
        }

        public void Update(User user)
        {
            var existing = users.FirstOrDefault(u => u.UserID == user.UserID);
            if (existing != null)
            {
                existing.Username = user.Username;
                existing.FullName = user.FullName;
                existing.Role = user.Role;
            }
        }

        public void Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.UserID == id);
            if (user != null)
            {
                users.Remove(user);
            }
        }
    }
}