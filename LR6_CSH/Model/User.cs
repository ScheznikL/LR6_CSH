using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR6_CSH_Client.Model
{
    public class User
    {
        public string Password { get; set; }
        public string Login { get; set; }
        public string Message { get; set; } = "";
        public string Resiver { get; set; } = "None";

        public User() { }

        public User(string password, string login)
        {
            Password = password;
            Login = login;
        }
    }
}
