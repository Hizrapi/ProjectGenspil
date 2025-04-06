using GenSpil.Type;

namespace GenSpil.Model
{
    public class User
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; } // Enum


        public User(string name, string password, Role role)
        {
            Name = name;
            Password = password;
            Role = role;

        }


    }
}

