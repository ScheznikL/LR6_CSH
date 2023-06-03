using System;
using LR6_CSH_Client.Controls;
using LR6_CSH_Client.Model;
using LR6_CSH_Client.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Net;
using System.Threading;
using System.Net.Http.Headers;

namespace LR6_CSH_Client
{
    public partial class AuthorizForm : Form
    {
        public HttpClient client /*= new HttpClient()*/;
        public string backendUrl = "http://localhost:8080";
        //public string backendUrl = "http://192.168.43.23:8080";
        private UsersData _users;
        private PacketBuilder _userpack;
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken ct;
        TaskStatus stat;
        bool flagcreateonce = true;

        public AuthorizForm()
        {
            InitializeComponent();
            _users = new UsersData();
            //client.DefaultRequestHeaders.Add("Authorization", /*Convert.ToBase64String(*/
            //     //System.Text.ASCIIEncoding.ASCII.GetBytes(
            //        $"Basic {tbLogin.Text}:{tbPassword.Text}");


        }

        private void btLogIn_Click(object sender, EventArgs e)
        {//TODO double




            ct = tokenSource.Token;
            try
            {
                if (flagcreateonce)
                {
                    client = new HttpClient(new HttpClientHandler()
                    {
                        Credentials = new NetworkCredential(tbLogin.Text, tbPassword.Text)
                    });
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic",/* Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(*/
                        $"{tbLogin.Text}:{tbPassword.Text}"   /*))*/);
                        flagcreateonce = false;
                }
                _userpack = new PacketBuilder(tbPassword.Text, tbLogin.Text);
                SentLogInRequest(_userpack, ct).ConfigureAwait(false);
                OpenMainForm(stat, regFlag: false); //!!!!!!!!!!!!!!!!
                //await SentLogInRequest(_userpack, ct);
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show($"\n{nameof(OperationCanceledException)} thrown\n");
                //OpenMainForm(taskLog.Status, regFlag: false);
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"\n{ex.Message} thrown\n");
            //    //OpenMainForm(TaskStatus.Faulted, regFlag: false);
            //}
            // await SentLogInRequest(_userpack, ct).ContinueWith(previous => OpenMainForm(previous.Status, regFlag: false));
        }
        private void btRegister_Click(object sender, EventArgs e)
        {
            ct = tokenSource.Token;
            try
            {
                _userpack = new PacketBuilder(tbPassword.Text, tbLogin.Text);
                SentRegisterRequest(_userpack, ct).ConfigureAwait(false);
                //await SentRegisterRequest(_userpack, ct).ContinueWith(previous => OpenMainForm(previous.Status, regFlag: false));
                OpenMainForm(stat, regFlag: true);
            }
            catch
            {
                //   MessageBox.Show($"\n{nameof(Exception)} thrown\n");
            }
        }

        private Task SentLogInRequest(PacketBuilder userpack, CancellationToken ct)
        {

            //using (var requestMessage =
            //new HttpRequestMessage(HttpMethod.Get, $"{backendUrl}/auth-user"))
            //{
            //    //var content = new StringContent(userpack.Userjson, Encoding.UTF8, "application/json");
            //    //requestMessage.Content = content;
            //    requestMessage.Headers.Authorization =
            //        new AuthenticationHeaderValue("Negotiate", "MY_TOKEN");

            //    await client.SendAsync(requestMessage);

            //}

            /**********************/
            var content = new StringContent(userpack.Userjson, Encoding.UTF8, "application/json");
            var response = client.PostAsync($"{backendUrl}/auth-user", content).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show($"You was loged in successfully.");
                stat = TaskStatus.RanToCompletion;
                return Task.CompletedTask;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                MessageBox.Show($"Wrong Password!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tokenSource.Cancel();
                stat = TaskStatus.Canceled;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                MessageBox.Show($"No such user exist", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tokenSource.Cancel();
                stat = TaskStatus.Canceled;
            }
            else
            {
                MessageBox.Show($"Error uploading user data. Status Code: {response.StatusCode}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tokenSource.Cancel();
                stat = TaskStatus.Faulted;
            }
            if (ct.IsCancellationRequested)
            {
                ct.ThrowIfCancellationRequested();
                return Task.FromCanceled(ct);
            }

            return Task.CompletedTask;
        }
        private async Task SentRegisterRequest(PacketBuilder userpack, CancellationToken ct)
        {

            var content = new StringContent(userpack.Userjson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{backendUrl}/reg-user", content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("User data uploaded successfully.");
                stat = TaskStatus.RanToCompletion;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                MessageBox.Show($"Error uploading user data. Status Code: {response.StatusCode}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                tokenSource.Cancel();
                stat = TaskStatus.Canceled;
            }
            else
            {
                MessageBox.Show($"Error uploading user data. Status Code: {response.StatusCode}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tokenSource.Cancel();
                stat = TaskStatus.Canceled;
            }
            if (ct.IsCancellationRequested)
            {
                stat = TaskStatus.Canceled;
                ct.ThrowIfCancellationRequested();
            }
        }

        private void OpenMainForm(TaskStatus stat, bool regFlag)
        {
            if (stat != TaskStatus.Canceled || stat != TaskStatus.Faulted)
            {
                MainForm mainform = new MainForm(_users, ref client, regFlag, _userpack);
                Size = new Size(553, 518);
                panelMainWin.Dock = DockStyle.Fill;

                mainform.TopLevel = false;
                mainform.FormBorderStyle = FormBorderStyle.None;
                mainform.Dock = DockStyle.Fill;
                panelLogIn.Visible = false;
                panelMainWin.Controls.Add(mainform);
                panelMainWin.Tag = mainform;
                mainform.BringToFront();
                mainform.Show();
            }
        }

        private void btRegister_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void btLogIn_MouseClick(object sender, MouseEventArgs e)
        {
            //  OpenMainForm(regFlag: false);
        }

        private void AuthorizForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tokenSource.Dispose();
        }
    }
}
