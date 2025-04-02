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


        public User (Role role, string username, string password)
        {
            this.username = username;
            this.password = password;
            this.Role = role;

        }


    }
}

