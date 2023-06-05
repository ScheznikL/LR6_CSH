using System;
using LR6_CSH_Client.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                //if (!cancellationToken.IsCancellationRequested)
                await Task.Run(() => GettingMessages(client, backendUrl, userD));
                await Task.Delay(interval, ct);
            }
        }

        public static async void GettingMessages(HttpClient client, string backendUrl, UsersData userD) //
        {
            var response = client.GetAsync($"{backendUrl}/get-messages").Result;
            //ConfigureAwait(false); // for thisfunc go to another thread

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return;
            }
            else if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                UsersData.AddRecievedMessages(json);
            }
            //else if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            //{ }
            else
            {
                tokenSource.Cancel();
                MessageBox.Show($"Error getting user data. Status Code: {response.StatusCode}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
