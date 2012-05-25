using fsUIControls;

namespace Calculator
{
    partial class fsModulesForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.modulePanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.treeView1 = new fsUIControls.fsTreeView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.currentModuleTitleLabel = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(138, 19);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(90, 32);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "Add Module";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(243, 19);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 32);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Close";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // modulePanel
            // 
            this.modulePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modulePanel.Location = new System.Drawing.Point(327, 28);
            this.modulePanel.Name = "modulePanel";
            this.modulePanel.Size = new System.Drawing.Size(345, 447);
            this.modulePanel.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.OKButton);
            this.panel2.Controls.Add(this.cancelButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(327, 475);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 63);
            this.panel2.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.treeView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(327, 538);
            this.panel3.TabIndex = 5;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(327, 538);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView1AfterSelect);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.currentModuleTitleLabel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(327, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(345, 28);
            this.panel4.TabIndex = 6;
            // 
            // currentModuleTitleLabel
            // 
            this.currentModuleTitleLabel.AutoSize = true;
            this.currentModuleTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.currentModuleTitleLabel.Location = new System.Drawing.Point(6, 9);
            this.currentModuleTitleLabel.Name = "currentModuleTitleLabel";
            this.currentModuleTitleLabel.Size = new System.Drawing.Size(144, 13);
            this.currentModuleTitleLabel.TabIndex = 0;
            this.currentModuleTitleLabel.Text = "currentModuleTitleLabel";
            // 
            // fsModulesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 538);
            this.Controls.Add(this.modulePanel);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Name = "fsModulesForm";
            this.Text = "Calculator Modules";
            this.Load += new System.EventHandler(this.ModulesFormLoad);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel modulePanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        fsTreeView treeView1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label currentModuleTitleLabel;
    }
}