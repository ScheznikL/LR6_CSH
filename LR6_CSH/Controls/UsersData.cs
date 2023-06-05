using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static ObservableCollection<Messages> UserMessages { get; set; } = new ObservableCollection<Messages>();
        public static object Locker = new object();
        public static User CurrentUser { get; set; }

        public UsersData() { }
        internal void AddUsers(string json)
        {
            if (!string.IsNullOrEmpty(json))
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
            else
            {
                MessageBox.Show("No one else here \n|:( |");
            }
        }
        public static void AddRecievedMessages(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                var tempUsersWithMessagesList = JsonSerializer.Deserialize<List<User>>(json);
                foreach (var message in tempUsersWithMessagesList)
                {
                    if (message.Login == CurrentUser.Login)
                    {
                        foreach (var oneMes in message.Messages)
                        {
                            var sender = oneMes.Split('|');
                            lock (Locker)
                            {
                                UserMessages.Add(new Messages(sender[1], message.Login, oneMes));
                            }
                        }
                    }
                }
            }

        }
        public static void CombineMessages()
        {
            //if (ReceivedMessages.Count > 1)
            //    lock (Locker)
            //    {
            //        UserMessages.AddRange(ReceivedMessages);
            //    }
            //else
            //{
            //    lock (Locker)
            //    {
            //        UserMessages.Add(ReceivedMessages[0]);
            //    }

            //}
        }
    }
}
