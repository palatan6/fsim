using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Parameters;
using CalculatorModules.User_Controls.Help_Dialogs;

namespace CalculatorModules.Hydrocyclone.Feeds
{
    public partial class fsFeedCurvesSelectionDialog : Form
    {
        //public fsFeedCurvesSelectionDialog()
        // -------- new -------- 
        // Regarding fc: we need an access to  yAxisParameters, y2AxisParameters and
        // nameToParameter in FeedCurvesControl 
        public fsFeedCurvesSelectionDialog(FeedCurvesControl fc)
        // ---------------------
        {
            InitializeComponent();
            feedsControl = fc; // --- new
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        internal List<fsParameterIdentifier> GetCheckedYAxisParameters()
        {
            return y1SelectionControl.GetCheckedYAxisParameters();
        }

        internal List<fsParameterIdentifier> GetCheckedY2AxisParameters()
        {
            return y2SelectionControl.GetCheckedYAxisParameters();
        }

        internal void AssignYAxisParameters(List<fsYAxisParameterWithChecking> list)
        {
            y1SelectionControl.AssignYAxisParameters(list);
        }

        internal void AssignY2AxisParameters(List<fsYAxisParameterWithChecking> list)
        {
            y2SelectionControl.AssignYAxisParameters(list);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetY2Visible(splitContainer1.Panel2Collapsed);
            
        }

        private void fsFeedCurvesSelectionDialog_Load(object sender, EventArgs e)
        {
            SetY2Visible(y2SelectionControl.GetCheckedYAxisParameters().Count != 0);
        }

        private void SetY2Visible(bool isVisible)
        {
            if (isVisible == !splitContainer1.Panel2Collapsed)
                return;

            if (isVisible)
            {
                splitContainer1.Panel2Collapsed = false;
                Width = Width * 2;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                Width = Width / 2;
            }
        }

        //----------- new -------------- drag-and-drop functionality
        private FeedCurvesControl feedsControl;

        private ListView sourceListView;

        private int targetIndex = -1;

        private int draggedItemIndex = -1;

        private ListView other(ListView lv)
        {
            if (lv.Equals(y1SelectionControl.otherVariablesListView))
                return y2SelectionControl.otherVariablesListView;
            return y1SelectionControl.otherVariablesListView;
        }

        // Starts the drag-and-drop operation when an item is dragged.
        private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            sourceListView = (ListView)sender;
            ((ListView)sender).DoDragDrop(e.Item, DragDropEffects.Move);
        }

        // Sets the target drop effect.
        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            if (sender is ListView)
                e.Effect = e.AllowedEffect;
        }

