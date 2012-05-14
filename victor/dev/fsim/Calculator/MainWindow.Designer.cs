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
            this.panel1 = new System.Windows.Forms.Panel();
            this.windowsDataGrid = new fmDataGrid.fmDataGrid();
            this.WindowNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.addModuleButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.windowsDataGrid)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.optionsToolStripMenuItem});
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowTilesToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // windowTilesToolStripMenuItem
            // 
            this.windowTilesToolStripMenuItem.Name = "windowTilesToolStripMenuItem";
            this.windowTilesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
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
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // unitsToolStripMenuItem
            // 
            this.unitsToolStripMenuItem.Name = "unitsToolStripMenuItem";
            this.unitsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.unitsToolStripMenuItem.Text = "&Units";
            this.unitsToolStripMenuItem.Click += new System.EventHandler(this.UnitsToolStripMenuItemClick);
            // 
            // machineTypeToolStripMenuItem
            // 
            this.machineTypeToolStripMenuItem.Name = "machineTypeToolStripMenuItem";
            this.machineTypeToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.machineTypeToolStripMenuItem.Text = "&Machine Type";
            this.machineTypeToolStripMenuItem.Click += new System.EventHandler(this.MachineTypeToolStripMenuItemClick);
            // 
            // showHideParametersToolStripMenuItem
            // 
            this.showHideParametersToolStripMenuItem.Name = "showHideParametersToolStripMenuItem";
            this.showHideParametersToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.showHideParametersToolStripMenuItem.Text = "Show/&Hide Parameters";
            this.showHideParametersToolStripMenuItem.Click += new System.EventHandler(this.showHideParametersToolStripMenuItem_Click);
            // 
            // precisionToolStripMenuItem
            // 
            this.precisionToolStripMenuItem.Name = "precisionToolStripMenuItem";
            this.precisionToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.precisionToolStripMenuItem.Text = "&Precision";
            this.precisionToolStripMenuItem.Click += new System.EventHandler(this.precisionToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.windowsDataGrid);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(170, 348);
            this.panel1.TabIndex = 3;
            // 
            // windowsDataGrid
            // 
            this.windowsDataGrid.AllowUserToAddRows = false;
            this.windowsDataGrid.AllowUserToDeleteRows = false;
            this.windowsDataGrid.AllowUserToResizeRows = false;
            this.windowsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.windowsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WindowNameColumn});
            this.windowsDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windowsDataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.windowsDataGrid.HighLightCurrentRow = false;
            this.windowsDataGrid.Location = new System.Drawing.Point(0, 72);
            this.windowsDataGrid.Name = "windowsDataGrid";
            this.windowsDataGrid.ReadOnly = true;
            this.windowsDataGrid.RowHeadersVisible = false;
            this.windowsDataGrid.RowTemplate.Height = 18;
            this.windowsDataGrid.Size = new System.Drawing.Size(170, 276);
            this.windowsDataGrid.TabIndex = 0;
            this.windowsDataGrid.CurrentCellChanged += new System.EventHandler(this.WindowsDataGridCurrentCellChanged);
            // 
            // WindowNameColumn
            // 
            this.WindowNameColumn.HeaderText = "Window";
            this.WindowNameColumn.Name = "WindowNameColumn";
            this.WindowNameColumn.ReadOnly = true;
            this.WindowNameColumn.Width = 160;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.addModuleButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(170, 72);
            this.panel2.TabIndex = 1;
            // 
            // addModuleButton
            // 
            this.addModuleButton.Location = new System.Drawing.Point(29, 19);
            this.addModuleButton.Name = "addModuleButton";
            this.addModuleButton.Size = new System.Drawing.Size(110, 34);
            this.addModuleButton.TabIndex = 0;
            this.addModuleButton.Text = "Add Module";
            this.addModuleButton.UseVisualStyleBackColor = true;
            this.addModuleButton.Click += new System.EventHandler(this.AddModuleButtonClick);
            // 
            // fsMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 372);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fsMainWindow";
            this.Text = "Calculator";
            this.Load += new System.EventHandler(this.MainWindowLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.windowsDataGrid)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private fmDataGrid.fmDataGrid windowsDataGrid;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button addModuleButton;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowTilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn WindowNameColumn;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem machineTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideParametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem precisionToolStripMenuItem;

    }
}

