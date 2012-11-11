namespace CalculatorModules.Hydrocyclone
{
    partial class fsHydrocycloneControl
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxCalculationOption = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView_Result3 = new System.Windows.Forms.DataGridView();
            this.ParameterColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitsColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeedColumn = new fmDataGrid.fmDataGridViewNumericalTextBoxColumn();
            this.UnderflowColumn = new fmDataGrid.fmDataGridViewNumericalTextBoxColumn();
            this.OverflowColumn = new fmDataGrid.fmDataGridViewNumericalTextBoxColumn();
            this.splitContainerTwoTables = new System.Windows.Forms.SplitContainer();
            this.fsParametersWithValuesTable1 = new fsUIControls.fsParametersWithValuesTable();
            this.fsParametersWithValuesTable2 = new fsUIControls.fsParametersWithValuesTable();
            this.panelRight.SuspendLayout();
            this.leftTopPanel.SuspendLayout();
            this.calculationOptionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Result3)).BeginInit();
            this.splitContainerTwoTables.Panel1.SuspendLayout();
            this.splitContainerTwoTables.Panel2.SuspendLayout();
            this.splitContainerTwoTables.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.splitContainerTwoTables);
            this.panelRight.Controls.Add(this.dataGridView_Result3);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Controls.Add(this.comboBoxCalculationOption);
            this.calculationOptionsPanel.Controls.Add(this.label1);
            // 
            // comboBoxCalculationOption
            // 
            this.comboBoxCalculationOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCalculationOption.FormattingEnabled = true;
            this.comboBoxCalculationOption.Location = new System.Drawing.Point(84, 11);
            this.comboBoxCalculationOption.Name = "comboBoxCalculationOption";
            this.comboBoxCalculationOption.Size = new System.Drawing.Size(146, 21);
            this.comboBoxCalculationOption.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Calculation:";
            // 
            // dataGridView_Result3
            // 
            this.dataGridView_Result3.AllowUserToAddRows = false;
            this.dataGridView_Result3.AllowUserToDeleteRows = false;
            this.dataGridView_Result3.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView_Result3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView_Result3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Result3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ParameterColumn3,
            this.UnitsColumn3,
            this.FeedColumn,
            this.UnderflowColumn,
            this.OverflowColumn});
            this.dataGridView_Result3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView_Result3.Location = new System.Drawing.Point(0, 221);
            this.dataGridView_Result3.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView_Result3.Name = "dataGridView_Result3";
            this.dataGridView_Result3.ReadOnly = true;
            this.dataGridView_Result3.RowHeadersVisible = false;
            this.dataGridView_Result3.RowTemplate.Height = 24;
            this.dataGridView_Result3.Size = new System.Drawing.Size(156, 131);
            this.dataGridView_Result3.TabIndex = 5;
            // 
            // ParameterColumn3
            // 
            this.ParameterColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ParameterColumn3.HeaderText = "Parameter";
            this.ParameterColumn3.Name = "ParameterColumn3";
            this.ParameterColumn3.ReadOnly = true;
            this.ParameterColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UnitsColumn3
            // 
            this.UnitsColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnitsColumn3.HeaderText = "Units";
            this.UnitsColumn3.Name = "UnitsColumn3";
            this.UnitsColumn3.ReadOnly = true;
            this.UnitsColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FeedColumn
            // 
            this.FeedColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FeedColumn.HeaderText = "Feed";
            this.FeedColumn.Name = "FeedColumn";
            this.FeedColumn.ReadOnly = true;
            this.FeedColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // UnderflowColumn
            // 
            this.UnderflowColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UnderflowColumn.HeaderText = "Underflow";
            this.UnderflowColumn.Name = "UnderflowColumn";
            this.UnderflowColumn.ReadOnly = true;
            this.UnderflowColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // OverflowColumn
            // 
            this.OverflowColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.OverflowColumn.HeaderText = "Overflow";
            this.OverflowColumn.Name = "OverflowColumn";
            this.OverflowColumn.ReadOnly = true;
            this.OverflowColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // splitContainerTwoTables
            // 
            this.splitContainerTwoTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerTwoTables.Location = new System.Drawing.Point(0, 0);
            this.splitContainerTwoTables.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainerTwoTables.Name = "splitContainerTwoTables";
            // 
            // splitContainerTwoTables.Panel1
            // 
            this.splitContainerTwoTables.Panel1.Controls.Add(this.fsParametersWithValuesTable1);
            // 
            // splitContainerTwoTables.Panel2
            // 
            this.splitContainerTwoTables.Panel2.Controls.Add(this.fsParametersWithValuesTable2);
            this.splitContainerTwoTables.Size = new System.Drawing.Size(156, 221);
            this.splitContainerTwoTables.SplitterDistance = 78;
            this.splitContainerTwoTables.SplitterWidth = 3;
            this.splitContainerTwoTables.TabIndex = 6;
            // 
            // fsParametersWithValuesTable1
            // 
            this.fsParametersWithValuesTable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fsParametersWithValuesTable1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.fsParametersWithValuesTable1.Location = new System.Drawing.Point(0, 0);
            this.fsParametersWithValuesTable1.Margin = new System.Windows.Forms.Padding(2);
            this.fsParametersWithValuesTable1.Name = "fsParametersWithValuesTable1";
            this.fsParametersWithValuesTable1.Size = new System.Drawing.Size(78, 221);
            this.fsParametersWithValuesTable1.TabIndex = 4;
            // 
            // fsParametersWithValuesTable2
            // 
            this.fsParametersWithValuesTable2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.fsParametersWithValuesTable2.Location = new System.Drawing.Point(0, 0);
            this.fsParametersWithValuesTable2.Margin = new System.Windows.Forms.Padding(2);
            this.fsParametersWithValuesTable2.Name = "fsParametersWithValuesTable2";
            this.fsParametersWithValuesTable2.Size = new System.Drawing.Size(75, 221);
            this.fsParametersWithValuesTable2.TabIndex = 5;
            // 
            // fsHydrocycloneControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "fsHydrocycloneControl";
            this.panelRight.ResumeLayout(false);
            this.leftTopPanel.ResumeLayout(false);
            this.calculationOptionsPanel.ResumeLayout(false);
            this.calculationOptionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Result3)).EndInit();
            this.splitContainerTwoTables.Panel1.ResumeLayout(false);
            this.splitContainerTwoTables.Panel2.ResumeLayout(false);
            this.splitContainerTwoTables.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView_Result3;
        private System.Windows.Forms.SplitContainer splitContainerTwoTables;
        private fsUIControls.fsParametersWithValuesTable fsParametersWithValuesTable1;
        private fsUIControls.fsParametersWithValuesTable fsParametersWithValuesTable2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParameterColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitsColumn3;
        private fmDataGrid.fmDataGridViewNumericalTextBoxColumn FeedColumn;
        private fmDataGrid.fmDataGridViewNumericalTextBoxColumn UnderflowColumn;
        private fmDataGrid.fmDataGridViewNumericalTextBoxColumn OverflowColumn;
        public System.Windows.Forms.ComboBox comboBoxCalculationOption;
    }
}
