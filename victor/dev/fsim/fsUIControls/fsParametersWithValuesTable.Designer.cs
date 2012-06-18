namespace fsUIControls
{
    partial class fsParametersWithValuesTable
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
            this.fmDataGrid1 = new fmDataGrid.fmDataGrid();
            this.ParametersColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValuesColumn = new fmDataGrid.fmDataGridViewNumericalTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.fmDataGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // fmDataGrid1
            // 
            this.fmDataGrid1.AllowUserToAddRows = false;
            this.fmDataGrid1.AllowUserToDeleteRows = false;
            this.fmDataGrid1.AllowUserToResizeRows = false;
            this.fmDataGrid1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.fmDataGrid1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.fmDataGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fmDataGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParametersColumn,
            this.UnitsColumn,
            this.ValuesColumn});
            this.fmDataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fmDataGrid1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.fmDataGrid1.HighLightCurrentRow = false;
            this.fmDataGrid1.Location = new System.Drawing.Point(0, 0);
            this.fmDataGrid1.Name = "fmDataGrid1";
            this.fmDataGrid1.RowHeadersVisible = false;
            this.fmDataGrid1.RowTemplate.Height = 18;
            this.fmDataGrid1.Size = new System.Drawing.Size(172, 199);
            this.fmDataGrid1.TabIndex = 0;
            // 
            // ParametersColumn
            // 
            this.ParametersColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParametersColumn.FillWeight = 75F;
            this.ParametersColumn.HeaderText = "Parameter";
            this.ParametersColumn.Name = "ParametersColumn";
            this.ParametersColumn.ReadOnly = true;
            this.ParametersColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UnitsColumn
            // 
            this.UnitsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnitsColumn.FillWeight = 75F;
            this.UnitsColumn.HeaderText = "Units";
            this.UnitsColumn.Name = "UnitsColumn";
            this.UnitsColumn.ReadOnly = true;
            this.UnitsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ValuesColumn
            // 
            this.ValuesColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ValuesColumn.HeaderText = "Value";
            this.ValuesColumn.Name = "ValuesColumn";
            // 
            // fsParametersWithValuesTable
            // 
            this.Controls.Add(this.fmDataGrid1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "fsParametersWithValuesTable";
            this.Size = new System.Drawing.Size(172, 199);
            ((System.ComponentModel.ISupportInitialize)(this.fmDataGrid1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private fmDataGrid.fmDataGrid fmDataGrid1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParametersColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitsColumn;
        private fmDataGrid.fmDataGridViewNumericalTextBoxColumn ValuesColumn;
    }
}
