namespace CalculatorModules.BeltFiltersWithReversibleTrays
{
    sealed partial class fsBeltFilterWithReversibleTrayControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.calculationComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.materialParametersDisplayCheckBox = new System.Windows.Forms.CheckBox();
            this.tablesSplitContainer.Panel1.SuspendLayout();
            this.tablesSplitContainer.Panel2.SuspendLayout();
            this.tablesSplitContainer.SuspendLayout();
            this.leftTopPanel.SuspendLayout();
            this.calculationOptionsPanel.SuspendLayout();
            this.tablesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablesSplitContainer
            // 
            this.tablesSplitContainer.Size = new System.Drawing.Size(287, 486);
            this.tablesSplitContainer.SplitterDistance = 140;
            // 
            // dataGrid
            // 
            this.dataGrid.Size = new System.Drawing.Size(143, 486);
            // 
            // materialParametersDataGrid
            // 
            this.materialParametersDataGrid.Size = new System.Drawing.Size(140, 486);
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Size = new System.Drawing.Size(287, 74);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Controls.Add(this.materialParametersDisplayCheckBox);
            this.calculationOptionsPanel.Controls.Add(this.label1);
            this.calculationOptionsPanel.Controls.Add(this.calculationComboBox);
            this.calculationOptionsPanel.Size = new System.Drawing.Size(236, 74);
            // 
            // tablesPanel
            // 
            this.tablesPanel.Location = new System.Drawing.Point(0, 74);
            this.tablesPanel.Size = new System.Drawing.Size(287, 486);
            // 
            // calculationComboBox
            // 
            this.calculationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.calculationComboBox.FormattingEnabled = true;
            this.calculationComboBox.Location = new System.Drawing.Point(84, 11);
            this.calculationComboBox.Name = "calculationComboBox";
            this.calculationComboBox.Size = new System.Drawing.Size(146, 21);
            this.calculationComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Calculation:";
            // 
            // materialParametersDisplayCheckBox
            // 
            this.materialParametersDisplayCheckBox.AutoSize = true;
            this.materialParametersDisplayCheckBox.Checked = true;
            this.materialParametersDisplayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.materialParametersDisplayCheckBox.Location = new System.Drawing.Point(3, 51);
            this.materialParametersDisplayCheckBox.Name = "materialParametersDisplayCheckBox";
            this.materialParametersDisplayCheckBox.Size = new System.Drawing.Size(149, 17);
            this.materialParametersDisplayCheckBox.TabIndex = 2;
            this.materialParametersDisplayCheckBox.Text = "Show Material Parameters";
            this.materialParametersDisplayCheckBox.UseVisualStyleBackColor = true;
            this.materialParametersDisplayCheckBox.CheckedChanged += new System.EventHandler(this.MaterialParametersDisplayCheckBoxCheckedChanged);
            // 
            // fsBeltFilterWithReversibleTrayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "fsBeltFilterWithReversibleTrayControl";
            this.Size = new System.Drawing.Size(287, 560);
            this.tablesSplitContainer.Panel1.ResumeLayout(false);
            this.tablesSplitContainer.Panel2.ResumeLayout(false);
            this.tablesSplitContainer.ResumeLayout(false);
            this.leftTopPanel.ResumeLayout(false);
            this.calculationOptionsPanel.ResumeLayout(false);
            this.calculationOptionsPanel.PerformLayout();
            this.tablesPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox calculationComboBox;
        private System.Windows.Forms.CheckBox materialParametersDisplayCheckBox;
    }
}
