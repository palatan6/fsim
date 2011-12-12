﻿namespace CalculatorModules
{
    sealed partial class fsPkeFromPcRcControl
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
            this.enterSolidsDensityComboBox = new System.Windows.Forms.ComboBox();
            this.enterSolidsDensityLabel = new System.Windows.Forms.Label();
            this.inputCakeComboBox = new System.Windows.Forms.ComboBox();
            this.inputCake = new System.Windows.Forms.Label();
            this.leftTopPanel.SuspendLayout();
            this.calculationOptionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Size = new System.Drawing.Size(327, 72);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Controls.Add(this.enterSolidsDensityComboBox);
            this.calculationOptionsPanel.Controls.Add(this.inputCakeComboBox);
            this.calculationOptionsPanel.Controls.Add(this.enterSolidsDensityLabel);
            this.calculationOptionsPanel.Controls.Add(this.inputCake);
            this.calculationOptionsPanel.Size = new System.Drawing.Size(276, 72);
            // 
            // enterSolidsDensityComboBox
            // 
            this.enterSolidsDensityComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enterSolidsDensityComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.enterSolidsDensityComboBox.FormattingEnabled = true;
            this.enterSolidsDensityComboBox.Location = new System.Drawing.Point(124, 38);
            this.enterSolidsDensityComboBox.Name = "enterSolidsDensityComboBox";
            this.enterSolidsDensityComboBox.Size = new System.Drawing.Size(146, 21);
            this.enterSolidsDensityComboBox.TabIndex = 3;
            // 
            // enterSolidsDensityLabel
            // 
            this.enterSolidsDensityLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.enterSolidsDensityLabel.AutoSize = true;
            this.enterSolidsDensityLabel.Location = new System.Drawing.Point(14, 42);
            this.enterSolidsDensityLabel.Name = "enterSolidsDensityLabel";
            this.enterSolidsDensityLabel.Size = new System.Drawing.Size(104, 13);
            this.enterSolidsDensityLabel.TabIndex = 2;
            this.enterSolidsDensityLabel.Text = "Enter Solids Density:";
            // 
            // inputCakeComboBox
            // 
            this.inputCakeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputCakeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputCakeComboBox.FormattingEnabled = true;
            this.inputCakeComboBox.Location = new System.Drawing.Point(124, 6);
            this.inputCakeComboBox.Name = "inputCakeComboBox";
            this.inputCakeComboBox.Size = new System.Drawing.Size(146, 21);
            this.inputCakeComboBox.TabIndex = 1;
            // 
            // inputCake
            // 
            this.inputCake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputCake.AutoSize = true;
            this.inputCake.Location = new System.Drawing.Point(56, 9);
            this.inputCake.Name = "inputCake";
            this.inputCake.Size = new System.Drawing.Size(62, 13);
            this.inputCake.TabIndex = 0;
            this.inputCake.Text = "Input Cake:";
            // 
            // fsPkeFromPcRcControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "fsPkeFromPcRcControl";
            this.Size = new System.Drawing.Size(327, 250);
            this.leftTopPanel.ResumeLayout(false);
            this.calculationOptionsPanel.ResumeLayout(false);
            this.calculationOptionsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox inputCakeComboBox;
        private System.Windows.Forms.Label inputCake;
        private System.Windows.Forms.ComboBox enterSolidsDensityComboBox;
        private System.Windows.Forms.Label enterSolidsDensityLabel;
    }
}