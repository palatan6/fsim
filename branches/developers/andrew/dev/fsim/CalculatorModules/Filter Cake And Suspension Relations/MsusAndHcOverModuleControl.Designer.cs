namespace CalculatorModules
{
    partial class MsusAndHcOverModuleControl
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
            this.tablesPanel = new System.Windows.Forms.Panel();
            this.filtrationOptionBox = new System.Windows.Forms.ComboBox();
            this.tablesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablesPanel
            // 
            this.tablesPanel.Controls.Add(this.filtrationOptionBox);
            this.tablesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablesPanel.Location = new System.Drawing.Point(0, 0);
            this.tablesPanel.Name = "tablesPanel";
            this.tablesPanel.Size = new System.Drawing.Size(226, 474);
            this.tablesPanel.TabIndex = 1;
            // 
            // filtrationOptionBox
            // 
            this.filtrationOptionBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.filtrationOptionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filtrationOptionBox.FormattingEnabled = true;
            this.filtrationOptionBox.Location = new System.Drawing.Point(43, 36);
            this.filtrationOptionBox.Name = "filtrationOptionBox";
            this.filtrationOptionBox.Size = new System.Drawing.Size(180, 21);
            this.filtrationOptionBox.TabIndex = 1;
            this.filtrationOptionBox.SelectedIndexChanged += new System.EventHandler(this.filtrationOptionBox_SelectedIndexChanged);
            // 
            // MsusAndHcOverModuleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tablesPanel);
            this.Name = "MsusAndHcOverModuleControl";
            this.Size = new System.Drawing.Size(226, 474);
            this.tablesPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel tablesPanel;
        private System.Windows.Forms.ComboBox filtrationOptionBox;
    }
}
