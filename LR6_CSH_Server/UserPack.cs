using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR6_CSH_Server
{
    class UserPack
    {
        public string Password { get; set; }
        public string Login { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Messages { get; set; } = new List<string>();
        public string Resiver { get; set; }

        public UserPack() { }

        public UserPack(string password, string login, List<string> messages, string resiver = "None")
        {
            Password = password;
            Login = login;
            Messages = messages;
            Resiver = resiver;
            Message = string.Empty;
        }
    }
}
