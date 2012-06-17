namespace Calculator
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
            this.rightPanel = new System.Windows.Forms.Panel();
            this.fsUnitsControl1 = new fsUIControls.fsUnitsControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.m_showSecondaryCheckbox = new System.Windows.Forms.CheckBox();
            this.listingPanel = new System.Windows.Forms.Panel();
            this.m_futureCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rightPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightPanel
            // 
            this.rightPanel.AutoScroll = true;
            this.rightPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.rightPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rightPanel.Controls.Add(this.fsUnitsControl1);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point(233, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(326, 377);
            this.rightPanel.TabIndex = 0;
            // 
            // fsUnitsControl1
            // 
            this.fsUnitsControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fsUnitsControl1.Location = new System.Drawing.Point(0, 0);
            this.fsUnitsControl1.Name = "fsUnitsControl1";
            this.fsUnitsControl1.Size = new System.Drawing.Size(322, 373);
            this.fsUnitsControl1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 408);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(559, 36);
            this.panel2.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(472, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(391, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // m_showSecondaryCheckbox
            // 
            this.m_showSecondaryCheckbox.AutoSize = true;
            this.m_showSecondaryCheckbox.Location = new System.Drawing.Point(273, 8);
            this.m_showSecondaryCheckbox.Name = "m_showSecondaryCheckbox";
            this.m_showSecondaryCheckbox.Size = new System.Drawing.Size(160, 17);
            this.m_showSecondaryCheckbox.TabIndex = 2;
            this.m_showSecondaryCheckbox.Text = "Show secondary parameters";
            this.m_showSecondaryCheckbox.UseVisualStyleBackColor = true;
            this.m_showSecondaryCheckbox.CheckedChanged += new System.EventHandler(this.ParametersDisplayCheckedChanged);
            // 
            // listingPanel
            // 
            this.listingPanel.AutoScroll = true;
            this.listingPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.listingPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.listingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listingPanel.Location = new System.Drawing.Point(0, 0);
            this.listingPanel.Name = "listingPanel";
            this.listingPanel.Size = new System.Drawing.Size(233, 377);
            this.listingPanel.TabIndex = 2;
            // 
            // m_futureCheckBox
            // 
            this.m_futureCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_futureCheckBox.AutoSize = true;
            this.m_futureCheckBox.Checked = true;
            this.m_futureCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_futureCheckBox.Location = new System.Drawing.Point(12, 8);
            this.m_futureCheckBox.Name = "m_futureCheckBox";
            this.m_futureCheckBox.Size = new System.Drawing.Size(99, 17);
            this.m_futureCheckBox.TabIndex = 1;
            this.m_futureCheckBox.Text = "Future Modules";
            this.m_futureCheckBox.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listingPanel);
            this.panel1.Controls.Add(this.rightPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 377);
            this.panel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel3.Controls.Add(this.m_showSecondaryCheckbox);
            this.panel3.Controls.Add(this.m_futureCheckBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 377);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(559, 31);
            this.panel3.TabIndex = 4;
            // 
            // fsUnitsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 444);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "fsUnitsDialog";
            this.Text = "UnitsDialog";
            this.Load += new System.EventHandler(this.UnitsDialogLoad);
            this.rightPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel listingPanel;
        private System.Windows.Forms.CheckBox m_futureCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox m_showSecondaryCheckbox;
        private fsUIControls.fsUnitsControl fsUnitsControl1;

    }
}