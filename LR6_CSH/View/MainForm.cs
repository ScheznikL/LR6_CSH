using System;
using LR6_CSH_Client.Controls;
using LR6_CSH_Client.Model;
using LR6_CSH_Client.Utils;
using LR6_CSH_Client.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Text.Json;
using System.Net;

namespace LR6_CSH_Client.View
{
    public partial class MainForm : Form
    {
        private UsersData _usersdata = new UsersData();
        private HttpClient _client = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
        private bool _regFlag;
        //public string backendUrl = "http://192.168.43.23:8080";
        public string backendUrl = "http://localhost:8080";
        private BindingSource _bs = new BindingSource();
        private string _httpStatusForTb = "";

        PacketBuilder _user;

        public MainForm(UsersData _users, ref HttpClient client, bool regFlag, PacketBuilder user)
        {
            InitializeComponent();
            dgvAllUsers.AutoGenerateColumns = false;
            _usersdata = _users;
            _client = client;
            _regFlag = regFlag;
            _bs.DataSource = _usersdata.Userslist;
            dgvAllUsers.DataSource = _bs;
            _user = user;
            ClearUserNameFromDGV();
            tbUserMessage.Enabled = false;
        }

        private void ClearUserNameFromDGV()
        {
            //var res = dgvAllUsers.Rows.Cast<DataGridViewRow>().Where(x => x.Cells[0].Value.ToString() == _user.UserFromPack.Login).ToList();
            // dgvAllUsers.Rows.Remove(dgvAllUsers.Rows.Cast<DataGridViewRow>().Where(x => x.Cells[0].Value.ToString() == _user.UserFromPack.Login).FirstOrDefault());
            _usersdata.Userslist.Remove(_user.UserFromPack);
            _bs.ResetBindings(false);
            //foreach (var row in dgvAllUsers.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        if (cell.Value != null && cell.Value.ToString() == searchString)
            //        {
            //            // Удалить строку
            //            dataGridView.Rows.Remove(row);
            //            break;
            //        }
            //    }
            //}
        }
        private void dgvAllUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string usermessage;
            tbUserMessage.Enabled = true;
            tbUserMessage.Text = "";

            if (tbMessages.Text != @"


                                        Choose the person ")
            {
                usermessage = tbMessages.Text;//TODO save
            }
            tbMessages.Text = "";
        }

        private void tbUserMessage_KeyDown(object sender, KeyEventArgs e)
        {
            //Not here
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await GetUserDataAsync("get-users");
            _bs.ResetBindings(true);
            await UserHttpRequets.PeriodicGetMessage(TimeSpan.FromSeconds(1), _client, backendUrl);
        }       

        private async void btSent_Click(object sender, EventArgs e)
        {
            var reciever = _bs.Current as User;
            var userMessage = $"[{DateTime.Now}]: me :\n{tbUserMessage.Text}\n";
            var rowUserMes = tbUserMessage.Text;
            tbMessages.Text += userMessage;

            _user.UserFromPack.Message = rowUserMes;
            _user.UserFromPack.Resiver = reciever.Login;
            var userJsonWithMessage = JsonSerializer.Serialize(_user.UserFromPack);
            await PostUseMessage(userJsonWithMessage);
            tbMessages.Text += _httpStatusForTb;
        }

        private async Task GetUserDataAsync(string absolutePath)
        {
            var response = await _client.GetAsync($"{backendUrl}/{absolutePath}").
                ConfigureAwait(false); // for thisfunc go to another thread

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                _usersdata.AddUsers(json);
            }
            else
            {
                MessageBox.Show($"Error getting user data. Status Code: {response.StatusCode}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task PostUseMessage(string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{backendUrl}/send-message", content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
               
            }
            else
            {
                MessageBox.Show($"Error uploading user data. Status Code: {response.StatusCode}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);                
                _httpStatusForTb = $"Sending error: {response.StatusCode}\n";
            }
        }


    }
}
