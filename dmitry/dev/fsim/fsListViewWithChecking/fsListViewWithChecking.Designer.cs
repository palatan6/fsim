namespace ListViewWithChecking
{
    partial class fsListViewWithChecking
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
            this.m_listView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // m_listView
            // 
            this.m_listView.AllowDrop = true;
            this.m_listView.CheckBoxes = true;
            this.m_listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_listView.Location = new System.Drawing.Point(0, 0);
            this.m_listView.Margin = new System.Windows.Forms.Padding(4);
            this.m_listView.MultiSelect = false;
            this.m_listView.Name = "m_listView";
            this.m_listView.Size = new System.Drawing.Size(200, 185);
            this.m_listView.TabIndex = 0;
            this.m_listView.UseCompatibleStateImageBehavior = false;
            this.m_listView.View = System.Windows.Forms.View.List;
            this.m_listView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewItem_Checked);
            this.m_listView.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListView_DragDrop);
            this.m_listView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseMove);
            this.m_listView.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListView_DragEnter);
            this.m_listView.DragLeave += new System.EventHandler(this.ListView_DragLeave);
            this.m_listView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListView_ItemDrag);
            this.m_listView.DragOver += new System.Windows.Forms.DragEventHandler(this.ListView_DragOver);
            this.m_listView.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.ListView_GiveFeedback);
            // 
            // fsListViewWithChecking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_listView);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fsListViewWithChecking";
            this.Size = new System.Drawing.Size(200, 185);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView m_listView;
    }
}
