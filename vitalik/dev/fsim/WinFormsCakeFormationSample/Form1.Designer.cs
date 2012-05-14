namespace WinFormsCakeFormationSample
{
    partial class Form1
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
            this.materialDataGroupBox = new System.Windows.Forms.GroupBox();
            this.MaterialParametersDataGrid = new fmDataGrid.fmDataGrid();
            this.MaterialParameterNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialParameterValue = new fmDataGrid.fmDataGridViewNumericalTextBoxColumn();
            this.cakeFormationGroupBox = new System.Windows.Forms.GroupBox();
            this.CakeFormationDataGrid = new fmDataGrid.fmDataGrid();
            this.CakeFormationParameterNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CakeFormationParameterValueColumn = new fmDataGrid.fmDataGridViewNumericalTextBoxColumn();
            this.useMultiThreadingFlag = new System.Windows.Forms.CheckBox();
            this.errorMessageTextBox = new System.Windows.Forms.TextBox();
            this.fsLabeledProgressBar1 = new UpdateHandler.fsLabeledProgressBar();
            this.deliquoringBox = new System.Windows.Forms.GroupBox();
            this.debugBox = new System.Windows.Forms.GroupBox();
            this.DeliquoringDataGrid = new fmDataGrid.fmDataGrid();
            this.deliquoringParameterColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deliquoringValueColumn = new fmDataGrid.fmDataGridViewNumericalTextBoxColumn();
            this.materialDataGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaterialParametersDataGrid)).BeginInit();
            this.cakeFormationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CakeFormationDataGrid)).BeginInit();
            this.deliquoringBox.SuspendLayout();
            this.debugBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeliquoringDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // materialDataGroupBox
            // 
            this.materialDataGroupBox.Controls.Add(this.MaterialParametersDataGrid);
            this.materialDataGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.materialDataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.materialDataGroupBox.Name = "materialDataGroupBox";
            this.materialDataGroupBox.Size = new System.Drawing.Size(196, 559);
            this.materialDataGroupBox.TabIndex = 0;
            this.materialDataGroupBox.TabStop = false;
            this.materialDataGroupBox.Text = "Material Data";
            // 
            // MaterialParametersDataGrid
            // 
            this.MaterialParametersDataGrid.AllowUserToAddRows = false;
            this.MaterialParametersDataGrid.AllowUserToResizeRows = false;
            this.MaterialParametersDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MaterialParametersDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaterialParameterNameColumn,
            this.MaterialParameterValue});
            this.MaterialParametersDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaterialParametersDataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.MaterialParametersDataGrid.HighLightCurrentRow = false;
            this.MaterialParametersDataGrid.Location = new System.Drawing.Point(3, 16);
            this.MaterialParametersDataGrid.Name = "MaterialParametersDataGrid";
            this.MaterialParametersDataGrid.RowHeadersVisible = false;
            this.MaterialParametersDataGrid.RowTemplate.Height = 18;
            this.MaterialParametersDataGrid.Size = new System.Drawing.Size(190, 540);
            this.MaterialParametersDataGrid.TabIndex = 0;
            this.MaterialParametersDataGrid.CellValueChangedByUser += new System.Windows.Forms.DataGridViewCellEventHandler(this.MaterialParametersDataGrid_CellValueChangedByUser);
            // 
            // MaterialParameterNameColumn
            // 
            this.MaterialParameterNameColumn.HeaderText = "Parameter";
            this.MaterialParameterNameColumn.Name = "MaterialParameterNameColumn";
            this.MaterialParameterNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MaterialParameterValue
            // 
            this.MaterialParameterValue.HeaderText = "Value";
            this.MaterialParameterValue.Name = "MaterialParameterValue";
            this.MaterialParameterValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MaterialParameterValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.MaterialParameterValue.Width = 60;
            // 
            // cakeFormationGroupBox
            // 
            this.cakeFormationGroupBox.Controls.Add(this.CakeFormationDataGrid);
            this.cakeFormationGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.cakeFormationGroupBox.Location = new System.Drawing.Point(196, 0);
            this.cakeFormationGroupBox.Name = "cakeFormationGroupBox";
            this.cakeFormationGroupBox.Size = new System.Drawing.Size(196, 559);
            this.cakeFormationGroupBox.TabIndex = 1;
            this.cakeFormationGroupBox.TabStop = false;
            this.cakeFormationGroupBox.Text = "Cake Formation";
            // 
            // CakeFormationDataGrid
            // 
            this.CakeFormationDataGrid.AllowUserToAddRows = false;
            this.CakeFormationDataGrid.AllowUserToResizeRows = false;
            this.CakeFormationDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CakeFormationDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CakeFormationParameterNameColumn,
            this.CakeFormationParameterValueColumn});
            this.CakeFormationDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CakeFormationDataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.CakeFormationDataGrid.HighLightCurrentRow = false;
            this.CakeFormationDataGrid.Location = new System.Drawing.Point(3, 16);
            this.CakeFormationDataGrid.Name = "CakeFormationDataGrid";
            this.CakeFormationDataGrid.RowHeadersVisible = false;
            this.CakeFormationDataGrid.RowTemplate.Height = 18;
            this.CakeFormationDataGrid.Size = new System.Drawing.Size(190, 540);
            this.CakeFormationDataGrid.TabIndex = 0;
            this.CakeFormationDataGrid.CellValueChangedByUser += new System.Windows.Forms.DataGridViewCellEventHandler(this.CakeFormationDataGrid_CellValueChangedByUser);
            // 
            // CakeFormationParameterNameColumn
            // 
            this.CakeFormationParameterNameColumn.HeaderText = "Parameter";
            this.CakeFormationParameterNameColumn.Name = "CakeFormationParameterNameColumn";
            // 
            // CakeFormationParameterValueColumn
            // 
            this.CakeFormationParameterValueColumn.HeaderText = "Value";
            this.CakeFormationParameterValueColumn.Name = "CakeFormationParameterValueColumn";
            this.CakeFormationParameterValueColumn.Width = 60;
            // 
            // useMultiThreadingFlag
            // 
            this.useMultiThreadingFlag.AutoSize = true;
            this.useMultiThreadingFlag.Checked = true;
            this.useMultiThreadingFlag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useMultiThreadingFlag.Location = new System.Drawing.Point(6, 19);
            this.useMultiThreadingFlag.Name = "useMultiThreadingFlag";
            this.useMultiThreadingFlag.Size = new System.Drawing.Size(118, 17);
            this.useMultiThreadingFlag.TabIndex = 3;
            this.useMultiThreadingFlag.Text = "Use MultiThreading";
            this.useMultiThreadingFlag.UseVisualStyleBackColor = true;
            // 
            // errorMessageTextBox
            // 
            this.errorMessageTextBox.Location = new System.Drawing.Point(6, 86);
            this.errorMessageTextBox.Multiline = true;
            this.errorMessageTextBox.Name = "errorMessageTextBox";
            this.errorMessageTextBox.Size = new System.Drawing.Size(146, 467);
            this.errorMessageTextBox.TabIndex = 4;
            this.errorMessageTextBox.Visible = false;
            // 
            // fsLabeledProgressBar1
            // 
            this.fsLabeledProgressBar1.Location = new System.Drawing.Point(6, 49);
            this.fsLabeledProgressBar1.Maximum = 100;
            this.fsLabeledProgressBar1.Name = "fsLabeledProgressBar1";
            this.fsLabeledProgressBar1.Size = new System.Drawing.Size(121, 31);
            this.fsLabeledProgressBar1.TabIndex = 5;
            this.fsLabeledProgressBar1.Value = 0;
            // 
            // deliquoringBox
            // 
            this.deliquoringBox.Controls.Add(this.DeliquoringDataGrid);
            this.deliquoringBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.deliquoringBox.Location = new System.Drawing.Point(392, 0);
            this.deliquoringBox.Name = "deliquoringBox";
            this.deliquoringBox.Size = new System.Drawing.Size(196, 559);
            this.deliquoringBox.TabIndex = 6;
            this.deliquoringBox.TabStop = false;
            this.deliquoringBox.Text = "Deliquoring";
            // 
            // debugBox
            // 
            this.debugBox.Controls.Add(this.errorMessageTextBox);
            this.debugBox.Controls.Add(this.fsLabeledProgressBar1);
            this.debugBox.Controls.Add(this.useMultiThreadingFlag);
            this.debugBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.debugBox.Location = new System.Drawing.Point(733, 0);
            this.debugBox.Name = "debugBox";
            this.debugBox.Size = new System.Drawing.Size(162, 559);
            this.debugBox.TabIndex = 7;
            this.debugBox.TabStop = false;
            this.debugBox.Text = "Debug";
            // 
            // DeliquoringDataGrid
            // 
            this.DeliquoringDataGrid.AllowUserToAddRows = false;
            this.DeliquoringDataGrid.AllowUserToDeleteRows = false;
            this.DeliquoringDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DeliquoringDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.deliquoringParameterColumn,
            this.deliquoringValueColumn});
            this.DeliquoringDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeliquoringDataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.DeliquoringDataGrid.HighLightCurrentRow = false;
            this.DeliquoringDataGrid.Location = new System.Drawing.Point(3, 16);
            this.DeliquoringDataGrid.Name = "DeliquoringDataGrid";
            this.DeliquoringDataGrid.RowHeadersVisible = false;
            this.DeliquoringDataGrid.RowTemplate.Height = 18;
            this.DeliquoringDataGrid.Size = new System.Drawing.Size(190, 540);
            this.DeliquoringDataGrid.TabIndex = 0;
            // 
            // deliquoringParameterColumn
            // 
            this.deliquoringParameterColumn.HeaderText = "Parameter";
            this.deliquoringParameterColumn.Name = "deliquoringParameterColumn";
            // 
            // deliquoringValueColumn
            // 
            this.deliquoringValueColumn.HeaderText = "Value";
            this.deliquoringValueColumn.Name = "deliquoringValueColumn";
            this.deliquoringValueColumn.Width = 60;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 559);
            this.Controls.Add(this.debugBox);
            this.Controls.Add(this.deliquoringBox);
            this.Controls.Add(this.cakeFormationGroupBox);
            this.Controls.Add(this.materialDataGroupBox);
            this.Name = "Form1";
            this.Text = "Cake Fromation Sample";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.materialDataGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MaterialParametersDataGrid)).EndInit();
            this.cakeFormationGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CakeFormationDataGrid)).EndInit();
            this.deliquoringBox.ResumeLayout(false);
            this.debugBox.ResumeLayout(false);
            this.debugBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DeliquoringDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox materialDataGroupBox;
        private System.Windows.Forms.GroupBox cakeFormationGroupBox;
        private fmDataGrid.fmDataGrid MaterialParametersDataGrid;
        private fmDataGrid.fmDataGrid CakeFormationDataGrid;
        private System.Windows.Forms.CheckBox useMultiThreadingFlag;
        private System.Windows.Forms.TextBox errorMessageTextBox;
        private UpdateHandler.fsLabeledProgressBar fsLabeledProgressBar1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialParameterNameColumn;
        private fmDataGrid.fmDataGridViewNumericalTextBoxColumn MaterialParameterValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn CakeFormationParameterNameColumn;
        private fmDataGrid.fmDataGridViewNumericalTextBoxColumn CakeFormationParameterValueColumn;
        private System.Windows.Forms.GroupBox deliquoringBox;
        private fmDataGrid.fmDataGrid DeliquoringDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn deliquoringParameterColumn;
        private fmDataGrid.fmDataGridViewNumericalTextBoxColumn deliquoringValueColumn;
        private System.Windows.Forms.GroupBox debugBox;
    }
}

