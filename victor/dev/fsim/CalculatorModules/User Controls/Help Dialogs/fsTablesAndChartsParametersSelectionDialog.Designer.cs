namespace CalculatorModules.User_Controls.Help_Dialogs
{
    partial class fsTablesAndChartsParametersSelectionDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.fsTablesAndChartsParametersSelectionControl1 = new CalculatorModules.User_Controls.Help_Dialogs.fsTablesAndChartsParametersSelectionControl();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(412, 427);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Location = new System.Drawing.Point(331, 427);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // fsTablesAndChartsParametersSelectionControl1
            // 
            this.fsTablesAndChartsParametersSelectionControl1.Location = new System.Drawing.Point(12, 12);
            this.fsTablesAndChartsParametersSelectionControl1.Name = "fsTablesAndChartsParametersSelectionControl1";
            this.fsTablesAndChartsParametersSelectionControl1.Size = new System.Drawing.Size(489, 409);
            this.fsTablesAndChartsParametersSelectionControl1.TabIndex = 3;
            // 
            // fsTablesAndChartsParametersSelectionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 458);
            this.Controls.Add(this.fsTablesAndChartsParametersSelectionControl1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Name = "fsTablesAndChartsParametersSelectionDialog";
            this.Text = "fsTablesAndChartsParametersSelectionDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private fsTablesAndChartsParametersSelectionControl fsTablesAndChartsParametersSelectionControl1;
    }
}