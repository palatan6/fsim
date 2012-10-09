using System;

namespace CalculatorModules.Base_Controls
{
    partial class fsOptionsSingleTableAndCommentsCalculatorControl
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
            this.dataGrid = new fsUIControls.fsParametersWithValuesTable();
            this.leftTopPanel.SuspendLayout();
            this.tablesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Size = new System.Drawing.Size(278, 48);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Size = new System.Drawing.Size(227, 48);
            // 
            // tablesPanel
            // 
            this.tablesPanel.Controls.Add(this.dataGrid);
            this.tablesPanel.Size = new System.Drawing.Size(278, 352);
            // 
            // dataGrid
            // 
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(278, 352);
            this.dataGrid.TabIndex = 0;
            // 
            // fsOptionsSingleTableAndCommentsCalculatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "fsOptionsSingleTableAndCommentsCalculatorControl";
            this.Size = new System.Drawing.Size(278, 400);
            this.leftTopPanel.ResumeLayout(false);
            this.tablesPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected fsUIControls.fsParametersWithValuesTable dataGrid;




    }
}
