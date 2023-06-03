using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR6_CSH_Server
{
    class UserMessage
    {
        public static List<UserMessage>  UserMessages { get; set; } = new List<UserMessage>();
        public string SenderLogin { get; set; }
        public string RecieverLogin { get; set; }
        public string Message { get; set; }
        public static object Locker = new object();

        public UserMessage() { }

        public UserMessage(string senderLogin, string recieverLogin, string message)
        {
            SenderLogin = senderLogin;
            RecieverLogin = recieverLogin;
            Message = message;
        }
        public static IEnumerable<UserMessage> FindMessages(UserPack user)
        {
            if (UserMessages.Count > 0)
            {
                var messagesList = UserMessages.Select(x => x).Where(y => y.RecieverLogin == user.Login);
                UserMessages.RemoveAll(y => y.RecieverLogin == user.Login);
                Console.WriteLine($"All {user.Login} messages send to be recieved");
                return messagesList;
            }
            else
            {
                return null;
            }
        }
    }
}
