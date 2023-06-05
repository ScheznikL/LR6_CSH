using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR6_CSH_Client.Model
{
    public class Messages
    {
        public string SenderLogin { get; set; }
        public string RecieverLogin { get; set; }
        //public List<string> Message { get; set; } = new List<string>();
        public string Message { get; set; }

        public Messages(string senderLogin, string recieverLogin, string messages)
        {
            SenderLogin = senderLogin;
            RecieverLogin = recieverLogin;
            Message = messages;
        }
    }
}
