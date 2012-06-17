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
            this.filtrOptionBox = new System.Windows.Forms.ComboBox();
            this.simulationBox = new System.Windows.Forms.ComboBox();
            this.leftTopPanel.SuspendLayout();
            this.calculationOptionsPanel.SuspendLayout();
            this.tablesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.Size = new System.Drawing.Size(329, 427);
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Size = new System.Drawing.Size(329, 48);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Controls.Add(this.simulationBox);
            this.calculationOptionsPanel.Controls.Add(this.filtrOptionBox);
            // 
            // tablesPanel
            // 
            this.tablesPanel.Size = new System.Drawing.Size(329, 427);
            // 
            // filtrOptionBox
            // 
            this.filtrOptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filtrOptionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtrOptionBox.FormattingEnabled = true;
            this.filtrOptionBox.Location = new System.Drawing.Point(71, 3);
            this.filtrOptionBox.Name = "filtrOptionBox";
            this.filtrOptionBox.Size = new System.Drawing.Size(150, 21);
            this.filtrOptionBox.TabIndex = 0;
            // 
            // simulationBox
            // 
            this.simulationBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simulationBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simulationBox.FormattingEnabled = true;
            this.simulationBox.Location = new System.Drawing.Point(71, 27);
            this.simulationBox.Name = "simulationBox";
            this.simulationBox.Size = new System.Drawing.Size(150, 21);
            this.simulationBox.TabIndex = 1;
            // 
            // CakeFormationAnalysisControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CakeFormationAnalysisControl";
            this.Size = new System.Drawing.Size(329, 475);
            this.leftTopPanel.ResumeLayout(false);
            this.calculationOptionsPanel.ResumeLayout(false);
            this.tablesPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox filtrOptionBox;
        private System.Windows.Forms.ComboBox simulationBox;
    }
}
