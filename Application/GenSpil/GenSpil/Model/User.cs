using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenSpil.Type;

namespace GenSpil.Model
{
    public class User
    {
        private string username { get; set; }
        private string password { get; set; }
        private Role Role { get; set; } // Enum

        public string Username => username;
        public string Password => password;

        public User (string username, string password, Role role)
        {
            Name = username;
            Password = password;
            Role = role;

        }

    }

}

