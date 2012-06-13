using System.Windows.Forms;

namespace fmDataGrid
{
    public class fmNumericalTextBoxHelper
    {
        public static void CheckKey(ref Keys key)
        {
            if (key >= (Keys)32)
            {
                if (key == (Keys)'.' || key == (Keys)',')
                {
                    key = (Keys)System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                }
                else if (!char.IsDigit((char)key))
                {
                    key = Keys.None;
                }
            }
            else if (key == Keys.Enter)
            {
                key = Keys.None;
            }
        }

        public static void CheckTextBox(TextBox tb)
        {
            double dVal;

            if (double.TryParse(tb.Text, out dVal))
            {
                tb.Font = new System.Drawing.Font(tb.Font, System.Drawing.FontStyle.Regular);
                tb.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                tb.Font = new System.Drawing.Font(tb.Font, System.Drawing.FontStyle.Bold);
                tb.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