        // Moves the insertion mark as the item is dragged.
        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (((ListView)sender).Equals(other(sourceListView)))
            {
                // Retrieve the client coordinates of the mouse pointer.
                Point targetPoint =
                    ((ListView)sender).PointToClient(new Point(e.X, e.Y));

                // Retrieve the index of the item closest to the mouse pointer.
                targetIndex = ((ListView)sender).InsertionMark.NearestIndex(new Point(0, targetPoint.Y));

                // Confirm that the mouse pointer is not over the dragged item.
                if (targetIndex > -1)
                {
                    // Determine whether the mouse pointer is to the top or
                    // the bottom of the midpoint of the closest item and set
                    // the InsertionMark.AppearsAfterItem property accordingly.
                    Rectangle itemBounds = ((ListView)sender).GetItemRect(targetIndex);
                    if (targetPoint.Y > itemBounds.Bottom + (itemBounds.Height / 2))
                        ((ListView)sender).InsertionMark.AppearsAfterItem = true;
                    else
                        ((ListView)sender).InsertionMark.AppearsAfterItem = false;
                }

                // Set the location of the insertion mark. If the mouse is
                // over the dragged item, the targetIndex value is -1 and
                // the insertion mark disappears.
                ((ListView)sender).InsertionMark.Index = targetIndex; 
            }
        }

        // Removes the insertion mark when the mouse leaves the control.
        private void ListView_DragLeave(object sender, EventArgs e)
        {
            ((ListView)sender).InsertionMark.Index = -1;
        }

        // Moves the item to the location of the insertion mark.
        private void ListView_DragDrop(object sender, DragEventArgs e)
        {
            ListView targetListView = other(sourceListView);
            if (((ListView)sender).Equals(targetListView))
            {
                // Retrieve the index of the insertion mark;
                //int targetIndex = targetListView.InsertionMark.Index;
                targetIndex = targetListView.InsertionMark.Index;

                // If the insertion mark is not visible, exit the method.
                if (targetIndex == -1)
                    return;

                // If the insertion mark is to the bottom of the item with
                // the corresponding index, increment the target index.
                if (targetListView.InsertionMark.AppearsAfterItem)
                    targetIndex++;

                // Retrieve the dragged item.
                ListViewItem draggedItem =
                    (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                String draggedItemName = draggedItem.Text;

                fsYAxisParameterWithChecking draggedYAxisParameter = feedsControl.nameToParameter[draggedItem.Text];
                draggedYAxisParameter.IsChecked = draggedItem.Checked;
                if (sourceListView.Equals(y1SelectionControl.otherVariablesListView))
                {
                    feedsControl.yAxisParameters.Remove(draggedYAxisParameter);
                    feedsControl.y2AxisParameters.Add(draggedYAxisParameter);
                }
                else
                {
                    feedsControl.y2AxisParameters.Remove(draggedYAxisParameter);
                    feedsControl.yAxisParameters.Add(draggedYAxisParameter);
                }
                AssignYAxisParameters(feedsControl.yAxisParameters);
                AssignY2AxisParameters(feedsControl.y2AxisParameters);
                // We have not to additionally insert (into targetListView) and remove
                // (from sourceListView) draggedItem as this is already done in AssignYAxisParameters,
                // AssignY2AxisParameters. But we have to place draggedItem at InsertionMark index.
                // This is done under appropriate defining ListViewIndexComparer.

                // Getting draggedItem index in rearranged otherVariablesListView.Items (here targetListView.Items)
                foreach (ListViewItem item in targetListView.Items)
                {
                    if (item.Text == draggedItemName)
                    {
                        draggedItemIndex = item.Index;
                        break;
                    }
                }
                // Setting an appropriate comparer
                targetListView.ListViewItemSorter = new ListViewIndexComparer(targetIndex, draggedItemIndex);
            }
        }

        // Sorts ListViewItem in the way to place draggedItem at InsertionMark index
        private class ListViewIndexComparer : System.Collections.IComparer
        {
            private int targetIndex;
            private int draggedItemIndex;
            private int min;
            private int max;

            public ListViewIndexComparer(int targetInd, int draggedInd)
            {
                targetIndex = targetInd;
                draggedItemIndex = draggedInd; 
                min = Math.Min(targetIndex, draggedItemIndex);
                max = Math.Max(targetIndex, draggedItemIndex);
            }

            public int Compare(object x, object y)
            {
                int indX = ((ListViewItem)x).Index;
                int indY = ((ListViewItem)y).Index;
                if (targetIndex == draggedItemIndex)
                {
                    return indX - indY;
                }
                else
                {                    
                    bool indCondition = indX >= min && indX <= max && indY >= min && indY <= max;
                    if (!indCondition)
                        // In intervals outside targetIndex .. draggedItemIndex the order is usual
                        return indX - indY;
                    else
                        // Inside targetIndex .. draggedItemIndex the order is an order of clockwise
                        // or counterclockwise cyclic permutation
                        if (targetIndex < draggedItemIndex)
                            if (indX == max)
                                return -1;
                            else
                                if (indY == max)
                                    return 1;
                                else
                                    return indX - indY;
                        else
                            if (indX == min)
                                return 1;
                            else
                                if (indY == min)
                                    return -1;
                                else
                                    return indX - indY;
                }
            }
        }
        //------------------------------
    }
}
