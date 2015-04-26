using System.Windows.Forms;
using CalculatorModules;

namespace SmallCalculator2
{
    public partial class CommentsWindow : Form
    {
        public CommentsWindow()
        {
            InitializeComponent();
        }

        public void ShowDialog(fsCalculatorControl module)
        {
            textBox1.Text = module.GetCommentsText();
            ShowDialog();
        }

        public string GetText()
        {
            return textBox1.Text;
        }
    }
}
