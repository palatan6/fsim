using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CalculatorModules.Base_Controls
{
    public partial class fsOptionsDoubleTableAndCommentsCalculatorControl : fsOptionsAndCommentsCalculatorControl
    {
        public fsOptionsDoubleTableAndCommentsCalculatorControl()
        {
            InitializeComponent();
        }

        protected override void StopGridsEdit()
        {
            dataGrid.EndEdit();
            materialParametersDataGrid.EndEdit();
        }
    }
}
