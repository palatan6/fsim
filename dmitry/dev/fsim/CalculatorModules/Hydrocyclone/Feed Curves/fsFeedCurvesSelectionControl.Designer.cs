namespace CalculatorModules.Hydrocyclone.Feeds
{
    partial class fsFeedCurvesSelectionControl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelFeedFunctions = new System.Windows.Forms.Panel();
            this.otherVariablesListView = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panelFeedFunctions.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelFeedFunctions);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 200);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // panelFeedFunctions
            // 
            this.panelFeedFunctions.Controls.Add(this.otherVariablesListView);
            this.panelFeedFunctions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFeedFunctions.Location = new System.Drawing.Point(3, 48);
            this.panelFeedFunctions.Name = "panelFeedFunctions";
            this.panelFeedFunctions.Size = new System.Drawing.Size(244, 150);
            this.panelFeedFunctions.TabIndex = 10;
            // 
            // otherVariablesListView
            // 
            this.otherVariablesListView.AllowDrop = true;
            this.otherVariablesListView.CheckBoxes = true;
            this.otherVariablesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.otherVariablesListView.Location = new System.Drawing.Point(0, 0);
            this.otherVariablesListView.Name = "otherVariablesListView";
            this.otherVariablesListView.Size = new System.Drawing.Size(244, 150);
            this.otherVariablesListView.TabIndex = 0;
            this.otherVariablesListView.UseCompatibleStateImageBehavior = false;
            this.otherVariablesListView.View = System.Windows.Forms.View.List;
            this.otherVariablesListView.MultiSelect = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 32);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Feed Functions";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(157, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Deselect All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1Click);
            // 
            // fsFeedCurvesSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "fsFeedCurvesSelectionControl";
            this.Size = new System.Drawing.Size(250, 200);
            this.groupBox1.ResumeLayout(false);
            this.panelFeedFunctions.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ListView otherVariablesListView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelFeedFunctions;
        private System.Windows.Forms.Label label1;
    }
}
