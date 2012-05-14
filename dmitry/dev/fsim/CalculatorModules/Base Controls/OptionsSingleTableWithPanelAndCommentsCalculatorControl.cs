using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CalculatorModules
{
    public partial class OptionsSingleTableWithPanelAndCommentsCalculatorControl : fsOptionsAndCommentsCalculatorControl
    {
        public OptionsSingleTableWithPanelAndCommentsCalculatorControl()
        {
            InitializeComponent();
        }

        protected override void StopGridsEdit()
        {
            dataGrid.EndEdit();
        }
    }
}
