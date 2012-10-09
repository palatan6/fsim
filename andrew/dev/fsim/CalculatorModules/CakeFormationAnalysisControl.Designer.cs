namespace CalculatorModules.Cake_Fromation
{
    partial class CakeFormationAnalysisControl
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
            this.filtrationOptionBox = new System.Windows.Forms.ComboBox();
            this.simulationBox = new System.Windows.Forms.ComboBox();
            this.leftTopPanel.SuspendLayout();
            this.calculationOptionsPanel.SuspendLayout();
            this.tablesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.Size = new System.Drawing.Size(329, 752);
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Size = new System.Drawing.Size(329, 72);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Controls.Add(this.simulationBox);
            this.calculationOptionsPanel.Controls.Add(this.filtrationOptionBox);
            this.calculationOptionsPanel.Size = new System.Drawing.Size(278, 48);
            // 
            // tablesPanel
            // 
            this.tablesPanel.Size = new System.Drawing.Size(329, 752);
            // 
            // filtrationOptionBox
            // 
            this.filtrationOptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filtrationOptionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtrationOptionBox.FormattingEnabled = true;
            this.filtrationOptionBox.Location = new System.Drawing.Point(6, 8);
            this.filtrationOptionBox.Name = "filtrationOptionBox";
            this.filtrationOptionBox.Size = new System.Drawing.Size(272, 21);
            this.filtrationOptionBox.TabIndex = 0;
            // 
            // simulationBox
            // 
            this.simulationBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simulationBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simulationBox.FormattingEnabled = true;
            this.simulationBox.Location = new System.Drawing.Point(6, 40);
            this.simulationBox.Name = "simulationBox";
            this.simulationBox.Size = new System.Drawing.Size(272, 21);
            this.simulationBox.TabIndex = 1;
            // 
            // CakeFormationAnalysisControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CakeFormationAnalysisControl";
            this.Size = new System.Drawing.Size(329, 800);
            this.leftTopPanel.ResumeLayout(false);
            this.calculationOptionsPanel.ResumeLayout(false);
            this.tablesPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox filtrationOptionBox;
        private System.Windows.Forms.ComboBox simulationBox;
    }
}
