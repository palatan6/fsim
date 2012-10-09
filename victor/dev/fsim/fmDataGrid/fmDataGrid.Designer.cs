using System;
using System.Drawing;
using System.Windows.Forms;
using Value;

namespace fmDataGrid
{
    partial class fmDataGrid
    {
        private void CopyEditedValueToCellValue()
        {
            CurrentCell.Value = CurrentCell.EditedFormattedValue;
        }

        private void fmDataGridTextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                CurrentCell.Value = (sender as TextBox).Text;
                if (CellValueChangedByUser != null)
                    CellValueChangedByUser(this,
                                           new DataGridViewCellEventArgs(CurrentCell.ColumnIndex, CurrentCell.RowIndex));
            }
        }

        private void fmCheckBoxClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((sender as fmDataGrid).Columns[e.ColumnIndex].GetType() == (new DataGridViewCheckBoxColumn()).GetType())
            {
                CopyEditedValueToCellValue();
                if (CellValueChangedByUser != null)
                    CellValueChangedByUser(this, new DataGridViewCellEventArgs(e.ColumnIndex, e.RowIndex));
            }
        }
        
        private void fmDataGridEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.TextChanged -= fmDataGridTextChanged;
            e.Control.TextChanged += fmDataGridTextChanged;
        }

        private void InitializeComponent()
        {
            #region Activate Immediate Writing text to cells
            EditingControlShowing += fmDataGridEditingControlShowing;
            #endregion

            #region Activate Immediate Writing checkBoxes to cells
            CellContentClick += fmCheckBoxClick;
            CellContentDoubleClick += fmCheckBoxClick;
            #endregion

            #region Activate Clearing of cell by pressing Delete button
            KeyDown += fmKeyDown;
            #endregion

            #region Setup font and row heights
            RowTemplate.Height = 18;
            Font = new Font(Font.FontFamily, 8.25f, FontStyle.Regular);
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            RowHeadersVisible = false;
            #endregion
        }

        void fmKeyDown(object sender, KeyEventArgs e)
        {
            if (CurrentCell != null)
            {
                int colIndex = CurrentCell.ColumnIndex;
                int rowIndex = CurrentCell.RowIndex;
                if (e.KeyCode == Keys.Delete && colIndex != -1 && rowIndex != -1
                    && Columns[colIndex].GetType() == (new fmDataGridViewNumericalTextBoxColumn()).GetType())
                {
                    CurrentCell.Value = "";
                    if (CellValueChangedByUser != null)
                        CellValueChangedByUser(this, new DataGridViewCellEventArgs(colIndex, rowIndex));
                }
            }
        }

        override protected void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            if (HighLightCurrentRow == true
                && CurrentCell != null 
                && e.RowIndex == CurrentCell.RowIndex)
            {
                Color newColor = Color.FromArgb(200, 255, 200);
                Color oldColor = e.CellStyle.BackColor;
                newColor = Color.FromArgb(oldColor.R * newColor.R / 255, 
                    oldColor.G * newColor.G / 255, 
                    oldColor.B * newColor.B / 255);
                e.CellStyle.BackColor = newColor;
            }

            if (CurrentCell != null)
            {
                if (CurrentCell.RowIndex == e.RowIndex
                    && CurrentCell.ColumnIndex == e.ColumnIndex)
                {
                    if (CurrentCell.OwningColumn.GetType() != (new DataGridViewCheckBoxColumn()).GetType())
                    {
                        DefaultCellStyle.SelectionBackColor = e.CellStyle.BackColor;
                        DefaultCellStyle.SelectionForeColor = e.CellStyle.ForeColor;
                        //e.AdvancedBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;
                        Rectangle r = e.CellBounds;
                        r.Width -= 2;
                        r.Height -= 2;
                        r.Inflate(-1, -1);
                        //r.Inflate(-1, -1);
                        e.PaintBackground(e.ClipBounds, false);
                        e.PaintContent(e.ClipBounds);
                        e.Graphics.DrawRectangle(new Pen(Color.Black), r);
                        e.Handled = true;
                    }
                    else
                    {
                        DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
                    }
                }
            }

            //base.OnCellPainting(e);
        }

        override protected void OnCurrentCellChanged(EventArgs e)
        {
            foreach (DataGridViewRow row in Rows)
                InvalidateRow(row.Index);
            base.OnCurrentCellChanged(e);
        }

        public event DataGridViewCellEventHandler CellValueChangedByUser;
    }
}
