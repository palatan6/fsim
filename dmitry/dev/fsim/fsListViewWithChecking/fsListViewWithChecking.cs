using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ListViewWithChecking
{
    /// <summary>
    /// Class implementing simple and fast operations (adding, removing, checking) on ListViews with checking
    /// having differents ListViewitem's names.
    /// Implements drag-and-drop functionality, smart items checking, and suit assigning actions to 
    /// adding, removing, checking, and dragging items between different instances of fsListViewWithChecking
    /// (with the use of fsListViewWithCheckingSupervisor class)
    /// </summary>
    public partial class fsListViewWithChecking : UserControl
    {
        private List<string> m_names;

        public virtual object this[int index]
        {
            get
            {
                if (index < 0 || m_count <= index)
                    throw new Exception("An attempt to get a fsListViewWithChecking item at an inappropriate index");
                return m_names[index];
            }
            set 
            {
                if (index < 0 || m_count <= index)
                    throw new Exception("An attempt to get a fsListViewWithChecking item at an inappropriate index");
                m_names[index] = (string)value;
            }
        }

        private List<bool> m_checks;

        private int m_count;

        public int Count { get { return m_count; } }

        #region Private doing with ListViewItems in m_listView

        private void AddItem()
        {
            m_listView.Items.Add("");
        }

        private void AddItemRange(int count)
        {
            if (count > 0)
                for (int i = 0; i < count; i++)
                {
                    AddItem();
                }
        }

        private void RemoveItem()
        {
            if (m_count > 0)
                m_listView.Items.RemoveAt(0);
            else
                throw new Exception("Attempt to remove an item from the empty m_listView");
        }

        #endregion

        #region Private doing with names in m_names

        private void InsertName(int index, string name)
        {
            try
            {
                m_names.Insert(index, name);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempt to insert the name at an inappropriate index");
            }
        }

        private void AddName(string name)
        {
            m_names.Add(name);
        }

        private void AddNameRange(IEnumerable<string> collection)
        {
            m_names.AddRange(collection);
        }

        private void RemoveNameAt(int index)
        {
            try
            {
                m_names.RemoveAt(index); ;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempt to remove the name at an inappropriate index");
            }
        }

        private bool RemoveName(string name)
        {
            return m_names.Remove(name);
        }

        #endregion

        #region Private doing with checks in m_checks

        private void InsertCheck(int index, bool check)
        {
            m_checks.Insert(index, check);
        }

        private void AddCheck(bool check)
        {
            m_checks.Add(check);
        }

        private void AddCheckRange(IEnumerable<bool> collection)
        {
            m_checks.AddRange(collection);
        }

        private void RemoveCheckAt(int index)
        {
            try
            {
                m_checks.RemoveAt(index); ;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempt to remove the check at an inappropriate index");
            }
        }

        #endregion

        #region Assigning

        /// <summary>
        /// Assigns names to a created fsListViewWithChecking instance.
        /// </summary>
        private void AssignNames()
        {
            for (int i = 0; i < m_count; i++)
            {
                m_listView.Items[i].Text = m_names[i];
            }
        }

        /// <summary>
        /// Assigns checks to a created fsListViewWithChecking instance.
        /// </summary>
        /// <returns>A dictionary of pairs ("index", new check value at "index") where a check value was changed at "index". </returns>
        private Dictionary<int, bool> AssignChecks()
        {
            var result = new Dictionary<int, bool>();
            for (int i = 0; i < m_count; i++)
            {
                bool checkValue = m_listView.Items[i].Checked;
                if (checkValue != m_checks[i])
                {
                    m_listView.Items[i].Checked = m_checks[i];
                    result.Add(i, m_checks[i]);
                }
            }
            return result;
        }

        /// <summary> 
        /// Assigns names and checks to a created fsListViewWithChecking instance.
        /// </summary>
        protected virtual void Assign()
        {
            AssignNames();
            AssignChecks();
        }

        #endregion

        #region Constructor

        /// <summary> 
        /// Creating an empty fsListViewWithChecking instance
        /// </summary>
        public fsListViewWithChecking()
        {
            InitializeComponent();
            m_names = new List<string>();
            m_checks = new List<bool>();
            m_count = 0;
            IsAdding = false;
            IsRemoving = false;
            isMultipleChecking = false;
            AllowedDrag = false;
            AllowedDrop = false;
        }

        /// <summary> 
        /// Adding items with names and checks to a fsListViewWithChecking instance.
        /// Items names are ordered by the natural order of "names". An item with the name "names[i]"
        /// has the checking "checks[i]". If the count of an either "names" or  "checks" is more than the
        /// count of another collection than the first one is truncated to be of an equal size.
        /// If any two names in "names" coincide an exception rises.
        /// </summary>
        /// <param name="names">Collection of names for ListViewItems</param>
        /// <param name="checks">Collection of checks for ListViewItems</param>
        public void AddToListViewWithChecking(IEnumerable<string> names, IEnumerable<bool> checks)
        {
            IsAdding = true;
            int namesCount = names.Count();
            for (int i = 0; i < namesCount - 1; i++)
            {
                string elementAt_i = names.ElementAt(i);
                for (int j = i + 1; j < namesCount; j++)
                {
                    if (elementAt_i.Equals(names.ElementAt(j)))
                        throw new Exception("The names of a fsListViewWithChecking must differ");
                }
            }
            int checksCount = checks.Count();
            int count = Math.Min(namesCount, checksCount);
            AddItemRange(count);
            m_count += count;
            if (namesCount == checksCount)
            {
                AddNameRange(names);
                AddCheckRange(checks);
            }
            else
            {
                if (checksCount < namesCount)
                {
                    for (int i = 0; i < checksCount; i++)
                    {
                        AddName(names.ElementAt(i));
                    }
                    AddCheckRange(checks);
                }
                else
                {
                    AddNameRange(names);
                    for (int i = 0; i < namesCount; i++)
                    {
                        AddCheck(checks.ElementAt(i));
                    }
                }
            }
            Assign();
            IsAdding = false;
        }

        /// <summary> 
        /// Adding items with names to a fsListViewWithChecking instance.
        /// Items names are ordered by the natural order of "names". All the added items are checked.
        /// </summary>
        /// <param name="names">Collection of names for ListViewItems</param>
        public void AddToListViewWithChecking(IEnumerable<string> names)
        {
            int namesCount = names.Count();
            var checks = new bool[namesCount];
            for (int i = 0; i < namesCount; i++)
            {
                checks[i] = true;
            }
            AddToListViewWithChecking(names, checks);
        }

        #endregion

        #region Doing with fsListViewWithChecking

        #region Events for constructing items

        /// <summary>
        /// An event which raises when an item is added to a fsListViewWithChecking instance
        /// </summary>
        public event EventHandler<EventArgs> AddListViewItemEvent;

        /// <summary>
        /// Publishing AddListViewItemEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnAddListViewItemEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = AddListViewItemEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        /// <summary>
        /// An event which raises when an item is removed from a fsListViewWithChecking instance
        /// </summary>
        public event EventHandler<EventArgs> RemoveListViewItemEvent;

        /// <summary>
        /// Publishing RemoveListViewItemEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnRemoveListViewItemEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = RemoveListViewItemEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        #endregion

        #region Constructing items

        /// <summary> 
        /// This flag displays if the process of adding items is on.
        /// </summary>
        public bool IsAdding { get; private set; }

        /// <summary> 
        /// This flag displays if the process of removing items is on.
        /// </summary>
        public bool IsRemoving { get; private set; }

        /// <summary>
        /// True if a fsListViewWithChecking instance contains an item with the name "name"; otherwise, false.
        /// </summary>
        /// <param name="name">Any string</param>
        public bool ContainsName(string name)
        {
            return m_names.Contains(name);
        }

        /// <summary> 
        /// Inserts an item "item" into a fsListViewWithChecking instance at index "index" 
        /// with a check value "isChecked".
        /// </summary>
        /// <param name="index">Index to insert at</param>
        /// <param name="item">Item to insert</param>
        /// <param name="isChecked">Check value to set</param>
        public virtual void Insert(int index, object item, bool isChecked)
        {
            if (ContainsName((string)item))
            {
                throw new Exception("The name of an item to be inserted already exists");
            }
            IsAdding = true;
            try
            {
                InsertName(index, (string)item);
            }
            catch (Exception)
            {
                IsAdding = false;
                throw new Exception("An attempt to insert an item at an inapproptiate index");
            }
            InsertCheck(index, isChecked);
            AddItem();
            m_count++;
            Assign();
            OnAddListViewItemEvent(new fsListViewWithCheckingEventArgs((string)item, index, isChecked));
            IsAdding = false;
        }

        /// <summary> 
        /// Adds an item "item" with a check value "isChecked" to a fsListViewWithChecking instance.
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <param name="isChecked">Check value to set</param>
        public virtual void Add(object item, bool isChecked)
        {
            if (ContainsName((string)item))
            {
                throw new Exception("The name of an item to be added already exists");
            }
            IsAdding = true;
            m_listView.Items.Add((string)item);
            m_listView.Items[m_count++].Checked = isChecked;
            AddName((string)item);
            AddCheck(isChecked);
            OnAddListViewItemEvent(new fsListViewWithCheckingEventArgs((string)item, m_count - 1, isChecked));
            IsAdding = false;
        }

        /// <summary> 
        /// Removes an item from a fsListViewWithChecking instance at index "index".
        /// If "index" is negative or more than the items count then an exception rises.
        /// </summary>
        /// <param name="index">Index to remove at</param>
        public virtual void RemoveAt(int index)
        {
            IsRemoving = true;
            string name = m_names[index];
            bool isChecked = m_checks[index];
            try
            {
                RemoveNameAt(index);
            }
            catch (Exception)
            {
                IsRemoving = false;
                throw new Exception("An attempt to remove the item at an inapproptiate index");
            }
            RemoveItem();
            RemoveCheckAt(index);
            m_count--;
            Assign();
            OnRemoveListViewItemEvent(new fsListViewWithCheckingEventArgs(name, index, isChecked));
            IsRemoving = false;
        }

        /// <summary> 
        /// Removes an item "item" from a fsListViewWithChecking instance;
        /// true if item is successfully removed; otherwise, false.
        /// </summary>
        /// <param name="item">Item to remove </param>
        public virtual bool Remove(object item)
        {
            int index = m_names.IndexOf((string)item);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }

        /// <summary> 
        /// Drags an item on a fsListViewWithChecking instance from the place 
        /// with the index "index1" and drops it on the place with the index "index2"
        /// </summary>
        /// <param name="index1">Index to drag from </param>
        /// <param name="index2">Index to drop on </param>
        public virtual void DragFromTo(int index1, int index2)
        {
            if (index1 == index2)
                return;
            string name1 = m_names[index1];
            bool check1 = m_checks[index1];
            m_names.RemoveAt(index1);
            m_checks.RemoveAt(index1);
            if (index1 < index2)
            {
                m_names.Insert(index2 - 1, name1);
                m_checks.Insert(index2 - 1, check1);
            }
            else
            {
                m_names.Insert(index2, name1);
                m_checks.Insert(index2, check1);
            }
            Assign();
        }

        #endregion

        #region Checking items

        /// <summary>
        /// This flag displays whether the process of multiple checking items is on.
        /// </summary>
        public bool isMultipleChecking { get; private set; }

        #region {Checking items} events

        /// <summary>
        /// An event which raises when an item of a fsListViewWithChecking instance is checked
        /// </summary>
        public event EventHandler<EventArgs> CheckListViewItemEvent;

        /// <summary>
        /// Publishing CheckListViewItemEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnCheckListViewItemEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = CheckListViewItemEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        /// <summary>
        /// An event which raises when a number of items of a fsListViewWithChecking instance is checked
        /// </summary>
        public event EventHandler<EventArgs> MultipleCheckListViewItemsEvent;

        /// <summary>
        /// Publishing CheckListViewItemEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnMultipleCheckListViewItemsEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = MultipleCheckListViewItemsEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            if (isfsListViewWithChecking(sender))
            {
                Point targetPoint = new Point(e.X, e.Y); // Mouse pointer coordinates in (ListView)sender)
                int InsertionMarkIndex = ((ListView)sender).InsertionMark.NearestIndex(targetPoint);
                // If mouse reaches the area of an item; the first one is determined 
                // by a possibility to insert Insertion Mark 
                if (InsertionMarkIndex > -1)
                {
                    Rectangle itemBounds = ((ListView)sender).GetItemRect(InsertionMarkIndex);
                    // If mouse pointer lies in the item rectangle in (ListView)sender)
                    if (itemBounds.X <= targetPoint.X && targetPoint.X <= itemBounds.Right &&
                        itemBounds.Top <= targetPoint.Y && targetPoint.Y <= itemBounds.Bottom)
                    {
                        // We force (ListView)sender to be focused for immediate selection of an item
                        // in whose rectangle lies mouse pointer.  
                        ((ListView)sender).Focus();
                        ((ListView)sender).Items[InsertionMarkIndex].Selected = true;
                    }
                }
            }
        }

        private void ListViewItem_Checked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Selected)
            {
                bool checkValue = e.Item.Checked;
                m_checks[e.Item.Index] = checkValue;
                OnCheckListViewItemEvent(new fsListViewWithCheckingEventArgs(e.Item));
            }
        }

        #endregion

        #region Doing with checking

        private void SetCheckAll(bool isChecked)
        {
            for (int i = 0; i < m_count; i++)
            {
                m_checks[i] = isChecked;
            }
        }

        private void SetCheck(IEnumerable<int> indices, bool isChecked)
        {
            foreach (var index in indices)
            {
                if (0 <= index && index < m_count)
                    m_checks[index] = isChecked;
            }
        }

        /// <summary> 
        /// Returns the list of all items in fsListViewWithChecking
        /// with the check value "isChecked"
        /// </summary>
        /// <param name="isChecked">check value </param>
        protected virtual List<object> GetItemsWithCheckValue(bool isChecked)
        {
            List<object> checkValueItems = new List<object>();
            for (int i = 0; i < m_count; i++)
            {
                if (m_checks[i] == isChecked)
                    checkValueItems.Add(m_names[i]);
            }
            return checkValueItems;
        }

        private List<int> GetIndicesWithCheckValue(bool isChecked)
        {
            List<int> checkValueIndices = new List<int>();
            for (int i = 0; i < m_count; i++)
            {
                if (m_listView.Items[i].Checked == isChecked)
                    checkValueIndices.Add(i);
            }
            return checkValueIndices;
        }

        private void MakeChecking()
        {
            isMultipleChecking = true;
            OnMultipleCheckListViewItemsEvent(new fsListViewWithCheckingEventArgs(AssignChecks()));
            isMultipleChecking = false;
        }

        /// <summary> 
        /// Checks all items in  fsListViewWithChecking
        /// </summary>
        public void CheckAll()
        {
            SetCheckAll(true);
            MakeChecking();
        }

        /// <summary> 
        /// Unchecks all items in  fsListViewWithChecking
        /// </summary>
        public void UncheckAll()
        {
            SetCheckAll(false);
            MakeChecking();
        }

        /// <summary> 
        /// Checks all items in fsListViewWithChecking with appropriate indices
        /// from the collection "indices"
        /// </summary>
        /// <param name="indices">Collection of indices to check at</param>
        public void Check(IEnumerable<int> indices)
        {
            SetCheck(indices, true);
            MakeChecking();
        }

        /// <summary> 
        /// Unchecks all items in fsListViewWithChecking with appropriate indices
        /// from the collection "indices"
        /// </summary>
        /// <param name="indices">Collection of indices to check at</param>
        public void Uncheck(IEnumerable<int> indices)
        {
            SetCheck(indices, false);
            MakeChecking();
        }

        /// <summary> 
        /// Returns the list of all checked items in fsListViewWithChecking
        /// </summary>
        public virtual List<object> GetCheckedItems()
        {
            return GetItemsWithCheckValue(true);
        }

        /// <summary> 
        /// Returns the list of all unchecked items in fsListViewWithChecking
        /// </summary>
        public virtual List<object> GetUncheckedItems()
        {
            return GetItemsWithCheckValue(false);
        }

        /// <summary> 
        /// Returns the list of indices of all checked items in fsListViewWithChecking
        /// </summary>
        public List<int> GetCheckedIndices()
        {
            return GetIndicesWithCheckValue(true);
        }

        /// <summary> 
        /// Returns the list of indices of all unchecked items in fsListViewWithChecking
        /// </summary>
        public List<int> GetUncheckedIndices()
        {
            return GetIndicesWithCheckValue(false);
        }

        #endregion

        #endregion

        #region Drag-and-drop functionality

        #region {Setting as source ListView} event

        /// <summary>
        /// An event which raises when an item has been started to be dragged on a fsListViewWithChecking instance;
        /// setting that instance as the source ListView
        /// </summary>
        public event EventHandler<EventArgs> SetAsSourceListViewEvent;

        /// <summary>
        /// Publishing SetAsSourceListViewEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnSetAsSourceListViewEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = SetAsSourceListViewEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        #endregion

        #region {Setting as target ListView} event

        /// <summary>
        /// An event which raises when an item has been dropped on a fsListViewWithChecking instance;
        /// setting that instance as the target ListView
        /// </summary>
        public event EventHandler<EventArgs> SetAsTargetListViewEvent;

        /// <summary>
        /// Publishing SetAsTargetListViewEvent
        /// </summary>
        /// <param name="e">An event argument which is to be converted to an appropriate type of a class which inherits EventArgs </param>
        protected virtual void OnSetAsTargetListViewEvent(EventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<EventArgs> handler = SetAsTargetListViewEvent;

            // Event will be null if there are no subscribers
            if (handler != null)
                handler(this, (fsListViewWithCheckingEventArgs)e);
        }

        #endregion

        #region ItemDrag action

        private string draggedItemName;

        private bool draggedItemCheckValue;

        /// <summary>
        /// True if this fsListViewWithChecking instance is allowed to drag on; otherwise, false.
        /// </summary>
        public bool AllowedDrag { get; set; }

        // Starts the drag-and-drop operation when an item is dragged.
        private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (AllowedDrag)
            {
                draggedItemName = ((ListViewItem)e.Item).Text;
                draggedItemCheckValue = ((ListViewItem)e.Item).Checked;
                OnSetAsSourceListViewEvent(new fsListViewWithCheckingEventArgs((ListViewItem)e.Item));
                ((ListView)sender).DoDragDrop(e.Item, DragDropEffects.Move);
            }
            else
            {
                OnSetAsSourceListViewEvent(new fsListViewWithCheckingEventArgs());
                ((ListView)sender).DoDragDrop(e.Item, DragDropEffects.None);
            }
        }

        #endregion

        #region DragEnter action

        /// <summary>
        /// True if this fsListViewWithChecking instance is allowed to drop on; otherwise, false.
        /// </summary>
        public bool AllowedDrop { get; set; }

        private bool isfsListViewWithChecking(object someObject)
        {
            if (someObject is ListView)
            {
                if (((ListView)someObject).Parent is fsListViewWithChecking)
                    return true;
                else
                    return false;
            }
            return false;
        }

        // Sets the target drop effect.
        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            if (isfsListViewWithChecking(sender))
            {
                OnSetAsTargetListViewEvent(new fsListViewWithCheckingEventArgs());
                if (AllowedDrop)
                    e.Effect = e.AllowedEffect;
            }
        }

        #endregion

        #region DragOver action

        /// <summary>
        /// The color of the insertion mark for this fsListViewWithChecking instance.
        /// </summary>
        public Color InsertionMarkColor { get; set; }

        // Moves the insertion mark as the item is dragged.
        private void ListView_DragOver(object sender, DragEventArgs e)
        {
            if (AllowedDrop)
            {
                ((ListView)sender).InsertionMark.Color = InsertionMarkColor;

                if (((ListView)sender).Items.Count == 0)
                {
                    ((ListView)sender).InsertionMark.Index = 0;
                    ((ListView)sender).InsertionMark.AppearsAfterItem = false;
                    return;
                }

                // Retrieve the client coordinates of the mouse pointer.
                Point targetPoint = ((ListView)sender).PointToClient(new Point(e.X, e.Y));

                // Retrieve the index of the item line closest to the mouse pointer.
                int InsertionMarkIndex = ((ListView)sender).InsertionMark.NearestIndex(new Point(0, targetPoint.Y));

                // Confirm that the mouse pointer is not over the dragged item.
                if (InsertionMarkIndex > -1)
                {
                    // Determine whether the mouse pointer is to the top or
                    // the bottom of the midpoint of the closest item and set
                    // the InsertionMark.AppearsAfterItem property accordingly.
                    Rectangle itemBounds = ((ListView)sender).GetItemRect(InsertionMarkIndex);
                    if (targetPoint.Y > itemBounds.Bottom + (itemBounds.Height / 2))
                        ((ListView)sender).InsertionMark.AppearsAfterItem = true;
                    else
                        ((ListView)sender).InsertionMark.AppearsAfterItem = false;
                }

                // Set the location of the insertion mark. If the mouse is
                // over the dragged item, the targetIndex value is -1 and
                // the insertion mark disappears.
                ((ListView)sender).InsertionMark.Index = InsertionMarkIndex;
            }
        }

        #endregion

        #region DragLeave action

        /// <summary>
        /// Assigning the value -1 to the index of the insertion mark for this fsListViewWithChecking instance.
        /// </summary>
        public void RemoveInsertionMark()
        {
            m_listView.InsertionMark.Index = -1;
        }

        // Removes the insertion mark when the mouse leaves the control.
        private void ListView_DragLeave(object sender, EventArgs e)
        {
            ((fsListViewWithChecking)((ListView)sender).Parent).RemoveInsertionMark();
        }

        #endregion

        #region DragDrop action

        private int targetIndex;

        // Moves the item to the location of the insertion mark.
        private void ListView_DragDrop(object sender, DragEventArgs e)
        {
            targetIndex = ((ListView)sender).InsertionMark.Index;

            // If the insertion mark is not visible, exit the method.
            if (targetIndex == -1)
                return;

            // If the insertion mark is to the bottom of the item with
            // the corresponding index, increment the target index.
            if (((ListView)sender).InsertionMark.AppearsAfterItem)
                targetIndex++;

            OnSetAsTargetListViewEvent(new fsListViewWithCheckingEventArgs(draggedItemName, targetIndex, draggedItemCheckValue));
        }

        #endregion

        #endregion

        #endregion
    }
}
