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
                Console.WriteLine($"Serialization wrror occured: {ex}");
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
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public async static Task DeserializeUsersFromStringToFile(string response, string token)
        {
            if (string.IsNullOrEmpty(response)) return;
            try
            {
                Task<UserPack> taskA = Task.Run(() => JsonConvert.DeserializeObject<UserPack>(response));
                // Execute the continuation when the antecedent finishes.
                await taskA.
                    ContinueWith(antecedent => Task.Run(() => UserOnServer.ConstructUsersOnServer(antecedent.Result,token)).
                    ContinueWith(lasttask => FileConfig.UpdateJSONfile()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.GetType().ToString());
            }
        }
    }
}






