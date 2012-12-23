namespace CalculatorModules.Hydrocyclone.Feeds
{
    partial class FormFeedCurves
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
            this.feedCurvesControl1 = new CalculatorModules.Hydrocyclone.Feeds.FeedCurvesControl();
            this.SuspendLayout();
            // 
            // feedCurvesControl1
            // 
            this.feedCurvesControl1.Location = new System.Drawing.Point(1, 0);
            this.feedCurvesControl1.Name = "feedCurvesControl1";
            this.feedCurvesControl1.Size = new System.Drawing.Size(783, 611);
            this.feedCurvesControl1.TabIndex = 0;
            // 
            // FormFeedCurves
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 612);
            // ---- new -----
            this.Location = new System.Drawing.Point(0, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            // --------------
            this.Controls.Add(this.feedCurvesControl1);
            this.Name = "FormFeedCurves";
            this.Text = "Feed Curves";
            this.Activated += new System.EventHandler(this.FormFeedCurves_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.closeButton_Click);
            this.ResumeLayout(false);

        }

        #endregion

        public FeedCurvesControl feedCurvesControl1;
    }
}