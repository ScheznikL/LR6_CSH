using System.Linq;
using System.Windows.Forms;

namespace LR4_CSH
{
    class ValidateUserString
    {
        public static void CellValidatingForLetterOnly(object sender, DataGridViewCellValidatingEventArgs e, DataGridView dataGridView, out bool isValid)
        {
            isValid = true;
            dataGridView.Rows[e.RowIndex].ErrorText = "";
            if (dataGridView.Rows[e.RowIndex].IsNewRow) { return; }
            if (HasSpecialCharsOrDigitOrSpaces(e.FormattedValue.ToString()))
            {
                isValid = false;
                e.Cancel = true;
                MessageBox.Show("Enter letters only.");
            }
        }
        public static void CellValidatingForLetterWithSpases(object sender, DataGridViewCellValidatingEventArgs e, DataGridView dataGridView, out bool isValid)
        {
            isValid = true;
            dataGridView.Rows[e.RowIndex].ErrorText = "";
            if (dataGridView.Rows[e.RowIndex].IsNewRow) { return; }
            if (HasSpecialCharsOrDigit(e.FormattedValue.ToString()))
            {
                isValid = false;
                e.Cancel = true;
                MessageBox.Show("Enter letters only.");
            }
        }
        public static void CellValidatingForDigitOnly(object sender, DataGridViewCellValidatingEventArgs e, DataGridView dataGridView, out bool isValid)
        {
            isValid = true;
            dataGridView.Rows[e.RowIndex].ErrorText = "";
            if (dataGridView.Rows[e.RowIndex].IsNewRow) { return; }
            if (!IsDigit(e.FormattedValue.ToString()))
            {
                isValid = false;
                e.Cancel = true;
                MessageBox.Show("Enter digits only.");
            }
        }
        public static void CellValidatingForUniqness(object sender, DataGridViewCellEventArgs e, DataGridView dataGridView, out bool isValid)
        {
            isValid = true;
            int hitCounter = 0;
            dataGridView.Rows[e.RowIndex].ErrorText = "";
            if (dataGridView.Rows[e.RowIndex].IsNewRow) { return; }
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                if (dataGridView.Rows[i].Cells[e.ColumnIndex].Value.ToString() == dataGridView.CurrentCell.Value.ToString())
                {
                    hitCounter++;
                }
            }
            if (hitCounter > 1)
            {
                isValid = false;
                MessageBox.Show("Enter only uniqe ID.");
            }
        }
        private static bool HasSpecialCharsOrDigitOrSpaces(string str)
        {
            return str.Any(ch => !char.IsLetter(ch));
        }
        private static bool HasSpecialCharsOrDigit(string str)
        {
            return str.Any(ch =>
            {
                if (!char.IsLetter(ch))
                {
                    if (char.IsWhiteSpace(ch))
                    {
                        return false;
                    }
                    else return true;
                }
                return false;
            });
        }
        private static bool IsDigit(string str)
        {
            return str.All(ch => char.IsDigit(ch));
        }
        public static string CapitalizeFirstLetter(string str)
        {
            if (str == null || str.Length == 0)
                return "";
            else if (str.Length == 1)
                return char.ToUpper(str[0]).ToString();
            else
                return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}
