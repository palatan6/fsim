namespace CalculatorModules.Base_Controls
{
    partial class fsOptionsDoubleTableAndCommentsCalculatorControl
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
            this.tablesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.dataGrid = new fsUIControls.fsParametersWithValuesTable();
            this.materialParametersDataGrid = new fsUIControls.fsParametersWithValuesTable();
            this.leftTopPanel.SuspendLayout();
            this.tablesPanel.SuspendLayout();
            this.tablesSplitContainer.Panel1.SuspendLayout();
            this.tablesSplitContainer.Panel2.SuspendLayout();
            this.tablesSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Size = new System.Drawing.Size(365, 48);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Size = new System.Drawing.Size(314, 48);
            // 
            // tablesPanel
            // 
            this.tablesPanel.Controls.Add(this.tablesSplitContainer);
            this.tablesPanel.Size = new System.Drawing.Size(365, 357);
            // 
            // tablesSplitContainer
            // 
            this.tablesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablesSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.tablesSplitContainer.Name = "tablesSplitContainer";
            // 
            // tablesSplitContainer.Panel1
            // 
            this.tablesSplitContainer.Panel1.Controls.Add(this.materialParametersDataGrid);
            // 
            // tablesSplitContainer.Panel2
            // 
            this.tablesSplitContainer.Panel2.Controls.Add(this.dataGrid);
            this.tablesSplitContainer.Size = new System.Drawing.Size(365, 357);
            this.tablesSplitContainer.SplitterDistance = 178;
            this.tablesSplitContainer.TabIndex = 1;
            // 
            // dataGrid
            // 
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(183, 357);
            this.dataGrid.TabIndex = 0;
            // 
            // materialParametersDataGrid
            // 
            this.materialParametersDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialParametersDataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.materialParametersDataGrid.Location = new System.Drawing.Point(0, 0);
            this.materialParametersDataGrid.Name = "materialParametersDataGrid";
            this.materialParametersDataGrid.Size = new System.Drawing.Size(178, 357);
            this.materialParametersDataGrid.TabIndex = 0;
            // 
            // fsOptionsDoubleTableAndCommentsCalculatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "fsOptionsDoubleTableAndCommentsCalculatorControl";
            this.Size = new System.Drawing.Size(365, 405);
            this.leftTopPanel.ResumeLayout(false);
            this.tablesPanel.ResumeLayout(false);
            this.tablesSplitContainer.Panel1.ResumeLayout(false);
            this.tablesSplitContainer.Panel2.ResumeLayout(false);
            this.tablesSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer tablesSplitContainer;
        protected fsUIControls.fsParametersWithValuesTable dataGrid;
        protected fsUIControls.fsParametersWithValuesTable materialParametersDataGrid;
    }
}
