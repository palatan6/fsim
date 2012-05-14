namespace CyclonPlus
{
    partial class MainCyclonPlusForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelProjectMain = new System.Windows.Forms.Panel();
            this.fmDataGridProject = new fmDataGrid.fmDataGrid();
            this.ColumnProjCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelProjectTop = new System.Windows.Forms.Panel();
            this.buttonDelProject = new System.Windows.Forms.Button();
            this.buttonUndoProject = new System.Windows.Forms.Button();
            this.buttonSaveProject = new System.Windows.Forms.Button();
            this.buttonCreateProject = new System.Windows.Forms.Button();
            this.checkBoxByCheckingProjects = new System.Windows.Forms.CheckBox();
            this.panelSuspMain = new System.Windows.Forms.Panel();
            this.fmDataGridSusp = new fmDataGrid.fmDataGrid();
            this.ColumnSuspCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelSuspTop = new System.Windows.Forms.Panel();
            this.buttonDelSusp = new System.Windows.Forms.Button();
            this.buttonUndoSusp = new System.Windows.Forms.Button();
            this.buttonSaveSusp = new System.Windows.Forms.Button();
            this.buttonCreateSusp = new System.Windows.Forms.Button();
            this.checkBoxByCheckingSusp = new System.Windows.Forms.CheckBox();
            this.panelSeriesMain = new System.Windows.Forms.Panel();
            this.fmDataGridSeries = new fmDataGrid.fmDataGrid();
            this.ColumnSeriesCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnSusp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnProject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelSeriesTop = new System.Windows.Forms.Panel();
            this.buttonDelSeries = new System.Windows.Forms.Button();
            this.buttonUndoSeries = new System.Windows.Forms.Button();
            this.buttonSaveSeries = new System.Windows.Forms.Button();
            this.buttonCreateSeries = new System.Windows.Forms.Button();
            this.checkBoxByCheckingSeries = new System.Windows.Forms.CheckBox();
            this.panelSimulMain = new System.Windows.Forms.Panel();
            this.fmDataGridSimul = new fmDataGrid.fmDataGrid();
            this.panelSimulTop = new System.Windows.Forms.Panel();
            this.buttonDuplicateSeries = new System.Windows.Forms.Button();
            this.buttonDelSimul = new System.Windows.Forms.Button();
            this.buttonUndoSimul = new System.Windows.Forms.Button();
            this.buttonSaveSimul = new System.Windows.Forms.Button();
            this.buttonCreateSimul = new System.Windows.Forms.Button();
            this.checkBoxByCheckingSimul = new System.Windows.Forms.CheckBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.splitContainerTopTables = new System.Windows.Forms.SplitContainer();
            this.splitContainerProjSusp = new System.Windows.Forms.SplitContainer();
            this.hydrocycloneControl1 = new CalculatorModules.Hydrocyclone.HydrocycloneControl();
            this.panelProjectMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fmDataGridProject)).BeginInit();
            this.panelProjectTop.SuspendLayout();
            this.panelSuspMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fmDataGridSusp)).BeginInit();
            this.panelSuspTop.SuspendLayout();
            this.panelSeriesMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fmDataGridSeries)).BeginInit();
            this.panelSeriesTop.SuspendLayout();
            this.panelSimulMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fmDataGridSimul)).BeginInit();
            this.panelSimulTop.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.splitContainerTopTables.Panel1.SuspendLayout();
            this.splitContainerTopTables.Panel2.SuspendLayout();
            this.splitContainerTopTables.SuspendLayout();
            this.splitContainerProjSusp.Panel1.SuspendLayout();
            this.splitContainerProjSusp.Panel2.SuspendLayout();
            this.splitContainerProjSusp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelProjectMain
            // 
            this.panelProjectMain.Controls.Add(this.fmDataGridProject);
            this.panelProjectMain.Controls.Add(this.panelProjectTop);
            this.panelProjectMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProjectMain.Location = new System.Drawing.Point(0, 0);
            this.panelProjectMain.Name = "panelProjectMain";
            this.panelProjectMain.Size = new System.Drawing.Size(266, 190);
            this.panelProjectMain.TabIndex = 0;
            // 
            // fmDataGridProject
            // 
            this.fmDataGridProject.AllowUserToAddRows = false;
            this.fmDataGridProject.AllowUserToDeleteRows = false;
            this.fmDataGridProject.AllowUserToResizeRows = false;
            this.fmDataGridProject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fmDataGridProject.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnProjCheck});
            this.fmDataGridProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fmDataGridProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.fmDataGridProject.HighLightCurrentRow = false;
            this.fmDataGridProject.Location = new System.Drawing.Point(0, 39);
            this.fmDataGridProject.MultiSelect = false;
            this.fmDataGridProject.Name = "fmDataGridProject";
            this.fmDataGridProject.RowHeadersVisible = false;
            this.fmDataGridProject.RowTemplate.Height = 18;
            this.fmDataGridProject.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.fmDataGridProject.Size = new System.Drawing.Size(266, 151);
            this.fmDataGridProject.TabIndex = 1;
            this.fmDataGridProject.CellValueChangedByUser += new System.Windows.Forms.DataGridViewCellEventHandler(this.fmDataGridProject_CellValueChangedByUser);
            // 
            // ColumnProjCheck
            // 
            this.ColumnProjCheck.HeaderText = "";
            this.ColumnProjCheck.Name = "ColumnProjCheck";
            this.ColumnProjCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnProjCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnProjCheck.Width = 50;
            // 
            // panelProjectTop
            // 
            this.panelProjectTop.Controls.Add(this.buttonDelProject);
            this.panelProjectTop.Controls.Add(this.buttonUndoProject);
            this.panelProjectTop.Controls.Add(this.buttonSaveProject);
            this.panelProjectTop.Controls.Add(this.buttonCreateProject);
            this.panelProjectTop.Controls.Add(this.checkBoxByCheckingProjects);
            this.panelProjectTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProjectTop.Location = new System.Drawing.Point(0, 0);
            this.panelProjectTop.Name = "panelProjectTop";
            this.panelProjectTop.Size = new System.Drawing.Size(266, 39);
            this.panelProjectTop.TabIndex = 0;
            // 
            // buttonDelProject
            // 
            this.buttonDelProject.Location = new System.Drawing.Point(217, 7);
            this.buttonDelProject.Name = "buttonDelProject";
            this.buttonDelProject.Size = new System.Drawing.Size(33, 23);
            this.buttonDelProject.TabIndex = 4;
            this.buttonDelProject.Text = "dl";
            this.buttonDelProject.UseVisualStyleBackColor = true;
            this.buttonDelProject.Click += new System.EventHandler(this.buttonDelProject_Click);
            // 
            // buttonUndoProject
            // 
            this.buttonUndoProject.Location = new System.Drawing.Point(184, 7);
            this.buttonUndoProject.Name = "buttonUndoProject";
            this.buttonUndoProject.Size = new System.Drawing.Size(33, 23);
            this.buttonUndoProject.TabIndex = 3;
            this.buttonUndoProject.Text = "un";
            this.buttonUndoProject.UseVisualStyleBackColor = true;
            this.buttonUndoProject.Click += new System.EventHandler(this.buttonUndoProject_Click);
            // 
            // buttonSaveProject
            // 
            this.buttonSaveProject.Location = new System.Drawing.Point(151, 7);
            this.buttonSaveProject.Name = "buttonSaveProject";
            this.buttonSaveProject.Size = new System.Drawing.Size(33, 23);
            this.buttonSaveProject.TabIndex = 2;
            this.buttonSaveProject.Text = "sv";
            this.buttonSaveProject.UseVisualStyleBackColor = true;
            this.buttonSaveProject.Click += new System.EventHandler(this.buttonSaveProject_Click);
            // 
            // buttonCreateProject
            // 
            this.buttonCreateProject.Location = new System.Drawing.Point(118, 7);
            this.buttonCreateProject.Name = "buttonCreateProject";
            this.buttonCreateProject.Size = new System.Drawing.Size(33, 23);
            this.buttonCreateProject.TabIndex = 1;
            this.buttonCreateProject.Text = "cr";
            this.buttonCreateProject.UseVisualStyleBackColor = true;
            this.buttonCreateProject.Click += new System.EventHandler(this.buttonCreateProject_Click);
            // 
            // checkBoxByCheckingProjects
            // 
            this.checkBoxByCheckingProjects.AutoSize = true;
            this.checkBoxByCheckingProjects.Location = new System.Drawing.Point(3, 9);
            this.checkBoxByCheckingProjects.Name = "checkBoxByCheckingProjects";
            this.checkBoxByCheckingProjects.Size = new System.Drawing.Size(107, 21);
            this.checkBoxByCheckingProjects.TabIndex = 0;
            this.checkBoxByCheckingProjects.Text = "by Checking";
            this.checkBoxByCheckingProjects.UseVisualStyleBackColor = true;
            // 
            // panelSuspMain
            // 
            this.panelSuspMain.Controls.Add(this.fmDataGridSusp);
            this.panelSuspMain.Controls.Add(this.panelSuspTop);
            this.panelSuspMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSuspMain.Location = new System.Drawing.Point(0, 0);
            this.panelSuspMain.Name = "panelSuspMain";
            this.panelSuspMain.Size = new System.Drawing.Size(262, 190);
            this.panelSuspMain.TabIndex = 1;
            // 
            // fmDataGridSusp
            // 
            this.fmDataGridSusp.AllowUserToAddRows = false;
            this.fmDataGridSusp.AllowUserToDeleteRows = false;
            this.fmDataGridSusp.AllowUserToResizeRows = false;
            this.fmDataGridSusp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fmDataGridSusp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSuspCheck});
            this.fmDataGridSusp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fmDataGridSusp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.fmDataGridSusp.HighLightCurrentRow = false;
            this.fmDataGridSusp.Location = new System.Drawing.Point(0, 39);
            this.fmDataGridSusp.MultiSelect = false;
            this.fmDataGridSusp.Name = "fmDataGridSusp";
            this.fmDataGridSusp.RowHeadersVisible = false;
            this.fmDataGridSusp.RowTemplate.Height = 18;
            this.fmDataGridSusp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.fmDataGridSusp.Size = new System.Drawing.Size(262, 151);
            this.fmDataGridSusp.TabIndex = 1;
            this.fmDataGridSusp.CellValueChangedByUser += new System.Windows.Forms.DataGridViewCellEventHandler(this.fmDataGridSusp_CellValueChangedByUser);
            // 
            // ColumnSuspCheck
            // 
            this.ColumnSuspCheck.HeaderText = "";
            this.ColumnSuspCheck.Name = "ColumnSuspCheck";
            this.ColumnSuspCheck.Width = 50;
            // 
            // panelSuspTop
            // 
            this.panelSuspTop.Controls.Add(this.buttonDelSusp);
            this.panelSuspTop.Controls.Add(this.buttonUndoSusp);
            this.panelSuspTop.Controls.Add(this.buttonSaveSusp);
            this.panelSuspTop.Controls.Add(this.buttonCreateSusp);
            this.panelSuspTop.Controls.Add(this.checkBoxByCheckingSusp);
            this.panelSuspTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuspTop.Location = new System.Drawing.Point(0, 0);
            this.panelSuspTop.Name = "panelSuspTop";
            this.panelSuspTop.Size = new System.Drawing.Size(262, 39);
            this.panelSuspTop.TabIndex = 0;
            // 
            // buttonDelSusp
            // 
            this.buttonDelSusp.Location = new System.Drawing.Point(217, 7);
            this.buttonDelSusp.Name = "buttonDelSusp";
            this.buttonDelSusp.Size = new System.Drawing.Size(33, 23);
            this.buttonDelSusp.TabIndex = 4;
            this.buttonDelSusp.Text = "dl";
            this.buttonDelSusp.UseVisualStyleBackColor = true;
            this.buttonDelSusp.Click += new System.EventHandler(this.buttonDelSusp_Click);
            // 
            // buttonUndoSusp
            // 
            this.buttonUndoSusp.Location = new System.Drawing.Point(184, 7);
            this.buttonUndoSusp.Name = "buttonUndoSusp";
            this.buttonUndoSusp.Size = new System.Drawing.Size(33, 23);
            this.buttonUndoSusp.TabIndex = 3;
            this.buttonUndoSusp.Text = "un";
            this.buttonUndoSusp.UseVisualStyleBackColor = true;
            this.buttonUndoSusp.Click += new System.EventHandler(this.buttonUndoSusp_Click);
            // 
            // buttonSaveSusp
            // 
            this.buttonSaveSusp.Location = new System.Drawing.Point(151, 7);
            this.buttonSaveSusp.Name = "buttonSaveSusp";
            this.buttonSaveSusp.Size = new System.Drawing.Size(33, 23);
            this.buttonSaveSusp.TabIndex = 2;
            this.buttonSaveSusp.Text = "sv";
            this.buttonSaveSusp.UseVisualStyleBackColor = true;
            this.buttonSaveSusp.Click += new System.EventHandler(this.buttonSaveSusp_Click);
            // 
            // buttonCreateSusp
            // 
            this.buttonCreateSusp.Location = new System.Drawing.Point(118, 7);
            this.buttonCreateSusp.Name = "buttonCreateSusp";
            this.buttonCreateSusp.Size = new System.Drawing.Size(33, 23);
            this.buttonCreateSusp.TabIndex = 1;
            this.buttonCreateSusp.Text = "cr";
            this.buttonCreateSusp.UseVisualStyleBackColor = true;
            this.buttonCreateSusp.Click += new System.EventHandler(this.buttonCreateSusp_Click);
            // 
            // checkBoxByCheckingSusp
            // 
            this.checkBoxByCheckingSusp.AutoSize = true;
            this.checkBoxByCheckingSusp.Location = new System.Drawing.Point(3, 9);
            this.checkBoxByCheckingSusp.Name = "checkBoxByCheckingSusp";
            this.checkBoxByCheckingSusp.Size = new System.Drawing.Size(107, 21);
            this.checkBoxByCheckingSusp.TabIndex = 0;
            this.checkBoxByCheckingSusp.Text = "by Checking";
            this.checkBoxByCheckingSusp.UseVisualStyleBackColor = true;
            // 
            // panelSeriesMain
            // 
            this.panelSeriesMain.Controls.Add(this.fmDataGridSeries);
            this.panelSeriesMain.Controls.Add(this.panelSeriesTop);
            this.panelSeriesMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSeriesMain.Location = new System.Drawing.Point(0, 0);
            this.panelSeriesMain.Name = "panelSeriesMain";
            this.panelSeriesMain.Size = new System.Drawing.Size(262, 190);
            this.panelSeriesMain.TabIndex = 2;
            // 
            // fmDataGridSeries
            // 
            this.fmDataGridSeries.AllowUserToAddRows = false;
            this.fmDataGridSeries.AllowUserToDeleteRows = false;
            this.fmDataGridSeries.AllowUserToResizeRows = false;
            this.fmDataGridSeries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fmDataGridSeries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSeriesCheck,
            this.ColumnSusp,
            this.ColumnProject});
            this.fmDataGridSeries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fmDataGridSeries.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.fmDataGridSeries.HighLightCurrentRow = false;
            this.fmDataGridSeries.Location = new System.Drawing.Point(0, 39);
            this.fmDataGridSeries.MultiSelect = false;
            this.fmDataGridSeries.Name = "fmDataGridSeries";
            this.fmDataGridSeries.RowHeadersVisible = false;
            this.fmDataGridSeries.RowTemplate.Height = 18;
            this.fmDataGridSeries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.fmDataGridSeries.Size = new System.Drawing.Size(262, 151);
            this.fmDataGridSeries.TabIndex = 1;
            this.fmDataGridSeries.CellValueChangedByUser += new System.Windows.Forms.DataGridViewCellEventHandler(this.fmDataGridSeries_CellValueChangedByUser);
            // 
            // ColumnSeriesCheck
            // 
            this.ColumnSeriesCheck.HeaderText = "";
            this.ColumnSeriesCheck.Name = "ColumnSeriesCheck";
            this.ColumnSeriesCheck.Width = 50;
            // 
            // ColumnSusp
            // 
            this.ColumnSusp.HeaderText = "Suspension";
            this.ColumnSusp.Name = "ColumnSusp";
            this.ColumnSusp.ReadOnly = true;
            // 
            // ColumnProject
            // 
            this.ColumnProject.HeaderText = "Project";
            this.ColumnProject.Name = "ColumnProject";
            this.ColumnProject.ReadOnly = true;
            // 
            // panelSeriesTop
            // 
            this.panelSeriesTop.Controls.Add(this.buttonDelSeries);
            this.panelSeriesTop.Controls.Add(this.buttonUndoSeries);
            this.panelSeriesTop.Controls.Add(this.buttonSaveSeries);
            this.panelSeriesTop.Controls.Add(this.buttonCreateSeries);
            this.panelSeriesTop.Controls.Add(this.checkBoxByCheckingSeries);
            this.panelSeriesTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSeriesTop.Location = new System.Drawing.Point(0, 0);
            this.panelSeriesTop.Name = "panelSeriesTop";
            this.panelSeriesTop.Size = new System.Drawing.Size(262, 39);
            this.panelSeriesTop.TabIndex = 0;
            // 
            // buttonDelSeries
            // 
            this.buttonDelSeries.Location = new System.Drawing.Point(217, 7);
            this.buttonDelSeries.Name = "buttonDelSeries";
            this.buttonDelSeries.Size = new System.Drawing.Size(33, 23);
            this.buttonDelSeries.TabIndex = 4;
            this.buttonDelSeries.Text = "dl";
            this.buttonDelSeries.UseVisualStyleBackColor = true;
            this.buttonDelSeries.Click += new System.EventHandler(this.buttonDelSeries_Click);
            // 
            // buttonUndoSeries
            // 
            this.buttonUndoSeries.Location = new System.Drawing.Point(184, 7);
            this.buttonUndoSeries.Name = "buttonUndoSeries";
            this.buttonUndoSeries.Size = new System.Drawing.Size(33, 23);
            this.buttonUndoSeries.TabIndex = 3;
            this.buttonUndoSeries.Text = "un";
            this.buttonUndoSeries.UseVisualStyleBackColor = true;
            this.buttonUndoSeries.Click += new System.EventHandler(this.buttonUndoSeries_Click);
            // 
            // buttonSaveSeries
            // 
            this.buttonSaveSeries.Location = new System.Drawing.Point(151, 7);
            this.buttonSaveSeries.Name = "buttonSaveSeries";
            this.buttonSaveSeries.Size = new System.Drawing.Size(33, 23);
            this.buttonSaveSeries.TabIndex = 2;
            this.buttonSaveSeries.Text = "sv";
            this.buttonSaveSeries.UseVisualStyleBackColor = true;
            this.buttonSaveSeries.Click += new System.EventHandler(this.buttonSaveSeries_Click);
            // 
            // buttonCreateSeries
            // 
            this.buttonCreateSeries.Location = new System.Drawing.Point(118, 7);
            this.buttonCreateSeries.Name = "buttonCreateSeries";
            this.buttonCreateSeries.Size = new System.Drawing.Size(33, 23);
            this.buttonCreateSeries.TabIndex = 1;
            this.buttonCreateSeries.Text = "cr";
            this.buttonCreateSeries.UseVisualStyleBackColor = true;
            this.buttonCreateSeries.Click += new System.EventHandler(this.buttonCreateSeries_Click);
            // 
            // checkBoxByCheckingSeries
            // 
            this.checkBoxByCheckingSeries.AutoSize = true;
            this.checkBoxByCheckingSeries.Location = new System.Drawing.Point(3, 9);
            this.checkBoxByCheckingSeries.Name = "checkBoxByCheckingSeries";
            this.checkBoxByCheckingSeries.Size = new System.Drawing.Size(107, 21);
            this.checkBoxByCheckingSeries.TabIndex = 0;
            this.checkBoxByCheckingSeries.Text = "by Checking";
            this.checkBoxByCheckingSeries.UseVisualStyleBackColor = true;
            // 
            // panelSimulMain
            // 
            this.panelSimulMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSimulMain.Controls.Add(this.fmDataGridSimul);
            this.panelSimulMain.Controls.Add(this.panelSimulTop);
            this.panelSimulMain.Location = new System.Drawing.Point(15, 204);
            this.panelSimulMain.Name = "panelSimulMain";
            this.panelSimulMain.Size = new System.Drawing.Size(798, 135);
            this.panelSimulMain.TabIndex = 3;
            // 
            // fmDataGridSimul
            // 
            this.fmDataGridSimul.AllowUserToAddRows = false;
            this.fmDataGridSimul.AllowUserToDeleteRows = false;
            this.fmDataGridSimul.AllowUserToResizeRows = false;
            this.fmDataGridSimul.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fmDataGridSimul.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fmDataGridSimul.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.fmDataGridSimul.HighLightCurrentRow = false;
            this.fmDataGridSimul.Location = new System.Drawing.Point(0, 39);
            this.fmDataGridSimul.MultiSelect = false;
            this.fmDataGridSimul.Name = "fmDataGridSimul";
            this.fmDataGridSimul.RowHeadersVisible = false;
            this.fmDataGridSimul.RowTemplate.Height = 18;
            this.fmDataGridSimul.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.fmDataGridSimul.Size = new System.Drawing.Size(798, 96);
            this.fmDataGridSimul.TabIndex = 1;
            this.fmDataGridSimul.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.fmDataGridSimul_RowEnter);
            // 
            // panelSimulTop
            // 
            this.panelSimulTop.Controls.Add(this.buttonDuplicateSeries);
            this.panelSimulTop.Controls.Add(this.buttonDelSimul);
            this.panelSimulTop.Controls.Add(this.buttonUndoSimul);
            this.panelSimulTop.Controls.Add(this.buttonSaveSimul);
            this.panelSimulTop.Controls.Add(this.buttonCreateSimul);
            this.panelSimulTop.Controls.Add(this.checkBoxByCheckingSimul);
            this.panelSimulTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSimulTop.Location = new System.Drawing.Point(0, 0);
            this.panelSimulTop.Name = "panelSimulTop";
            this.panelSimulTop.Size = new System.Drawing.Size(798, 39);
            this.panelSimulTop.TabIndex = 0;
            // 
            // buttonDuplicateSeries
            // 
            this.buttonDuplicateSeries.Location = new System.Drawing.Point(198, 10);
            this.buttonDuplicateSeries.Name = "buttonDuplicateSeries";
            this.buttonDuplicateSeries.Size = new System.Drawing.Size(33, 23);
            this.buttonDuplicateSeries.TabIndex = 5;
            this.buttonDuplicateSeries.Text = "du";
            this.buttonDuplicateSeries.UseVisualStyleBackColor = true;
            this.buttonDuplicateSeries.Click += new System.EventHandler(this.buttonDuplicateSeries_Click);
            // 
            // buttonDelSimul
            // 
            this.buttonDelSimul.Location = new System.Drawing.Point(369, 10);
            this.buttonDelSimul.Name = "buttonDelSimul";
            this.buttonDelSimul.Size = new System.Drawing.Size(33, 23);
            this.buttonDelSimul.TabIndex = 4;
            this.buttonDelSimul.Text = "dl";
            this.buttonDelSimul.UseVisualStyleBackColor = true;
            this.buttonDelSimul.Click += new System.EventHandler(this.buttonDelSimul_Click);
            // 
            // buttonUndoSimul
            // 
            this.buttonUndoSimul.Location = new System.Drawing.Point(312, 10);
            this.buttonUndoSimul.Name = "buttonUndoSimul";
            this.buttonUndoSimul.Size = new System.Drawing.Size(33, 23);
            this.buttonUndoSimul.TabIndex = 3;
            this.buttonUndoSimul.Text = "un";
            this.buttonUndoSimul.UseVisualStyleBackColor = true;
            this.buttonUndoSimul.Click += new System.EventHandler(this.buttonUndoSimul_Click);
            // 
            // buttonSaveSimul
            // 
            this.buttonSaveSimul.Location = new System.Drawing.Point(255, 10);
            this.buttonSaveSimul.Name = "buttonSaveSimul";
            this.buttonSaveSimul.Size = new System.Drawing.Size(33, 23);
            this.buttonSaveSimul.TabIndex = 2;
            this.buttonSaveSimul.Text = "sv";
            this.buttonSaveSimul.UseVisualStyleBackColor = true;
            this.buttonSaveSimul.Click += new System.EventHandler(this.buttonSaveSimul_Click);
            // 
            // buttonCreateSimul
            // 
            this.buttonCreateSimul.Location = new System.Drawing.Point(141, 10);
            this.buttonCreateSimul.Name = "buttonCreateSimul";
            this.buttonCreateSimul.Size = new System.Drawing.Size(33, 23);
            this.buttonCreateSimul.TabIndex = 1;
            this.buttonCreateSimul.Text = "cr";
            this.buttonCreateSimul.UseVisualStyleBackColor = true;
            this.buttonCreateSimul.Click += new System.EventHandler(this.buttonCreateSimul_Click);
            // 
            // checkBoxByCheckingSimul
            // 
            this.checkBoxByCheckingSimul.AutoSize = true;
            this.checkBoxByCheckingSimul.Location = new System.Drawing.Point(3, 9);
            this.checkBoxByCheckingSimul.Name = "checkBoxByCheckingSimul";
            this.checkBoxByCheckingSimul.Size = new System.Drawing.Size(107, 21);
            this.checkBoxByCheckingSimul.TabIndex = 0;
            this.checkBoxByCheckingSimul.Text = "by Checking";
            this.checkBoxByCheckingSimul.UseVisualStyleBackColor = true;
            // 
            // panelTop
            // 
            this.panelTop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTop.Controls.Add(this.splitContainerTopTables);
            this.panelTop.Location = new System.Drawing.Point(12, 8);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(798, 190);
            this.panelTop.TabIndex = 8;
            // 
            // splitContainerTopTables
            // 
            this.splitContainerTopTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTopTables.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTopTables.Name = "splitContainerTopTables";
            // 
            // splitContainerTopTables.Panel1
            // 
            this.splitContainerTopTables.Panel1.Controls.Add(this.splitContainerProjSusp);
            // 
            // splitContainerTopTables.Panel2
            // 
            this.splitContainerTopTables.Panel2.Controls.Add(this.panelSeriesMain);
            this.splitContainerTopTables.Size = new System.Drawing.Size(798, 190);
            this.splitContainerTopTables.SplitterDistance = 532;
            this.splitContainerTopTables.TabIndex = 14;
            // 
            // splitContainerProjSusp
            // 
            this.splitContainerProjSusp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerProjSusp.Location = new System.Drawing.Point(0, 0);
            this.splitContainerProjSusp.Name = "splitContainerProjSusp";
            // 
            // splitContainerProjSusp.Panel1
            // 
            this.splitContainerProjSusp.Panel1.Controls.Add(this.panelProjectMain);
            // 
            // splitContainerProjSusp.Panel2
            // 
            this.splitContainerProjSusp.Panel2.Controls.Add(this.panelSuspMain);
            this.splitContainerProjSusp.Size = new System.Drawing.Size(532, 190);
            this.splitContainerProjSusp.SplitterDistance = 266;
            this.splitContainerProjSusp.TabIndex = 12;
            // 
            // hydrocycloneControl1
            // 
            this.hydrocycloneControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hydrocycloneControl1.Location = new System.Drawing.Point(12, 344);
            this.hydrocycloneControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.hydrocycloneControl1.Name = "hydrocycloneControl1";
            this.hydrocycloneControl1.Size = new System.Drawing.Size(798, 475);
            this.hydrocycloneControl1.TabIndex = 13;
            // 
            // MainCyclonPlusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1386, 747);
            this.Controls.Add(this.hydrocycloneControl1);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelSimulMain);
            this.Name = "MainCyclonPlusForm";
            this.Text = "CyclonPlus";
            this.Load += new System.EventHandler(this.MainCyclonPlusForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainCyclonPlusForm_FormClosing);
            this.MaximumSizeChanged += new System.EventHandler(this.MainCyclonPlusForm_MaximumSizeChanged);
            this.panelProjectMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fmDataGridProject)).EndInit();
            this.panelProjectTop.ResumeLayout(false);
            this.panelProjectTop.PerformLayout();
            this.panelSuspMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fmDataGridSusp)).EndInit();
            this.panelSuspTop.ResumeLayout(false);
            this.panelSuspTop.PerformLayout();
            this.panelSeriesMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fmDataGridSeries)).EndInit();
            this.panelSeriesTop.ResumeLayout(false);
            this.panelSeriesTop.PerformLayout();
            this.panelSimulMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fmDataGridSimul)).EndInit();
            this.panelSimulTop.ResumeLayout(false);
            this.panelSimulTop.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.splitContainerTopTables.Panel1.ResumeLayout(false);
            this.splitContainerTopTables.Panel2.ResumeLayout(false);
            this.splitContainerTopTables.ResumeLayout(false);
            this.splitContainerProjSusp.Panel1.ResumeLayout(false);
            this.splitContainerProjSusp.Panel2.ResumeLayout(false);
            this.splitContainerProjSusp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelProjectMain;
        private fmDataGrid.fmDataGrid fmDataGridProject;
        private System.Windows.Forms.Panel panelProjectTop;
        private System.Windows.Forms.Button buttonDelProject;
        private System.Windows.Forms.Button buttonUndoProject;
        private System.Windows.Forms.Button buttonSaveProject;
        private System.Windows.Forms.Button buttonCreateProject;
        private System.Windows.Forms.CheckBox checkBoxByCheckingProjects;
        private System.Windows.Forms.Panel panelSuspMain;
        private fmDataGrid.fmDataGrid fmDataGridSusp;
        private System.Windows.Forms.Panel panelSuspTop;
        private System.Windows.Forms.Button buttonDelSusp;
        private System.Windows.Forms.Button buttonUndoSusp;
        private System.Windows.Forms.Button buttonSaveSusp;
        private System.Windows.Forms.Button buttonCreateSusp;
        private System.Windows.Forms.CheckBox checkBoxByCheckingSusp;
        private System.Windows.Forms.Panel panelSeriesMain;
        private fmDataGrid.fmDataGrid fmDataGridSeries;
        private System.Windows.Forms.Panel panelSeriesTop;
        private System.Windows.Forms.Button buttonDelSeries;
        private System.Windows.Forms.Button buttonUndoSeries;
        private System.Windows.Forms.Button buttonSaveSeries;
        private System.Windows.Forms.Button buttonCreateSeries;
        private System.Windows.Forms.CheckBox checkBoxByCheckingSeries;
        private System.Windows.Forms.Panel panelSimulMain;
        private fmDataGrid.fmDataGrid fmDataGridSimul;
        private System.Windows.Forms.Panel panelSimulTop;
        private System.Windows.Forms.Button buttonDelSimul;
        private System.Windows.Forms.Button buttonUndoSimul;
        private System.Windows.Forms.Button buttonSaveSimul;
        private System.Windows.Forms.Button buttonCreateSimul;
        private System.Windows.Forms.CheckBox checkBoxByCheckingSimul;
        private System.Windows.Forms.Button buttonDuplicateSeries;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnProjCheck;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSuspCheck;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSeriesCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSusp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnProject;
        private System.Windows.Forms.SplitContainer splitContainerProjSusp;
        private CalculatorModules.Hydrocyclone.HydrocycloneControl hydrocycloneControl1;
        private System.Windows.Forms.SplitContainer splitContainerTopTables;
    }
}

