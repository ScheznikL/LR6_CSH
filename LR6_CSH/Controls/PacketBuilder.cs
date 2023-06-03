using System;
using LR6_CSH_Client.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace LR6_CSH_Client.Controls
{
    public class PacketBuilder
    {
        public byte[] ArrToSend { get; }
        public string Userjson { get; }
        public User UserFromPack { get; }

        public PacketBuilder(string password, string login)
        {
            UserFromPack = new User(password, login);
            Userjson = JsonSerializer.Serialize(UserFromPack);
            ArrToSend = JsonSerializer.SerializeToUtf8Bytes(Userjson);          
        }
    }
}
