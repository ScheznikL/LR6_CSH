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


        public static async Task PeriodicGetMessage(TimeSpan interval, HttpClient client, string backendUrl)
        {
            var ct = tokenSource.Token;
            while (!tokenSource.IsCancellationRequested)
            {
               //if (!cancellationToken.IsCancellationRequested)
                await GettingMessages(client, backendUrl);
                await Task.Delay(interval, ct);
            }
        }

        public async static Task GettingMessages(HttpClient client, string backendUrl) //
        {
            var response = await client.GetAsync($"{backendUrl}/get-messages").
               ConfigureAwait(false); // for thisfunc go to another thread
            
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                UsersData.AddrecievedMessages(json);
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {            }
            else
            {
                tokenSource.Cancel();
                MessageBox.Show($"Error getting user data. Status Code: {response.StatusCode}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
