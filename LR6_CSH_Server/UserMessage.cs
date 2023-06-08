using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR6_CSH_Server
{
    class UserMessage
    {
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
            AdaptToUserOnServer();
        }
        private void AdaptToUserOnServer()
        {
            foreach (var user in UserOnServer.UsersOnServer)
            {
                if (user.Login == RecieverLogin)
                {
                    user.Messages.Add(Message);
                }
            }
        }
        public static bool FindMessages(string usertoken)
        {
            var userWithMessages = UserOnServer.UsersOnServer.Where(x => x.Token == usertoken && x.Messages.Count > 0).FirstOrDefault();
            if (UserOnServer.UsersOnServer.Any(x => x.Token == usertoken && x.Messages.Count > 0))
            {
                PreparePack(usertoken, userWithMessages.Messages);
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void PreparePack(string usertoken, List<string> messages)
        {
            if(!UserOnServer.UsersPack.Where(x => $"{x.Login}:{x.Password}" == usertoken).FirstOrDefault().Messages.Equals(messages))
            {
                UserOnServer.UsersPack.Where(x => $"{x.Login}:{x.Password}" == usertoken).FirstOrDefault().Messages.AddRange(messages);
            }
        }
        public static void ClearePotentiallyReadMes(string usertoken)
        {
            var userWithMessages = UserOnServer.UsersOnServer.Where(x => x.Token == usertoken && x.Messages.Count > 0).FirstOrDefault();
            int index = UserOnServer.UsersOnServer.IndexOf(userWithMessages);//TODO 
            UserOnServer.UsersOnServer[UserOnServer.UsersOnServer.IndexOf(userWithMessages)].Messages.Clear();
        }
    }
}
