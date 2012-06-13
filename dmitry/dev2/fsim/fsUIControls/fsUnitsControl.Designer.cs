namespace fsUIControls
{
    partial class fsUnitsControl
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
            this.rightPanel = new System.Windows.Forms.Panel();
            this.unitsPanel = new System.Windows.Forms.Panel();
            this.shemePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.schemeBox = new System.Windows.Forms.ComboBox();
            this.rightPanel.SuspendLayout();
            this.shemePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // rightPanel
            // 
            this.rightPanel.AutoScroll = true;
            this.rightPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.rightPanel.Controls.Add(this.unitsPanel);
            this.rightPanel.Controls.Add(this.shemePanel);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(0, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(293, 232);
            this.rightPanel.TabIndex = 1;
            // 
            // unitsPanel
            // 
            this.unitsPanel.AutoScroll = true;
            this.unitsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unitsPanel.Location = new System.Drawing.Point(0, 53);
            this.unitsPanel.Name = "unitsPanel";
            this.unitsPanel.Size = new System.Drawing.Size(293, 179);
            this.unitsPanel.TabIndex = 0;
            // 
            // shemePanel
            // 
            this.shemePanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.shemePanel.Controls.Add(this.label1);
            this.shemePanel.Controls.Add(this.schemeBox);
            this.shemePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.shemePanel.Location = new System.Drawing.Point(0, 0);
            this.shemePanel.Name = "shemePanel";
            this.shemePanel.Size = new System.Drawing.Size(293, 53);
            this.shemePanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Units Scheme:";
            // 
            // schemeBox
            // 
            this.schemeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.schemeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.schemeBox.FormattingEnabled = true;
            this.schemeBox.Location = new System.Drawing.Point(112, 20);
            this.schemeBox.Name = "schemeBox";
            this.schemeBox.Size = new System.Drawing.Size(178, 21);
            this.schemeBox.TabIndex = 0;
            this.schemeBox.SelectedIndexChanged += new System.EventHandler(this.SchemeBoxSelectedIndexChanged);
            // 
            // fsUnitsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rightPanel);
            this.Name = "fsUnitsControl";
            this.Size = new System.Drawing.Size(293, 232);
            this.Load += new System.EventHandler(this.UnitsDialogLoad);
            this.rightPanel.ResumeLayout(false);
            this.shemePanel.ResumeLayout(false);
            this.shemePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel rightPanel;
        private System.Windows.Forms.Panel unitsPanel;
        private System.Windows.Forms.Panel shemePanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox schemeBox;
    }
}
