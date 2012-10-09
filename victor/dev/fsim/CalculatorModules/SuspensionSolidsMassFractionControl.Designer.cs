namespace CalculatorModules
{
    partial class SuspensionSolidsMassFractionControl
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
            this.saltContentComboBox = new System.Windows.Forms.ComboBox();
            this.concentrationComboBox = new System.Windows.Forms.ComboBox();
            this.concentrationLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.leftTopPanel.SuspendLayout();
            this.calculationOptionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Size = new System.Drawing.Size(362, 72);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Controls.Add(this.saltContentComboBox);
            this.calculationOptionsPanel.Controls.Add(this.concentrationComboBox);
            this.calculationOptionsPanel.Controls.Add(this.label1);
            this.calculationOptionsPanel.Controls.Add(this.concentrationLabel);
            this.calculationOptionsPanel.Size = new System.Drawing.Size(311, 72);
            // 
            // saltContentComboBox
            // 
            this.saltContentComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saltContentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.saltContentComboBox.FormattingEnabled = true;
            this.saltContentComboBox.Items.AddRange(new object[] {
            "Considered",
            "Neglected"});
            this.saltContentComboBox.Location = new System.Drawing.Point(125, 8);
            this.saltContentComboBox.Name = "saltContentComboBox";
            this.saltContentComboBox.Size = new System.Drawing.Size(180, 21);
            this.saltContentComboBox.TabIndex = 0;
            // 
            // concentrationComboBox
            // 
            this.concentrationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.concentrationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.concentrationComboBox.FormattingEnabled = true;
            this.concentrationComboBox.Location = new System.Drawing.Point(125, 40);
            this.concentrationComboBox.Name = "concentrationComboBox";
            this.concentrationComboBox.Size = new System.Drawing.Size(180, 21);
            this.concentrationComboBox.TabIndex = 3;
            // 
            // concentrationLabel
            // 
            this.concentrationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.concentrationLabel.AutoSize = true;
            this.concentrationLabel.Location = new System.Drawing.Point(-30, 43);
            this.concentrationLabel.Name = "concentrationLabel";
            this.concentrationLabel.Size = new System.Drawing.Size(149, 13);
            this.concentrationLabel.TabIndex = 2;
            this.concentrationLabel.Text = "Solutes Content Measured as:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nonvolatile Solutes:";
            // 
            // SuspensionSolidsMassFractionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SuspensionSolidsMassFractionControl";
            this.Size = new System.Drawing.Size(362, 224);
            this.leftTopPanel.ResumeLayout(false);
            this.calculationOptionsPanel.ResumeLayout(false);
            this.calculationOptionsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox saltContentComboBox;
        private System.Windows.Forms.ComboBox concentrationComboBox;
        private System.Windows.Forms.Label concentrationLabel;
        private System.Windows.Forms.Label label1;
    }
}
