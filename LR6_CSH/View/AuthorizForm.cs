using System;
using LR6_CSH_Client.Controls;
using LR6_CSH_Client.Utils;
using LR6_CSH_Client.View;
using System.Drawing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Net.Http.Headers;

namespace LR6_CSH_Client
{
    public partial class AuthorizForm : Form
    {
        public HttpClient client = new HttpClient();
        //public string backendUrl = "http://10.0.2.15:8080";
        public string backendUrl = "http://localhost:8080";
        //public string backendUrl = "http://192.168.43.23:8080";
        //public string backendUrl = "http://192.168.56.1:8081";
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
        }
        private void btLogIn_Click(object sender, EventArgs e)
        {
            if (ValidateUserString.CellValidatingForLetterWithSpases(tbLogin, tbPassword))
            {
                tokenSource = new CancellationTokenSource();
                ct = tokenSource.Token;
                try
                {
                    if (flagcreateonce)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic",
                            $"{tbLogin.Text}:{tbPassword.Text}");
                        flagcreateonce = false;
                    }
                    _userpack = new PacketBuilder(tbPassword.Text, tbLogin.Text);
                    SentLogInRequest(_userpack, ct).ConfigureAwait(false);
                    OpenMainForm(stat, regFlag: false);
                }
                catch (OperationCanceledException)
                {
                    //MessageBox.Show($"\n{nameof(OperationCanceledException)} thrown\n");
                }
            }
        }
        private async void btRegister_Click(object sender, EventArgs e)
        {
            if (ValidateUserString.CellValidatingForLetterWithSpases(tbLogin, tbPassword))
            {
                tokenSource = new CancellationTokenSource();
                ct = tokenSource.Token;
                try
                {
                    if (flagcreateonce)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                            "Basic", $"{tbLogin.Text}:{tbPassword.Text}");
                        flagcreateonce = false;
                    }
                    _userpack = new PacketBuilder(tbPassword.Text, tbLogin.Text);
                    await Task.Run(() => SentRegisterRequest(_userpack, ct));
                    OpenMainForm(stat, regFlag: true);
                }
                catch
                {
                    //   MessageBox.Show($"\n{nameof(Exception)} thrown\n");
                }
            }
        }
        private Task SentLogInRequest(PacketBuilder userpack, CancellationToken ct)
        {
            try
            {
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
            catch
            {
                tokenSource.Cancel();
                ct.ThrowIfCancellationRequested();
                return Task.FromCanceled(ct);
            }
        }
        private void SentRegisterRequest(PacketBuilder userpack, CancellationToken ct)
        {
            var content = new StringContent(userpack.Userjson, Encoding.UTF8, "application/json");
            var response = client.PostAsync($"{backendUrl}/reg-user", content).Result;

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
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                MessageBox.Show($"Login {tbLogin.Text} already exist. Please choose another one.", "The login is taken",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void AuthorizForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            tokenSource.Dispose();
        }
        private void tbLogin_TextChanged(object sender, EventArgs e)
        {
            tbLogin.BackColor = Color.White;
        }
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.PasswordChar = checkBox.Checked ? '\0' : '*';
        }
        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
            tbPassword.BackColor = Color.White;
        }
    }
}
