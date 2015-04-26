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
            this.components = new System.ComponentModel.Container();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.unitsPanel = new System.Windows.Forms.Panel();
            this.shemePanel = new System.Windows.Forms.Panel();
            this.splitButtonMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.schemeBox = new System.Windows.Forms.ComboBox();
            this.btSaveDefaultForButton = new ExoticControls.SplitButton();
            this.showUsUnitsCheckbox = new System.Windows.Forms.CheckBox();
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
            this.rightPanel.Size = new System.Drawing.Size(662, 464);
            this.rightPanel.TabIndex = 1;
            // 
            // unitsPanel
            // 
            this.unitsPanel.AutoScroll = true;
            this.unitsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.unitsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unitsPanel.Location = new System.Drawing.Point(0, 0);
            this.unitsPanel.Name = "unitsPanel";
            this.unitsPanel.Size = new System.Drawing.Size(662, 411);
            this.unitsPanel.TabIndex = 0;
            // 
            // shemePanel
            // 
            this.shemePanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.shemePanel.Controls.Add(this.showUsUnitsCheckbox);
            this.shemePanel.Controls.Add(this.btSaveDefaultForButton);
            this.shemePanel.Controls.Add(this.label1);
            this.shemePanel.Controls.Add(this.schemeBox);
            this.shemePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.shemePanel.Location = new System.Drawing.Point(0, 411);
            this.shemePanel.Name = "shemePanel";
            this.shemePanel.Size = new System.Drawing.Size(662, 53);
            this.shemePanel.TabIndex = 1;
            // 
            // splitButtonMenu
            // 
            this.splitButtonMenu.Name = "splitButtonMenu";
            this.splitButtonMenu.Size = new System.Drawing.Size(61, 4);
            this.splitButtonMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.splitButtonMenu_ItemClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Units Scheme:";
            // 
            // schemeBox
            // 
            this.schemeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.schemeBox.FormattingEnabled = true;
            this.schemeBox.Location = new System.Drawing.Point(91, 18);
            this.schemeBox.Name = "schemeBox";
            this.schemeBox.Size = new System.Drawing.Size(108, 21);
            this.schemeBox.TabIndex = 0;
            this.schemeBox.SelectedIndexChanged += new System.EventHandler(this.SchemeBoxSelectedIndexChanged);
            // 
            // btSaveDefaultForButton
            // 
            this.btSaveDefaultForButton.AlwaysDropDown = true;
            this.btSaveDefaultForButton.ContextMenuStrip = this.splitButtonMenu;
            this.btSaveDefaultForButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btSaveDefaultForButton.ImageKey = "Normal";
            this.btSaveDefaultForButton.Location = new System.Drawing.Point(208, 18);
            this.btSaveDefaultForButton.Name = "btSaveDefaultForButton";
            this.btSaveDefaultForButton.Size = new System.Drawing.Size(108, 21);
            this.btSaveDefaultForButton.TabIndex = 2;
            this.btSaveDefaultForButton.Text = "Save Default for:";
            this.btSaveDefaultForButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btSaveDefaultForButton.UseVisualStyleBackColor = true;
            // 
            // showUsUnitsCheckbox
            // 
            this.showUsUnitsCheckbox.AutoSize = true;
            this.showUsUnitsCheckbox.Location = new System.Drawing.Point(325, 20);
            this.showUsUnitsCheckbox.Name = "showUsUnitsCheckbox";
            this.showUsUnitsCheckbox.Size = new System.Drawing.Size(96, 17);
            this.showUsUnitsCheckbox.TabIndex = 3;
            this.showUsUnitsCheckbox.Text = "Show US units";
            this.showUsUnitsCheckbox.UseVisualStyleBackColor = true;
            this.showUsUnitsCheckbox.CheckedChanged += new System.EventHandler(this.showUsUnitsCheckbox_CheckedChanged);
            // 
            // fsUnitsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rightPanel);
            this.Name = "fsUnitsControl";
            this.Size = new System.Drawing.Size(662, 464);
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
        private System.Windows.Forms.ContextMenuStrip splitButtonMenu;
        private ExoticControls.SplitButton btSaveDefaultForButton;
        private System.Windows.Forms.CheckBox showUsUnitsCheckbox;
    }
}
