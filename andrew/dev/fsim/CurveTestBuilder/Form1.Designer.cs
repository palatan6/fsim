namespace CurveTestBuilder
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listBoxGraficsChoose = new System.Windows.Forms.ListBox();
            this.fmZedGraphControlMain = new fmZedGraph.fmZedGraphControl();
            this.SuspendLayout();
            // 
            // listBoxGraficsChoose
            // 
            this.listBoxGraficsChoose.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxGraficsChoose.FormattingEnabled = true;
            this.listBoxGraficsChoose.ItemHeight = 16;
            this.listBoxGraficsChoose.Items.AddRange(new object[] {
            "sin(x)",
            "sqrt(x)",
            "x^2"});
            this.listBoxGraficsChoose.Location = new System.Drawing.Point(0, 0);
            this.listBoxGraficsChoose.Name = "listBoxGraficsChoose";
            this.listBoxGraficsChoose.Size = new System.Drawing.Size(246, 468);
            this.listBoxGraficsChoose.TabIndex = 1;
            this.listBoxGraficsChoose.SelectedIndexChanged += new System.EventHandler(this.listBoxGraficsChoose_SelectedIndexChanged);
            // 
            // fmZedGraphControlMain
            // 
            this.fmZedGraphControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fmZedGraphControlMain.IsAntiAlias = true;
            this.fmZedGraphControlMain.Location = new System.Drawing.Point(246, 0);
            this.fmZedGraphControlMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fmZedGraphControlMain.Name = "fmZedGraphControlMain";
            this.fmZedGraphControlMain.ScrollGrace = 0;
            this.fmZedGraphControlMain.ScrollMaxX = 0;
            this.fmZedGraphControlMain.ScrollMaxY = 0;
            this.fmZedGraphControlMain.ScrollMaxY2 = 0;
            this.fmZedGraphControlMain.ScrollMinX = 0;
            this.fmZedGraphControlMain.ScrollMinY = 0;
            this.fmZedGraphControlMain.ScrollMinY2 = 0;
            this.fmZedGraphControlMain.Size = new System.Drawing.Size(795, 482);
            this.fmZedGraphControlMain.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1041, 482);
            this.Controls.Add(this.fmZedGraphControlMain);
            this.Controls.Add(this.listBoxGraficsChoose);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxGraficsChoose;
        private fmZedGraph.fmZedGraphControl fmZedGraphControlMain;
    }
}

