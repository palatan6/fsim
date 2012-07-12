namespace CalculatorModules
{
     sealed partial class fsCakePorossityControl
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
             this.saturationComboBox = new System.Windows.Forms.ComboBox();
             this.machineTypeComboBox = new System.Windows.Forms.ComboBox();
             this.saltContentComboBox = new System.Windows.Forms.ComboBox();
             this.label1 = new System.Windows.Forms.Label();
             this.label3 = new System.Windows.Forms.Label();
             this.label2 = new System.Windows.Forms.Label();
             this.leftTopPanel.SuspendLayout();
             this.calculationOptionsPanel.SuspendLayout();
             this.SuspendLayout();
             // 
             // leftTopPanel
             // 
             this.leftTopPanel.Size = new System.Drawing.Size(371, 96);
             // 
             // calculationOptionsPanel
             // 
             this.calculationOptionsPanel.Controls.Add(this.saturationComboBox);
             this.calculationOptionsPanel.Controls.Add(this.machineTypeComboBox);
             this.calculationOptionsPanel.Controls.Add(this.label2);
             this.calculationOptionsPanel.Controls.Add(this.saltContentComboBox);
             this.calculationOptionsPanel.Controls.Add(this.label3);
             this.calculationOptionsPanel.Controls.Add(this.label1);
             this.calculationOptionsPanel.Size = new System.Drawing.Size(320, 96);
             // 
             // saturationComboBox
             // 
             this.saturationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
             this.saturationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
             this.saturationComboBox.FormattingEnabled = true;
             this.saturationComboBox.Location = new System.Drawing.Point(134, 3);
             this.saturationComboBox.Name = "saturationComboBox";
             this.saturationComboBox.Size = new System.Drawing.Size(180, 21);
             this.saturationComboBox.TabIndex = 6;
             // 
             // machineTypeComboBox
             // 
             this.machineTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
             this.machineTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
             this.machineTypeComboBox.FormattingEnabled = true;
             this.machineTypeComboBox.Location = new System.Drawing.Point(134, 35);
             this.machineTypeComboBox.Name = "machineTypeComboBox";
             this.machineTypeComboBox.Size = new System.Drawing.Size(180, 21);
             this.machineTypeComboBox.TabIndex = 5;
             // 
             // saltContentComboBox
             // 
             this.saltContentComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
             this.saltContentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
             this.saltContentComboBox.FormattingEnabled = true;
             this.saltContentComboBox.Location = new System.Drawing.Point(134, 67);
             this.saltContentComboBox.Name = "saltContentComboBox";
             this.saltContentComboBox.Size = new System.Drawing.Size(180, 21);
             this.saltContentComboBox.TabIndex = 4;
             // 
             // label1
             // 
             this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
             this.label1.AutoSize = true;
             this.label1.Location = new System.Drawing.Point(27, 70);
             this.label1.Name = "label1";
             this.label1.Size = new System.Drawing.Size(101, 13);
             this.label1.TabIndex = 2;
             this.label1.Text = "Nonvolatile Solutes:";
             // 
             // label3
             // 
             this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
             this.label3.AutoSize = true;
             this.label3.Location = new System.Drawing.Point(48, 40);
             this.label3.Name = "label3";
             this.label3.Size = new System.Drawing.Size(80, 13);
             this.label3.TabIndex = 3;
             this.label3.Text = "Filter Geometry:";
             // 
             // label2
             // 
             this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
             this.label2.AutoSize = true;
             this.label2.Location = new System.Drawing.Point(50, 6);
             this.label2.Name = "label2";
             this.label2.Size = new System.Drawing.Size(78, 13);
             this.label2.TabIndex = 2;
             this.label2.Text = "Cake Moisture:";
             // 
             // fsCakePorossityControl
             // 
             this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
             this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
             this.Name = "fsCakePorossityControl";
             this.Size = new System.Drawing.Size(371, 366);
             this.leftTopPanel.ResumeLayout(false);
             this.calculationOptionsPanel.ResumeLayout(false);
             this.calculationOptionsPanel.PerformLayout();
             this.ResumeLayout(false);
 
         }
 
         #endregion
 
         private System.Windows.Forms.Label label3;
         private System.Windows.Forms.Label label2;
         private System.Windows.Forms.Label label1;
         private System.Windows.Forms.ComboBox saturationComboBox;
         private System.Windows.Forms.ComboBox machineTypeComboBox;
         private System.Windows.Forms.ComboBox saltContentComboBox;
     }
}
