namespace CalculatorModules
{
    partial class fsOptionsSingleTableWithPanelAndCommentsCalculatorControl
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelRight = new System.Windows.Forms.Panel();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.dataGrid = new fsUIControls.fsParametersWithValuesTable();
            this.leftTopPanel.SuspendLayout();
            this.tablesPanel.SuspendLayout();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftTopPanel
            // 
            this.leftTopPanel.Size = new System.Drawing.Size(837, 59);
            // 
            // calculationOptionsPanel
            // 
            this.calculationOptionsPanel.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.calculationOptionsPanel.Size = new System.Drawing.Size(769, 59);
            // 
            // tablesPanel
            // 
            this.tablesPanel.Controls.Add(this.splitContainerMain);
            this.tablesPanel.Size = new System.Drawing.Size(837, 416);
            // 
            // panelRight
            // 
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(472, 416);
            this.panelRight.TabIndex = 1;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.dataGrid);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelRight);
            this.splitContainerMain.Size = new System.Drawing.Size(837, 416);
            this.splitContainerMain.SplitterDistance = 361;
            this.splitContainerMain.TabIndex = 2;
            // 
            // dataGrid
            // 
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.Margin = new System.Windows.Forms.Padding(4);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(361, 416);
            this.dataGrid.TabIndex = 0;
            // 
            // OptionsSingleTableWithPanelAndCommentsCalculatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "fsOptionsSingleTableWithPanelAndCommentsCalculatorControl";
            this.Size = new System.Drawing.Size(837, 475);
            this.leftTopPanel.ResumeLayout(false);
            this.tablesPanel.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            this.splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        protected System.Windows.Forms.Panel panelRight;
        public fsUIControls.fsParametersWithValuesTable dataGrid;
    }
}
