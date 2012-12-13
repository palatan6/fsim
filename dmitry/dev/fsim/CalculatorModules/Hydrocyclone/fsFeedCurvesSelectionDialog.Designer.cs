using System;
using System.Windows.Forms;
using System.Drawing;

namespace CalculatorModules.Hydrocyclone.Feeds
{
    partial class fsFeedCurvesSelectionDialog
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
            this.cancelButton = new Button();
            this.okButton = new Button();
            this.panel1 = new Panel();
            this.splitContainer1 = new SplitContainer();
            this.y1SelectionControl = new CalculatorModules.Hydrocyclone.Feeds.fsFeedCurvesSelectionControl();
            this.y2SelectionControl = new CalculatorModules.Hydrocyclone.Feeds.fsFeedCurvesSelectionControl();
            this.panel2 = new Panel();
            this.button1 = new Button();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.cancelButton.Location = new Point(436, 5);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new EventHandler(this.CancelButtonClick);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.okButton.Location = new Point(355, 5);
            this.okButton.Name = "okButton";
            this.okButton.Size = new Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new EventHandler(this.OkButtonClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new Point(0, 247);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(514, 31);
            this.panel1.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 41);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.y1SelectionControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.y2SelectionControl);
            this.splitContainer1.Size = new Size(514, 206);
            this.splitContainer1.SplitterDistance = 254;
            this.splitContainer1.TabIndex = 5;
            // 
            // y1SelectionControl
            // 
            this.y1SelectionControl.Dock = DockStyle.Fill;
            this.y1SelectionControl.Location = new Point(0, 0);
            this.y1SelectionControl.Name = "y1SelectionControl";
            this.y1SelectionControl.Size = new Size(254, 206);
            this.y1SelectionControl.TabIndex = 3;
            this.y1SelectionControl.Text = "Y Axis";
            // 
            // y2SelectionControl
            // 
            this.y2SelectionControl.Dock = DockStyle.Fill;
            this.y2SelectionControl.Location = new Point(0, 0);
            this.y2SelectionControl.Name = "y2SelectionControl";
            this.y2SelectionControl.Size = new Size(256, 206);
            this.y2SelectionControl.TabIndex = 0;
            this.y2SelectionControl.Text = "Y2 Axis";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(514, 41);
            this.panel2.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom)
                        | AnchorStyles.Right)));
            this.button1.ForeColor = SystemColors.WindowText;
            this.button1.Location = new Point(427, 12);
            this.button1.Name = "button1";
            this.button1.Size = new Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Y1 / Y2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            // 
            // fsFeedCurvesSelectionDialog
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(514, 278);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "fsFeedCurvesSelectionDialog";
            this.Text = "fsFeedCurvesSelectionDialog";
            this.Load += new EventHandler(this.fsFeedCurvesSelectionDialog_Load);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

            //------- new -----------
            // Initialize the drag-and-drop operation when running
            // under Windows XP or a later operating system.
            if (OSFeature.Feature.IsPresent(OSFeature.Themes))
            {
                foreach (var lv in new[] { this.y1SelectionControl.otherVariablesListView, this.y2SelectionControl.otherVariablesListView })
                {
                    lv.ListViewItemSorter = new ListViewIndexComparer(-1, -1);
                    lv.AllowDrop = true;
                    lv.ItemDrag += new ItemDragEventHandler(ListView_ItemDrag);
                    lv.DragEnter += new DragEventHandler(ListView_DragEnter);
                    lv.DragOver += new DragEventHandler(ListView_DragOver);
                    lv.DragLeave += new EventHandler(ListView_DragLeave);
                    lv.DragDrop += new DragEventHandler(ListView_DragDrop);
                }
            }
            //-----------------------

        }

        #endregion

        public fsFeedCurvesSelectionControl y1SelectionControl;
        public fsFeedCurvesSelectionControl y2SelectionControl;
        private Button cancelButton;
        private Button okButton;
        private Panel panel1;
        private SplitContainer splitContainer1;
        private Panel panel2;
        private Button button1;
    }
}