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
        // Regarding fc: we need an access to  yAxisParameters, y2AxisParameters and
        // nameToParameter in FeedCurvesControl 
        public fsFeedCurvesSelectionDialog(FeedCurvesControl fc)
        {
            InitializeComponent();
            feedsControl = fc;
            // for drag-and-drop: we need an access to feedsControl in fsFeedCurvesSelectionControl
            y1SelectionControl.feedsControl = fc;
            y2SelectionControl.feedsControl = fc;
        }

        internal List<fsParameterIdentifier> GetCheckedYAxisParameters()
        {
            return y1SelectionControl.GetCheckedYAxisParameters();
        }

        internal List<fsParameterIdentifier> GetCheckedY2AxisParameters()
        {
            return y2SelectionControl.GetCheckedYAxisParameters();
        }

        private void ListView_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            e.Item.Focused = true;
            e.Item.Selected = true;
        }

        private void ListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //if (e.Item.Focused)
            if (e.Item.Selected || e.Item.Focused)
            {
                if (((ListView)sender).Equals(y1SelectionControl.otherVariablesListView))
                {
                    feedsControl.yAxisParameters = ResetFromItemCollection(y1SelectionControl.otherVariablesListView.Items);
                    AssignYAxisWithoutListView(feedsControl.yAxisParameters);
                    feedsControl.SetSelectedParameters(this, FeedCurvesControl.fsYAxisKind.Y1);
                }
                else
                {
                    feedsControl.y2AxisParameters = ResetFromItemCollection(y2SelectionControl.otherVariablesListView.Items);
                    AssignY2AxisWithoutListView(feedsControl.y2AxisParameters);
                    feedsControl.SetSelectedParameters(this, FeedCurvesControl.fsYAxisKind.Y2);
                }
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    SetY2Visible(splitContainer1.Panel2Collapsed);
            
        //}

        //private void fsFeedCurvesSelectionDialog_Load(object sender, EventArgs e)
        //{
        //    SetY2Visible(y2SelectionControl.GetCheckedYAxisParameters().Count != 0);
        //}

        //private void SetY2Visible(bool isVisible)
        //{
        //    if (isVisible == !splitContainer1.Panel2Collapsed)
        //        return;

        //    if (isVisible)
        //    {
        //        splitContainer1.Panel2Collapsed = false;
        //        Width = Width * 2;
        //    }
        //    else
        //    {
        //        splitContainer1.Panel2Collapsed = true;
        //        Width = Width / 2;
        //    }
        //}

        //----------- new -------------- drag-and-drop functionality
        internal void AssignYAxisParametersByOrder(fsYAxisParameterWithChecking[] list)
        {
            y1SelectionControl.AssignYAxisParametersByOrder(list);
        }

        internal void AssignY2AxisParametersByOrder(fsYAxisParameterWithChecking[] list)
        {
            y2SelectionControl.AssignYAxisParametersByOrder(list);
        }

        internal void AssignYAxisWithoutListView(fsYAxisParameterWithChecking[] list)
        {
            y1SelectionControl.AssignYAxisWithoutListView(list);
        }

        internal void AssignY2AxisWithoutListView(fsYAxisParameterWithChecking[] list)
        {
            y2SelectionControl.AssignYAxisWithoutListView(list);
        }

        private FeedCurvesControl feedsControl;

        private ListView sourceListView;

        private fsYAxisParameterWithChecking[] ResetFromItemCollection(ListView.ListViewItemCollection collection)
        {
            var array = new fsYAxisParameterWithChecking[collection.Count];
            foreach (ListViewItem item in collection)
            {
                fsYAxisParameterWithChecking parameter = feedsControl.nameToParameter[item.Text];
                parameter.IsChecked = item.Checked;
                array[item.Index] = new fsYAxisParameterWithChecking(parameter);
            }
            return array;
        }

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
            if (((ListView)sender).Items.Count == 0)
            {
                ((ListView)sender).InsertionMark.Index = 0;
                ((ListView)sender).InsertionMark.AppearsAfterItem = false;
                return;
            }

            // Retrieve the client coordinates of the mouse pointer.
            Point targetPoint =
                ((ListView)sender).PointToClient(new Point(e.X, e.Y));

            // Retrieve the index of the item closest to the mouse pointer.
            int targetIndex;
            if (((ListView)sender).Equals(other(sourceListView)))
                targetIndex = ((ListView)sender).InsertionMark.NearestIndex(new Point(0, targetPoint.Y));
            else
                targetIndex = ((ListView)sender).InsertionMark.NearestIndex(targetPoint);

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

        // Removes the insertion mark when the mouse leaves the control.
        private void ListView_DragLeave(object sender, EventArgs e)
        {
            ((ListView)sender).InsertionMark.Index = -1;
        }

        // Moves the item to the location of the insertion mark.
        private void ListView_DragDrop(object sender, DragEventArgs e)
        {
            ListView targetListView = other(sourceListView);
            int targetIndex = ((ListView)sender).InsertionMark.Index;

            // If the insertion mark is not visible, exit the method.
            if (targetIndex == -1)
                return;

            // If the insertion mark is to the bottom of the item with
            // the corresponding index, increment the target index.
            if (((ListView)sender).InsertionMark.AppearsAfterItem)
                targetIndex++;

            // Retrieve the dragged item.
            ListViewItem draggedItem =
                (ListViewItem)e.Data.GetData(typeof(ListViewItem));
            String draggedItemName = draggedItem.Text;

            fsYAxisParameterWithChecking draggedYAxisParameter = feedsControl.nameToParameter[draggedItem.Text];
            draggedYAxisParameter.IsChecked = draggedItem.Checked;

            if (((ListView)sender).Equals(targetListView))
            {
                // Insert a copy of the dragged item at the target index.
                // A copy must be inserted before the original item is removed
                // to preserve item index values. 
                targetListView.Items.Insert(targetIndex, (ListViewItem)draggedItem.Clone());

                // Remove the original copy of the dragged item.
                sourceListView.Items.Remove(draggedItem);

                feedsControl.yAxisParameters = ResetFromItemCollection(y1SelectionControl.otherVariablesListView.Items);
                AssignYAxisParametersByOrder(feedsControl.yAxisParameters);

                feedsControl.y2AxisParameters = ResetFromItemCollection(y2SelectionControl.otherVariablesListView.Items);
                AssignY2AxisParametersByOrder(feedsControl.y2AxisParameters);

                feedsControl.SetSelectedParameters(this, FeedCurvesControl.fsYAxisKind.Y1andY2);
            }
            else
            {
                // Insert a copy of the dragged item at the target index.
                // A copy must be inserted before the original item is removed
                // to preserve item index values. 
                sourceListView.Items.Insert(targetIndex, (ListViewItem)draggedItem.Clone());

                // Remove the original copy of the dragged item.
                sourceListView.Items.Remove(draggedItem);

                if (sourceListView.Equals(y1SelectionControl.otherVariablesListView))
                {
                    feedsControl.yAxisParameters = ResetFromItemCollection(y1SelectionControl.otherVariablesListView.Items);
                    AssignYAxisParametersByOrder(feedsControl.yAxisParameters);
                    feedsControl.SetSelectedParameters(this, FeedCurvesControl.fsYAxisKind.Y1);
                }
                else
                {
                    feedsControl.y2AxisParameters = ResetFromItemCollection(y2SelectionControl.otherVariablesListView.Items);
                    AssignY2AxisParametersByOrder(feedsControl.y2AxisParameters);
                    feedsControl.SetSelectedParameters(this, FeedCurvesControl.fsYAxisKind.Y2);
                }
            }
        }

        // Sorts ListViewItem in the way to place draggedItem at InsertionMark index
        private class ListViewIndexComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                return ((ListViewItem)x).Index - ((ListViewItem)y).Index;
            }
        }
        //------------------------------
    }
}
