using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LR6_CSH_Client.Utils
{
    public class ValidateUserString
    {
        public static bool CellValidatingForLetterWithSpases(TextBox login, MaskedTextBox password)
        {
            if (HasSpecialCharsOrSpaces(login.Text))
            {
                login.BackColor = Color.FromKnownColor(KnownColor.LightSalmon);
                MessageBox.Show("Login shouldn't contain spases, or any spesial character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(password.Text.Contains(" "))
            {
                password.BackColor = Color.FromKnownColor(KnownColor.LightSalmon);
                MessageBox.Show("Password shouldn't contain spases.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrEmpty(password.Text))
            {
                password.BackColor = Color.FromKnownColor(KnownColor.LightSalmon);
                MessageBox.Show("Enter the password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if(password.Text.Length > 8)
            {
                password.BackColor = Color.FromKnownColor(KnownColor.LightSalmon);
                MessageBox.Show("Password shouldn't be more than 8 symbols", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
       
        private static bool HasSpecialCharsOrSpaces(string str)
        {
            return str.Any(ch => !char.IsLetter(ch) && !char.IsDigit(ch));
        }
        private static bool IsDigit(string str)
        {
            return str.All(ch => char.IsDigit(ch));
        }
    }
}
