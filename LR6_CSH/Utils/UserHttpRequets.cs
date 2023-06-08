using System;
using LR6_CSH_Client.Controls;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms;
using System.Threading;

namespace LR6_CSH_Client.Utils
{
    public class UserHttpRequets
    {
        static CancellationTokenSource tokenSource = new CancellationTokenSource();

        public static async Task PeriodicGetMessage(TimeSpan interval, HttpClient client, string backendUrl, UsersData userD)
        {
            var ct = tokenSource.Token;
            while (!tokenSource.IsCancellationRequested)
            {
                try
                {
                    await Task.Run(() => GettingMessages(client, backendUrl, userD));
                    await Task.Delay(interval, ct);
                }
                catch (Exception ex)
                {
                    tokenSource.Cancel();
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public static async void GettingMessages(HttpClient client, string backendUrl, UsersData userD)
        {
            var response = client.GetAsync($"{backendUrl}/get-messages").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return;
            }
            else if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                UsersData.AddRecievedMessages(json);
            }
            else
            {
                tokenSource.Cancel();
                MessageBox.Show($"Error getting user data. Status Code: {response.StatusCode}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //public static 
    }
}
