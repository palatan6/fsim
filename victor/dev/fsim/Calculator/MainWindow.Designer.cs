namespace Calculator
{
    partial class fsMainWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.machineTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.precisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.addModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.modulesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(715, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowTilesToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // windowTilesToolStripMenuItem
            // 
            this.windowTilesToolStripMenuItem.Name = "windowTilesToolStripMenuItem";
            this.windowTilesToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.windowTilesToolStripMenuItem.Text = "Window Tiles";
            this.windowTilesToolStripMenuItem.Click += new System.EventHandler(this.WindowTilesToolStripMenuItemClick);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unitsToolStripMenuItem,
            this.machineTypeToolStripMenuItem,
            this.showHideParametersToolStripMenuItem,
            this.precisionToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // unitsToolStripMenuItem
            // 
            this.unitsToolStripMenuItem.Name = "unitsToolStripMenuItem";
            this.unitsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.unitsToolStripMenuItem.Text = "&Units";
            this.unitsToolStripMenuItem.Click += new System.EventHandler(this.UnitsToolStripMenuItemClick);
            // 
            // machineTypeToolStripMenuItem
            // 
            this.machineTypeToolStripMenuItem.Name = "machineTypeToolStripMenuItem";
            this.machineTypeToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.machineTypeToolStripMenuItem.Text = "&Machine Type";
            this.machineTypeToolStripMenuItem.Click += new System.EventHandler(this.MachineTypeToolStripMenuItemClick);
            // 
            // showHideParametersToolStripMenuItem
            // 
            this.showHideParametersToolStripMenuItem.Name = "showHideParametersToolStripMenuItem";
            this.showHideParametersToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.showHideParametersToolStripMenuItem.Text = "Show/&Hide Parameters";
            this.showHideParametersToolStripMenuItem.Click += new System.EventHandler(this.showHideParametersToolStripMenuItem_Click);
            // 
            // precisionToolStripMenuItem
            // 
            this.precisionToolStripMenuItem.Name = "precisionToolStripMenuItem";
            this.precisionToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.precisionToolStripMenuItem.Text = "&Precision";
            this.precisionToolStripMenuItem.Click += new System.EventHandler(this.precisionToolStripMenuItem_Click);
            // 
            // modulesToolStripMenuItem
            // 
            this.modulesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.addModuleToolStripMenuItem});
            this.modulesToolStripMenuItem.Name = "modulesToolStripMenuItem";
            this.modulesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.modulesToolStripMenuItem.Text = "Modules";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(146, 6);
            // 
            // addModuleToolStripMenuItem
            // 
            this.addModuleToolStripMenuItem.Name = "addModuleToolStripMenuItem";
            this.addModuleToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.addModuleToolStripMenuItem.Text = "Add Module...";
            this.addModuleToolStripMenuItem.Click += new System.EventHandler(this.addModuleToolStripMenuItem_Click);
            // 
            // fsMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 372);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fsMainWindow";
            this.Text = "SEPEX";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainWindowLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowTilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem machineTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideParametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem precisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addModuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;

    }
}

