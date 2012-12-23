using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CalculatorModules.Hydrocyclone.Feeds
{
    public partial class FormFeedCurves : Form
    {
        private fsHydrocycloneNewControl hydrocycloneControl;

        private void ProcessLostFocus(object sender, EventArgs e)
        {
            hydrocycloneControl.buttonShowFeeds.Enabled = true;
        }
        
        public FormFeedCurves(fsHydrocycloneNewControl hC)
        {
            InitializeComponent();
            hydrocycloneControl = hC;
            this.Deactivate += ProcessLostFocus;
        }

        private void FormFeedCurves_Activated(object sender, EventArgs e)
        {
            hydrocycloneControl.buttonShowFeeds.Enabled = false;
        }

        private void closeButton_Click(object sender, FormClosingEventArgs e)
        {
            hydrocycloneControl.buttonShowFeeds.Enabled = true;
            this.Visible = false;
            e.Cancel = true;
        }
    }
}
