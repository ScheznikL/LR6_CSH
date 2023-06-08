using System;
using System.IO;
using System.Threading;

namespace LR6_CSH_Server
{
    static class FileConfig
    {
        private static string currentDir = Directory.GetCurrentDirectory();
        private static string filepath = "";

        public static void ReadUserInfo()
        {
            filepath = $"{CreateDirictoryIfNotExist()}\\userData2.json";
            if (File.Exists(filepath))
            {
                var fileInfo = new FileInfo(filepath);
                if (fileInfo.Length > 0)
                {
                    if (!JSONOrganaizer.DeserializeUsersFromFile(filepath))
                    {
                        Console.WriteLine($"Fail to load users from file.");
                    }
                    else
                    {
                        Console.WriteLine($"Server gets users info.");
                    }
                }
                else
                {
                    Console.WriteLine($"For now, file:{filepath} is empty.");
                }
            }
            else
            {
                File.Create(filepath);
                Console.WriteLine($"File:{filepath} created.");
            }
        }
        private static string CreateDirictoryIfNotExist()
        {
            if (!Directory.Exists($@"{currentDir}\UserBase)"))
            {
                Directory.CreateDirectory($@"{currentDir}\UserBase");
            }
            filepath = $@"{currentDir}\UserBase";
            return filepath;
        }
        public static void UpdateJSONfile(CancellationToken ct)
        {
            if (!ct.IsCancellationRequested)
            {
                try
                {
                    JSONOrganaizer.SerializeUserData(filepath);
                    Console.WriteLine("JSON File updated.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while updating file, message: {ex.Message}");
                }
            }
        }
    }
}
