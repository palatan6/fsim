﻿namespace CalculatorModules
{
    sealed partial class fsCakeMoistureContentFromWetAndDryCakeMassControl
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
            this.leftTopPanel.Size = new System.Drawing.Size(338, 72);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Controls.Add(this.saltContentComboBox);
            this.calculationOptionsPanel.Controls.Add(this.concentrationComboBox);
            this.calculationOptionsPanel.Controls.Add(this.label1);
            this.calculationOptionsPanel.Controls.Add(this.concentrationLabel);
            this.calculationOptionsPanel.Size = new System.Drawing.Size(287, 72);
            // 
            // saltContentComboBox
            // 
            this.saltContentComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saltContentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.saltContentComboBox.FormattingEnabled = true;
            this.saltContentComboBox.Items.AddRange(new object[] {
            "Considered",
            "Neglected"});
            this.saltContentComboBox.Location = new System.Drawing.Point(140, 6);
            this.saltContentComboBox.Name = "saltContentComboBox";
            this.saltContentComboBox.Size = new System.Drawing.Size(136, 21);
            this.saltContentComboBox.TabIndex = 0;
            // 
            // concentrationComboBox
            // 
            this.concentrationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.concentrationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.concentrationComboBox.FormattingEnabled = true;
            this.concentrationComboBox.Location = new System.Drawing.Point(140, 38);
            this.concentrationComboBox.Name = "concentrationComboBox";
            this.concentrationComboBox.Size = new System.Drawing.Size(136, 21);
            this.concentrationComboBox.TabIndex = 3;
            // 
            // concentrationLabel
            // 
            this.concentrationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.concentrationLabel.AutoSize = true;
            this.concentrationLabel.Location = new System.Drawing.Point(23, 42);
            this.concentrationLabel.Name = "concentrationLabel";
            this.concentrationLabel.Size = new System.Drawing.Size(111, 13);
            this.concentrationLabel.TabIndex = 2;
            this.concentrationLabel.Text = "Solved salt measured:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Salt Content in dry cake:";
            // 
            // fsCakeMoistureContentFromWetAndDryCakeMassControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "fsCakeMoistureContentFromWetAndDryCakeMassControl";
            this.Size = new System.Drawing.Size(338, 224);
            this.leftTopPanel.ResumeLayout(false);
            this.calculationOptionsPanel.ResumeLayout(false);
            this.calculationOptionsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox concentrationComboBox;
        private System.Windows.Forms.Label concentrationLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox saltContentComboBox;
    }
}