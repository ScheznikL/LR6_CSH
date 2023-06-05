using System.Windows.Forms;

namespace LR6_CSH_Client.View
{
    public partial class AllSendersForm : Form
    {
        public AllSendersForm(string messageSender)
        {
            InitializeComponent();
            lbNotification.Text += messageSender;
        }
    }
}
