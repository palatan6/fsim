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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.leftTopPanel.SuspendLayout();
            this.calculationOptionsPanel.SuspendLayout();
            this.tablesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.Size = new System.Drawing.Size(329, 728);
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Size = new System.Drawing.Size(329, 72);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Controls.Add(this.label2);
            this.calculationOptionsPanel.Controls.Add(this.label1);
            this.calculationOptionsPanel.Controls.Add(this.simulationBox);
            this.calculationOptionsPanel.Controls.Add(this.filtrationOptionBox);
            this.calculationOptionsPanel.Size = new System.Drawing.Size(278, 72);
            // 
            // tablesPanel
            // 
            this.tablesPanel.Location = new System.Drawing.Point(0, 72);
            this.tablesPanel.Size = new System.Drawing.Size(329, 728);
            // 
            // filtrationOptionBox
            // 
            this.filtrationOptionBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.filtrationOptionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtrationOptionBox.FormattingEnabled = true;
            this.filtrationOptionBox.Location = new System.Drawing.Point(110, 8);
            this.filtrationOptionBox.Name = "filtrationOptionBox";
            this.filtrationOptionBox.Size = new System.Drawing.Size(160, 21);
            this.filtrationOptionBox.TabIndex = 0;
            this.filtrationOptionBox.TabStop = false;
            // 
            // simulationBox
            // 
            this.simulationBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simulationBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simulationBox.FormattingEnabled = true;
            this.simulationBox.Location = new System.Drawing.Point(110, 40);
            this.simulationBox.Name = "simulationBox";
            this.simulationBox.Size = new System.Drawing.Size(160, 21);
            this.simulationBox.TabIndex = 1;
            this.simulationBox.TabStop = false;
            this.simulationBox.DropDown += new System.EventHandler(this.simulationBox_DropDown);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter Type:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Calculation Option:";
            // 
            // CakeFormationAnalysisControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "CakeFormationAnalysisControl";
            this.Size = new System.Drawing.Size(329, 800);
            this.leftTopPanel.ResumeLayout(false);
            this.calculationOptionsPanel.ResumeLayout(false);
            this.calculationOptionsPanel.PerformLayout();
            this.tablesPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox filtrationOptionBox;
        private System.Windows.Forms.ComboBox simulationBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
