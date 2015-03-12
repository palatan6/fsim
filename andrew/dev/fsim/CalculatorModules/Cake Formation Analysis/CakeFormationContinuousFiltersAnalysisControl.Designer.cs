namespace CalculatorModules
{
    partial class CakeFormationContinuousFiltersAnalysisControl
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
            this.leftTopPanel.SuspendLayout();
            this.calculationOptionsPanel.SuspendLayout();
            this.tablesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // simulationBox
            // 
            this.simulationBox.Items.AddRange(new object[] {
            "Default",
            "Medium Resistance considered",
            "Medium Resistance and cake compressibility considered",
            "Show also ne"});
            // 
            // dataGrid
            // 
            this.dataGrid.Size = new System.Drawing.Size(329, 728);
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Size = new System.Drawing.Size(329, 72);
            // 
            // tablesPanel
            // 
            this.tablesPanel.Size = new System.Drawing.Size(329, 728);
            // 
            // CakeFormationContinuousFiltersAnalysisControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CakeFormationContinuousFiltersAnalysisControl";
            this.Size = new System.Drawing.Size(329, 800);
            this.leftTopPanel.ResumeLayout(false);
            this.calculationOptionsPanel.ResumeLayout(false);
            this.calculationOptionsPanel.PerformLayout();
            this.tablesPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
