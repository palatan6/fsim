namespace CalculatorModules.Cake_Fromation
{
    partial class fsCakeFormationBaseControl
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
            this.materialParametersDisplayCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.calculationComboBox = new System.Windows.Forms.ComboBox();
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
            this.tablesSplitContainer.Size = new System.Drawing.Size(287, 341);
            this.tablesSplitContainer.SplitterDistance = 139;
            // 
            // dataGrid
            // 
            this.dataGrid.Size = new System.Drawing.Size(144, 341);
            // 
            // materialParametersDataGrid
            // 
            this.materialParametersDataGrid.Size = new System.Drawing.Size(139, 341);
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Size = new System.Drawing.Size(287, 64);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Controls.Add(this.materialParametersDisplayCheckBox);
            this.calculationOptionsPanel.Controls.Add(this.label1);
            this.calculationOptionsPanel.Controls.Add(this.calculationComboBox);
            this.calculationOptionsPanel.Size = new System.Drawing.Size(236, 64);
            // 
            // tablesPanel
            // 
            this.tablesPanel.Location = new System.Drawing.Point(0, 64);
            this.tablesPanel.Size = new System.Drawing.Size(287, 341);
            // 
            // materialParametersDisplayCheckBox
            // 
            this.materialParametersDisplayCheckBox.AutoSize = true;
            this.materialParametersDisplayCheckBox.Checked = true;
            this.materialParametersDisplayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.materialParametersDisplayCheckBox.Location = new System.Drawing.Point(3, 43);
            this.materialParametersDisplayCheckBox.Name = "materialParametersDisplayCheckBox";
            this.materialParametersDisplayCheckBox.Size = new System.Drawing.Size(149, 17);
            this.materialParametersDisplayCheckBox.TabIndex = 5;
            this.materialParametersDisplayCheckBox.Text = "Show Material Parameters";
            this.materialParametersDisplayCheckBox.UseVisualStyleBackColor = true;
            this.materialParametersDisplayCheckBox.CheckedChanged += new System.EventHandler(this.MaterialParametersDisplayCheckBoxCheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Calculation:";
            // 
            // calculationComboBox
            // 
            this.calculationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.calculationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.calculationComboBox.FormattingEnabled = true;
            this.calculationComboBox.Location = new System.Drawing.Point(84, 8);
            this.calculationComboBox.Name = "calculationComboBox";
            this.calculationComboBox.Size = new System.Drawing.Size(146, 21);
            this.calculationComboBox.TabIndex = 3;
            // 
            // fsCakeFormationBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "fsCakeFormationBaseControl";
            this.Size = new System.Drawing.Size(287, 405);
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

        private System.Windows.Forms.CheckBox materialParametersDisplayCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox calculationComboBox;
    }
}
