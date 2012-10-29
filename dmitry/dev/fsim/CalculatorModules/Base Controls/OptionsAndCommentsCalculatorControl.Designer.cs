using CalculatorModules.User_Controls;

namespace CalculatorModules
{
    partial class fsOptionsAndCommentsCalculatorControl
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
            this.leftPanel = new System.Windows.Forms.Panel();
            this.tablesPanel = new System.Windows.Forms.Panel();
            this.leftTopPanel = new System.Windows.Forms.Panel();
            this.calculationOptionsPanel = new System.Windows.Forms.Panel();
            this.showHideDiagramPanel = new System.Windows.Forms.Panel();
            this.showHideCommentsButton = new System.Windows.Forms.Button();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.fsTableAndChart1 = new CalculatorModules.User_Controls.fsTableAndChart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.leftPanel.SuspendLayout();
            this.leftTopPanel.SuspendLayout();
            this.showHideDiagramPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftPanel
            // 
            this.leftPanel.Controls.Add(this.tablesPanel);
            this.leftPanel.Controls.Add(this.leftTopPanel);
            this.leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(94, 400);
            this.leftPanel.TabIndex = 0;
            // 
            // tablesPanel
            // 
            this.tablesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablesPanel.Location = new System.Drawing.Point(0, 48);
            this.tablesPanel.Name = "tablesPanel";
            this.tablesPanel.Size = new System.Drawing.Size(94, 352);
            this.tablesPanel.TabIndex = 1;
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Controls.Add(this.calculationOptionsPanel);
            this.leftTopPanel.Controls.Add(this.showHideDiagramPanel);
            this.leftTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.leftTopPanel.Location = new System.Drawing.Point(0, 0);
            this.leftTopPanel.Name = "leftTopPanel";
            this.leftTopPanel.Size = new System.Drawing.Size(94, 48);
            this.leftTopPanel.TabIndex = 0;
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calculationOptionsPanel.Location = new System.Drawing.Point(0, 0);
            this.calculationOptionsPanel.Name = "calculationOptionsPanel";
            this.calculationOptionsPanel.Size = new System.Drawing.Size(43, 48);
            this.calculationOptionsPanel.TabIndex = 0;
            // 
            // showHideDiagramPanel
            // 
            this.showHideDiagramPanel.Controls.Add(this.showHideCommentsButton);
            this.showHideDiagramPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.showHideDiagramPanel.Location = new System.Drawing.Point(43, 0);
            this.showHideDiagramPanel.Name = "showHideDiagramPanel";
            this.showHideDiagramPanel.Size = new System.Drawing.Size(51, 48);
            this.showHideDiagramPanel.TabIndex = 1;
            // 
            // showHideCommentsButton
            // 
            this.showHideCommentsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.showHideCommentsButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.showHideCommentsButton.Location = new System.Drawing.Point(6, 8);
            this.showHideCommentsButton.Name = "showHideCommentsButton";
            this.showHideCommentsButton.Size = new System.Drawing.Size(35, 34);
            this.showHideCommentsButton.TabIndex = 0;
            this.showHideCommentsButton.Text = ">";
            this.showHideCommentsButton.UseVisualStyleBackColor = true;
            this.showHideCommentsButton.Click += new System.EventHandler(this.Button1Click);
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add(this.fsTableAndChart1);
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightPanel.Location = new System.Drawing.Point(0, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(180, 400);
            this.rightPanel.TabIndex = 1;
            this.rightPanel.Visible = false;
            // 
            // fsTableAndChart1
            // 
            this.fsTableAndChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fsTableAndChart1.Location = new System.Drawing.Point(0, 0);
            this.fsTableAndChart1.Name = "fsTableAndChart1";
            this.fsTableAndChart1.Size = new System.Drawing.Size(180, 400);
            this.fsTableAndChart1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.leftPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rightPanel);
            this.splitContainer1.Size = new System.Drawing.Size(278, 400);
            this.splitContainer1.SplitterDistance = 94;
            this.splitContainer1.TabIndex = 2;
            // 
            // fsOptionsAndCommentsCalculatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "fsOptionsAndCommentsCalculatorControl";
            this.Size = new System.Drawing.Size(278, 400);
            this.leftPanel.ResumeLayout(false);
            this.leftTopPanel.ResumeLayout(false);
            this.showHideDiagramPanel.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel leftPanel;
        protected System.Windows.Forms.Panel leftTopPanel;
        private System.Windows.Forms.Panel rightPanel;
        protected System.Windows.Forms.Panel calculationOptionsPanel;
        private System.Windows.Forms.Panel showHideDiagramPanel;
        private System.Windows.Forms.Button showHideCommentsButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private fsTableAndChart fsTableAndChart1;
        protected System.Windows.Forms.Panel tablesPanel;
    }
}
