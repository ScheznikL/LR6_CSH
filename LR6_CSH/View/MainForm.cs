using System;
using LR6_CSH_Client.Controls;
using LR6_CSH_Client.Model;
using LR6_CSH_Client.Utils;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private event Action<string> CreateMessageForm;
        private SolidBrush reportsForegroundBrushSelected = new SolidBrush(Color.FromKnownColor(KnownColor.PaleGoldenrod));
        private SolidBrush reportsForegroundBrush = new SolidBrush(Color.Black);
        private SolidBrush reportsBackgroundBrushSelected = new SolidBrush(Color.FromKnownColor(KnownColor.LightGoldenrodYellow));
        private SolidBrush reportsBackgroundEven = new SolidBrush(Color.FromKnownColor(KnownColor.LightGoldenrodYellow));
        private SolidBrush reportsBackgroundNotEven = new SolidBrush(Color.Bisque);

        private UsersData _usersdata = new UsersData();
        private HttpClient _client;
        private bool _regFlag;
        private int setIncreaseHeight = 25;
        //public string backendUrl = "http://192.168.43.23:8080";
        public string backendUrl = "http://localhost:8080";
        public string _previouslyRecievedFrom = "";
        private BindingSource _bs = new BindingSource();
        private string _httpStatusForTb = "";
        private Dictionary<string, List<string>> _listBIStorage = new Dictionary<string, List<string>>();

        PacketBuilder _user;

        public MainForm(UsersData _users, ref HttpClient client, bool regFlag, PacketBuilder user)
        {
            InitializeComponent();
            lBoxMessages.Visible = false;
            dgvAllUsers.AutoGenerateColumns = false;
            _usersdata = _users;
            _client = client;
            _regFlag = regFlag;
            _bs.DataSource = _usersdata.Userslist;
            dgvAllUsers.DataSource = _bs;
            _user = user;

            tbUserMessage.Enabled = false;
            lbUserName.Text = _user.UserFromPack.Login;
            UsersData.UserMessages.CollectionChanged += UserMessages_CollectionChanged;
            lBoxMessages.DrawMode = DrawMode.OwnerDrawVariable;
            CreateMessageForm += OnCreateMessageDialog;
        }

        private void OnCreateMessageDialog(string messageSender)
        {
            Task.Run(() =>
            {
                var newMessageForm = new AllSendersForm(messageSender);
                newMessageForm.ShowDialog();
            }).ConfigureAwait(false);
        }
        private void ClearSelection()
        {
            if (dgvAllUsers.SelectedRows.Count > 0)
            {
                dgvAllUsers.ClearSelection();
            }
        }
        private void UpdateListBox(string value)
        {
            if (lBoxMessages.InvokeRequired)
            {
                lBoxMessages.Invoke(new Action<string>(UpdateListBox), value);
            }
            else
            {
                if (value.Length > 60 || value.Contains('\n'))
                {
                    setIncreaseHeight = 50;
                }
                value = value.Replace("|", ": ");
                lBoxMessages.Items.Add(value);
                lBoxMessages.TopIndex = lBoxMessages.Items.Count - 1;
            }
        }

        private void lBoxMessages_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            /* if (e.Index % 2 == 0) e.ItemHeight += setIncreaseHeight;*///Set the Height of the item at index 2 to 50
                                                                         /*else */
            e.ItemHeight += setIncreaseHeight;
        }
        private void lBoxMessages_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            //e.Graphics.DrawString(le.ItemHeight += setIncreaseHeightBoxMessages.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            bool selected = (e.State == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < lBoxMessages.Items.Count)
            {
                Graphics g = e.Graphics;

                //background:
                SolidBrush backgroundBrush;
                if (selected)
                    backgroundBrush = reportsBackgroundBrushSelected;
                else if ((index % 2) == 0)
                    backgroundBrush = reportsBackgroundEven;
                else
                    backgroundBrush = reportsBackgroundNotEven;
                g.FillRectangle(backgroundBrush, e.Bounds);

                //text:
                SolidBrush foregroundBrush = (selected) ? reportsForegroundBrushSelected : reportsForegroundBrush;
                //  g.DrawString(lBoxMessages.Items[e.Index].ToString(), e.Font, foregroundBrush, lBoxMessages.GetItemRectangle(index).Location);
                e.Graphics.DrawString(lBoxMessages.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            }

            e.DrawFocusRectangle();
        }

        private void UserMessages_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<int> indexes = new List<int>();
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var senderU = _bs.Current as User;
                for (int i = UsersData.UserMessages.Count - 1; i >= 0; i--)
                {
                    if (senderU.Login == UsersData.UserMessages[i].SenderLogin && dgvAllUsers.SelectedCells.Count != 0)
                    {
                        if (lBoxMessages.Visible == true)
                        {
                            UpdateListBox(UsersData.UserMessages[i].Message);
                            lock (UsersData.Locker)
                            {
                                UsersData.UserMessages.RemoveAt(i);
                            }
                        }
                    }
                    else
                    {
                        if (_previouslyRecievedFrom != UsersData.UserMessages[i].SenderLogin)
                        {
                            _previouslyRecievedFrom = UsersData.UserMessages[i].SenderLogin;
                            CreateMessageForm?.Invoke(UsersData.UserMessages[i].SenderLogin);
                        }

                    }
                }
            }
        }

        private void ClearUserNameFromDGV()
        {
            SaveCurrentUserToUserData(_usersdata);
            _usersdata.Userslist.Remove(_usersdata.Userslist.Where(x => x.Login == _user.UserFromPack.Login).FirstOrDefault());
            _bs.ResetBindings(false);
            ClearSelection();
        }

        private void SaveCurrentUserToUserData(UsersData users)
        {
            UsersData.CurrentUser = _user.UserFromPack;
        }

        private void DGVAllUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _previouslyRecievedFrom = "";
            CheckDictionary();
            tbUserMessage.Enabled = true;
            tbUserMessage.Text = "";
            lbInitial.Visible = false;
            lBoxMessages.Visible = true;
        }

        private void CheckDictionary()
        {
            lBoxMessages.Items.Clear();
            var chosenU = _bs.Current as User;
            if (_listBIStorage.Count != 0)
            {
                foreach (var k in _listBIStorage)
                {

                    if (k.Key == chosenU.Login)
                    {
                        foreach (var i in k.Value)
                        {
                            UpdateListBox(i);
                        }
                    }
                }
                if (_listBIStorage.ContainsKey(chosenU.Login))
                {
                    _listBIStorage.Remove(chosenU.Login);
                }
            }
            else if(UsersData.UserMessages.Count > 0)
            {
                GetUpdates();
            }
        }

        private void StoreListBoxItems()
        {
            var user = _bs.Current as User;
            if (lBoxMessages.Items.Count != 0 && dgvAllUsers.SelectedCells.Count != 0)
            {
                if (!_listBIStorage.ContainsKey(user.Login))
                {
                    var savedMessages = new List<string>();
                    foreach (var item in lBoxMessages.Items)
                    {
                        savedMessages.Add(item.ToString());
                    }
                    _listBIStorage.Add(user.Login, savedMessages);
                }
            }
        }

        private void GetUpdates()
        {
            var senderU = _bs.Current as User;
            for (int i = UsersData.UserMessages.Count - 1; i >= 0; i--)
            {
                if (senderU.Login == UsersData.UserMessages[i].SenderLogin && dgvAllUsers.SelectedCells.Count != 0)
                {
                    UpdateListBox(UsersData.UserMessages[i].Message);
                    lock (UsersData.Locker)
                    {
                        UsersData.UserMessages.RemoveAt(i);
                    }
                }
                else
                {
                    if (_previouslyRecievedFrom != UsersData.UserMessages[i].SenderLogin)
                    {
                        _previouslyRecievedFrom = UsersData.UserMessages[i].SenderLogin;
                        CreateMessageForm?.Invoke(UsersData.UserMessages[i].SenderLogin);
                    }
                }
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await Task.Run(() => GetUserDataAsync("get-users"));
            _bs.ResetBindings(true);
            ClearUserNameFromDGV();
            await UserHttpRequets.PeriodicGetMessage(TimeSpan.FromSeconds(1), _client, backendUrl, _usersdata);
        }

        private async void btSent_Click(object sender, EventArgs e)
        {
            var reciever = _bs.Current as User;
            var userMessage = $"[{DateTime.Now.ToShortTimeString()}]: me :\n{tbUserMessage.Text}\n";
            var rowUserMes = tbUserMessage.Text;
            tbUserMessage.Text = "";

            UpdateListBox(userMessage);
            _user.UserFromPack.Message = rowUserMes;
            _user.UserFromPack.Resiver = reciever.Login;
            var userJsonWithMessage = JsonSerializer.Serialize(_user.UserFromPack);
            await PostUserMessage(userJsonWithMessage);

        }

        private async void GetUserDataAsync(string absolutePath)
        {
            var response = _client.GetAsync($"{backendUrl}/{absolutePath}").Result;
            //ConfigureAwait(false); // for thisfunc go to another thread

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                _usersdata.AddUsers(json);
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                MessageBox.Show("No one else here, but you users", "No content",
                 MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"Error getting user data. Status Code: {response.StatusCode}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task PostUserMessage(string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{backendUrl}/send-message", content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                //TODO INdecator
            }
            else
            {
                MessageBox.Show($"Error uploading user data. Status Code: {response.StatusCode}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _httpStatusForTb = $"Sending error: {response.StatusCode}\n";
            }
        }

        private void dgvAllUsers_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            StoreListBoxItems();
        }
    }
}
