using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using LR6_CSH_Client.Model;


namespace LR6_CSH_Client.Controls
{
    public class UsersData
    {
        // public IEnumerable<User> Users => Userslist;

        internal List<User> Userslist { get; set; } = new List<User>();
        public static List<Messages> ReceivedMessages { get; set; } = new List<Messages>();
        public static List<Messages> UserMessages { get; set; } = new List<Messages>();
        public static object Locker = new object();

        public UsersData() { }
        internal void AddUsers(string json)
        {
            if (Userslist.Count < 0)
            {
                Userslist = JsonSerializer.Deserialize<List<User>>(json);
            }
            else
            {
                Userslist.AddRange(JsonSerializer.Deserialize<List<User>>(json));
            }

        }
        public static void AddrecievedMessages(string json)
        {
            if (UserMessages.Count == 0) UserMessages = JsonSerializer.Deserialize<List<Messages>>(json);
            else
            {
                ReceivedMessages = JsonSerializer.Deserialize<List<Messages>>(json);
                CombineMessages();
            }
        }
        public static void CombineMessages()
        {
            if (ReceivedMessages.Count > 1)
                lock (Locker)
                {
                    UserMessages.AddRange(ReceivedMessages);
                }
            else
            {
                lock (Locker)
                {
                    UserMessages.Add(ReceivedMessages[0]);
                }

            }
        }
    }
}
