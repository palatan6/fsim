namespace CalculatorModules.Base_Controls
{
    partial class OptionsQuadraTableAndCommentsCalculatorControl
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
            this.MaterialAndSpecialTablesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.ResultsAndInputsSplitContainer = new System.Windows.Forms.SplitContainer();
            this.ResultsGroupBox = new System.Windows.Forms.GroupBox();
            this.ResultsTable = new fsUIControls.fsParametersWithValuesTable();
            this.InputsGroupBox = new System.Windows.Forms.GroupBox();
            this.InputsTable = new fsUIControls.fsParametersWithValuesTable();
            this.tablesSplitContainer.Panel1.SuspendLayout();
            this.tablesSplitContainer.Panel2.SuspendLayout();
            this.tablesSplitContainer.SuspendLayout();
            this.leftTopPanel.SuspendLayout();
            this.tablesPanel.SuspendLayout();
            this.MaterialAndSpecialTablesSplitContainer.Panel1.SuspendLayout();
            this.MaterialAndSpecialTablesSplitContainer.Panel2.SuspendLayout();
            this.MaterialAndSpecialTablesSplitContainer.SuspendLayout();
            this.ResultsAndInputsSplitContainer.Panel1.SuspendLayout();
            this.ResultsAndInputsSplitContainer.Panel2.SuspendLayout();
            this.ResultsAndInputsSplitContainer.SuspendLayout();
            this.ResultsGroupBox.SuspendLayout();
            this.InputsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablesSplitContainer
            // 
            // 
            // tablesSplitContainer.Panel1
            // 
            this.tablesSplitContainer.Panel1.Controls.Add(this.MaterialAndSpecialTablesSplitContainer);
            // 
            // materialParametersDataGrid
            // 
            this.materialParametersDataGrid.Size = new System.Drawing.Size(133, 174);
            // 
            // MaterialAndSpecialTablesSplitContainer
            // 
            this.MaterialAndSpecialTablesSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaterialAndSpecialTablesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaterialAndSpecialTablesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MaterialAndSpecialTablesSplitContainer.Name = "MaterialAndSpecialTablesSplitContainer";
            this.MaterialAndSpecialTablesSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MaterialAndSpecialTablesSplitContainer.Panel1
            // 
            this.MaterialAndSpecialTablesSplitContainer.Panel1.Controls.Add(this.materialParametersDataGrid);
            // 
            // MaterialAndSpecialTablesSplitContainer.Panel2
            // 
            this.MaterialAndSpecialTablesSplitContainer.Panel2.Controls.Add(this.ResultsAndInputsSplitContainer);
            this.MaterialAndSpecialTablesSplitContainer.Size = new System.Drawing.Size(135, 352);
            this.MaterialAndSpecialTablesSplitContainer.SplitterDistance = 176;
            this.MaterialAndSpecialTablesSplitContainer.TabIndex = 1;
            // 
            // ResultsAndInputsSplitContainer
            // 
            this.ResultsAndInputsSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultsAndInputsSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsAndInputsSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.ResultsAndInputsSplitContainer.Name = "ResultsAndInputsSplitContainer";
            this.ResultsAndInputsSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ResultsAndInputsSplitContainer.Panel1
            // 
            this.ResultsAndInputsSplitContainer.Panel1.Controls.Add(this.ResultsGroupBox);
            // 
            // ResultsAndInputsSplitContainer.Panel2
            // 
            this.ResultsAndInputsSplitContainer.Panel2.Controls.Add(this.InputsGroupBox);
            this.ResultsAndInputsSplitContainer.Size = new System.Drawing.Size(135, 172);
            this.ResultsAndInputsSplitContainer.SplitterDistance = 74;
            this.ResultsAndInputsSplitContainer.TabIndex = 2;
            // 
            // ResultsGroupBox
            // 
            this.ResultsGroupBox.Controls.Add(this.ResultsTable);
            this.ResultsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.ResultsGroupBox.Name = "ResultsGroupBox";
            this.ResultsGroupBox.Size = new System.Drawing.Size(133, 72);
            this.ResultsGroupBox.TabIndex = 1;
            this.ResultsGroupBox.TabStop = false;
            this.ResultsGroupBox.Text = "Results";
            // 
            // ResultsTable
            // 
            this.ResultsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ResultsTable.Location = new System.Drawing.Point(3, 16);
            this.ResultsTable.Name = "ResultsTable";
            this.ResultsTable.Size = new System.Drawing.Size(127, 53);
            this.ResultsTable.TabIndex = 0;
            // 
            // InputsGroupBox
            // 
            this.InputsGroupBox.Controls.Add(this.InputsTable);
            this.InputsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.InputsGroupBox.Name = "InputsGroupBox";
            this.InputsGroupBox.Size = new System.Drawing.Size(133, 92);
            this.InputsGroupBox.TabIndex = 1;
            this.InputsGroupBox.TabStop = false;
            this.InputsGroupBox.Text = "Inputs";
            // 
            // InputsTable
            // 
            this.InputsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InputsTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.InputsTable.Location = new System.Drawing.Point(3, 16);
            this.InputsTable.Name = "InputsTable";
            this.InputsTable.Size = new System.Drawing.Size(127, 73);
            this.InputsTable.TabIndex = 0;
            this.InputsTable.CellValueChangedByUser += new System.Windows.Forms.DataGridViewCellEventHandler(this.InputsTable_CellValueChangedByUser);
            // 
            // OptionsQuadraTableAndCommentsCalculatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "OptionsQuadraTableAndCommentsCalculatorControl";
            this.tablesSplitContainer.Panel1.ResumeLayout(false);
            this.tablesSplitContainer.Panel2.ResumeLayout(false);
            this.tablesSplitContainer.ResumeLayout(false);
            this.leftTopPanel.ResumeLayout(false);
            this.tablesPanel.ResumeLayout(false);
            this.MaterialAndSpecialTablesSplitContainer.Panel1.ResumeLayout(false);
            this.MaterialAndSpecialTablesSplitContainer.Panel2.ResumeLayout(false);
            this.MaterialAndSpecialTablesSplitContainer.ResumeLayout(false);
            this.ResultsAndInputsSplitContainer.Panel1.ResumeLayout(false);
            this.ResultsAndInputsSplitContainer.Panel2.ResumeLayout(false);
            this.ResultsAndInputsSplitContainer.ResumeLayout(false);
            this.ResultsGroupBox.ResumeLayout(false);
            this.InputsGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MaterialAndSpecialTablesSplitContainer;
        private System.Windows.Forms.GroupBox ResultsGroupBox;
        protected fsUIControls.fsParametersWithValuesTable ResultsTable;
        private System.Windows.Forms.SplitContainer ResultsAndInputsSplitContainer;
        private System.Windows.Forms.GroupBox InputsGroupBox;
        protected fsUIControls.fsParametersWithValuesTable InputsTable;
    }
}
