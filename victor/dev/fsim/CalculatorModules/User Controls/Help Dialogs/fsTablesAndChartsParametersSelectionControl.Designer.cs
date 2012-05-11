namespace CalculatorModules.User_Controls.Help_Dialogs
{
    partial class fsTablesAndChartsParametersSelectionControl
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.materialVariablesListView = new System.Windows.Forms.ListView();
            this.materialConstantsListView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.otherVariablesListView = new System.Windows.Forms.ListView();
            this.otherConstantsListView = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 398);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 16);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(491, 379);
            this.splitContainer1.SplitterDistance = 157;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 23);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.materialVariablesListView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.materialConstantsListView);
            this.splitContainer2.Size = new System.Drawing.Size(157, 356);
            this.splitContainer2.SplitterDistance = 162;
            this.splitContainer2.TabIndex = 2;
            // 
            // materialVariablesListView
            // 
            this.materialVariablesListView.CheckBoxes = true;
            this.materialVariablesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialVariablesListView.Location = new System.Drawing.Point(0, 0);
            this.materialVariablesListView.Name = "materialVariablesListView";
            this.materialVariablesListView.Size = new System.Drawing.Size(157, 162);
            this.materialVariablesListView.TabIndex = 0;
            this.materialVariablesListView.UseCompatibleStateImageBehavior = false;
            this.materialVariablesListView.View = System.Windows.Forms.View.List;
            // 
            // materialConstantsListView
            // 
            this.materialConstantsListView.CheckBoxes = true;
            this.materialConstantsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialConstantsListView.Location = new System.Drawing.Point(0, 0);
            this.materialConstantsListView.Name = "materialConstantsListView";
            this.materialConstantsListView.Size = new System.Drawing.Size(157, 190);
            this.materialConstantsListView.TabIndex = 0;
            this.materialConstantsListView.UseCompatibleStateImageBehavior = false;
            this.materialConstantsListView.View = System.Windows.Forms.View.List;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Material Parameters";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 23);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.otherVariablesListView);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.otherConstantsListView);
            this.splitContainer3.Size = new System.Drawing.Size(330, 356);
            this.splitContainer3.SplitterDistance = 162;
            this.splitContainer3.TabIndex = 2;
            // 
            // otherVariablesListView
            // 
            this.otherVariablesListView.CheckBoxes = true;
            this.otherVariablesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.otherVariablesListView.Location = new System.Drawing.Point(0, 0);
            this.otherVariablesListView.Name = "otherVariablesListView";
            this.otherVariablesListView.Size = new System.Drawing.Size(330, 162);
            this.otherVariablesListView.TabIndex = 0;
            this.otherVariablesListView.UseCompatibleStateImageBehavior = false;
            this.otherVariablesListView.View = System.Windows.Forms.View.List;
            // 
            // otherConstantsListView
            // 
            this.otherConstantsListView.CheckBoxes = true;
            this.otherConstantsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.otherConstantsListView.Location = new System.Drawing.Point(0, 0);
            this.otherConstantsListView.Name = "otherConstantsListView";
            this.otherConstantsListView.Size = new System.Drawing.Size(330, 190);
            this.otherConstantsListView.TabIndex = 0;
            this.otherConstantsListView.UseCompatibleStateImageBehavior = false;
            this.otherConstantsListView.View = System.Windows.Forms.View.List;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(330, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Machine Setting Parameters";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fsTablesAndChartsParametersSelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "fsTablesAndChartsParametersSelectionControl";
            this.Size = new System.Drawing.Size(500, 404);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListView materialVariablesListView;
        private System.Windows.Forms.ListView materialConstantsListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.ListView otherVariablesListView;
        private System.Windows.Forms.ListView otherConstantsListView;
        private System.Windows.Forms.Label label2;
    }
}
