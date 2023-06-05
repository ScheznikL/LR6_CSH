using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LR6_CSH_Server
{
    class UserOnServer
    {
        //private string _id;
        public static List<UserOnServer> UsersOnServer { get; set; } = new List<UserOnServer>();
        public static List<UserPack> UsersPack { get; set; } = new List<UserPack>();

        // private string Id { get => _id; set => Guid.NewGuid().ToString(); }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public List<string> Messages { get; set; } = new List<string>();
        private static object locker = new object();

        public UserOnServer() { }
        public UserOnServer(string password, string login, string token)
        {
            Password = password;
            Login = login;
            Token = token;
            Messages = new List<string>();
        }

        public static Task ConstructUsersOnServer(UserPack user, string token, ref CancellationTokenSource tokenSource, out CancellationToken ct)
        {
            ct = tokenSource.Token;
            var newUser = new UserOnServer(user.Password, user.Login, token);
            if (!UsersOnServer.Any(y => y.Login == user.Login))
            {
                //lock (locker)
                {
                    UsersOnServer.Add(newUser);
                    UsersPack.Add(new UserPack(newUser.Password, newUser.Login, newUser.Messages));
                }
                Console.WriteLine($"Data of user {user.Login} received successfully.");
                return Task.CompletedTask;
            }
            else
            {                
                Console.WriteLine($"User {user.Login} already exist.");
                tokenSource.Cancel();
                return Task.FromCanceled(ct);
                //throw new ArgumentException($"User {user.Login} already exist.");
            }
        }
        public static int CheckUsersPerenceAndPassword(UserPack user)
        {
            var foundUser = UsersOnServer.FirstOrDefault(x => x.Login == user.Login);
            if (foundUser != default(UserOnServer) || foundUser != null)
            {
                if(foundUser.Password == user.Password)
                {
                    return 200;
                }
                else
                {
                    Console.WriteLine($"User {foundUser.Login} entered wrong password.");
                    return 401;
                }
            }
            Console.WriteLine($"User {foundUser.Login} not found.");
            return 404;
        }
    }
}
