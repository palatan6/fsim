namespace CalculatorModules.User_Controls
{
    partial class fsTableAndChart
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.yAxisSplitContainer = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.m_xAxisComboBox = new System.Windows.Forms.ComboBox();
            this.yAxisConfigure = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_yAxisList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.m_y2AxisList = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.inputsTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.detalizationBox = new fmDataGrid.fmNumericalTextBox();
            this.rangeTo = new fmDataGrid.fmNumericalTextBox();
            this.rangeFrom = new fmDataGrid.fmNumericalTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.iterationList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fsDiagramWithTable1 = new CalculatorModules.User_Controls.fsDiagramWithTable();
            this.panel1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.yAxisSplitContainer.Panel1.SuspendLayout();
            this.yAxisSplitContainer.Panel2.SuspendLayout();
            this.yAxisSplitContainer.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.Controls.Add(this.splitContainer2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(201, 562);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 112);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.yAxisSplitContainer);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.inputsTextBox);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Size = new System.Drawing.Size(201, 450);
            this.splitContainer2.SplitterDistance = 175;
            this.splitContainer2.SplitterWidth = 5;
            this.splitContainer2.TabIndex = 11;
            // 
            // yAxisSplitContainer
            // 
            this.yAxisSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.yAxisSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.yAxisSplitContainer.IsSplitterFixed = true;
            this.yAxisSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.yAxisSplitContainer.Margin = new System.Windows.Forms.Padding(4);
            this.yAxisSplitContainer.Name = "yAxisSplitContainer";
            this.yAxisSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // yAxisSplitContainer.Panel1
            // 
            this.yAxisSplitContainer.Panel1.Controls.Add(this.label4);
            this.yAxisSplitContainer.Panel1.Controls.Add(this.m_xAxisComboBox);
            this.yAxisSplitContainer.Panel1.Controls.Add(this.yAxisConfigure);
            // 
            // yAxisSplitContainer.Panel2
            // 
            this.yAxisSplitContainer.Panel2.Controls.Add(this.splitContainer1);
            this.yAxisSplitContainer.Size = new System.Drawing.Size(201, 175);
            this.yAxisSplitContainer.SplitterDistance = 68;
            this.yAxisSplitContainer.SplitterWidth = 5;
            this.yAxisSplitContainer.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 17);
            this.label4.TabIndex = 12;
            this.label4.Text = "X Axis";
            // 
            // m_xAxisComboBox
            // 
            this.m_xAxisComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_xAxisComboBox.FormattingEnabled = true;
            this.m_xAxisComboBox.Location = new System.Drawing.Point(64, 7);
            this.m_xAxisComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.m_xAxisComboBox.Name = "m_xAxisComboBox";
            this.m_xAxisComboBox.Size = new System.Drawing.Size(128, 24);
            this.m_xAxisComboBox.TabIndex = 11;
            this.m_xAxisComboBox.SelectedIndexChanged += new System.EventHandler(this.XAxisComboBoxSelectedIndexChanged);
            // 
            // yAxisConfigure
            // 
            this.yAxisConfigure.Location = new System.Drawing.Point(64, 41);
            this.yAxisConfigure.Margin = new System.Windows.Forms.Padding(4);
            this.yAxisConfigure.Name = "yAxisConfigure";
            this.yAxisConfigure.Size = new System.Drawing.Size(129, 28);
            this.yAxisConfigure.TabIndex = 10;
            this.yAxisConfigure.Text = "Configure Y Axes";
            this.yAxisConfigure.UseVisualStyleBackColor = true;
            this.yAxisConfigure.Click += new System.EventHandler(this.YAxisConfigureClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_yAxisList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_y2AxisList);
            this.splitContainer1.Size = new System.Drawing.Size(201, 102);
            this.splitContainer1.SplitterDistance = 95;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            // 
            // m_yAxisList
            // 
            this.m_yAxisList.CheckBoxes = true;
            this.m_yAxisList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.m_yAxisList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_yAxisList.Location = new System.Drawing.Point(0, 0);
            this.m_yAxisList.Margin = new System.Windows.Forms.Padding(4);
            this.m_yAxisList.Name = "m_yAxisList";
            this.m_yAxisList.Size = new System.Drawing.Size(95, 102);
            this.m_yAxisList.TabIndex = 9;
            this.m_yAxisList.UseCompatibleStateImageBehavior = false;
            this.m_yAxisList.View = System.Windows.Forms.View.Details;
            this.m_yAxisList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.YAxisListItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Y Axis";
            // 
            // m_y2AxisList
            // 
            this.m_y2AxisList.CheckBoxes = true;
            this.m_y2AxisList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.m_y2AxisList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_y2AxisList.Location = new System.Drawing.Point(0, 0);
            this.m_y2AxisList.Margin = new System.Windows.Forms.Padding(4);
            this.m_y2AxisList.Name = "m_y2AxisList";
            this.m_y2AxisList.Size = new System.Drawing.Size(101, 102);
            this.m_y2AxisList.TabIndex = 2;
            this.m_y2AxisList.UseCompatibleStateImageBehavior = false;
            this.m_y2AxisList.View = System.Windows.Forms.View.Details;
            this.m_y2AxisList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.YAxisListItemChecked);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Y2 Axis";
            // 
            // inputsTextBox
            // 
            this.inputsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputsTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.inputsTextBox.Location = new System.Drawing.Point(0, 17);
            this.inputsTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputsTextBox.Multiline = true;
            this.inputsTextBox.Name = "inputsTextBox";
            this.inputsTextBox.ReadOnly = true;
            this.inputsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inputsTextBox.Size = new System.Drawing.Size(201, 253);
            this.inputsTextBox.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Top;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Inputs:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.detalizationBox);
            this.panel2.Controls.Add(this.rangeTo);
            this.panel2.Controls.Add(this.rangeFrom);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.iterationList);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(201, 112);
            this.panel2.TabIndex = 10;
            // 
            // detalizationBox
            // 
            this.detalizationBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.detalizationBox.ForeColor = System.Drawing.Color.Red;
            this.detalizationBox.Location = new System.Drawing.Point(95, 74);
            this.detalizationBox.Margin = new System.Windows.Forms.Padding(4);
            this.detalizationBox.Name = "detalizationBox";
            this.detalizationBox.Size = new System.Drawing.Size(97, 23);
            this.detalizationBox.TabIndex = 10;
            this.detalizationBox.TextChanged += new System.EventHandler(this.DetalizationBoxTextChanged);
            // 
            // rangeTo
            // 
            this.rangeTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.rangeTo.ForeColor = System.Drawing.Color.Red;
            this.rangeTo.Location = new System.Drawing.Point(135, 42);
            this.rangeTo.Margin = new System.Windows.Forms.Padding(4);
            this.rangeTo.Name = "rangeTo";
            this.rangeTo.Size = new System.Drawing.Size(57, 23);
            this.rangeTo.TabIndex = 9;
            this.rangeTo.TextChanged += new System.EventHandler(this.RangeToTextChanged);
            // 
            // rangeFrom
            // 
            this.rangeFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.rangeFrom.ForeColor = System.Drawing.Color.Red;
            this.rangeFrom.Location = new System.Drawing.Point(64, 42);
            this.rangeFrom.Margin = new System.Windows.Forms.Padding(4);
            this.rangeFrom.Name = "rangeFrom";
            this.rangeFrom.Size = new System.Drawing.Size(57, 23);
            this.rangeFrom.TabIndex = 8;
            this.rangeFrom.TextChanged += new System.EventHandler(this.RangeFromTextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Iterate";
            // 
            // iterationList
            // 
            this.iterationList.DropDownHeight = 5000;
            this.iterationList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.iterationList.FormattingEnabled = true;
            this.iterationList.IntegralHeight = false;
            this.iterationList.Location = new System.Drawing.Point(64, 9);
            this.iterationList.Margin = new System.Windows.Forms.Padding(4);
            this.iterationList.Name = "iterationList";
            this.iterationList.Size = new System.Drawing.Size(128, 24);
            this.iterationList.TabIndex = 0;
            this.iterationList.SelectedIndexChanged += new System.EventHandler(this.IterationListSelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 78);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Detalization";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Range";
            // 
            // fsDiagramWithTable1
            // 
            this.fsDiagramWithTable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fsDiagramWithTable1.Location = new System.Drawing.Point(201, 0);
            this.fsDiagramWithTable1.Margin = new System.Windows.Forms.Padding(5);
            this.fsDiagramWithTable1.Name = "fsDiagramWithTable1";
            this.fsDiagramWithTable1.Size = new System.Drawing.Size(406, 562);
            this.fsDiagramWithTable1.TabIndex = 1;
            // 
            // fsTableAndChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fsDiagramWithTable1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fsTableAndChart";
            this.Size = new System.Drawing.Size(607, 562);
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.yAxisSplitContainer.Panel1.ResumeLayout(false);
            this.yAxisSplitContainer.Panel1.PerformLayout();
            this.yAxisSplitContainer.Panel2.ResumeLayout(false);
            this.yAxisSplitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox iterationList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private fsDiagramWithTable fsDiagramWithTable1;
        private System.Windows.Forms.SplitContainer yAxisSplitContainer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox inputsTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView m_y2AxisList;
        private System.Windows.Forms.ListView m_yAxisList;
        private fmDataGrid.fmNumericalTextBox detalizationBox;
        private fmDataGrid.fmNumericalTextBox rangeTo;
        private fmDataGrid.fmNumericalTextBox rangeFrom;
        private System.Windows.Forms.Button yAxisConfigure;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox m_xAxisComboBox;
    }
}
