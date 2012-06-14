namespace SmallCalculator2
{
    partial class fsUnitsDialog
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
            this.fsUnitsControl1 = new fsUIControls.fsUnitsControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.showSecondaryCheckbox = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fsUnitsControl1
            // 
            this.fsUnitsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fsUnitsControl1.Location = new System.Drawing.Point(0, 0);
            this.fsUnitsControl1.Name = "fsUnitsControl1";
            this.fsUnitsControl1.Size = new System.Drawing.Size(318, 257);
            this.fsUnitsControl1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.showSecondaryCheckbox);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 257);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 72);
            this.panel1.TabIndex = 3;
            // 
            // showSecondaryCheckbox
            // 
            this.showSecondaryCheckbox.AutoSize = true;
            this.showSecondaryCheckbox.Location = new System.Drawing.Point(12, 6);
            this.showSecondaryCheckbox.Name = "showSecondaryCheckbox";
            this.showSecondaryCheckbox.Size = new System.Drawing.Size(160, 17);
            this.showSecondaryCheckbox.TabIndex = 2;
            this.showSecondaryCheckbox.Text = "Show secondary parameters";
            this.showSecondaryCheckbox.UseVisualStyleBackColor = true;
            this.showSecondaryCheckbox.CheckedChanged += new System.EventHandler(this.ParametersDisplayCheckedChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(231, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(150, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonOkClick);
            // 
            // fsUnitsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 329);
            this.Controls.Add(this.fsUnitsControl1);
            this.Controls.Add(this.panel1);
            this.Name = "fsUnitsDialog";
            this.Text = "UnitsDialog";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

    
        private fsUIControls.fsUnitsControl fsUnitsControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox showSecondaryCheckbox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}