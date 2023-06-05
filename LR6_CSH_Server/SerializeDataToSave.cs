using Newtonsoft.Json;
using System;
using System.IO;
//using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR6_CSH_Server
{
    class JSONOrganaizer
    {
        static System.Threading.CancellationTokenSource tokenSource = new System.Threading.CancellationTokenSource();
        static System.Threading.CancellationToken ct;

        public static void SerializeUserData(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            try
            {
                var serializer = new JsonSerializer();
                using (var file = new StreamWriter(path))
                {
                    serializer.Serialize(file, UserOnServer.UsersOnServer);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Serialization error occured: {ex}");
            }
        }

        public static bool DeserializeUsersFromFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return false;
            try
            {
                var jsonDeserializer = new JsonSerializer();
                using (var file = new StreamReader(filePath))
                {
                    UserOnServer.UsersOnServer = (List<UserOnServer>)jsonDeserializer.Deserialize(file, typeof(List<UserOnServer>));
                }
                AdaptForUserPack();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private static void AdaptForUserPack()
        {
            foreach (var user in UserOnServer.UsersOnServer)
            {
                if(user.Messages == null)
                {
                    user.Messages = new List<string>();
                }                  
                if (UserOnServer.UsersOnServer.Count != UserOnServer.UsersPack.Count)
                {
                    UserOnServer.UsersPack.Add(new UserPack(user.Password, user.Login, user.Messages));
                }
            }
            Console.WriteLine($"UsersPack filled with data, Count:{UserOnServer.UsersPack.Count}");
        }

        public static async Task DeserializeUsersFromStringToFile(string response, string token)
        {
            try
            {
                //ct = tokenSource.Token; 
                if (string.IsNullOrEmpty(response)) return;

                Task<UserPack> taskA = Task.Run(() => JsonConvert.DeserializeObject<UserPack>(response));
                //var user =  JsonConvert.DeserializeObject<UserPack>(response);
                // Execute the continuation when the antecedent finishes.

                //Task.Run(()=>UserOnServer.ConstructUsersOnServer(user, token, ref tokenSource, out ct), ct).
                //    ContinueWith(delegate { FileConfig.UpdateJSONfile(ct); }, ct, TaskContinuationOptions.ExecuteSynchronously).
                //    ContinueWith(delegate
                //    {
                //        try
                //        {
                //            if (ct.IsCancellationRequested)
                //            {
                //                ct.ThrowIfCancellationRequested();
                //            }
                //        }
                //        catch
                //        {
                //            throw;
                //        }

                //    }, TaskContinuationOptions.ExecuteSynchronously);


                await taskA.
                    ContinueWith(delegate { UserOnServer.ConstructUsersOnServer(taskA.Result, token, ref tokenSource, out ct); },
                    ct, TaskContinuationOptions.ExecuteSynchronously).
                    ContinueWith(delegate { FileConfig.UpdateJSONfile(ct); }, ct, TaskContinuationOptions.ExecuteSynchronously).
                    ContinueWith(delegate
                    {
                        try
                        {
                            if (ct.IsCancellationRequested)
                            {
                                ct.ThrowIfCancellationRequested();
                            }
                        }
                        catch
                        {
                            throw;
                        }

                    }, TaskContinuationOptions.ExecuteSynchronously);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine($"Code forbidden sent to {token}");
                throw;
            }

            //await taskA.
            //    ContinueWith(antecedent => Task.Run(() => UserOnServer.ConstructUsersOnServer(taskA.Result, token, ref tokenSource, out ct)).
            //    ContinueWith(lasttask => FileConfig.UpdateJSONfile(ct))).
            //    ContinueWith(check =>
            //    {
            //        if (ct.IsCancellationRequested)
            //        {
            //            ct.ThrowIfCancellationRequested();
            //        }
            //    });

        }
    }
}






