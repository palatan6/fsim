using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ListViewWithChecking
{
    /// <summary>
    /// This class manages drag-and-drop operations between fsListViewWithChecking instances.
    /// </summary>
    public class fsListViewWithCheckingSupervisor
    {
        /// <summary>
        /// A directed graph defined by vertices and their lists of sons; the arc v1->v2 means that
        /// the drag-and-drop operation from the fsListViewWithChecking instance v1 to the 
        /// fsListViewWithChecking instance v2 is allowed. If a vertex v0 is not contained in 
        /// DragDropChart keys than the dragging on fsListViewWithChecking instance v0 is not allowed.
        /// </summary>
        protected virtual Dictionary<fsListViewWithChecking, List<fsListViewWithChecking>> DragDropChart { get; private set; }

        /// <summary>
        /// A fsListViewWithChecking instance which the dragging starts from.
        /// </summary>
        protected virtual fsListViewWithChecking sourceListView { get; private set; }

        /// <summary>
        /// A fsListViewWithChecking instance which the drag-and-drop operation ends on.
        /// </summary>
        protected virtual fsListViewWithChecking targetListView { get; private set; }

        /// <summary>
        /// The index in sourceListView of a dragged item.
        /// </summary>
        protected int sourceIndex { get; private set; }

        /// <summary>
        /// A dragged item from sourceListView.
        /// </summary>
        protected virtual object sourceItem { get; private set; }

        /// <summary>
        /// A check value of a dragged item from sourceListView.
        /// </summary>
        protected bool sourceCheckValue { get; private set; }

        /// <summary>
        /// An action to be done when an item is added to a fsListViewWithChecking instance.
        /// </summary>
        public OnAddAction AddAction { get; set; }

        /// <summary>
        /// An action to be done when an item is removed from a fsListViewWithChecking instance.
        /// </summary>
        public OnRemoveAction RemoveAction { get; set; }

        /// <summary>
        /// An action to be done when an (single) item is checked by a user in a fsListViewWithChecking instance.
        /// </summary>
        public OnCheckAction CheckAction { get; set; }

        /// <summary>
        /// An action to be done when a number of items is forced by a user to be (un)checked 
        /// (using some controls) in a fsListViewWithChecking instance.
        /// </summary>
        public OnMultipleCheckAction MultipleCheckAction { get; set; }

        /// <summary>
        /// An action to be done when an item is dragged from a fsListViewWithChecking instance
        /// and is dropped to another one.
        /// </summary>
        public OnDragDropAction DragDropAction { get; set; }

        private void SubscribeToEvents(fsListViewWithChecking list)
        {
            list.SetAsSourceListViewEvent += HandleSetAsSourceListViewEvent;
            list.SetAsTargetListViewEvent += HandleSetAsTargetListViewEvent;
            list.CheckListViewItemEvent += HandleCheckListViewItemEvent;
            list.MultipleCheckListViewItemsEvent += HandleMultipleCheckListViewItemsEvent;
            list.AddListViewItemEvent += HandleAddListViewItemEvent;
            list.RemoveListViewItemEvent += HandleRemoveListViewItemEvent;
        }

        /// <summary>
        /// Constructs DragDropChart by two arrays.
        /// </summary>
        /// <param name="keys">Array of departure vertices of DragDropChart digraph</param>
        /// <param name="values">Array of lists of sons of corresponding (by index) vertices from "keys"</param>
        public fsListViewWithCheckingSupervisor(fsListViewWithChecking[] keys,
                                                IEnumerable<fsListViewWithChecking>[] values)
        {
            sourceListView = null;

            DragDropChart = new Dictionary<fsListViewWithChecking, List<fsListViewWithChecking>>();
            if (keys.Length != values.Length)
                throw new Exception("The lengths of the first and second array parameters differ");
            var allLists = new List<fsListViewWithChecking>();
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i].AllowedDrag = true;
                if (!allLists.Contains(keys[i]))
                {
                    SubscribeToEvents(keys[i]);
                    allLists.Add(keys[i]);
                }
                foreach (fsListViewWithChecking list in values[i])
                {
                    if (!allLists.Contains(list))
                    {
                        SubscribeToEvents(list);
                        allLists.Add(list);
                    }
                }
                DragDropChart.Add(keys[i], new List<fsListViewWithChecking>(values[i]));
            }
        }

        /// <summary>
        /// Subscribing to SetAsSourceListViewEvent
        /// </summary>
        /// <param name="sender">Source ListView with checking in drag-and-drop operation</param>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs</param>
        protected virtual void HandleSetAsSourceListViewEvent(object sender, EventArgs e)
        {
            sourceListView = (fsListViewWithChecking)sender;
            if (((fsListViewWithChecking)sender).AllowedDrag)
            {
                sourceIndex = ((fsListViewWithCheckingEventArgs)e).Index;
                sourceCheckValue = ((fsListViewWithCheckingEventArgs)e).Checked;
                sourceItem = ((fsListViewWithCheckingEventArgs)e).Item;
            }
        }

        /// <summary>
        /// This field is created to prevent inappropriate OnAddAction, OnRemoveAction, 
        /// or OnCheckAction while drag-and-drop action as a whole is on.
        /// </summary>
        private bool isDropping = false;

        /// <summary>
        /// Subscribing to SetAsTargetListViewEvent
        /// </summary>
        /// <param name="sender">Target ListView with checking in drag-and-drop operation</param>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs</param>
        protected virtual void HandleSetAsTargetListViewEvent(object sender, EventArgs e)
        {
            // To prevent inappropriate behavior of drag-and-drop functionality
            // that can be caused by dragging from a control that is not a fsListViewWithChecking instance:
            if (sourceListView == null)
            {
                ((fsListViewWithChecking)sender).AllowedDrop = false;
                ((fsListViewWithChecking)sender).RemoveInsertionMark();
                return;
            }
            if (((fsListViewWithCheckingEventArgs)e).IsPreparatory)
            {
                if (sourceListView.AllowedDrag)
                {
                    ((fsListViewWithChecking)sender).AllowedDrop = DragDropChart[sourceListView].Contains((fsListViewWithChecking)sender) ||
                                                                           sourceListView.Equals((fsListViewWithChecking)sender);
                }
                else
                {
                    ((fsListViewWithChecking)sender).AllowedDrop = false;
                    ((fsListViewWithChecking)sender).RemoveInsertionMark();
                }
            }
            else
            {
                isDropping = true;
                targetListView = (fsListViewWithChecking)sender;
                int targetIndex = ((fsListViewWithCheckingEventArgs)e).Index;
                if (sourceListView.Equals(targetListView))
                {
                    sourceListView.RemoveInsertionMark();
                    sourceListView.DragFromTo(sourceIndex, targetIndex);
                }
                else
                {
                    if (targetListView.ContainsName((string)sourceItem))
                        throw new Exception("The target ListView already contains the name of a dragged item");
                    sourceListView.RemoveAt(sourceIndex);
                    targetListView.Insert(targetIndex, sourceItem, sourceCheckValue);
                }
                DragDropAction(sourceListView, targetListView, sourceIndex, targetIndex, sourceItem, sourceCheckValue);
                isDropping = false;
                // To prevent inappropriate behavior of drag-and-drop functionality
                // that can be caused by dragging from a control that is not a fsListViewWithChecking instance 
                sourceListView = null;
            }
        }

        /// <summary>
        /// Subscribing to CheckListViewItemEvent
        /// </summary>
        /// <param name="sender">ListView with checking in which an item is checked</param>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs</param>
        protected virtual void HandleCheckListViewItemEvent(object sender, EventArgs e)
        {
            if (!(isDropping || ((fsListViewWithChecking)sender).IsAdding ||
                ((fsListViewWithChecking)sender).IsRemoving ||
                ((fsListViewWithChecking)sender).isMultipleChecking))
            {
                CheckAction((fsListViewWithChecking)sender, ((fsListViewWithCheckingEventArgs)e).Index, ((fsListViewWithCheckingEventArgs)e).Checked);
            }
        }

        /// <summary>
        /// Subscribing to MultipleCheckListViewItemsEvent
        /// </summary>
        /// <param name="sender">ListView with checking in which a number of items is checked</param>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs</param>
        protected virtual void HandleMultipleCheckListViewItemsEvent(object sender, EventArgs e)
        {
            MultipleCheckAction((fsListViewWithChecking)sender, ((fsListViewWithCheckingEventArgs)e).IndicesAndChecks);
        }

        /// <summary>
        /// Subscribing to AddListViewItemEvent
        /// </summary>
        /// <param name="sender">ListView with checking in which an item is added</param>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs</param>
        protected virtual void HandleAddListViewItemEvent(object sender, EventArgs e)
        {
            if (!isDropping)
            {
                AddAction((fsListViewWithChecking)sender, ((fsListViewWithCheckingEventArgs)e).Index,
                          ((fsListViewWithCheckingEventArgs)e).Item, ((fsListViewWithCheckingEventArgs)e).Checked);
            }
        }

        /// <summary>
        /// Subscribing to RemoveListViewItemEvent
        /// </summary>
        /// <param name="sender">ListView with checking in which an item is removed</param>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs</param>
        protected virtual void HandleRemoveListViewItemEvent(object sender, EventArgs e)
        {
            if (!isDropping)
            {
                RemoveAction((fsListViewWithChecking)sender, ((fsListViewWithCheckingEventArgs)e).Index,
                             ((fsListViewWithCheckingEventArgs)e).Item, ((fsListViewWithCheckingEventArgs)e).Checked);
            }
        }
    }
}