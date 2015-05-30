﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Units;

namespace SmallCalculator2
{
    public partial class fsUnitsDialog : Form
    {
        private int SmallFormHeight = 460;
        private int BigFormHeight = 650;

        public Dictionary<fsCharacteristic, fsUnit> Characteristics
        {
            get { return fsUnitsControl1.Characteristics; }
        }

        public fsUnitsDialog()
        {
            InitializeComponent();
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {
            fsUnitsControl1.Save();
            CloseDialogWindow();
        }

        private void ButtonCancelClick(object sender, EventArgs e)
        {
            CloseDialogWindow();
        }

        private void CloseDialogWindow()
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
